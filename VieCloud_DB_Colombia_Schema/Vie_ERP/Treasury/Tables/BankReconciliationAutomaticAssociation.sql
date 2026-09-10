CREATE TABLE [Treasury].[BankReconciliationAutomaticAssociation] (
    [Id]                                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankReconciliationAutomaticDetailId]  INT NOT NULL,
    [BankReconciliationAutomaticExtractId] INT NOT NULL,
    CONSTRAINT [PK_BankReconciliationAutomaticAssociatioAssociation_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliationAutomaticAssociation_BankReconciliationAutomaticAssociation] FOREIGN KEY ([BankReconciliationAutomaticDetailId]) REFERENCES [Treasury].[BankReconciliationAutomaticDetail] ([Id]),
    CONSTRAINT [FK_BankReconciliationAutomaticAssociation_BankReconciliationAutomaticExtractDetail] FOREIGN KEY ([BankReconciliationAutomaticExtractId]) REFERENCES [Treasury].[BankReconciliationAutomaticExtractDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único que referencia el detalle del extracto bancario automático; clave foránea a BankReconciliationAutomaticExtractDetail. Relaciona movimientos del estado de cuenta bancaria con asientos contables.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticExtractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id referenciando el detalle de los extractos bancarios', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticExtractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticExtractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único que referencia el detalle del libro de tesorería o libro bancario automático; clave foránea a BankReconciliationAutomaticDetail. Vincula registros contables internos con extractos bancarios para conciliación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id referenciando el detalle de los libros', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y clave primaria (INT IDENTITY) del registro de asociación automática entre extracto bancario y detalle de libro contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociaciones automáticas entre los detalles contables y los extractos bancarios dentro del proceso de conciliación bancaria automática. Relaciona cada movimiento del libro contable con su correspondiente transacción del extracto bancario.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation';
