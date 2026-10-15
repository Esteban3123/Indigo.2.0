CREATE TABLE [Treasury].[SchedulePaymentDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierId]            INT             NOT NULL,
    [ThirdPartyId]          INT             NOT NULL,
    [SchedulePaymentId]     INT             NOT NULL,
    [DistributionLineId]    INT             NOT NULL,
    [ExpenseConceptId]      INT             NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [Nature]                TINYINT         NOT NULL,
    [AccountPayableId]      INT             NOT NULL,
    [AccountPayableShareId] INT             NOT NULL,
    [AmountPaid]            DECIMAL (18, 2) NOT NULL,
    [AmountPercent]         DECIMAL (5, 2)  NOT NULL,
    [PaymentConceptId]      INT             NOT NULL,
    [Paid]                  BIT             NOT NULL,
    [Description]           VARCHAR (300)   NULL,
    [VoucherTransactionId]  INT             NULL,
    [GeneratedVoucher]      BIT             CONSTRAINT [DF_SchedulePaymentDetail_GeneratedVoucher] DEFAULT ((0)) NOT NULL,
    [DiscountValue]         DECIMAL (18, 2) CONSTRAINT [DF__ScheduleP__Disco__76E710A9] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SchedulePaymentDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchedulePaymentDetail_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_AccountPayableShares] FOREIGN KEY ([AccountPayableShareId]) REFERENCES [Payments].[AccountPayableShares] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_DistributionLines] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_ExpenseConcepts] FOREIGN KEY ([ExpenseConceptId]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_SchedulePayment] FOREIGN KEY ([SchedulePaymentId]) REFERENCES [Treasury].[SchedulePayment] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_TreasuryPaymentConcepts] FOREIGN KEY ([PaymentConceptId]) REFERENCES [Treasury].[TreasuryPaymentConcepts] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_AccountPayable];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_AccountPayableShares];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_DistributionLines];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_ExpenseConcepts];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_MainAccounts];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_SchedulePayment];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_Supplier];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_ThirdParty];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_TreasuryPaymentConcepts];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_VoucherTransaction];

