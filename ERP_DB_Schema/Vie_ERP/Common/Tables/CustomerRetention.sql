CREATE TABLE [Common].[CustomerRetention] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CustomerId]             INT NOT NULL,
    [PortfolioNoteConceptId] INT NOT NULL,
    [RetentionConceptId]     INT NOT NULL,
    [Status]                 BIT NOT NULL,
    CONSTRAINT [PK_CustomerRetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomerRetention_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_CustomerRetention_PortfolioNoteConcept] FOREIGN KEY ([PortfolioNoteConceptId]) REFERENCES [Portfolio].[PortfolioNoteConcept] ([Id]),
    CONSTRAINT [FK_CustomerRetention_RetentionConcepts] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Common].[CustomerRetention] NOCHECK CONSTRAINT [FK_CustomerRetention_Customer];


GO
ALTER TABLE [Common].[CustomerRetention] NOCHECK CONSTRAINT [FK_CustomerRetention_PortfolioNoteConcept];


GO
ALTER TABLE [Common].[CustomerRetention] NOCHECK CONSTRAINT [FK_CustomerRetention_RetentionConcepts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  0 - Inactivo  1 - Activo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de retención', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de nota de cartera', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'PortfolioNoteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cliente', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'Id';

