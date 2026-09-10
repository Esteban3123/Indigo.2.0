CREATE TABLE [dbo].[SubirFondos] (
    [Cedula]  VARCHAR (20)  NULL,
    [Salud]   VARCHAR (100) NULL,
    [Pension] VARCHAR (100) NULL,
    [Caja]    VARCHAR (100) NULL,
    [ARL]     VARCHAR (100) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla auxiliar que almacena información de afiliación a seguridad social de personas identificadas por cédula, registrando las entidades a las que pertenecen en salud, pensión, caja de compensación y ARL. Su nombre sugiere un proceso de carga o migración masiva de datos de fondos de seguridad social, posiblemente como tabla temporal de staging.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirFondos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirFondos';
GO
