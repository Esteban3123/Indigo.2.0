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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas específicas del fondo de ojo (retina, disco óptico, vasos sanguíneos); texto descriptivo de hallazgos oftalmológicos en evaluación optométrica (VARCHAR 5000)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Fundus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones específicas del grupo de fondo de ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Fundus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Fundus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de exploración de fondo de ojo: 1=Directa (oftalmoscopio directo), 2=Indirecta (lente auxiliar); método de visualización retiniana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de fondo de ojo: 1 Directo, 2 Indirecto', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo evaluado en examen de fondo de ojo: 1=Ojo derecho (OD), 2=Ojo izquierdo (OS); lateralidad oftalmológica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo al que corresponde la evaluación de fondo de ojo: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de valoración optométrica clínica (FK a OptometryClinicalEvaluationC); agrupa evaluaciones de fondo de ojo por atención', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro de evaluación de fondo de ojo (fundoscopia); clave primaria de evaluación oftalmológica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de evaluación de fondo de ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los hallazgos del fondo de ojo (fundoscopia) obtenidos durante la evaluación clínica optométrica. Permite documentar por cada ojo examinado el tipo de hallazgo y la descripción detallada del fondo ocular.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationFundus';
