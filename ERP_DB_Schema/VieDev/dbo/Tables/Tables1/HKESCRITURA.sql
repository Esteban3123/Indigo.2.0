CREATE TABLE [dbo].[HKESCRITURA] (
    [ID]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [numero_solicitud]        INT           NULL,
    [codigo_interno_servicio] INT           NULL,
    [cups]                    VARCHAR (20)  NULL,
    [fecha_validacion]        DATE          NULL,
    [hora_validacion]         TIME (7)      NULL,
    [resultados]              VARCHAR (MAX) NULL,
    [codigo_tecnologo]        VARCHAR (20)  NULL,
    [codigo_radiologo]        VARCHAR (20)  NULL,
    [registro_medico]         VARCHAR (20)  NULL,
    [nombre_medico]           VARCHAR (120) NULL,
    [doc_medico_remitente]    VARCHAR (20)  NULL,
    [nombre_medico_remitente] VARCHAR (120) NULL,
    [url_imagen]              VARCHAR (256) NULL,
    [via_ingreso]             INT           NULL,
    CONSTRAINT [PK_HKESCRITURA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador y llave primaria, código único de solictud.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';

