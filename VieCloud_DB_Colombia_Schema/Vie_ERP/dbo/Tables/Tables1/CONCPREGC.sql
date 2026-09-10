CREATE TABLE [dbo].[CONCPREGC] (
    [IDCONCPRE] TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONCPREGU] VARCHAR (380) NOT NULL,
    [CONTIPRES] TINYINT       NOT NULL,
    [COESPECIA] BIT           NOT NULL,
    [CONMEDICO] BIT           NOT NULL,
    [COENFERME] BIT           NOT NULL,
    [CONCAPOYO] BIT           NOT NULL,
    CONSTRAINT [PK_CONCPREGC] PRIMARY KEY CLUSTERED ([IDCONCPRE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si la pregunta está dirigida a personal de apoyo o administrativo en centros de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONCAPOYO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pregunta Para Apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONCAPOYO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONCAPOYO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que identifica si la pregunta es aplicable a enfermeras, enfermeros o personal de enfermería en procesos clínicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'COENFERME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pregunta Para Emfermer@', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'COENFERME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'COENFERME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que señala si la pregunta corresponde a médicos, doctores o profesionales médicos en evaluaciones de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pregunta Para Médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que indica si la pregunta es exclusiva para especialistas o profesionales con especialidad médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'COESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pregunta Para Especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'COESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'COESPECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de respuesta esperada (TINYINT): 0=Selección Múltiple, 1=Selección Única; define el formato de respuesta en formularios de consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONTIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de respuesta 0-Selección Multiple, 1-Selección Única', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONTIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONTIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de la pregunta (VARCHAR 380) formulada en procesos de atención, consulta clínica, ingreso o evaluación de pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONCPREGU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TextoPregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONCPREGU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'CONCPREGU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (TINYINT IDENTITY) de la pregunta de consulta; clave primaria de la tabla CONCPREGC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preguntas o ítems configurables para encuestas o formularios de consulta clínica, indicando a qué tipo de profesional aplica cada pregunta (médico, enfermería, especialista, apoyo diagnóstico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGC';
