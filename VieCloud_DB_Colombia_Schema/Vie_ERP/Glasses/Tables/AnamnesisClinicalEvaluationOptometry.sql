CREATE TABLE [Glasses].[AnamnesisClinicalEvaluationOptometry] (
    [Id]                            INT        IDENTITY (1, 1) NOT NULL,
    [IdClinicalEvaluationOptometry] INT        NOT NULL,
    [Code]                          CHAR (10)  NOT NULL,
    [OtherDescription]              CHAR (200) NULL,
    CONSTRAINT [PK_AnamnesisClinicalEvaluationOptometry] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnamnesisClinicalEvaluationOptometry_ClinicalEvaluationOptometry] FOREIGN KEY ([IdClinicalEvaluationOptometry]) REFERENCES [Glasses].[ClinicalEvaluationOptometry] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional de antecedentes clínicos en evaluación optométrica; almacena observaciones complementarias, síntomas, antecedentes oftalmológicos o datos relevantes no capturados en campos estructurados (CHAR 200, nullable)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'OtherDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda otra descripción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'OtherDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'OtherDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la anamnesis clínica en evaluación optométrica; clasificación o referencia del tipo de antecedente, síntoma o hallazgo registrado (CHAR 10, requerido)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el código', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea hacia tabla ClinicalEvaluationOptometry; vincula este registro de anamnesis con la evaluación oftalmológica/optométrica del paciente (INT, FK, requerido)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdClinicalEvaluationOptometry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ClinicalEvaluationOptometry', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdClinicalEvaluationOptometry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdClinicalEvaluationOptometry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (identity) del registro de anamnesis clínica en evaluación optométrica (INT, PK, IDENTITY 1,1)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes de anamnesis registrados en una evaluación clínica de optometría. Cada registro asocia un código de antecedente (síntoma, motivo de consulta u historial previo) a una evaluación óptica específica, con posibilidad de agregar una descripción libre adicional.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry';
