CREATE TABLE [Glasses].[OptometryExternalReviewDetail] (
    [Id]                             INT            IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT            NOT NULL,
    [HasAnophthalmia]                TINYINT        NOT NULL,
    [AffectedLateralityAnophthalmia] TINYINT        NULL,
    [AnophthalmiaObservations]       VARCHAR (100)  NULL,
    [HasBlindness]                   TINYINT        NULL,
    [AffectedLateralityBlindness]    TINYINT        NULL,
    [BlindnessObservations]          VARCHAR (100)  NULL,
    [DominantEye]                    TINYINT        NULL,
    [HasHeterochromia]               TINYINT        NULL,
    [HeterochromiaObservations]      VARCHAR (100)  NULL,
    [HasMotility]                    TINYINT        NULL,
    [AffectedLateralityMotility]     TINYINT        NULL,
    [MotilityObservations]           VARCHAR (200)  NULL,
    [HasCoverTest]                   TINYINT        NULL,
    [CoverTestResult]                TINYINT        NULL,
    [CoverTestTypeOfAbnormality]     TINYINT        NULL,
    [MaximumPointConvergence]        NUMERIC (4, 1) NULL,
    [PupillaryDistance]              NUMERIC (4, 1) NULL,
    [HasStereopsis]                  TINYINT        NULL,
    [TechniqueUsedInStereopsis]      TINYINT        NULL,
    [StereopsisArcSeconds]           SMALLINT       NULL,
    [ExternalExamination]            VARCHAR (5000) NULL,
    [RightEye]                       VARCHAR (5000) NULL,
    [LeftEye]                        VARCHAR (5000) NULL,
    [ProximityPointConvergence]      VARCHAR (250)  NULL,
    [CoverTestObservations]          VARCHAR (500)  NULL,
    CONSTRAINT [PK_OptometryExternalReviewDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryExternalReviewDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundos de arco en prueba de estereopsis (1=800s, 2=400s, 3=200s, 4=140s, 5=100s, 6=80s, 7=60s, 8=50s, 9=40s). SMALLINT. Nota: Deprecado desde 14-05-2023, migrado a Valoración Complementaria.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'StereopsisArcSeconds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina los segundos de arco: 1 - 800 seconds, 2 - 400 seconds, 3 - 200 seconds, 4 - 140 seconds, 5 - 100 seconds, 6 - 80 seconds, 7 - 60 seconds, 8 - 50 seconds, 9 - 40 seconds (14-05-2023 - Despues de la actualización San jose deja de utilizarse este campo ya que se muda del page de examen externo y se pasa a valoracion complementaria -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'StereopsisArcSeconds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'StereopsisArcSeconds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica usada para medir estereopsis/visión tridimensional (1=Tismus, 2=Random, 3=Frisby, 4=Lang). TINYINT. Nota: Deprecado desde 14-05-2023, migrado a Valoración Complementaria.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'TechniqueUsedInStereopsis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina la técnica usada para la esteriopsis: 1 Tismus, 2 Random, 3 Frisby, 4 Lang (14-05-2023 - Despues de la actualización San jose deja de utilizarse este campo ya que se muda del page de examen externo y se pasa a valoracion complementaria -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'TechniqueUsedInStereopsis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'TechniqueUsedInStereopsis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si paciente posee estereopsis/visión estéreo (1=Sí, 2=No). TINYINT. Nota: Deprecado desde 14-05-2023, migrado a Valoración Complementaria.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasStereopsis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene esteriopsis: 1 Sí, 2 No (14-05-2023 - Despues de la actualización San jose deja de utilizarse este campo ya que se muda del page de examen externo y se pasa a valoracion complementaria -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasStereopsis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasStereopsis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distancia pupilar en milímetros (mm); medida interpupilar para ajuste de lentes. NUMERIC(4,1). Odontología, oftalmología.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina la distancia pupilar (en milímetros)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto próximo de convergencia en centímetros (cm); distancia máxima de enfoque binocular. NUMERIC(4,1).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'MaximumPointConvergence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el punto próximo de convergencia (en centímetros)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'MaximumPointConvergence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'MaximumPointConvergence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de anomalía detectada en Cover Test: 1=Forias (desalineación latente), 2=Tropías (desalineación manifiesta), 3=Ambas. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestTypeOfAbnormality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el tipo de anormalidad del Cover Test: 1 Forias, 2 Tropías, 3 Ambos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestTypeOfAbnormality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestTypeOfAbnormality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de Cover Test: True=Normal/Ortofórico (alineación perfecta), False=Anormal (heteroforia/heterotropía). TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el resultado del cover test: True Normal (Ortofórico), False Anormal', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se realizó Cover Test al paciente (1=Sí, 2=No). TINYINT. Prueba de alineación ocular.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasCoverTest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene prueba de Cover Test: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasCoverTest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasCoverTest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas específicas sobre movimiento/motilidad ocular extraocular. VARCHAR(200). Restricciones, nistagmo, paresias.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'MotilityObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones específicas para la motilidad', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'MotilityObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'MotilityObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lateralidad afectada en motilidad ocular: 1=OD/Derecho, 2=OI/Izquierdo, 3=Ambos ojos. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityMotility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina la lateralidad en la que afecta la motilidad: 1 Derecho, 2 Izquierdo, 3 Ambos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityMotility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityMotility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si paciente presenta deficiencia de motilidad ocular (visco, disociación): 1=Sí, 2=No. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasMotility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene motilidad (es visco): 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasMotility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasMotility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas sobre heterocromía del iris. VARCHAR(100). Variaciones de color, parches, patrones.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HeterochromiaObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones específicas para la heterocromía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HeterochromiaObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HeterochromiaObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si paciente tiene heterocromía iridis (ojos de colores diferentes): 1=Sí, 2=No. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasHeterochromia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene heterocromía (ojos de colores diferentes entre sí): 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasHeterochromia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasHeterochromia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo dominante del paciente para enfoque preferente: 1=OD/Derecho, 2=OI/Izquierdo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'DominantEye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el ojo dominante del paciente: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'DominantEye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'DominantEye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas específicas sobre ceguera/pérdida visual. VARCHAR(100). Grado, causa, prótesis.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'BlindnessObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones específicas para la invidencia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'BlindnessObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'BlindnessObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lateralidad afectada por ceguera/invidencia: 1=OD/Derecho, 2=OI/Izquierdo, 3=Ambos. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityBlindness';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina la lateralidad en la que afecta la invidencia: 1 Derecho, 2 Izquierdo, 3 Ambos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityBlindness';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityBlindness';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si paciente presenta ceguera/invidencia (pérdida total visión): 1=Sí, 2=No. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasBlindness';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene invidencia: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasBlindness';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasBlindness';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas sobre anoftalmía. VARCHAR(100). Prótesis ocular, historial, adaptación.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AnophthalmiaObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones específicas para la anoftalmía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AnophthalmiaObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AnophthalmiaObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lateralidad afectada por anoftalmía: 1=OD/Derecho, 2=OI/Izquierdo, 3=Ambos. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityAnophthalmia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina la lateralidad en la que afecta la anoftalmía: 1 Derecho, 2 Izquierdo, 3 Ambos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityAnophthalmia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityAnophthalmia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si paciente tiene anoftalmía (ausencia congénita/adquirida de globo ocular): 1=Sí, 2=No. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasAnophthalmia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene anoftalmía (ausencia de globos oculares): 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasAnophthalmia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasAnophthalmia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) a tabla cabecera OptometryClinicalEvaluationC. INT NOT NULL. Referencia evaluación optométrica clínica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla cabecera de OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de detalle en pestaña Examen Externo Optométrico. INT IDENTITY. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla detalle de la pestaña Examen Externo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la revisión externa optométrica del paciente, incluyendo hallazgos de anoftalmia, ceguera, heterocromía, motilidad ocular, prueba de cobertura, convergencia y estereopsis registrados durante la evaluación clínica de optometría.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción narrativa de los hallazgos del examen externo ocular y de anexos (párpados, conjuntiva, córnea, etc.) observados durante la consulta optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'ExternalExamination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'ExternalExamination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y hallazgos clínicos del ojo derecho registrados en el examen externo optométrico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'RightEye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'RightEye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y hallazgos clínicos del ojo izquierdo registrados en el examen externo optométrico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'LeftEye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'LeftEye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del punto próximo de convergencia (PPC), indicando la distancia o condición en que los ojos convergen al acercar un objeto.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'ProximityPointConvergence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'ProximityPointConvergence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales y notas clínicas sobre el resultado de la prueba de cobertura (cover test) para detección de estrabismo o forias.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestObservations';
