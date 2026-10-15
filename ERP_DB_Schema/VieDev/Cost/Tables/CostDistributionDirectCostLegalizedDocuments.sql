CREATE TABLE [Cost].[CostDistributionDirectCostLegalizedDocuments] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [DistributionDirectCostId] INT NOT NULL,
    [ProvisionDocumentId]      INT NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostLegalizedDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostDetailLegalizedDocuments_CostDistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetailLegalizedDocuments_ProvisionDocument] FOREIGN KEY ([ProvisionDocumentId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionDirectCostLegalizedDocuments] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetailLegalizedDocuments_CostDistributionDirectCost];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostLegalizedDocuments] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetailLegalizedDocuments_ProvisionDocument];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento de provision', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'ProvisionDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la distribucion de elementos del costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la legalización de documentos de la distribucion del costo, costo directo  ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostLegalizedDocuments', @level2type = N'COLUMN', @level2name = N'Id';

