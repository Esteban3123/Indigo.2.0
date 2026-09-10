CREATE TABLE [Report].[SUBGRUPOS_SUSCEPTIBLE_AUTO] (
    [ID]          INT        IDENTITY (1, 1) NOT NULL,
    [CODSUBIPS]   CHAR (10)  NULL,
    [DESSUBIPS]   CHAR (300) NOT NULL,
    [SUSCEPTIBLE] CHAR (3)   NULL,
    CONSTRAINT [PK_SUBGRUPOS_SUSCEPTIBLE_AUTO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Susceptible', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'SUBGRUPOS_SUSCEPTIBLE_AUTO', @level2type = N'COLUMN', @level2name = N'SUSCEPTIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción subgrupo IPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'SUBGRUPOS_SUSCEPTIBLE_AUTO', @level2type = N'COLUMN', @level2name = N'DESSUBIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código subgrupo IPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'SUBGRUPOS_SUSCEPTIBLE_AUTO', @level2type = N'COLUMN', @level2name = N'CODSUBIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'SUBGRUPOS_SUSCEPTIBLE_AUTO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de referencia ubicada en el esquema de reportes que almacena subgrupos de IPS (Instituciones Prestadoras de Salud) junto con un indicador que señala si dicho subgrupo es susceptible. Cada registro asocia un código y descripción de subgrupo IPS con una marca de susceptibilidad, sirviendo como catálogo de apoyo para procesos de reporte o clasificación automatizada de subgrupos susceptibles.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'SUBGRUPOS_SUSCEPTIBLE_AUTO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'SUBGRUPOS_SUSCEPTIBLE_AUTO';
GO
