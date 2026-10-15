CREATE TABLE [GeneralLedger].[GeneralLedgerBalance] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Month]         INT             NOT NULL,
    [Year]          INT             CONSTRAINT [DF_GeneralLedgerBalance_Year] DEFAULT ((2014)) NOT NULL,
    [IdMainAccount] INT             NOT NULL,
    [IdThirdParty]  INT             NULL,
    [IdCostCenter]  INT             NULL,
    [DebitValue]    DECIMAL (21, 5) CONSTRAINT [DF_AccountingBalance_DebitValue] DEFAULT ((0)) NOT NULL,
    [CreditValue]   DECIMAL (21, 5) CONSTRAINT [DF_AccountingBalance_CreditValue] DEFAULT ((0)) NOT NULL,
    [TimeStamp]     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AccountingBalance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingBalance_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountingBalance_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountingBalance_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerBalance] NOCHECK CONSTRAINT [FK_AccountingBalance_CostCenter];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerBalance] NOCHECK CONSTRAINT [FK_AccountingBalance_MainAccount];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerBalance] NOCHECK CONSTRAINT [FK_AccountingBalance_ThirdParty];

