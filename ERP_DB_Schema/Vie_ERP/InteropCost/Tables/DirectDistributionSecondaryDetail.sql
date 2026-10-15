CREATE TABLE [InteropCost].[DirectDistributionSecondaryDetail] (
    [Id]                            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DirectDistributionSecondaryId] INT            NOT NULL,
    [ProductionCenterId]            INT            NOT NULL,
    [MeasurementUnitId]             INT            NOT NULL,
    [Percentage]                    NUMERIC (5, 2) CONSTRAINT [DF_DirectDistributionSecondaryDetail_Percentage] DEFAULT ((0)) NOT NULL,
    [Value]                         NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_DirectDistributionSecondaryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DirectDistributionSecondaryDetail_DirectDistributionSecondary] FOREIGN KEY ([DirectDistributionSecondaryId]) REFERENCES [InteropCost].[DirectDistributionSecondary] ([Id]),
    CONSTRAINT [FK_DirectDistributionSecondaryDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_DirectDistributionSecondaryDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_DirectDistributionSecondaryDetail_DirectDistributionSecondary];


GO
ALTER TABLE [InteropCost].[DirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_DirectDistributionSecondaryDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [InteropCost].[DirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_DirectDistributionSecondaryDetail_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje que le corresponde al centro de costo despues de realizar laq distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del centro de producción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID secundaria de distribución directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Id';

