CREATE TABLE [Report].[ADMCARTERA] (
    [Id_Factura]                  INT             NOT NULL,
    [FACTURA]                     VARCHAR (20)    NOT NULL,
    [FECHA FACTURA]               DATE            NULL,
    [NUMERO CUENTA]               VARCHAR (50)    NOT NULL,
    [CUENTA CONTABLE]             VARCHAR (100)   NOT NULL,
    [VALOR FACTURA]               NUMERIC (18, 2) NOT NULL,
    [VALOR CUENTA]                NUMERIC (18, 2) NOT NULL,
    [SALDO CUENTA]                NUMERIC (18, 2) NOT NULL,
    [NIT]                         VARCHAR (25)    NULL,
    [TERCERO]                     VARCHAR (300)   NULL,
    [GRUPO ATENCION]              VARCHAR (100)   NOT NULL,
    [SALDO INICIAL]               VARCHAR (2)     NOT NULL,
    [CODIGO ESTADO CARTERA]       INT             NOT NULL,
    [ESTADO CUENTAS]              VARCHAR (28)    NOT NULL,
    [ESTADO CARTERA]              VARCHAR (28)    NULL,
    [CONSECUTIVO RADICADO]        INT             NULL,
    [FECHA CREACION RADICADO]     DATE            NULL,
    [FECHA CONFIRMACION RADICADO] DATE            NULL,
    [FECHA RECIBIDO ENTIDAD]      DATE            NULL,
    [EDAD CARTERA]                INT             NULL,
    [HASTA 60]                    VARCHAR (2)     NOT NULL,
    [ENTRE 60 Y 90]               VARCHAR (2)     NOT NULL,
    [ENTRE 90 Y 180]              VARCHAR (2)     NOT NULL,
    [ENTRE 180 Y 360]             VARCHAR (2)     NOT NULL,
    [MAYOR 360]                   VARCHAR (2)     NOT NULL,
    [GLOSA]                       VARCHAR (2)     NOT NULL,
    [DEVOLUCIONES]                VARCHAR (2)     NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte para el análisis de cartera administrativa (cuentas por cobrar), que almacena información de facturas con sus valores, saldos y cuentas contables asociadas a terceros (pagadores/entidades) identificados por NIT. Registra el ciclo de radicación de facturas ante entidades y clasifica la antigüedad de la cartera en rangos de días (hasta 60, 60-90, 90-180, 180-360 y mayor a 360), además de indicadores de glosas y devoluciones.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'ADMCARTERA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'ADMCARTERA';
GO
