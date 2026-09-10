CREATE TABLE [Report].[RADICACIONES] (
    [FECHA DE BUSQUEDA]           DATE          NULL,
    [ID. PACIENTE]                VARCHAR (25)  NOT NULL,
    [NOMBRE PACIENTE]             CHAR (250)    NOT NULL,
    [EDAD AÑOS]                   INT           NULL,
    [EDAD MESES]                  INT           NULL,
    [TEL.MOVIL]                   VARCHAR (MAX) NOT NULL,
    [TEL.FIJO]                    VARCHAR (MAX) NOT NULL,
    [CORREO ELECTRONICO]          CHAR (50)     NULL,
    [FECHA ORDEN]                 DATE          NULL,
    [ESTADO RADICACION]           VARCHAR (10)  NULL,
    [# RADICADO]                  INT           NOT NULL,
    [FECHA RADICADO]              DATE          NULL,
    [FECHA CONFIRMACION]          DATE          NULL,
    [CX PRINCIPAL]                VARCHAR (300) NULL,
    [CUPS PRINCIPAL]              CHAR (20)     NOT NULL,
    [OTROS PROC]                  VARCHAR (300) NULL,
    [CUPS OTROS]                  CHAR (20)     NULL,
    [NOMBRE PROFESIONAL]          CHAR (60)     NULL,
    [ESPECIALIDAD]                CHAR (60)     NULL,
    [ORDEN MEDICA]                VARCHAR (2)   NOT NULL,
    [ORDEN DE INSUMOS]            VARCHAR (2)   NOT NULL,
    [CONSENTIMIENTO INFORMADO]    VARCHAR (2)   NOT NULL,
    [AUTORIZACION]                VARCHAR (2)   NOT NULL,
    [COPAGO]                      VARCHAR (2)   NOT NULL,
    [FECHA VIGENCIA AUTORIZACION] VARCHAR (2)   NOT NULL,
    [FECHA VIGENCIA]              DATETIME      NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que almacena el seguimiento de radicaciones de procedimientos quirúrgicos/médicos para pacientes, registrando datos demográficos y de contacto, fechas clave del proceso (orden, radicado, confirmación, vigencia de autorización), estado de la radicación, procedimientos con sus códigos CUPS, profesional y especialidad tratante, así como el cumplimiento de requisitos documentales como orden médica, consentimiento informado, autorización, copago e insumos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'RADICACIONES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'RADICACIONES';
GO
