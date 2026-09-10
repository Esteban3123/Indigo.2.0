CREATE TABLE [dbo].[PermissionRoll] (
    [Id]          INT         NOT NULL,
    [IdRoll]      INT         NOT NULL,
    [IdForm]      VARCHAR (5) NOT NULL,
    [Action]      VARCHAR (5) NOT NULL,
    [ActionValue] BIT         NOT NULL,
    [TimeStamp]   ROWVERSION  NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena los permisos asignados a roles del sistema, vinculando cada rol con un formulario específico y una acción determinada. Para cada combinación rol-formulario-acción, registra mediante un valor booleano si dicha acción está habilitada o no. La columna ROWVERSION permite controlar la concurrencia optimista en actualizaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PermissionRoll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PermissionRoll';
GO
