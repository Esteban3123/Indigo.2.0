CREATE TABLE [WebAppointment].[QaNotificationWhitelist] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Channel]     INT            NOT NULL,
    [Value]       NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [IsActive]    BIT            CONSTRAINT [DF_QaWhitelist_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_QaNotificationWhitelist] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT, default=1) para activar/desactivar temporalmente el destino whitelist sin eliminarlo. Permite pausar notificaciones a un tester o número sin perder histórico de configuración.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'IsActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite habilitar/deshabilitar temporalmente el destinatario sin eliminarlo.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'IsActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anotación libre sobre el propósito, equipo o escenario (ej: ''''Tester QA – smoke tests'''', ''''Dev team – validación RIPS'''', ''''Ingreso urgencia''''). Facilita identificar el propietario y contexto de uso de este destino de prueba.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción libre del destinatario/purpose (ej.: “Tester QA – smoke tests”).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico o número de celular/móvil de prueba (NVARCHAR 255). Destino seguro donde sí se envían notificaciones durante QA para validar plantillas, flujos y entrega sin afectar usuarios reales.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección de correo o número de celular de prueba correspondiente al canal.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de canal de notificación (INT): EMAIL, WHATSAPP, SMS, PUSH u otro. Define si Value es correo electrónico o número de teléfono/móvil de prueba.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Channel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Canal de notificación (p. ej., EMAIL, WHATSAPP, SMS).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Channel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Channel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la entrada whitelist (INT IDENTITY, clave primaria). Secuencial, auto-incrementado, sin reutilización.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único (IDENTITY).', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de destinos de notificación validados (whitelist) para ambiente QA/Testing. Almacena emails y números de teléfono seguros (prueba, no reales) para evitar envíos accidentales a usuarios en producción durante ciclos de QA, smoke tests y validación de canales de comunicación.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de destinos “seguros” para notificaciones en QA (emails y números de prueba/whitelist). Evita enviar mensajes a personas reales.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'TABLE', @level1name = N'QaNotificationWhitelist';

