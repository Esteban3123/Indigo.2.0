CREATE TABLE [Report].[TABLAINVENTARIOS] (
    [ID_COMPANY]          VARCHAR (9)     NULL,
    [MODULO]              VARCHAR (12)    NOT NULL,
    [TRANSACCION]         VARCHAR (36)    NOT NULL,
    [AÑO]                 INT             NULL,
    [MES]                 INT             NULL,
    [DIA]                 INT             NULL,
    [CODIGO GRUPO]        VARCHAR (20)    NOT NULL,
    [GRUPO PRODUCTO]      VARCHAR (100)   NOT NULL,
    [CUENTA CONTABLE]     VARCHAR (50)    NOT NULL,
    [NOMBRE ALMACEN]      VARCHAR (100)   NOT NULL,
    [TIPO DE TRANSACCION] VARCHAR (7)     NULL,
    [TIPO ALMACEN]        VARCHAR (12)    NOT NULL,
    [TOTAL ENTRADAS]      NUMERIC (38, 2) NULL,
    [TOTAL SALIDAS]       NUMERIC (38, 2) NULL,
    [AFECTA INVENTARIO]   VARCHAR (2)     NOT NULL,
    [CODIGO DOCUMENTO]    VARCHAR (20)    NULL,
    [TIPO ORDEN]          VARCHAR (43)    NULL,
    [ESTADO]              VARCHAR (11)    NULL,
    [CODIGO INTERNO]      VARCHAR (20)    NULL,
    [FECHA BUSQUEDA]      DATETIME        NULL,
    [ULT_ACTUA]           DATETIME        NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que consolida movimientos de inventario (entradas y salidas) por almacén, grupo de producto y cuenta contable, desagregados por fecha (año, mes, día). Registra transacciones de distintos módulos identificando tipo de transacción, tipo de orden, estado del documento y si el movimiento afecta el inventario contablemente. Sirve como base para informes de gestión de inventarios en el esquema de reporting.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TABLAINVENTARIOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'TABLAINVENTARIOS';
GO
