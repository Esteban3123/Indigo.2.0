CREATE TABLE [Portfolio].[RulesDeteriorationClassification] (
    [Id]                            INT     IDENTITY (1, 1) NOT NULL,
    [SettingPortfolioId]            INT     NOT NULL,
    [HealthPortfolioClassification] TINYINT NOT NULL,
    [NiifBook]                      TINYINT NOT NULL,
    [FiscalBook]                    TINYINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RDC_SettingPortfolio] FOREIGN KEY ([SettingPortfolioId]) REFERENCES [Portfolio].[SettingPortfolio] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración del portafolio de cartera (llave foránea a SettingPortfolio). Referencia al encabezado de parámetros y reglas del portafolio de facturación y cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'SettingPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave foránea al encabezado de parámetros del portafolio', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'SettingPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'SettingPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de cartera en salud (TINYINT): 1 = Entidad Responsable de Pago (ERP), 2 = Cliente/Asegurador. Define la categoría del deudor en el portafolio de facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'HealthPortfolioClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación de cartera salud: 1 = Entidad Responsable de Pago, 2 = Cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'HealthPortfolioClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'HealthPortfolioClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Libro contable NIIF para la regla de deterioro de cartera (TINYINT): 1 = Usa Fecha de Radicación de la Factura, 2 = Usa Fecha de Emisión de la Factura. Determina la antigüedad de la deuda en normas NIIF.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'NiifBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Libro NIIF de la regla de deterioro: 1 = Fecha de Radicación de la Factura, 2 = Fecha de Emisión de la Factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'NiifBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'NiifBook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Libro fiscal para la regla de deterioro de cartera (TINYINT): 1 = Usa Fecha de Radicación de la Factura, 2 = Usa Fecha de Emisión de la Factura. Determina el cálculo de provisión y glosa según normativa tributaria.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'FiscalBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Libro fiscal de la regla de deterioro: : 1 = Fecha de Radicación de la Factura, 2 = Fecha de Emisión de la Factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'FiscalBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'FiscalBook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reglas de clasificación por deterioro de cartera: define cómo se clasifica el deterioro de las cuentas por cobrar según la configuración de cartera, el libro NIIF y el libro fiscal, para el cálculo y registro contable del deterioro de la cartera en salud.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de la regla de clasificación por deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'Id';
