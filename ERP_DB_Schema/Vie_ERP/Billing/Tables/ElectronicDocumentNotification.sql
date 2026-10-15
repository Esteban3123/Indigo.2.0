CREATE TABLE [Billing].[ElectronicDocumentNotification] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicDocumentId] INT           NOT NULL,
    [Email]                VARCHAR (100) NOT NULL,
    [Status]               BIT           NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ShippingDate]         DATETIME      NULL,
    CONSTRAINT [PK_ElectronicDocumentNotification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicDocumentNotification_ElectronicDocument] FOREIGN KEY ([ElectronicDocumentId]) REFERENCES [Billing].[ElectronicDocument] ([Id])
);


GO
ALTER TABLE [Billing].[ElectronicDocumentNotification] NOCHECK CONSTRAINT [FK_ElectronicDocumentNotification_ElectronicDocument];


GO
CREATE NONCLUSTERED INDEX [IX_ElectronicDocumentNotification_Status]
    ON [Billing].[ElectronicDocumentNotification]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha y hora del envio de la notificación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creacion de la notificación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica que el envío de la notificación de la factura electrónica se ha completado en estado = 1.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentNotification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electronico de donde se envia la factura electronica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentNotification', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del documento electronico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentNotification', @level2type = N'COLUMN', @level2name = N'ElectronicDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de las notificaciones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentNotification', @level2type = N'COLUMN', @level2name = N'Id';

