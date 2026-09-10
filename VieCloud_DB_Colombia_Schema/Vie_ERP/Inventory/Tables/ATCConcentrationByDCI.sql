CREATE TABLE [Inventory].[ATCConcentrationByDCI] (
    [Id]                         INT             IDENTITY (1, 1) NOT NULL,
    [AtcId]                      INT             NOT NULL,
    [DCIId]                      INT             NOT NULL,
    [Concentration]              DECIMAL (18, 2) NOT NULL,
    [ConcentrationMeasureUnitId] INT             NOT NULL,
    CONSTRAINT [PK__ATCConce__3214EC07661A1BF3] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ATCConcentrationByDCI_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_ATCConcentrationByDCI_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_ATCConcentrationByDCI_UnitMeasure] FOREIGN KEY ([ConcentrationMeasureUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [IX_ATCConcentrationByDCI] UNIQUE NONCLUSTERED ([AtcId] ASC, [DCIId] ASC)
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida de concentración (mg, mcg, %, mL, etc.). Referencia a tabla InventoryMeasurementUnit. Unidad en que se expresa la potencia o dosis del principio activo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasureUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de la concentración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasureUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasureUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico decimal (18,2) de la concentración del DCI en el medicamento. Ejemplo: en ACETAMINOFEN + CODEINA, almacena 500 para paracetamol y 8 para codeína. Potencia o cantidad del principio activo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración del DCI que compone el medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Denominador Común Internacional (DCI/INN) que compone el medicamento. Medicamentos combinados generan múltiples registros. Ejemplo: ACETAMINOFEN + CODEINA tiene dos DCIId (uno por ACETAMINOFEN, otro por CODEINA). Referencia a tabla DCI.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de los DCI que componen el medicamento  Ejemplo:  Para el medicamento ACETAMINOFEN + CODEINA el DCI se encuentra combinado, por lo cual se guardan dos registros:  1. El DCI del ACETAMINOFEN   2. El DCI de la CODEINA ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del clasificador ATC (Anatomical Therapeutic Chemical) del medicamento. Código que agrupa medicamentos por principio activo y uso terapéutico. Referencia a tabla ATC. Clave de la composición farmacéutica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del medicamento compuesto por más de un DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) que distingue cada combinación único de ATC-DCI-Concentración en la tabla. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentraciones de principios activos (DCI) asociadas a códigos ATC en el inventario de medicamentos. Permite conocer la dosis o concentración de cada denominación común internacional dentro de su clasificación terapéutica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI';
