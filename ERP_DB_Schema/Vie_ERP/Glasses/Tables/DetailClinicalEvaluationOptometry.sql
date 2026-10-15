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
ALTER TABLE [Glasses].[DetailClinicalEvaluationOptometry] NOCHECK CONSTRAINT [FK_DetailClinicalEvaluationOptometry_ClinicalEvaluationOptometry];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tropical', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Tropies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forias', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Phorias';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión estereopsis', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'StereopsisVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba de color', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'ColorTest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos del ojo izquierda', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeMovements';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos del ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeMovements';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeVLSubjective';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo derecho VL Subjetivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeVLSubjective';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo izquierdo subjetivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeSubjective';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo derecho subjetivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeSubjective';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo izquierdo con cicloplejía y refracción de visión lejana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeWithCyclopegiaFarVisionRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción del ojo izquierdo sin retinoscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeWithoutRetinoscopyRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción de queratometría del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeKeratometryRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo derecho con cicloplejía y refracción de visión lejana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeWithCyclopegiaFarVisionRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción del ojo derecho sin retinoscopia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeWithoutRetinoscopyRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción de queratometría del ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeKeratometryRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual con corrección en la visión proximal del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeProximalVisionVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prismas del ojo izquierdo para medir la agudeza visual con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyePrismsVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual adicional del ojo izquierdo con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAdditionVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual con corrección Av 20 en ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAv20VisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual del eje del ojo izquierdo con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeAxisVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual con corrección del cilindro del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeCylinderVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual de la esfera del ojo izquierdo con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeSphereVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual con corrección en la visión proximal del ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeProximalVisionVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prismas del ojo derecho para medir la agudeza visual con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyePrismsVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual adicional del ojo derecho con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAdditionVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual con corrección Av 20 en el ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAv20VisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual del eje del ojo derecho con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeAxisVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual del cilindro del ojo derecho con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeCylinderVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual de la esfera del ojo derecho con corrección', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeSphereVisualAcuityWithCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión proximal no corregida del ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeUncorrectedProximalVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión lejana sin corrección en el ojo izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'LeftEyeUncorrectedFarVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión proximal no corregida del ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeUncorrectedProximalVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visión lejana sin corrección en el ojo derecho', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'RightEyeUncorrectedFarVision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ClinicalEvaluationOptometry', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'IdClinicalEvaluationOptometry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'DetailClinicalEvaluationOptometry', @level2type = N'COLUMN', @level2name = N'Id';

