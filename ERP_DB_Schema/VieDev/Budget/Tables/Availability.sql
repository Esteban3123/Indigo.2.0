CREATE TABLE [Budget].[Availability] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]  INT           NOT NULL,
    [DependencyId]         INT           NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [ExpirationDays]       INT           NOT NULL,
    [ExpirationDate]       DATETIME      NOT NULL,
    [AvailabilityType]     TINYINT       NOT NULL,
    [Observations]         VARCHAR (MAX) NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  CONSTRAINT [DF_Availability_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME      CONSTRAINT [DF_Availability_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [AnnulmentConceptId]   INT           NULL,
    [AnnulmentDescription] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Availability__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Availability_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_Availability_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id]),
    CONSTRAINT [FK_Availability_Dependency] FOREIGN KEY ([DependencyId]) REFERENCES [Budget].[Dependency] ([Id])
);


GO
ALTER TABLE [Budget].[Availability] NOCHECK CONSTRAINT [FK_Availability_BudgetaryValidity];


GO
ALTER TABLE [Budget].[Availability] NOCHECK CONSTRAINT [FK_Availability_Concept];


GO
ALTER TABLE [Budget].[Availability] NOCHECK CONSTRAINT [FK_Availability_Dependency];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Availability', @level2type = N'COLUMN', @level2name = N'TimeStamp';

