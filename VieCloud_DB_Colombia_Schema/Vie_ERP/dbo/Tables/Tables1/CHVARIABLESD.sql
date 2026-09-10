CREATE TABLE [dbo].[CHVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_CHVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHVARIABLESD_CHVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[CHVARIABLES] ([ID]),
    CONSTRAINT [FK_CHVARIABLESD_CHVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[CHVARIABLES] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión lógica o condicional de variable en lista de chequeo (VARCHAR 250), define relación, cálculo o validación entre variable padre e hijo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Expresiones de variables de lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de variable hijo (FK → CHVARIABLES.ID), vinculación a variable dependiente en estructura jerárquica de checklist clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla CHVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de variable padre (FK → CHVARIABLES.ID), establece relación jerárquica padre-hijo en lista de chequeo, permite variables dependientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla CHVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (INT IDENTITY), clave primaria de la tabla de detalles de variables de checklist.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciones entre variables de historia clínica o formularios clínicos dinámicos, donde una variable padre condiciona o agrupa a una variable hijo mediante una expresión lógica o de validación. Usada para definir dependencias, reglas de visibilidad o cálculos entre campos en formularios clínicos parametrizables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD';
