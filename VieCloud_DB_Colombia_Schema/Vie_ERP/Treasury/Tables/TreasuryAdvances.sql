CREATE TABLE [Treasury].[TreasuryAdvances] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherTransactionDetail] INT             NOT NULL,
    [Detail]                     VARCHAR (500)   NOT NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__TreasuryA__Curre__79D95744] DEFAULT ((1)) NULL,
    [TRMValue]                   NUMERIC (20, 5) CONSTRAINT [DF__TreasuryA__TRMVa__7BC19FB6] DEFAULT ((1)) NOT NULL,
    [ValueInCurrencyHeader]      DECIMAL (18, 2) CONSTRAINT [DF__TreasuryA__Value__7F92309A] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TreasuryAdvances__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryAdvances_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_TreasuryAdvances_VoucherTransactionDetails] FOREIGN KEY ([IdVoucherTransactionDetail]) REFERENCES [Treasury].[VoucherTransactionDetails] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TreasuryAdvances__IdVoucherTransactionDetail]
    ON [Treasury].[TreasuryAdvances]([IdVoucherTransactionDetail] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto del anticipo convertido a la moneda de la cabecera del comprobante de egreso (DECIMAL 18,2). Resultado de Value × TRMValue para consolidación financiera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del anticipo en la moneda de la cabecera del comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de Cambio Representativa del Mercado (TRM) vigente al momento de la transacción (NUMERIC 20,5). Convierte Value a moneda de cabecera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TRM De la transaccion de la moneda', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda (FK a Common.Currency) de la caja o cuenta bancaria donde se registra el anticipo. Ej: COP, USD', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda de la caja o cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto del anticipo en moneda transaccional (DECIMAL 18,2). Valor base antes de conversión o ajuste por TRM', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o concepto del anticipo (VARCHAR 500), explica motivo, beneficiario o referencia del desembolso anticipado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del Anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el detalle del comprobante de egreso (VoucherTransactionDetails). Vincula el anticipo a la transacción de tesorería original', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del detalle del comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de anticipo en tesorería', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anticipos o adelantos de tesorería registrados como líneas de detalle dentro de un comprobante de transacción financiera. Cada registro representa un concepto de anticipo con su valor, moneda y tasa de cambio (TRM) aplicada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances';
