CREATE TABLE [Report].[IMAG2] (
    [ID PACIENTE]               VARCHAR (25)    NOT NULL,
    [NOMBRE PACIENTE]           CHAR (250)      NOT NULL,
    [COD. CUPS]                 CHAR (20)       NOT NULL,
    [NOMBRE CUPS]               CHAR (300)      NOT NULL,
    [FECHA ORDEN]               DATETIME        NOT NULL,
    [TOMA DE IMAGEN]            DATETIME        NULL,
    [FECHA TRANSCRIPCION]       DATETIME        NULL,
    [FECHA LECTURA]             DATETIME        NULL,
    [ESTADO DEL SERVICIO]       VARCHAR (20)    NULL,
    [ESTADO FACTURACION]        VARCHAR (20)    NULL,
    [VALOR FACTURADO]           DECIMAL (20, 2) NULL,
    [FECHA BUSQUEDA]            DATE            NULL,
    [AÑO FECHA BUSQUEDA]        INT             NULL,
    [MES AÑO FECHA BUSQUEDA]    INT             NULL,
    [MES NOMBRE FECHA BUSQUEDA] VARCHAR (10)    NULL,
    [DIA FECHA BUSQUEDA]        INT             NULL,
    [ULT_ACTUAL]                DATETIME        NULL
);

