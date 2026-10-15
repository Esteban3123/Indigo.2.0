CREATE TABLE [Budget].[AvailabilityModification] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]  INT           NOT NULL,
    [AvailabilityId]       INT           NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [Observations]         VARCHAR (MAX) NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  CONSTRAINT [DF_AvailabilityModification_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME      CONSTRAINT [DF_AvailabilityModification_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [UpTo]                 TINYINT       CONSTRAINT [DF__Availabili__UpTo__4065A6D0] DEFAULT ((3)) NOT NULL,
    [Document]             VARCHAR (100) NULL,
    [EntityId]             INT           NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityName]           VARCHAR (250) NULL,
    [AnnulmentConceptId]   INT           NULL,
    [AnnulmentDescription] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_AvailabilityModification__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailabilityModification_Availability] FOREIGN KEY ([AvailabilityId]) REFERENCES [Budget].[Availability] ([Id]),
    CONSTRAINT [FK_AvailabilityModification_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_AvailabilityModification_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id])
);


GO
ALTER TABLE [Budget].[AvailabilityModification] NOCHECK CONSTRAINT [FK_AvailabilityModification_Availability];


GO
ALTER TABLE [Budget].[AvailabilityModification] NOCHECK CONSTRAINT [FK_AvailabilityModification_BudgetaryValidity];


GO
ALTER TABLE [Budget].[AvailabilityModification] NOCHECK CONSTRAINT [FK_AvailabilityModification_Concept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de documento hasta el cual se modifica el recurso   4 - Presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'UpTo';

