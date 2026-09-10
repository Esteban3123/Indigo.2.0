CREATE TABLE [Payments].[AccountPayableExchangeRate] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [AccountPayableId] INT             NOT NULL,
    [CurrencyId]       INT             NOT NULL,
    [Value]            NUMERIC (20, 5) NOT NULL,
    [ValueReverse]     NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_AccountPayableExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableExchangeRate_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_AccountPayableExchangeRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [IX_AccountPayableExchangeRate] UNIQUE NONCLUSTERED ([AccountPayableId] ASC, [CurrencyId] ASC)
);


GO
ALTER TABLE [Payments].[AccountPayableExchangeRate] NOCHECK CONSTRAINT [FK_AccountPayableExchangeRate_AccountPayable];




GO
ALTER TABLE [Payments].[AccountPayableExchangeRate] NOCHECK CONSTRAINT [FK_AccountPayableExchangeRate_AccountPayable];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio inversa: moneda de esta tabla (CurrencyId) → moneda de la CxP. Recíproco matemático de Value. Ejemplo: CxP en USD, tabla en CRC = 0.00153. NUMERIC(20,5) para conversiones precisas en ambas direcciones.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la CxC   Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio directa: moneda de la CxP → moneda de esta tabla (CurrencyId). Ejemplo: CxP en USD, tabla en CRC (1 USD = 650 CRC) = 650. NUMERIC(20,5) de precisión monetaria.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda del anticipo contra la moneda de esta tabla (CurrencyId)  Ejemplo CxC en dollares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda de destino o referencia (FK a [Common].[Currency]). Define la divisa contra la cual se calcula la tasa de cambio de la CxP.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Cuenta por Pagar (CxP, Accounts Payable). FK a [Payments].[AccountPayable]. Vincula el registro de tasa de cambio a la factura, deuda o obligación de pago específica.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de la tabla AccountPayableExchangeRate. Clave primaria que indexa cada registro de tasa de cambio.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las tasas de cambio de moneda aplicadas a cada cuenta por pagar, permitiendo gestionar el valor de las deudas en divisas extranjeras y su equivalencia en moneda local.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate';
