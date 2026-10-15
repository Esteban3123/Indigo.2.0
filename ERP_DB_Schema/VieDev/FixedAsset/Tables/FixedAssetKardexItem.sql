CREATE TABLE [FixedAsset].[FixedAssetKardexItem] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MovementType]          TINYINT       NOT NULL,
    [PhysicalAssetId]       INT           NOT NULL,
    [PreviousResponsibleId] INT           NULL,
    [ResponsibleId]         INT           NOT NULL,
    [PreviousLocationId]    INT           NULL,
    [LocationId]            INT           NOT NULL,
    [DocumentDate]          DATETIME      NOT NULL,
    [EntityId]              INT           NOT NULL,
    [EntityCode]            VARCHAR (20)  NOT NULL,
    [EntityName]            VARCHAR (250) NOT NULL,
    [ImportedEntityId]      INT           NULL,
    [ImportedEntityCode]    VARCHAR (20)  NULL,
    [ImportedEntityName]    VARCHAR (250) NULL,
    [AffectPhysical]        BIT           CONSTRAINT [DF_FixedAssetKardexItem_AffectInventory] DEFAULT ((1)) NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    CONSTRAINT [PK_FixedAssetKardexItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetKardexItem_FixedAssetLocation] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_FixedAssetKardexItem_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetKardexItem_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetKardexItem] NOCHECK CONSTRAINT [FK_FixedAssetKardexItem_FixedAssetLocation];


GO
ALTER TABLE [FixedAsset].[FixedAssetKardexItem] NOCHECK CONSTRAINT [FK_FixedAssetKardexItem_FixedAssetPhysicalAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetKardexItem] NOCHECK CONSTRAINT [FK_FixedAssetKardexItem_FixedAssetResponsible];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de movimiento  1 - Entrada  2 - Salida', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetKardexItem', @level2type = N'COLUMN', @level2name = N'MovementType';

