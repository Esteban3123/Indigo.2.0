# AGENTS.md

This file provides guidance to Codex (Codex.ai/code) when working with code in this repository.

## What this is

Source of truth for the Indigo Vie ERP SQL Server database (Colombia). Classic SSDT database project: `Database Schema.sln` → single project `Vie_Erp\VieCloudColombia.sqlproj` (SQL Azure V12 schema provider). Output is a DACPAC; deployment to environments is **manual** (DBA via sqlpackage/SSMS) — the pipeline only validates and publishes artifacts.

Consumed by the sibling repos `..\ERP_Services` (WCF backend, XPO) and `..\ERP_Presentation` (WinForms client): any stored procedure / view / table change for a feature is made here and deployed separately from the code release.

## Build & validation

```
msbuild "Vie_Erp\VieCloudColombia.sqlproj" /p:Configuration=Release /p:Platform="Any CPU"
```
→ `Vie_Erp\bin\Release\VieCloudColombia.dacpac`.

CI (`azure-pipelines.yml`, triggers on `master` and release branches like `v25.47c`): build DACPAC → deploy to a throwaway test DB (`Vie_ERP_Test_<buildId>`) with sqlpackage to validate schema → publish artifact. See `docs/pipeline-documentation.md`.

## Structure & conventions

- `Vie_Erp\<Domain>\` per business domain (~52: Billing, Portfolio, Common, Payroll, Inventory, Treasury, Glosas, EHR, ...), each with `Tables\`, `Views\`, `Stored Procedures\`, optionally `Functions\` and `Triggers\`, plus `<Domain>.sql` (the CREATE SCHEMA).
  - Note the misspelled `Porfolio` folder exists alongside `Portfolio` — check both when searching.
- **Every new .sql file must be added as a `<Build Include>` entry in `VieCloudColombia.sqlproj`** — files on disk but not in the project are silently excluded from the DACPAC. New folders also need a `<Folder Include>` entry.
- Naming: tables PascalCase; views `View*`; SPs mixed (`SP_*` prefix or verb-first like `CreateInvoice`, `Get*`); functions PascalCase.
- SP style (follow existing): header comment block (`-- Author / Create date / Description`), `SET NOCOUNT ON;`, `BEGIN TRY/CATCH` with RAISERROR. Good reference: `Vie_Erp\Billing\Stored Procedures\SP_AssociateInvoice.sql`.
- Encoding: UTF-8 with BOM, CRLF. Spanish/English mix; legacy column names are Spanish abbreviations (`IPCODPACI`, `CODUSUMOD`).
- No pre/post-deployment scripts or publish profiles in the repo.

## Workflow

- Cross-repo features use the same branch name here as in ERP_Services / ERP_Presentation (`v25.47c`, `feature/*`, `fix/*`).
- Mark `DB Changes: Yes` in the code-repo PR template whenever a change here must be deployed with the feature.
- Azure DevOps remote (`dev.azure.com/IndigoVie`). Pushes intermittently fail with "Connection was reset"; retry — the push may have landed despite the error.
