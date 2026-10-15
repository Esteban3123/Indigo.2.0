CREATE TABLE [Payments].[DocumentSupportUser] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentSupportId] INT          NOT NULL,
    [UserId]            INT          NOT NULL,
    [UserCode]          VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_DocumentSupportUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentSupportId] FOREIGN KEY ([DocumentSupportId]) REFERENCES [Payments].[DocumentSupport] ([Id]),
    CONSTRAINT [FK_DocumentSupportUser_DocumentSupport] FOREIGN KEY ([DocumentSupportId]) REFERENCES [Payments].[DocumentSupport] ([Id])
);


GO
ALTER TABLE [Payments].[DocumentSupportUser] NOCHECK CONSTRAINT [FK_DocumentSupportId];


GO
ALTER TABLE [Payments].[DocumentSupportUser] NOCHECK CONSTRAINT [FK_DocumentSupportUser_DocumentSupport];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion que se esta dando permisos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'DocumentSupportId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorizacion a usuarios', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DocumentSupportUser', @level2type = N'COLUMN', @level2name = N'Id';

