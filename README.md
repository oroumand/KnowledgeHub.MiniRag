# KnowledgeHub.MiniRag

یک پروژه نمونه برای پیاده‌سازی **RAG (Retrieval-Augmented Generation)** با استفاده از:

- ASP.NET Core (.NET 10)
- EF Core + SQL Server
- OpenAI (Embedding + Chat)
- Qdrant (Vector Database)

---

## 🎯 هدف پروژه

هدف این پروژه ساخت یک **چت‌بات ساده مبتنی بر داده‌های شخصی** است که بتواند:

- مقالات یا متن‌های شما را دریافت کند
- آن‌ها را به chunkهای کوچک‌تر تقسیم کند
- embedding تولید کند
- در vector database ذخیره کند
- و در نهایت به صورت **semantic search + chat** پاسخ بدهد

---

## 🧠 RAG چیست؟

RAG یعنی:

> ترکیب جستجوی معنایی (Semantic Search) با مدل‌های زبانی (LLM)

به زبان ساده:

1. سوال کاربر گرفته می‌شود
2. مرتبط‌ترین متن‌ها از دیتابیس پیدا می‌شوند
3. این متن‌ها به مدل داده می‌شوند
4. مدل فقط بر اساس آن‌ها پاسخ می‌دهد

---

## 🏗️ معماری پروژه

این پروژه بر اساس:

- Clean Architecture (سبک سبک!)
- Feature-based structure
- Separation of concerns

ساخته شده است.

### لایه‌ها:

- Core (Domain + Application)
- Infras.SqlServer (Persistence)
- Infras.AI (OpenAI + Qdrant)
- Endpoints (API)

---

## ⚙️ تکنولوژی‌ها

| بخش | تکنولوژی |
|-----|--------|
API | ASP.NET Core |
ORM | EF Core |
Database | SQL Server |
Vector DB | Qdrant |
AI | OpenAI |

---

## 🚀 قابلیت‌های نسخه 1

- ثبت سند
- chunk کردن متن
- تولید embedding
- ذخیره در Qdrant
- جستجوی معنایی (Semantic Search)
- چت مبتنی بر RAG
- مشاهده سند و chunkها

---

## 🧪 اجرای پروژه

مراحل اجرا در این فایل توضیح داده شده:

👉 `docs/development/local-setup.md`

---

## 📚 مستندات

- معماری: `docs/architecture/overview.md`
- ساختار پروژه: `docs/architecture/project-structure.md`
- RAG pipeline: `docs/rag/rag-pipeline.md`
- نحوه استفاده: `docs/development/how-to-use.md`

---

## 📌 نکات مهم

- این پروژه برای **آموزش و نمونه‌سازی** طراحی شده
- برخی تصمیم‌ها (مثل عدم استفاده از Repository) آگاهانه ساده‌سازی شده‌اند
- برای production نیاز به بهبود دارد

---

## 🧭 برنامه نسخه‌های بعدی

- chat history
- re-index
- PDF ingestion
- URL ingestion
- background processing

---

## 🪪 License

MIT