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
ALTER TABLE [Treasury].[TreasuryAdvances] NOCHECK CONSTRAINT [FK_TreasuryAdvances_Currency];


GO
ALTER TABLE [Treasury].[TreasuryAdvances] NOCHECK CONSTRAINT [FK_TreasuryAdvances_VoucherTransactionDetails];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TreasuryAdvances__IdVoucherTransactionDetail]
    ON [Treasury].[TreasuryAdvances]([IdVoucherTransactionDetail] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del anticipo en la moneda de la cabecera del comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRM De la transaccion de la moneda', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda de la caja o cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del Anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del detalle del comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryAdvances', @level2type = N'COLUMN', @level2name = N'Id';

