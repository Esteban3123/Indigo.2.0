CREATE TABLE [Billing].[NumberingAuthorizationUser] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NumberingAuthorizationId] INT          NOT NULL,
    [UserId]                   INT          NOT NULL,
    [UserCode]                 VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NumberingAuthorizationUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NumberingAuthorizationId] FOREIGN KEY ([NumberingAuthorizationId]) REFERENCES [Billing].[NumberingAuthorization] ([Id]),
    CONSTRAINT [FK_NumberingAuthorizationUser_NumberingAuthorization] FOREIGN KEY ([NumberingAuthorizationId]) REFERENCES [Billing].[NumberingAuthorization] ([Id])
);


GO
ALTER TABLE [Billing].[NumberingAuthorizationUser] NOCHECK CONSTRAINT [FK_NumberingAuthorizationId];


GO
ALTER TABLE [Billing].[NumberingAuthorizationUser] NOCHECK CONSTRAINT [FK_NumberingAuthorizationUser_NumberingAuthorization];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'NumberingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'NumberingAuthorizationUser', @level2type = N'COLUMN', @level2name = N'Id';

