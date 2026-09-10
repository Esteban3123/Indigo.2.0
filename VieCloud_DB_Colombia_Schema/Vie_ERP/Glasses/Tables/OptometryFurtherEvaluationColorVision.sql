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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado general del test de visión cromática/daltonismo realizado: 1=Normal, 2=Anormal. Indica si el paciente presenta capacidad normal o deficiencia en la percepción del color.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resutado general del test de color realizado: 1 Normal, 2 Anormal', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del test de Lanthony (prueba de discriminación cromática desaturada): 1=Sí se realizó, 2=No se realizó. Evaluación de daltonismo adquirido o deficiencia de visión de color.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Lanthony';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se realizó test de Lanthony: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Lanthony';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Lanthony';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del test de Farnsworth (prueba de arreglo de colores para diagnóstico de daltonismo): 1=Sí se realizó, 2=No se realizó. Test complementario de visión cromática.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Farnsworth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se realizó test de Farnsworth: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Farnsworth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Farnsworth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del test de Ishihara (prueba estándar de placas pseudoisocromáticas para detección de daltonismo): 1=Sí se realizó, 2=No se realizó. Screening principal de deficiencia rojo-verde.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Ihsihara';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se realizó test de Ihsihara: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Ihsihara';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Ihsihara';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que vincula esta evaluación de color a su registro cabecera de valoración optométrica clínica. Identificador único de la evaluación optométrica padre.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de evaluación de visión cromática/pruebas de daltonismo. Clave primaria autonumérica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de evaluación de visión cromática', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de evaluaciones adicionales de visión del color realizadas durante la consulta optométrica, incluyendo las pruebas de Ishihara, Farnsworth y Lanthony para detectar deficiencias en la percepción cromática del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationColorVision';
