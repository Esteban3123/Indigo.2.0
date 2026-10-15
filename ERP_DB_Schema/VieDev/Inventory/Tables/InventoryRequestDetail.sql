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
ALTER TABLE [Inventory].[InventoryRequestDetail] NOCHECK CONSTRAINT [FK_InventoryRequestDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[InventoryRequestDetail] NOCHECK CONSTRAINT [FK_InventoryRequestDetail_InventoryRequest];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 = Registrado, 2 = Confirmado, 3 = anulado, 4 = Rechazada ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta cantidad se registrará con "0" si la unidad funcional de la cabecera está en los parametros de solicitudes  y ademas si está parametrizado como "Requiere autorizacion", Si por el contrario no requiere autorizacion está cantidad debería ser igual a la de la columa "OriginalQuantity"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDetail', @level2type = N'COLUMN', @level2name = N'Quantity';

