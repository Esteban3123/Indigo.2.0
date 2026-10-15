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
ALTER TABLE [Portfolio].[PortfolioAdvanceExchangeRate] NOCHECK CONSTRAINT [FK_PortfolioAdvanceExchangeRate_Currency];


GO
ALTER TABLE [Portfolio].[PortfolioAdvanceExchangeRate] NOCHECK CONSTRAINT [FK_PortfolioAdvanceExchangeRate_PortfolioAdvance];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor reversado al tipo de cambio del anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor adignado al tipo de cambio del anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del anticipo realizado a la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioAdvanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';

