CREATE TABLE [Glasses].[OptometryFurtherEvaluationFundus] (
    [Id]                             INT            IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT            NOT NULL,
    [Eye]                            INT            NOT NULL,
    [Type]                           INT            NOT NULL,
    [Fundus]                         VARCHAR (5000) NOT NULL,
    CONSTRAINT [PK_Fundus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryFurtherEvaluationFundus_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones específicas del grupo de fondo de ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Fundus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de fondo de ojo: 1 Directo, 2 Indirecto', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ojo al que corresponde la evaluación de fondo de ojo: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de evaluación de fondo de ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Id';

