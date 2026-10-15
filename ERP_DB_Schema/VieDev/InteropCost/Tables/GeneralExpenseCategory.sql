CREATE TABLE [InteropCost].[GeneralExpenseCategory] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (50)  NOT NULL,
    [Description]      VARCHAR (250) CONSTRAINT [DF_InteropCost.GeneralExpenseCategory_Description] DEFAULT ('') NOT NULL,
    [ParentId]         INT           NULL,
    [Status]           BIT           CONSTRAINT [DF_InteropCost.GeneralExpenseCategory_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [Timestamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_InteropCost.GeneralExpenseCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InteropCost.GeneralExpenseCategory_InteropCost.GeneralExpenseCategory] FOREIGN KEY ([ParentId]) REFERENCES [InteropCost].[GeneralExpenseCategory] ([Id])
);


GO
ALTER TABLE [InteropCost].[GeneralExpenseCategory] NOCHECK CONSTRAINT [FK_InteropCost.GeneralExpenseCategory_InteropCost.GeneralExpenseCategory];

