CREATE TABLE [Inventory].[MedicalFormulaDetailAnnulled] (
    [Id]                     INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaDetailId] INT      NOT NULL,
    [AnnulmentQuantity]      INT      NOT NULL,
    [AnnulmentUser]          INT      NOT NULL,
    [AnnulmentDate]          DATETIME NOT NULL,
    CONSTRAINT [PK_MedicalFormulaDetailAnnulled] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaDetailAnnulled_MedicalFormulaDetail] FOREIGN KEY ([MedicalFormulaDetailId]) REFERENCES [Inventory].[MedicalFormulaDetail] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realiza la anulación del detalle de la fórmula médica (receta). Tipo: DATETIME. Rastro de auditoría para medicamentos anulados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se realiza la aunlación del detalle de la fórmula médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario (profesional de salud, farmacéutico, administrador) que realiza la anulación del detalle de fórmula. FK a tabla de usuarios. Auditoría PII.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que esta realizando la anulación del detalle de la fórmula médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del medicamento/producto que se anula en el detalle de la fórmula. Tipo: INT. Controla inventario de medicamentos dispensados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto que se esta anulando', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de fórmula médica (línea de receta) que se está anulando. FK a [Inventory].[MedicalFormulaDetail]. Vinculación a medicamento, dosis, cantidad original.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del detalle de la fórmula médica que se esta anulando', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (identity) del registro de anulación de detalle de fórmula médica. PK. Cada anulación genera un registro de auditoría independiente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del detalle de la fórmula médica anulada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de anulaciones parciales o totales de ítems en fórmulas médicas: guarda qué cantidad fue anulada, quién realizó la anulación y cuándo, permitiendo trazabilidad de medicamentos o insumos retirados de una orden médica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled';
