CREATE TABLE [dbo].[ReceiptsVsPurchaseOrders] (
    [ID_COMPANY]                 VARCHAR (9)     NULL,
    [NRO COMPROBANTE DE ENTRADA] VARCHAR (20)    NOT NULL,
    [FECHA]                      DATETIME        NOT NULL,
    [NRO FACTURA]                VARCHAR (100)   NOT NULL,
    [FECHA FACTURA]              DATETIME        NOT NULL,
    [NRO ORDEN DE COMPRA]        VARCHAR (20)    NULL,
    [NIT]                        VARCHAR (25)    NOT NULL,
    [PROVEEDOR]                  VARCHAR (100)   NOT NULL,
    [ALMACEN]                    VARCHAR (100)   NOT NULL,
    [TIPO PRODUCTO]              VARCHAR (11)    NOT NULL,
    [CODIGO PRODUCTO]            VARCHAR (20)    NOT NULL,
    [DESCRIPCION PRODUCTO]       VARCHAR (400)   NOT NULL,
    [CANTIDAD ORDENADA]          INT             NULL,
    [CANTIDAD RECIBIDA]          INT             NOT NULL,
    [VALOR UNITARIO OC]          DECIMAL (18, 2) NULL,
    [VALOR UNITARIO FACTURA]     DECIMAL (18, 2) NOT NULL,
    [VALOR TOTAL OC]             DECIMAL (18, 2) NULL,
    [VALOR TOTAL FACTURA]        DECIMAL (18, 2) NOT NULL,
    [VALOR IMPUESTO OC]          DECIMAL (18, 2) NULL,
    [VALOR IMPUESTO FACTURA]     DECIMAL (18, 2) NOT NULL,
    [VALOR TOTAL]                DECIMAL (18, 2) NOT NULL,
    [FECHA BUSQUEDA]             DATE            NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de staging o reporte que consolida el cruce entre comprobantes de entrada de mercancía y órdenes de compra, permitiendo comparar cantidades ordenadas vs. recibidas y valores unitarios/totales e impuestos entre la orden de compra y la factura del proveedor. Almacena datos por empresa, proveedor (NIT), almacén y producto, facilitando la conciliación de recepciones con facturas en procesos de compras y abastecimiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ReceiptsVsPurchaseOrders';
GO
