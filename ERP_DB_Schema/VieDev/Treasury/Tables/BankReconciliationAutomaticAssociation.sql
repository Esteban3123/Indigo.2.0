CREATE TABLE [Treasury].[BankReconciliationAutomaticAssociation] (
    [Id]                                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankReconciliationAutomaticDetailId]  INT NOT NULL,
    [BankReconciliationAutomaticExtractId] INT NOT NULL,
    CONSTRAINT [PK_BankReconciliationAutomaticAssociatioAssociation_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliationAutomaticAssociation_BankReconciliationAutomaticAssociation] FOREIGN KEY ([BankReconciliationAutomaticDetailId]) REFERENCES [Treasury].[BankReconciliationAutomaticDetail] ([Id]),
    CONSTRAINT [FK_BankReconciliationAutomaticAssociation_BankReconciliationAutomaticExtractDetail] FOREIGN KEY ([BankReconciliationAutomaticExtractId]) REFERENCES [Treasury].[BankReconciliationAutomaticExtractDetail] ([Id])
);


GO
ALTER TABLE [Treasury].[BankReconciliationAutomaticAssociation] NOCHECK CONSTRAINT [FK_BankReconciliationAutomaticAssociation_BankReconciliationAutomaticAssociation];


GO
ALTER TABLE [Treasury].[BankReconciliationAutomaticAssociation] NOCHECK CONSTRAINT [FK_BankReconciliationAutomaticAssociation_BankReconciliationAutomaticExtractDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id referenciando el detalle de los extractos bancarios', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticExtractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id referenciando el detalle de los libros', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticAssociation', @level2type = N'COLUMN', @level2name = N'Id';

