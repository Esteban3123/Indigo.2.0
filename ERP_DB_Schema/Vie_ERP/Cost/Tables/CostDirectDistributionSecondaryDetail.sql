CREATE TABLE [Cost].[CostDirectDistributionSecondaryDetail] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DirectDistributionSecondaryId] INT             NOT NULL,
    [ProductionCenterId]            INT             NOT NULL,
    [MeasurementUnitId]             INT             NULL,
    [Percentage]                    NUMERIC (5, 2)  CONSTRAINT [DF_CostDirectDistributionSecondaryDetail_Percentage] DEFAULT ((0)) NOT NULL,
    [Value]                         DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostDirectDistributionSecondaryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_CostDirectDistributionSecondary] FOREIGN KEY ([DirectDistributionSecondaryId]) REFERENCES [Cost].[CostDirectDistributionSecondary] ([Id]),
    CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_CostDirectDistributionSecondary];


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_CostProductionCenter];


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_InventoryMeasurementUnit];


GO
CREATE NONCLUSTERED INDEX [IX_CostDirectDistributionSecondaryDetail_DirectDistributionSecondaryId]
    ON [Cost].[CostDirectDistributionSecondaryDetail]([DirectDistributionSecondaryId] ASC)
    INCLUDE([ProductionCenterId], [Value]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje que le corresponde al centro de costo despues de realizar laq distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de producción operativo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de distribución secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Id';

