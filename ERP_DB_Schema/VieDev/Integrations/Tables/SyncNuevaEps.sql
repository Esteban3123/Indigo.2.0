CREATE TABLE [Integrations].[SyncNuevaEps] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [InvoiceId]    INT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    CONSTRAINT [PK_SyncNuevaEps] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'SyncNuevaEps', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la factura relacionado a Billing.Invoice', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'SyncNuevaEps', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'SyncNuevaEps', @level2type = N'COLUMN', @level2name = N'Id';

