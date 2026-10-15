CREATE TABLE [Taxes].[TaxesInvoiceDetail] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesInvoiceId]       INT            NOT NULL,
    [LiquidationConceptId] INT            NOT NULL,
    [PercentageConcept]    NUMERIC (5, 2) NOT NULL,
    [BaseValue]            NUMERIC (18)   NOT NULL,
    [Value]                NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_TaxesInvoiceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesInvoiceDetail_TaxesInvoice] FOREIGN KEY ([TaxesInvoiceId]) REFERENCES [Taxes].[TaxesInvoice] ([Id]),
    CONSTRAINT [FK_TaxesInvoiceDetail_TaxesLiquidationConcept] FOREIGN KEY ([LiquidationConceptId]) REFERENCES [Taxes].[TaxesLiquidationConcept] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesInvoiceDetail] NOCHECK CONSTRAINT [FK_TaxesInvoiceDetail_TaxesInvoice];


GO
ALTER TABLE [Taxes].[TaxesInvoiceDetail] NOCHECK CONSTRAINT [FK_TaxesInvoiceDetail_TaxesLiquidationConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del detalle de factura de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del concepto.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'PercentageConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de liquidación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'LiquidationConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de factura de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'TaxesInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';

