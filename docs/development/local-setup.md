# راه‌اندازی پروژه به صورت محلی

در این فایل، مراحل اجرای پروژه KnowledgeHub.MiniRag روی سیستم محلی توضیح داده شده است.

---

# پیش‌نیازها

قبل از اجرای پروژه، مطمئن شوید موارد زیر نصب شده‌اند:

- .NET SDK (نسخه 10 یا بالاتر)
- Docker (برای اجرای Qdrant)
- SQL Server (یا SQL Server Express)

---

# 1. کلون کردن پروژه

git clone https://github.com/oroumand/KnowledgeHub.MiniRag.git  
cd KnowledgeHub.MiniRag

---

# 2. اجرای Qdrant (Vector Database)

docker pull qdrant/qdrant  
docker run -p 6333:6333 -p 6334:6334 qdrant/qdrant

---

# 3. تنظیم اتصال به دیتابیس

فایل appsettings.json را باز کنید و مقدار ConnectionStrings را تنظیم کنید:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=KnowledgeHubMiniRagDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}

---

# 4. اجرای Migration

دستور زیر را در ریشه پروژه اجرا کنید:

dotnet ef database update --project src/02.Infras/KnowledgeHub.MiniRag.Infras.SqlServer --startup-project src/03.Endpoints/KnowledgeHub.MiniRag.Endpoints.Api

---

# 5. تنظیم API Key برای OpenAI

در ویندوز:

setx OPENAI_API_KEY "YOUR_API_KEY"

بعد از اجرای این دستور، یک ترمینال جدید باز کنید.

---

# 6. اجرای پروژه

dotnet run --project src/03.Endpoints/KnowledgeHub.MiniRag.Endpoints.Api

---

# 7. تست اولیه

## تست سلامت API

GET /

---

## ثبت سند

POST /documents

{
  "title": "Layered Architecture Notes",
  "rawText": "Layered architecture is one of the most common patterns in enterprise systems...",
  "sourceType": "Book",
  "author": "Practice Notes"
}

---

## جستجوی معنایی

POST /search/semantic

{
  "query": "Why is layered architecture slow?",
  "topK": 3
}

---

## چت RAG

POST /chat/ask

{
  "question": "What is a downside of layered architecture?",
  "topK": 3
}

---

# نکات مهم

- Qdrant باید در حال اجرا باشد
- API Key باید تنظیم شده باشد
- دیتابیس باید migration شده باشد

---

# خطاهای رایج

## خطای OpenAI

- API key تنظیم نشده
- اینترنت در دسترس نیست

---

## خطای Qdrant

- Docker اجرا نشده
- پورت 6333 در دسترس نیست

---

## خطای EF Core

- connection string اشتباه است
- migration اجرا نشده

---

# جمع‌بندی

با انجام مراحل بالا، پروژه به صورت کامل اجرا خواهد شد و می‌توانید:

- سند ثبت کنید
- جستجوی معنایی انجام دهید
- و چت RAG را تست کنید