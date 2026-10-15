CREATE TABLE [Billing].[BillingNoteDetailTax] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingNoteDetailId] INT             NOT NULL,
    [TaxPercentage]       NUMERIC (5, 2)  NOT NULL,
    [TaxValue]            NUMERIC (20, 2) NOT NULL,
    [BaseValue]           NUMERIC (20, 2) CONSTRAINT [DF_BillingNoteDetailTax_BaseValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_BillingNoteDetailTax] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingNoteDetailTax_BillingNoteDetail] FOREIGN KEY ([BillingNoteDetailId]) REFERENCES [Billing].[BillingNoteDetail] ([Id])
);


GO
ALTER TABLE [Billing].[BillingNoteDetailTax] NOCHECK CONSTRAINT [FK_BillingNoteDetailTax_BillingNoteDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base para el calculo del impuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Impuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del Impuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'TaxPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Detalle de la Nota ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'BillingNoteDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetailTax', @level2type = N'COLUMN', @level2name = N'Id';

