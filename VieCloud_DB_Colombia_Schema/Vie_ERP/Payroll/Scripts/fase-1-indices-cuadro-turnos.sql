/* =============================================================================
   Fase 1 - Performance de carga del Cuadro de Turnos  (rama chore-modularize-shift-board)
   Indices de soporte para la lectura del cuadro de turnos.

   Estos indices tambien estan declarados en el proyecto SSDT Vie_ERP
   (Payroll/Tables/Schedule.sql y Payroll/Tables/ScheduleDetail.sql), por lo que
   el DACPAC los creara en el publish. Este script es para que el DBA los cree
   MANUALMENTE y ONLINE (Azure SQL / Enterprise) ANTES del publish, evitando el
   bloqueo de un indice creado offline sobre tablas grandes.

     - Idempotente: no falla si el indice ya existe.
     - ONLINE = ON: requiere Azure SQL Database o SQL Server Enterprise.
       En ediciones Standard / on-prem sin ONLINE, quitar "WITH (ONLINE = ON)".
     - Definiciones IDENTICAS a las del SSDT (nombre, columnas, INCLUDE):
       no generan drift en el DACPAC (ONLINE es opcion de build, no persiste).
   ============================================================================= */

SET NOCOUNT ON;
GO

/* --- [Payroll].[Schedule] : respalda el filtro (FunctionalUnitId, Period) de GetSchedule --- */
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_Schedule__FunctionalUnitId_Period'
      AND object_id = OBJECT_ID(N'[Payroll].[Schedule]')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Schedule__FunctionalUnitId_Period]
        ON [Payroll].[Schedule] ([FunctionalUnitId] ASC, [Period] ASC)
        INCLUDE ([EmployeeId], [TotalHour], [State])
        WITH (ONLINE = ON);
    PRINT 'Creado: IX_Schedule__FunctionalUnitId_Period';
END
ELSE
    PRINT 'Ya existe: IX_Schedule__FunctionalUnitId_Period';
GO

/* --- [Payroll].[ScheduleDetail] : respalda la consulta batch (EmployeeId, DateDetail) --- */
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_ScheduleDetail_EmployeeId_DateDetail'
      AND object_id = OBJECT_ID(N'[Payroll].[ScheduleDetail]')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ScheduleDetail_EmployeeId_DateDetail]
        ON [Payroll].[ScheduleDetail] ([EmployeeId] ASC, [DateDetail] ASC)
        INCLUDE ([ScheduleFunctionalUnitId], [FunctionalUnitId], [TotalNumberHours], [Letter], [ScheduleTemplateId])
        WITH (ONLINE = ON);
    PRINT 'Creado: IX_ScheduleDetail_EmployeeId_DateDetail';
END
ELSE
    PRINT 'Ya existe: IX_ScheduleDetail_EmployeeId_DateDetail';
GO
