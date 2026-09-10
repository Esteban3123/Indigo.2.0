-- =============================================================================
-- Parámetros de gestión de autorización (asignación automática de usuarios
-- facturadores a los ingresos hospitalarios). Registro único (settings-style,
-- igual que Authorization.SettingsAuthorization): la aplicación siempre
-- lee/escribe la única fila existente (no hay columna de tenant/centro de atención).
-- =============================================================================

CREATE TABLE [Authorization].[AuthorizationManagementParameters] (
    [Id]                    INT IDENTITY(1,1) NOT NULL,
    [AutomaticAssignment]   BIT NOT NULL,
    [EntryType]             VARCHAR(50) NULL,   -- claves de tipo de ingreso seleccionadas, separadas por coma (ej. "1")
    [StartDateAssignment]   DATETIME NULL,
    [CreationUser]          VARCHAR(20) NOT NULL,
    [CreationDate]          DATETIME NOT NULL CONSTRAINT [DF_AuthorizationManagementParameters_CreationDate] DEFAULT (GETUTCDATE()),
    [ModificationUser]      VARCHAR(20) NULL,
    [ModificationDate]      DATETIME NULL,
    CONSTRAINT [PK_AuthorizationManagementParameters] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de parámetros de gestión de autorización; clave primaria IDENTITY; INT. Se espera una única fila en la tabla.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la asignación de usuarios facturadores a los ingresos hospitalarios se realiza de forma automática; BIT NOT NULL. Cuando es 0, los segmentos de tipo de ingreso, fecha de asignación y usuarios asignados no aplican.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'AutomaticAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la asignación es automática', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'AutomaticAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'AutomaticAssignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Claves del/los tipo(s) de ingreso para los que aplica la asignación automática, separadas por coma (selección múltiple); VARCHAR(50) NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Claves de tipo de ingreso seleccionadas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha a partir de la cual aplica la asignación automática de usuarios facturadores; DATETIME NULL, requerida en el cliente cuando AutomaticAssignment = 1.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'StartDateAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de la asignación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'StartDateAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'StartDateAssignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro de parámetros; VARCHAR(20) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creó el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de creación del registro; DATETIME NOT NULL DEFAULT GETUTCDATE().', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de creación del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación del registro; VARCHAR(20) NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modificó el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de la última modificación del registro; DATETIME NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de la última modificación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de gestión de autorización: configura si la asignación de usuarios facturadores a los ingresos hospitalarios es automática, a partir de qué fecha, y para qué tipo de ingreso aplica. Registro único (settings-style); los usuarios asignados y sus novedades se almacenan en Authorization.UsersAssignment y Authorization.UserNovelties respectivamente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters';
GO
