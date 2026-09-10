CREATE TABLE [WebAppointment].[SystemUser] (
    [IDUsername]   INT           IDENTITY (1, 1) NOT NULL,
    [Username]     VARCHAR (25)  NOT NULL,
    [Password]     VARCHAR (200) NOT NULL,
    [Email]        VARCHAR (150) NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [UpdateDate]   DATETIME      NULL,
    [Status]       BIT           NOT NULL,
    [NumMovil]     VARCHAR (10)  NOT NULL,
    [DocumentType] INT           NOT NULL,
    CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED ([IDUsername] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios del sistema de agendamiento web (WebAppointment). Guarda las credenciales de acceso, datos de contacto y estado de cada usuario registrado en la plataforma.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del usuario del sistema.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'IDUsername';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'IDUsername';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario para iniciar sesión en la plataforma de agendamiento.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'Username';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'Username';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña cifrada del usuario para autenticación en el sistema.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'Password';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'Password';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del usuario, usado para notificaciones y recuperación de acceso.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el usuario en el sistema.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del usuario.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'UpdateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'UpdateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo del usuario (1 = activo, 0 = inactivo).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de celular o móvil del usuario, usado para contacto o autenticación.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'NumMovil';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'NumMovil';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad del usuario (cédula, pasaporte, etc.), referencia a tabla maestra de tipos de documento.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'SystemUser', @level2type = N'COLUMN', @level2name = N'DocumentType';
