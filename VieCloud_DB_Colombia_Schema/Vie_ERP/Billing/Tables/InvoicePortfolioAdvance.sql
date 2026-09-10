CREATE TABLE [Billing].[InvoicePortfolioAdvance] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceId]          INT             NOT NULL,
    [PortfolioAdvanceId] INT             NOT NULL,
    [Value]              NUMERIC (20, 2) NOT NULL,
    CONSTRAINT [PK_InvoicePortfolioAdvance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoicePortfolioAdvance_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoicePortfolioAdvance_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id])
);


GO
ALTER TABLE [Billing].[InvoicePortfolioAdvance] NOCHECK CONSTRAINT [FK_InvoicePortfolioAdvance_Invoice];




GO
ALTER TABLE [Billing].[InvoicePortfolioAdvance] NOCHECK CONSTRAINT [FK_InvoicePortfolioAdvance_Invoice];


GO



GO
CREATE NONCLUSTERED INDEX [IX_InvoicePortfolioAdvance__InvoiceId]
    ON [Billing].[InvoicePortfolioAdvance]([InvoiceId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Cruzado, monto numérico (20,2) aplicado del anticipo a la factura; importe de descuento o aplicación de pago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Cruzado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Anticipo del Paciente (FK Portfolio.PortfolioAdvance); referencia a pago anticipado, depósito o garantía del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Anticipo del Paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Factura (FK Billing.Invoice); referencia única al documento de cobro, cuenta o facturación de atención en salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación Factura-Anticipo (PK, IDENTITY); clave primaria del cruce entre factura y anticipo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la relación entre facturas y anticipos de cartera, indicando qué valor de un anticipo fue aplicado o cruzado contra una factura específica de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoicePortfolioAdvance';

GO
CREATE NONCLUSTERED INDEX [IX_InvoicePortfolioAdvance_InvoiceId]
    ON [Billing].[InvoicePortfolioAdvance]([InvoiceId] ASC)
    INCLUDE([PortfolioAdvanceId], [Value]);
