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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de detalle de rol; timestamp de auditoría que registra cuándo se asignó la actividad al rol', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro; identificación del operador/administrador que realizó la asignación de actividad al rol (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad asignada; FK hacia tabla Activities que vincula las tareas/permisos permitidos en este rol de almacén/WHS', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'ActivitiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de actividades', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'ActivitiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'ActivitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rol de almacén/WHS; FK hacia tabla WHSRole que agrupa este detalle dentro de un rol específico de gestión de inventario', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'WHSRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del rol de WHS', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'WHSRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'WHSRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria única del detalle de rol; identificador secuencial del registro de asignación actividad-rol (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de actividades asignadas a cada rol de bodega (WHS). Registra qué actividades o permisos tiene habilitado cada rol, incluyendo el usuario y la fecha en que se realizó la asignación.', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'WHS', @level1type = N'TABLE', @level1name = N'WHSRoleDetail';
