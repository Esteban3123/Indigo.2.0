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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refracción subjetiva con cicloplejía (pinhole)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del campo Prisma', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'ProximalVisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual lejana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'FarVisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda Adition', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Adition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del campo Eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del campo Cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del campo Esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Derecho  2 - izquierdo  ', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de vaoracion (TABLA OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionSubjectiveWithoutCyclopejiaDetail', @level2type = N'COLUMN', @level2name = N'Id';

