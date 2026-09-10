/* =============================================================================
   Fase 3.1 - Backfill de Payroll.ScheduleDetail.ScheduleId
   (rama chore-modularize-shift-board)

   Puebla la nueva columna ScheduleDetail.ScheduleId (FK 1-a-N a Schedule.Id,
   agregada en Task A2, nullable, hoy toda NULL) a partir de la relacion ANCHA
   existente: cada Payroll.Schedule tiene 31 columnas D01..D31 (INT NULL), cada
   una FK a un ScheduleDetail. Un dia del mes = una columna. Este script invierte
   esa relacion hacia la FK normalizada 1-a-N.

   REGLA DIFERENCIADA (decidida en diseno, ver
   fase-3-1-normalizacion-schedule-diseno.md) segun el tipo de detalle:

     Bloque 1 - CASO SIMPLE (~4,09 M detalles referenciados por UN solo Schedule):
       ScheduleDetail.ScheduleId = Id del Schedule que lo referencia.

     Bloque 2 - 711 COMPARTIDOS CROSS-UF (referenciados por 2 Schedule de UF
       DISTINTA, mismo empleado/periodo): un detalle solo puede tener un
       ScheduleId. Se CLONA: el original queda con el Schedule de menor Id
       (determinista); para el segundo Schedule se inserta una COPIA (Id nuevo por
       IDENTITY), con ScheduleId = segundo Schedule, y se REAPUNTA la columna Dxx
       de ese segundo Schedule al clon. Resultado: cada cuadro de cada UF se queda
       con su propia fila; ningun dia se pierde. INSERTA ~711 filas y ACTUALIZA
       ~711 slots Dxx: es intencional y necesario.

     Bloque 3 - COMPARTIDOS MISMA-UF, generados por cabeceras DUPLICADAS (dos o
       mas filas Schedule para el mismo (EmployeeId, Period, FunctionalUnitId)
       apuntando a los mismos detalles). Los trios se DERIVAN dinamicamente
       (GROUP BY (EmployeeId,Period,FunctionalUnitId) HAVING COUNT(*)>1), sin Ids
       fijos, para que el script corra igual en QA y en PRODUCCION. En QA
       INDIGO751 salen 2 trios (41 detalles); en otro ambiente saldran los que
       haya. Clonar NO arregla la raiz (quedarian gemelos); el dedup de cabecera
       esta FUERA de alcance de 3.1. Se asigna cada detalle compartido a la
       cabecera de MENOR Id de su trio y se LISTA la incidencia (los trios
       derivados) para decision posterior. NO se borra ni se fusiona ninguna
       cabecera.

   ALCANCE / MUTACIONES: UPDATE ScheduleDetail.ScheduleId (bloques 1,2,3),
   INSERT de ~711 clones (bloque 2), UPDATE de ~711 columnas Dxx en Schedule
   (bloque 2, reapuntar al clon). NO borra nada. NO toca cabeceras Schedule salvo
   reapuntar los Dxx de los 711 cross-UF.

   ScheduleId QUEDA NULLABLE en 3.1: hay ~120.914 detalles huerfanos (no colgados
   de ningun Schedule) que quedan NULL a proposito. Volverla NOT NULL se difiere
   al paso "contract" final.

   IDEMPOTENCIA (seguro re-ejecutar):
     - Bloque 1: solo toca filas con ScheduleId IS NULL, excluye los 752 compartidos.
     - Bloque 2: el conjunto compartido se recalcula desde el estado VIVO de
       Schedule; tras un COMMIT previo el original queda con RefCount=1 (ya no
       compartido) y no reingresa. Ademas, guarda explicita: no clona si la
       columna Dxx del segundo Schedule ya apunta a un detalle cuyo ScheduleId es
       ese mismo segundo Schedule (= ya es el clon). El reapuntado de cada Dxx
       solo actua si la columna aun apunta al ORIGINAL.
     - Bloque 3: solo toca filas con ScheduleId IS NULL.

   RENDIMIENTO: el UNPIVOT de Schedule (196K x 31) es pesado; un doble-UNPIVOT en
   CTE se degrada (timeout 60s en pruebas ad-hoc). Se materializa UNA vez en #Refs
   (indice clustered por SDId) y se maneja TODO desde ahi, igual que el script de
   investigacion (fase-3-1-investigacion-anomalias.sql).

   HARNESS DE PRUEBA (patron del repo): todo va en una transaccion con
   SET XACT_ABORT ON. Por defecto REVIERTE (ROLLBACK) para probar y leer las
   validaciones V1..V6 sin escribir. Para APLICAR: cambiar la variable
   @Aplicar de 0 a 1 (una sola linea, cerca del inicio) y re-ejecutar. No hay que
   comentar/descomentar ROLLBACK/COMMIT (evita el error "COMMIT sin BEGIN").

   EJECUCION: correr COMPLETO en UNA sola sesion (SSMS/ADS) contra la DB objetivo,
   SELECCIONANDO TODO el script (no ejecutar solo una parte: BEGIN TRAN y el
   COMMIT deben ir en la misma ejecucion).
   Usa tablas temporales #Refs/#Shared/#CloneWork/#CloneMap/#Repoint/#RefsPost,
   por lo que NO sirve sentencia-por-sentencia (las #temp no sobreviven entre
   conexiones). Lo corre el DBA (MCP es read-only). Ambiente sugerido de prueba:
   QA (INDIGO751). En produccion aplica doble confirmacion antes del COMMIT.

   REQUISITO PREVIO: Task A2 aplicada (columna ScheduleDetail.ScheduleId existe).
   El script aborta con mensaje claro si la columna no existe.
   ============================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;

/* --- Precondicion: la columna ScheduleId debe existir (Task A2) -------------- */
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID(N'[Payroll].[ScheduleDetail]')
      AND name = N'ScheduleId'
)
BEGIN
    RAISERROR('Falta [Payroll].[ScheduleDetail].[ScheduleId]. Aplicar Task A2 (DDL) antes del backfill.', 16, 1);
    RETURN;
