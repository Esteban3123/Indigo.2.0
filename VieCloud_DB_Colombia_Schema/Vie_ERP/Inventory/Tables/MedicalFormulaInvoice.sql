CREATE TABLE [Inventory].[MedicalFormulaInvoice] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaId] INT NULL,
    [InvoiceId]        INT NULL,
    CONSTRAINT [PK__MedicalF__3214EC07F2D652EC] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la factura (FK a Invoicing.Invoice). Referencia la factura de venta o cobro asociada a la fórmula médica dispensada. Tipo: INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la fórmula médica (FK a Inventory.MedicalFormula). Vincula la receta, prescripción o fórmula de medicamentos a esta factura. Tipo: INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'MedicalFormulaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la fórmula médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'MedicalFormulaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'MedicalFormulaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (IDENTITY) y clave primaria de la tabla. Identificador único de la relación entre fórmula médica e factura. Tipo: INT, NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona fórmulas médicas (recetas) con facturas de inventario. Permite saber qué factura de medicamentos está asociada a cada fórmula o receta médica dispensada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoice';
