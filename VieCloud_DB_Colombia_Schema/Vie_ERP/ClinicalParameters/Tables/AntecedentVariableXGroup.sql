CREATE TABLE [ClinicalParameters].[AntecedentVariableXGroup] (
    [Id]                        INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats]  INT NOT NULL,
    [IdAntecedentGroupXFormats] INT NOT NULL,
    [IdHistoryVariables]        INT NOT NULL,
    [Mandatory]                 BIT NULL,
    CONSTRAINT [PK_AntecedentVariableXGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de obligatoriedad del registro (BIT). Define si la variable de antecedente es requerida/obligatoria en la captura de datos clínicos.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'Mandatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro obligatorio', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'Mandatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'Mandatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo (INT, FK) de la variable de historial clínico. Referencia a HistoryVariables: cédula, diagnóstico, procedimiento, examen, antecedente médico o quirúrgico.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo de HistoryVariables', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación (INT, FK) con AntecedentGroupXFormats. Agrupa variables de antecedentes (personales, familiares, quirúrgicos, alérgicos) en formularios clínicos.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdAntecedentGroupXFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla AntecedentGroupXFormat', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdAntecedentGroupXFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdAntecedentGroupXFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación (INT, FK) con ClinicalHistoryFormats. Vincula la variable al formulario de historia clínica de ingreso, atención o urgencia.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (INT, PK, IDENTITY). Clave primaria de la asociación antecedente-variable-grupo en formularios clínicos.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las variables de antecedentes clínicos con los grupos de antecedentes y los formatos de historia clínica, indicando qué campos deben registrarse en cada sección del formulario y si su diligenciamiento es obligatorio.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup';
