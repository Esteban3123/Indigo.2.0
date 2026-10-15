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
ALTER TABLE [Maintenance].[MaintenanceFailureRequestDetailNotification] NOCHECK CONSTRAINT [FK_MaintenanceFailureRequestDetailNotification_MaintenanceFailureRequestDetail];


GO
ALTER TABLE [Maintenance].[MaintenanceFailureRequestDetailNotification] NOCHECK CONSTRAINT [FK_MaintenanceFailureRequestDetailNotification_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del envío', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del envío      0. Aún no enviado      1. Enviado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electrónico del destinatario', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la solicitud de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'MaintenanceFailureRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceFailureRequestDetailNotification', @level2type = N'COLUMN', @level2name = N'Id';

