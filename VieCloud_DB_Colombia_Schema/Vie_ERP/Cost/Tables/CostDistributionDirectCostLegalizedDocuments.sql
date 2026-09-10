CREATE TABLE [Cost].[CostDistributionDirectCostLegalizedDocuments] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [DistributionDirectCostId] INT NOT NULL,
    [ProvisionDocumentId]      INT NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostLegalizedDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostDetailLegalizedDocuments_CostDistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetailLegalizedDocuments_ProvisionDocument] FOREIGN KEY ([ProvisionDocumentId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del documento de provisión/legalización de costos directos; referencia a documento soporte que respalda la distribución del costo (FK → CostDistributionDirectCost). Tipo: INT. Dominio: documentación de provisión, soporte de gasto, comprobante de costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'ProvisionDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento de provision', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'ProvisionDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'ProvisionDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/encabezado de distribución de costos directos; referencia a registro maestro que agrupa elementos de costo a distribuir (FK → CostDistributionDirectCost). Tipo: INT. Dominio: centro de costo, distribución de gasto, asignación de costo directo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la distribucion de elementos del costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la legalización de documentos en la distribución de costos directos; clave primaria que vincula documento de provisión con cabecera de distribución. Tipo: INT IDENTITY. Dominio: legalización, documento soporte, auditoría de costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la legalización de documentos de la distribucion del costo, costo directo  ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los documentos legalizados (documentos de provisión) con las distribuciones de costos directos, permitiendo identificar qué documentos soportan o respaldan cada distribución de costo directo en el proceso de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments';
