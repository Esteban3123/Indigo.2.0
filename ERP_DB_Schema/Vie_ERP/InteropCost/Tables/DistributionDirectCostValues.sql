CREATE TABLE [InteropCost].[DistributionDirectCostValues] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [ByArea]                   DECIMAL (18, 2) CONSTRAINT [DF_DistributionDirectCostValues_ByArea] DEFAULT ((0)) NOT NULL,
    [ByOfficialHours]          DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea1] DEFAULT ((0)) NOT NULL,
    [BySupplyValue]            DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea2] DEFAULT ((0)) NOT NULL,
    [ByWorkmanship]            DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea3] DEFAULT ((0)) NOT NULL,
    [ByAssetValue]             DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea4] DEFAULT ((0)) NOT NULL,
    [BySales]                  DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea5] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DistributionDirectCostValues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionDirectCostValues_DistributionDirectCostValues] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [InteropCost].[DistributionDirectCost] ([Id]),
    CONSTRAINT [FK_DistributionDirectCostValues_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionDirectCostValues] NOCHECK CONSTRAINT [FK_DistributionDirectCostValues_DistributionDirectCostValues];


GO
ALTER TABLE [InteropCost].[DistributionDirectCostValues] NOCHECK CONSTRAINT [FK_DistributionDirectCostValues_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Por ventas', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Por valor de activo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByAssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Por mano de obra', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByWorkmanship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Por valor de oferta', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Por horas oficiales', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByOfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Por área', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de producción  al que pertenecen los valores', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del documento de distribución al que pertenece los valores', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'identificación del registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'Id';

