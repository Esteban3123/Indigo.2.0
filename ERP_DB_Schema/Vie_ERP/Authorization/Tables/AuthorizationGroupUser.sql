CREATE TABLE [Authorization].[AuthorizationGroupUser] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationGroupId] INT          NOT NULL,
    [UserId]               INT          NOT NULL,
    [UserCode]             VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AuthorizationGroupUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationGroupUser_AuthorizationGroup] FOREIGN KEY ([AuthorizationGroupId]) REFERENCES [Authorization].[AuthorizationGroup] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationGroupUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationGroupUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationGroupUser', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationGroupUser', @level2type = N'COLUMN', @level2name = N'Id';

