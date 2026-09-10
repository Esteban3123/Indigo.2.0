CREATE TABLE [dbo].[LaboratoryOrders] (
    [ID_COMPANY]              VARCHAR (9)   NULL,
    [TIPO IDENTIFICACION]     VARCHAR (2)   NULL,
    [NRO IDENTIFICACION]      VARCHAR (25)  NOT NULL,
    [NOMBRE PACIENTE]         VARCHAR (250) NULL,
    [ENTIDAD]                 VARCHAR (300) NULL,
    [GRUPO ATENCION]          VARCHAR (123) NULL,
    [CODIGO CENTRO ATENCION]  CHAR (10)     NOT NULL,
    [CENTRO ATENCION]         CHAR (100)    NOT NULL,
    [CODIGO UNIDAD FUNCIONAL] CHAR (10)     NOT NULL,
    [UNIDAD FUNCIONAL]        CHAR (60)     NOT NULL,
    [NRO INGRESO]             CHAR (10)     NOT NULL,
    [TIPO INGRESO]            VARCHAR (12)  NOT NULL,
    [NRO FOLIO]               NCHAR (10)    NOT NULL,
    [IDENTIFICACION MEDICO]   CHAR (20)     NOT NULL,
    [MEDICO]                  CHAR (60)     NOT NULL,
    [CODIGO ESPECIALIDAD]     CHAR (3)      NULL,
    [ESPECIALIDAD]            CHAR (60)     NULL,
    [FECHA HISTORIA]          DATETIME      NOT NULL,
    [CODIGO DIAGNOSTICO]      CHAR (4)      NULL,
    [DIAGNISTICO]             CHAR (350)    NULL,
    [TIPO CITA]               VARCHAR (23)  NOT NULL,
    [CODIGO SERVICIO]         CHAR (20)     NOT NULL,
    [DESCRIPCION]             VARCHAR (250) NULL,
    [CANTIDAD]                INT           NOT NULL,
    [TIPO SERVICIO]           VARCHAR (11)  NOT NULL,
    [CUPS]                    INT           NOT NULL,
    [DESCRIPCION CUPS]        VARCHAR (250) NULL,
    [GRUPO CUPS]              VARCHAR (100) NULL,
    [SUBGRUPO CUPS]           CHAR (300)    NOT NULL,
    [FECHA BUSQUEDA]          DATE          NULL,
    [ULT_ACTUAL]              DATETIME      NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de staging o reporte que almacena órdenes de laboratorio generadas en el HIS, consolidando en una sola fila la información del paciente, el médico solicitante, el centro y unidad funcional de atención, el diagnóstico (CIE) y el servicio solicitado con su codificación CUPS. Registra el número de ingreso, folio y tipo de cita asociados a cada orden. La columna `ULT_ACTUAL` sugiere que los registros se actualizan periódicamente mediante un proceso de sincronización o carga batch.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LaboratoryOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LaboratoryOrders';
GO
