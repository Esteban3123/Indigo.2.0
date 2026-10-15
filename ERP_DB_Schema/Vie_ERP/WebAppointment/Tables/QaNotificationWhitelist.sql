CREATE TABLE [WebAppointment].[QaNotificationWhitelist] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Channel]     INT            NOT NULL,
    [Value]       NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [IsActive]    BIT            CONSTRAINT [DF_QaWhitelist_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_QaNotificationWhitelist] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permite habilitar/deshabilitar temporalmente el destinatario sin eliminarlo.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre del destinatario/purpose (ej.: “Tester QA – smoke tests”).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo o número de celular de prueba correspondiente al canal.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Canal de notificación (p. ej., EMAIL, WHATSAPP, SMS).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Channel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de destinos “seguros” para notificaciones en QA (emails y números de prueba/whitelist). Evita enviar mensajes a personas reales.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist';

