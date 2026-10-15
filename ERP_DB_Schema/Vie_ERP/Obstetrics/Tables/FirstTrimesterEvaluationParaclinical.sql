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
ALTER TABLE [Obstetrics].[FirstTrimesterEvaluationParaclinical] NOCHECK CONSTRAINT [FK_FirstTrimesterEvaluationOfParaclinicalTests_PerinatalMaternalAssessmentC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado citología

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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Citología cervicovaginal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CervicovaginalCytologyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Citología cervicovaginal            0 = Normal,      1 = Anormal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'CervicovaginalCytology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Inmuno globulina M - Toxoplasma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmuno globulina M - Toxoplasma         1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Inmuno globulina G - Toxoplasma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGToxoplasmosisDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmuno globulina G - Toxoplasma        1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGToxoplasmosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Inmuno globulina M - Rubeola', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMMeaslesDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmuno globulina M - Rubeola         1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMMeasles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Inmuno globulina G - Rubeola', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGMeaslesDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmuno globulina G - Rubeola       1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgGMeasles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Hormona estimulante de la tiroides', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'TSHDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hormona estimulante de la tiroides', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'TSH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Anticuerpos Hepatitis B', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HepatitisBAntibodiesDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anticuerpos Hepatitis B      1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HepatitisBAntibodies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diluciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Serología para sífilis', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serología para sífilis           1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Prueba rápida sífilis', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RapidSyphilisTestDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba rápida sífilis        1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RapidSyphilisTest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de HIV2', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Virus de la inmunodeficiencia humana           1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de HIV1', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Virus de la inmunodeficiencia humana        1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Glucemia basal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BasalGlycemiaDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Glucemia basal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BasalGlycemia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Hemograma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCountDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemograma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Urocultivo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'UrineCultureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Urocultivo', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'UrineCulture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los datos del primer trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Guarda los datos del primer trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'FirstTrimesterEvaluationParaclinical';

