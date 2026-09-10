CREATE TABLE [dbo].[serviciospendientes] (
    [ID_COMPANY]                               VARCHAR (9)     NULL,
    [NIT ENTIDAD]                              VARCHAR (25)    NULL,
    [ENTIDAD]                                  VARCHAR (50)    NULL,
    [CODIGO GRUPO]                             VARCHAR (20)    NOT NULL,
    [GRUPO DE ATENCION]                        VARCHAR (50)    NULL,
    [INGRESO]                                  VARCHAR (10)    NULL,
    [FECHA INGRESO]                            DATETIME        NOT NULL,
    [FECHA EGRESO]                             VARCHAR (1)     NOT NULL,
    [TIPO IDENTIFICACION]                      VARCHAR (31)    NOT NULL,
    [IDENTIFICACION]                           VARCHAR (25)    NOT NULL,
    [PACIENTE]                                 VARCHAR (250)   NULL,
    [ESTADO FACTURA]                           VARCHAR (11)    NOT NULL,
    [NRO FACTURA]                              VARCHAR (1)     NOT NULL,
    [FECHA FACTURA]                            VARCHAR (10)    NOT NULL,
    [TOTAL FACTURA]                            VARCHAR (1)     NOT NULL,
    [VR TOTAL DESCUENTO]                       VARCHAR (1)     NOT NULL,
    [VR TOTAL ENTIDAD]                         VARCHAR (1)     NOT NULL,
    [VR TOTAL CUOTA RECUPERACION]              VARCHAR (1)     NOT NULL,
    [CAB_ORD_NRO]                              INT             NOT NULL,
    [CAB_ORD_FECHA_ORDEN]                      DATETIME        NOT NULL,
    [DET_ORD_TIPO]                             VARCHAR (12)    NOT NULL,
    [DET_ORD_GRUPO FACTURACION]                VARCHAR (100)   NULL,
    [DET_ORD_GRUPO]                            VARCHAR (121)   NULL,
    [DET_ORD_SUBGRUPO]                         VARCHAR (321)   NULL,
    [DET_ORD_PRESENTACION]                     VARCHAR (13)    NOT NULL,
    [DET_ORD_CUPS]                             VARCHAR (20)    NOT NULL,
    [DET_ORD_DESCRIPCION_CUPS]                 VARCHAR (60)    NULL,
    [DET_ORD_CODIGO_SERVICIO/PRODUCTO]         VARCHAR (20)    NULL,
    [DET_ORD_DESCRIPCION_SERVICIO/PRODUCTO]    VARCHAR (60)    NULL,
    [DET_ORDQX_SUBCODIGO]                      VARCHAR (20)    NOT NULL,
    [DET_ORDQX_SUBNOMBRE]                      VARCHAR (60)    NULL,
    [DET VISUALIZA FACTURA]                    VARCHAR (2)     NOT NULL,
    [DET_ORD_PAQUETE]                          VARCHAR (33)    NOT NULL,
    [DET_ORD_INCLUIDO_EN_PAQUETE]              VARCHAR (22)    NOT NULL,
    [DET_ORD_CUPS_PAQUETE]                     VARCHAR (20)    NOT NULL,
    [DET_ORD_DESCRIPCION_CUPS_PAQUETE]         VARCHAR (300)   NOT NULL,
    [DET_ORD_TIPO_LIQUIDACION]                 VARCHAR (56)    NOT NULL,
    [DET_ORD_CUPS_INCLUIDO]                    VARCHAR (20)    NOT NULL,
    [DET_ORD_DESCRIPCION_CUPS_INCLUIDO]        VARCHAR (60)    NULL,
    [DET_ORD_CODIGO_UNIDAD_FUNCIONAL]          VARCHAR (20)    NOT NULL,
    [DET_ORD_UNIDAD_FUNCIONAL_PRESTO_SERVICIO] VARCHAR (50)    NULL,
    [DET_ORD_COD_CENTRO_COSTO]                 VARCHAR (20)    NULL,
    [DET_ORD_CENTRO_COSTO]                     VARCHAR (200)   NULL,
    [DET_ORD_IDENTIFICACION_PROFESIONAL]       VARCHAR (20)    NULL,
    [DET_ORD_PROFESIONAL]                      VARCHAR (60)    NULL,
    [DET_ORD_ESPECIALIDAD]                     VARCHAR (60)    NULL,
    [DET_ORD_CANTIDAD]                         INT             NOT NULL,
    [DET_ORD_VALOR_SERVICIO]                   NUMERIC (18)    NOT NULL,
    [DET_ORD_VALOR_UNITARIO]                   NUMERIC (18)    NULL,
    [DET_ORD_VALOR_TOTAL]                      NUMERIC (18)    NOT NULL,
    [DET_ORD_TECNOLOGIA_PBS]                   VARCHAR (6)     NOT NULL,
    [FECHA ALTA MEDICA]                        DATETIME        NULL,
    [FECHA ANULACION]                          VARCHAR (1)     NOT NULL,
    [CAMA ACTIVA]                              CHAR (10)       NULL,
    [ID_ORDE_DETALLE]                          INT             NOT NULL,
    [ID_FACTURA]                               VARCHAR (1)     NOT NULL,
    [ID ENTIDAD]                               INT             NULL,
    [TIPO REGISTRO]                            VARCHAR (1)     NOT NULL,
    [FECHA BUSQUEDA]                           DATE            NULL,
    [FECHA SERVICIO BUSQUEDA]                  DATE            NULL,
    [AMBITO DE INGRESO]                        VARCHAR (35)    NULL,
    [UNIDAD FUNCIONAL DE INGRESO]              VARCHAR (71)    NULL,
    [UNIDAD FUNCIONAL DE EGRESO]               VARCHAR (71)    NULL,
    [AMBITO DE EGRESO]                         VARCHAR (35)    NULL,
    [FACTURADO]                                VARCHAR (2)     NOT NULL,
    [CANTIDAD]                                 INT             NOT NULL,
    [AÑO FECHA BUSQUEDA]                       INT             NULL,
    [MES AÑO FECHA BUSQUEDA]                   INT             NULL,
    [MES NOMBRE FECHA BUSQUEDA]                VARCHAR (10)    NULL,
    [DIA FECHA BUSQUEDA]                       NVARCHAR (4000) NULL,
    [MES_LABEL_BUSQUEDA]                       NVARCHAR (4000) NOT NULL,
    [ULT_ACTUAL]                               DATETIME        NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha del último actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'ULT_ACTUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda MES_LABEL_BUSQUEDA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'MES_LABEL_BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda DIA FECHA BUSQUEDA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DIA FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el mes nombre fecha busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'MES NOMBRE FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el mes año fecha busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'MES AÑO FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el año fecha busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'AÑO FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda FACTURADO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'FACTURADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ambito de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'AMBITO DE EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la unidad funcional de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'UNIDAD FUNCIONAL DE EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la unidad funcional de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'UNIDAD FUNCIONAL DE INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ambito de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'AMBITO DE INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha del servicio de busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'FECHA SERVICIO BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'TIPO REGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ID de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'ID ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ID de la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'ID_FACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ID del orden de detalle', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'ID_ORDE_DETALLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cama activa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'CAMA ACTIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'FECHA ANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de alta médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'FECHA ALTA MEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda ORD_TECNOLOGIA_PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_TECNOLOGIA_PBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor total ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_VALOR_TOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor unitario ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_VALOR_UNITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor del servicio ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_VALOR_SERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cantidad ordinaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la epecialidad ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_ESPECIALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el profesional ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_PROFESIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la identificación del profesional ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_IDENTIFICACION_PROFESIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el centro de costo ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_CENTRO_COSTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el codigo del centro de costo ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_COD_CENTRO_COSTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la unidad funcional que presto el servicio ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_UNIDAD_FUNCIONAL_PRESTO_SERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el codigo de la unidad funcional ordinaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_CODIGO_UNIDAD_FUNCIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la descripción de CUPS ordinarios incluido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_DESCRIPCION_CUPS_INCLUIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guardan los CUPS incluido ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_CUPS_INCLUIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda ORD_TIPO_LIQUIDACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_TIPO_LIQUIDACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la descripción de los paquetes de CUPS ordinarios ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_DESCRIPCION_CUPS_PAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guardan los paquetes de CUPS ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_CUPS_PAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda ORD_INCLUIDO_EN_PAQUETE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_INCLUIDO_EN_PAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el paquete ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_PAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda VISUALIZA FACTURA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET VISUALIZA FACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el subnombre quirurgico ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORDQX_SUBNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el subcodigo quirurgico ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORDQX_SUBCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la descripción de servicio/producto ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_DESCRIPCION_SERVICIO/PRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda ORD_CODIGO_SERVICIO/PRODUCTO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_CODIGO_SERVICIO/PRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la descripción de CUPS ordinarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_DESCRIPCION_CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guardan los CUPS ordinarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la presentación ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_PRESENTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el subgrupo ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_SUBGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el grupo ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_GRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el grupo de facturación ordinario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_GRUPO FACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda ORD_TIPO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'DET_ORD_TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda CAB_ORD_FECHA_ORDEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'CAB_ORD_FECHA_ORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda CAB_ORD_NRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'CAB_ORD_NRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de la cuota de recuperación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'VR TOTAL CUOTA RECUPERACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'VR TOTAL ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de descuento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'VR TOTAL DESCUENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el total de la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'TOTAL FACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'FECHA FACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el Nro de factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'NRO FACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el estado de la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'ESTADO FACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'PACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'TIPO IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'FECHA EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'FECHA INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el grupo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'GRUPO DE ATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el código de grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'CODIGO GRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la el nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el NIT de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'NIT ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ID de la compañia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'serviciospendientes', @level2type = N'COLUMN', @level2name = N'ID_COMPANY';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de staging/reporte que consolida los servicios clínicos ordenados a pacientes que aún no han sido facturados o están pendientes de facturación. Almacena el detalle de cada ítem de orden (código CUPS, descripción, tipo de liquidación, valores unitarios y totales, profesional y unidad funcional prestadora) junto con datos del ingreso hospitalario, entidad aseguradora y estado de factura. Incluye campos calculados de fechas (año, mes, día) para facilitar filtros y agrupaciones en reportes de cartera y gestión de facturación pendiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'serviciospendientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'serviciospendientes';
GO
