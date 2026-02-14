# Task: Company–Employee–Project (ABP)

ABP + Angular + SQL Server. Manage **Companies**, **Employees**, and **Projects** with CRUD and permissions.

---

## Login

| Field    | Value    |
|----------|----------|
| Username | `admin`  |
| Password | `1q2w3E*` |

---

## How to Run

### Option A – Aspire (one command)

```powershell
cd aspnet-core
dotnet run --project src/CompanyEmployeeProject.DbMigrator   # once
dotnet run --project src/CompanyEmployeeProject.AppHost
```

Then open the app from the **Aspire dashboard** (API + Angular start together).

### Option B – Backend and frontend separately

**Backend**

```powershell
cd aspnet-core
dotnet run --project src/CompanyEmployeeProject.HttpApi.Host
```

**Frontend** (new terminal)

```powershell
cd angular
npm install --legacy-peer-deps
npm start
```

Browser: `http://localhost:4242`

---

## What’s Included

- **Backend:** Companies, Employees, Projects (CRUD, permissions, localization).
- **Frontend:** List/Create/Edit for each; permission guards and menu; Arabic + RTL.
- **Run:** Aspire (AppHost) or run API + Angular separately.

For full setup and code details, see **docs/IMPLEMENTATION_GUIDE.md**.
