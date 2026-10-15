CREATE TABLE [Inventory].[MedicalFormulaDetailProducts] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaDetailId] INT          NOT NULL,
    [ProductId]              INT          NOT NULL,
    [BatchSerialId]          INT          NULL,
    [WarehouseId]            INT          NOT NULL,
    [Quantity]               INT          NOT NULL,
    [CreationUser]           VARCHAR (20) NULL,
    [CreationDate]           DATETIME     NULL,
    CONSTRAINT [PK_MedicalFormulaDetailProducts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaDetailProducts_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_MedicalFormulaDetailProducts_InventoryProducts] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_MedicalFormulaDetailProducts_MedicalFormulaDetail] FOREIGN KEY ([MedicalFormulaDetailId]) REFERENCES [Inventory].[MedicalFormulaDetail] ([Id]),
    CONSTRAINT [FK_MedicalFormulaDetailProducts_WareHouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[MedicalFormulaDetailProducts] NOCHECK CONSTRAINT [FK_MedicalFormulaDetailProducts_BatchSerial];


GO
ALTER TABLE [Inventory].[MedicalFormulaDetailProducts] NOCHECK CONSTRAINT [FK_MedicalFormulaDetailProducts_InventoryProducts];


GO
ALTER TABLE [Inventory].[MedicalFormulaDetailProducts] NOCHECK CONSTRAINT [FK_MedicalFormulaDetailProducts_MedicalFormulaDetail];


GO
ALTER TABLE [Inventory].[MedicalFormulaDetailProducts] NOCHECK CONSTRAINT [FK_MedicalFormulaDetailProducts_WareHouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad a entregar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del la formula detalle', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'Id';

