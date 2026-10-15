CREATE TABLE [Glasses].[OptometryFurtherEvaluationPupillaryExam] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [Size]                           VARCHAR (10) NOT NULL,
    [Direct]                         VARCHAR (10) NOT NULL,
    [Consensual]                     VARCHAR (10) NOT NULL,
    [Accommodation]                  VARCHAR (10) NOT NULL,
    [DPAR]                           VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_PupillaryExam] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryFurtherEvaluationPupillaryExam_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del defecto pupilar aferente relativo (DPAR) del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'DPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la acomodación del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Accommodation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del consensual del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Consensual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del directo del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Direct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del tamaño del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ojo al que corresponde al examen pupilar: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de examen pupilar', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Id';

