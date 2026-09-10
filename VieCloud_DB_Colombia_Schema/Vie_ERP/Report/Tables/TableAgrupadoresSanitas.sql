CREATE TABLE [Report].[TableAgrupadoresSanitas] (
    [ID]                           INT             IDENTITY (1, 1) NOT NULL,
    [CUPS]                         CHAR (10)       NOT NULL,
    [codigoDescripcionRelacionada] INT             NULL,
    [grupoIndigo]                  VARCHAR (60)    NULL,
    [descripcionCUPS]              VARCHAR (300)   NULL,
    [sanitasPGP]                   VARCHAR (30)    NULL,
    [observacion]                  VARCHAR (300)   NULL,
    [agrupadorNT]                  VARCHAR (100)   NULL,
    [valor]                        NUMERIC (20, 2) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que almacena agrupadores de procedimientos médicos identificados por código CUPS, asociando cada procedimiento a grupos clasificatorios como `grupoIndigo` y `agrupadorNT`, con referencia a la aseguradora Sanitas mediante `sanitasPGP`. Incluye descripción del CUPS, un código de descripción relacionada, observaciones y un valor numérico, sugiriendo uso en liquidación o tarifación de servicios con dicha aseguradora.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TableAgrupadoresSanitas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TableAgrupadoresSanitas';
GO
