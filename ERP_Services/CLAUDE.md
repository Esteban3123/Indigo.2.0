# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

VB.NET (.NET Framework 4.8) WCF backend of the Indigo Vie healthcare ERP for Colombia. Consumed by the WinForms client in the sibling repo `..\ERP_Presentation`. Code mixes Spanish and English; domain terms (facturación, cartera, glosas, RIPS, DIAN) are Spanish.

## Solutions

| Solution | Scope |
|----------|-------|
| `Indigo.ReferenceArchitecture.sln` | Main (~193 projects): all service, application, domain, infrastructure layers. CI builds this one. |
| `Indigo.ReferenceArchitecture.Database.sln` | Data repositories / models subset (~31 projects). |
| `Indigo.ReferenceArchitecture.ElectronicDocuments.sln` | DIAN electronic invoicing stack (~87 projects), incl. `WindowsServices.ElectronicDocuments` / `WindowsServices.SendElectronicDocuments` Windows-service hosts. |

## Build

```
nuget restore Indigo.ReferenceArchitecture.sln
msbuild Indigo.ReferenceArchitecture.sln /p:Configuration=Release /p:Platform="Any CPU" /m
```

- packages.config-based NuGet. DevExpress XPO 20.1.8, EntityFramework 6.x, Newtonsoft.Json 13.
- CI: `azure-pipelines.yml` (NuGet restore + VSBuild Release, private agent pool).
- Tests: SpecFlow BDD in `Application.Accounting.Test`, `Application.Inventory.Test`, plus `WindowsServices.ElectronicDocuments.Test`. No solution-wide test run; execute per-project via VS test runner.
- After changing `Domain.*` entities or service contracts consumed by the client: rebuild and copy the resulting DLLs to `..\ERP_Presentation\DLL\` manually — there is no automated copy. Stale DLLs cause `MissingMethodException` at client runtime.

## Architecture

Layered request flow, one chain per business module (Billing, Accounting, Inventory, Payroll, Portfolio, ...):

```
DistribuitedServices.<Module>   WCF contracts ([ServiceContract] + JwtMessageServiceBehavior)
  → Application.<Module>        *AdminService business logic
    → Domain.<Module>           entities
    → Infrastructure.Data.*     XPO repositories (XpoBaseService: LoadCollection/LoadView)
```

- WCF hosting: `DistributeService.Deployment\` holds the `.svc` files (Billing.svc, Accounting.svc, ... ~26 services) and web.config for IIS.
- XPO gateway for the WinForms client: `DistributedService.Xpo.Deployment\` exposes `XpoGate.svc` + ~50 `XpoGateEx*.svc` instances (load balancing). Contract `IXpoGate` (SelectData/ModifyData/UpdateSchema) — the client tunnels all its XPO queries through here.
- Data access is XPO only — no raw ADO.NET in this repo. Stored procedures themselves live in the sibling repo `..\VieCloud_DB_Colombia_Schema` (source of truth, DACPAC deploy).
- Connection strings: web.config `CONX_GENESIS` / `CONX_GENESIS_REPORTS`, with `{0}` placeholder for database name injected at runtime (multi-company).
- Auth: JWT via `JwtMessageServiceBehavior`, Azure Key Vault integration.

## Conventions & gotchas

- **Misspelled names are load-bearing** — do not "fix" them: `DistribuitedServices.*` (not Distributed), `Insfrastructure.Data.Xpo.CrystalRepository`, `Autentication`, `AdmissionsSequense`. Renaming breaks references everywhere.
- File encodings mixed (UTF-8 with/without BOM, some legacy Windows-1252). Preserve the existing encoding when editing files with Spanish accents.
- Cross-repo features use the same branch name in every affected repo (`v25.47c`, `feature/*`, `fix/*`). A typical feature touches this repo + ERP_Presentation + VieCloud_DB_Colombia_Schema.
- `..\ERP_Services_Core` is the parallel .NET 8 / C# port of these services (SDK-style, ProjectReference). Changes here often need porting there on a matching branch.
- Azure DevOps remote (`dev.azure.com/IndigoVie`). Pushes intermittently fail with "Connection was reset"; retry — the push may have landed despite the error.
