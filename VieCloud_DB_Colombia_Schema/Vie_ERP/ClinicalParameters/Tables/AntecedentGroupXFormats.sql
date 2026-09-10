CREATE TABLE [ClinicalParameters].[AntecedentGroupXFormats] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT NOT NULL,
    [CodeAntecedent]           INT NOT NULL,
    [OrderGroup]               INT NULL,
    CONSTRAINT [PK_AntecedentXFormats] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden secuencial que define la posición del grupo de antecedentes en la historia clínica intrahospitalaria parametrizada; determina la secuencia de visualización y organización de antecedentes en el formulario clínico configurado.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'OrderGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo vinculado al orden en el cual es agregado el grupo en la historia clinica intrahospitalaria parametrizada', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'OrderGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'OrderGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del antecedente clínico (antecedente patológico, quirúrgico, farmacológico, alérgico u ocupacional); referencia a la codificación de antecedentes del paciente en el sistema de parámetros clínicos.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'CodeAntecedent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del antecedente', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'CodeAntecedent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'CodeAntecedent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con la tabla ClinicalHistoryFormats; vincula este grupo de antecedentes a un formato específico de historia clínica parametrizado en el sistema EHR.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo de la asociación entre formato de historia clínica y grupo de antecedentes; clave primaria identity (1,1) para rastrea la relación antecedent-formato.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los grupos de antecedentes clínicos con los formatos de historia clínica, definiendo cuáles grupos de antecedentes aparecen en cada formato y en qué orden se presentan.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats';
