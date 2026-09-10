CREATE TABLE [Billing].[InvoiceDetailBasicInvoice] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceId]            INT             NOT NULL,
    [BasicBillingDetailId] INT             NOT NULL,
    [Quantity]             INT             NOT NULL,
    [UnitSalesPrice]       NUMERIC (18, 2) NOT NULL,
    [TotalSalesPrice]      NUMERIC (18, 2) NOT NULL,
    [Balance]              NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_InvoiceDetailBasicInvoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CC_InvoiceDetailBasicInvoice_Balance] CHECK ([Balance]>=(0)),
    CONSTRAINT [FK_InvoiceDetailBasicInvoice_BasicBillingDetail] FOREIGN KEY ([BasicBillingDetailId]) REFERENCES [Billing].[BasicBillingDetail] ([Id]),
    CONSTRAINT [FK_InvoiceDetailBasicInvoice_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceDetailBasicInvoice] NOCHECK CONSTRAINT [CC_InvoiceDetailBasicInvoice_Balance];


GO
ALTER TABLE [Billing].[InvoiceDetailBasicInvoice] NOCHECK CONSTRAINT [FK_InvoiceDetailBasicInvoice_BasicBillingDetail];


GO
ALTER TABLE [Billing].[InvoiceDetailBasicInvoice] NOCHECK CONSTRAINT [FK_InvoiceDetailBasicInvoice_Invoice];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente del renglón de factura. Se afecta por notas de cartera, glosas y procesos de preauditoría. Siempre ≥ 0. Vinculado a cobranza y cartera.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al saldo del detalle. Este campo es afectado por notas de cartera de tipo preauditoría', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio total de venta del renglón (Cantidad × Precio Unitario). Valor facturado antes de descuentos y glosas. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio total', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta por servicio/procedimiento/producto. Base para cálculo del total. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'UnitSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio Unitario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'UnitSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'UnitSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios, procedimientos, productos o actos facturados en el renglón. Entero positivo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle básico de facturación. FK a tabla BasicBillingDetail. Define el servicio/procedimiento/producto facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'BasicBillingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de la factura básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'BasicBillingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'BasicBillingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura padre. FK a tabla Invoice. Agrupa múltiples renglones en una factura de ingreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) del renglón de factura básica. PK de la tabla. INT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de ítems incluidos en una factura de facturación simplificada (factura básica), registrando cada servicio o producto cobrado con su cantidad, precio unitario, precio total y saldo pendiente por ítem.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice';
