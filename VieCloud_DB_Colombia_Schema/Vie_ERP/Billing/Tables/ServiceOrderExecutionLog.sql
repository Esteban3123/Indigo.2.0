CREATE TABLE [Billing].[ServiceOrderExecutionLog] (
    [Id]                  BIGINT           IDENTITY (1, 1) NOT NULL,
    [ExecutionId]         UNIQUEIDENTIFIER NOT NULL,
    [StepOrder]           INT              NOT NULL,
    [StepName]            VARCHAR (120)    NOT NULL,
    [StartedAt]           DATETIME2 (3)    NOT NULL,
    [EndedAt]             DATETIME2 (3)    NOT NULL,
    [DurationMs]          AS (DATEDIFF(MILLISECOND, [StartedAt], [EndedAt])),
    [ServiceOrderId]      INT              NULL,
    [ServiceOrderCode]    VARCHAR (20)     NULL,
    [AdmissionNumber]     VARCHAR (20)     NULL,
    [PatientCode]         VARCHAR (25)     NULL,
    [EntityName]          VARCHAR (250)    NULL,
    [EntityId]            INT              NULL,
    [DetailRows]          INT              NULL,
    [SurgicalRows]        INT              NULL,
    [UserCode]            VARCHAR (20)     NULL,
    [Observation]         VARCHAR (MAX)    NULL,
    [CreatedAt]           DATETIME2 (3)    CONSTRAINT [DF_ServiceOrderExecutionLog_CreatedAt] DEFAULT (SYSUTCDATETIME()) NOT NULL,
    CONSTRAINT [PK_ServiceOrderExecutionLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderExecutionLog_ExecutionId_StepOrder]
    ON [Billing].[ServiceOrderExecutionLog]([ExecutionId] ASC, [StepOrder] ASC)
    INCLUDE([StepName], [DurationMs], [ServiceOrderId], [ServiceOrderCode], [AdmissionNumber], [PatientCode], [DetailRows], [SurgicalRows]);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderExecutionLog_CreatedAt_Duration]
    ON [Billing].[ServiceOrderExecutionLog]([CreatedAt] DESC, [DurationMs] DESC)
    INCLUDE([ExecutionId], [StepName], [ServiceOrderId], [ServiceOrderCode], [AdmissionNumber], [UserCode]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bitacora transaccional de checkpoints de ejecucion para Billing.SP_GenerateServiceOrder_Output. Permite medir duracion por flujo, volumen de detalles y observaciones para diagnosticar cuellos de botella en la generacion de ordenes de servicio asociadas a dispensaciones u otros origenes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderExecutionLog';

