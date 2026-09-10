CREATE TABLE [dbo].[RSGRUEXPRES] (
    [ID]         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCGRUPO]  INT           NOT NULL,
    [EXPRESION]  VARCHAR (MAX) NOT NULL,
    [NOMRIESGO]  VARCHAR (500) NOT NULL,
    [DESRIESGO]  VARCHAR (MAX) NOT NULL,
    [PLANRIESGO] VARCHAR (MAX) NOT NULL,
    [FECHREGIS]  DATETIME      NOT NULL,
    CONSTRAINT [PK_RSGRUEXPRES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSGRUEXPRES_RSGRUPO] FOREIGN KEY ([IDHCGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de generación/registro del criterio de riesgo en el sistema; auditoria de creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Generación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan de acción o protocolo a seguir (VARCHAR MAX) cuando la expresión valida; recomendaciones clínicas, intervenciones, derivaciones según el riesgo detectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'PLANRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plan a seguir de acuerdo a lo que evalua la expresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'PLANRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'PLANRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada (VARCHAR MAX) del riesgo clínico que evalúa la expresión; contexto clínico y consecuencias potenciales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'DESRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del riesgo que evalua la expresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'DESRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'DESRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o etiqueta del riesgo evaluado (VARCHAR 500); término clínico para búsqueda semántica de tipos de riesgo (complicación, contraindicación, evento adverso).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'NOMRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del riesgo que evalua la expresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'NOMRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'NOMRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión lógica o condicional (VARCHAR MAX) a validar para detectar riesgo clínico en historia clínica o datos de paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Expresion a validar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) referenciando ID en RSGRUPO; identificador del grupo de revisión clínica por sistema/aparato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'IDHCGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo de revisión por sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'IDHCGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'IDHCGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la expresión de riesgo en el grupo de evaluación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de expresiones o reglas de evaluación de riesgos clínicos asociadas a grupos de historia clínica. Guarda el nombre, descripción, plan de manejo y la expresión lógica que define cada riesgo identificado en el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUEXPRES';
