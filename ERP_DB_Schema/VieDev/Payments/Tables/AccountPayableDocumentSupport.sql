CREATE TABLE [Payments].[AccountPayableDocumentSupport] (
    [Id]                INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableId]  INT         NOT NULL,
    [DocumentSupportId] INT         NOT NULL,
    [InvoicePrefix]     VARCHAR (6) NULL,
    [Consecutive]       BIGINT      NOT NULL,
    CONSTRAINT [PK_AccountPayableDocumentSupport__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableId] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_DocumentSupport_Id] FOREIGN KEY ([DocumentSupportId]) REFERENCES [Payments].[DocumentSupport] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableDocumentSupport] NOCHECK CONSTRAINT [FK_AccountPayableId];


GO
ALTER TABLE [Payments].[AccountPayableDocumentSupport] NOCHECK CONSTRAINT [FK_DocumentSupport_Id];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccountPayableDocumentSupport]
    ON [Payments].[AccountPayableDocumentSupport]([AccountPayableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del documento soporte', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDocumentSupport', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo del documento soporte', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDocumentSupport', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento soporte', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDocumentSupport', @level2type = N'COLUMN', @level2name = N'DocumentSupportId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDocumentSupport', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDocumentSupport', @level2type = N'COLUMN', @level2name = N'Id';

