CREATE TABLE [Budget].[AvailabilityExtension] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetaryValidityId] INT          NOT NULL,
    [DocumentDate]        DATETIME     NOT NULL,
    [CreationUser]        VARCHAR (20) CONSTRAINT [DF_AvailabilityExtension_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME     CONSTRAINT [DF_AvailabilityExtension_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_AvailabilityExtension] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailabilityExtension_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[AvailabilityExtension] NOCHECK CONSTRAINT [FK_AvailabilityExtension_BudgetaryValidity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la fecha del documento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtension', @level2type = N'COLUMN', @level2name = N'DocumentDate';

