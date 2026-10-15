CREATE TABLE [WHS].[WHSRoleDetail] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WHSRoleId]    INT          NOT NULL,
    [ActivitiesId] INT          NOT NULL,
    [CreationUser] VARCHAR (20) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    CONSTRAINT [PK_WHSRoleDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__WHSRoleDe__Activ__7922C828] FOREIGN KEY ([ActivitiesId]) REFERENCES [WHS].[Activities] ([Id]),
    CONSTRAINT [FK__WHSRoleDe__WHSRo__7A16EC61] FOREIGN KEY ([WHSRoleId]) REFERENCES [WHS].[WHSRole] ([Id])
);


GO
ALTER TABLE [WHS].[WHSRoleDetail] NOCHECK CONSTRAINT [FK__WHSRoleDe__Activ__7922C828];


GO
ALTER TABLE [WHS].[WHSRoleDetail] NOCHECK CONSTRAINT [FK__WHSRoleDe__WHSRo__7A16EC61];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de actividades', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'ActivitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del rol de WHS', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'WHSRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'Id';

