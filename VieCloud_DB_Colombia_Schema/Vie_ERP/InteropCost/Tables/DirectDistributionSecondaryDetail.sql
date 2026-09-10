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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de la distribución directa secundaria asignado al centro de producción. Tipo: NUMERIC(18). Representa el monto, cantidad o costo a distribuir según la unidad de medida.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de asignación al centro de producción tras distribución secundaria. Tipo: NUMERIC(5,2), rango 0-100%. Especifica la proporción que le corresponde al centro de costo. Defecto: 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje que le corresponde al centro de costo despues de realizar laq distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida asociada (FK a Inventory.InventoryMeasurementUnit). Solo válidas unidades tipo 4 - sistema de costos. Determina escala de Value.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción/unidad funcional receptor de la distribución (FK a InteropCost.ProductionCenter). Clave para asignar costos a centros de atención.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del centro de producción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la distribución directa secundaria padre (FK a InteropCost.DirectDistributionSecondary). Agrupa detalles de una misma distribución.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID secundaria de distribución directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'DirectDistributionSecondaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la tabla (INT IDENTITY 1,1). Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la distribución directa secundaria de costos: registra el desglose por centro de producción y unidad de medida, indicando el porcentaje y valor asignado a cada destino dentro de un proceso de distribución secundaria de costos indirectos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondaryDetail';
