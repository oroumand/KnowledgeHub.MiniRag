# ساختار پروژه

در این فایل، ساختار کلی پروژه `KnowledgeHub.MiniRag` و نقش هر بخش توضیح داده شده است.

هدف این ساختار:
- خوانایی بالا
- جداسازی مسئولیت‌ها
- قابلیت توسعه آسان

---

# ساختار کلی

src
 ├ 01.Core
 │  ├ KnowledgeHub.MiniRag.Core.Domain
 │  └ KnowledgeHub.MiniRag.Core.Application
 │
 ├ 02.Infras
 │  ├ KnowledgeHub.MiniRag.Infras.SqlServer
 │  └ KnowledgeHub.MiniRag.Infras.AI
 │
 └ 03.Endpoints
    └ KnowledgeHub.MiniRag.Endpoints.Api

---

# توضیح لایه‌ها

## 01.Core

این لایه شامل منطق اصلی سیستم است و به جزئیات فنی وابسته نیست.

---

### Core.Domain

شامل مدل‌های دامنه:

نمونه‌ها:
- SourceDocument
- DocumentChunk
- DocumentSourceType

این لایه:
- ساده است
- وابسته به EF Core یا OpenAI نیست
- فقط مفاهیم اصلی را نگه می‌دارد

---

### Core.Application

این لایه شامل:
- use caseها
- سرویس‌های اصلی
- abstractionها

---

### ساختار داخلی Application

Documents/
Search/
Chat/
Shared/

---

### Documents

- DocumentIngestionService
- ثبت سند
- chunk کردن متن
- شروع pipeline ingestion

---

### Search

- SemanticSearchService
- جستجوی معنایی روی chunkها

---

### Chat

- RagChatService
- ساخت prompt
- ترکیب context و سوال کاربر

---

### Shared

شامل abstractionهای مشترک:

- IApplicationDbContext
- IEmbeddingService
- IVectorStoreService
- ITextChunkingService
- IChatCompletionService

---

## 02.Infras

این لایه شامل پیاده‌سازی جزئیات فنی است.

---

### Infras.SqlServer

مسئول persistence رابطه‌ای:

- ApplicationDbContext
- EF Core configurations
- اتصال به SQL Server

---

### Infras.AI

مسئول integration با AI و vector store:

- OpenAI Embedding
- OpenAI Chat
- Qdrant Vector Store

---

### ساختار داخلی AI

Shared/AI
Shared/VectorStore
Shared/DependencyInjection

---

## 03.Endpoints

این لایه API سیستم است.

---

### Endpoints.Api

شامل:

- تعریف endpointها
- request/response
- اتصال به Application

---

### ساختار داخلی Endpoints

Documents/
Search/
Chat/

---

### Documents

- POST /documents
- GET /documents
- GET /documents/{id}

---

### Search

- POST /search/semantic

---

### Chat

- POST /chat/ask

---

# ساختار docs

docs
 ├ adr
 ├ architecture
 ├ rag
 └ development

---

# نکات مهم طراحی

## 1. Feature-based داخل لایه‌ها

کدها بر اساس feature دسته‌بندی شده‌اند، نه بر اساس تکنولوژی.

---

## 2. جداسازی Infras

- SqlServer → فقط دیتابیس
- AI → فقط OpenAI و Qdrant

---

## 3. Application به Infrastructure وابسته نیست

Application فقط abstractionها را می‌شناسد.

---

## 4. Endpoints فقط orchestration است

Endpointها:
- request را می‌گیرند
- سرویس را صدا می‌زنند
- response می‌دهند

---

# جمع‌بندی

این ساختار کمک می‌کند:

- پروژه قابل فهم باشد
- توسعه featureهای جدید ساده باشد
- وابستگی‌ها کنترل شده باشند
- پروژه برای آموزش مناسب باشد