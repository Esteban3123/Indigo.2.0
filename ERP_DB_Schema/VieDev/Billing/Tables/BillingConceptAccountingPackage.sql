CREATE TABLE [Billing].[BillingConceptAccountingPackage] (
    [Id]                              INT     IDENTITY (1, 1) NOT NULL,
    [BillingConceptId]                INT     NOT NULL,
    [UnitType]                        TINYINT NOT NULL,
    [ConceptMainAccountId]            INT     NOT NULL,
    [FavorableDeviationMainAccountId] INT     NOT NULL,
    [VariationPVMainAccountId]        INT     NULL,
    CONSTRAINT [PK_BillingConceptAccountingPackage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingConceptAccountingPackage_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_BillingConceptAccountingPackage_ConceptMainAccount] FOREIGN KEY ([ConceptMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccountingPackage_FavorableDeviationMainAccount] FOREIGN KEY ([FavorableDeviationMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccountingPackage_VariationPVMainAccount] FOREIGN KEY ([VariationPVMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Billing].[BillingConceptAccountingPackage] NOCHECK CONSTRAINT [FK_BillingConceptAccountingPackage_BillingConcept];


GO
ALTER TABLE [Billing].[BillingConceptAccountingPackage] NOCHECK CONSTRAINT [FK_BillingConceptAccountingPackage_ConceptMainAccount];


GO
ALTER TABLE [Billing].[BillingConceptAccountingPackage] NOCHECK CONSTRAINT [FK_BillingConceptAccountingPackage_FavorableDeviationMainAccount];


GO
ALTER TABLE [Billing].[BillingConceptAccountingPackage] NOCHECK CONSTRAINT [FK_BillingConceptAccountingPackage_VariationPVMainAccount];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Variación PV', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'VariationPVMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta de desviación favorable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'FavorableDeviationMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de la cuenta contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'ConceptMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de unidad  1 - Urgencias  2 - Hospitalizacion  3 - Quirofanos  4 - Servicios Ambulatorios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de facturacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccountingPackage', @level2type = N'COLUMN', @level2name = N'Id';

