CREATE TABLE [Budget].[CollectionModification] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]  INT           NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [CollectionId]         INT           NOT NULL,
    [Document]             VARCHAR (100) NOT NULL,
    [Observations]         VARCHAR (MAX) NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  CONSTRAINT [DF_CollectionModification_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME      CONSTRAINT [DF_CollectionModification_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [EntityId]             INT           NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityName]           VARCHAR (250) NULL,
    [AnnulmentConceptId]   INT           NULL,
    [AnnulmentDescription] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CollectionModification__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CollectionModification_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_CollectionModification_Collection] FOREIGN KEY ([CollectionId]) REFERENCES [Budget].[Collection] ([Id]),
    CONSTRAINT [FK_CollectionModification_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id])
);


GO
ALTER TABLE [Budget].[CollectionModification] NOCHECK CONSTRAINT [FK_CollectionModification_BudgetaryValidity];


GO
ALTER TABLE [Budget].[CollectionModification] NOCHECK CONSTRAINT [FK_CollectionModification_Collection];


GO
ALTER TABLE [Budget].[CollectionModification] NOCHECK CONSTRAINT [FK_CollectionModification_Concept];

