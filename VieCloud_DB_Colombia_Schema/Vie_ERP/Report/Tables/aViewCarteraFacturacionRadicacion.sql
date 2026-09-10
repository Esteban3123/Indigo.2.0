CREATE TABLE [Report].[aViewCarteraFacturacionRadicacion] (
    [ID_COMPANY]                  VARCHAR (9)     NULL,
    [ESTADO CARTERA]              VARCHAR (28)    NULL,
    [DEVOLUCION]                  VARCHAR (2)     NOT NULL,
    [NIT]                         VARCHAR (25)    NOT NULL,
    [TERCERO]                     VARCHAR (300)   NOT NULL,
    [GRUPO ATENCION]              VARCHAR (100)   NOT NULL,
    [TIPO DOCUMENTO]              VARCHAR (36)    NULL,
    [FACTURA]                     VARCHAR (20)    NOT NULL,
    [VALOR FACTURA]               NUMERIC (18, 2) NOT NULL,
    [SALDO FACTURA]               NUMERIC (18, 2) NOT NULL,
    [FECHA FACTURA]               DATE            NULL,
    [HORA FACTURA]                TIME (7)        NULL,
    [FECHA CREACION RADICADO]     DATE            NULL,
    [HORA CREACION RADICADO]      TIME (7)        NULL,
    [FECHA OFICIO RADICADO]       DATE            NULL,
    [HORA OFICIO RADICADO]        TIME (7)        NULL,
    [FECHA RADICACION]            DATE            NULL,
    [HORA RADICACION]             TIME (7)        NULL,
    [FECHA CONFIRMACION RADICADO] DATE            NULL,
    [HORA CONFIRMACION RADICADO]  TIME (7)        NULL,
    [CONSECUTIVO RADICADO]        INT             NULL,
    [ESTADO FACTURA]              VARCHAR (9)     NOT NULL,
    [SALDO INICIAL]               VARCHAR (2)     NOT NULL,
    [USUARIO FACTURO]             CHAR (60)       NULL,
    [USUARIO RADICO]              CHAR (60)       NULL,
    [CATEGORIA]                   VARCHAR (100)   NULL,
    [CANTIDAD]                    INT             NOT NULL,
    [FECHA BUSQUEDA]              DATE            NULL,
    [AÑO BUSQUEDA]                INT             NULL,
    [MES BUSQUEDA]                INT             NULL,
    [MES NOMBRE BUSQUEDA]         NVARCHAR (4000) NOT NULL,
    [ULT_ACTUAL]                  DATETIME        NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que consolida información de cartera, facturación y radicación de facturas por tercero (aseguradora/pagador), identificado por NIT. Almacena valores y saldos de facturas junto con las fechas y horas de cada etapa del proceso: emisión, creación del radicado, oficio, radicación física y confirmación. Incluye campos de búsqueda temporal (año, mes) para segmentación de reportes de cartera, así como los usuarios que facturaron y radicaron cada documento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'aViewCarteraFacturacionRadicacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'aViewCarteraFacturacionRadicacion';
GO
