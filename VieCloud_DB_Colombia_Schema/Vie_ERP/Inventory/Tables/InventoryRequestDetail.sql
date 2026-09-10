CREATE TABLE [Inventory].[InventoryRequestDetail] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryRequestId]  INT           NOT NULL,
    [InventoryProductId]  INT           NOT NULL,
    [Quantity]            INT           NOT NULL,
    [OutstandingQuantity] INT           NOT NULL,
    [Description]         VARCHAR (300) NULL,
    [Status]              TINYINT       CONSTRAINT [DF__Inventory__Statu__63D370DE] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_InventoryRequestDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryRequestDetail_InventoryProduct] FOREIGN KEY ([InventoryProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_InventoryRequestDetail_InventoryRequest] FOREIGN KEY ([InventoryRequestId]) REFERENCES [Inventory].[InventoryRequest] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle (TINYINT, Default=1): 1=Registrado, 2=Confirmado, 3=Anulado, 4=Rechazado. Controla el ciclo de vida de cada línea de la solicitud de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1 = Registrado, 2 = Confirmado, 3 = anulado, 4 = Rechazada ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 300) del detalle de solicitud. Notas, especificaciones, observaciones o referencias adicionales del producto solicitado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente de despacho (INT). Inicia igual a Quantity en la creación; disminuye con cada orden de despacho/envío del detalle. Rastrea lo aún no entregado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea una solicitud este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una orden de despacho del la solicitud en cuestion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada (INT) del producto. Registra ''''0'''' si la unidad funcional requiere autorización previa; en caso contrario, iguala OriginalQuantity. Define el volumen inicial del pedido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta cantidad se registrará con "0" si la unidad funcional de la cabecera está en los parametros de solicitudes  y ademas si está parametrizado como "Requiere autorizacion", Si por el contrario no requiere autorizacion está cantidad debería ser igual a la de la columa "OriginalQuantity"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del producto de inventario solicitado. Referencia a Inventory.InventoryProduct. Vincula el artículo, insumo o medicamento específico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del producto de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la solicitud de inventario padre. Referencia a Inventory.InventoryRequest. Agrupa múltiples productos en una misma solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de solicitud de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT) del detalle de solicitud de inventario. Clave primaria de la tabla InventoryRequestDetail.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada ítem solicitado en una requisición de inventario: indica qué producto se pidió, cuántas unidades se solicitaron, cuántas unidades están pendientes de despacho y el estado actual de ese renglón de la solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail';
