CREATE TABLE [dbo].[ImportProductRates] (
    [CodigoProducto]         VARCHAR (50)  NOT NULL,
    [TipoLiquidacion]        TINYINT       NOT NULL,
    [TipoTarifa]             TINYINT       NOT NULL,
    [TipoPorcentaje]         TINYINT       NOT NULL,
    [CUPS]                   VARCHAR (50)  NULL,
    [DescripcionRelacionada] VARCHAR (50)  NULL,
    [FechaInicial]           DATE          NOT NULL,
    [FechaFinal]             DATE          NOT NULL,
    [Porcentaje]             DECIMAL (18)  NULL,
    [PreciodeVenta]          DECIMAL (18)  NOT NULL,
    [PrecioconRecargo]       DECIMAL (18)  NOT NULL,
    [Contratado]             BIT           NULL,
    [AlCotizar]              BIT           NULL,
    [Observaciones]          VARCHAR (500) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de importación (staging) que almacena tarifas de productos con vigencia definida por fechas inicial y final. Registra precios de venta, precio con recargo y porcentaje, clasificados por tipo de liquidación, tipo de tarifa y tipo de porcentaje. Incluye el código CUPS (Clasificación Única de Procedimientos en Salud), indicadores de si el producto está contratado y si aplica para cotización, lo que sugiere uso en procesos de carga masiva de tarifarios en un contexto de facturación o contratación en salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ImportProductRates';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ImportProductRates';
GO
