# scripts — SQL por fase (cuadro de turnos, work item 42284)

Scripts SQL de soporte para el DBA, organizados por fase. Son **complementarios** al proyecto SSDT `Vie_ERP` (que ya declara los objetos y los aplica vía DACPAC); sirven para aplicación manual/controlada (p. ej. crear índices `ONLINE` antes del publish para evitar bloqueos en tablas grandes). El diseño completo, la bitácora y las métricas de esta iniciativa viven en el repositorio `ERP_DB_Schema` original (`Traza\chore-modularize-shift-board\`) — este README solo documenta el runbook de aplicación.

## Fase 1 — Performance de carga

- **[`fase-1-indices-cuadro-turnos.sql`](fase-1-indices-cuadro-turnos.sql)** — crea los 2 índices de soporte de la lectura del cuadro de turnos:
  - `IX_Schedule__FunctionalUnitId_Period` — filtro `(FunctionalUnitId, Period)` de `GetSchedule`.
  - `IX_ScheduleDetail_EmployeeId_DateDetail` — consulta batch `(EmployeeId, DateDetail)` que reemplaza el N+1.
  - Idempotente, `ONLINE = ON`, definiciones **idénticas** a las del SSDT → sin drift en el DACPAC.

## Fase 2 — Performance de carga (continuación)

- **[`fase-2-indices-cuadro-turnos.sql`](fase-2-indices-cuadro-turnos.sql)** — extiende `IX_ScheduleDetailConcept__ScheduleDetailHourId` a **covering** con `INCLUDE (ConceptType, ConceptId)`, y `iFunctionalUnitId_Payroll_Contract_5A9740F2` a `(FunctionalUnitId, Valid) INCLUDE (EmployeeId, PositionId)` (rehabilitado — en este repo el índice de `Contract` existía deshabilitado). Requiere `DROP_EXISTING`, idempotente, `ONLINE`. El script comprueba primero si el índice base existe en absoluto en el ambiente antes de decidir `CREATE` normal o `DROP_EXISTING` (drift detectado entre ambientes QA durante el desarrollo original).

## Fase 3 — Normalización del modelo `Schedule` (`D01..D31` → 1-a-N)

Runbook para aplicar la Fase 3 completa (3.1 datos + 3.3 reporte). Correr **en este orden**:

1. **[`fase-3-1-alter-scheduleid.sql`](fase-3-1-alter-scheduleid.sql)** — agrega `ScheduleDetail.ScheduleId` + FK + índice. Prerequisito de todo lo demás. *(Aditivo, idempotente. Equivalente al DDL del SSDT `ScheduleDetail.sql`.)*
2. **[`fase-3-1-backfill-scheduleid.sql`](fase-3-1-backfill-scheduleid.sql)** — puebla `ScheduleId` desde `D01..D31`. Idempotente, con harness de prueba: correr **seleccionando todo el script** con `@Aplicar = 0` (default) → revierte y muestra V1..V6; si todas pasan, poner `@Aplicar = 1` (una sola línea cerca del inicio) y re-ejecutar → `COMMIT`. Deriva sin Ids hardcodeados los tríos de cabecera duplicada y clona los detalles compartidos cross-UF. **No** requiere comentar/descomentar `ROLLBACK`/`COMMIT`.
3. **[`fase-3-1-verificacion.sql`](fase-3-1-verificacion.sql)** — verificación post-backfill (read-only). Debe dar **V1=0, V2=0, V3=0** (V4/V5/V6 informativos).
4. **[`fase-3-3-crear-vista.sql`](fase-3-3-crear-vista.sql)** — crea `Payroll.ViewReportSchedule` (pivote para el reporte XPO). `CREATE OR ALTER`, idempotente. Requiere pasos 1-2. *(Equivalente al SSDT `Views/ViewReportSchedule.sql`.)*

Opcional antes del paso 2: **[`fase-3-1-investigacion-anomalias.sql`](fase-3-1-investigacion-anomalias.sql)** (read-only) — muestra las anomalías (tríos de cabecera duplicada + detalles compartidos cross-UF), si existieran en este ambiente.

> **Verificación funcional** tras el runbook: abrir el Cuadro de Turnos (form) y el reporte impreso en QA y comparar contra el estado previo (letra/horas por día, totales). El código que lee desde el modelo normalizado **necesita este runbook aplicado** para que la matriz llegue poblada — sin él, la pantalla y el reporte muestran la matriz vacía sin ningún error.

### Nota — pasos 1 y 4 también viven en el SSDT

`ScheduleDetail.ScheduleId` (paso 1) y `ViewReportSchedule` (paso 4) están declarados en `Vie_ERP` y se aplican vía **DACPAC publish**. Estos scripts sueltos son para aplicación **manual en QA** sin publicar todo el DACPAC; sus definiciones son **idénticas** al SSDT → sin drift.

## Fases siguientes (aún sin SQL)

- **Contract final (cierre Fase 3)** — reescritura del *save* del servidor para consumir `Details`, **drop** de las 31 columnas + 31 FK de `Schedule`, `ScheduleId` → `NOT NULL`, colapso del boilerplate de las entidades. Iniciativa futura, fuera de este runbook.

## Convención

- Nombre: `fase-<n>-<tema-corto-kebab>.sql`.
- Todo script debe ser **idempotente** (`IF NOT EXISTS ...`) y no generar drift con el SSDT.
- Índices sobre tablas grandes: crear `ONLINE` (Azure SQL / Enterprise) en ventana coordinada con el DBA.
