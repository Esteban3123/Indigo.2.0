CREATE TABLE [FixedAsset].[InputRemissionEquipment] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdInputRemission]      INT           NOT NULL,
    [RemissionSource]       TINYINT       NOT NULL,
    [SourceCode]            VARCHAR (20)  NULL,
    [PurchaseOrderDetailId] INT           NULL,
    [IdInventoryType]       INT           NOT NULL,
    [IdEquipmentType]       INT           NOT NULL,
    [IdEquipment]           INT           NOT NULL,
    [IdIvaCode]             INT           NOT NULL,
    [Quantity]              INT           NOT NULL,
    [ProductValue]          NUMERIC (18)  NOT NULL,
    [TotalValue]            NUMERIC (18)  NOT NULL,
    [TaxesValue]            NUMERIC (18)  NOT NULL,
    [IdTrademark]           INT           NOT NULL,
    [Model]                 VARCHAR (100) NOT NULL,
    [IdPolize]              INT           NOT NULL,
    CONSTRAINT [PK_InputRemissionEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InputRemissionEquipment_InventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[InputRemissionEquipment] NOCHECK CONSTRAINT [FK_InputRemissionEquipment_InventoryType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del equipo de remisión de entrada ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipment', @level2type = N'COLUMN', @level2name = N'Id';

