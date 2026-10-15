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

