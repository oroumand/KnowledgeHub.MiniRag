# نحوه استفاده از سیستم

در این فایل، یک سناریوی کامل از استفاده از سیستم RAG پیاده‌سازی‌شده در پروژه توضیح داده می‌شود.

هدف این است که ببینیم یک کاربر چگونه از ابتدا تا انتها با سیستم کار می‌کند.

---

# سناریو: ساخت یک چت‌بات بر اساس مقالات

فرض کنید شما مجموعه‌ای از مقاله‌ها دارید و می‌خواهید:

- آن‌ها را وارد سیستم کنید
- روی آن‌ها جستجوی معنایی انجام دهید
- و یک چت‌بات بسازید که فقط بر اساس همین داده‌ها پاسخ بدهد

---

# مرحله 1: ثبت یک سند

ابتدا یک سند را وارد سیستم می‌کنیم.

POST /documents

{
  "title": "Layered Architecture Notes",
  "rawText": "Layered architecture is one of the most common patterns in enterprise systems. It organizes code into layers such as presentation, application, domain, and infrastructure. A major downside is that changes may need to cross many layers, which can slow delivery.",
  "sourceType": "Book",
  "author": "Practice Notes"
}

---

# چه اتفاقی می‌افتد؟

سیستم:

1. متن را chunk می‌کند  
2. chunkها را در SQL ذخیره می‌کند  
3. برای هر chunk embedding تولید می‌کند  
4. embeddingها را در Qdrant ذخیره می‌کند  

---

# مرحله 2: جستجوی معنایی

حالا می‌خواهیم ببینیم سیستم می‌تواند متن مرتبط پیدا کند یا نه.

POST /search/semantic

{
  "query": "Why can layered architecture slow development?",
  "topK": 3
}

---

# نتیجه

سیستم:

- query را embedding می‌کند  
- در Qdrant جستجو می‌کند  
- chunkهای مرتبط را پیدا می‌کند  
- متن آن‌ها را از SQL برمی‌گرداند  

---

# مرحله 3: چت با سیستم (RAG)

حالا می‌خواهیم از سیستم سوال بپرسیم.

POST /chat/ask

{
  "question": "What is a downside of layered architecture?",
  "topK": 3
}

---

# چه اتفاقی می‌افتد؟

سیستم:

1. semantic search انجام می‌دهد  
2. chunkهای مرتبط را پیدا می‌کند  
3. یک prompt می‌سازد  
4. prompt را به OpenAI می‌دهد  
5. پاسخ تولید می‌شود  
6. پاسخ + منابع برگردانده می‌شود  

---

# نمونه پاسخ

{
  "answer": "A downside of layered architecture is that changes may need to cross multiple layers, which can slow delivery.",
  "sources": [...]
}

---

# نکات مهم در استفاده

## 1. کیفیت داده مهم است

هرچه متن ورودی بهتر باشد:
- جستجو بهتر می‌شود
- پاسخ دقیق‌تر می‌شود

---

## 2. chunking تاثیر مستقیم دارد

اگر chunkها:
- خیلی بزرگ باشند → دقت کم می‌شود  
- خیلی کوچک باشند → context ناقص می‌شود  

---

## 3. سوال خوب بپرسید

سوال واضح و مشخص:
- نتیجه بهتر می‌دهد

---

## 4. سیستم حافظه ندارد

در نسخه فعلی:
- هر سوال مستقل است  
- history نگهداری نمی‌شود  

---

# محدودیت‌های فعلی

- عدم پشتیبانی از فایل PDF  
- عدم پشتیبانی از URL  
- عدم chat history  
- عدم re-index  

---

# جمع‌بندی

در این پروژه شما می‌توانید:

1. داده‌های خود را وارد کنید  
2. آن‌ها را به صورت معنایی جستجو کنید  
3. و یک چت‌بات بسازید که فقط بر اساس همان داده‌ها پاسخ دهد  

این همان هسته اصلی یک سیستم RAG است.