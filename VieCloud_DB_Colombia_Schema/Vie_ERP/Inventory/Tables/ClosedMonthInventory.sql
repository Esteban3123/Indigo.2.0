CREATE TABLE [Inventory].[ClosedMonthInventory] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ProductCost]      DECIMAL (18, 2) NULL,
    [FinalProductCost] NUMERIC (18, 2) NULL,
    [SellingPrice]     NUMERIC (18, 2) NULL,
    [ProductId]        INT             NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [WareHouseId]      INT             NULL,
    CONSTRAINT [PK_ClosedMonthInventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Inventory].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventory_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth];




GO
ALTER TABLE [Inventory].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén o depósito donde se registra el inventario cerrado; referencia a centro de distribución o bodega de medicamentos/insumos. INT NOT NULL, clave foránea opcional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'WareHouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'WareHouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'WareHouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del período mensual cerrado de inventario; vinculado a cierre contable y auditoria de existencias. FK → [Inventory].[ClosedMonth]. INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del mes cerrado de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto, medicamento o insumo inventariado; referencia a catálogo de productos. FK → [Inventory].[InventoryProduct]. INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta unitario del producto al cliente o usuario final; expresado en moneda local. NUMERIC(18,2), puede ser nulo si no aplica venta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'SellingPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'SellingPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'SellingPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo unitario del último evento de compra o adquisición del producto; valor histórico de la última transacción de entrada. NUMERIC(18,2), refleja último precio pagado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio del ultimo costo del producto, este es el ultimo valor con el que se compro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'FinalProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo promedio ponderado del producto, actualizado dinámicamente cada vez que ingresa nueva cantidad; usado para valuación de inventario. DECIMAL(18,2), puede ser nulo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio del producto, este se actualiza cada vez que se realiza una entrada del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de unidades en existencia al cierre del mes; número de items en stock del producto. INT NOT NULL, base para cálculo de valor de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de inventario cerrado mensual; secuencia Identity. INT IDENTITY(1,1) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del cierre mensual de inventario por producto y bodega. Guarda las cantidades, costos y precios de venta de cada artículo al momento del cierre del período contable/inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory';
