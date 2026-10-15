CREATE TABLE [Glasses].[OptometryFurtherEvaluationGonioscopy] (
    [Id]                             INT                                                                           IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT                                                                           NOT NULL,
    [Eye]                            INT                                                                           NOT NULL,
    [Gonioscopy]                     VARCHAR (500) MASKED WITH (FUNCTION = 'partial(0, "Gonioscopy_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_Gonioscopy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryFurtherEvaluationGonioscopy_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Texto de descripción de evaluación de gonioscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Gonioscopy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ojo al que corresponde la evaluación de gonioscopia: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de evaluación de gonioscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Id';

