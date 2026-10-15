CREATE TABLE [Budget].[CommitmentModification] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]  INT           NOT NULL,
    [CommitmentId]         INT           NOT NULL,
    [Document]             VARCHAR (100) NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [Observations]         VARCHAR (MAX) NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  CONSTRAINT [DF_CommitmentModification_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME      CONSTRAINT [DF_CommitmentModification_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [UpTo]                 TINYINT       CONSTRAINT [DF__Commitment__UpTo__3F718297] DEFAULT ((2)) NOT NULL,
    [EntityId]             INT           NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityName]           VARCHAR (250) NULL,
    [AnnulmentConceptId]   INT           NULL,
    [AnnulmentDescription] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CommitmentModification__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommitmentModification_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_CommitmentModification_Commitment] FOREIGN KEY ([CommitmentId]) REFERENCES [Budget].[Commitment] ([Id]),
    CONSTRAINT [FK_CommitmentModification_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id])
);


GO
ALTER TABLE [Budget].[CommitmentModification] NOCHECK CONSTRAINT [FK_CommitmentModification_BudgetaryValidity];


GO
ALTER TABLE [Budget].[CommitmentModification] NOCHECK CONSTRAINT [FK_CommitmentModification_Commitment];


GO
ALTER TABLE [Budget].[CommitmentModification] NOCHECK CONSTRAINT [FK_CommitmentModification_Concept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de documento hasta el cual se modifica el recurso   3 - Disponibilidad  4 - Presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModification', @level2type = N'COLUMN', @level2name = N'UpTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModification', @level2type = N'COLUMN', @level2name = N'TimeStamp';

