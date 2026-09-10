CREATE TABLE [MixingStation].[ExternalCareCenterUsers] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ExternalCareCenterId] INT          NOT NULL,
    [UserId]               INT          NOT NULL,
    [UserCode]             VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ExternalCareCenterUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExternalCareCenterUsers_ExternalCareCenter] FOREIGN KEY ([ExternalCareCenterId]) REFERENCES [MixingStation].[ExternalCareCenter] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario de seguridad externo; identificador textual (VARCHAR 50) asignado al profesional de salud o personal administrativo del centro de atención externo para autenticación y auditoría en el ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del usuario de seguridad con permisos de acceso; referencia al usuario del sistema que tiene autorización para operar en nombre del centro de atención externo (FK implícita a tabla de usuarios).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del centro de atención externo (cabecera); FK a [ExternalCareCenter] que agrupa los usuarios autorizados por institución, clínica, laboratorio o unidad funcional externa.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la relación usuario-centro; clave primaria del registro de asignación de permisos entre usuario de seguridad y centro de atención externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los usuarios externos asociados a cada centro de atención externo (MixingStation). Vincula un usuario del sistema con un centro de atención externo y su código de usuario correspondiente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers';
