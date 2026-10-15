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
ALTER TABLE [Inventory].[LoanMerchandiseDetail] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetail] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetail_LoanMerchandise];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea un prestamo este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio que tenia el producto al momento de ejecutar el ajuste del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cantidad del producto sin importar los lotes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del prestamo de mercancia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetail', @level2type = N'COLUMN', @level2name = N'Id';

