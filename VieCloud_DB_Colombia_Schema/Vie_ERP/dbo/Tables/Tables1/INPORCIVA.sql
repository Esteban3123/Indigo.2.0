CREATE TABLE [dbo].[INPORCIVA] (
    [CODGOIVA]  CHAR (2)     NOT NULL,
    [DESCRIIVA] VARCHAR (80) NOT NULL,
    [PORCEIVA]  NUMERIC (18) NULL,
    [TARIFIVA]  NUMERIC (3)  NULL,
    CONSTRAINT [PK_INPORCIVA] PRIMARY KEY CLUSTERED ([CODGOIVA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifa del IVA, código numérico de hasta 3 dígitos que categoriza o clasifica el tipo de alícuota de impuesto al valor agregado para aplicación automática en procesos de facturación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'TARIFIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tarifa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'TARIFIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'TARIFIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del IVA, valor numérico (0-100) que representa la alícuota o tasa de impuesto al valor agregado a aplicar en transacciones de venta, facturación y cálculo de valores finales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'PORCEIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procentaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'PORCEIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'PORCEIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del IVA, texto que explica el nombre o tipo de categoría de impuesto al valor agregado (ej: IVA 0%, IVA 5%, IVA 19%) usado en documentos de cobro y reportes fiscales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'DESCRIIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion IVA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'DESCRIIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'DESCRIIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del IVA (Impuesto al Valor Agregado), clave primaria alfanumérica de 2 caracteres que identifica de forma única cada tarifa o categoría de IVA aplicable en facturación y RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'CODGOIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'CODGOIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'CODGOIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tarifas y porcentajes de IVA (Impuesto al Valor Agregado) aplicables en la facturación. Contiene los diferentes grupos o tipos de IVA con su descripción, porcentaje e identificador de tarifa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA';
