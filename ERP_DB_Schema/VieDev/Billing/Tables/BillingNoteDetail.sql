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
ALTER TABLE [Billing].[BillingNoteDetail] NOCHECK CONSTRAINT [FK_BillingNoteDetail_Invoice];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de la Nota relacionado con la factura electrónica. Dependiendo de la naturaleza, puede ser:  
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

