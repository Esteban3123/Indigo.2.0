CREATE TABLE [Budget].[Dependency] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetaryValidityId] INT           NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Name]                VARCHAR (100) NULL,
    [ResponsibleId]       INT           NOT NULL,
    [Status]              BIT           CONSTRAINT [DF_BudgetDependency_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_Dependency_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_Dependency_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Dependency__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetDependency_ThirdParty] FOREIGN KEY ([ResponsibleId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Dependency_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[Dependency] NOCHECK CONSTRAINT [FK_BudgetDependency_ThirdParty];


GO
ALTER TABLE [Budget].[Dependency] NOCHECK CONSTRAINT [FK_Dependency_BudgetaryValidity];

