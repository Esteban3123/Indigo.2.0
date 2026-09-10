/* =============================================================================
   Fase 2 - Performance de carga del Cuadro de Turnos  (rama chore-modularize-shift-board)
   Índice de apoyo recomendado por el motor (missing index DMV, score alto).

   Extiende el índice EXISTENTE de la FK ScheduleDetailHourId a "covering"
   agregando INCLUDE (ConceptType, ConceptId), para evitar key-lookups al unir
   ScheduleDetailHour -> ScheduleDetailConcept.

   Alcance: acelera el path de DETALLE (multi-selección de empleados, que hace
   join a ScheduleDetailConcept) y el guardado. NO afecta el path de selección
   de UF / cambio de mes (ese es GetSchedule, que no toca esta tabla).

     - Idempotente: solo recrea si aún no tiene ConceptId como columna incluida.
     - Requiere DROP_EXISTING (extender INCLUDE = recrear el índice).
     - ONLINE = ON: requiere Azure SQL / Enterprise. Quitar en Standard/on-prem.
     - Definición IDÉNTICA a la del SSDT (Vie_ERP/Payroll/Tables/ScheduleDetailConcept.sql).
   ============================================================================= */

SET NOCOUNT ON;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_ScheduleDetailConcept__ScheduleDetailHourId'
      AND object_id = OBJECT_ID(N'[Payroll].[ScheduleDetailConcept]')
)
BEGIN
    -- El indice base no existe en este ambiente (drift entre QAs: algunas copias
    -- de Payroll.ScheduleDetailConcept nunca lo tuvieron) -> crear desde cero,
    -- sin DROP_EXISTING (requiere que el indice ya exista para poder usarlo).
    CREATE NONCLUSTERED INDEX [IX_ScheduleDetailConcept__ScheduleDetailHourId]
        ON [Payroll].[ScheduleDetailConcept] ([ScheduleDetailHourId] ASC)
        INCLUDE ([ConceptType], [ConceptId])
        WITH (ONLINE = ON);
    PRINT 'Creado (no existia en este ambiente): IX_ScheduleDetailConcept__ScheduleDetailHourId';
END
ELSE IF NOT EXISTS (
    SELECT 1
    FROM sys.index_columns ic
    JOIN sys.columns  c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
    JOIN sys.indexes  i ON i.object_id = ic.object_id AND i.index_id  = ic.index_id
    WHERE i.name = N'IX_ScheduleDetailConcept__ScheduleDetailHourId'
      AND i.object_id = OBJECT_ID(N'[Payroll].[ScheduleDetailConcept]')
      AND ic.is_included_column = 1
      AND c.name = N'ConceptId'
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ScheduleDetailConcept__ScheduleDetailHourId]
        ON [Payroll].[ScheduleDetailConcept] ([ScheduleDetailHourId] ASC)
        INCLUDE ([ConceptType], [ConceptId])
        WITH (DROP_EXISTING = ON, ONLINE = ON);
    PRINT 'Extendido a covering: IX_ScheduleDetailConcept__ScheduleDetailHourId (+INCLUDE ConceptType, ConceptId)';
END
ELSE
    PRINT 'Ya es covering: IX_ScheduleDetailConcept__ScheduleDetailHourId';
GO

/* -----------------------------------------------------------------------------
   Índice #2: Contract(FunctionalUnitId, Valid) INCLUDE (EmployeeId, PositionId)

   Extiende el índice EXISTENTE iFunctionalUnitId_Payroll_Contract_5A9740F2
   (antes solo FunctionalUnitId) agregando Valid a la clave y EmployeeId/PositionId
   como INCLUDE.

   Causa raíz: tras corregir el predicado no-sargable (& -> && en
   EmployeeRepository.GetEmployeesByFunctionalUnit y ScheduleRepository.GetSchedule),
   el motor generó un WHERE EXISTS sargable pero SIN este índice de apoyo seguía
   escaneando Employee completo (9.214 filas) + Contract completo (63.422 filas)
   via Hash Match, en vez de un seek. Confirmado con el missing-index DMV
   (score=909) recien visible porque antes el predicado & se lo impedia.

   Alcance: acelera GetEmployeesByFunctionalUnit (selección de UF) y el filtro
   EXISTS de GetSchedule que comparte la misma forma (Contract JOIN Position).
   ----------------------------------------------------------------------------- */
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'iFunctionalUnitId_Payroll_Contract_5A9740F2'
      AND object_id = OBJECT_ID(N'[Payroll].[Contract]')
)
BEGIN
    -- El indice base no existe en este ambiente -> crear desde cero, sin
    -- DROP_EXISTING (requiere que el indice ya exista para poder usarlo).
    CREATE NONCLUSTERED INDEX [iFunctionalUnitId_Payroll_Contract_5A9740F2]
        ON [Payroll].[Contract] ([FunctionalUnitId] ASC, [Valid] ASC)
        INCLUDE ([EmployeeId], [PositionId])
        WITH (ONLINE = ON);
    PRINT 'Creado (no existia en este ambiente): iFunctionalUnitId_Payroll_Contract_5A9740F2';
END
ELSE IF NOT EXISTS (
    SELECT 1
    FROM sys.index_columns ic
    JOIN sys.columns  c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
    JOIN sys.indexes  i ON i.object_id = ic.object_id AND i.index_id  = ic.index_id
    WHERE i.name = N'iFunctionalUnitId_Payroll_Contract_5A9740F2'
      AND i.object_id = OBJECT_ID(N'[Payroll].[Contract]')
      AND ic.is_included_column = 1
      AND c.name = N'PositionId'
)
BEGIN
    CREATE NONCLUSTERED INDEX [iFunctionalUnitId_Payroll_Contract_5A9740F2]
        ON [Payroll].[Contract] ([FunctionalUnitId] ASC, [Valid] ASC)
        INCLUDE ([EmployeeId], [PositionId])
        WITH (DROP_EXISTING = ON, ONLINE = ON);
    PRINT 'Extendido a covering: iFunctionalUnitId_Payroll_Contract_5A9740F2 (+Valid en clave, +INCLUDE EmployeeId, PositionId)';
END
ELSE
    PRINT 'Ya es covering: iFunctionalUnitId_Payroll_Contract_5A9740F2';
GO
