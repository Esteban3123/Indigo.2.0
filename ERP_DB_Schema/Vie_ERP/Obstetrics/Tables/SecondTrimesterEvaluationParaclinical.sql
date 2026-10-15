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
ALTER TABLE [Obstetrics].[SecondTrimesterEvaluationParaclinical] NOCHECK CONSTRAINT [FK_SecondTrimesterEvaluationOfParaclinicalTests_PerinatalMaternalAssessmentC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Inmuno globulina M - Toxoplasma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmuno globulina M - Toxoplasma     1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diluciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Serología para sífilis ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serología para sífilis     1) + Positivo,    2) - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Prueba de tolerancia oral a la glucosa', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'PTOGDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba de tolerancia oral a la glucosa', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'PTOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Hemograma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCountDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemograma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'BloodCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Virus de la inmunodeficiencia humana', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Virus de la inmunodeficiencia humana       1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Virus de la inmunodeficiencia humana', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Virus de la inmunodeficiencia humana      1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los datos del segundo trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Guarda los datos del segundo trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'SecondTrimesterEvaluationParaclinical';

