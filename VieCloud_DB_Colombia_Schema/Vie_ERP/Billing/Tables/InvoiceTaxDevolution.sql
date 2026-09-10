CREATE TABLE [Billing].[InvoiceTaxDevolution] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [InvoiceId]               INT             NOT NULL,
    [TaxId]                   INT             NOT NULL,
    [Value]                   NUMERIC (20, 2) NOT NULL,
    [ValueInOfficialCurrency] NUMERIC (20, 2) NOT NULL,
    CONSTRAINT [PK_InvoiceTaxDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceTaxDevolution_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoiceTaxDevolution_Tax] FOREIGN KEY ([TaxId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de la devolución del IVA convertido a moneda oficial o de registro contable (NUMERIC 20,2); valor normalizado para consolidación RIPS y reportes tributarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'ValueInOfficialCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'ValueInOfficialCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'ValueInOfficialCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto del IVA devuelto en moneda original (NUMERIC 20,2); valor de reintegro o devolución fiscal del impuesto al valor agregado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del IVA que se devolvio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del impuesto IVA (INT, FK→GeneralLedger.GeneralLedgerIVA) que fue aplicado y devuelto en esta transacción', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'TaxId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del IVA que se aplico la devolucion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'TaxId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'TaxId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura (INT, FK→Billing.Invoice) a la cual se le aplicó la devolución del IVA; referencia la factura de venta o atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura que se le aplico la devolucion del IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de devolución de impuesto en la tabla InvoiceTaxDevolution', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devoluciones de impuestos asociadas a facturas de facturación. Registra los valores de cada impuesto devuelto por factura, tanto en moneda original como en moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution';
