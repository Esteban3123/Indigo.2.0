CREATE TABLE [Inventory].[PhysicalInventory] (
    [Id]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WarehouseId]   INT NOT NULL,
    [ProductId]     INT NOT NULL,
    [BatchSerialId] INT NULL,
    [Quantity]      INT NOT NULL,
    CONSTRAINT [PK_PhysicalInventory__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhysicalInventory_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_PhysicalInventory_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PhysicalInventory_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[PhysicalInventory] NOCHECK CONSTRAINT [FK_PhysicalInventory_BatchSerial];




GO
ALTER TABLE [Inventory].[PhysicalInventory] NOCHECK CONSTRAINT [FK_PhysicalInventory_BatchSerial];


GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_PhysicalInventory_WarehouseId_Quantity_ProductId]
    ON [Inventory].[PhysicalInventory]([WarehouseId] ASC, [Quantity] ASC)
    INCLUDE([ProductId]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PhysicalInventory__ProductId__WarehouseId__BatchSerialId]
    ON [Inventory].[PhysicalInventory]([ProductId] ASC, [WarehouseId] ASC, [BatchSerialId] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2019-11-06
-- Description:	Se valida que el inventario fisico no tenga cantidades negativas
-- =============================================
CREATE TRIGGER [Inventory].[tgg_ValidateNegatives]
   ON  [Inventory].[PhysicalInventory]
   AFTER INSERT,UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM INSERTED WHERE Quantity < 0)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. La cantidad disponible del inventario físico no puede ser negativo', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades físicas contabilizadas del producto en el almacén; número entero (INT) que representa el stock real verificado en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote o número de serie asociado (FK → Inventory.BatchSerial); permite rastrear trazabilidad de medicamentos/insumos por lote o unidad serializada; puede ser nulo si el producto no requiere control de lote.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote o serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto inventariado (FK → Inventory.InventoryProduct); referencia única al catálogo de productos, medicamentos e insumos médicos almacenados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén o depósito donde se ubica físicamente el producto (FK → Inventory.Warehouse); referencia al centro de acopio, farmacia o bodega del establecimiento de salud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, INT IDENTITY) del registro de inventario físico; clave primaria que identifica cada conteo o verificación de stock en el almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de inventario físico por bodega y producto. Guarda las cantidades reales contadas en bodega para cada producto, con opción de identificar lote o número de serie, permitiendo conciliar el stock del sistema con el inventario físico real.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventory';

GO
CREATE NONCLUSTERED INDEX [IX_PhysicalInventory_WarehouseId_ProductId]
    ON [Inventory].[PhysicalInventory]([WarehouseId] ASC, [ProductId] ASC)
    INCLUDE([Id], [Quantity], [BatchSerialId]);
