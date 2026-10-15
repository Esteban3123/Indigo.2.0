CREATE TABLE [Portfolio].[AccountReceivablePromptPayment] (
    [Id]                  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId] INT            NOT NULL,
    [DeadLine]            DATE           NOT NULL,
    [PercentageDiscount]  NUMERIC (5, 2) NOT NULL,
    [MainAccountId]       INT            NOT NULL,
    CONSTRAINT [PK_AccountReceivablePromptPayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivablePromptPayment_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivablePromptPayment_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivablePromptPayment] NOCHECK CONSTRAINT [FK_AccountReceivablePromptPayment_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivablePromptPayment] NOCHECK CONSTRAINT [FK_AccountReceivablePromptPayment_MainAccounts];

