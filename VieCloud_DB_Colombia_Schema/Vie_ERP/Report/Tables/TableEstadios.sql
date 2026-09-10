CREATE TABLE [Report].[TableEstadios] (
    [ID]          INT          NOT NULL,
    [Descripcion] VARCHAR (30) NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de catálogo que almacena estadios identificados por un código entero y su descripción corta. Dado el esquema `Report`, se utiliza como referencia de lookup en consultas o reportes, probablemente para clasificar estadios clínicos de enfermedades (ej. estadios oncológicos), aunque el contexto exacto no puede confirmarse solo con esta definición.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TableEstadios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TableEstadios';
GO
