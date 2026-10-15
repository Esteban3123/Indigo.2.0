CREATE TABLE [Inventory].[KardexControl] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentDate]            DATETIME      NOT NULL,
    [ThirdPartyId]            INT           NULL,
    [ProductId]               INT           NOT NULL,
    [WarehouseId]             INT           NOT NULL,
    [BatchSerialId]           INT           NULL,
    [MovementType]            TINYINT       NOT NULL,
    [Quantity]                INT           NOT NULL,
    [PreviousAmountProduct]   INT           NOT NULL,
    [PreviousAmountWarehouse] INT           NOT NULL,
    [PreviousAmountBatch]     INT           NOT NULL,
    [EntityId]                INT           NOT NULL,
    [EntityCode]              VARCHAR (20)  NOT NULL,
    [EntityName]              VARCHAR (250) NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    CONSTRAINT [PK_KardexControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_KardexControl_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_KardexControl_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_KardexControl_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_KardexControl_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[KardexControl] NOCHECK CONSTRAINT [FK_KardexControl_BatchSerial];


GO
ALTER TABLE [Inventory].[KardexControl] NOCHECK CONSTRAINT [FK_KardexControl_InventoryProduct];


GO
ALTER TABLE [Inventory].[KardexControl] NOCHECK CONSTRAINT [FK_KardexControl_ThirdParty];


GO
ALTER TABLE [Inventory].[KardexControl] NOCHECK CONSTRAINT [FK_KardexControl_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de movimiento  1 - Entrada  2 - Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexControl', @level2type = N'COLUMN', @level2name = N'MovementType';

