CREATE TABLE [Integrations].[SyncNuevaEpsMirth] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [InvoiceId]    INT      NOT NULL,
    [Status]       INT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    CONSTRAINT [PK_SyncNuevaEpsMirth] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'SyncNuevaEpsMirth', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado manejado por Mirth para notificar el envio a NuevaEPS (1- Sin procesar, 2- Procesado, 3- Fallido)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'SyncNuevaEpsMirth', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la factura relacionado a Billing.Invoice', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'SyncNuevaEpsMirth', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'SyncNuevaEpsMirth', @level2type = N'COLUMN', @level2name = N'Id';

