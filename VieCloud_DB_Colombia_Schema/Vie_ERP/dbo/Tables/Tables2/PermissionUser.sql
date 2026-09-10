CREATE TABLE [dbo].[PermissionUser] (
    [Id]          INT         NOT NULL,
    [IdUser]      INT         NOT NULL,
    [IdForm]      VARCHAR (5) NOT NULL,
    [Action]      VARCHAR (3) NOT NULL,
    [ActionValue] BIT         NOT NULL,
    [TenantId]    SMALLINT    NULL,
    [TimeStamp]   ROWVERSION  NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de control de acceso que almacena los permisos específicos asignados a usuarios individuales sobre formularios o módulos del sistema. Cada registro asocia un usuario con un formulario e indica una acción concreta (probablemente C/R/U/D) y si dicha acción está habilitada o no mediante un valor booleano. Soporta arquitectura multitenant mediante `TenantId`.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PermissionUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PermissionUser';
GO
