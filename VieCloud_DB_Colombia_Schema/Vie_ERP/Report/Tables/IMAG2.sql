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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que almacena el seguimiento del ciclo de vida de órdenes de imagenología, registrando desde la solicitud hasta la facturación. Captura las fechas clave del proceso (orden, toma, transcripción y lectura), identificando al paciente y el procedimiento mediante código CUPS. Incluye campos derivados de una fecha de búsqueda (año, mes, día) para facilitar análisis temporales, junto con el estado del servicio, facturación y valor facturado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'IMAG2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'IMAG2';
GO
