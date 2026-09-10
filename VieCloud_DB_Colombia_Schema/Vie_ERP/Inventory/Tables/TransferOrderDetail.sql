CREATE TABLE [Inventory].[TransferOrderDetail] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferOrderId]               INT             NOT NULL,
    [InventoryRequestDetailId]      INT             NULL,
    [ProductId]                     INT             NOT NULL,
    [InventoryQuantity]             INT             NOT NULL,
    [Quantity]                      INT             NOT NULL,
    [Value]                         DECIMAL (18, 2) CONSTRAINT [DF_TransferOrderDetail_Value] DEFAULT ((0)) NOT NULL,
    [Description]                   VARCHAR (300)   NULL,
    [InventoryRequestDetailOtherId] INT             NULL,
    CONSTRAINT [PK_TransferOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_KindsAgreements_InventoryRequestOtherDetail] FOREIGN KEY ([InventoryRequestDetailOtherId]) REFERENCES [Inventory].[InventoryRequestDetailOther] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_InventoryRequestDetail] FOREIGN KEY ([InventoryRequestDetailId]) REFERENCES [Inventory].[InventoryRequestDetail] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_TransferOrder] FOREIGN KEY ([TransferOrderId]) REFERENCES [Inventory].[TransferOrder] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_TransferOrderDetail_TransferOrder];




GO



GO



GO



GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_TransferOrderDetail_TransferOrder];


GO
CREATE NONCLUSTERED INDEX [IX_TransferOrderDetail_TransferOrderId_Quantity_Value]
    ON [Inventory].[TransferOrderDetail]([TransferOrderId] ASC)
    INCLUDE([Quantity], [Value]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de detalle de solicitud de inventario alternativa o complementaria (FK a InventoryRequestDetailOther). Referencia a solicitudes de inventario distintas del flujo estándar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailOtherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del otro detalle de solicitdes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailOtherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailOtherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del detalle del traslado (VARCHAR 300). Anotaciones, observaciones o notas sobre el producto o la línea del documento de transferencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo promedio unitario o total del producto trasladado en esta línea (DECIMAL 18,2). Valor monetario de referencia para valuación del inventario en tránsito.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio del producto con el que se traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto a despachar en esta línea del traslado (INT). Número de items que se remiten efectivamente según el documento de transferencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de items que se van a despachar en el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades disponibles en inventario físico al momento de crear el documento de transferencia (INT). Stock existente registrado antes del despacho.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de items que se encuantran en el inventario fisico en el momento en que se crea el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del producto a ser trasladado (INT, FK a InventoryProduct). Referencia al catálogo de productos/medicamentos/insumos del inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto que se va a despachar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de solicitud de inventario que genera esta línea de traslado (INT, FK a InventoryRequestDetail). Vinculación con la solicitud de reabastecimiento original.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de detalle de solicitud de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de transferencia madre a la que pertenece este detalle (INT, FK a TransferOrder). Clave de relación con el encabezado del documento de traslado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de orden de transferencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la línea o detalle de la orden de transferencia (INT, PK). Clave primaria de la tabla TransferOrderDetail.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las órdenes de transferencia de inventario entre bodegas o centros de atención. Registra cada producto solicitado en la transferencia, con sus cantidades, valor y referencias a la solicitud de inventario de origen.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetail';
