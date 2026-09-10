CREATE TABLE [dbo].[ADDISCAPACI] (
    [DISCCODIGO] CHAR (3)    NOT NULL,
    [DISCDESCRI] CHAR (100)  NULL,
    [ESTADO]     INT         NULL,
    [DISCTIPO]   VARCHAR (3) CONSTRAINT [DF__ADDISCAPA__DISCT__368272D1] DEFAULT ('') NULL,
    CONSTRAINT [PK_ADDISCAPACI] PRIMARY KEY CLUSTERED ([DISCCODIGO] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de discapacidad: 1=Activo, 2=Inactivo. Determina si la clasificación de discapacidad está disponible para uso en atenciones y registros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1-Activo  2-Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la discapacidad o condición de incapacidad. Texto que especifica el tipo de discapacidad (física, sensorial, cognitiva, psíquica, múltiple) para documentación médica y legal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'DISCDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Discapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'DISCDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'DISCDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la discapacidad (3 caracteres). Identificador primario que clasifica tipos de discapacidad en el sistema de atención, usado en historias clínicas, RIPS y reportes de pacientes con capacidades especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'DISCCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Discapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'DISCCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'DISCCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Discapacidad física = 01, Discapacidad visual = 02, Discapacidad auditiva = 03, Discapacidad intelectual = 04, Discapacidad sicosocial = 05, Sordoceguera = 06, Discapacidad múltiple = 07, Sin discapacidad = 08   
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'DISCTIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de discapacidad asociados a pacientes. Registra los códigos y descripciones de las diferentes discapacidades que pueden ser asignadas en la historia clínica o admisión del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI';
