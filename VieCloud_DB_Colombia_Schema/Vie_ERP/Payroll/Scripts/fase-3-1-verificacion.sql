/* =============================================================================
   Fase 3.1 - Verificacion POST-backfill de Payroll.ScheduleDetail.ScheduleId
   (rama chore-modularize-shift-board)

   Script de SOLO LECTURA que corre el DBA DESPUES de haber COMMITEADO el backfill
   (fase-3-1-backfill-scheduleid.sql). Confirma que la nueva FK normalizada 1-a-N
   ScheduleDetail.ScheduleId quedo consistente con la relacion ANCHA existente
   (Payroll.Schedule.D01..D31, cada columna = un dia = FK a ScheduleDetail).

   NO MUTA NADA: solo SELECT / PRINT y tablas temporales de trabajo. Sin SQL
   dinamico, sin SELECT *, sin transacciones de escritura. Seguro re-ejecutar.

   REALIDADES DEL BACKFILL QUE ESTAS VALIDACIONES CONTEMPLAN
   (ver fase-3-1-normalizacion-schedule-diseno.md y fase-3-1-backfill-scheduleid.sql):

     1. CASO SIMPLE: detalle referenciado por 1 solo Schedule -> ScheduleId = ese
        Schedule.
     2. 711 CLONES CROSS-UF: detalle compartido por 2 Schedule de UF DISTINTA -> el
        original queda para el Schedule de MENOR Id; se inserto una COPIA (Id nuevo
        por IDENTITY) para el segundo Schedule y se reapunto su columna Dxx al clon.
        => la tabla ScheduleDetail tiene ~711 filas MAS que antes, y NO debe quedar
        ningun detalle compartido cross-UF.
     3. TRIOS DE CABECERA DUPLICADA (derivados, environment-agnostic): detalle
        compartido por 2 Schedule del MISMO (EmployeeId, Period, FunctionalUnitId)
        -> cabeceras Schedule DUPLICADAS. El detalle se asigno a la cabecera de
        MENOR Id (MenorId) del trio; la cabecera gemela (Id mayor) SIGUE apuntando
        con sus Dxx a esos mismos detalles, cuyo ScheduleId ahora = MenorId (no la
        gemela). Es una incidencia de limpieza CONOCIDA e INTENCIONALMENTE NO
        RESUELTA en 3.1 -> esas discrepancias son ESPERADAS, no fallan.
     4. HUERFANOS: ~120.914 detalles no colgados de ningun Schedule quedan con
        ScheduleId NULL a proposito (la columna es NULLABLE en 3.1). NO es fallo.

   NUMEROS DE REFERENCIA QA (INDIGO751), verificados 2026-07-21 ANTES del backfill.
   Son ESPECIFICOS DEL AMBIENTE: sirven de cotejo, NUNCA como aserciones duras.
     - Detalles distintos referenciados : 4.093.743
     - Slots Dxx vivos (no-null)        : 4.094.495  (= 4.093.743 + 752 compartidos)
     - Detalles compartidos             : 752  (41 mismo-UF + 711 cross-UF)
     - Total ScheduleDetail (pre)       : 4.214.657
     - Huerfanos (pre)                  : 120.914  (4.214.657 - 4.093.743)
     - Post-backfill esperado:
         ScheduleId asignados = 4.093.743 + 711 clones = 4.094.454
         Huerfanos (NULL)     = 120.914 (sin cambio)
         Total ScheduleDetail = 4.214.657 + 711 = 4.215.368

   EJECUCION: correr COMPLETO en UNA sola sesion (SSMS/ADS) contra la DB objetivo,
   DESPUES del COMMIT del backfill. Usa temporales #Refs/#DupHeaders/#TwinHeaders,
   por lo que NO sirve sentencia-por-sentencia (las #temp no sobreviven entre
   conexiones). Lo corre el DBA (el MCP es read-only).

   REQUISITO PREVIO: Task A2 aplicada (columna ScheduleDetail.ScheduleId existe) y
   backfill COMMITEADO. El script aborta con mensaje claro si la columna no existe.

   RESULTADO ESPERADO GLOBAL:  V1=0, V2=0, V3=0  (V4/V5/V6 informativos).
   ============================================================================= */

SET NOCOUNT ON;

