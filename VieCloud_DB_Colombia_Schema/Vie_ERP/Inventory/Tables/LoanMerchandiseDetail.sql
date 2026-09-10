CREATE TABLE [Inventory].[LoanMerchandiseDetail] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseId]   INT             NOT NULL,
    [ProductId]           INT             NOT NULL,
    [Quantity]            INT             NOT NULL,
    [UnitValue]           NUMERIC (18, 2) NOT NULL,
    [OutstandingQuantity] INT             CONSTRAINT [DF_LoanMerchandiseDetail_OutstandingQuantity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_LoanMerchandiseDetail_ValidateOutstandingQuantity] CHECK ([OutstandingQuantity]>=(0)),
    CONSTRAINT [FK_LoanMerchandiseDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDetail_LoanMerchandise] FOREIGN KEY ([LoanMerchandiseId]) REFERENCES [Inventory].[LoanMerchandise] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetail] NOCHECK CONSTRAINT [CK_LoanMerchandiseDetail_ValidateOutstandingQuantity];




GO
ALTER TABLE [Inventory].[LoanMerchandiseDetail] NOCHECK CONSTRAINT [CK_LoanMerchandiseDetail_ValidateOutstandingQuantity];


GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente de devolución (INT, ≥0) del artículo prestado. Inicia igual a Quantity cuando se crea el préstamo; disminuye con cada devolución parcial o total hasta llegar a cero. Rastrea el saldo no devuelto del préstamo de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea un prestamo este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo unitario promedio (NUMERIC 18,2) del producto al momento del ajuste o ejecución del préstamo. Valor monetario histórico para cálculo de valuación de inventario y trazabilidad de costos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio que tenia el producto al momento de ejecutar el ajuste del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total (INT) del producto prestado sin considerar lotes o series. Cantidad inicial de unidades de inventario movidas en el préstamo de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cantidad del producto sin importar los lotes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del producto o artículo inventariado. Clave foránea que referencia Inventory.InventoryProduct(Id), vincula el producto específico al detalle del préstamo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del préstamo de mercancía padre. Clave foránea que referencia Inventory.LoanMerchandise(Id), agrupa todos los productos asociados a un mismo préstamo o movimiento de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del prestamo de mercancia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de detalle del préstamo de mercancía en la tabla LoanMerchandiseDetail. Clave primaria que indexa cada línea de artículo prestado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los ítems incluidos en un préstamo de mercancía: registra cada producto, la cantidad entregada en préstamo, su valor unitario y la cantidad pendiente de devolución o liquidación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail';
