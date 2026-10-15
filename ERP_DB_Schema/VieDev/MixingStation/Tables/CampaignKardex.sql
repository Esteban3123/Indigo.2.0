CREATE TABLE [MixingStation].[CampaignKardex] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]  INT             NOT NULL,
    [MovementType]      TINYINT         NOT NULL,
    [MovementDate]      DATETIME        NOT NULL,
    [EntityName]        VARCHAR (100)   NOT NULL,
    [Description]       VARCHAR (500)   NOT NULL,
    [ProductId]         INT             NOT NULL,
    [BatchSerialId]     INT             NULL,
    [Quantity]          DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId] INT             NOT NULL,
    [CreationUser]      VARCHAR (20)    NOT NULL,
    [CreationDate]      DATETIME        NOT NULL,
    CONSTRAINT [PK_CampaignKardex] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignKardex_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_CampaignKardex_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_CampaignKardex_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_CampaignKardex_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignKardex] NOCHECK CONSTRAINT [FK_CampaignKardex_BatchSerial];


GO
ALTER TABLE [MixingStation].[CampaignKardex] NOCHECK CONSTRAINT [FK_CampaignKardex_CampaignDetail];


GO
ALTER TABLE [MixingStation].[CampaignKardex] NOCHECK CONSTRAINT [FK_CampaignKardex_MeasurementUnit];


GO
ALTER TABLE [MixingStation].[CampaignKardex] NOCHECK CONSTRAINT [FK_CampaignKardex_Product];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de movimiento  1 - Entrada  2 - Salida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'MovementType';

