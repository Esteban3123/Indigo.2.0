CREATE TABLE [MedicalHistory].[AssertivenessRecord] (
    [RecordID]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatedOn]            DATETIME       DEFAULT (getdate()) NULL,
    [ProfessionalUserCode] INT            NOT NULL,
    [PromptName]           NVARCHAR (255) NOT NULL,
    [PromptResponse]       NVARCHAR (MAX) NOT NULL,
    [Assertive]            BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([RecordID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de asertividad clínica: almacena las respuestas de profesionales de la salud a preguntas o indicaciones (prompts) de evaluación, junto con el resultado de si la respuesta fue considerada asertiva o no.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asertividad.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'RecordID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'RecordID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se generó el registro, momento de la evaluación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'CreatedOn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'CreatedOn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que respondió la pregunta; identifica al usuario clínico evaluado.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'ProfessionalUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'ProfessionalUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o título de la pregunta o indicación clínica presentada al profesional.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'PromptName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'PromptName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta escrita que el profesional proporcionó al prompt o indicación clínica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'PromptResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'PromptResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si la respuesta del profesional fue asertiva (1 = sí fue asertiva, 0 = no fue asertiva).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'Assertive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AssertivenessRecord', @level2type = N'COLUMN', @level2name = N'Assertive';
