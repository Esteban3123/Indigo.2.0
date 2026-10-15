CREATE TABLE [Cost].[CostDistributionDirectCostValues] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [ByArea]                   DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByArea] DEFAULT ((0)) NOT NULL,
    [ByOfficialHours]          DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByOfficialHours] DEFAULT ((0)) NOT NULL,
    [BySupplyValue]            DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_BySupplyValue] DEFAULT ((0)) NOT NULL,
    [ByWorkmanship]            DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByWorkmanship] DEFAULT ((0)) NOT NULL,
    [ByAssetValue]             DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByAssetValue] DEFAULT ((0)) NOT NULL,
    [BySales]                  DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_BySales] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostValues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostValues_CostDistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostValues_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionDirectCostValues] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostValues_CostDistributionDirectCost];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostValues] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostValues_CostProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ventas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor de activo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByAssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mano de obra', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByWorkmanship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor de oferta', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'horas oficiales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByOfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Area', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'D de costo directo de distribución', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'Id';

