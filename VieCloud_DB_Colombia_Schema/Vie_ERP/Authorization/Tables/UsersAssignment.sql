-- =============================================================================
-- Usuarios facturadores asignados a los ingresos hospitalarios, gestionados
-- desde Authorization.AccountManagementParameters. UserCode referencia de forma
-- lógica (no FK física, distinta base de datos) al usuario de seguridad
-- (INDIGOSECV2), igual convención que AuthorizationScheduleTemplateUsers.
-- =============================================================================

CREATE TABLE [Authorization].[UsersAssignment] (
    [Id]         INT IDENTITY(1,1) NOT NULL,
    [UserCode]   VARCHAR(20) NOT NULL,
    [FullName]   NVARCHAR(100) NOT NULL,
    [Status]     BIT NOT NULL CONSTRAINT [DF_UsersAssignment_Status] DEFAULT ((1)),
    [EntryType]  TINYINT NOT NULL CONSTRAINT [DF_UsersAssignment_EntryType] DEFAULT ((0)),  -- 1=Hospitalario
    [IsRemoved]  BIT NOT NULL CONSTRAINT [DF_UsersAssignment_IsRemoved] DEFAULT ((0)),
    CONSTRAINT [PK_UsersAssignment] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX_UsersAssignment_UserCode]
    ON [Authorization].[UsersAssignment] ([UserCode] ASC)
    WHERE [IsRemoved] = 0
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de usuario asignado; clave primaria IDENTITY; INT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario de seguridad (base de datos INDIGOSECV2) asignado como facturador; referencia lógica, sin FK física por ser una base de datos distinta; VARCHAR(20) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario asignado, almacenado de forma denormalizada para evitar una consulta cruzada a INDIGOSECV2 en cada listado; NVARCHAR(100) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo del usuario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario asignado está activo para recibir asignaciones automáticas; BIT NOT NULL DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado activo/inactivo del usuario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso para el que el usuario está asignado: 1=Hospitalario; TINYINT NOT NULL DEFAULT 0.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de ingreso: 1=Hospitalario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de eliminación lógica: 1 indica que el usuario fue quitado de la asignación desde el cliente (no se elimina físicamente la fila); BIT NOT NULL DEFAULT 0.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca de eliminación lógica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios facturadores asignados a los ingresos hospitalarios en el módulo de gestión de cuentas. Cada usuario puede tener novedades asociadas en Authorization.UserNovelties. La eliminación es lógica (IsRemoved), no física.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment';
GO
