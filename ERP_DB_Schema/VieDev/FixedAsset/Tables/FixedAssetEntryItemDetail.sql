CREATE TABLE [FixedAsset].[FixedAssetEntryItemDetail] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryItemId]  INT          NOT NULL,
    [Plate]                  VARCHAR (50) NOT NULL,
    [Serie]                  VARCHAR (50) NOT NULL,
    [ReponsibleId]           INT          NOT NULL,
    [LocationId]             INT          NOT NULL,
    [AdquisitionDate]        DATE         NOT NULL,
    [Depreciate]             BIT          NOT NULL,
    [HandlesWarranty]        BIT          NOT NULL,
    [WarrantyExpirationDate] DATE         NULL,
    [StatusAssetId]          INT          NOT NULL,
    [Refund]                 BIT          CONSTRAINT [DF_FixedAssetEntryItemDetail_Refund] DEFAULT ((0)) NOT NULL,
    [ValidSmallerAmount]     BIT          CONSTRAINT [DF__FixedAsse__Valid__264DA1B6] DEFAULT ((1)) NOT NULL,
    [Amortize]               BIT          CONSTRAINT [DF__FixedAsse__Amort__2D84AF2A] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetIngressEquipmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryItemDetail_FixedAssetResponsible] FOREIGN KEY ([ReponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItemDetail_FixedAssetStatusAsset] FOREIGN KEY ([StatusAssetId]) REFERENCES [FixedAsset].[FixedAssetStatusAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipmentDetail_FixedAssetIngressEquipment] FOREIGN KEY ([FixedAssetEntryItemId]) REFERENCES [FixedAsset].[FixedAssetEntryItem] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipmentDetail_Location] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetEntryItemDetail_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetEntryItemDetail_FixedAssetStatusAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipmentDetail_FixedAssetIngressEquipment];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipmentDetail_Location];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Amortiza:   0 - No  1 - Si', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'Amortize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Valida Menor Cuantia SI. NO.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'ValidSmallerAmount';

