CREATE TABLE [Obstetrics].[FirstTrimesterEvaluationParaclinical] (
    [Id]                             INT             IDENTITY (1, 1) NOT NULL,
    [IdPerinatalMaternalAssessmentC] INT             NOT NULL,
    [UrineCulture]                   VARCHAR (50)    NULL,
    [UrineCultureDate]               DATETIME        NULL,
    [BloodCount]                     VARCHAR (50)    NULL,
    [BloodCountDate]                 DATETIME        NULL,
    [BasalGlycemia]                  VARCHAR (50)    NULL,
    [BasalGlycemiaDate]              DATETIME        NULL,
    [HIV1]                           INT             NULL,
    [HIV1Date]                       DATETIME        NULL,
    [HIV2]                           INT             NULL,
    [HIV2Date]                       DATETIME        NULL,
    [RapidSyphilisTest]              INT             NULL,
    [RapidSyphilisTestDate]          DATETIME        NULL,
    [VDRL]                           INT             NULL,
    [VDRLDate]                       DATETIME        NULL,
    [Dilutions]                      INT             NULL,
    [HepatitisBAntibodies]           INT             NULL,
    [HepatitisBAntibodiesDate]       DATETIME        NULL,
    [TSH]                            DECIMAL (18, 2) NULL,
    [TSHDate]                        DATETIME        NULL,
    [IgGMeasles]                     INT             NULL,
    [IgGMeaslesDate]                 DATETIME        NULL,
    [IgMMeasles]                     INT             NULL,
    [IgMMeaslesDate]                 DATETIME        NULL,
    [IgGToxoplasmosis]               INT             NULL,
    [IgGToxoplasmosisDate]           DATETIME        NULL,
    [IgMToxoplasmosis]               INT             NULL,
    [IgMToxoplasmosisDate]           DATETIME        NULL,
    [CervicovaginalCytology]         BIT             NULL,
    [CervicovaginalCytologyDate]     DATETIME        NULL,
    [CytologyResult]                 INT             NULL,
    [Chagas]                         VARCHAR (50)    NULL,
    [ChagasDate]                     DATETIME        NULL,
    [Observations]                   VARCHAR (500)   NULL,
    CONSTRAINT [PK_FirstTrimesterEvaluationOfParaclinicalTests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FirstTrimesterEvaluationOfParaclinicalTests_PerinatalMaternalAssessmentC] FOREIGN KEY ([IdPerinatalMaternalAssessmentC]) REFERENCES [Obstetrics].[PerinatalMaternalAssessmentC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clinicas, notas adicionales o hallazgos relevantes registrados durante la evaluacion de paraclínicos del primer trimestre; campo de texto libre (VARCHAR 500) para contextualizar resultados anormales o procedimientos especiales.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de citología cervicovaginal con clasificación según sistema Bethesda: 1=ASC-US, 2=ASC-H, 3=LEI bajo grado (HPV/NIC I), 4=LEI alto grado (NIC II-III), 5=LEI alto grado sospechosa infiltración, 6=Carcinoma escamocelular/glandular, 7-9=Células atípicas endocervicales/endometriales/glandulares sin significado, 10-12=Células atípicas sospechosas neoplasia, 13-17=Adenocarcinomas e neoplasias, 18=Inadecuada para lectura.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CytologyResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado citología

1 = ASC-US (células escamosas atípicas de significado indeterminado)
2 = ASC-H (células escamosas atípicas de significado indeterminado sugestivo de LEI de alto grado)
3 = Lesión intraepitelial escamosa (LEI) de bajo grado -HPV (NIC I) (LEI BG)
4 = Lesión intraepitelial escamosa (LEI) de alto grado (NIC II-III CA INSITU) (LEI AG)
5 = Lesión intraepitelial escamosa de alto grado sospechosa de infiltración
6 = Carcinoma de células escamosas (Escamocelular) glandulares
7 = Células endocervicales atípicas sin ningún otro significado
8 = Células endometriales atípicas sin ningún otro significado
9 = Células glandulares atípicas sin ningún otro significado
10 = Células endocervicales atípicas sospechosas de neoplasia
11 = Células endometriales atípicas sospechosas de neoplasia
12 = Células glandulares atípicas sospechosas de neoplasia
13 = Adenocarcinoma endocervical in situ
14 = Adenocarcinoma endocervical
15 = Adenocarcinoma endometrial
16 = Otras neoplasias
17 = Otras neoplasias
18 = Inadecuada para lectura', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CytologyResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CytologyResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de citología cervicovaginal (Papanicolaou), dato tipo DATETIME que registra el momento del procedimiento de tamizaje cervical durante primer trimestre de embarazo.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CervicovaginalCytologyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Citología cervicovaginal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CervicovaginalCytologyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CervicovaginalCytologyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Citología cervicovaginal (Papanicolaou): 0=Normal, 1=Anormal; dato tipo BIT que indica resultado preliminar de citología cervical para detección de displasias/neoplasias en gestantes.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CervicovaginalCytology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Citología cervicovaginal            0 = Normal,      1 = Anormal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CervicovaginalCytology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CervicovaginalCytology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de dosificación de inmunoglobulina M anti-Toxoplasma; dato DATETIME que registra el momento del análisis serológico para infección aguda por Toxoplasma gondii en madre gestante.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inmuno globulina M - Toxoplasma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmunoglobulina M anti-Toxoplasma, marcador de infección aguda por Toxoplasma gondii: 1=Positivo (infección activa), 2=Negativo, 3=Indeterminado, 4=No realizado; INT con valores 1-4 según resultado.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmuno globulina M - Toxoplasma         1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de dosificación de inmunoglobulina G anti-Toxoplasma; dato DATETIME que registra el momento del análisis serológico para inmunidad/infección previa por Toxoplasma gondii.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGToxoplasmosisDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inmuno globulina G - Toxoplasma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGToxoplasmosisDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGToxoplasmosisDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmunoglobulina G anti-Toxoplasma, marcador de inmunidad o infección previa por Toxoplasma gondii: 1=Positivo (inmunidad/infección), 2=Negativo, 3=Indeterminado, 4=No realizado; INT con valores 1-4.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGToxoplasmosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmuno globulina G - Toxoplasma        1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGToxoplasmosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGToxoplasmosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de dosificación de inmunoglobulina M anti-Rubeola; dato DATETIME que registra el momento del análisis serológico para infección aguda por virus de rubeola en gestante.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMMeaslesDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inmuno globulina M - Rubeola', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMMeaslesDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMMeaslesDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmunoglobulina M anti-Rubeola, marcador de infección aguda por virus de rubeola: 1=Positivo (infección activa), 2=Negativo, 3=Indeterminado, 4=No realizado; INT con valores 1-4 según resultado.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMMeasles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmuno globulina M - Rubeola         1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMMeasles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMMeasles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de dosificación de inmunoglobulina G anti-Rubeola; dato DATETIME que registra el momento del análisis serológico para inmunidad/infección previa por virus de rubeola.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGMeaslesDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inmuno globulina G - Rubeola', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGMeaslesDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGMeaslesDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmunoglobulina G anti-Rubeola, marcador de inmunidad o infección previa por virus de rubeola: 1=Positivo (inmunidad/infección), 2=Negativo, 3=Indeterminado, 4=No realizado; INT con valores 1-4.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGMeasles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmuno globulina G - Rubeola       1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGMeasles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGMeasles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de medición de hormona estimulante de la tiroides (TSH); dato DATETIME que registra el momento del análisis de función tiroidea en primer trimestre de embarazo.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'TSHDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Hormona estimulante de la tiroides', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'TSHDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'TSHDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hormona estimulante de la tiroides (TSH), valor numérico DECIMAL (18,2) que mide función tiroidea; relevante en primer trimestre para detección de hipotiroidismo/hipertiroidismo gestacional.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'TSH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hormona estimulante de la tiroides', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'TSH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'TSH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de detección de anticuerpos contra Hepatitis B (anti-HBs); dato DATETIME que registra el momento del análisis serológico para inmunidad/infección por virus Hepatitis B.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HepatitisBAntibodiesDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anticuerpos Hepatitis B', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HepatitisBAntibodiesDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HepatitisBAntibodiesDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anticuerpos contra Hepatitis B (anti-HBs), marcador de inmunidad o infección: 1=Positivo (inmunidad/infección), 2=Negativo, 3=No realizado; INT con valores 1-3 según resultado.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HepatitisBAntibodies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anticuerpos Hepatitis B      1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HepatitisBAntibodies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HepatitisBAntibodies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diluciones realizadas en pruebas serológicas (típicamente VDRL); INT que registra factor de dilución del suero para cuantificación de anticuerpos en sífilis u otras serologías.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diluciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de serología VDRL (Veneral Disease Research Laboratory) para sífilis; dato DATETIME que registra el momento del test no treponémico de detección de sífilis en gestante.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Serología para sífilis', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serología VDRL para sífilis, test no treponémico: 1=Positivo (sífilis), 2=Negativo, 3=No realizado; INT con valores 1-3; requiere confirmación con test treponémico.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serología para sífilis           1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de prueba rápida de sífilis; dato DATETIME que registra el momento del test point-of-care treponémico para detección rápida de sífilis en gestante.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RapidSyphilisTestDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Prueba rápida sífilis', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RapidSyphilisTestDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RapidSyphilisTestDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba rápida de sífilis (test treponémico): 1=Positivo (sífilis), 2=Negativo, 3=No realizado; INT con valores 1-3; complementario a VDRL para confirmación diagnóstica.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RapidSyphilisTest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba rápida sífilis        1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RapidSyphilisTest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RapidSyphilisTest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de prueba de VIH tipo 2; dato DATETIME que registra el momento del análisis para detección de virus de inmunodeficiencia humana tipo 2 en gestante.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de HIV2', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Virus de inmunodeficiencia humana (VIH tipo 2), test serológico: 1=Positivo, 2=Negativo, 3=No realizado; INT con valores 1-3; PII sensible en contexto materno-perinatal.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Virus de la inmunodeficiencia humana           1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de prueba de VIH tipo 1; dato DATETIME que registra el momento del análisis para detección de virus de inmunodeficiencia humana tipo 1 en gestante.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de HIV1', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Virus de inmunodeficiencia humana (VIH tipo 1), test serológico: 1=Positivo, 2=Negativo, 3=No realizado; INT con valores 1-3; PII sensible en contexto materno-perinatal.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Virus de la inmunodeficiencia humana        1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de medición de glucemia basal en ayunas; dato DATETIME que registra el momento del análisis de glucosa sérica en primer trimestre de embarazo.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BasalGlycemiaDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Glucemia basal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BasalGlycemiaDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BasalGlycemiaDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Glucemia basal (glucosa en ayunas), valor tipo VARCHAR (50) en mg/dL; relevante para tamizaje de diabetes gestacional y control metabólico en primer trimestre.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BasalGlycemia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Glucemia basal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BasalGlycemia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BasalGlycemia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de hemograma (cuadro hemático); dato DATETIME que registra el momento del análisis hematológico completo en primer trimestre de embarazo.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCountDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Hemograma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCountDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCountDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemograma (cuadro hemático completo), resultado tipo VARCHAR (50); incluye conteos de RBC, WBC, plaquetas y otros parámetros para evaluación de anemia e infecciones gestacionales.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemograma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de urocultivo; dato DATETIME que registra el momento de la toma de muestra para cultivo de bacterias en orina de gestante.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'UrineCultureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Urocultivo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'UrineCultureDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'UrineCultureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Urocultivo, resultado tipo VARCHAR (50) que reporta presencia/ausencia de bacterias patógenas; tamizaje rutinario en primer trimestre para bacteriuria asintomática o infección urinaria.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'UrineCulture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Urocultivo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'UrineCulture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'UrineCulture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena datos de paraclínicos del primer trimestre de evaluación perinatal materna, incluye cultivos, serologías, inmunoglobulinas, citología cervicovaginal y pruebas de función tiroidea realizadas en gestantes durante el control prenatal de primer trimestre.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena los datos del primer trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Guarda los datos del primer trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de evaluación paraclínica del primer trimestre.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la evaluación materna perinatal a la que pertenecen estos resultados de laboratorio del primer trimestre.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del examen de Chagas (enfermedad de Chagas) realizado en el primer trimestre del embarazo.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Chagas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Chagas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realizó o reportó el examen de Chagas.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'ChagasDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'ChagasDate';
