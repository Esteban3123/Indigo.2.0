CREATE TABLE [Glasses].[DetailClinicalEvaluationOptometry] (
    [Id]                                               INT        IDENTITY (1, 1) NOT NULL,
    [IdClinicalEvaluationOptometry]                    INT        NOT NULL,
    [RightEyeUncorrectedFarVision]                     CHAR (20)  NULL,
    [RightEyeUncorrectedProximalVision]                CHAR (20)  NULL,
    [LeftEyeUncorrectedFarVision]                      CHAR (20)  NULL,
    [LeftEyeUncorrectedProximalVision]                 CHAR (20)  NULL,
    [RightEyeSphereVisualAcuityWithCorrection]         CHAR (10)  NULL,
    [RightEyeCylinderVisualAcuityWithCorrection]       CHAR (10)  NULL,
    [RightEyeAxisVisualAcuityWithCorrection]           CHAR (10)  NULL,
    [RightEyeAv20VisualAcuityWithCorrection]           CHAR (10)  NULL,
    [RightEyeAdditionVisualAcuityWithCorrection]       CHAR (10)  NULL,
    [RightEyePrismsVisualAcuityWithCorrection]         CHAR (10)  NULL,
    [RightEyeProximalVisionVisualAcuityWithCorrection] CHAR (10)  NULL,
    [LeftEyeSphereVisualAcuityWithCorrection]          CHAR (10)  NULL,
    [LeftEyeCylinderVisualAcuityWithCorrection]        CHAR (10)  NULL,
    [LeftEyeAxisVisualAcuityWithCorrection]            CHAR (10)  NULL,
    [LeftEyeAv20VisualAcuityWithCorrection]            CHAR (10)  NULL,
    [LeftEyeAdditionVisualAcuityWithCorrection]        CHAR (10)  NULL,
    [LeftEyePrismsVisualAcuityWithCorrection]          CHAR (10)  NULL,
    [LeftEyeProximalVisionVisualAcuityWithCorrection]  CHAR (10)  NULL,
    [RightEyeKeratometryRefraction]                    CHAR (10)  NULL,
    [RightEyeWithoutRetinoscopyRefraction]             CHAR (10)  NULL,
    [RightEyeWithCyclopegiaFarVisionRefraction]        CHAR (10)  NULL,
    [LeftEyeKeratometryRefraction]                     CHAR (10)  NULL,
    [LeftEyeWithoutRetinoscopyRefraction]              CHAR (10)  NULL,
    [LeftEyeWithCyclopegiaFarVisionRefraction]         CHAR (10)  NULL,
    [RightEyeSubjective]                               CHAR (300) NULL,
    [LeftEyeSubjective]                                CHAR (300) NULL,
    [RightEyeVLSubjective]                             CHAR (300) NULL,
    [LeftEyeVLSubjective]                              CHAR (300) NULL,
    [RightEyeMovements]                                BIT        NULL,
    [LeftEyeMovements]                                 BIT        NULL,
    [ColorTest]                                        BIT        NULL,
    [StereopsisVision]                                 CHAR (20)  NULL,
    [Phorias]                                          CHAR (200) NOT NULL,
    [Tropies]                                          CHAR (200) NOT NULL,
    [Observations]                                     CHAR (200) NULL,
    CONSTRAINT [PK_DetailClinicalEvaluationOptometry] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DetailClinicalEvaluationOptometry_ClinicalEvaluationOptometry] FOREIGN KEY ([IdClinicalEvaluationOptometry]) REFERENCES [Glasses].[ClinicalEvaluationOptometry] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas adicionales del examen optométrico; notas y hallazgos relevantes (CHAR 200)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tropías u oculomotricidad; desviación ocular manifiesta en reposo o movimiento (CHAR 200)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Tropies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tropical', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Tropies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Tropies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forias u heteroforia; desviación ocular latente detectada en examen optométrico (CHAR 200)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Phorias';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forias', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Phorias';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Phorias';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión estereoscópica o percepción de profundidad; capacidad de visión binocular (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'StereopsisVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visión estereopsis', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'StereopsisVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'StereopsisVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba de color o test cromático; evaluación de discriminación cromática y daltonismo (BIT)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'ColorTest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba de color', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'ColorTest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'ColorTest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos oculares del ojo izquierdo; seguimiento y sacádicos evaluados (BIT)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeMovements';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Movimientos del ojo izquierda', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeMovements';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeMovements';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos oculares del ojo derecho; seguimiento y sacádicos evaluados (BIT)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeMovements';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Movimientos del ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeMovements';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeMovements';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación subjetiva de visión de cerca (VL) ojo izquierdo; refracción por patient feedback (CHAR 300)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeVLSubjective';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Movimientos del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeVLSubjective';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeVLSubjective';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación subjetiva de visión de cerca (VL) ojo derecho; refracción por paciente (CHAR 300)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeVLSubjective';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo derecho VL Subjetivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeVLSubjective';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeVLSubjective';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta subjetiva del paciente ojo izquierdo; manifestaciones y síntomas referidos (CHAR 300)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeSubjective';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo izquierdo subjetivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeSubjective';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeSubjective';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta subjetiva del paciente ojo derecho; manifestaciones y síntomas referidos (CHAR 300)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeSubjective';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo derecho subjetivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeSubjective';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeSubjective';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción ciclopléjica ojo izquierdo visión lejana; cicloplejía para neutralizar acomodación (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeWithCyclopegiaFarVisionRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo izquierdo con cicloplejía y refracción de visión lejana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeWithCyclopegiaFarVisionRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeWithCyclopegiaFarVisionRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción ojo izquierdo sin retinoscopia; método subjetivo o autorefractor (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeWithoutRetinoscopyRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Refracción del ojo izquierdo sin retinoscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeWithoutRetinoscopyRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeWithoutRetinoscopyRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Queratometría ojo izquierdo; medida de curvatura corneal para cálculo refractivo (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeKeratometryRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Refracción de queratometría del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeKeratometryRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeKeratometryRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción ciclopléjica ojo derecho visión lejana; cicloplejía para aislamiento acomodativo (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeWithCyclopegiaFarVisionRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ojo derecho con cicloplejía y refracción de visión lejana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeWithCyclopegiaFarVisionRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeWithCyclopegiaFarVisionRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción ojo derecho sin retinoscopia; método subjetivo o autorefractor (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeWithoutRetinoscopyRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Refracción del ojo derecho sin retinoscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeWithoutRetinoscopyRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeWithoutRetinoscopyRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Queratometría ojo derecho; medida de curvatura corneal para cálculo refractivo (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeKeratometryRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Refracción de queratometría del ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeKeratometryRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeKeratometryRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual proximal (cerca) ojo izquierdo con corrección; visión de lectura (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeProximalVisionVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual con corrección en la visión proximal del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeProximalVisionVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeProximalVisionVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prismas ojo izquierdo con corrección; compensación de desviaciones oculares (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyePrismsVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prismas del ojo izquierdo para medir la agudeza visual con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyePrismsVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyePrismsVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adición presbíope (add) ojo izquierdo con corrección; poder bifocal o progresivo (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAdditionVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual adicional del ojo izquierdo con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAdditionVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAdditionVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual 20/20 ojo izquierdo con corrección; visión óptima corregida (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAv20VisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual con corrección Av 20 en ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAv20VisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAv20VisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje cilíndrico ojo izquierdo con corrección; orientación del astigmatismo (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAxisVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual del eje del ojo izquierdo con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAxisVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAxisVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cilindro (poder astigmático) ojo izquierdo con corrección (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeCylinderVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual con corrección del cilindro del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeCylinderVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeCylinderVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esfera (poder refractivo) ojo izquierdo con corrección; miopía o hipermetropía (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeSphereVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual de la esfera del ojo izquierdo con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeSphereVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeSphereVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual proximal (cerca) ojo derecho con corrección; visión de lectura (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeProximalVisionVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual con corrección en la visión proximal del ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeProximalVisionVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeProximalVisionVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prismas ojo derecho con corrección; compensación de desviaciones oculares (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyePrismsVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prismas del ojo derecho para medir la agudeza visual con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyePrismsVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyePrismsVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adición presbíope (add) ojo derecho con corrección; poder bifocal o progresivo (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAdditionVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual adicional del ojo derecho con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAdditionVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAdditionVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual 20/20 ojo derecho con corrección; visión óptima corregida (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAv20VisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual con corrección Av 20 en el ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAv20VisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAv20VisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje cilíndrico ojo derecho con corrección; orientación del astigmatismo (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAxisVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual del eje del ojo derecho con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAxisVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAxisVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cilindro (poder astigmático) ojo derecho con corrección (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeCylinderVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual del cilindro del ojo derecho con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeCylinderVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeCylinderVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esfera (poder refractivo) ojo derecho con corrección; miopía o hipermetropía (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeSphereVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual de la esfera del ojo derecho con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeSphereVisualAcuityWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeSphereVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión proximal (cerca) sin corrección ojo izquierdo; evaluación de presbicia (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeUncorrectedProximalVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visión proximal no corregida del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeUncorrectedProximalVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeUncorrectedProximalVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión lejana sin corrección ojo izquierdo; agudeza natural no compensada (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeUncorrectedFarVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visión lejana sin corrección en el ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeUncorrectedFarVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeUncorrectedFarVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión proximal (cerca) sin corrección ojo derecho; evaluación de presbicia (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeUncorrectedProximalVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visión proximal no corregida del ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeUncorrectedProximalVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeUncorrectedProximalVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión lejana sin corrección ojo derecho; agudeza natural no compensada (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeUncorrectedFarVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Visión lejana sin corrección en el ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeUncorrectedFarVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeUncorrectedFarVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a ClinicalEvaluationOptometry; agrupa detalles refractivos de una evaluación optométrica (INT, PK)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdClinicalEvaluationOptometry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ClinicalEvaluationOptometry', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdClinicalEvaluationOptometry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdClinicalEvaluationOptometry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único incremental; clave primaria del detalle clínico optométrico (INT, IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la evaluación clínica optométrica del paciente: registra los resultados de agudeza visual sin corrección, con corrección óptica, refracción, queratometría, movimientos oculares, test de color, estereopsis, forias y tropías para ojo derecho e izquierdo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry';
