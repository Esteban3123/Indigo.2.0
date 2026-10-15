CREATE TABLE [Glasses].[OptometryRefractionAutokeratometryDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [AutokeratometryOne]             VARCHAR (10) NOT NULL,
    [AutokeratometryTwo]             VARCHAR (10) NOT NULL,
    [AutokeratometryThree]           VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_OptometryRefractionAutokeratometryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryRefractionAutokeratometryDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de los campos de Autoqueratometria concatenados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'AutokeratometryOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Derecho 2. Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de valoracion (TABLA OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'Id';

