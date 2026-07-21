# BlueWashAPI

**BlueWashAPI** is a C# (.NET) RESTful Web API designed for end-to-end business operations, covering user and role management, financial accounting, inventory, purchasing, sales, and supplier integration.

---

##  Features

- **User & Role**: Full user and role management, including role assignment.
- **Chart of Accounts & Currency**: Supports financial accounts and multi-currency setups.
- **Customer & Supplier**: Manage client and supplier data relations.
- **Inventory**: Inventory tracking to support purchase and sales modules.
- **Purchasing**: Purchase Header & Detail; Record orders and item-level details.
- **Sales**:
  - Sales Header & Detail; Process and detail transactions.
  - Sales Payment; Manage payment records tied to sales.
- **Financial Ledger**: General Account Header & Detail; structured journal entries for audit and reconciliation.
- **Extensibility**: Scaffolding for future enhancements with modular structure and interception facilities.
- **Containerized Deployment**: Supports Docker and local development configuration.

---

##  Tech Stack & Architecture

- **Programming Language**: C# (.NET)
- **ORM & Migrations**: Entity Framework Core with support for database migrations.
- **Configuration**: `appsettings.json` for multiple environments (`Development`, `Docker`).
- **Project Structure**:
  - `Authentication`, `Controllers`, `DAL`, `DTO`, `Models`, `Data`
  - Supportive modules: `Exception`, `Interceptors`, `Utility`, `OptionsSetup`, `Migrations`
- **Deployment**:
  - Dockerfile
  - `docker-compose.yaml` for multi-container orchestration
