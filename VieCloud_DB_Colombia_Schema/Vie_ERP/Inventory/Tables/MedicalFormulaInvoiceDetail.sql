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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de intersección que vincula detalles de facturas de medicamentos con líneas específicas de fórmulas médicas. Relaciona facturación (receta farmacéutica), inventario de medicamentos y detalles de prescripción médica para auditoría, glosa y trazabilidad de dispensación. Clave de referencia entre facturación y medicinas dispensadas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla para guardar la relacion entre el detalle de la factura y el detalle de formulamedica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de detalle.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la factura de fórmula médica a la que pertenece este detalle.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalFormulaInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalFormulaInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al renglón o ítem de la factura general asociado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle de la fórmula médica (medicamento o insumo recetado) vinculado a este registro de facturación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaInvoiceDetail', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';
