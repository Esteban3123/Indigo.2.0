CREATE TABLE [Maintenance].[EquipmentTypeMeasurementUnitDetail] (
    [Id]                                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdTechnicalLogMeasurementUnitDetail] INT NOT NULL,
    [IdEquipmentType]                     INT NOT NULL,
    CONSTRAINT [PK_EquipmentTypeMeasurementUnitDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypeMeasurementUnitDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_EquipmentTypeMeasurementUnitDetail_EquipmentTypeMeasurementUnitDetail1] FOREIGN KEY ([IdTechnicalLogMeasurementUnitDetail]) REFERENCES [Maintenance].[EquipmentTypeMeasurementUnitDetail] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentTypeMeasurementUnitDetail] NOCHECK CONSTRAINT [FK_EquipmentTypeMeasurementUnitDetail_EquipmentType];


GO
ALTER TABLE [Maintenance].[EquipmentTypeMeasurementUnitDetail] NOCHECK CONSTRAINT [FK_EquipmentTypeMeasurementUnitDetail_EquipmentTypeMeasurementUnitDetail1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de equipo relacionado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida asociada en el registro tecnico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLogMeasurementUnitDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de Unidades de Medida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeMeasurementUnitDetail', @level2type = N'COLUMN', @level2name = N'Id';

