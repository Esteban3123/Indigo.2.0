CREATE TABLE [Taxes].[SettingsTaxes] (
    [Id]                                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceIndustryJournalVoucherTypeId] INT NOT NULL,
    [InvoicePropertyJournalVoucherTypeId] INT NOT NULL,
    [InvoiceLowTaxesJournalVoucherTypeId] INT NOT NULL,
    [AllowCreateThirdParty]               BIT CONSTRAINT [DF_SettingsTaxes_AllowCreateThirdParty] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SettingsTaxes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsTaxes_JournalVoucherTypes] FOREIGN KEY ([InvoiceIndustryJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTaxes_JournalVoucherTypes1] FOREIGN KEY ([InvoicePropertyJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTaxes_JournalVoucherTypes2] FOREIGN KEY ([InvoiceLowTaxesJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT, default=0) que autoriza la creación de terceros (proveedores, acreedores, entidades) usando código catastral como identificación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'AllowCreateThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si permite Crear Tercero con codigo catastral', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'AllowCreateThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'AllowCreateThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) que referencia el tipo de asiento diario para impuestos reducidos; vincula facturas con retención o tributación baja a GeneralLedger.JournalVoucherTypes.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoiceLowTaxesJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo del ticket diario de impuestos bajos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoiceLowTaxesJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoiceLowTaxesJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) que referencia el tipo de asiento diario para propiedades inmuebles; asocia facturas de bienes raíces a su registro contable en GeneralLedger.JournalVoucherTypes.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoicePropertyJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de tipo de asiento del diario de propiedades de factura.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoicePropertyJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoicePropertyJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) que referencia el tipo de asiento diario del sector industrial; vincula facturas del sector a su clasificación contable en GeneralLedger.JournalVoucherTypes.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoiceIndustryJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de asiento diario del sector de facturas.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoiceIndustryJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoiceIndustryJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de configuración de impuestos en el sistema contable.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración general del módulo de impuestos: define qué tipos de comprobante contable (voucher) se usan para facturas de industria y comercio, propiedad y retenciones de bajo monto, además de controlar si se permite crear terceros automáticamente durante el proceso tributario.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes';
