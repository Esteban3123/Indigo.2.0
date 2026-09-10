CREATE TABLE [Inventory].[InventoryRequestDetailOther] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryRequestId]       INT           NOT NULL,
    [ComponentType]            TINYINT       NOT NULL,
    [ATCId]                    INT           NULL,
    [SupplieId]                INT           NULL,
    [InventoryProductId]       INT           NULL,
    [Quantity]                 INT           NOT NULL,
    [OutstandingQuantity]      INT           NOT NULL,
    [Description]              VARCHAR (300) NULL,
    [Status]                   TINYINT       CONSTRAINT [DF__Inventory__Statu__64C79517] DEFAULT ((1)) NOT NULL,
    [OriginalQuantity]         INT           CONSTRAINT [DF__Inventory__Origi__6AC2C794] DEFAULT ((0)) NULL,
    [UserConfirmAuthorization] VARCHAR (20)  NULL,
    CONSTRAINT [PK_InventoryRequestDetailOther] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryRequestDetailOther_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_InventoryRequestDetailOther_InventoryProduct] FOREIGN KEY ([InventoryProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_InventoryRequestDetailOther_InventoryRequest] FOREIGN KEY ([InventoryRequestId]) REFERENCES [Inventory].[InventoryRequest] ([Id]),
    CONSTRAINT [FK_InventoryRequestDetailOther_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó o autorizó el detalle de la solicitud de inventario (VARCHAR 20, PII: identificación de profesional)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'UserConfirmAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirmación de autorización del usuario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'UserConfirmAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'UserConfirmAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad original solicitada en el detalle antes de ajustes o despachos parciales (INT, valor por defecto 0)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'OriginalQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aquí se registra la cantidad original de la solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'OriginalQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'OriginalQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle: 1=Registrado, 2=Confirmado, 3=Anulado (TINYINT, default 1)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1 = Registrado, 2 = Confirmado, 3 = anulado ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional o notas del artículo solicitado en esta línea de detalle (VARCHAR 300)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente o sin despachar del artículo; inicia igual a Quantity y disminuye con cada orden de despacho (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea una solicitud este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una orden de despacho del la solicitud en cuestion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total solicitada del medicamento, insumo o producto en este detalle (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto de inventario genérico asociado (FK a Inventory.InventoryProduct, INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'InventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del producto de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'InventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'InventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del insumo o suministro específico solicitado (FK a Inventory.InventorySupplie, INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del producto del insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del medicamento por clasificación ATC (Anatomical Therapeutic Chemical) (FK a Inventory.ATC, INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del producto del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de componente: 1=Medicamento (ATC), 2=Insumo (Supplie), 3=Producto genérico (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 : Medicamento  2 : Insumo  3 : Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la solicitud de inventario padre a la cual pertenece este detalle (FK a Inventory.InventoryRequest, INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de solicitud de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico del detalle de solicitud de inventario (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de ítems adicionales u otros componentes incluidos en una solicitud de inventario, registrando medicamentos por código ATC, insumos o productos de inventario solicitados con sus cantidades, estado de despacho y usuario que autorizó la confirmación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther';
