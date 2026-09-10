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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo ingresado en la celda específica del Cover Test (prueba de oclusión); documenta hallazgos de alineación ocular, desviación o tropía en visión lejana o cercana. VARCHAR(15), nullable.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el texto diligenciado en la tabla y celda especificada del resultado del Cover Test', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de columna (zero-based, 0+) donde se almacena el valor en la tabla del Cover Test; identifica posición horizontal en la grilla de resultados optométricos.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestColumn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia a la columna en donde se guarda el valor (toma el 0 como índice, zero-based index)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestColumn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestColumn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de fila (zero-based, 0+) donde se almacena el valor en la tabla del Cover Test; identifica posición vertical en la grilla de resultados optométricos.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestRow';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia a la fila en donde se guarda el valor (toma el 0 como índice, zero-based index)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestRow';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestRow';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tabla del Cover Test: 1=Visión lejana (distancia), 2=Visión cercana (proximidad); organiza resultados de prueba de oclusión por distancia de fijación. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestTableNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia la tabla en donde se digita la informacion del Cover Test: 1 Visión lejana, 2 Visión cercana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestTableNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'CoverTestTableNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a OptometryClinicalEvaluationC (Id); vincula detalles del Cover Test a la evaluación clínica optométrica de cabecera en examen externo/refracción.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla cabecera de OptometryClinicalEvaluationC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) del registro de detalles del Cover Test en pestaña Examen Externo; permite trazabilidad de cada celda con resultado optométrico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla detalle de las tablas del Cover Test en la pestaña Examen Externo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los resultados del Cover Test (prueba de cobertura ocular) registrados en la evaluación clínica de optometría externa. Almacena cada celda de la grilla del Cover Test con su número de tabla, fila, columna y descripción del hallazgo visual (tropía, foria, ortoforia, etc.).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryExternalReviewDetailCoverTest';
