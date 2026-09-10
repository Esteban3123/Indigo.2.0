CREATE TABLE [dbo].[EXAVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_EXAVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EXAVARIABLESD_EXAVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[EXAVARIABLES] ([ID]),
    CONSTRAINT [FK_EXAVARIABLESD_EXAVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[EXAVARIABLES] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión o fórmula de cálculo (VARCHAR 500) que define la relación lógica o matemática entre variable padre e hijo en exámenes/pruebas diagnósticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Expresion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la variable hijo/dependiente en tabla EXAVARIABLES; representa la variable resultante o calculada en la relación jerárquica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacionado con ID de tabla EXAVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la variable padre/independiente en tabla EXAVARIABLES; representa la variable base o antecedente en la relación jerárquica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacionado con ID de tabla EXAVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT, PK) de la relación variable padre-hijo en exámenes diagnósticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciones jerárquicas entre variables de exámenes o evaluaciones clínicas, donde cada registro define la expresión o fórmula que vincula una variable padre con una variable hija. Se usa para construir árboles de dependencia o cálculos compuestos en formularios de evaluación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD';
