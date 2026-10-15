CREATE TABLE [Maintenance].[EquipmentTypeTechnicalLog] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentTypeId] TINYINT NOT NULL,
    [TechnicalLogId]  TINYINT NOT NULL,
    CONSTRAINT [PK_EquipmentTypeTechnicalLog__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypeTechnicalLog_EquipmentTypeTechnicalLog] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [Maintenance].[EquipmentType] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentTypeTechnicalLog] NOCHECK CONSTRAINT [FK_EquipmentTypeTechnicalLog_EquipmentTypeTechnicalLog];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de registro técnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'TechnicalLogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'Id';

