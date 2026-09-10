CREATE TABLE [Glasses].[OptometryRefractionSubjectiveWithCyclopejiaDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [Sphere]                         VARCHAR (10) NOT NULL,
    [Cylinder]                       VARCHAR (10) NOT NULL,
    [Axis]                           VARCHAR (10) NOT NULL,
    [VisualAcuity]                   INT          NOT NULL,
    [Adition]                        VARCHAR (20) NULL,
    [Pinhole]                        INT          NULL,
    CONSTRAINT [PK_OptometryRefractionSubjectiveWithCyclopejiaDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryRefractionSubjectiveWithCyclopejiaDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba de pinhole (orificio estenopeico) en refracción subjetiva con cicloplejía; INT; mide agudeza con corrección de defectos refractivos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Refracción subjetiva con cicloplejía (pinhole)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adición (poder adicional para lectura cercana en presbicia); VARCHAR(20); valores numéricos típicamente +0.50 a +3.50 dioptrías', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Adicion (creado el 13-05-2023 por Leonardo Rojas - actualización san José)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Adition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual (capacidad de visión); INT; escala Snellen o decimal (ej: 20/20, 1.0); resultado de refracción subjetiva con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'VisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Agudeza visual', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'VisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'VisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje (orientación del cilindro en refracción); VARCHAR(10); rango 0-180 grados; define dirección del astigmatismo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cilindro (potencia cilíndrica para corrección de astigmatismo); VARCHAR(10); valores negativos o positivos en dioptrías', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esfera (potencia esférica principal para miopía o hipermetropía); VARCHAR(10); valores en dioptrías; componente base de la refracción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo examinado; INT; 1=Ojo derecho (OD), 2=Ojo izquierdo (OS); identifica lateralidad en evaluación optométrica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Derecho  2 - izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la evaluación clínica optométrica (cabecera); INT; FK a [OptometryClinicalEvaluationC]; agrupa detalles de refracción subjetiva con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de vaoracion (TABLA OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK); INT IDENTITY; consecutivo de detalle de refracción subjetiva con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la refracción subjetiva con ciclopejia (bajo efecto de ciclopléjico) registrada en la evaluación clínica optométrica. Guarda los valores refractivos de cada ojo del paciente: esfera, cilindro, eje, agudeza visual, adición y agujero estenopeico (pinhole).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithCyclopejiaDetail';
