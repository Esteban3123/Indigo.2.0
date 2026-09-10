CREATE TABLE [PathologyALULA].[INTERPRETATION_CODES] (
    [diagnosis_result_id]     INT NOT NULL,
    [diagnosis_coding_method] INT NOT NULL,
    [diagnosis_code]          INT NOT NULL,
    CONSTRAINT [PK_INTERPRETATION_CODES] PRIMARY KEY CLUSTERED ([diagnosis_result_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico patológico; clasificación numérica del hallazgo diagnóstico (ej: CIE-10, SNOMED CT). Utilizado en reportes de patología, análisis clínicos y facturación de servicios de salud (RIPS).', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de codificación del diagnóstico; estándar o clasificación utilizada para asignar códigos diagnósticos (ej: CIE-10, SNOMED CT, nomenclatura local). Define la interpretación y estandarización de resultados patológicos.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_coding_method';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Método de codificación de diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_coding_method';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_coding_method';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del resultado diagnóstico patológico; clave consecutiva que vincula códigos y métodos de codificación a un resultado de análisis específico en el módulo de patología.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_result_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_result_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_result_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Códigos de interpretación diagnóstica en patología: relaciona cada resultado de diagnóstico con su método de codificación (por ejemplo CIE-10, SNOMED) y el código diagnóstico asignado. Se usa para estandarizar y clasificar los hallazgos de estudios de patología.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES';
