/* =============================================================================
   Fase 3.1 - Investigacion de anomalias de ScheduleDetail compartidos
   (rama chore-modularize-shift-board)

   Contexto: Payroll.Schedule tiene una fila por (EmployeeId, Period,
   FunctionalUnitId) con 31 columnas D01..D31 (INT NULL), cada una FK a
   Payroll.ScheduleDetail(Id) -> un dia del mes = una columna. La normalizacion
   Fase 3.1 agrega ScheduleDetail.ScheduleId (relacion 1-a-N real). Antes del
   backfill hay que caracterizar los detalles "compartidos": ids de
   ScheduleDetail referenciados por MAS de una fila de Schedule.

   Hallazgo previo (QA INDIGO751): 752 ScheduleDetail son referenciados por
   exactamente 2 Schedule (todos mismo Employee + mismo Period). De esos:
     - 41  con UF unica (DistinctFUs = 1) = cabeceras Schedule DUPLICADAS.
     - 711 con dos UF (DistinctFUs = 2)   = mismo empleado/periodo en dos UF
                                            que comparten el mismo detalle.
   El backfill (A3) trata cada grupo distinto: los 41 se resuelven asignando el
   detalle al Schedule de menor Id; los 711 se clonan (una copia por Schedule).

   Proposito de este script:
     1. Materializar (Schedule, dia) -> ScheduleDetail UNA sola vez en #Refs
        (el UNPIVOT sobre toda la tabla sin tabla temporal se degrada; se
        materializa + indice clustered por SDId y se reusa 3 veces).
     2. Construir #Shared = SDId con RefCount y DistinctFUs (referenciados >1).
     3. Listar el grupo 41 (cabeceras duplicadas), nominal.
     4. Listar las cabeceras duplicadas a nivel trio (incidencia de limpieza).
     5. Muestra (TOP 40) del grupo 711 cross-UF con datos del detalle.

   Alcance: SOLO LECTURA (SELECT + tablas temporales). No muta nada.

   Ejecucion: correr COMPLETO en UNA sola sesion (SSMS/ADS) contra QA
   INDIGO751. Usa tablas temporales #Refs/#Shared, por lo que NO sirve
   ejecutarlo sentencia-por-sentencia (las #temp no sobreviven entre
   conexiones). Ambiente sugerido: QA (INDIGO751); no requiere produccion.

   Nota (verificado 2026-07-21, QA INDIGO751): el split reproduce 41 / 711
   (total 752). La lista a nivel trio del paso 4 NO es de 41 filas: son 2 trios
   duplicados (empleados 13652 y 15674, periodo 12/2025), y esos 2 trios generan
   los 41 detalles compartidos del grupo con UF unica. "41" cuenta detalles
   compartidos, no cabeceras.
   ============================================================================= */

SET NOCOUNT ON;

/* --- Paso 1: materializar todas las referencias (Schedule, dia) -> SDId ------
   Una sola pasada de UNPIVOT sobre Payroll.Schedule, persistida en #Refs con
   indice clustered por SDId para los joins/agrupaciones posteriores. --------- */
IF OBJECT_ID('tempdb..#Refs') IS NOT NULL DROP TABLE #Refs;

SELECT ScheduleId, EmployeeId, Period, FunctionalUnitId, DayCol, SDId
INTO #Refs
FROM (
    SELECT Id AS ScheduleId, EmployeeId, Period, FunctionalUnitId,
        D01,D02,D03,D04,D05,D06,D07,D08,D09,D10,D11,D12,D13,D14,D15,D16,
        D17,D18,D19,D20,D21,D22,D23,D24,D25,D26,D27,D28,D29,D30,D31
    FROM Payroll.Schedule
) src
UNPIVOT (SDId FOR DayCol IN (
    D01,D02,D03,D04,D05,D06,D07,D08,D09,D10,D11,D12,D13,D14,D15,D16,
    D17,D18,D19,D20,D21,D22,D23,D24,D25,D26,D27,D28,D29,D30,D31
)) u;

CREATE CLUSTERED INDEX IX_Refs_SDId ON #Refs (SDId);

/* --- Paso 2: conjunto de detalles compartidos (referenciados por >1 Schedule) */
IF OBJECT_ID('tempdb..#Shared') IS NOT NULL DROP TABLE #Shared;

SELECT SDId,
       COUNT(*)                        AS RefCount,
       COUNT(DISTINCT FunctionalUnitId) AS DistinctFUs
INTO #Shared
FROM #Refs
GROUP BY SDId
HAVING COUNT(*) > 1;

/* --- Resumen: split por DistinctFUs (esperado: 1->41, 2->711, total 752) ---- */
PRINT '=== Resumen: SDId compartidos por numero de UF distintas ===';
SELECT DistinctFUs,
       COUNT(*)          AS SharedSDIds,
       SUM(RefCount)     AS TotalRefs
FROM #Shared
GROUP BY DistinctFUs
ORDER BY DistinctFUs;

SELECT COUNT(*) AS TotalSharedSDIds FROM #Shared;

/* --- Paso 3: grupo 41 (cabeceras duplicadas, DistinctFUs = 1) ---------------
   Listado nominal (SDId x Schedule) para construir la asignacion-a-menor-Id. */
PRINT '=== Grupo 41: detalles de cabeceras Schedule duplicadas (misma UF) ===';
SELECT r.SDId, r.ScheduleId, r.EmployeeId, r.Period, r.FunctionalUnitId, r.DayCol
FROM #Refs r
JOIN #Shared s ON s.SDId = r.SDId AND s.DistinctFUs = 1
ORDER BY r.EmployeeId, r.Period, r.FunctionalUnitId, r.SDId, r.ScheduleId;

/* --- Paso 4: cabeceras duplicadas a nivel trio (incidencia de limpieza) -----
   Cada trio con >1 Schedule es una cabecera duplicada a resolver. Este es el
   listado de incidencia (NO cuenta 41; ver nota del encabezado). -------------- */
PRINT '=== Cabeceras duplicadas por trio (EmployeeId, Period, FunctionalUnitId) ===';
SELECT EmployeeId, Period, FunctionalUnitId,
       COUNT(*) AS Cuadros,
       MIN(Id)  AS MenorId,
       MAX(Id)  AS MayorId
FROM Payroll.Schedule
GROUP BY EmployeeId, Period, FunctionalUnitId
HAVING COUNT(*) > 1
ORDER BY EmployeeId, Period;

/* --- Paso 5: muestra del grupo 711 cross-UF (DistinctFUs > 1) ---------------
   Une al detalle para ver DateDetail/UF del detalle/letra y confirmar que son
   mismo empleado+periodo en dos UF distintas (se clonaran en el backfill). ---- */
PRINT '=== Grupo 711: muestra (TOP 40) cross-UF con datos del detalle ===';
SELECT TOP 40
       r.SDId, r.ScheduleId, r.EmployeeId, r.Period, r.FunctionalUnitId, r.DayCol,
       sd.DateDetail,
       sd.FunctionalUnitId         AS DetailFU,
       sd.ScheduleFunctionalUnitId AS DetailSchedFU,
       sd.Letter
FROM #Refs r
JOIN #Shared s            ON s.SDId = r.SDId AND s.DistinctFUs > 1
JOIN Payroll.ScheduleDetail sd ON sd.Id = r.SDId
ORDER BY r.SDId, r.ScheduleId;

/* --- Limpieza --------------------------------------------------------------- */
IF OBJECT_ID('tempdb..#Refs')   IS NOT NULL DROP TABLE #Refs;
IF OBJECT_ID('tempdb..#Shared') IS NOT NULL DROP TABLE #Shared;
