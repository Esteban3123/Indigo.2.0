CREATE TABLE [Payments].[AccountPayableShares] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable] INT             NOT NULL,
    [Share]            INT             NOT NULL,
    [DateExpires]      DATETIME        NOT NULL,
    [InitialValue]     DECIMAL (18, 2) NOT NULL,
    [DebitValue]       DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_DebitValue] DEFAULT ((0.0)) NOT NULL,
    [CreditValue]      DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_CreditValue] DEFAULT ((0.0)) NOT NULL,
    [ValueTransfers]   DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_ValueTransfers] DEFAULT ((0.0)) NOT NULL,
    [PaymentValue]     DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_PaymentValue] DEFAULT ((0.0)) NOT NULL,
    [CrossingValue]    DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableShares_CrossingValue] DEFAULT ((0.0)) NOT NULL,
    [Balance]          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AccountPayableShares__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableShares_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableShares] NOCHECK CONSTRAINT [FK_AccountPayableShares_AccountPayable];

