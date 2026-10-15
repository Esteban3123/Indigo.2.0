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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecharegistro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagen en base64', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'pdf';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'URL de la imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'url_imagen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'via_ingreso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'nombre_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero registro medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'registro_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo tecnologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'codigo_tecnologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'hora de validacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'hora_validacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de validacion de la imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecha_validacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'fecha_proceso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo interno de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id unico de la orden de VIE HIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKMDM', @level2type = N'COLUMN', @level2name = N'ID';

