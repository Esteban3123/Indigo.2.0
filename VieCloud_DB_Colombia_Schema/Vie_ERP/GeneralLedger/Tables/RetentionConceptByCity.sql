CREATE TABLE [GeneralLedger].[RetentionConceptByCity] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RetentionConceptId] INT            NOT NULL,
    [CityId]             INT            NOT NULL,
    [Rate]               DECIMAL (5, 3) NOT NULL,
    [TimeStamp]          ROWVERSION     NOT NULL,
    CONSTRAINT [PK_RetentionConceptByCity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RetentionConceptByCity_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_RetentionConceptByCity_RetentionConcept] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de retención por ciudad. Registra el instante exacto de creación, actualización o modificación del concepto de retención aplicable a la ciudad; auditoría de cambios en tasas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o tasa de retención (DECIMAL 5,3) aplicable al concepto de retención en la ciudad especificada; porcentaje de descuento, glosa o deducción fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de retencion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la ciudad donde aplica la retención (referencia a [Common].[City]); vinculación geográfica del concepto de retención.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'CityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la ciudad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'CityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del concepto de retención (referencia a [GeneralLedger].[RetentionConcepts]); tipo de retención aplicable por ciudad (ej: IVA, ICA, renta).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de retencion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK IDENTITY) del registro de retención por ciudad; clave primaria del mapeo ciudad-concepto-tasa.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasas de retención tributaria por concepto de retención y municipio/ciudad. Permite configurar el porcentaje de retención aplicable según el tipo de retención (ICA, fuente, IVA, etc.) y la ciudad donde se realiza la transacción contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity';
