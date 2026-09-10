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



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de producto en fórmula médica (DATETIME). Auditoría de cuándo se asignó el medicamento/insumo a la receta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (VARCHAR 20). Identificación del profesional o sistema que registró el producto en la fórmula médica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto a entregar o dispensar (INT). Dosis, número de comprimidos, ampollas o unidades físicas del medicamento/insumo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad a entregar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén o depósito de origen (INT, FK a Warehouse). Centro de distribución, farmacia o unidad funcional que suministra el producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote o número de serie del producto (INT, FK a BatchSerial, nullable). Trazabilidad: lote específico, fecha de vencimiento, número de serie para auditoría sanitaria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto, medicamento o insumo (INT, FK a InventoryProduct). Código único del fármaco, dispositivo médico o material consumible en la fórmula.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea detalle de la fórmula médica/receta (INT, FK a MedicalFormulaDetail). Vincula el producto a la receta específica del paciente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del la formula detalle', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la asignación de producto en fórmula (INT IDENTITY). Clave primaria que rastrea cada medicamento/insumo en cada receta médica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Productos específicos (medicamentos o insumos) asociados al detalle de una fórmula médica, incluyendo el lote, la bodega de despacho y la cantidad dispensada. Registra el movimiento de inventario generado por cada ítem de una orden o receta médica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailProducts';
