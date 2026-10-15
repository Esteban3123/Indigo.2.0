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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'via_ingreso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Url de la Imagen ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'url_imagen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del médico remitente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'nombre_medico_remitente';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Documento del médico remitente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'doc_medico_remitente';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'nombre_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'registro_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_radiologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_tecnologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exámen validado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'resultados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de envio  HH:MM:SS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'hora_validacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de envio YYYY-MM-DD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'fecha_validacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Códificacion única de procedimientos en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código interno del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador y llave primaria, código único de solictud.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autoincrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKESCRITURA', @level2type = N'COLUMN', @level2name = N'ID';

