CREATE TABLE [Portfolio].[ReportPortfolioByAge] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [ClosingDate]            DATE          NULL,
    [OperatingUnits]         VARCHAR (MAX) NULL,
    [PersonTypes]            VARCHAR (MAX) NULL,
    [ThirdParties]           VARCHAR (MAX) NULL,
    [DocumentTypes]          VARCHAR (MAX) NULL,
    [Status]                 VARCHAR (MAX) NULL,
    [CalculateAgeBy]         INT           NULL,
    [IncludeAdvance]         INT           NULL,
    [OrderBy]                INT           NULL,
    [GroupOrDetailByAccount] BIT           NULL,
    CONSTRAINT [PK_ReportPortfolioByAge] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano (1=Sí, 0=No) que determina si el reporte agrupa cartera por cuenta o detalla registro por registro; usado en análisis de portfolio y gestión de deuda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'GroupOrDetailByAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo booleano que especifica si se agrupa o detalla por cuenta. 1 - Si, 2 - No.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'GroupOrDetailByAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'GroupOrDetailByAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de ordenamiento del reporte de cartera por antigüedad; define secuencia de presentación (ej: por fecha, monto, antigüedad).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'OrderBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ordenar por', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'OrderBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'OrderBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio booleano que indica si se incluyen anticipos, abonos o pagos parciales en el cálculo de cartera vencida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'IncludeAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio si incluye avance', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'IncludeAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'IncludeAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio numérico que define la base temporal para calcular antigüedad: por fecha de factura, vencimiento, últimos pagos o cierre contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'CalculateAgeBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio del calculo de la edad', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'CalculateAgeBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'CalculateAgeBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estados de cartera concatenados por comas (ej: vigente, vencido, en glosa, en cobranza); filtro multiestado para portfolio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'estados concatenados por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de documentos de facturación concatenados por comas (ej: factura, nota crédito, recibo, documento equivalente); clave de clasificación RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'DocumentTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de documentos concatenados por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'DocumentTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'DocumentTypes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Terceros/proveedores/acreedores concatenados por comas; filtro para análisis de cartera por entidad o centro de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'ThirdParties';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Terceros concatenados por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'ThirdParties';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'ThirdParties';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de personas concatenadas por comas (ej: natural, jurídica, paciente, profesional de salud); segmentación demográfica del portfolio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'PersonTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tipo de personas concatenadas por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'PersonTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'PersonTypes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidades funcionales/operativas concatenadas por comas (ej: urgencia, hospitalización, consulta externa, laboratorio); filtro por centro de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'OperatingUnits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidades operativas concatenadas por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'OperatingUnits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'OperatingUnits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de corte del reporte de cartera; fecha de referencia para antigüedad y estado de saldos en portfolio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'ClosingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de corte', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'ClosingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'ClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) del registro de configuración de reporte de cartera por antigüedad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de parámetros para el reporte de cartera por edades. Guarda los filtros y criterios seleccionados por el usuario (unidades operativas, tipos de persona, terceros, tipo de documento, estado de cartera, forma de calcular la antigüedad y agrupación) para generar el informe de antigüedad de saldos pendientes de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge';
