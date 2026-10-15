CREATE TABLE [Billing].[InvoiceGlosa] (
    [InvoiceNumber] VARCHAR (15)  NOT NULL,
    [Observation]   VARCHAR (MAX) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o información de la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceGlosa', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la factura de glosa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceGlosa', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';

