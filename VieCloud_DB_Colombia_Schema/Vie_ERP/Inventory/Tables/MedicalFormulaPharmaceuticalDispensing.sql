CREATE TABLE [Inventory].[MedicalFormulaPharmaceuticalDispensing] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaId]             INT          NOT NULL,
    [PharmaceuticalDispensingId]   INT          NOT NULL,
    [PharmaceuticalDispensingCode] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_MedicalFormulaPharmaceuticalDispensing_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaPharmaceuticalDispensing_MedicalFormula] FOREIGN KEY ([MedicalFormulaId]) REFERENCES [Inventory].[MedicalFormula] ([Id]),
    CONSTRAINT [FK_MedicalFormulaPharmaceuticalDispensing_PharmaceuticalDispensing] FOREIGN KEY ([PharmaceuticalDispensingId]) REFERENCES [Inventory].[PharmaceuticalDispensing] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico de dispensación farmacéutica (VARCHAR 20). Identificador único de la entrega de medicamentos en farmacia, vinculado a recetas médicas y control de inventario de fármacos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de dispensación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de dispensación farmacéutica (FK a PharmaceuticalDispensing). Referencia a la transacción de entrega de medicamentos, fármacos o productos farmacéuticos en la unidad de farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de dispensación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de fórmula médica (FK a MedicalFormula). Referencia a la prescripción o receta médica que ordena los medicamentos a dispensar en farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'MedicalFormulaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de fórmula médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'MedicalFormulaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'MedicalFormulaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY 1,1). Clave primaria de relación entre fórmula médica y dispensación farmacéutica en el sistema de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las fórmulas médicas con sus dispensaciones farmacéuticas, registrando qué medicamentos o insumos fueron despachados en farmacia para cada fórmula prescrita.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing';
