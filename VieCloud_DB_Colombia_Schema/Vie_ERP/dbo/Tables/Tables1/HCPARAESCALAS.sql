CREATE TABLE [dbo].[HCPARAESCALAS] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]             VARCHAR (3)   NOT NULL,
    [NOMBREESCALA]       VARCHAR (MAX) NOT NULL,
    [SEXO]               INT           NOT NULL,
    [EDADMIN]            INT           NOT NULL,
    [EDADMAX]            INT           NOT NULL,
    [DASMEDICO]          BIT           NULL,
    [DASENFERMERIA]      BIT           NULL,
    [DASTERAPIAS]        BIT           NULL,
    [DASACADEMICO]       BIT           NULL,
    [DASESPECIALISTAS]   BIT           NULL,
    [DASSERVICIOAPOYO]   BIT           NULL,
    [TRIAGE]             BIT           NULL,
    [PharmaceuticalCare] BIT           NULL,
    CONSTRAINT [PK_HCPARAESCALAS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla si la escala clínica se muestra en el formulario de Triage de urgencias/triaje para clasificación de pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'TRIAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar escala en el formulario de Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'TRIAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'TRIAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla la visibilidad de la escala en el Dashboard del Servicio de Apoyo (laboratorio, imagenología, farmacia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASSERVICIOAPOYO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visible en Dashboard servicio de apoyo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASSERVICIOAPOYO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASSERVICIOAPOYO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla la visibilidad de la escala en el Dashboard de Especialistas médicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASESPECIALISTAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visible en Dashboard especialistas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASESPECIALISTAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASESPECIALISTAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla la visibilidad de la escala en el Dashboard Académico para docencia e investigación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASACADEMICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visible en Dashboard academico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASACADEMICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASACADEMICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla la visibilidad de la escala en el módulo de Terapias (fisioterapia, ocupacional, fonoaudiología)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASTERAPIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visible en Terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASTERAPIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASTERAPIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla la visibilidad de la escala en el Dashboard de Enfermería para seguimiento de pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASENFERMERIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visible en Dashboard enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASENFERMERIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASENFERMERIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que controla la visibilidad de la escala en el Dashboard Médico para evaluación clínica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visible en Dashboard medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'DASMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida en meses para aplicar la escala de valoración (INT, rango etario superior)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad maxima en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida en meses para aplicar la escala de valoración (INT, rango etario inferior)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad minima en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo del paciente al cual aplica la escala: 1=Masculino, 2=Femenino, 3=Ambos sexos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo  1 - masculino  2 - Femenino  3 - Ambos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la escala clínica o de evaluación (VARCHAR MAX, ej: Escala de Glasgow, APGAR, Morse)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'NOMBREESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el nombre de la escala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'NOMBREESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'NOMBREESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de identificación de la escala (VARCHAR 3, identificador único corto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la escala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la tabla, clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros y configuración de las escalas clínicas de valoración utilizadas en historia clínica, incluyendo los criterios de aplicación por sexo, rango de edad y los perfiles asistenciales habilitados para diligenciarlas (médico, enfermería, terapias, especialistas, entre otros).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la escala puede ser diligenciada por el perfil de atención farmacéutica (pharmaceutical care / químico farmacéutico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'PharmaceuticalCare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAESCALAS', @level2type = N'COLUMN', @level2name = N'PharmaceuticalCare';
