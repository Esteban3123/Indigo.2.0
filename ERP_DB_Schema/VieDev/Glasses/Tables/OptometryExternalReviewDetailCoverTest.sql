CREATE TABLE [Glasses].[OptometryExternalReviewDetailCoverTest] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [CoverTestTableNumber]           TINYINT      NOT NULL,
    [CoverTestRow]                   TINYINT      NOT NULL,
    [CoverTestColumn]                TINYINT      NOT NULL,
    [CoverTestDescription]           VARCHAR (15) NULL,
    CONSTRAINT [PK_OptometryExternalReviewDetailCoverTest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryExternalReviewDetailCoverTest_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el texto diligenciado en la tabla y celda especificada del resultado del Cover Test', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia a la columna en donde se guarda el valor (toma el 0 como índice, zero-based index)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestColumn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia a la fila en donde se guarda el valor (toma el 0 como índice, zero-based index)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestRow';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia la tabla en donde se digita la informacion del Cover Test: 1 Visión lejana, 2 Visión cercana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestTableNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla cabecera de OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla detalle de las tablas del Cover Test en la pestaña Examen Externo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'Id';

