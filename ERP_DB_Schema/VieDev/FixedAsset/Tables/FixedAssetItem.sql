CREATE TABLE [FixedAsset].[FixedAssetItem] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                           VARCHAR (20)    NOT NULL,
    [Description]                    VARCHAR (300)   NOT NULL,
    [ItemTypeId]                     INT             NOT NULL,
    [ItemCatalogId]                  INT             NOT NULL,
    [IVAId]                          INT             NOT NULL,
    [LastCostItem]                   NUMERIC (18)    NOT NULL,
    [Observations]                   VARCHAR (300)   NULL,
    [FairValue]                      DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetItem_FairValue] DEFAULT ((0)) NOT NULL,
    [AllowDepreciate]                BIT             CONSTRAINT [DF_FixedAssetItem_AllowDepreciate] DEFAULT ((1)) NOT NULL,
    [Status]                         BIT             NOT NULL,
    [CreationUser]                   VARCHAR (20)    NOT NULL,
    [CreationDate]                   DATETIME        NOT NULL,
    [ModificationUser]               VARCHAR (20)    NULL,
    [ModificationDate]               DATETIME        NULL,
    [TimeStamp]                      ROWVERSION      NOT NULL,
    [DepreciateByTimeUse]            BIT             CONSTRAINT [DF_FixedAssetItem_DepreciateByTimeUse] DEFAULT ((0)) NOT NULL,
    [Amortizes]                      BIT             CONSTRAINT [DF__FixedAsse__Amort__7866DB75] DEFAULT ((0)) NOT NULL,
    [CatalogOfPropertyandServicesId] INT             NULL,
    CONSTRAINT [PK_FixedAssetItem__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Equipment_EquipmentType] FOREIGN KEY ([ItemTypeId]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipment_FixedAssetEquipmentCatalog] FOREIGN KEY ([ItemCatalogId]) REFERENCES [FixedAsset].[FixedAssetItemCatalog] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipment_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_FixedAssetItem_FixedAssetCatalogOfPropertyandServices] FOREIGN KEY ([CatalogOfPropertyandServicesId]) REFERENCES [FixedAsset].[FixedAssetCatalogOfPropertyandServices] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItem] NOCHECK CONSTRAINT [FK_Equipment_EquipmentType];


GO
ALTER TABLE [FixedAsset].[FixedAssetItem] NOCHECK CONSTRAINT [FK_FixedAssetEquipment_FixedAssetEquipmentCatalog];


GO
ALTER TABLE [FixedAsset].[FixedAssetItem] NOCHECK CONSTRAINT [FK_FixedAssetEquipment_GeneralLedgerIVA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del catalogo de bienes y servicios', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CatalogOfPropertyandServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Amortiza | 1 = Si | 0 = No |', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Amortizes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si permite depreciar    Si permite depreciar se debe de llenar obligatoriamente el detalle de esta tabla como minimo con el libro oficial', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'AllowDepreciate';

