CREATE TABLE [Billing].[BillingAuthorizationUser] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingAuthorizationId] INT          NOT NULL,
    [UserId]                 INT          NOT NULL,
    [UserCode]               VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_BillingAuthorizationUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingAuthorizationUser_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id])
);


GO
ALTER TABLE [Billing].[BillingAuthorizationUser] NOCHECK CONSTRAINT [FK_BillingAuthorizationUser_BillingAuthorization];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'Id';

