# نمای کلی معماری پروژه

پروژه `KnowledgeHub.MiniRag` یک نمونه ساده ولی واقعی از پیاده‌سازی **RAG (Retrieval-Augmented Generation)** در اکوسیستم .NET است.

هدف این پروژه این نیست که یک محصول enterprise-ready نهایی باشد، بلکه هدف آن این است که:

- مفاهیم اصلی RAG را به‌صورت عملی نشان بدهد
- یک نمونه آموزشی تمیز و قابل فهم ارائه کند
- پایه‌ای مناسب برای توسعه نسخه‌های بعدی بسازد

---

# هدف‌های معماری پروژه

در طراحی این پروژه، چند هدف اصلی در نظر گرفته شده است:

- کد برای توسعه‌دهنده قابل فهم باشد
- مرز بین مسئولیت‌ها روشن باشد
- پروژه برای یادگیری مناسب باشد
- توسعه مرحله‌ای و incremental آسان باشد
- مستندسازی تصمیم‌های معماری ممکن باشد

---

# سبک معماری

این پروژه از یک رویکرد ترکیبی استفاده می‌کند:

- **Clean Architecture (سبک و ساده‌شده)**
- **Feature-based Organization**
- **Separation of Concerns**

یعنی:
- مرز لایه‌ها حفظ شده
- اما داخل لایه‌ها، کدها بر اساس featureها سازمان‌دهی شده‌اند

---

# لایه‌های اصلی پروژه

## 1. Core.Domain

این لایه شامل مفاهیم اصلی دامنه است.

نمونه‌ها:
- `SourceDocument`
- `DocumentChunk`
- `DocumentSourceType`

این لایه باید تا حد ممکن خالص بماند و به جزئیات فنی وابسته نباشد.

---

## 2. Core.Application

این لایه شامل use caseها و abstractionهای اصلی سیستم است.

نمونه‌ها:
- `DocumentIngestionService`
- `SemanticSearchService`
- `RagChatService`

همچنین قراردادهایی مثل این‌ها در این لایه تعریف شده‌اند:
- `IApplicationDbContext`
- `IEmbeddingService`
- `ITextChunkingService`
- `IChatCompletionService`
- `IVectorStoreService`

---

## 3. Infras.SqlServer

این پروژه فقط مسئول persistence رابطه‌ای است.

نمونه مسئولیت‌ها:
- `ApplicationDbContext`
- EF Core configurations
- SQL Server integration
- dependency injection مربوط به دیتابیس

هدف این جداسازی این است که concerns مربوط به دیتابیس رابطه‌ای با concerns مربوط به AI قاطی نشوند.

---

## 4. Infras.AI

این پروژه مسئول یکپارچه‌سازی با سرویس‌های AI و vector infrastructure است.

نمونه مسئولیت‌ها:
- OpenAI Embedding integration
- OpenAI Chat integration
- Qdrant Vector Store integration
- dependency injection مربوط به AI

این پروژه عمداً از `Infras.SqlServer` جدا شده تا مرز زیرساخت AI و persistence روشن‌تر باشد.

---

## 5. Endpoints.Api

این پروژه نقطه ورود HTTP به سیستم است.

مسئولیت‌ها:
- تعریف endpointها
- دریافت request
- بازگرداندن response
- اتصال لایه‌ها در `Program.cs`

این لایه نباید محل اصلی business logic باشد.

---

# ساختار feature-based

در این پروژه، کدها داخل لایه‌ها بر اساس featureها سازمان‌دهی شده‌اند.

مثال:

- `Documents`
- `Search`
- `Chat`
- `Shared`

هدف این تصمیم:
- پیدا کردن کدهای مرتبط آسان‌تر شود
- رشد پروژه مدیریت‌پذیرتر باشد
- خوانایی برای توسعه‌دهندگان بهتر شود

---

# منبع حقیقت داده‌ها

در این پروژه:

- **SQL Server** منبع حقیقت داده‌های اصلی است
- **Qdrant** فقط برای retrieval معنایی استفاده می‌شود

یعنی:
- متن کامل سند و chunkها در SQL نگه‌داری می‌شوند
- embeddingها و indexهای برداری در Qdrant ذخیره می‌شوند

این تصمیم باعث می‌شود:
- رابطه بین داده اصلی و retrieval روشن باشد
- مهاجرت یا re-index ساده‌تر شود
- سیستم قابل دیباگ‌تر باشد

---

# جریان کلی داده در پروژه

جریان ساده داده در نسخه 1 این‌طور است:

1. کاربر یک سند ثبت می‌کند
2. متن سند chunk می‌شود
3. chunkها در SQL ذخیره می‌شوند
4. برای هر chunk embedding تولید می‌شود
5. embeddingها در Qdrant ذخیره می‌شوند
6. هنگام جستجو، query کاربر embedding می‌شود
7. Qdrant نزدیک‌ترین chunkها را پیدا می‌کند
8. متن واقعی chunkها از SQL خوانده می‌شود
9. در حالت chat، این context به مدل داده می‌شود تا پاسخ grounded تولید شود

---

# تصمیم‌های مهم معماری

چند تصمیم مهم این پروژه با ADR ثبت شده‌اند، از جمله:

- استفاده از Clean Architecture سبک
- استفاده از SQL Server به عنوان relational store
- استفاده از Qdrant برای vector storage
- استفاده از OpenAI برای embedding و chat
- استفاده از ساختار feature-based
- استفاده از `DbSet` در Application به‌جای Repository
- جداسازی `Infras.AI` از `Infras.SqlServer`

جزئیات این تصمیم‌ها در پوشه `docs/adr` ثبت شده‌اند.

---

# ساده‌سازی‌های آگاهانه در نسخه 1

این پروژه برای نسخه اول، عمداً برخی پیچیدگی‌ها را حذف کرده است:

- Repository pattern کامل
- background processing
- indexing status
- retry strategy
- chat history
- test automation
- PDF / URL ingestion

این حذف‌ها آگاهانه بوده‌اند تا پروژه:
- سریع‌تر قابل توسعه باشد
- برای آموزش مناسب بماند
- مفاهیم اصلی RAG را شفاف‌تر نشان دهد

---

# مسیر توسعه آینده

در نسخه‌های بعدی می‌توان این پروژه را توسعه داد با:

- re-index کردن اسناد
- حذف سند و پاک‌سازی vectorها
- chat session و message history
- ingestion از فایل PDF
- ingestion از URL
- background indexing
- بهبود prompt engineering
- تست‌های خودکار

---

# جمع‌بندی

`KnowledgeHub.MiniRag` یک نمونه آموزشی و قابل توسعه از معماری RAG در .NET است که تلاش می‌کند بین این سه مورد تعادل برقرار کند:

- سادگی
- تمیزی معماری
- قابلیت رشد

این پروژه برای توسعه‌دهندگانی طراحی شده که می‌خواهند منطق RAG را نه فقط در حد تئوری، بلکه در قالب یک سیستم واقعی و قابل اجرا یاد بگیرند.