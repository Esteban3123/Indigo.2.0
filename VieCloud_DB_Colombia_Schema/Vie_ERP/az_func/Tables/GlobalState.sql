CREATE TABLE [az_func].[GlobalState] (
    [UserFunctionID]  CHAR (16) NOT NULL,
    [UserTableID]     INT       NOT NULL,
    [LastSyncVersion] BIGINT    NOT NULL,
    [LastAccessTime]  DATETIME  DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserFunctionID] ASC, [UserTableID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del estado de sincronización de tablas por función de usuario. Controla la última versión sincronizada y el último acceso, permitiendo gestionar la consistencia de datos entre módulos o sesiones del sistema.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la función o módulo del usuario al que pertenece el estado de sincronización.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState', @level2type = N'COLUMN', @level2name = N'UserFunctionID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState', @level2type = N'COLUMN', @level2name = N'UserFunctionID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla del sistema cuyo estado de sincronización se está registrando.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState', @level2type = N'COLUMN', @level2name = N'UserTableID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState', @level2type = N'COLUMN', @level2name = N'UserTableID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de versión de la última sincronización realizada, usado para detectar cambios o actualizaciones pendientes.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState', @level2type = N'COLUMN', @level2name = N'LastSyncVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState', @level2type = N'COLUMN', @level2name = N'LastSyncVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último acceso registrado para esta combinación de función y tabla.', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState', @level2type = N'COLUMN', @level2name = N'LastAccessTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'az_func', @level1type = N'TABLE', @level1name = N'GlobalState', @level2type = N'COLUMN', @level2name = N'LastAccessTime';
