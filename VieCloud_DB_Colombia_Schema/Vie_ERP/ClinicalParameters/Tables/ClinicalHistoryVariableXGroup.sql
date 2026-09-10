CREATE TABLE [ClinicalParameters].[ClinicalHistoryVariableXGroup] (
    [Id]                          INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats]    INT NOT NULL,
    [IdClinicalHistoryGroupXPage] INT NOT NULL,
    [IdHistoryVariables]          INT NOT NULL,
    [Mandatory]                   BIT NULL,
    CONSTRAINT [PK_ClinicalHistoryVariableXGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de campo obligatorio (BIT 0/1) en la variable clínica; determina si el registro de la variable es requerido en la historia clínica durante la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'Mandatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro obligatorio', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'Mandatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'Mandatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia tabla HistoryVariables; identifica la variable clínica específica (síntoma, signo vital, hallazgo, diagnóstico, parámetro de laboratorio) que forma parte del grupo de variables.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HistoryVariables', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia tabla ClinicalHistoryGroupXPage; vincula la variable al grupo de variables dentro de la página/sección de un formato de historia clínica.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryGroupXPage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ClinicalHistoryGroupXPage', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryGroupXPage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryGroupXPage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia tabla ClinicalHistoryFormats; referencia al formato de historia clínica (plantilla de ingreso, urgencia, consulta, procedimiento) al cual pertenece esta variable.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (IDENTITY) del registro de asociación entre variable clínica, grupo de variables y página del formato de historia clínica.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo del registro', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociación entre variables clínicas y grupos dentro de un formato de historia clínica. Define qué variables (campos) pertenecen a cada grupo en cada página del formulario clínico, y si son de diligenciamiento obligatorio.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryVariableXGroup';
