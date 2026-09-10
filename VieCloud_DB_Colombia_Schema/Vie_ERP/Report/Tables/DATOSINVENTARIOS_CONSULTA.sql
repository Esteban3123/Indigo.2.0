CREATE TABLE [Report].[DATOSINVENTARIOS_CONSULTA] (
    [ID_COMPANY]           VARCHAR (9)     NULL,
    [MODULO]               VARCHAR (12)    NOT NULL,
    [TRANSACCION]          VARCHAR (36)    NOT NULL,
    [AÑO]                  INT             NULL,
    [MES]                  INT             NULL,
    [DIA]                  INT             NULL,
    [CODIGO GRUPO]         VARCHAR (20)    NOT NULL,
    [GRUPO PRODUCTO]       VARCHAR (100)   NOT NULL,
    [CUENTA CONTABLE]      VARCHAR (50)    NOT NULL,
    [NOMBRE ALMACEN]       VARCHAR (100)   NOT NULL,
    [TIPO DE TRANSACCION]  VARCHAR (7)     NULL,
    [TIPO ALMACEN]         VARCHAR (12)    NOT NULL,
    [TOTAL ENTRADAS]       NUMERIC (38, 2) NULL,
    [TOTAL SALIDAS]        NUMERIC (38, 2) NULL,
    [AFECTA INVENTARIO]    VARCHAR (2)     NOT NULL,
    [CODIGO DOCUMENTO]     VARCHAR (20)    NULL,
    [TIPO ORDEN]           VARCHAR (43)    NULL,
    [ESTADO]               VARCHAR (11)    NULL,
    [CODIGO INTERNO]       VARCHAR (20)    NULL,
    [FECHA BUSQUEDA]       DATETIME        NULL,
    [ULT_ACTUA]            DATETIME        NULL,
    [ID COMPROBANTE]       INT             NOT NULL,
    [COMPROBANTE CONTABLE] BIGINT          NOT NULL,
    [TIPO COMPROBANTE]     VARCHAR (123)   NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que almacena datos consolidados de movimientos de inventario por empresa, módulo y transacción, registrando entradas y salidas agrupadas por grupo de producto, almacén y cuenta contable. Incluye información temporal (año, mes, día), tipo de transacción, estado del documento y su comprobante contable asociado. Actúa como tabla de staging o precálculo para consultas de reporting sobre inventarios, indicando si cada movimiento afecta o no el inventario.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'DATOSINVENTARIOS_CONSULTA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'DATOSINVENTARIOS_CONSULTA';
GO
