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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de la retención calculada (NUMERIC 18,2). Monto en dinero que se retiene del pago de la factura según el tipo y tasa de retención aplicada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base imponible o valor base sobre el cual se calcula la retención (NUMERIC 18,2). Monto del que se extrae el porcentaje de retención (ReteFuente, ReteIva, ReteIca u otra).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base a la cual se le practica la retención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o tasa de retención a aplicar (NUMERIC 5,3). Ejemplo: 2.5%, 19%, expresado en formato numérico decimal. Varía según tipo de retención y normativa tributaria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de retención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de retención fiscal aplicada a la factura (TINYINT). Valores: 0=Ninguna, 1=ReteFuente, 2=ReteIva, 3=ReteIca, 4=Otra. Según cuenta contable y normativa RIPS/DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de retencion de acuerdo a la cuenta contable.   0 - Ninguna  1 - ReteFuente  2 - ReteIva   3 - ReteIca  4 - Otra', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'RetentionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control para anticipo de impuestos en facturación (TINYINT). Valores: 0=No calcular, 1=Solo informar, 2=Reconocer contablemente. Parámetro de negocio para tratamiento de retenciones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'CalculateTaxAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si se calculará Anticipos de Impuestos de acuerdo al parámetro de facturación:  0 - No  1 - Solo Informar  2 - Reconocer', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'CalculateTaxAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'CalculateTaxAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT FK) de la configuración de retención del cliente. Referencia a [Common].[CustomerRetention]. Vincula políticas de retención específicas por cliente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'CustomerRetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la retención asociada al cliente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'CustomerRetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'CustomerRetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT FK) de la factura asociada. Referencia a [Billing].[Invoice]. Une el registro de retención al comprobante de venta o factura emitida.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT IDENTITY, PRIMARY KEY). Clave primaria de [InvoiceCustomerRetention]. Identifica cada aplicación de retención en una factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retenciones aplicadas a clientes en facturas de facturación: registra el detalle de cada retención tributaria (por ejemplo, retención en la fuente, ICA, IVA) asociada a una factura y un cliente, incluyendo el tipo, la tarifa y el valor calculado sobre la base gravable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceCustomerRetention';