END

DECLARE @Fail BIT = 0;

/* INTERRUPTOR: 0 = PRUEBA (revierte al final para leer V1..V6). 1 = APLICAR (COMMIT).
   Cambiar SOLO este valor a 1 para persistir. No hace falta comentar/descomentar nada mas. */
DECLARE @Aplicar BIT = 0;

BEGIN TRAN;

/* =============================================================================
   Paso 0 - Materializar referencias (Schedule, dia) -> ScheduleDetail UNA vez
   ============================================================================= */
IF OBJECT_ID('tempdb..#Refs')   IS NOT NULL DROP TABLE #Refs;
IF OBJECT_ID('tempdb..#Shared') IS NOT NULL DROP TABLE #Shared;

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

/* Detalles compartidos (referenciados por >1 Schedule) + su tipo (DistinctFUs).
   DistinctFUs = 1 -> mismo-UF (cabeceras duplicadas, 41).
   DistinctFUs = 2 -> cross-UF (clonar, 711).                                    */
SELECT SDId,
       COUNT(*)                         AS RefCount,
       COUNT(DISTINCT FunctionalUnitId) AS DistinctFUs
INTO #Shared
FROM #Refs
GROUP BY SDId
HAVING COUNT(*) > 1;

CREATE CLUSTERED INDEX IX_Shared_SDId ON #Shared (SDId);

