CREATE TABLE [Obstetrics].[SecondTrimesterEvaluationParaclinical] (
    [Id]                             INT           IDENTITY (1, 1) NOT NULL,
    [IdPerinatalMaternalAssessmentC] INT           NOT NULL,
    [HIV1]                           INT           NULL,
    [HIV1Date]                       DATETIME      NULL,
    [HIV2]                           INT           NULL,
    [HIV2Date]                       DATETIME      NULL,
    [BloodCount]                     VARCHAR (50)  NULL,
    [BloodCountDate]                 DATETIME      NULL,
    [PTOG]                           VARCHAR (50)  NULL,
    [PTOGDate]                       DATETIME      NULL,
    [VDRL]                           INT           NULL,
    [VDRLDate]                       DATETIME      NULL,
    [Dilutions]                      INT           NULL,
    [IgMToxoplasmosis]               INT           NULL,
    [IgMToxoplasmosisDate]           DATETIME      NULL,
    [Observations]                   VARCHAR (500) NULL,
    CONSTRAINT [PK_SecondTrimesterEvaluationOfParaclinicalTests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SecondTrimesterEvaluationOfParaclinicalTests_PerinatalMaternalAssessmentC] FOREIGN KEY ([IdPerinatalMaternalAssessmentC]) REFERENCES [Obstetrics].[PerinatalMaternalAssessmentC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas, hallazgos relevantes o notas adicionales del segundo trimestre paraclínico; VARCHAR(500)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización del examen de Inmunoglobulina M (IgM) para detección de toxoplasmosis congénita; DATETIME', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inmuno globulina M - Toxoplasma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de Inmunoglobulina M (IgM) - Toxoplasma (indicador de infección aguda): 1) Positivo, 2) Negativo, 3) Indeterminado, 4) No realizado; INT', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmuno globulina M - Toxoplasma     1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de diluciones seriadas realizadas en pruebas serológicas (ej: VDRL); INT', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diluciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de serología VDRL (Venereal Disease Research Laboratory) para sífilis; DATETIME', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Serología para sífilis ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de serología VDRL para detección de sífilis materna: 1) Positivo, 2) Negativo, 3) No realizado; INT', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serología para sífilis     1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de Prueba de Tolerancia Oral a la Glucosa (PTOG, curva glucémica); DATETIME', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'PTOGDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Prueba de tolerancia oral a la glucosa', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'PTOGDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'PTOGDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de Prueba de Tolerancia Oral a la Glucosa (PTOG) para screening de diabetes gestacional; VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'PTOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba de tolerancia oral a la glucosa', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'PTOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'PTOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de Hemograma completo (recuento celular sanguíneo); DATETIME', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCountDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Hemograma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCountDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCountDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de Hemograma completo (glóbulos rojos, blancos, plaquetas, hemoglobina); VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemograma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de prueba de Virus de la Inmunodeficiencia Humana tipo 2 (VIH-2); DATETIME', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Virus de la inmunodeficiencia humana', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de prueba VIH-2 (Virus de Inmunodeficiencia Humana tipo 2): 1) Positivo, 2) Negativo, 3) No realizado; INT', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Virus de la inmunodeficiencia humana       1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de prueba de Virus de la Inmunodeficiencia Humana tipo 1 (VIH-1); DATETIME', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Virus de la inmunodeficiencia humana', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de prueba VIH-1 (Virus de Inmunodeficiencia Humana tipo 1): 1) Positivo, 2) Negativo, 3) No realizado; INT', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Virus de la inmunodeficiencia humana      1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena resultados de exámenes paraclínicos (laboratorio, serología, inmunología) realizados durante el segundo trimestre de evaluación perinatal materno (semanas 13-27 de gestación)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena los datos del segundo trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Guarda los datos del segundo trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de evaluación paraclínica del segundo trimestre.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la valoración materna perinatal a la que pertenece este conjunto de exámenes del segundo trimestre.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
