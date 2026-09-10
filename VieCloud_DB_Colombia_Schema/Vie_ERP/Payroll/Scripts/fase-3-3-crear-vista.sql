/* =============================================================================
   Fase 3.3 - PASO 4 de 4 - Vista Payroll.ViewReportSchedule
   (rama chore-modularize-shift-board)

   Vista pivote que reconstruye D01..D31 (como Id del ScheduleDetail de cada dia)
   desde el modelo normalizado (ScheduleDetail.ScheduleId + DAY(DateDetail)), mas
   las columnas propias de Schedule que mapea el reporte XPO (PayrollSchedule).
   Permite que el reporte "Cuadro de turno" (rptSchedule) lea la matriz sin
   depender de las 31 columnas fisicas, habilitando su drop en el contract.

   Equivalente ejecutable del objeto declarado en el SSDT
   (Vie_ERP/Payroll/Views/ViewReportSchedule.sql), para aplicar en QA sin publicar
   todo el DACPAC. Cuerpo IDENTICO al SSDT -> sin drift en el publish.

     - Idempotente: CREATE OR ALTER (SQL Server 2016 SP1+ / Azure SQL).
     - Requiere el PASO 1 (columna ScheduleId) y el PASO 2 (backfill) aplicados;
       si ScheduleId no esta poblado, la vista devuelve D01..D31 en NULL.

   ORDEN DE EJECUCION (runbook completo en README.md):
     1) fase-3-1-alter-scheduleid.sql
     2) fase-3-1-backfill-scheduleid.sql
     3) fase-3-1-verificacion.sql
     4) fase-3-3-crear-vista.sql            <-- ESTE
   ============================================================================= */

CREATE OR ALTER VIEW [Payroll].[ViewReportSchedule]
AS
SELECT
    s.Id,
    s.EmployeeId,
    s.Period,
    s.FunctionalUnitId,
    s.TotalHour,
    s.State,
    MAX(CASE WHEN DAY(sd.DateDetail) = 1  THEN sd.Id END) AS D01,
    MAX(CASE WHEN DAY(sd.DateDetail) = 2  THEN sd.Id END) AS D02,
    MAX(CASE WHEN DAY(sd.DateDetail) = 3  THEN sd.Id END) AS D03,
    MAX(CASE WHEN DAY(sd.DateDetail) = 4  THEN sd.Id END) AS D04,
    MAX(CASE WHEN DAY(sd.DateDetail) = 5  THEN sd.Id END) AS D05,
    MAX(CASE WHEN DAY(sd.DateDetail) = 6  THEN sd.Id END) AS D06,
    MAX(CASE WHEN DAY(sd.DateDetail) = 7  THEN sd.Id END) AS D07,
    MAX(CASE WHEN DAY(sd.DateDetail) = 8  THEN sd.Id END) AS D08,
    MAX(CASE WHEN DAY(sd.DateDetail) = 9  THEN sd.Id END) AS D09,
    MAX(CASE WHEN DAY(sd.DateDetail) = 10 THEN sd.Id END) AS D10,
    MAX(CASE WHEN DAY(sd.DateDetail) = 11 THEN sd.Id END) AS D11,
    MAX(CASE WHEN DAY(sd.DateDetail) = 12 THEN sd.Id END) AS D12,
    MAX(CASE WHEN DAY(sd.DateDetail) = 13 THEN sd.Id END) AS D13,
    MAX(CASE WHEN DAY(sd.DateDetail) = 14 THEN sd.Id END) AS D14,
    MAX(CASE WHEN DAY(sd.DateDetail) = 15 THEN sd.Id END) AS D15,
    MAX(CASE WHEN DAY(sd.DateDetail) = 16 THEN sd.Id END) AS D16,
    MAX(CASE WHEN DAY(sd.DateDetail) = 17 THEN sd.Id END) AS D17,
    MAX(CASE WHEN DAY(sd.DateDetail) = 18 THEN sd.Id END) AS D18,
    MAX(CASE WHEN DAY(sd.DateDetail) = 19 THEN sd.Id END) AS D19,
    MAX(CASE WHEN DAY(sd.DateDetail) = 20 THEN sd.Id END) AS D20,
    MAX(CASE WHEN DAY(sd.DateDetail) = 21 THEN sd.Id END) AS D21,
    MAX(CASE WHEN DAY(sd.DateDetail) = 22 THEN sd.Id END) AS D22,
    MAX(CASE WHEN DAY(sd.DateDetail) = 23 THEN sd.Id END) AS D23,
    MAX(CASE WHEN DAY(sd.DateDetail) = 24 THEN sd.Id END) AS D24,
    MAX(CASE WHEN DAY(sd.DateDetail) = 25 THEN sd.Id END) AS D25,
    MAX(CASE WHEN DAY(sd.DateDetail) = 26 THEN sd.Id END) AS D26,
    MAX(CASE WHEN DAY(sd.DateDetail) = 27 THEN sd.Id END) AS D27,
    MAX(CASE WHEN DAY(sd.DateDetail) = 28 THEN sd.Id END) AS D28,
    MAX(CASE WHEN DAY(sd.DateDetail) = 29 THEN sd.Id END) AS D29,
    MAX(CASE WHEN DAY(sd.DateDetail) = 30 THEN sd.Id END) AS D30,
    MAX(CASE WHEN DAY(sd.DateDetail) = 31 THEN sd.Id END) AS D31
FROM [Payroll].[Schedule] AS s
LEFT JOIN [Payroll].[ScheduleDetail] AS sd ON sd.ScheduleId = s.Id
GROUP BY s.Id, s.EmployeeId, s.Period, s.FunctionalUnitId, s.TotalHour, s.State;
GO