DECLARE @RefsCount INT = (SELECT COUNT(*) FROM #Refs);
DECLARE @SharedCount INT = (SELECT COUNT(*) FROM #Shared);
PRINT '=== Paso 0: referencias materializadas ===';
PRINT '  Refs totales (Dxx no-null): ' + CAST(@RefsCount AS VARCHAR(20));
PRINT '  Detalles compartidos (>1 Schedule): ' + CAST(@SharedCount AS VARCHAR(20))
    + '  (esperado 752: 41 mismo-UF + 711 cross-UF)';

/* =============================================================================
   Bloque 1 - CASO SIMPLE: detalle referenciado por 1 solo Schedule.
   Idempotente: solo filas con ScheduleId NULL; excluye los 752 compartidos.
   ============================================================================= */
UPDATE sd
SET sd.ScheduleId = r.ScheduleId
FROM Payroll.ScheduleDetail sd
JOIN #Refs r        ON r.SDId = sd.Id
LEFT JOIN #Shared s ON s.SDId = sd.Id
WHERE sd.ScheduleId IS NULL
  AND s.SDId IS NULL;

PRINT '=== Bloque 1 (simple): detalles asignados en esta ejecucion: '
    + CAST(@@ROWCOUNT AS VARCHAR(20)) + ' ===';

/* =============================================================================
   Bloque 2 - 711 COMPARTIDOS CROSS-UF: clonar para el segundo Schedule.

   Driver #CloneWork (una fila por detalle cross-UF):
     - Ordena las 2 referencias por ScheduleId: RN=1 (primero, conserva original),
       RN=2 (segundo, se clona).
     - Guarda explicita de idempotencia: excluye filas cuyo Dxx del segundo
       Schedule ya apunta a un detalle cuyo ScheduleId = ese segundo Schedule
       (= ya se clono en una corrida previa).
   ============================================================================= */
IF OBJECT_ID('tempdb..#CloneWork') IS NOT NULL DROP TABLE #CloneWork;
IF OBJECT_ID('tempdb..#CloneMap')  IS NOT NULL DROP TABLE #CloneMap;
IF OBJECT_ID('tempdb..#Repoint')   IS NOT NULL DROP TABLE #Repoint;

;WITH Ranked AS (
    SELECT r.SDId, r.ScheduleId, r.DayCol,
           ROW_NUMBER() OVER (PARTITION BY r.SDId ORDER BY r.ScheduleId) AS RN
    FROM #Refs r
    JOIN #Shared s ON s.SDId = r.SDId AND s.DistinctFUs = 2
)
SELECT
    r2.SDId       AS OriginalSDId,
    r1.ScheduleId AS FirstScheduleId,
    r2.ScheduleId AS SecondScheduleId,
    r2.DayCol     AS SecondDayCol,
    sd.DateDetail AS OriginalDateDetail
INTO #CloneWork
FROM Ranked r1
JOIN Ranked r2                 ON r2.SDId = r1.SDId AND r1.RN = 1 AND r2.RN = 2
JOIN Payroll.ScheduleDetail sd ON sd.Id  = r2.SDId
WHERE NOT EXISTS (   -- idempotencia: el 2do Schedule ya apunta a un detalle propio (el clon)
    SELECT 1 FROM Payroll.ScheduleDetail d
    WHERE d.Id = r2.SDId AND d.ScheduleId = r2.ScheduleId
);

CREATE CLUSTERED INDEX IX_CloneWork ON #CloneWork (OriginalSDId);

DECLARE @CloneCount INT = (SELECT COUNT(*) FROM #CloneWork);
PRINT '=== Bloque 2 (cross-UF): detalles a clonar en esta ejecucion: '
    + CAST(@CloneCount AS VARCHAR(20))
    + '  (esperado 711 en corrida limpia; 0 si ya se aplico) ===';

/* Clonar: copia de todas las columnas EXCEPTO Id (IDENTITY asigna una nueva),
   con ScheduleId = segundo Schedule. OUTPUT captura (CloneId, SecondScheduleId,
   CloneDateDetail) para reconstruir el mapeo del reapuntado.                    */
CREATE TABLE #CloneMap (
    CloneId          INT  NOT NULL,
    SecondScheduleId INT  NOT NULL,
    CloneDateDetail  DATE NOT NULL
);

INSERT INTO Payroll.ScheduleDetail
    (GroupId, EmployeeId, ContractId, CompanyId, BranchOfficeId, FunctionalUnitId,
     CenterCostId, ScheduleFunctionalUnitId, PayrollLiquidationNumber, Letter,
     DateDetail, TotalNumberHours, ScheduleTemplateId, Status, State, ScheduleId)
OUTPUT inserted.Id, inserted.ScheduleId, inserted.DateDetail
    INTO #CloneMap (CloneId, SecondScheduleId, CloneDateDetail)
SELECT
    sd.GroupId, sd.EmployeeId, sd.ContractId, sd.CompanyId, sd.BranchOfficeId, sd.FunctionalUnitId,
    sd.CenterCostId, sd.ScheduleFunctionalUnitId, sd.PayrollLiquidationNumber, sd.Letter,
    sd.DateDetail, sd.TotalNumberHours, sd.ScheduleTemplateId, sd.Status, sd.State,
    cw.SecondScheduleId
FROM #CloneWork cw
JOIN Payroll.ScheduleDetail sd ON sd.Id = cw.OriginalSDId;

DECLARE @Cloned INT = @@ROWCOUNT;
PRINT '  Clones insertados: ' + CAST(@Cloned AS VARCHAR(20));

/* Conservar el original en el primer Schedule (menor Id). Idempotente: ScheduleId NULL. */
UPDATE sd
SET sd.ScheduleId = cw.FirstScheduleId
FROM Payroll.ScheduleDetail sd
JOIN #CloneWork cw ON cw.OriginalSDId = sd.Id
WHERE sd.ScheduleId IS NULL;
PRINT '  Originales cross-UF asignados al primer Schedule: ' + CAST(@@ROWCOUNT AS VARCHAR(20));

/* Mapa de reapuntado: enlaza clon <-> driver por (SecondScheduleId, DateDetail).
   Clave UNICA verificada en QA (ningun 2do Schedule referencia 2 detalles
   cross-UF en la misma fecha). Trae la columna Dxx EXACTA a reapuntar.          */
SELECT cw.OriginalSDId, cw.SecondScheduleId, cw.SecondDayCol, cm.CloneId
INTO #Repoint
FROM #CloneWork cw
JOIN #CloneMap cm
    ON cm.SecondScheduleId = cw.SecondScheduleId
   AND cm.CloneDateDetail  = cw.OriginalDateDetail;

/* Reapuntar la columna Dxx del segundo Schedule al clon. 31 UPDATE estaticos
   (sin SQL dinamico, cumple el gate de seguridad del repo). Cada UPDATE filtra
   SecondDayCol = 'DNN' -> a lo sumo una fila #Repoint por (Schedule, columna),
   join determinista. Guarda de idempotencia: WHERE s.DNN = OriginalSDId (solo si
   la columna aun apunta al ORIGINAL; si ya es el clon, no-op).                  */
DECLARE @Repointed INT = 0;
UPDATE s SET s.D01 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D01' WHERE s.D01 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D02 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D02' WHERE s.D02 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D03 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D03' WHERE s.D03 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D04 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D04' WHERE s.D04 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D05 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D05' WHERE s.D05 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D06 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D06' WHERE s.D06 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D07 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D07' WHERE s.D07 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D08 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D08' WHERE s.D08 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D09 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D09' WHERE s.D09 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D10 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D10' WHERE s.D10 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D11 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D11' WHERE s.D11 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D12 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D12' WHERE s.D12 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D13 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D13' WHERE s.D13 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D14 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D14' WHERE s.D14 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D15 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D15' WHERE s.D15 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D16 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D16' WHERE s.D16 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D17 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D17' WHERE s.D17 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D18 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D18' WHERE s.D18 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D19 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D19' WHERE s.D19 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D20 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D20' WHERE s.D20 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D21 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D21' WHERE s.D21 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D22 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D22' WHERE s.D22 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D23 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D23' WHERE s.D23 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D24 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D24' WHERE s.D24 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D25 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D25' WHERE s.D25 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D26 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D26' WHERE s.D26 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D27 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D27' WHERE s.D27 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D28 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D28' WHERE s.D28 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D29 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D29' WHERE s.D29 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D30 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D30' WHERE s.D30 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;
UPDATE s SET s.D31 = rp.CloneId FROM Payroll.Schedule s JOIN #Repoint rp ON rp.SecondScheduleId = s.Id AND rp.SecondDayCol = 'D31' WHERE s.D31 = rp.OriginalSDId; SET @Repointed += @@ROWCOUNT;

PRINT '  Columnas Dxx reapuntadas al clon: ' + CAST(@Repointed AS VARCHAR(20))
    + '  (debe igualar los clones insertados)';

/* =============================================================================
   Bloque 3 - COMPARTIDOS MISMA-UF (cabeceras duplicadas). Environment-agnostic:
   los trios se DERIVAN, sin Ids fijos. Asigna cada detalle compartido misma-UF a
   la cabecera de MENOR Id de su trio (EmployeeId, Period, FunctionalUnitId).
   NO clona, NO borra. Idempotente: solo filas con ScheduleId NULL.
   ============================================================================= */

/* Trios de cabecera duplicada, derivados (misma clave de negocio, >1 fila). */
IF OBJECT_ID('tempdb..#DupHeaders') IS NOT NULL DROP TABLE #DupHeaders;
SELECT EmployeeId, Period, FunctionalUnitId, MIN(Id) AS MenorId
INTO #DupHeaders
FROM Payroll.Schedule
GROUP BY EmployeeId, Period, FunctionalUnitId
HAVING COUNT(*) > 1;

/* Asignar los detalles misma-UF (DistinctFUs=1) al MenorId de su trio. Un detalle
   misma-UF tiene todas sus referencias en el mismo (Employee,Period,UF), asi que
   cualquiera de ellas resuelve el trio -> MenorId. DISTINCT colapsa las 2 refs. */
UPDATE sd
SET sd.ScheduleId = a.MenorId
FROM Payroll.ScheduleDetail sd
JOIN (
    SELECT DISTINCT r.SDId, dh.MenorId
    FROM #Refs r
    JOIN #Shared s      ON s.SDId = r.SDId AND s.DistinctFUs = 1
    JOIN #DupHeaders dh ON dh.EmployeeId = r.EmployeeId
                       AND dh.Period = r.Period
                       AND dh.FunctionalUnitId = r.FunctionalUnitId
) a ON a.SDId = sd.Id
WHERE sd.ScheduleId IS NULL;
PRINT '=== Bloque 3 (misma-UF): detalles asignados al MenorId de su trio: '
    + CAST(@@ROWCOUNT AS VARCHAR(20)) + '  (QA INDIGO751: 41) ===';

PRINT '=== Bloque 3 - INCIDENCIA DE LIMPIEZA (decision posterior, fuera de 3.1) ===';
PRINT '  Cabeceras Schedule DUPLICADAS a resolver (derivadas; no se borra nada en 3.1):';
SELECT dh.EmployeeId, dh.Period, dh.FunctionalUnitId,
       COUNT(s.Id) AS Cuadros, dh.MenorId AS MenorId_Asignado, MAX(s.Id) AS MayorId_Gemelo
FROM #DupHeaders dh
JOIN Payroll.Schedule s ON s.EmployeeId = dh.EmployeeId
                       AND s.Period = dh.Period
                       AND s.FunctionalUnitId = dh.FunctionalUnitId
GROUP BY dh.EmployeeId, dh.Period, dh.FunctionalUnitId, dh.MenorId
ORDER BY dh.EmployeeId, dh.Period;

/* =============================================================================
   VALIDACIONES V1..V6  (leer con ROLLBACK activo; TODAS deben pasar antes de COMMIT)
   ============================================================================= */
PRINT '';
PRINT '================= VALIDACIONES =================';

/* Recalcular referencias desde el estado VIVO (post-mutacion): los Dxx cross-UF
   del 2do Schedule ahora apuntan al clon. */
IF OBJECT_ID('tempdb..#RefsPost') IS NOT NULL DROP TABLE #RefsPost;
SELECT ScheduleId, FunctionalUnitId, SDId
INTO #RefsPost
FROM (
    SELECT Id AS ScheduleId, FunctionalUnitId,
        D01,D02,D03,D04,D05,D06,D07,D08,D09,D10,D11,D12,D13,D14,D15,D16,
        D17,D18,D19,D20,D21,D22,D23,D24,D25,D26,D27,D28,D29,D30,D31
    FROM Payroll.Schedule
) src
UNPIVOT (SDId FOR DayCol IN (
    D01,D02,D03,D04,D05,D06,D07,D08,D09,D10,D11,D12,D13,D14,D15,D16,
    D17,D18,D19,D20,D21,D22,D23,D24,D25,D26,D27,D28,D29,D30,D31
)) u;
CREATE CLUSTERED INDEX IX_RefsPost_SDId ON #RefsPost (SDId);

/* V1 - Huerfanos: detalles que quedan con ScheduleId NULL. INFORMATIVO (nullable
   en 3.1): son detalles no referenciados por ningun Schedule. */
DECLARE @V1_Orphans INT = (SELECT COUNT(*) FROM Payroll.ScheduleDetail WHERE ScheduleId IS NULL);
PRINT 'V1 (info) Detalles con ScheduleId NULL (huerfanos, permitido): ' + CAST(@V1_Orphans AS VARCHAR(20))
    + '  (~120.914 esperado en QA INDIGO751; no falla)';

/* V2 - Integridad FK: ningun detalle asignado apunta a un Schedule inexistente. */
DECLARE @V2_BrokenFK INT = (
    SELECT COUNT(*)
    FROM Payroll.ScheduleDetail sd
    WHERE sd.ScheduleId IS NOT NULL
      AND NOT EXISTS (SELECT 1 FROM Payroll.Schedule s WHERE s.Id = sd.ScheduleId)
);
PRINT 'V2 FK rotas (esperado 0): ' + CAST(@V2_BrokenFK AS VARCHAR(20));
IF @V2_BrokenFK <> 0 SET @Fail = 1;

/* V3 - Referencias huerfanas de asignacion: toda referencia viva debe apuntar a
   un detalle con ScheduleId ya poblado (0 detalles referenciados sin asignar). */
DECLARE @V3_UnassignedRefs INT = (
    SELECT COUNT(*)
    FROM #RefsPost rp
    JOIN Payroll.ScheduleDetail sd ON sd.Id = rp.SDId
    WHERE sd.ScheduleId IS NULL
);
PRINT 'V3 Referencias vivas a detalle sin asignar (esperado 0): ' + CAST(@V3_UnassignedRefs AS VARCHAR(20));
IF @V3_UnassignedRefs <> 0 SET @Fail = 1;

/* V4 - Cobertura: referencias donde detalle.ScheduleId <> Schedule que referencia.
   La UNICA discrepancia legitima es un gemelo de cabecera duplicada: una cabecera
   del trio que NO es el MenorId sigue apuntando a un detalle ya asignado al MenorId
   de su trio. Todo se evalua derivado (#TwinHeaders), sin Ids fijos. Cualquier otra
   discrepancia es un error. En QA INDIGO751 son 41 (los 2 trios). */
IF OBJECT_ID('tempdb..#TwinHeaders') IS NOT NULL DROP TABLE #TwinHeaders;
SELECT s.Id AS ScheduleId, dh.MenorId
INTO #TwinHeaders
FROM Payroll.Schedule s
JOIN #DupHeaders dh ON dh.EmployeeId = s.EmployeeId
                   AND dh.Period = s.Period
                   AND dh.FunctionalUnitId = s.FunctionalUnitId
WHERE s.Id <> dh.MenorId;

DECLARE @V4_Mismatch INT = (
    SELECT COUNT(*)
    FROM #RefsPost rp
    JOIN Payroll.ScheduleDetail sd ON sd.Id = rp.SDId
    WHERE sd.ScheduleId IS NOT NULL
      AND sd.ScheduleId <> rp.ScheduleId
);
/* Discrepancias NO explicadas por un gemelo apuntando al detalle de su MenorId. */
DECLARE @V4_Unexpected INT = (
    SELECT COUNT(*)
    FROM #RefsPost rp
    JOIN Payroll.ScheduleDetail sd ON sd.Id = rp.SDId
    LEFT JOIN #TwinHeaders th ON th.ScheduleId = rp.ScheduleId AND th.MenorId = sd.ScheduleId
    WHERE sd.ScheduleId IS NOT NULL
      AND sd.ScheduleId <> rp.ScheduleId
      AND th.ScheduleId IS NULL
);
PRINT 'V4 Refs con detalle asignado a OTRO Schedule (gemelos de cabecera duplicada): '
    + CAST(@V4_Mismatch AS VARCHAR(20)) + '  (QA INDIGO751: 41)';
PRINT 'V4 De esas, discrepancias NO explicadas por un gemelo (esperado 0): '
    + CAST(@V4_Unexpected AS VARCHAR(20));
IF @V4_Unexpected <> 0 SET @Fail = 1;
PRINT '   Discrepancias por Schedule que referencia (deben ser solo cabeceras gemelas):';
SELECT rp.ScheduleId AS ReferenciaEnSchedule, COUNT(*) AS Refs
FROM #RefsPost rp
JOIN Payroll.ScheduleDetail sd ON sd.Id = rp.SDId
WHERE sd.ScheduleId IS NOT NULL AND sd.ScheduleId <> rp.ScheduleId
GROUP BY rp.ScheduleId
ORDER BY rp.ScheduleId;

/* V5 - Ya no queda sharing cross-UF: recomputar compartidos sobre el estado vivo.
   Esperado: cross-UF (DistinctFUs=2) = 0; mismo-UF (DistinctFUs=1) = 41 gemelos
   (intencional, dedup de cabecera diferido). */
DECLARE @V5_CrossUF INT = 0, @V5_SameUF INT = 0;
SELECT @V5_CrossUF = SUM(CASE WHEN DistinctFUs = 2 THEN 1 ELSE 0 END),
       @V5_SameUF  = SUM(CASE WHEN DistinctFUs = 1 THEN 1 ELSE 0 END)
FROM (
    SELECT SDId, COUNT(DISTINCT FunctionalUnitId) AS DistinctFUs
    FROM #RefsPost
    GROUP BY SDId
    HAVING COUNT(*) > 1
) x;
PRINT 'V5 Compartidos cross-UF restantes (esperado 0): ' + CAST(ISNULL(@V5_CrossUF,0) AS VARCHAR(20));
PRINT 'V5 Compartidos mismo-UF restantes (gemelos de cabecera duplicada, se mantienen): '
    + CAST(ISNULL(@V5_SameUF,0) AS VARCHAR(20)) + '  (QA INDIGO751: 41)';
IF ISNULL(@V5_CrossUF,0) <> 0 SET @Fail = 1;

/* V6 - Conteo global de asignados. Esperado (corrida limpia) = detalles distintos
   referenciados + clones. Se imprime para cotejo manual. */
DECLARE @V6_Assigned INT = (SELECT COUNT(*) FROM Payroll.ScheduleDetail WHERE ScheduleId IS NOT NULL);
PRINT 'V6 (info) Detalles con ScheduleId asignado: ' + CAST(@V6_Assigned AS VARCHAR(20))
    + '  (~4.094.454 esperado en QA INDIGO751 = 4.093.743 distintos + 711 clones)';

PRINT '===============================================';

/* =============================================================================
   RESOLUCION DE LA TRANSACCION (segun @Fail y @Aplicar; todas las ramas guardadas)
   - @Fail = 1        -> REVIERTE siempre (falla dura), ignora @Aplicar.
   - @Aplicar = 0     -> PRUEBA: revierte (leer V1..V6 arriba).
   - @Aplicar = 1     -> APLICA: COMMIT.
   Todas las ramas verifican @@TRANCOUNT > 0 -> nunca "COMMIT/ROLLBACK sin BEGIN".
   ============================================================================= */
IF @Fail = 1
BEGIN
    PRINT '*** VALIDACIONES FALLARON (@Fail=1): se REVIERTE. ***';
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
END
ELSE IF @Aplicar = 1
BEGIN
    IF @@TRANCOUNT > 0 COMMIT TRAN;
    PRINT '*** APLICADO (COMMIT): ScheduleId persistido. ***';
END
ELSE
BEGIN
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    PRINT 'PRUEBA (ROLLBACK). Poner @Aplicar = 1 arriba y re-ejecutar para aplicar.';
END

/* --- Limpieza de temporales ------------------------------------------------- */
IF OBJECT_ID('tempdb..#Refs')      IS NOT NULL DROP TABLE #Refs;
IF OBJECT_ID('tempdb..#Shared')    IS NOT NULL DROP TABLE #Shared;
IF OBJECT_ID('tempdb..#CloneWork') IS NOT NULL DROP TABLE #CloneWork;
IF OBJECT_ID('tempdb..#CloneMap')  IS NOT NULL DROP TABLE #CloneMap;
IF OBJECT_ID('tempdb..#Repoint')   IS NOT NULL DROP TABLE #Repoint;
IF OBJECT_ID('tempdb..#RefsPost')   IS NOT NULL DROP TABLE #RefsPost;
IF OBJECT_ID('tempdb..#DupHeaders') IS NOT NULL DROP TABLE #DupHeaders;
IF OBJECT_ID('tempdb..#TwinHeaders') IS NOT NULL DROP TABLE #TwinHeaders;
