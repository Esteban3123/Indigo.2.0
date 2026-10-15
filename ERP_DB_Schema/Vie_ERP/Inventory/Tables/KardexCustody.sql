CREATE TABLE [Inventory].[KardexCustody] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MovementType]            TINYINT         NOT NULL,
    [ThirdPartyId]            INT             NULL,
    [WarehouseId]             INT             NOT NULL,
    [ProductId]               INT             NOT NULL,
    [BatchSerialId]           INT             NULL,
    [DocumentDate]            DATETIME        NOT NULL,
    [Quantity]                INT             NOT NULL,
    [Value]                   DECIMAL (18, 2) NOT NULL,
    [PreviousCost]            DECIMAL (18, 2) NOT NULL,
    [AverageCost]             DECIMAL (18, 2) NOT NULL,
    [PreviousAverageCost]     DECIMAL (18, 2) NOT NULL,
    [PreviousAmountProduct]   INT             NOT NULL,
    [PreviousAmountWarehouse] INT             NOT NULL,
    [PreviousAmountBatch]     INT             NOT NULL,
    [EntityId]                INT             NOT NULL,
    [EntityCode]              VARCHAR (20)    NOT NULL,
    [EntityName]              VARCHAR (250)   NOT NULL,
    [ImportedEntityId]        INT             NULL,
    [ImportedEntityCode]      VARCHAR (20)    NULL,
    [ImportedEntityName]      VARCHAR (250)   NULL,
    [AffectInventory]         BIT             CONSTRAINT [DF_KardexCustody_AffectInventory] DEFAULT ((1)) NOT NULL,
    [CreationUser]            VARCHAR (20)    NOT NULL,
    [CreationDate]            DATETIME        NOT NULL,
    [AdmissionNumber]         CHAR (10)       NULL,
    CONSTRAINT [PK_KardexCustody__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_KardexCustody_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_KardexCustody_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_KardexCustody_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_KardexCustody_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[KardexCustody] NOCHECK CONSTRAINT [FK_KardexCustody_BatchSerial];


GO
ALTER TABLE [Inventory].[KardexCustody] NOCHECK CONSTRAINT [FK_KardexCustody_InventoryProduct];


GO
ALTER TABLE [Inventory].[KardexCustody] NOCHECK CONSTRAINT [FK_KardexCustody_ThirdParty];


GO
ALTER TABLE [Inventory].[KardexCustody] NOCHECK CONSTRAINT [FK_KardexCustody_Warehouse];


GO
CREATE NONCLUSTERED INDEX [IX_KardexCustody__ThirdPartyId]
    ON [Inventory].[KardexCustody]([ThirdPartyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_KardexCustody__ProductId]
    ON [Inventory].[KardexCustody]([ProductId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_KardexCustody__BatchSerialId]
    ON [Inventory].[KardexCustody]([BatchSerialId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_KardexCustody__WarehouseId]
    ON [Inventory].[KardexCustody]([WarehouseId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de admisión', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el usuario que creo el movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el movimiento del KardexCustody afecto el inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'AffectInventory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el nombre de la entidad de la cual fue importada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'ImportedEntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo de la entidad de la cual fue importada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'ImportedEntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la entidad de la cual fue importado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'ImportedEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad anterior que habia del producto en el lote que se va a registrar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'PreviousAmountBatch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad anterior que habia pero solo en el almacen que se esta registrando', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'PreviousAmountWarehouse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad anterior del producto, este se calcula sumando la cantidad de productos que hay en todos los almacenes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'PreviousAmountProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor anterior del ultimo costo promedio, es decir el ultimo costo promedio antes de que se ejecutara el movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'PreviousAverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del costo promedio, El valor del nuevo costo promedio incluyendo el movimiento actual    Cuando el movimiento es de tipo salida este campo va en 0', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del ultimo costo, Este campo especifica cual era el ultimo costo (Valor por el que se habia comprado), antes de que se ejecutara este movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'PreviousCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando es Entrada este campo es el Costo del producto cuando se realizo el movimiento, es decir que si es una compra este seria el valor por el que se compro    Cuando es de tipo de salida este campo es el valor de la venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote o serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de movimiento  1 - Entrada  2 - Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'MovementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del KardexCustody', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexCustody', @level2type = N'COLUMN', @level2name = N'Id';

