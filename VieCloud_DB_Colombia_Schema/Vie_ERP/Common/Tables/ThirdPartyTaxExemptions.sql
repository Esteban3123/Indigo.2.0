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



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifa de IVA exenta, identificador de la tasa impositiva relacionada en contabilidad general (FK GeneralLedgerIVA)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ExemptFeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tarifa de IVA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ExemptFeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ExemptFeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento de exoneración tributaria, registro de cuándo se emitió o procesó la exención', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de institución con exoneración tributaria, entidad que goza del beneficio fiscal (FK TaxExemptions)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'InstitutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exoneracion tributaria de tipo institucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'InstitutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'InstitutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Artículo o Resolución que ampara la exoneración, acto administrativo o legal que sustenta la exención', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ART/RESNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del Art / Res', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ART/RESNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ART/RESNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del documento de exoneración, número único del acto o resolución tributaria (VARCHAR 40, PII)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentIdentification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del documento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentIdentification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentIdentification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del registro de exoneración, descripción adicional o observaciones del beneficio tributario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de exoneración tributaria, clasificación del acto que genera el beneficio (FK TaxExemptions)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exoneracion tributaria de tipo documento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'DocumentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero o proveedor beneficiado, entidad que recibe la exoneración fiscal (FK ThirdParty)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de exoneración tributaria, clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de exenciones tributarias o fiscales asociadas a terceros (proveedores, empresas o entidades), indicando el tipo de documento, la identificación del tercero, la resolución o artículo legal que ampara la exención, la institución que la emite y la tarifa de exención aplicable.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyTaxExemptions';
