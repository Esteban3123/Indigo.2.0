CREATE TABLE [Inventory].[HighRiskDrugs] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [DCIId]                INT           NOT NULL,
    [InventoryRiskLevelId] INT           NOT NULL,
    [Observation]          VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_HighRiskDrugs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [fk_InventoryRiskLevelId] FOREIGN KEY ([InventoryRiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación, comentario o nota clínica sobre el medicamento de alto riesgo; almacena restricciones, contraindicaciones, advertencias de uso o datos relevantes para la dispensación segura (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del nivel de riesgo del medicamento en inventario; referencia a tabla InventoryRiskLevel para clasificar drogas de alto riesgo (críticas, controladas, psicotrópicas, etc.)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla: [Inventory].[InventoryRiskLevel]  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la Denominación Común Internacional (DCI/principio activo); referencia a tabla Inventory.DCI que vincula el medicamento específico de riesgo con su código farmacológico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY INT), consecutivo automático de la tabla de medicamentos de alto riesgo; usado para auditoría, trazabilidad y control regulatorio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de medicamentos de alto riesgo según su denominación común internacional (DCI), asociados a un nivel de riesgo de inventario y con observaciones de seguridad. Permite identificar qué fármacos requieren controles especiales por su peligrosidad o potencial de error en dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs';
