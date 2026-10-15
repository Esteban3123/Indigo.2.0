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
ALTER TABLE [Inventory].[InventoryRequestDetailOther] NOCHECK CONSTRAINT [FK_InventoryRequestDetailOther_ATC];


GO
ALTER TABLE [Inventory].[InventoryRequestDetailOther] NOCHECK CONSTRAINT [FK_InventoryRequestDetailOther_InventoryProduct];


GO
ALTER TABLE [Inventory].[InventoryRequestDetailOther] NOCHECK CONSTRAINT [FK_InventoryRequestDetailOther_InventoryRequest];


GO
ALTER TABLE [Inventory].[InventoryRequestDetailOther] NOCHECK CONSTRAINT [FK_InventoryRequestDetailOther_InventorySupplie];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirmación de autorización del usuario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'UserConfirmAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aquí se registra la cantidad original de la solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'OriginalQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 = Registrado, 2 = Confirmado, 3 = anulado ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea una solicitud este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una orden de despacho del la solicitud en cuestion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del producto de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'InventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del producto del insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del producto del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 : Medicamento  2 : Insumo  3 : Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de solicitud de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetailOther', @level2type = N'COLUMN', @level2name = N'Id';

