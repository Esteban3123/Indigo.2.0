CREATE TABLE [dbo].[CorregirSueldos] (
    [Cedula]   VARCHAR (50) NULL,
    [Salario]  NUMERIC (18) NULL,
    [FechaFin] DATE         NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla auxiliar o temporal que almacena correcciones de sueldos para empleados identificados por cédula, registrando el salario corregido y una fecha de fin asociada. Por su estructura simple y sin claves primarias ni foráneas, se utiliza probablemente como tabla de trabajo para procesos batch de ajuste o migración de datos salariales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CorregirSueldos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CorregirSueldos';
GO
