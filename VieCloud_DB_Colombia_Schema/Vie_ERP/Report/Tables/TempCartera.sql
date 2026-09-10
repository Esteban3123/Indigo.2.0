CREATE TABLE [Report].[TempCartera] (
    [IdCuenta] VARCHAR (50) NOT NULL,
    [Cuenta]   VARCHAR (20) NULL,
    [Descri]   VARCHAR (20) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TempCartera', @level2type = N'COLUMN', @level2name = N'Descri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TempCartera', @level2type = N'COLUMN', @level2name = N'Cuenta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TempCartera', @level2type = N'COLUMN', @level2name = N'IdCuenta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla temporal del esquema de reportes que almacena cuentas de cartera con su identificador, código de cuenta y descripción. Se utiliza como almacenamiento intermedio para procesos de generación de reportes relacionados con cartera, sin restricciones de clave foránea ni índices definidos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TempCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TempCartera';
GO
