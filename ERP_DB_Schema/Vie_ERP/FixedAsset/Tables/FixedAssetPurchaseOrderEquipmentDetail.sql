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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Componente depreciacón', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id ubicación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Unidad funcional ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de serie', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id. Equipo de orden de compra de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrderEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';

