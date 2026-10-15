CREATE TABLE [Authorization].[CancellationReasonsUser] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CancellationReasonsId] INT          NOT NULL,
    [UserId]                INT          NOT NULL,
    [UserCode]              VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CancellationReasonsUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CancellationReasonsUser_CancellationReasons] FOREIGN KEY ([CancellationReasonsId]) REFERENCES [Authorization].[CancellationReasons] ([Id])
);


GO
ALTER TABLE [Authorization].[CancellationReasonsUser] NOCHECK CONSTRAINT [FK_CancellationReasonsUser_CancellationReasons];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'CancellationReasonsUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'CancellationReasonsUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cancelación que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'CancellationReasonsUser', @level2type = N'COLUMN', @level2name = N'CancellationReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'CancellationReasonsUser', @level2type = N'COLUMN', @level2name = N'Id';

