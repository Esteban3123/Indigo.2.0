CREATE TABLE [Authorization].[PostponementReasonsUser] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PostponementReasonsId] INT          NOT NULL,
    [UserId]                INT          NOT NULL,
    [UserCode]              VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PostponementReasonsUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PostponementReasonsUser_PostponementReasons] FOREIGN KEY ([PostponementReasonsId]) REFERENCES [Authorization].[PostponementReasons] ([Id])
);


GO
ALTER TABLE [Authorization].[PostponementReasonsUser] NOCHECK CONSTRAINT [FK_PostponementReasonsUser_PostponementReasons];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cancelación que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PostponementReasonsUser', @level2type = N'COLUMN', @level2name = N'Id';

