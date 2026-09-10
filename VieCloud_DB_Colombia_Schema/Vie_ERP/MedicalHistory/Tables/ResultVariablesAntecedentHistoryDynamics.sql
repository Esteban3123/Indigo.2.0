CREATE TABLE [MedicalHistory].[ResultVariablesAntecedentHistoryDynamics] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]            INT            NOT NULL,
    [CodeAntecedent]         INT            NOT NULL,
    [IdHistoryVariables]     INT            NOT NULL,
    [Value]                  VARCHAR (5000) NOT NULL,
    [IdHistoryVariablesList] INT            NULL,
    CONSTRAINT [PK_ResultVariablesAntecedentHistoryDynamics] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores registrados para las variables dinámicas de los antecedentes en la historia clínica del paciente. Cada fila representa la respuesta o dato capturado para una variable específica dentro de un antecedente (personal, familiar, quirúrgico, etc.) asociado a una historia clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de valor de variable de antecedente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia clínica de antecedentes del paciente a la que pertenece este registro.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de antecedente (personal, familiar, quirúrgico, alérgico, etc.) al que corresponde la variable registrada.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'CodeAntecedent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'CodeAntecedent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la variable dinámica de historia clínica cuyo valor se está almacenando (ej: medicamento, dosis, fecha de diagnóstico previo).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o respuesta registrada para la variable del antecedente, puede contener texto libre, datos clínicos o respuestas del formulario dinámico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la opción seleccionada de una lista desplegable asociada a la variable, cuando la variable es de tipo lista o catálogo (puede ser nulo si el valor es texto libre).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'IdHistoryVariablesList';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesAntecedentHistoryDynamics', @level2type = N'COLUMN', @level2name = N'IdHistoryVariablesList';
