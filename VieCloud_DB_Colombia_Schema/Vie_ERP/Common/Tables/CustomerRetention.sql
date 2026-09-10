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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de retención del cliente: 0=Inactivo, 1=Activo. Indica si la política de retención está vigente o suspendida (BIT, NOT NULL).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  0 - Inactivo  1 - Activo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención aplicado al cliente. Referencia a GeneralLedger.RetentionConcepts. Define tipo y porcentaje de retención (INT, FK, NOT NULL).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de retención', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de nota de cartera asociado. Referencia a Portfolio.PortfolioNoteConcept. Vincula observaciones de gestión de cartera (INT, FK, NOT NULL).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'PortfolioNoteConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de nota de cartera', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'PortfolioNoteConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'PortfolioNoteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del cliente o contratante. Referencia a Common.Customer. Clave foránea para vincular políticas de retención (INT, FK, NOT NULL).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cliente', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de retención del cliente. Clave primaria, generado automáticamente (INT, IDENTITY, PK, NOT NULL).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los conceptos de retención aplicados a clientes en el proceso de cartera, asociando cada cliente con un concepto de nota de cartera y un concepto de retención específico, indicando si dicha retención está activa o inactiva.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CustomerRetention';

GO
CREATE NONCLUSTERED INDEX [IX_CustomerRetention_CustomerId]
    ON [Common].[CustomerRetention]([CustomerId] ASC)
    INCLUDE([RetentionConceptId]);
