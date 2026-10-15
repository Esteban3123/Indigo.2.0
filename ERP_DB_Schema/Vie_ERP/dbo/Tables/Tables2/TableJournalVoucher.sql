CREATE TABLE [dbo].[TableJournalVoucher] (
    [Id]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherType] INT           NOT NULL,
    [VoucherDate]   DATETIME      NOT NULL,
    [Imported]      BIT           NOT NULL,
    [Status]        TINYINT       NOT NULL,
    [Detail]        VARCHAR (500) NULL,
    [EntityCode]    VARCHAR (20)  NULL,
    [EntityId]      INT           NULL,
    [EntityName]    VARCHAR (250) NULL,
    [IsClosedYear]  BIT           NOT NULL,
    CONSTRAINT [PK_TableJournalVoucher__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

