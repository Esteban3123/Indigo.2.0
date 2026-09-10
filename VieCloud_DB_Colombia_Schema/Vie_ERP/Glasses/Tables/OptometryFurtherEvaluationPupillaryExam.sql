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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Defecto Pupilar Aferente Relativo (DPAR): hallazgo oftalmológico de asimetría pupilar ante estímulo luminoso, indicador de neuropatía óptica. VARCHAR(10), valor cualitativo/cuantitativo del ojo examinado.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'DPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del defecto pupilar aferente relativo (DPAR) del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'DPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'DPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Capacidad de acomodación pupilar del ojo: respuesta dinámica de la pupila ante cambios de distancia focal (visión cercana-lejana). VARCHAR(10), medida cualitativa o grado de respuesta acomodativa.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Accommodation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la acomodación del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Accommodation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Accommodation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reflejo pupilar consensual (indirecto): contracción de la pupila del ojo no estimulado cuando se ilumina el contralateral. VARCHAR(10), patrón de respuesta pupilar bilateral.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Consensual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del consensual del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Consensual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Consensual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reflejo pupilar directo: contracción inmediata de la pupila al estimularla con luz en el mismo ojo. VARCHAR(10), medida de respuesta fotomotora directa.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Direct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del directo del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Direct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Direct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tamaño o diámetro pupilar basal del ojo en condiciones de luz estándar (midriasis-miosis). VARCHAR(10), medida morfológica de la pupila en milímetros o escala cualitativa.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Size';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del tamaño del ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Size';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lateralidad del ojo examinado en evaluación pupilar: 1=Ojo Derecho (OD), 2=Ojo Izquierdo (OI). INT, identificador binario de ojo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo al que corresponde al examen pupilar: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera de valoración optométrica clínica parent: referencia a OptometryClinicalEvaluationC. INT, clave foránea de evaluación oftalmológica principal.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de valoración OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de examen pupilar. INT IDENTITY, clave primaria de examen de pupilas.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de examen pupilar', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los resultados del examen pupilar realizado durante la evaluación optométrica adicional del paciente, incluyendo el tamaño de la pupila y sus respuestas reflejas para cada ojo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryFurtherEvaluationPupillaryExam';
