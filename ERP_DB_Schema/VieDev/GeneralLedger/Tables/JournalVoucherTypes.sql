CREATE TABLE [GeneralLedger].[JournalVoucherTypes] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Description]      VARCHAR (500) CONSTRAINT [DF_DocumentType_Description] DEFAULT ('-') NULL,
    [Consecutive]      BIGINT        CONSTRAINT [DF_DocumentType_Consecutive] DEFAULT ((0)) NOT NULL,
    [Status]           BIT           CONSTRAINT [DF_DocumentType_State] DEFAULT ((1)) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_JournalVoucherTypes__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

