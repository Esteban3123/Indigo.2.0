CREATE TABLE [Glasses].[OptometryFurtherEvaluationGonioscopy] (
    [Id]                             INT                                                                           IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT                                                                           NOT NULL,
    [Eye]                            INT                                                                           NOT NULL,
    [Gonioscopy]                     VARCHAR (500) MASKED WITH (FUNCTION = 'partial(0, "Gonioscopy_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_Gonioscopy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryFurtherEvaluationGonioscopy_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Glasses].[OptometryFurtherEvaluationGonioscopy].[Gonioscopy]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo de hallazgos en evaluación de gonioscopia (examen del ángulo de la cámara anterior del ojo), datos clínicos PII ofuscados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Gonioscopy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Texto de descripción de evaluación de gonioscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Gonioscopy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Gonioscopy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo evaluado: 1=Ojo Derecho (OD), 2=Ojo Izquierdo (OI) en gonioscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo al que corresponde la evaluación de gonioscopia: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera (clave foránea) que vincula a la evaluación clínica optométrica principal (OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de evaluación de gonioscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de evaluación de gonioscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los resultados de la gonioscopía (examen del ángulo iridocorneal) obtenidos durante la evaluación clínica optométrica, indicando el ojo evaluado y los hallazgos del procedimiento.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationGonioscopy';
