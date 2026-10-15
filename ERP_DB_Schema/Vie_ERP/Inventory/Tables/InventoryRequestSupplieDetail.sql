CREATE TABLE [Inventory].[InventoryRequestSupplieDetail] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryRequestId]  INT           NOT NULL,
    [InventorySupplieId]  INT           NOT NULL,
    [Quantity]            INT           NOT NULL,
    [OutstandingQuantity] INT           NOT NULL,
    [Description]         VARCHAR (300) NULL,
    CONSTRAINT [PK_InventoryRequestSupplieDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryRequestSupplieDetail_InventoryRequest] FOREIGN KEY ([InventoryRequestId]) REFERENCES [Inventory].[InventoryRequest] ([Id]),
    CONSTRAINT [FK_InventoryRequestSupplieDetail_InventorySupplie] FOREIGN KEY ([InventorySupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryRequestSupplieDetail] NOCHECK CONSTRAINT [FK_InventoryRequestSupplieDetail_InventoryRequest];


GO
ALTER TABLE [Inventory].[InventoryRequestSupplieDetail] NOCHECK CONSTRAINT [FK_InventoryRequestSupplieDetail_InventorySupplie];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la solicitud realizada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestSupplieDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad pendiente del insumo, la cantidad inicial es igual a la cantidad solicitada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestSupplieDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad solicituda del insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestSupplieDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único de la tabla de insumos Inventory.InventorySupplie', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestSupplieDetail', @level2type = N'COLUMN', @level2name = N'InventorySupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único de la cabecera de la solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestSupplieDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único del detalle de la solicitud de insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestSupplieDetail', @level2type = N'COLUMN', @level2name = N'Id';

