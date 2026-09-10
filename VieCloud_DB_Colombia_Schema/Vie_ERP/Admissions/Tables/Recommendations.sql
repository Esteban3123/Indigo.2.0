CREATE TABLE [Admissions].[Recommendations] (
    [CODCENATE]      VARCHAR (10)  NOT NULL,
    [NOMCENATE]      VARCHAR (100) NULL,
    [DASHACADEMIC]   BIT           NULL,
    [DASHMEDIC]      BIT           NULL,
    [DASHSPECIALIST] BIT           NULL,
    [DASHENFERM]     BIT           NULL,
    [SUPPORTSERVICE] BIT           NULL,
    [DASHTHERAPY]    BIT           NULL,
    CONSTRAINT [PK__Recommen__23952C637ACE1CED] PRIMARY KEY CLUSTERED ([CODCENATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita dashboard de terapia física, ocupacional y rehabilitación para el centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHTHERAPY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dashboard de terapia (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHTHERAPY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHTHERAPY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita servicio de soporte administrativo, logístico o técnico en el centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'SUPPORTSERVICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio de soporte (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'SUPPORTSERVICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'SUPPORTSERVICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita dashboard de enfermería para gestión de cuidados y atenciones de pacientes', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHENFERM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dashboard enfermeria (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHENFERM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHENFERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita dashboard de especialistas médicos para seguimiento de consultas y procedimientos especializados', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHSPECIALIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dashboard especialista (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHSPECIALIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHSPECIALIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita dashboard médico para visualización de atenciones, diagnósticos y evolución del paciente', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHMEDIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dashboard médico (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHMEDIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHMEDIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita dashboard académico para docencia, investigación y formación en el centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHACADEMIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dashboard acadenico (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHACADEMIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHACADEMIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social del centro de atención, clínica, hospital, IPS o unidad funcional de salud', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'NOMCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'NOMCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'NOMCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador VARCHAR(10) del centro de atención, clínica u IPS. Llave primaria de la tabla Recommendations', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de recomendaciones o habilitaciones por centro de atención, indicando qué tipos de tableros o módulos (académico, médico, especialista, enfermería, servicio de soporte, terapia) están activos para cada sede o centro.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations';
