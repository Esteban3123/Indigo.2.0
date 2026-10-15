CREATE TABLE [Cost].[CostGeneralExpenseCategory] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (50)  NOT NULL,
    [Description]      VARCHAR (250) CONSTRAINT [DF_Cost.CostGeneralExpenseCategory_Description] DEFAULT ('') NOT NULL,
    [ParentId]         INT           NULL,
    [Status]           BIT           CONSTRAINT [DF_Cost.CostGeneralExpenseCategory_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [Timestamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Cost.CostGeneralExpenseCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cost.CostGeneralExpenseCategory_Cost.CostGeneralExpenseCategory] FOREIGN KEY ([ParentId]) REFERENCES [Cost].[CostGeneralExpenseCategory] ([Id])
);


GO
ALTER TABLE [Cost].[CostGeneralExpenseCategory] NOCHECK CONSTRAINT [FK_Cost.CostGeneralExpenseCategory_Cost.CostGeneralExpenseCategory];

