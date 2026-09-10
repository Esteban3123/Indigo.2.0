CREATE TABLE [Billing].[BillingNoteDetail] (
    [Id]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingNoteId]  INT             NOT NULL,
    [InvoiceId]      INT             NOT NULL,
    [InvoiceNumber]  VARCHAR (20)    NOT NULL,
    [CUFE]           VARCHAR (250)   NOT NULL,
    [DocumentDate]   DATETIME        NOT NULL,
    [AdjusmentValue] NUMERIC (20, 2) NOT NULL,
    [ConceptId]      INT             NOT NULL,
    [BillingValue]   NUMERIC (20, 2) CONSTRAINT [DF_BillingNoteDetail_BillingValue] DEFAULT ((0)) NOT NULL,
    [DiscountValue]  NUMERIC (20, 2) CONSTRAINT [DF_BillingNoteDetail_DiscountValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_BillingNoteDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingNoteDetail_BillingNote] FOREIGN KEY ([BillingNoteId]) REFERENCES [Billing].[BillingNote] ([Id]),
    CONSTRAINT [FK_BillingNoteDetail_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id])
);


GO
ALTER TABLE [Billing].[BillingNoteDetail] NOCHECK CONSTRAINT [FK_BillingNoteDetail_BillingNote];




GO
ALTER TABLE [Billing].[BillingNoteDetail] NOCHECK CONSTRAINT [FK_BillingNoteDetail_BillingNote];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del descuento (NUMERIC 20,2). Monto de rebaja o descuento aplicado en la factura dentro del detalle de la nota de ajuste.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento definido en la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado neto (NUMERIC 20,2). Monto base de la factura sin descuentos, retenciones, IVA ni ajustes. Valor bruto de servicios/bienes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la factura sin descuentos, retenciones e IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de la nota (INT). Naturaleza del ajuste: débito (intereses, gastos, cambio valor) o crédito (devolución, anulación, rebaja, descuento, rescisión, otros, sin referencia).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de la Nota relacionado con la factura electrónica. Dependiendo de la naturaleza, puede ser:  
    - Débito:  
      1. Intereses  
      2. Gastos por cobrar  
      3. Cambio de valor  
    - Crédito:  
      1. Devolución o no aceptación de partes del servicio  
      2. Anulación de factura electrónica  
      3. Rebaja total aplicada  
      4. Descuento total aplicado  
      5. Rescisión: nulidad por falta de requisitos  
      6. Otros  
	  7. Sin referencia a una factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor a ajustar (NUMERIC 20,2). Monto en pesos del ajuste (débito o crédito) aplicado mediante la nota de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'AdjusmentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor a ajustar con la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'AdjusmentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'AdjusmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la factura (DATETIME). Momento de emisión del documento original de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único de Facturación Electrónica (VARCHAR 250). Identificador único DIAN de la factura, usado para validación y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'CUFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Único de Facturación Electrónica asociada a la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'CUFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'CUFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura (VARCHAR 20), secuencial o referencia de la factura electrónica asociada al detalle de ajuste.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la Factura (Billing.Invoice) relacionada. Referencia cruzada a factura electrónica original.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la Nota de Facturación (Billing.BillingNote) asociada al detalle. Vincula el ajuste o glosa a la nota madre.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'BillingNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Nota ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'BillingNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'BillingNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de detalle de nota de facturación. Clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de notas de facturación (notas crédito o débito): registra cada factura afectada por una nota de ajuste, con los valores ajustados, descuentos aplicados y el código CUFE de la factura electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNoteDetail';
