CREATE TABLE [Inventory].[PharmaceuticalDispensingExecutionLog] (
    [Id]                       BIGINT           IDENTITY (1, 1) NOT NULL,
    [ExecutionId]              UNIQUEIDENTIFIER NOT NULL,
    [StepOrder]                INT              NOT NULL,
    [StepName]                 VARCHAR (120)    NOT NULL,
    [StartedAt]                DATETIME2 (3)    NOT NULL,
    [EndedAt]                  DATETIME2 (3)    NOT NULL,
    [DurationMs]               AS (DATEDIFF(MILLISECOND, [StartedAt], [EndedAt])),
    [PharmaceuticalDispensingId] INT            NULL,
    [DispensingCode]           VARCHAR (20)     NULL,
    [AdmissionNumber]          VARCHAR (20)     NULL,
    [DetailRows]               INT              NULL,
    [BatchRows]                INT              NULL,
    [UserCode]                 VARCHAR (20)     NULL,
    [Observation]              VARCHAR (MAX)    NULL,
    [CreatedAt]                DATETIME2 (3)    CONSTRAINT [DF_PharmaceuticalDispensingExecutionLog_CreatedAt] DEFAULT (SYSUTCDATETIME()) NOT NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingExecutionLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_PharmaceuticalDispensingExecutionLog_ExecutionId_StepOrder]
    ON [Inventory].[PharmaceuticalDispensingExecutionLog]([ExecutionId] ASC, [StepOrder] ASC)
    INCLUDE([StepName], [DurationMs], [PharmaceuticalDispensingId], [DispensingCode], [AdmissionNumber], [DetailRows], [BatchRows]);


GO
CREATE NONCLUSTERED INDEX [IX_PharmaceuticalDispensingExecutionLog_CreatedAt_Duration]
    ON [Inventory].[PharmaceuticalDispensingExecutionLog]([CreatedAt] DESC, [DurationMs] DESC)
    INCLUDE([ExecutionId], [StepName], [PharmaceuticalDispensingId], [DispensingCode], [AdmissionNumber], [UserCode]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bitacora transaccional de checkpoints de ejecucion para Inventory.SP_GeneratePharmaceuticalDispensing_Output. Permite medir duracion por flujo, volumen de detalles/lotes y observaciones de error o salida temprana para diagnosticar cuellos de botella en dispensaciones farmaceuticas grandes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingExecutionLog';

