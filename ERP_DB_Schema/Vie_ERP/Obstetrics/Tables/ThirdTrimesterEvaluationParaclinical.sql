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
ALTER TABLE [Obstetrics].[ThirdTrimesterEvaluationParaclinical] NOCHECK CONSTRAINT [FK_ThirdTrimesterEvaluationOfParaclinicalTests_PerinatalMaternalAssessmentC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Cultivo retrovaginal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RetrovaginalCultureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cultivo retrovaginal', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'RetrovaginalCulture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Inmuno globulina M - Toxoplasma', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosisDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inmuno globulina M - Toxoplasma      1) + Positivo,    2) - Negativo,   3) Indeterminado,   4) No tiene ', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'IgMToxoplasmosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diluciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'Dilutions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Serología para sífilis', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRLDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serología para sífilis       1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'VDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Virus de la inmunodeficiencia humana', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Virus de la inmunodeficiencia humana       1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Virus de la inmunodeficiencia humana', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Virus de la inmunodeficiencia humana     1) + Positivo,   2)  - Negativo,   3) No tiene', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical', @level2type = N'COLUMN', @level2name = N'HIV1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los datos del tercer trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Guarda los datos del tercer trimestre de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'ThirdTrimesterEvaluationParaclinical';

