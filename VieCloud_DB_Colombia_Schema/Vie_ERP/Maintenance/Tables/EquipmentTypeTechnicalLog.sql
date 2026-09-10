CREATE TABLE [Maintenance].[EquipmentTypeTechnicalLog] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentTypeId] TINYINT NOT NULL,
    [TechnicalLogId]  TINYINT NOT NULL,
    CONSTRAINT [PK_EquipmentTypeTechnicalLog__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypeTechnicalLog_EquipmentTypeTechnicalLog] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [Maintenance].[EquipmentType] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro técnico de mantenimiento. Referencia a log técnico que documenta inspecciones, reparaciones, calibraciones o intervenciones realizadas en equipos médicos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'TechnicalLogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de registro técnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'TechnicalLogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'TechnicalLogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo médico o clínico asociado. Clave foránea que enlaza a la tabla EquipmentType para categorizar equipos como: monitores, ventiladores, bombas infusoras, equipos de laboratorio, imagenología u otros dispositivos de centro de atención.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincrementable (INT IDENTITY). Identificador único de la relación entre tipo de equipo y registro técnico de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los tipos de equipo médico o técnico con sus bitácoras o registros técnicos de mantenimiento, permitiendo saber qué log de mantenimiento corresponde a cada tipo de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeTechnicalLog';
