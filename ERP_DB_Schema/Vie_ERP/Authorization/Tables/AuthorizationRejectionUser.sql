CREATE TABLE [Authorization].[AuthorizationRejectionUser] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationRejectionId] INT          NOT NULL,
    [UserId]                   INT          NOT NULL,
    [UserCode]                 VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AuthorizationRejectionUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationRejectionUser_AuthorizationRejection] FOREIGN KEY ([AuthorizationRejectionId]) REFERENCES [Authorization].[AuthorizationRejection] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationRejectionUser] NOCHECK CONSTRAINT [FK_AuthorizationRejectionUser_AuthorizationRejection];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationRejectionUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationRejectionUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rechazo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationRejectionUser', @level2type = N'COLUMN', @level2name = N'AuthorizationRejectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationRejectionUser', @level2type = N'COLUMN', @level2name = N'Id';

