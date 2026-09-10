CREATE TABLE [Glasses].[OptometryFurtherEvaluationBiomicroscopy] (
    [Id]                             INT            IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT            NOT NULL,
    [Eye]                            INT            NOT NULL,
    [Biomicroscopy]                  VARCHAR (5000) NOT NULL,
    CONSTRAINT [PK_Biomicroscopy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryFurtherEvaluationBiomicroscopy_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo detallado de hallazgos en evaluación de biomicroscopía (examen con lámpara de hendidura); observaciones clínicas de segmento anterior ocular, córnea, cristalino, cámara anterior, iris. VARCHAR(5000).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Biomicroscopy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Texto de descripción de evaluación de biomicroscopía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Biomicroscopy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Biomicroscopy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo evaluado: 1=Ojo Derecho (OD), 2=Ojo Izquierdo (OI). Identificador lateral de la biomicroscopía oftalmológica. INT, valores 1-2.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo al que corresponde la evaluación de biomicroscopía: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación (FK) a cabecera de valoración optométrica clínica (OptometryClinicalEvaluationC). Agrupa múltiples evaluaciones de biomicroscopía por ojo bajo un mismo paciente/atención. INT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) de registro de evaluación de biomicroscopía. INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de evaluación de biomicroscopía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los hallazgos de biomicroscopía (lámpara de hendidura) obtenidos durante la evaluación clínica optométrica, diferenciando el ojo evaluado. Permite documentar el estado del segmento anterior del ojo por paciente en cada consulta.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationBiomicroscopy';
