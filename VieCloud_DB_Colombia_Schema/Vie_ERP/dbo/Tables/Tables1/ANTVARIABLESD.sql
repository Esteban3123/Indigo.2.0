CREATE TABLE [dbo].[ANTVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_ANTVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ANTVARIABLESD_ANTVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[ANTVARIABLES] ([ID]),
    CONSTRAINT [FK_ANTVARIABLESD_ANTVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[ANTVARIABLES] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión lógica o fórmula (VARCHAR 500). Guarda la variable, fórmula condicional, o expresión SQL que define cómo se relacionan o calculan los antecedentes entre variable padre e hijo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Variable de la tabla ANTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de variable hijo (INT, FK a ANTVARIABLES.ID). Referencia a la variable dependiente o variable hijo que se relaciona condicionalmente con la variable padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ANTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de variable padre (INT, FK a ANTVARIABLES.ID). Referencia a la variable antecedente o variable padre que condiciona o estructura la relación jerárquica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda ID de la tabla ANTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (INT, PK). Número secuencial que identifica cada registro de relación entre variables padre e hijo en la tabla de antecedentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las relaciones jerárquicas entre variables de antecedentes clínicos, vinculando una variable padre con sus variables hijo mediante una expresión o condición que define su dependencia o regla de evaluación en formularios de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD';
