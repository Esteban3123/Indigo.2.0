CREATE TABLE [Treasury].[PaymentMethods] (
    [Id]                           INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashReceipt]                INT                                                                       NOT NULL,
    [PaymentMethodTypes]           TINYINT                                                                   NOT NULL,
    [Value]                        DECIMAL (18, 2)                                                           NOT NULL,
    [IdBank]                       INT                                                                       NULL,
    [CheckNumber]                  VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Check_Ofuscado", 0)')   NULL,
    [DepositDate]                  DATETIME                                                                  NULL,
    [IdEntityBankAccount]          INT                                                                       NULL,
    [DepositNumber]                VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Deposit_Ofuscado", 0)') NULL,
    [DepositType]                  TINYINT                                                                   NULL,
    [IdCard]                       INT                                                                       NULL,
    [CardNumber]                   VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Card_Ofuscado", 0)')    NULL,
    [BaseValue]                    DECIMAL (18, 2)                                                           NULL,
    [CommissionValue]              DECIMAL (18, 2)                                                           NULL,
    [PercentageCommission]         DECIMAL (5, 2)                                                            NULL,
    [RTFValue]                     DECIMAL (18, 2)                                                           NULL,
    [PercentageRTF]                DECIMAL (5, 2)                                                            NULL,
    [ICAValue]                     DECIMAL (18, 2)                                                           NULL,
    [PercentageICA]                DECIMAL (5, 2)                                                            NULL,
    [IdCostCenter]                 INT                                                                       NULL,
    [CurrencyId]                   INT                                                                       NULL,
    [TRM]                          NUMERIC (20, 5)                                                           NULL,
    [ValueInCurrencyHeader]        NUMERIC (20, 5)                                                           CONSTRAINT [DF__PaymentMe__Value__017A790C] DEFAULT ((0)) NOT NULL,
    [IdAgreementsRedemptionPoints] INT                                                                       NULL,
    [TransactionNumber]            VARCHAR (30)                                                              NULL,
    [TransactionDate]              DATETIME                                                                  NULL,
    [RedemptionPoints]             INT                                                                       NULL,
    CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentMethods_AgreementsRedemptionPoints] FOREIGN KEY ([IdAgreementsRedemptionPoints]) REFERENCES [Treasury].[AgreementsRedemptionPoints] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Bank] FOREIGN KEY ([IdBank]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Cards] FOREIGN KEY ([IdCard]) REFERENCES [Treasury].[Cards] ([Id]),
    CONSTRAINT [FK_PaymentMethods_CashReceipts] FOREIGN KEY ([IdCashReceipt]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_PaymentMethods_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PaymentMethods_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_AgreementsRedemptionPoints];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_Bank];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_Cards];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_CashReceipts];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_CostCenter];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_Currency];


GO
ALTER TABLE [Treasury].[PaymentMethods] NOCHECK CONSTRAINT [FK_PaymentMethods_EntityBankAccounts];


GO
CREATE NONCLUSTERED INDEX [IX_PaymentMethods_IdCashReceipt]
    ON [Treasury].[PaymentMethods]([IdCashReceipt] ASC)
    INCLUDE([PaymentMethodTypes], [Value]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de puntos a redimir', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'RedemptionPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la transaccion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TransactionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la transaccion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TransactionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato de puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdAgreementsRedemptionPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor convertido a la moneda de la cabecera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor del trm de la moneda al que fue convertido el valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Reteica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Reteica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'ICAValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del retefuente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la retefuente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de la comision', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageCommission';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la comisión', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CommissionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor de la base con el que se obtiene el valor de la comision', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de la tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de consignación: 1 = Local, 2 = Nacional', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de consignación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de consignación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CheckNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de formas de pago: 1 = Efectivo, 2 = Cheque, 3 = Tarjeta, 4  = Consignación, 5 = Redencion de puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCashReceipt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'Id';

