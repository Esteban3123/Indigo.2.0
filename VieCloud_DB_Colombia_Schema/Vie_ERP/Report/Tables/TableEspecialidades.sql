CREATE TABLE [Report].[TableEspecialidades] (
    [cododo]       VARCHAR (3)   NOT NULL,
    [codsanitas]   INT           NOT NULL,
    [especialidad] VARCHAR (100) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de correspondencia entre dos sistemas de codificación de especialidades médicas: el código interno (`cododo`, 3 caracteres) y el código de Sanitas (`codsanitas`, entero). Almacena además el nombre descriptivo de la especialidad. Se ubica en el esquema `Report`, lo que sugiere uso en procesos de generación de informes o cruce de datos entre plataformas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TableEspecialidades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TableEspecialidades';
GO
