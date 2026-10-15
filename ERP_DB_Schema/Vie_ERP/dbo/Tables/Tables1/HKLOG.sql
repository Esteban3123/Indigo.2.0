CREATE TABLE [dbo].[HKLOG] (
    [ID]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [numero_solicitud] INT          NULL,
    [fecha]            DATE         NULL,
    [hora]             TIME (7)     NULL,
    [codigo_usuario]   VARCHAR (20) NULL,
    [estado]           VARCHAR (2)  NULL,
    CONSTRAINT [PK_KKLOG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado que se maneja para el HIS. RC= Registro recibido por el RIS, PR= En proceso marcado por el RIS, LE= Cuando el registro contenga una lectura, RE=El registro ha sido validado, CA=Cuando el RIS cancele la orden, CH=Cuando el HIS envie una cancelación al RIS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario en el RIS realiza el cambio de estado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'codigo_usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de envio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'hora';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de envio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'fecha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador ,  código único de solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autoincrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'ID';

