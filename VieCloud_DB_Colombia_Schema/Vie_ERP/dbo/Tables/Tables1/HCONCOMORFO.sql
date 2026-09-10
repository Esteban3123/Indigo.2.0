CREATE TABLE [dbo].[HCONCOMORFO] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]     INT           NULL,
    [CODMORFOLO]  VARCHAR (10)  NOT NULL,
    [DESCRIPCION] VARCHAR (100) NULL,
    CONSTRAINT [PK_HCONCOMORFO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCONCOMORFO_HCONCOMORFO] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[HCONCOMORFO] ([ID])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCONCOMORFO]
    ON [dbo].[HCONCOMORFO]([CODMORFOLO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo morfológico o clasificación anatómica. Texto descriptivo del registro morfológico para reportes clínicos, diagnósticos y análisis patológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion morfologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del registro morfológico. Identificador único alfanumérico de la clasificación morfológica, diagnóstico anatomopatológico o hallazgo morfológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'CODMORFOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo registro morfologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'CODMORFOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'CODMORFOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación jerárquica padre-hijo en la tabla. Estructura arbórea que permite agrupar morfologías en categorías y subcategorías de clasificación anatómica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion con la presente tabla - estructura jerarquíca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincremental único (clave primaria) de cada registro morfológico en la tabla HCONCOMORFO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo jerárquico de morfologías (hallazgos morfológicos) utilizado en historia clínica. Permite clasificar y codificar características morfológicas de diagnósticos o patologías, organizadas en estructura padre-hijo para agrupación por categorías.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOMORFO';
