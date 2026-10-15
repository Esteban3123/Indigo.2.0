CREATE TABLE [Glasses].[AnamnesisClinicalEvaluationOptometry] (
    [Id]                            INT        IDENTITY (1, 1) NOT NULL,
    [IdClinicalEvaluationOptometry] INT        NOT NULL,
    [Code]                          CHAR (10)  NOT NULL,
    [OtherDescription]              CHAR (200) NULL,
    CONSTRAINT [PK_AnamnesisClinicalEvaluationOptometry] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnamnesisClinicalEvaluationOptometry_ClinicalEvaluationOptometry] FOREIGN KEY ([IdClinicalEvaluationOptometry]) REFERENCES [Glasses].[ClinicalEvaluationOptometry] ([Id])
);


GO
ALTER TABLE [Glasses].[AnamnesisClinicalEvaluationOptometry] NOCHECK CONSTRAINT [FK_AnamnesisClinicalEvaluationOptometry_ClinicalEvaluationOptometry];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda otra descripción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'OtherDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el código', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ClinicalEvaluationOptometry', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdClinicalEvaluationOptometry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'AnamnesisClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';

