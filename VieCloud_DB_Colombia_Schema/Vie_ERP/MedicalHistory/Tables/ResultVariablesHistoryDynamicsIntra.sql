CREATE TABLE [MedicalHistory].[ResultVariablesHistoryDynamicsIntra] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]            INT            NOT NULL,
    [IdHistoryPages]         INT            NOT NULL,
    [IdHistoryGroups]        INT            NOT NULL,
    [IdHistoryVariables]     INT            NOT NULL,
    [Value]                  VARCHAR (5000) NULL,
    [IdHistoryVariablesList] INT            NULL,
    CONSTRAINT [PK_ResultVariablesHistoryDynamicsIntra] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los valores de las variables clínicas dinámicas capturadas dentro de la historia clínica intrahospitalaria del paciente. Cada fila representa la respuesta o dato ingresado para una variable específica dentro de una página y grupo del formulario de historia clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de valor de variable clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia clínica intrahospitalaria del paciente a la que pertenece este registro.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Página del formulario de historia clínica donde se encuentra la variable registrada.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryPages';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryPages';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo o sección dentro de la página del formulario de historia clínica al que pertenece la variable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variable clínica específica del formulario (signo vital, síntoma, hallazgo, pregunta clínica) a la que corresponde el valor registrado.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o respuesta ingresada para la variable clínica; puede ser texto libre, número, fecha u otro dato clínico capturado en la historia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la opción seleccionada de una lista desplegable o catálogo de valores predefinidos para la variable clínica, cuando aplica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryVariablesList';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ResultVariablesHistoryDynamicsIntra', @level2type = N'COLUMN', @level2name = N'IdHistoryVariablesList';
