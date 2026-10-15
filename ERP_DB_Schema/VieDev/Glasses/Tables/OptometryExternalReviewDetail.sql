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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina los segundos de arco: 1 - 800 seconds, 2 - 400 seconds, 3 - 200 seconds, 4 - 140 seconds, 5 - 100 seconds, 6 - 80 seconds, 7 - 60 seconds, 8 - 50 seconds, 9 - 40 seconds (14-05-2023 - Despues de la actualización San jose deja de utilizarse este campo ya que se muda del page de examen externo y se pasa a valoracion complementaria -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'StereopsisArcSeconds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina la técnica usada para la esteriopsis: 1 Tismus, 2 Random, 3 Frisby, 4 Lang (14-05-2023 - Despues de la actualización San jose deja de utilizarse este campo ya que se muda del page de examen externo y se pasa a valoracion complementaria -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'TechniqueUsedInStereopsis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene esteriopsis: 1 Sí, 2 No (14-05-2023 - Despues de la actualización San jose deja de utilizarse este campo ya que se muda del page de examen externo y se pasa a valoracion complementaria -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasStereopsis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina la distancia pupilar (en milímetros)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el punto próximo de convergencia (en centímetros)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'MaximumPointConvergence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el tipo de anormalidad del Cover Test: 1 Forias, 2 Tropías, 3 Ambos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestTypeOfAbnormality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el resultado del cover test: True Normal (Ortofórico), False Anormal', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'CoverTestResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene prueba de Cover Test: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasCoverTest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones específicas para la motilidad', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'MotilityObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina la lateralidad en la que afecta la motilidad: 1 Derecho, 2 Izquierdo, 3 Ambos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityMotility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene motilidad (es visco): 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasMotility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones específicas para la heterocromía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HeterochromiaObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene heterocromía (ojos de colores diferentes entre sí): 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasHeterochromia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el ojo dominante del paciente: 1 Derecho, 2 Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'DominantEye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones específicas para la invidencia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'BlindnessObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina la lateralidad en la que afecta la invidencia: 1 Derecho, 2 Izquierdo, 3 Ambos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityBlindness';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene invidencia: 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasBlindness';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones específicas para la anoftalmía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AnophthalmiaObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina la lateralidad en la que afecta la anoftalmía: 1 Derecho, 2 Izquierdo, 3 Ambos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'AffectedLateralityAnophthalmia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene anoftalmía (ausencia de globos oculares): 1 Sí, 2 No', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'HasAnophthalmia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla cabecera de OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla detalle de la pestaña Examen Externo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetail', @level2type = N'COLUMN', @level2name = N'Id';

