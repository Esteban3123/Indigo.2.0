CREATE TABLE [dbo].[NTVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_NTVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTVARIABLESD_NTVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[NTVARIABLES] ([ID]),
    CONSTRAINT [FK_NTVARIABLESD_NTVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[NTVARIABLES] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión o fórmula de validación almacenada (VARCHAR 250). Contiene la lógica, condición o regla a evaluar en el contexto de variables dependientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la expresión a validar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la variable dependiente o subordinada que referencia a NTVARIABLES.ID. Variable cuyo valor depende de la relación padre-hijo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla NTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la variable independiente o superior que referencia a NTVARIABLES.ID. Variable controladora en la relación jerárquica padre-hijo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla NTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (INT IDENTITY). Clave primaria de la relación entre variables padre-hijo en NTVARIABLESD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciones jerárquicas entre variables clínicas o de parametrización, donde cada registro define un vínculo padre-hijo con una expresión o fórmula asociada. Permite construir árboles de dependencia entre variables para formularios, reglas clínicas o cálculos dinámicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD';
