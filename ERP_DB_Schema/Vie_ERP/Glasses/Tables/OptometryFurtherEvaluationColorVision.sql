CREATE TABLE [Glasses].[OptometryFurtherEvaluationColorVision] (
    [Id]                             INT IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT NOT NULL,
    [Ihsihara]                       INT NOT NULL,
    [Farnsworth]                     INT NOT NULL,
    [Lanthony]                       INT NOT NULL,
    [Result]                         INT NOT NULL,
    CONSTRAINT [PK_ColorVision] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryFurtherEvaluationColorVision_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resutado general del test de color realizado: 1 Normal, 2 Anormal', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se realizó test de Lanthony: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Lanthony';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se realizó test de Farnsworth: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Farnsworth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se realizó test de Ihsihara: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Ihsihara';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de evaluación de visión cromática', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Id';

