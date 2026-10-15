CREATE TABLE [Glasses].[OptometryRefractionObjectiveDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [TypeRefraction]                 INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [Sphere]                         VARCHAR (10) NOT NULL,
    [Cylinder]                       VARCHAR (10) NOT NULL,
    [Axis]                           VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_OptometryRefractionSubjectiveDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryRefractionObjectiveDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del campo Eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionObjectiveDetail', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del campo Cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionObjectiveDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del campo Esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionObjectiveDetail', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Derecho  2 - izquierdo  ', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionObjectiveDetail', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Refraccion objetiva CON ciclopejia, 2 - Refraccion objetiva SIN ciclopejia  ', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionObjectiveDetail', @level2type = N'COLUMN', @level2name = N'TypeRefraction';

