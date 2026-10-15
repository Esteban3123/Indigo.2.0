CREATE TABLE [Glasses].[OptometryRefractionKeratometryDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [KeratometryOne]                 VARCHAR (10) NOT NULL,
    [KeratometryTwo]                 VARCHAR (10) NOT NULL,
    [KeratometryThree]               VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_OptometryRefractionKeratometryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryRefractionKeratometryDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de los campos de Queratometría concatenados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryThree';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de los campos de Queratometría concatenados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de los campos de Queratometría concatenados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Derecho 2. Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de valoracion (TABLA OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'Id';

