CREATE TABLE [Obstetrics].[ThirdTrimesterEvaluationParaclinical] (
    [Id]                             INT           IDENTITY (1, 1) NOT NULL,
    [IdPerinatalMaternalAssessmentC] INT           NOT NULL,
    [HIV1]                           INT           NULL,
    [HIV1Date]                       DATETIME      NULL,
    [HIV2]                           INT           NULL,
    [HIV2Date]                       DATETIME      NULL,
    [VDRL]                           INT           NULL,
    [VDRLDate]                       DATETIME      NULL,
    [Dilutions]                      INT           NULL,
    [IgMToxoplasmosis]               INT           NULL,
    [IgMToxoplasmosisDate]           DATETIME      NULL,
    [RetrovaginalCulture]            VARCHAR (50)  NULL,
    [RetrovaginalCultureDate]        DATETIME      NULL,
    [Observations]                   VARCHAR (500) NULL,
    CONSTRAINT [PK_ThirdTrimesterEvaluationOfParaclinicalTests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdTrimesterEvaluationOfParaclinicalTests_PerinatalMaternalAssessmentC] FOREIGN KEY ([IdPerinatalMaternalAssessmentC]) REFERENCES [Obstetrics].[PerinatalMaternalAssessmentC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas adicionales, hallazgos relevantes o comentarios del profesional de salud sobre los resultados paraclínicos del tercer trimestre. Texto libre (VARCHAR 500).', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización del cultivo retrovaginal (recto-vaginal), prueba para detectar Streptococcus agalactiae (GBS) u otros patógenos de transmisión vertical. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RetrovaginalCultureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Cultivo retrovaginal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RetrovaginalCultureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RetrovaginalCultureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del cultivo retrovaginal (recto-vaginal): valores 1=Positivo, 2=Negativo, 3=Indeterminado, 4=No realizado. Identifica colonización por GBS u otros organismos. VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RetrovaginalCulture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cultivo retrovaginal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RetrovaginalCulture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RetrovaginalCulture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización del ensayo de Inmunoglobulina M (IgM) para Toxoplasma, marcador de infección aguda materna. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inmuno globulina M - Toxoplasma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de IgM Toxoplasma: 1=Positivo (infección activa), 2=Negativo, 3=Indeterminado, 4=No realizado. Detecta riesgo de transmisión congénita. INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmuno globulina M - Toxoplasma      1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de diluciones realizadas en ensayos serológicos (VDRL, toxoplasmosis), indicador de carga o intensidad de anticuerpos. INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diluciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de la prueba VDRL (Venereal Disease Research Laboratory) para detección de sífilis materna. Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Serología para sífilis', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de serología VDRL: 1=Positivo (sífilis), 2=Negativo, 3=No realizado. Screening de infección sifilítica en embarazo. INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serología para sífilis       1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de prueba para Virus de Inmunodeficiencia Humana tipo 2 (VIH-2). Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Virus de la inmunodeficiencia humana', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de VIH-2: 1=Positivo, 2=Negativo, 3=No realizado. Detección de infección por VIH tipo 2 en embarazo. INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Virus de la inmunodeficiencia humana       1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de prueba para Virus de Inmunodeficiencia Humana tipo 1 (VIH-1). Tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Virus de la inmunodeficiencia humana', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de VIH-1: 1=Positivo, 2=Negativo, 3=No realizado. Detección de infección por VIH tipo 1 en embarazo. INT.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Virus de la inmunodeficiencia humana     1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena resultados de exámenes paraclínicos (pruebas de laboratorio e inmunología) realizados en el tercer trimestre de evaluación perinatal materno. Incluye serologías para infecciones (VIH, sífilis, toxoplasmosis) y cultivos vaginales de riesgo vertical.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena los datos del tercer trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Guarda los datos del tercer trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro paraclínico del tercer trimestre.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la evaluación materna perinatal a la que pertenece este conjunto de exámenes paraclínicos del tercer trimestre.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
