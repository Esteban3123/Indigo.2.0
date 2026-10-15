CREATE TABLE [Payments].[FilingUnit] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [ParentId]         INT           NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_FilingUnit_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_FilingUnit_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_FilingUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilingUnit_FilingUnit] FOREIGN KEY ([ParentId]) REFERENCES [Payments].[FilingUnit] ([Id])
);


GO
ALTER TABLE [Payments].[FilingUnit] NOCHECK CONSTRAINT [FK_FilingUnit_FilingUnit];

