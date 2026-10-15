CREATE TABLE [InteropCost].[DistributionDirectCostDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [MainAccountId]            INT             NOT NULL,
    [CostCenterId]             INT             NULL,
    [MeasurementUnitId]        INT             NULL,
    [Percentage]               NUMERIC (5, 2)  CONSTRAINT [DF_DistributionDirectCostDetail_Percentage] DEFAULT ((0)) NOT NULL,
    [Count]                    NUMERIC (18, 2) CONSTRAINT [DF_DistributionDirectCostDetail_Count] DEFAULT ((0)) NOT NULL,
    [CostValue]                DECIMAL (18, 2) CONSTRAINT [DF_DistributionDirectCostDetail_CostValue] DEFAULT ((0)) NOT NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_DistributionDirectCostDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionDirectCostDetail_DistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [InteropCost].[DistributionDirectCost] ([Id]),
    CONSTRAINT [FK_DistributionDirectCostDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_DistributionDirectCostDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_DistributionDirectCostDetail_DistributionDirectCost];


GO
ALTER TABLE [InteropCost].[DistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_DistributionDirectCostDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [InteropCost].[DistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_DistributionDirectCostDetail_ProductionCenter];


GO
CREATE NONCLUSTERED INDEX [IX_DistributionDirectCostDetail__ProductionCenterId__MeasurementUnitId__DistributionDirectCostId__CostValue]
    ON [InteropCost].[DistributionDirectCostDetail]([ProductionCenterId] ASC, [MeasurementUnitId] ASC, [DistributionDirectCostId] ASC, [CostValue] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del costo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Count';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje que le corresponde al centro de costo despues de realizar laq distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id centro de costos ', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la distribucion por gasto directo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la distribucion por gasto directo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Id';

