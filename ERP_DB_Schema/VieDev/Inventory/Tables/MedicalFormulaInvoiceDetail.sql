CREATE TABLE [Inventory].[MedicalFormulaInvoiceDetail] (
    [Id]                      INT IDENTITY (1, 1) NOT NULL,
    [MedicalFormulaInvoiceId] INT NOT NULL,
    [InvoiceDetailId]         INT NOT NULL,
    [MedicalFormulaDetailId]  INT NOT NULL,
    CONSTRAINT [PK_MedicalFormulaInvoice_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaInvoiceDetail_InvoiceDetailId] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Billing].[InvoiceDetail] ([Id]),
    CONSTRAINT [FK_MedicalFormulaInvoiceDetail_MedicalFormulaDetail] FOREIGN KEY ([MedicalFormulaDetailId]) REFERENCES [Inventory].[MedicalFormulaDetail] ([Id]),
    CONSTRAINT [FK_MedicalFormulaInvoiceDetail_MedicalFormulaInvoiceId] FOREIGN KEY ([MedicalFormulaInvoiceId]) REFERENCES [Inventory].[MedicalFormulaInvoice] ([Id])
);


GO
ALTER TABLE [Inventory].[MedicalFormulaInvoiceDetail] NOCHECK CONSTRAINT [FK_MedicalFormulaInvoiceDetail_InvoiceDetailId];


GO
ALTER TABLE [Inventory].[MedicalFormulaInvoiceDetail] NOCHECK CONSTRAINT [FK_MedicalFormulaInvoiceDetail_MedicalFormulaDetail];


GO
ALTER TABLE [Inventory].[MedicalFormulaInvoiceDetail] NOCHECK CONSTRAINT [FK_MedicalFormulaInvoiceDetail_MedicalFormulaInvoiceId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla para guardar la relacion entre el detalle de la factura y el detalle de formulamedica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail';

