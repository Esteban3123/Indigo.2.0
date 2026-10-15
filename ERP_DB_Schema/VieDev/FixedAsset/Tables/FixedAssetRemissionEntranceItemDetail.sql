CREATE TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionEntranceItemId] INT          NOT NULL,
    [Plate]                   VARCHAR (50) NOT NULL,
    [Serie]                   VARCHAR (50) NOT NULL,
    [ResponsibleId]           INT          NOT NULL,
    [LocationId]              INT          NOT NULL,
    [AdquisitionDate]         DATE         NOT NULL,
    [Depreciate]              BIT          NOT NULL,
    [HandlesWarranty]         BIT          NOT NULL,
    [WarrantyExpirationDate]  DATE         NULL,
    [StatusAssetId]           INT          NOT NULL,
    CONSTRAINT [PK_InputRemissionEquipmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetRemissionEntranceItemDetail_FixedAssetStatusAsset] FOREIGN KEY ([StatusAssetId]) REFERENCES [FixedAsset].[FixedAssetStatusAsset] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_InputRemissionEquipmentDetail] FOREIGN KEY ([RemissionEntranceItemId]) REFERENCES [FixedAsset].[FixedAssetRemissionEntranceItem] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_Location] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_Responsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetRemissionEntranceItemDetail_FixedAssetStatusAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetail] NOCHECK CONSTRAINT [FK_InputRemissionEquipmentDetail_InputRemissionEquipmentDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetail] NOCHECK CONSTRAINT [FK_InputRemissionEquipmentDetail_Location];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetail] NOCHECK CONSTRAINT [FK_InputRemissionEquipmentDetail_Responsible];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deprecia (1 - SI, 0 - NO)    Nota: este campo depende del AllowDeprecate que se encuentra en el articulo ya que si alla esta marcado como no permite depreciar aca tambien debe ir siempre 0, pero si alla esta marcado como que si permite depreciar entonces aca es opcional por el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';

