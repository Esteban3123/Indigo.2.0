CREATE TABLE [Billing].[InvoiceCustomerRetention] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceId]           INT             NOT NULL,
    [CustomerRetentionId] INT             NOT NULL,
    [CalculateTaxAdvance] TINYINT         NOT NULL,
    [RetentionType]       TINYINT         NOT NULL,
    [RetentionRate]       NUMERIC (5, 3)  NOT NULL,
    [BaseValue]           NUMERIC (18, 2) NOT NULL,
    [Value]               NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_InvoiceCustomerRetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceCustomerRetention_CustomerRetention] FOREIGN KEY ([CustomerRetentionId]) REFERENCES [Common].[CustomerRetention] ([Id]),
    CONSTRAINT [FK_InvoiceCustomerRetention_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceCustomerRetention] NOCHECK CONSTRAINT [FK_InvoiceCustomerRetention_CustomerRetention];


GO
ALTER TABLE [Billing].[InvoiceCustomerRetention] NOCHECK CONSTRAINT [FK_InvoiceCustomerRetention_Invoice];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la retención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base a la cual se le practica la retención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de retención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de retencion de acuerdo a la cuenta contable.   0 - Ninguna  1 - ReteFuente  2 - ReteIva   3 - ReteIca  4 - Otra', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se calculará Anticipos de Impuestos de acuerdo al parámetro de facturación:  0 - No  1 - Solo Informar  2 - Reconocer', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'CalculateTaxAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la retención asociada al cliente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'CustomerRetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'Id';

