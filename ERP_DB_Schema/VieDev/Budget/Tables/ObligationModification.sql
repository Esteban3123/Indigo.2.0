CREATE TABLE [Budget].[ObligationModification] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]  INT           NOT NULL,
    [ObligationId]         INT           NOT NULL,
    [Document]             VARCHAR (100) NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [Observations]         VARCHAR (MAX) NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  CONSTRAINT [DF_ObligationModification_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME      CONSTRAINT [DF_ObligationModification_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [UpTo]                 TINYINT       CONSTRAINT [DF__Obligation__UpTo__3E7D5E5E] DEFAULT ((1)) NOT NULL,
    [EntityId]             INT           NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityName]           VARCHAR (250) NULL,
    [AnnulmentConceptId]   INT           NULL,
    [AnnulmentDescription] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ObligationModification__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ObligationModification_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_ObligationModification_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id]),
    CONSTRAINT [FK_ObligationModification_Obligation] FOREIGN KEY ([ObligationId]) REFERENCES [Budget].[Obligation] ([Id])
);


GO
ALTER TABLE [Budget].[ObligationModification] NOCHECK CONSTRAINT [FK_ObligationModification_BudgetaryValidity];


GO
ALTER TABLE [Budget].[ObligationModification] NOCHECK CONSTRAINT [FK_ObligationModification_Concept];


GO
ALTER TABLE [Budget].[ObligationModification] NOCHECK CONSTRAINT [FK_ObligationModification_Obligation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de documento hasta el cual se modifica el recurso   2 - Compromiso,  3 - Disponibilidad,  4 - Presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ObligationModification', @level2type = N'COLUMN', @level2name = N'UpTo';

