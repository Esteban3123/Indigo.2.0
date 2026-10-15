CREATE TABLE [dbo].[HKMDM] (
    [ID]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [numero_solicitud]        INT             NULL,
    [codigo_interno_servicio] INT             NULL,
    [cups]                    VARCHAR (20)    NULL,
    [fecha_proceso]           DATETIME        NULL,
    [fecha_validacion]        DATE            NULL,
    [hora_validacion]         DATETIME        NULL,
    [codigo_tecnologo]        VARCHAR (20)    NULL,
    [registro_medico]         VARCHAR (150)   NULL,
    [nombre_medico]           VARCHAR (150)   NULL,
    [via_ingreso]             INT             NULL,
    [url_imagen]              VARCHAR (256)   NULL,
    [pdf]                     VARBINARY (MAX) NULL,
    [fecharegistro]           DATETIME        NULL,
    CONSTRAINT [PK_HKMDM] PRIMARY KEY CLUSTERED ([ID] ASC)
);

