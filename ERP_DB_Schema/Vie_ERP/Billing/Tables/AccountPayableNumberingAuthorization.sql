CREATE TABLE [Billing].[AccountPayableNumberingAuthorization] (
    [Id]                       INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableId]         INT         NOT NULL,
    [NumberingAuthorizationId] INT         NOT NULL,
    [InvoicePrefix]            VARCHAR (6) NULL,
    [Consecutive]              BIGINT      NOT NULL,
    CONSTRAINT [PK_AccountPayableNumberingAuthorization__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableId] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_NumberingAuthorization_Id] FOREIGN KEY ([NumberingAuthorizationId]) REFERENCES [Billing].[NumberingAuthorization] ([Id])
);


GO
ALTER TABLE [Billing].[AccountPayableNumberingAuthorization] NOCHECK CONSTRAINT [FK_AccountPayableId];


GO
ALTER TABLE [Billing].[AccountPayableNumberingAuthorization] NOCHECK CONSTRAINT [FK_NumberingAuthorization_Id];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo del documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'NumberingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'Id';

