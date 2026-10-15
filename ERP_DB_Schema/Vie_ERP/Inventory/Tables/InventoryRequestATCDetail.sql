CREATE TABLE [Inventory].[InventoryRequestATCDetail] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryRequestId]  INT           NOT NULL,
    [ATCId]               INT           NOT NULL,
    [Quantity]            INT           NOT NULL,
    [OutstandingQuantity] INT           NOT NULL,
    [Description]         VARCHAR (300) NULL,
    CONSTRAINT [PK_InventoryRequestATCDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryrequestATCDetail_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_InventoryRequestATCDetail_InventoryRequest] FOREIGN KEY ([InventoryRequestId]) REFERENCES [Inventory].[InventoryRequest] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryRequestATCDetail] NOCHECK CONSTRAINT [FK_InventoryrequestATCDetail_ATC];


GO
ALTER TABLE [Inventory].[InventoryRequestATCDetail] NOCHECK CONSTRAINT [FK_InventoryRequestATCDetail_InventoryRequest];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la solicitud realizada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestATCDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad pendiente del medicamento, la cantidad inicial es igual a la cantidad solicitada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestATCDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad solicituda del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestATCDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único de la tabla de medicamentos Inventory.ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestATCDetail', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único de la cabecera de la solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestATCDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único del detalle de la solicitud de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestATCDetail', @level2type = N'COLUMN', @level2name = N'Id';

