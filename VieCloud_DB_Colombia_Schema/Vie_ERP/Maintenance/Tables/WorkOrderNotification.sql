CREATE TABLE [Maintenance].[WorkOrderNotification] (
    [Id]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]  INT           NOT NULL,
    [ThirdPartyId] INT           NOT NULL,
    [Type]         TINYINT       NOT NULL,
    [Email]        VARCHAR (250) NOT NULL,
    [Status]       BIT           NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [ShippingDate] DATETIME      NULL,
    CONSTRAINT [PK_WorkOrderNotification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderNotification_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_WorkOrderNotification_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME, nullable) efectiva de envío de la notificación por correo electrónico. NULL si aún no se ha enviado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del envío', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de notificación. Registro de auditoría.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de envío de la notificación (BIT): 0=No enviado, 1=Enviado/entregado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del envío      0. Aún no enviado      1. Enviado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico (VARCHAR 250) del destinatario de la notificación. Campo PII sensible.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico del destinatario', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de notificación (TINYINT): 0=Mantenimiento Programado, 1=Asignación, 2=Confirmación, 3=Anulación, 4=Aceptación, 5=Rechazo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de registro      0. Mantenimiento Programado   1. Asignación   2. Confirmación   3. Anulación   4. Aceptación      5. Rechazo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del tercero o proveedor de mantenimiento. Referencia a Common.ThirdParty.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la orden de trabajo asociada. Referencia a Maintenance.WorkOrder.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de notificación de orden de trabajo. Clave primaria autoincrementada.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de notificaciones enviadas o pendientes asociadas a órdenes de trabajo de mantenimiento. Guarda a qué tercero se notificó, por qué medio (email), el tipo de notificación, su estado y las fechas de creación y envío.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderNotification';
