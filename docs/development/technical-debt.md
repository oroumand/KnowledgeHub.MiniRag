# بدهی‌های فنی (Technical Debt)

در این فایل، مواردی که به صورت آگاهانه در نسخه 1 ساده‌سازی شده‌اند ثبت شده است.

هدف:
- فراموش نشدن این موارد
- برنامه‌ریزی برای بهبود در آینده

---

# 1. Query در لایه API

در حال حاضر:
- برخی read queryها مستقیماً در Endpoints نوشته شده‌اند

دلیل:
- سادگی
- سرعت توسعه

وضعیت آینده:
- انتقال به Application layer

---

# 2. نبود Indexing Status

در حال حاضر:
- مشخص نیست یک chunk یا document index شده یا نه

مشکل:
- در صورت خطا، وضعیت نامشخص است

بهبود آینده:
- اضافه کردن status (Pending / Indexed / Failed)

---

# 3. عدم وجود Retry

در حال حاضر:
- اگر OpenAI یا Qdrant خطا بدهد، retry نداریم

بهبود:
- retry policy
- resilience

---

# 4. نبود Background Processing

در حال حاضر:
- ingestion به صورت synchronous انجام می‌شود

مشکل:
- کندی در داده‌های بزرگ

بهبود:
- background jobs
- queue

---

# 5. عدم وجود Chat History

در حال حاضر:
- هر درخواست chat مستقل است

بهبود:
- ChatSession
- ChatMessage
- conversation context

---

# 6. نبود Re-index

در حال حاضر:
- امکان re-index داده وجود ندارد

بهبود:
- endpoint برای re-index
- sync بین SQL و Qdrant

---

# 7. نبود Test

در حال حاضر:
- هیچ تستی نوشته نشده

بهبود:
- unit test
- integration test

---

# 8. Chunking ساده

در حال حاضر:
- chunking فقط بر اساس طول متن است

بهبود:
- sentence-based chunking
- overlap
- smarter splitting

---

# جمع‌بندی

این موارد عمداً برای نسخه 1 ساده نگه داشته شده‌اند تا:

- تمرکز روی مفاهیم اصلی باشد
- توسعه سریع‌تر انجام شود

در نسخه‌های بعدی این موارد بهبود داده خواهند شد.