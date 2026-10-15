CREATE TABLE [dbo].[TableJournalVoucherDetail] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdJournalVoucher] INT             NULL,
    [IdMainAccount]    INT             NOT NULL,
    [IdThirdParty]     INT             NULL,
    [IdCostCenter]     INT             NULL,
    [DebitValue]       DECIMAL (18, 2) NOT NULL,
    [CreditValue]      DECIMAL (18, 2) NOT NULL,
    [Detail]           VARCHAR (MAX)   NULL,
    [IdRetention]      INT             NULL,
    [RetentionRate]    DECIMAL (5, 2)  NULL,
    [BaseValue]        DECIMAL (18)    NULL,
    [BillingValue]     DECIMAL (18)    NULL,
    CONSTRAINT [PK_TableJournalVoucherDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

