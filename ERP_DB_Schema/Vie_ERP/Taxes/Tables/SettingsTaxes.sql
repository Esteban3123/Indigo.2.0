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
ALTER TABLE [Taxes].[SettingsTaxes] NOCHECK CONSTRAINT [FK_SettingsTaxes_JournalVoucherTypes];


GO
ALTER TABLE [Taxes].[SettingsTaxes] NOCHECK CONSTRAINT [FK_SettingsTaxes_JournalVoucherTypes1];


GO
ALTER TABLE [Taxes].[SettingsTaxes] NOCHECK CONSTRAINT [FK_SettingsTaxes_JournalVoucherTypes2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si permite Crear Tercero con codigo catastral', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'AllowCreateThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo del ticket diario de impuestos bajos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoiceLowTaxesJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de tipo de asiento del diario de propiedades de factura.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoicePropertyJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de asiento diario del sector de facturas.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'InvoiceIndustryJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxes', @level2type = N'COLUMN', @level2name = N'Id';

