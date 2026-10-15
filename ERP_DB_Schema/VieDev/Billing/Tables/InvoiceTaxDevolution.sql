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
ALTER TABLE [Billing].[InvoiceTaxDevolution] NOCHECK CONSTRAINT [FK_InvoiceTaxDevolution_Invoice];


GO
ALTER TABLE [Billing].[InvoiceTaxDevolution] NOCHECK CONSTRAINT [FK_InvoiceTaxDevolution_Tax];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'ValueInOfficialCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del IVA que se devolvio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del IVA que se aplico la devolucion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'TaxId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la factura que se le aplico la devolucion del IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTaxDevolution', @level2type = N'COLUMN', @level2name = N'Id';

