CREATE TABLE [Inventory].[Review] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [Description]            VARCHAR (MAX)   NULL,
    [EntityName]             VARCHAR (250)   NOT NULL,
    [EntityCode]             VARCHAR (20)    NOT NULL,
    [JournalVoucherType]     VARCHAR (200)   NULL,
    [Consecutive]            BIGINT          NULL,
    [JournalVoucherId]       INT             NOT NULL,
    [MainAccountId]          INT             NOT NULL,
    [MainAccount]            VARCHAR (200)   NOT NULL,
    [DocumentDate]           DATE            NULL,
    [AccountingDocumentDate] DATE            NULL,
    [DebitValue]             DECIMAL (20, 4) NOT NULL,
    [AccountingDebitValue]   DECIMAL (20, 4) NOT NULL,
    [CreditValue]            DECIMAL (20, 4) NOT NULL,
    [AccountingCreditValue]  DECIMAL (20, 4) NOT NULL
);

