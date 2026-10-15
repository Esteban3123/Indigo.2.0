CREATE TABLE [Glasses].[OptometryFurtherEvaluationTonometry] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [Flattening]                     VARCHAR (10) NOT NULL,
    [Digital]                        VARCHAR (10) NOT NULL,
    [Another]                        VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_Tonometry] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryFurtherEvaluationTonometry_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de Otros del ojo en la evaluación de tonometría', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationTonometry', @level2type = N'COLUMN', @level2name = N'Another';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del digital del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationTonometry', @level2type = N'COLUMN', @level2name = N'Digital';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la aplanación del ojo en milímetros de mercurio (mmHg)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationTonometry', @level2type = N'COLUMN', @level2name = N'Flattening';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ojo al que corresponde la evaluación de tonometría: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationTonometry', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationTonometry', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de evaluación de tonometría', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationTonometry', @level2type = N'COLUMN', @level2name = N'Id';

