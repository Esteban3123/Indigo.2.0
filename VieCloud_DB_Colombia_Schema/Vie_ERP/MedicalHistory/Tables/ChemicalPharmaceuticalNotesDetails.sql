CREATE TABLE [MedicalHistory].[ChemicalPharmaceuticalNotesDetails] (
    [Id]                            INT       IDENTITY (1, 1) NOT NULL,
    [ChemicalPharmaceuticalNotesId] INT       NOT NULL,
    [CODPRODUC]                     CHAR (20) NOT NULL,
    CONSTRAINT [PK_ChemicalPharmaceuticalNotesDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto químico-farmacéutico (medicamento, fármaco, sustancia activa). Identificador único del producto registrado en inventario o catálogo de medicamentos. Tipo: CHAR(20), clave para trazabilidad de prescripciones y dispensación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo de producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la nota química-farmacéutica (nota de medicación, registro farmacológico). Referencia a la nota padre que agrupa detalles de medicamentos prescritos, dispensados o administrados al paciente. Tipo: INT, FK.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'ChemicalPharmaceuticalNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el quimico famaceutico Notas ID', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'ChemicalPharmaceuticalNotesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'ChemicalPharmaceuticalNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (consecutivo, número de línea) del detalle de medicamento en la nota química-farmacéutica. Clave primaria de la tabla. Tipo: INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los productos o medicamentos asociados a las notas químico-farmacéuticas de la historia clínica. Registra cada ítem (fármaco, insumo o preparado) vinculado a una nota de farmacia para un paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails';
