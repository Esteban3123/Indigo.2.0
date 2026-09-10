CREATE TABLE [Inventory].[ProductRateDetailPackage] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductRateDetailId] INT NOT NULL,
    [PackageDetailId]     INT NOT NULL,
    [ProductId]           INT NOT NULL,
    [Quantity]            INT NULL,
    [DoseNumber]          INT NULL,
    CONSTRAINT [PK_ProductRateDetailPackage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductRateDetailPackage_PackageDetailId] FOREIGN KEY ([PackageDetailId]) REFERENCES [MixingStation].[PackageDetail] ([Id]),
    CONSTRAINT [FK_ProductRateDetailPackage_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductRateDetailPackage_ProductRateDetail] FOREIGN KEY ([ProductRateDetailId]) REFERENCES [Inventory].[ProductRateDetail] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de dosis en el paquete terapéutico; orden de administración o dispensación del medicamento/producto dentro del tratamiento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'DoseNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'DoseNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'DoseNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto por dosis; volumen o número de dosis a suministrar en cada administración.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto en inventario (CUM, código de medicamento o insumo); referencia a InventoryProduct para trazabilidad farmacéutica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUM de referencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de paquete en MixingStation; vincula la composición física del paquete terapéutico con los productos que lo integran.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'PackageDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del paquete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'PackageDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'PackageDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de tarifa de producto; enlaza el precio/tarifa negociada del medicamento con su inclusión en el paquete.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'ProductRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de tarifa de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'ProductRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'ProductRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla; identificador único del registro de asociación entre producto, paquete y tarifa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los detalles de tarifas de productos con los paquetes de atención, indicando qué productos (medicamentos o insumos) están incluidos en cada paquete, en qué cantidad y en qué número de dosis.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetailPackage';
