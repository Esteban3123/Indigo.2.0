CREATE TABLE [Maintenance].[EquipmentTypeFeatureDetail] (
    [Id]              INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentType] INT         NOT NULL,
    [TagControl]      VARCHAR (3) NOT NULL,
    CONSTRAINT [PK_EquipmentTypeFeatureDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypeFeatureDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentTypeFeatureDetail] NOCHECK CONSTRAINT [FK_EquipmentTypeFeatureDetail_EquipmentType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tag del control que se debe mostrar en el registro del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'TagControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de equipo relacionado a la caracteristica tecnica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo d ela tabla de relacion entre Caracteristicas y Tipo de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeFeatureDetail', @level2type = N'COLUMN', @level2name = N'Id';

