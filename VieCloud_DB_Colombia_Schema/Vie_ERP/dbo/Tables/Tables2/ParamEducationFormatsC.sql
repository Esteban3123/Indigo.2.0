CREATE TABLE [dbo].[ParamEducationFormatsC] (
    [Id]                            INT           IDENTITY (1, 1) NOT NULL,
    [Code]                          VARCHAR (3)   NOT NULL,
    [Type]                          BIT           NOT NULL,
    [Obligatory]                    BIT           NULL,
    [DashboardMedico]               BIT           NULL,
    [DashboardEspecialista]         BIT           NULL,
    [DashboardAtencionFarmaceutica] BIT           NULL,
    [DashboardNursing]              BIT           NULL,
    [DashboardTerapia]              BIT           NULL,
    [DashboardServicioApoyo]        BIT           NULL,
    [Triage]                        BIT           NULL,
    [Description]                   VARCHAR (300) NOT NULL,
    [MinAge]                        INT           NOT NULL,
    [UnitMinAge]                    INT           NOT NULL,
    [MaxAge]                        INT           NOT NULL,
    [UnitMaxAge]                    INT           NOT NULL,
    [Sex]                           INT           NOT NULL,
    [Status]                        BIT           NOT NULL,
    [CreationUser]                  CHAR (20)     NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ModificationUser]              CHAR (20)     NULL,
    [ModificationDate]              DATETIME      NULL,
    CONSTRAINT [PK_ParamEducationFormatsC] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del formato educativo o encuesta (DATETIME, auditoría de cambios)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro (CHAR 20, auditoría, clave de acceso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del formato educativo o encuesta (DATETIME, auditoría de registro inicial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del formato educativo o encuesta (CHAR 20, auditoría, clave de acceso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del formato: 1=Activo, 0=Inactivo (BIT, disponibilidad en atención clínica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado. Activo o Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género aplicable: 1=Masculino, 2=Femenino, 3=Ambos (INT, filtro demográfico para elegibilidad)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Masculino  2 - Femenino  3 - Ambos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para edad máxima: 1=Años, 2=Meses, 3=Días (INT, rango etario de aplicación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'UnitMaxAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Años  2 - Meses   3 - Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'UnitMaxAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'UnitMaxAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida para aplicar el formato o encuesta (INT, criterio de elegibilidad clínica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'MaxAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad máxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'MaxAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'MaxAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para edad mínima: 1=Años, 2=Meses, 3=Días (INT, rango etario de aplicación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'UnitMinAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Años  2 - Meses   3 - Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'UnitMinAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'UnitMinAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida para aplicar el formato o encuesta (INT, criterio de elegibilidad clínica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'MinAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad mínima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'MinAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'MinAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o título descriptivo del formato educativo o encuesta (VARCHAR 300, búsqueda semántica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del formato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad del formato en módulo de Triage: 1=Visible, 0=No visible (BIT, control de interfaz clínica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Triage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visibilidad Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Triage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Triage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad en dashboard de Servicios de Apoyo: 1=Visible, 0=No visible (BIT, control de interfaz)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardServicioApoyo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visibilidad DashboardServicioApoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardServicioApoyo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardServicioApoyo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad en dashboard de Terapia: 1=Visible, 0=No visible (BIT, control de interfaz para terapeutas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardTerapia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visibilidad DashboardTerapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardTerapia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardTerapia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad en dashboard de Enfermería: 1=Visible, 0=No visible (BIT, control de interfaz para enfermeras)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardNursing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visibilidad DashboardEnfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardNursing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardNursing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad en dashboard de Atención Farmacéutica: 1=Visible, 0=No visible (BIT, control interfaz farmacéutico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardAtencionFarmaceutica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visibilidad DashboardAtencionFarmaceuta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardAtencionFarmaceutica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardAtencionFarmaceutica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad en dashboard del Especialista: 1=Visible, 0=No visible (BIT, control de interfaz médico especialista)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardEspecialista';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visibilidad DashboardEspecialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardEspecialista';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardEspecialista';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad en dashboard del Médico General: 1=Visible, 0=No visible (BIT, control de interfaz médico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardMedico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visibilidad DashboardMedico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardMedico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardMedico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obligatoriedad cuando Type=0 (encuesta): 1=Obligatoria, 0=Opcional (BIT NULL, criterio de llenado en atención)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Obligatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el tipo es encuesta este campo indica si es obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Obligatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Obligatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento: 1=Formato educativo, 0=Encuesta (BIT, clasificación de instrumento clínico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tipo     1 = Formato de educación  0 = Encuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del formato educativo o encuesta (VARCHAR 3, identificador en reportes y RIPS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Codigo formatos educación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo de la tabla (INT IDENTITY, clave primaria, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración de los formatos educativos clínicos (educación al paciente y/o cuidador). Define qué formularios de educación están disponibles, a qué tipo de atención corresponden, en qué módulos o dashboards del sistema aparecen, y cuáles son las restricciones de edad y sexo para su aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC';
