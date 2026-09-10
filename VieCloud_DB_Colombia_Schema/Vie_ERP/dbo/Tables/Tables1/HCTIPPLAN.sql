CREATE TABLE [dbo].[HCTIPPLAN] (
    [CODPLANIF] CHAR (2)      NOT NULL,
    [DESPLANIF] VARCHAR (100) NULL,
    [INDAUDFOR] NUMERIC (18)  NOT NULL,
    CONSTRAINT [PK_HCTIPPLAN] PRIMARY KEY CLUSTERED ([CODPLANIF] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador o código de auditoría; flag booleano (0/1) que marca si el tipo de planificación requiere auditoría o control de calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de planificación; texto que explica la naturaleza, propósito o categoría de la planificación (ej: planificación de atención, urgencia, electiva, preventiva)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'DESPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Tipo de Planificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'DESPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'DESPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de planificación; identificador único de 2 caracteres que clasifica la modalidad de planificación en atención o gestión sanitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'CODPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Planificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'CODPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'CODPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de planificación de historia clínica. Cataloga las categorías o modalidades de plan de atención clínica que pueden asignarse en la historia del paciente (por ejemplo, plan de manejo, plan de cuidados, plan quirúrgico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN';
