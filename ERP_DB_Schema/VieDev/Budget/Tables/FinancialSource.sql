CREATE TABLE [Budget].[FinancialSource] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetaryValidityId] INT           NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Name]                VARCHAR (100) NOT NULL,
    [Classification]      TINYINT       NOT NULL,
    [Status]              BIT           CONSTRAINT [DF_FinancialSource_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_FinancialSource_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_FinancialSource_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_FinancialSource__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FinancialSource_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[FinancialSource] NOCHECK CONSTRAINT [FK_FinancialSource_BudgetaryValidity];