/* --- Precondicion: la columna ScheduleId debe existir (Task A2) -------------- */
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID(N'[Payroll].[ScheduleDetail]')
      AND name = N'ScheduleId'
)
BEGIN
    RAISERROR('Falta [Payroll].[ScheduleDetail].[ScheduleId]. Aplicar Task A2 y el backfill antes de verificar.', 16, 1);
    RETURN;
END

DECLARE @Fail BIT = 0;

/* =============================================================================
   Paso 0 - Materializar el estado VIVO (post-backfill) UNA sola vez.
   El UNPIVOT de Schedule (196K x 31) es pesado; un doble-UNPIVOT en CTE se
   degrada (timeout 60s en pruebas). Se materializa en #Refs con indice clustered
   por SDId, mismo idiom que fase-3-1-backfill / investigacion.
   ============================================================================= */
IF OBJECT_ID('tempdb..#Refs')        IS NOT NULL DROP TABLE #Refs;
IF OBJECT_ID('tempdb..#DupHeaders')  IS NOT NULL DROP TABLE #DupHeaders;
IF OBJECT_ID('tempdb..#TwinHeaders') IS NOT NULL DROP TABLE #TwinHeaders;

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

/* Trios de cabecera duplicada, DERIVADOS (sin Ids fijos): misma clave de negocio
   con mas de una fila Schedule. MenorId = cabecera "canonica" del trio. */
SELECT EmployeeId, Period, FunctionalUnitId, MIN(Id) AS MenorId
INTO #DupHeaders
FROM Payroll.Schedule
GROUP BY EmployeeId, Period, FunctionalUnitId
HAVING COUNT(*) > 1;

/* Cabeceras GEMELAS: filas del trio que NO son el MenorId. Estas siguen apuntando
   (via sus Dxx) a detalles cuyo ScheduleId = MenorId -> discrepancia LEGITIMA. */
SELECT s.Id AS ScheduleId, dh.MenorId
INTO #TwinHeaders
FROM Payroll.Schedule s
JOIN #DupHeaders dh ON dh.EmployeeId       = s.EmployeeId
                   AND dh.Period           = s.Period
                   AND dh.FunctionalUnitId = s.FunctionalUnitId
WHERE s.Id <> dh.MenorId;

