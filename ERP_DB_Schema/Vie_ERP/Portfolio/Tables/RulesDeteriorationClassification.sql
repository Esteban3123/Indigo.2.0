CREATE TABLE [Portfolio].[RulesDeteriorationClassification] (
    [Id]                                INT             IDENTITY (1, 1) NOT NULL,
    [SettingPortfolioId]                INT             NOT NULL,
    [HealthPortfolioClassification]     TINYINT         NOT NULL,
    [NiifBook]                          TINYINT         NOT NULL,
    [FiscalBook]                        TINYINT         NOT NULL,
    CONSTRAINT [PK_RulesDeteriorationClassification__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RDC_SettingPortfolio] FOREIGN KEY ([SettingPortfolioId]) REFERENCES [Portfolio].[SettingPortfolio] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foránea al encabezado de parámetros del portafolio', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'SettingPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación de cartera salud: 1 = Entidad Responsable de Pago, 2 = Cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'HealthPortfolioClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Libro NIIF de la regla de deterioro: 1 = Fecha de Radicación de la Factura, 2 = Fecha de Emisión de la Factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'NiifBook';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Libro fiscal de la regla de deterioro: 1 = Fecha de Radicación de la Factura, 2 = Fecha de Emisión de la Factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RulesDeteriorationClassification', @level2type = N'COLUMN', @level2name = N'FiscalBook';


