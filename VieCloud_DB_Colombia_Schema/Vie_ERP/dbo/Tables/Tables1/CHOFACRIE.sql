CREATE TABLE [dbo].[CHOFACRIE] (
    [CODFACRIE] CHAR (3)  NOT NULL,
    [DESFACRIE] CHAR (40) NOT NULL,
    CONSTRAINT [PK_CHOFACRIE] PRIMARY KEY CLUSTERED ([CODFACRIE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del factor de riesgo; texto que identifica y caracteriza el factor de riesgo clínico, epidemiológico o laboral en la atención de salud (comorbilidades, antecedentes, exposiciones, condiciones de vulnerabilidad). Tipo: CHAR(40).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE', @level2type = N'COLUMN', @level2name = N'DESFACRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Factor de Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE', @level2type = N'COLUMN', @level2name = N'DESFACRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE', @level2type = N'COLUMN', @level2name = N'DESFACRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del factor de riesgo; identificador único de 3 caracteres que clasifica y referencia cada factor de riesgo en historiales clínicos, diagnósticos y reportes de atención. PK. Tipo: CHAR(3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE', @level2type = N'COLUMN', @level2name = N'CODFACRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Factor de Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE', @level2type = N'COLUMN', @level2name = N'CODFACRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE', @level2type = N'COLUMN', @level2name = N'CODFACRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos o razones de rechazo de facturación (glosas o causales de no aceptación de facturas). Permite clasificar los motivos por los cuales una factura es rechazada o devuelta en el proceso de cobro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE';
