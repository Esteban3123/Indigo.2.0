CREATE TABLE [Budget].[BudgetModification] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [DocumentSource]      TINYINT       NOT NULL,
    [DocumentDate]        DATETIME      NOT NULL,
    [Document]            VARCHAR (100) NOT NULL,
    [BudgetaryValidityId] INT           NOT NULL,
    [Observations]        VARCHAR (MAX) NOT NULL,
    [Status]              TINYINT       NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_BudgetModification_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_BudgetModification_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [AnnulmentUser]       VARCHAR (20)  NULL,
    [AnnulmentDate]       DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [DocumentType]        TINYINT       NULL,
    [EntityId]            INT           NULL,
    [EntityCode]          VARCHAR (20)  NULL,
    [EntityName]          VARCHAR (250) NULL,
    CONSTRAINT [PK_BudgetModification__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetModification_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetModification] NOCHECK CONSTRAINT [FK_BudgetModification_BudgetaryValidity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo de documento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetModification', @level2type = N'COLUMN', @level2name = N'DocumentType';

