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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la solicitud en flujo HIS-RIS (sistema de información radiológica): RC=Registro recibido por RIS, PR=En proceso en RIS, LE=Lectura/informe radiológico, RE=Registro validado, CA=Orden cancelada por RIS, CH=Cancelación enviada desde HIS. Tipo: VARCHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado que se maneja para el HIS. RC= Registro recibido por el RIS, PR= En proceso marcado por el RIS, LE= Cuando el registro contenga una lectura, RE=El registro ha sido validado, CA=Cuando el RIS cancele la orden, CH=Cuando el HIS envie una cancelación al RIS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del usuario/profesional del RIS que ejecutó el cambio de estado. Referencia al operador o sistema que modificó el registro. Tipo: VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'codigo_usuario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario en el RIS realiza el cambio de estado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'codigo_usuario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'codigo_usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora exacta de envío o cambio de estado entre HIS y RIS. Formato TIME. Búsquedas: momento, horario de envío.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'hora';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de envio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'hora';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'hora';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de envío o cambio de estado en el flujo HIS-RIS. Tipo: DATE. Búsquedas: día, fecha de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'fecha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la solicitud/orden de estudio radiológico. Código único que vincula la orden entre HIS y RIS. Tipo: INT. Búsquedas: solicitud, número de orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador ,  código único de solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY). Clave primaria que identifica cada registro de log en la tabla HKLOG. Tipo: INT, no nulo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autoincrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría (log) de solicitudes o eventos del sistema: guarda cada acción registrada con su fecha, hora, usuario responsable y estado resultante. Útil para rastrear quién hizo qué y cuándo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLOG';
