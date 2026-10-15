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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica ya se le realizo la devolucion al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'Refund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id.  estado del activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'StatusAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de vencimiento de la garantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el item maneja garantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deprecia (1 Si - 0 No)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Ubicación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'LocationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Reponsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'ReponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Serie', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'Plate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de elemento de entrada de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItemDetail', @level2type = N'COLUMN', @level2name = N'Id';

