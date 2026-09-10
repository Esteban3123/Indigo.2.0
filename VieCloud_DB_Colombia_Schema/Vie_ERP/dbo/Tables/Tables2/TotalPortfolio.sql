CREATE TABLE [dbo].[TotalPortfolio] (
    [ID_COMPANY]                  VARCHAR (9)   NULL,
    [CIUDAD]                      VARCHAR (100) NULL,
    [CENTRO DE ATENCION]          VARCHAR (100) NULL,
    [ESTADO CARTERA]              VARCHAR (22)  NOT NULL,
    [DEVOLUCION]                  VARCHAR (2)   NOT NULL,
    [NIT]                         VARCHAR (25)  NOT NULL,
    [TERCERO]                     VARCHAR (300) NOT NULL,
    [CONTRATO]                    VARCHAR (30)  NULL,
    [GRUPO DE ATENCION]           VARCHAR (100) NOT NULL,
    [REGIMEN]                     VARCHAR (27)  NOT NULL,
    [TIPO DOCUMENTO]              VARCHAR (36)  NULL,
    [NRO FACTURA]                 VARCHAR (20)  NOT NULL,
    [VALOR FACTURA]               NUMERIC (18)  NULL,
    [SALDO FACTURA]               NUMERIC (18)  NULL,
    [FECHA FACTURA]               DATE          NULL,
    [FECHA CREACION RADICADO]     DATETIME      NULL,
    [FECHA DOCUMENTO RADICADO]    DATETIME      NULL,
    [FECHA RADICACION]            DATETIME      NULL,
    [FECHA CONFIRMACION RADICADO] DATETIME      NULL,
    [CONSECUTIVO RADICACION]      INT           NULL,
    [ESTADO FACTURA]              VARCHAR (9)   NOT NULL,
    [SALDO INICIAL]               VARCHAR (2)   NOT NULL,
    [USUARIO FACTURO]             VARCHAR (83)  NULL,
    [USUARIO RADICO]              VARCHAR (83)  NULL,
    [CATEGORIA]                   VARCHAR (100) NULL,
    [FECHA INGRESO]               DATE          NULL,
    [FECHA EGRESO CORTE]          DATE          NULL,
    [AÑO CONFIRMACION RAD]        INT           NULL,
    [MES CONFIRMACION RAD]        INT           NULL,
    [AÑO FACTURA]                 INT           NULL,
    [MES FACTURA]                 INT           NULL,
    [FECHA BUSQUEDA]              DATE          NULL,
    [AÑO BUSQUEDA]                INT           NULL,
    [MES BUSQUEDA]                INT           NULL,
    [TimeStamp]                   ROWVERSION    NOT NULL,
    [ULT_ACTUAL]                  DATETIME      NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de cartera total que consolida información de facturación y radicación de cuentas médicas por empresa, ciudad y centro de atención. Registra datos del tercero (asegurador/pagador) identificado por NIT, contrato, régimen y grupo de atención, junto con valores y saldos de facturas. Incluye trazabilidad de fechas del ciclo de radicación y campos calculados de año/mes para análisis de antigüedad de cartera, orientada a reporting financiero y seguimiento de cobros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TotalPortfolio';
GO
