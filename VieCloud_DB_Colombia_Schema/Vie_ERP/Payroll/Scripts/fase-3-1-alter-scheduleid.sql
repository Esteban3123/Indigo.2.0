/* =============================================================================
   Fase 3.1 - PASO 1 de 4 - Esquema: columna ScheduleDetail.ScheduleId
   (rama chore-modularize-shift-board)

   Agrega la FK normalizada ScheduleDetail.ScheduleId -> Schedule.Id (relacion
   1-a-N que reemplaza gradualmente las 31 columnas D01..D31 de Schedule), su
   FK y su indice de soporte. Aditivo y reversible: NO borra nada, la columna
   queda NULLABLE durante la transicion (expand-contract).

   Este script es el equivalente ejecutable del DDL declarado en el SSDT
   (Vie_ERP/Payroll/Tables/ScheduleDetail.sql), para aplicar en QA sin publicar
   todo el DACPAC. Definiciones IDENTICAS al SSDT -> sin drift en el publish.

     - Idempotente: cada bloque valida existencia antes de crear.
     - La FK se agrega WITH NOCHECK (convencion de todas las FK de esta tabla):
       evita el scan de validacion sobre la tabla completa al aplicar.
     - ONLINE = ON en el indice: requiere Azure SQL / Enterprise. Quitar en
       Standard/on-prem.

   ORDEN DE EJECUCION (runbook completo en README.md):
     1) fase-3-1-alter-scheduleid.sql   <-- ESTE
     2) fase-3-1-backfill-scheduleid.sql
     3) fase-3-1-verificacion.sql
     4) fase-3-3-crear-vista.sql
   ============================================================================= */

SET NOCOUNT ON;
GO

/* --- Columna ScheduleId ----------------------------------------------------- */
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID(N'[Payroll].[ScheduleDetail]')
      AND name = N'ScheduleId'
)
BEGIN
    ALTER TABLE [Payroll].[ScheduleDetail] ADD [ScheduleId] INT NULL;
    PRINT 'Creada: columna [Payroll].[ScheduleDetail].[ScheduleId]';
END
ELSE
    PRINT 'Ya existe: columna [Payroll].[ScheduleDetail].[ScheduleId]';
GO

/* --- FK_ScheduleDetail_Schedule (WITH NOCHECK, convencion de la tabla) ------- */
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys
    WHERE name = N'FK_ScheduleDetail_Schedule'
      AND parent_object_id = OBJECT_ID(N'[Payroll].[ScheduleDetail]')
)
BEGIN
    ALTER TABLE [Payroll].[ScheduleDetail] WITH NOCHECK
        ADD CONSTRAINT [FK_ScheduleDetail_Schedule]
        FOREIGN KEY ([ScheduleId]) REFERENCES [Payroll].[Schedule] ([Id]);
    PRINT 'Creada: FK_ScheduleDetail_Schedule (NOCHECK)';
END
ELSE
    PRINT 'Ya existe: FK_ScheduleDetail_Schedule';
GO

/* --- IX_ScheduleDetail__ScheduleId ------------------------------------------ */
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_ScheduleDetail__ScheduleId'
      AND object_id = OBJECT_ID(N'[Payroll].[ScheduleDetail]')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ScheduleDetail__ScheduleId]
        ON [Payroll].[ScheduleDetail] ([ScheduleId] ASC)
        WITH (ONLINE = ON);
    PRINT 'Creado: IX_ScheduleDetail__ScheduleId';
END
ELSE
    PRINT 'Ya existe: IX_ScheduleDetail__ScheduleId';
GO
