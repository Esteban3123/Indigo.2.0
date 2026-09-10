CREATE TABLE [dbo].[Conceptos] (
    [Codigo]       VARCHAR (5)   NULL,
    [Nombre]       VARCHAR (500) NULL,
    [Clase]        VARCHAR (5)   NULL,
    [TipoConcepto] VARCHAR (50)  NULL,
    [Homologacion] VARCHAR (5)   NULL,
    [Formula]      VARCHAR (500) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de catálogo que almacena conceptos de liquidación o facturación en salud, identificados por un código y nombre, clasificados por clase y tipo de concepto. Incluye una fórmula de cálculo asociada y un código de homologación, posiblemente para mapeo con estándares externos o entre sistemas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Conceptos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Conceptos';
GO
