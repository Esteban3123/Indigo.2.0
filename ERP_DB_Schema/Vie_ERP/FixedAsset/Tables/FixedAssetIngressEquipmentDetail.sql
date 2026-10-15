CREATE TABLE [FixedAsset].[FixedAssetIngressEquipmentDetail] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetIngressEquipment] INT          NOT NULL,
    [IdEquipment]                  INT          NOT NULL,
    [LicensePlate]                 VARCHAR (50) NOT NULL,
    [Serie]                        VARCHAR (50) NOT NULL,
    [IdReponsible]                 INT          NOT NULL,
    [IdFunctionalUnit]             INT          NOT NULL,
    [IdLocation]                   INT          NOT NULL,
    [AdquisitionDate]              DATE         NOT NULL,
    [Depreciate]                   BIT          NOT NULL,
    [ComponentDepreciate]          BIT          NULL,
    CONSTRAINT [PK_FixedAssetIngressEquipmentDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deprecia por Componente (Si - No)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deprecia (Si - No)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Ubicación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Reponsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdReponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Serie', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Equipo de entrada de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngressEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';

