CREATE TABLE [Treasury].[DischargeBill] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherTransactionD]         INT             NOT NULL,
    [IdAccountPayable]              INT             NOT NULL,
    [IdAccountPayableShare]         INT             NOT NULL,
    [AdvancedValue]                 DECIMAL (18, 2) NOT NULL,
    [AdvancePercent]                DECIMAL (5, 2)  NOT NULL,
    [IdPaymentConcept]              INT             NOT NULL,
    [BaseValueDiscount]             DECIMAL (18, 2) NOT NULL,
    [DiscountPercent]               DECIMAL (5, 2)  NOT NULL,
    [PaymentOrderValue]             DECIMAL (18)    CONSTRAINT [DF_DischargeBill_PaymentOrderValue] DEFAULT ((0)) NOT NULL,
    [ValueInCurrencyHeader]         DECIMAL (18, 5) CONSTRAINT [DF__Discharge__Value__2641E967] DEFAULT ((0)) NULL,
    [TRMValue]                      NUMERIC (20, 5) CONSTRAINT [DF__Discharge__TRMVa__27360DA0] DEFAULT ((1)) NOT NULL,
    [ValueDiscountInCurrencyHeader] DECIMAL (18, 5) CONSTRAINT [DF__Discharge__Value__291E5612] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_DischargeBill] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DischargeBill_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_DischargeBill_AccountPayableShares] FOREIGN KEY ([IdAccountPayableShare]) REFERENCES [Payments].[AccountPayableShares] ([Id]),
    CONSTRAINT [FK_DischargeBill_TreasuryPaymentConcept] FOREIGN KEY ([IdPaymentConcept]) REFERENCES [Treasury].[TreasuryPaymentConcepts] ([Id]),
    CONSTRAINT [FK_DischargeBill_VoucherTransactionD] FOREIGN KEY ([IdVoucherTransactionD]) REFERENCES [Treasury].[VoucherTransactionDetails] ([Id])
);


GO
ALTER TABLE [Treasury].[DischargeBill] NOCHECK CONSTRAINT [FK_DischargeBill_AccountPayable];


GO
ALTER TABLE [Treasury].[DischargeBill] NOCHECK CONSTRAINT [FK_DischargeBill_AccountPayableShares];


GO
ALTER TABLE [Treasury].[DischargeBill] NOCHECK CONSTRAINT [FK_DischargeBill_TreasuryPaymentConcept];


GO
ALTER TABLE [Treasury].[DischargeBill] NOCHECK CONSTRAINT [FK_DischargeBill_VoucherTransactionD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de descuento en la moneda de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'ValueDiscountInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el valor en el tipo de la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en la moneda de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';

