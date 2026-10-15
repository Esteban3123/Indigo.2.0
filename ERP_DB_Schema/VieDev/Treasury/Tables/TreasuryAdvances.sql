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

