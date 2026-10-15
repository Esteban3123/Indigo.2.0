CREATE TABLE [Common].[ThirdPartyTaxExemptions] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [ThirdPartyId]           INT           NOT NULL,
    [DocumentTypeId]         INT           NOT NULL,
    [Detail]                 VARCHAR (100) NULL,
    [DocumentIdentification] VARCHAR (40)  NOT NULL,
    [ART/RESNumber]          VARCHAR (40)  NULL,
    [InstitutionId]          INT           NOT NULL,
    [DocumentDate]           DATETIME      NULL,
    [ExemptFeeId]            INT           NOT NULL,
    CONSTRAINT [PK_ThirdPartyTaxExemptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdPartyTaxExemptions_GeneralLedgerIVA] FOREIGN KEY ([ExemptFeeId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_ThirdPartyTaxExemptions_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_ThirdPartyTaxExemptionsDocumentType_TaxExemptions] FOREIGN KEY ([DocumentTypeId]) REFERENCES [Common].[TaxExemptions] ([Id]),
    CONSTRAINT [FK_ThirdPartyTaxExemptionsInstitution_TaxExemptions] FOREIGN KEY ([InstitutionId]) REFERENCES [Common].[TaxExemptions] ([Id])
);


GO
ALTER TABLE [Common].[ThirdPartyTaxExemptions] NOCHECK CONSTRAINT [FK_ThirdPartyTaxExemptions_GeneralLedgerIVA];


GO
ALTER TABLE [Common].[ThirdPartyTaxExemptions] NOCHECK CONSTRAINT [FK_ThirdPartyTaxExemptions_ThirdParty];


GO
ALTER TABLE [Common].[ThirdPartyTaxExemptions] NOCHECK CONSTRAINT [FK_ThirdPartyTaxExemptionsDocumentType_TaxExemptions];


GO
ALTER TABLE [Common].[ThirdPartyTaxExemptions] NOCHECK CONSTRAINT [FK_ThirdPartyTaxExemptionsInstitution_TaxExemptions];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifa de IVA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ExemptFeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exoneracion tributaria de tipo institucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'InstitutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del Art / Res', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ART/RESNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del documento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentIdentification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exoneracion tributaria de tipo documento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'Id';

