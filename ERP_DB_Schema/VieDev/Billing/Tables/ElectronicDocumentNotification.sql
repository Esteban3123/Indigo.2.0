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

