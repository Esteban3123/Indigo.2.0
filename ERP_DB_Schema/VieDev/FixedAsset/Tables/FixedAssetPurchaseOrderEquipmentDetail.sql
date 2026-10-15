CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipmentDetail] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetPurchaseOrderEquipment] INT          NOT NULL,
    [IdEquipment]                        INT          NOT NULL,
    [LicensePlate]                       VARCHAR (50) NOT NULL,
    [Serie]                              VARCHAR (50) NOT NULL,
    [IdResponsible]                      INT          NOT NULL,
    [IdFunctionalUnit]                   INT          NOT NULL,
    [IdLocation]                         INT          NOT NULL,
    [AdquisitionDate]                    DATE         NOT NULL,
    [Depreciate]                         BIT          NOT NULL,
    [ComponentDepreciate]                BIT          NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderEquipmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_FixedAssetResponsible] FOREIGN KEY ([IdResponsible]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_FunctionalUnit] FOREIGN KEY ([IdFunctionalUnit]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_Location] FOREIGN KEY ([IdLocation]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipmentDetail] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipmentDetail] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_FunctionalUnit];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipmentDetail] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_Location];

