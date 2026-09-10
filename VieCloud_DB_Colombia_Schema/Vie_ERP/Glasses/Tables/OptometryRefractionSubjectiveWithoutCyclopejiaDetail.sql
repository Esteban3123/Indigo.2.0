CREATE TABLE [Glasses].[OptometryRefractionSubjectiveWithoutCyclopejiaDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [Sphere]                         VARCHAR (10) NOT NULL,
    [Cylinder]                       VARCHAR (10) NOT NULL,
    [Axis]                           VARCHAR (10) NOT NULL,
    [Adition]                        VARCHAR (20) NULL,
    [FarVisualAcuity]                INT          NOT NULL,
    [ProximalVisualAcuity]           INT          NOT NULL,
    [Pinhole]                        INT          NULL,
    CONSTRAINT [PK_OptometryRefractionObjectiveDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryRefractionSubjectiveWithoutCyclopejiaDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de agudeza visual con prueba de pinhole (sin cicloplejía); medida de mejora refractiva mediante oclusión pupilar', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Refracción subjetiva con cicloplejía (pinhole)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual próxima o de cerca (visión cercana); valor numérico en escala de medición oftalmológica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'ProximalVisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Prisma', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'ProximalVisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'ProximalVisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual lejana o de lejos (visión a distancia); valor numérico en escala de medición oftalmológica', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'FarVisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agudeza visual lejana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'FarVisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'FarVisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adición o adición bifocal; poder dióptrico adicional para visión de cerca en lentes multifocales o progresivos', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda Adition', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Adition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje del cilindro; ángulo en grados (0-180) que define la orientación del componente cilíndrico en la refracción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cilindro o componente cilíndrico; valor dióptrico que corrige el astigmatismo en la refracción subjetiva', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esfera o componente esférico; valor dióptrico que corrige la miopía o hipermetropía en la refracción subjetiva', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo examinado; identificador: 1=Ojo derecho (OD), 2=Ojo izquierdo (OS) en refracción sin cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Derecho  2 - izquierdo  ', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación FK a evaluación clínica optométrica cabecera (OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de vaoracion (TABLA OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK); consecutivo de detalle de refracción subjetiva sin cicloplejía por ojo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la refracción subjetiva sin cicloplejia por ojo, registrada durante la evaluación clínica optométrica. Guarda los valores de esfera, cilindro, eje, adición y agudeza visual de lejos y cerca para cada ojo examinado.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail';
