CREATE TABLE [Cost].[CostDistributionDirectCostIva] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [CostDistributionDirectCostId] INT             NOT NULL,
    [BaseIva]                      DECIMAL (18, 2) NOT NULL,
    [GeneralLedgerIvaId]           INT             NOT NULL,
    [IvaValue]                     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostIva] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostIva_CostDistributionDirectCost] FOREIGN KEY ([CostDistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostIva_GeneralLedgerIVA] FOREIGN KEY ([GeneralLedgerIvaId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionDirectCostIva] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostIva_CostDistributionDirectCost];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostIva] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostIva_GeneralLedgerIVA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor del iva - se da con la operación => Base iva * tarifa iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tarifa del iva relacionada con la tabla GeneralLedgerIva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'GeneralLedgerIvaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor base del iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'BaseIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Distribución de costos costo directo - tabla CostDistributionDirectCost', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Distribución de costos, Costo directo Iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'Id';

