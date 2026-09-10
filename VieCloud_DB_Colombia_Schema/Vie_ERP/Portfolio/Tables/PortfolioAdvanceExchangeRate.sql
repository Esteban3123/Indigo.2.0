CREATE TABLE [Portfolio].[PortfolioAdvanceExchangeRate] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [PortfolioAdvanceId] INT             NOT NULL,
    [CurrencyId]         INT             NOT NULL,
    [Value]              NUMERIC (20, 5) NOT NULL,
    [ValueReverse]       NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_PortfolioAdvanceExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioAdvanceExchangeRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PortfolioAdvanceExchangeRate_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [IX_PortfolioAdvanceExchangeRate] UNIQUE NONCLUSTERED ([PortfolioAdvanceId] ASC, [CurrencyId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 20,5) del tipo de cambio inverso o reversado del anticipo. Representa la tasa de conversión de la moneda base hacia la moneda de referencia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor reversado al tipo de cambio del anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 20,5) del tipo de cambio directo asignado al anticipo. Representa la tasa de conversión de la moneda hacia la moneda base.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor adignado al tipo de cambio del anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la moneda oficial o divisa en la que se registra el tipo de cambio. Referencia FK a Common.Currency. Permite asociar tasas de cambio para múltiples monedas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del anticipo realizado a la cuenta por cobrar (CxC). Referencia FK a Portfolio.PortfolioAdvance. Usado para vincular la tasa de cambio al anticipo específico.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anticipo realizado a la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de tipo de cambio asociado al anticipo de cartera. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las tasas de cambio (tasa de conversión de moneda) aplicadas a los anticipos de cartera, permitiendo convertir valores entre diferentes divisas para el manejo financiero de anticipos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate';
