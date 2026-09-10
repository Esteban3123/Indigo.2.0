CREATE TABLE [Maintenance].[MaintenanceFailureRequestDetailNotification] (
    [Id]                                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceFailureRequestDetailId] INT           NOT NULL,
    [ThirdPartyId]                      INT           NOT NULL,
    [Email]                             VARCHAR (250) NOT NULL,
    [Status]                            BIT           NOT NULL,
    [CreationDate]                      DATETIME      NOT NULL,
    [ShippingDate]                      DATETIME      NULL,
    CONSTRAINT [PK_MaintenanceFailureRequestDetailNotification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceFailureRequestDetailNotification_MaintenanceFailureRequestDetail] FOREIGN KEY ([MaintenanceFailureRequestDetailId]) REFERENCES [Maintenance].[MaintenanceFailureRequestDetail] ([Id]),
    CONSTRAINT [FK_MaintenanceFailureRequestDetailNotification_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del envío de la notificación al tercero; NULL si aún no se ha enviado. Tipo: DATETIME, nullable.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del envío', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de notificación. Tipo: DATETIME, no nullable. Auditoría de cuándo se generó la notificación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del envío de notificación: 0=Pendiente de envío, 1=Enviado. Tipo: BIT (booleano). Indica si la notificación fue despachada al destinatario.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del envío      0. Aún no enviado      1. Enviado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del destinatario (tercero) para envío de notificación. Tipo: VARCHAR(250), PII ofuscado. Contacto principal para alertas de falla de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico del destinatario', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor, contratista, aliado) que recibe la notificación. Tipo: INT. FK a [Common].[ThirdParty].', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de la solicitud de mantenimiento correctivo asociado. Tipo: INT. FK a [Maintenance].[MaintenanceFailureRequestDetail]. Vincula la notificación a la falla específica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'MaintenanceFailureRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la solicitud de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'MaintenanceFailureRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'MaintenanceFailureRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de notificación. Tipo: INT IDENTITY(1,1), clave primaria. Llave de auditoría y trazabilidad del envío.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de notificaciones enviadas a terceros (proveedores, contratistas u otros) sobre el detalle de una solicitud de falla en mantenimiento. Permite rastrear a quién se notificó, por qué correo electrónico, cuándo se creó la notificación y cuándo fue despachada.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification';
