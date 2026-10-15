CREATE TABLE [Budget].[BudgetaryEntity] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Section]             VARCHAR (4)   NOT NULL,
    [Unit]                VARCHAR (2)   NOT NULL,
    [Region]              VARCHAR (4)   NOT NULL,
    [Name]                VARCHAR (200) NOT NULL,
    [BudgetaryValidityId] INT           NULL,
    [ThirdPartyId]        INT           NOT NULL,
    [Recognition]         BIT           NOT NULL,
    [ESE]                 BIT           NOT NULL,
    [Status]              BIT           CONSTRAINT [DF_BudgetInstitutions_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_BudgetaryEntity_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_BudgetaryEntity_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_BudgetaryEntity__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetaryEntity_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetaryEntity] NOCHECK CONSTRAINT [FK_BudgetaryEntity_BudgetaryValidity];

