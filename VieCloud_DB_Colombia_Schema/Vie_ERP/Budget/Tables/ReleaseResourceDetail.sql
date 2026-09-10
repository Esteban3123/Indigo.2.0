CREATE TABLE [Budget].[ReleaseResourceDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ReleaseResourceId]          INT             NOT NULL,
    [ObligationDetailId]         INT             NULL,
    [CommimentDetailId]          INT             NULL,
    [ObligationModificationId]   INT             NULL,
    [CommitmentModificationId]   INT             NULL,
    [AvailabilityModificationId] INT             NULL,
    [Value]                      NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_ReleaseResourceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReleaseResourceDetail_AvailabilityModification] FOREIGN KEY ([AvailabilityModificationId]) REFERENCES [Budget].[AvailabilityModification] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_CommitmentDetail] FOREIGN KEY ([CommimentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_CommitmentModification] FOREIGN KEY ([CommitmentModificationId]) REFERENCES [Budget].[CommitmentModification] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_ObligationModification] FOREIGN KEY ([ObligationModificationId]) REFERENCES [Budget].[ObligationModification] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_ReleaseResource] FOREIGN KEY ([ReleaseResourceId]) REFERENCES [Budget].[ReleaseResource] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (NUMERIC 18,2) del detalle de liberación de recursos presupuestales, monto en pesos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la modificación de disponibilidad presupuestal asociada al detalle de liberación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id modificacion de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la modificación del compromiso presupuestal vinculado al detalle de liberación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'CommitmentModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id modificacion de el compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'CommitmentModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'CommitmentModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la modificación de la obligación presupuestal relacionada al detalle de liberación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id modificacion de la obligacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ObligationModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del compromiso presupuestal asociado a la línea de liberación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'CommimentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id detalle del compromiso asociado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'CommimentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'CommimentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de la obligación presupuestal vinculado a la línea de liberación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id detalle de la oblicacion asociada', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del encabezado o registro maestro de liberación de recursos presupuestales', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ReleaseResourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera de la liberacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ReleaseResourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'ReleaseResourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) único de cada línea de detalle en la tabla de liberación de recursos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la liberación de recursos presupuestales. Registra los valores liberados asociados a compromisos, obligaciones y sus modificaciones dentro del proceso de ejecución presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReleaseResourceDetail';