DECLARE @cRefs INT = (SELECT COUNT(*) FROM #Refs);
DECLARE @cDup  INT = (SELECT COUNT(*) FROM #DupHeaders);
DECLARE @cTwin INT = (SELECT COUNT(*) FROM #TwinHeaders);
PRINT '=== Paso 0: estado vivo materializado ===';
PRINT '  Refs vivas (Dxx no-null): ' + CAST(@cRefs AS VARCHAR(20));
PRINT '  Trios de cabecera duplicada (derivados): ' + CAST(@cDup AS VARCHAR(20))
    + '  (referencia QA 751: 2 trios)';
PRINT '  Cabeceras gemelas (Id <> MenorId): ' + CAST(@cTwin AS VARCHAR(20));
PRINT '';
PRINT '================= VALIDACIONES =================';

/* =============================================================================
   V1 - COBERTURA (check nuclear de correctitud).
   Toda referencia viva (Schedule.Dxx) debe resolver a un ScheduleDetail cuyo
   ScheduleId ESTA poblado y apunta al MISMO Schedule que la referencia. La UNICA
   discrepancia legitima: una cabecera GEMELA de trio duplicado apuntando a un
   detalle ya asignado al MenorId de su trio. Cuenta las referencias donde
   detalle.ScheduleId != Schedule que referencia (o esta NULL) Y esa discrepancia
   NO se explica por un gemelo -> Esperado 0.
   ============================================================================= */
DECLARE @V1_Mismatch INT = (
    SELECT COUNT(*)
    FROM #Refs rp
    JOIN Payroll.ScheduleDetail sd ON sd.Id = rp.SDId
    WHERE sd.ScheduleId IS NULL
       OR sd.ScheduleId <> rp.ScheduleId
);
DECLARE @V1_Unexpected INT = (
    SELECT COUNT(*)
    FROM #Refs rp
    JOIN Payroll.ScheduleDetail sd ON sd.Id = rp.SDId
    LEFT JOIN #TwinHeaders th ON th.ScheduleId = rp.ScheduleId AND th.MenorId = sd.ScheduleId
    WHERE (sd.ScheduleId IS NULL OR sd.ScheduleId <> rp.ScheduleId)
      AND th.ScheduleId IS NULL
);
PRINT 'V1 Refs con detalle NO alineado al Schedule que referencia (incluye gemelos): '
    + CAST(@V1_Mismatch AS VARCHAR(20)) + '  (referencia QA 751: 41 gemelos)';
PRINT 'V1 De esas, discrepancias NO explicadas por cabecera duplicada (ESPERADO 0): '
    + CAST(@V1_Unexpected AS VARCHAR(20));
IF @V1_Unexpected <> 0 SET @Fail = 1;

/* Desglose por Schedule que referencia: TODOS deben ser cabeceras gemelas. Si
   aparece un Schedule que NO esta en #TwinHeaders, ahi esta la anomalia. */
PRINT '   Desglose de discrepancias por Schedule (deben ser solo cabeceras gemelas):';
SELECT rp.ScheduleId          AS ReferenciaEnSchedule,
       COUNT(*)               AS Refs,
       CASE WHEN th.ScheduleId IS NULL THEN 'ANOMALIA' ELSE 'gemela-ok' END AS Clasificacion
FROM #Refs rp
JOIN Payroll.ScheduleDetail sd ON sd.Id = rp.SDId
LEFT JOIN #TwinHeaders th ON th.ScheduleId = rp.ScheduleId AND th.MenorId = sd.ScheduleId
WHERE sd.ScheduleId IS NULL OR sd.ScheduleId <> rp.ScheduleId
GROUP BY rp.ScheduleId, CASE WHEN th.ScheduleId IS NULL THEN 'ANOMALIA' ELSE 'gemela-ok' END
ORDER BY Clasificacion, rp.ScheduleId;

/* =============================================================================
   V2 - INTEGRIDAD FK: ningun detalle asignado apunta a un Schedule inexistente.
   Esperado 0.
   ============================================================================= */
DECLARE @V2_BrokenFK INT = (
    SELECT COUNT(*)
    FROM Payroll.ScheduleDetail sd
    WHERE sd.ScheduleId IS NOT NULL
      AND NOT EXISTS (SELECT 1 FROM Payroll.Schedule s WHERE s.Id = sd.ScheduleId)
);
PRINT 'V2 Detalles con ScheduleId a Schedule inexistente (ESPERADO 0): '
    + CAST(@V2_BrokenFK AS VARCHAR(20));
IF @V2_BrokenFK <> 0 SET @Fail = 1;

/* =============================================================================
   V3 - SIN CROSS-UF RESIDUAL: tras clonar los 711, ningun detalle debe seguir
   referenciado por 2 Schedule de UF distinta. Se recomputa el sharing sobre #Refs
   (estado vivo). cross-UF (DistinctFUs=2) ESPERADO 0; mismo-UF (DistinctFUs=1)
   son los gemelos de cabecera duplicada, que SE MANTIENEN (dedup diferido).
   ============================================================================= */
DECLARE @V3_CrossUF INT = 0, @V3_SameUF INT = 0;
SELECT @V3_CrossUF = SUM(CASE WHEN DistinctFUs = 2 THEN 1 ELSE 0 END),
       @V3_SameUF  = SUM(CASE WHEN DistinctFUs = 1 THEN 1 ELSE 0 END)
FROM (
    SELECT SDId, COUNT(DISTINCT FunctionalUnitId) AS DistinctFUs
    FROM #Refs
    GROUP BY SDId
    HAVING COUNT(*) > 1
) x;
PRINT 'V3 Detalles compartidos cross-UF restantes (ESPERADO 0): '
    + CAST(ISNULL(@V3_CrossUF,0) AS VARCHAR(20));
PRINT 'V3 (info) Detalles compartidos mismo-UF restantes (gemelos, se mantienen): '
    + CAST(ISNULL(@V3_SameUF,0) AS VARCHAR(20)) + '  (referencia QA 751: 41)';
IF ISNULL(@V3_CrossUF,0) <> 0 SET @Fail = 1;

/* =============================================================================
   V4 - HUERFANOS (INFORMATIVO, NO FALLA): detalles no referenciados por ningun
   cuadro quedan con ScheduleId NULL a proposito (columna nullable en 3.1).
   ============================================================================= */
DECLARE @V4_Orphans INT = (SELECT COUNT(*) FROM Payroll.ScheduleDetail WHERE ScheduleId IS NULL);
PRINT 'V4 (info) Detalles con ScheduleId NULL (huerfanos, ESPERADO/permitido): '
    + CAST(@V4_Orphans AS VARCHAR(20)) + '  (referencia QA 751: ~120.914; NO falla)';

/* =============================================================================
   V6 - RECONCILIACION DE CONTEOS (INFORMATIVO). Se imprime para cotejo manual
   contra los numeros de referencia QA 751 (NO son aserciones duras: dependen del
   ambiente).
   ============================================================================= */
DECLARE @V6_Assigned INT = (SELECT COUNT(*) FROM Payroll.ScheduleDetail WHERE ScheduleId IS NOT NULL);
DECLARE @V6_Total    INT = (SELECT COUNT(*) FROM Payroll.ScheduleDetail);
PRINT 'V6 (info) Detalles con ScheduleId asignado: ' + CAST(@V6_Assigned AS VARCHAR(20))
    + '  (referencia QA 751: 4.094.454 = 4.093.743 distintos + 711 clones)';
PRINT 'V6 (info) Total filas ScheduleDetail: ' + CAST(@V6_Total AS VARCHAR(20))
    + '  (referencia QA 751: 4.215.368 = 4.214.657 + 711 clones)';
PRINT 'V6 (info) Asignados + huerfanos = total: '
    + CAST(@V6_Assigned + @V4_Orphans AS VARCHAR(20)) + ' vs ' + CAST(@V6_Total AS VARCHAR(20))
    + '  (deben coincidir en cualquier ambiente)';

/* =============================================================================
   V5 - SPOT-CHECK NOMINAL (INFORMATIVO). El DBA fija @Sid a un cuadro
   REPRESENTATIVO y coteja la matriz devuelta contra lo que muestra el formulario.
   NO se hardcodea ningun Id en la logica: @Sid es un parametro que el operador
   llena. Si @Sid queda NULL, se listan candidatos (cuadros NO duplicados con mas
   dias poblados) para que el DBA elija uno.
   ============================================================================= */
DECLARE @Sid INT = NULL;   -- <<< DBA: fijar al Id de un Schedule representativo a inspeccionar

IF @Sid IS NULL
BEGIN
    PRINT 'V5 @Sid no fijado: se listan cuadros candidatos (fijar @Sid y re-ejecutar el bloque).';
    SELECT TOP 20
           rp.ScheduleId,
           rp.EmployeeId,
           rp.Period,
           rp.FunctionalUnitId,
           COUNT(*) AS DiasVivos
    FROM #Refs rp
    LEFT JOIN #TwinHeaders th ON th.ScheduleId = rp.ScheduleId
    LEFT JOIN #DupHeaders  dh ON dh.MenorId    = rp.ScheduleId
    WHERE th.ScheduleId IS NULL      -- excluye gemelas
      AND dh.MenorId    IS NULL      -- y tambien las MenorId de trios (para un caso "limpio")
    GROUP BY rp.ScheduleId, rp.EmployeeId, rp.Period, rp.FunctionalUnitId
    ORDER BY COUNT(*) DESC, rp.ScheduleId;
END
ELSE
BEGIN
    PRINT 'V5 Spot-check del Schedule @Sid = ' + CAST(@Sid AS VARCHAR(20))
        + ' via el camino NUEVO (ScheduleId):';
    SELECT sd.DateDetail, sd.Letter, sd.TotalNumberHours
    FROM Payroll.ScheduleDetail sd
    WHERE sd.ScheduleId = @Sid
    ORDER BY sd.DateDetail;
END

/* =============================================================================
   RESUMEN
   ============================================================================= */
PRINT '';
PRINT '===============================================';
IF @Fail = 1
    PRINT '*** RESULTADO: FALLO -> revisar V1/V2/V3 marcados arriba. El backfill NO quedo consistente. ***';
ELSE
    PRINT 'RESULTADO: OK -> V1=0, V2=0, V3=0. Backfill consistente (V4/V5/V6 son informativos).';
PRINT '===============================================';

/* --- Limpieza de temporales ------------------------------------------------- */
IF OBJECT_ID('tempdb..#Refs')        IS NOT NULL DROP TABLE #Refs;
IF OBJECT_ID('tempdb..#DupHeaders')  IS NOT NULL DROP TABLE #DupHeaders;
IF OBJECT_ID('tempdb..#TwinHeaders') IS NOT NULL DROP TABLE #TwinHeaders;
