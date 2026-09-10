CREATE TABLE [Report].[TablaResultadosLaboratorio] (
    [NUMINGRES]       CHAR (10)      NOT NULL,
    [IPCODPACI]       VARCHAR (25)   NOT NULL,
    [IDHCHISPACA]     INT            NOT NULL,
    [IDEXAGRUPO]      INT            NOT NULL,
    [NOMBRE_GRUPO]    VARCHAR (100)  NOT NULL,
    [VALOR_GRUPO]     INT            NOT NULL,
    [ID_VARIABLE]     INT            NOT NULL,
    [NOMBRE_VARIABLE] VARCHAR (150)  NOT NULL,
    [OPCION]          TINYINT        NULL,
    [VALOR]           VARCHAR (5000) NOT NULL,
    [ID]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_TablaResultadosLaboratorio] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que almacena resultados de exámenes de laboratorio asociados a ingresos hospitalarios y pacientes identificados. Organiza los resultados mediante grupos de exámenes y variables con sus respectivos valores, permitiendo estructurar múltiples parámetros por ingreso. Su ubicación en el esquema `Report` sugiere uso como tabla intermedia o de staging para generación de reportes clínicos de laboratorio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TablaResultadosLaboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TablaResultadosLaboratorio';
GO
