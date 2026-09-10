CREATE TABLE [Maintenance].[TechnicalLogDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentRegistration] INT          NOT NULL,
    [IdTechnicalLog]          INT          NOT NULL,
    [Name]                    VARCHAR (50) NOT NULL,
    [ValueMin]                VARCHAR (10) NULL,
    [ValueMax]                VARCHAR (10) NULL,
    [IdMeasurementUnit]       INT          NOT NULL,
    CONSTRAINT [PK_TechnicalLogDetailed] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalLogDetail_EquipmentRegistration] FOREIGN KEY ([IdEquipmentRegistration]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id]),
    CONSTRAINT [FK_TechnicalLogDetail_TechnicalLog] FOREIGN KEY ([IdTechnicalLog]) REFERENCES [Maintenance].[TechnicalLog] ([Id]),
    CONSTRAINT [FK_TechnicalLogDetailed_MeasurementUnit] FOREIGN KEY ([IdMeasurementUnit]) REFERENCES [Maintenance].[MeasurementUnit] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (Kg, L, mmHg, °C, V, Hz, etc.). Referencia FK a [Maintenance].[MeasurementUnit]. Define la escala de ValueMin y ValueMax.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida asociada.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo registrado o tolerancia máxima del parámetro técnico medido. VARCHAR(10). Complementa el rango aceptable del parámetro monitoreado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'ValueMax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del detalle del registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'ValueMax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'ValueMax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor mínimo registrado o tolerancia mínima del parámetro técnico medido. VARCHAR(10). Puede ser numérico o especificación técnica del equipamiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'ValueMin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del detalle del registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'ValueMin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'ValueMin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del parámetro técnico medido en el detalle del registro (ej: temperatura, presión, voltaje, frecuencia). VARCHAR(50). Identifica qué se midió.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del detalle del registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro técnico cabecera/maestro de mantenimiento. Referencia FK a [Maintenance].[TechnicalLog]. Agrupa múltiples detalles de mediciones en un evento de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro tecnico cabecera asociado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdTechnicalLog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la recepción/registro del equipo biomédico asociado al detalle técnico. Referencia FK a [Maintenance].[EquipmentRegistration]. Vincula el detalle con el equipo específico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la recepcion del equipo asociado  a el detalle del registro tecnico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del detalle del registro técnico de mantenimiento de equipos (INT IDENTITY). Clave primaria.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico del detalle de registro tecnicop', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los parámetros o variables registradas en el log técnico de un equipo, incluyendo los rangos de valores permitidos y la unidad de medida correspondiente a cada variable monitoreada durante el mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalLogDetail';
