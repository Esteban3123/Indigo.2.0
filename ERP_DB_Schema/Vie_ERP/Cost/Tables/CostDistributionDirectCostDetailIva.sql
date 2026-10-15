CREATE TABLE [Cost].[CostDistributionDirectCostDetailIva] (
    [Id]                                 INT             IDENTITY (1, 1) NOT NULL,
    [CostDistributionDirectCostDetailId] INT             NOT NULL,
    [BaseIva]                            DECIMAL (18, 2) NOT NULL,
    [GeneralLedgerIvaId]                 INT             NOT NULL,
    [IvaValue]                           DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostDetailIva] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostDetailIva_CostDistributionDirectCostDetail] FOREIGN KEY ([CostDistributionDirectCostDetailId]) REFERENCES [Cost].[CostDistributionDirectCostDetail] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetailIva_GeneralLedgerIVA] FOREIGN KEY ([GeneralLedgerIvaId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetailIva] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetailIva_CostDistributionDirectCostDetail];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetailIva] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetailIva_GeneralLedgerIVA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Iva calculado por detalle', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tarifa de Iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'GeneralLedgerIvaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Base para el calculo del Iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'BaseIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la distribución del costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro (Iva calculado por Tarifa elegida para cada detalle de la distribución del costo)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'Id';

