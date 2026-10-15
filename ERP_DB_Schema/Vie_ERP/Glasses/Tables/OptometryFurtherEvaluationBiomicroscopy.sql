CREATE TABLE [Glasses].[OptometryFurtherEvaluationBiomicroscopy] (
    [Id]                             INT            IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT            NOT NULL,
    [Eye]                            INT            NOT NULL,
    [Biomicroscopy]                  VARCHAR (5000) NOT NULL,
    CONSTRAINT [PK_Biomicroscopy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryFurtherEvaluationBiomicroscopy_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Texto de descripción de evaluación de biomicroscopía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Biomicroscopy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ojo al que corresponde la evaluación de biomicroscopía: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de evaluación de biomicroscopía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Id';

