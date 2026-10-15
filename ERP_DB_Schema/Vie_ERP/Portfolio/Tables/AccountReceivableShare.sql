CREATE TABLE [Portfolio].[AccountReceivableShare] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId]        INT             NOT NULL,
    [Number]                     INT             NOT NULL,
    [ExpiredDate]                DATETIME        NOT NULL,
    [Value]                      NUMERIC (18, 2) NOT NULL,
    [Balance]                    NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_Balance] DEFAULT ((0)) NOT NULL,
    [DebitValue]                 NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_DebitValue] DEFAULT ((0)) NOT NULL,
    [CreditValue]                NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_CreditValue] DEFAULT ((0)) NOT NULL,
    [TransferValue]              NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_TransferValue] DEFAULT ((0)) NOT NULL,
    [PaymentValue]               NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_PaymentValue] DEFAULT ((0)) NOT NULL,
    [CrossingValue]              NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_CrossingValue] DEFAULT ((0)) NOT NULL,
    [InterestPaymentDate]        DATETIME        NULL,
    [InterestValue]              NUMERIC (18)    NULL,
    [SurchargesValue]            NUMERIC (18)    NULL,
    [CapitalRepaymentAgreement]  NUMERIC (18)    NULL,
    [FinancialInterest]          NUMERIC (18)    NULL,
    [RepaymentAgreementInterest] NUMERIC (18)    NULL,
    CONSTRAINT [PK_AccountReceivableShare__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableShare_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivableShare] NOCHECK CONSTRAINT [FK_AccountReceivableShare_AccountReceivable];


GO
CREATE NONCLUSTERED INDEX [IX_AccountReceivableShare__AccountReceivableId]
    ON [Portfolio].[AccountReceivableShare]([AccountReceivableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interes - Acuerdo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'RepaymentAgreementInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interes financiero - Acuerdo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'FinancialInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Capital - Acuerdo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CapitalRepaymentAgreement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Recargos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'SurchargesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Intereses', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'InterestValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de liquidacion de intereses', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'InterestPaymentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se ha afectado a través de los cruce de cuentas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Abono', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'PaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Traslado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'TransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor debito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';

