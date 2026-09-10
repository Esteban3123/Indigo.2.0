CREATE TABLE [Report].[Transacciones] (
    [AÑO]             INT             NULL,
    [MES]             INT             NULL,
    [TRANSACCION]     VARCHAR (50)    NULL,
    [CUENTA_CONTABLE] VARCHAR (10)    NULL,
    [MODULO]          VARCHAR (50)    NULL,
    [TOTAL_ENTRADAS]  DECIMAL (18, 2) NULL,
    [TOTAL_SALIDAS]   DECIMAL (18, 2) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que consolida movimientos contables (entradas y salidas) agrupados por período (año y mes), tipo de transacción, cuenta contable y módulo del sistema. Sirve como estructura de almacenamiento para reportes financiero-contables agregados, probablemente cargada periódicamente desde procesos ETL o procedimientos de cierre contable.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'Transacciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'Transacciones';
GO
