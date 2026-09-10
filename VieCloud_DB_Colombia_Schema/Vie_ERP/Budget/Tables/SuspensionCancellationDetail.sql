CREATE TABLE [Budget].[SuspensionCancellationDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SuspensionCancellationId] INT             NOT NULL,
    [SuspensionDetailId]       INT             NOT NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_SuspensionAnnularDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionAnnular] FOREIGN KEY ([SuspensionCancellationId]) REFERENCES [Budget].[SuspensionCancellation] ([Id]),
    CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionDetail] FOREIGN KEY ([SuspensionDetailId]) REFERENCES [Budget].[SuspensionDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (NUMERIC 18,2) del levantamiento o cancelación de la suspensión de servicios, presupuesto o contrato', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del levantamiento de la suspencion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de suspensión relacionado; referencia a SuspensionDetail.Id para rastrear el rubro suspendido', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'SuspensionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la suspension', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'SuspensionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'SuspensionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera de levantamiento/cancelación de suspensión; referencia a SuspensionCancellation.Id para agrupar detalles', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'SuspensionCancellationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de el levantamiento de la suspension', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'SuspensionCancellationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'SuspensionCancellationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada línea de detalle de cancelación de suspensión', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los valores asociados a cada suspensión o cancelación presupuestal, registrando el monto específico por ítem de suspensión dentro de un proceso de suspensión o cancelación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SuspensionCancellationDetail';
