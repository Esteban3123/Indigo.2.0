CREATE TABLE [GeneralLedger].[JournalVoucherDetails] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccounting]  INT             NOT NULL,
    [IdMainAccount] INT             NOT NULL,
    [IdThirdParty]  INT             NULL,
    [IdCostCenter]  INT             NULL,
    [DebitValue]    DECIMAL (21, 5) CONSTRAINT [DF_AccountingDetail_DebitValue] DEFAULT ((0)) NOT NULL,
    [CreditValue]   DECIMAL (21, 5) CONSTRAINT [DF_AccountingDetail_CreditValue] DEFAULT ((0)) NOT NULL,
    [Detail]        VARCHAR (MAX)   CONSTRAINT [DF_AccountingDetail_Detail] DEFAULT ('-') NULL,
    [IdRetention]   INT             NULL,
    [RetentionRate] DECIMAL (6, 3)  NULL,
    [BaseValue]     DECIMAL (18, 2) CONSTRAINT [DF_JournalVoucherDetails_BaseValue] DEFAULT ((0)) NULL,
    [BillingValue]  DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_JournalVoucherDetails__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingDetail_Accounting] FOREIGN KEY ([IdAccounting]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_AccountingDetail_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountingDetail_PUC] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountingDetail_RetentionConcept] FOREIGN KEY ([IdRetention]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountingDetail_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_Accounting];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_CostCenter];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_PUC];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_RetentionConcept];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [UX_JournalVoucherDetails_IdAccounting]
    ON [GeneralLedger].[JournalVoucherDetails]([IdAccounting] ASC)
    INCLUDE([IdMainAccount], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue], [Detail], [IdRetention], [RetentionRate], [BaseValue], [BillingValue]);


GO
CREATE NONCLUSTERED INDEX [IDX_JournalVoucherDetails_IdMainAccount_DB]
    ON [GeneralLedger].[JournalVoucherDetails]([IdMainAccount] ASC)
    INCLUDE([IdAccounting], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue]);

