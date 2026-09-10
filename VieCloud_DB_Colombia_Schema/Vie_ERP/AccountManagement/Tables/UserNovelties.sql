CREATE TABLE [AccountManagement].[UserNovelties] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [AssignedUserId] INT            NOT NULL,
    [NoveltyDate]    DATETIME       DEFAULT (getdate()) NOT NULL,
    [Description]    NVARCHAR (300) NOT NULL,
    [IsUserActive]   BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserNovelties_UsersAssignment] FOREIGN KEY ([AssignedUserId]) REFERENCES [AccountManagement].[UsersAssignment] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estado resultante (BIT: 1=activo, 0=inactivo) del usuario después de la novedad; refleja si el usuario quedó habilitado o deshabilitado en el sistema.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del usuario debido a la novedad, 1 = activo, 0 = inactivo', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (hasta 300 caracteres) del motivo, tipo o detalle de la novedad registrada para el usuario (ej: cambio de contrato, suspensión, reactivación, licencia).', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la novedad', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se registra o crea la novedad, evento o cambio de estado del usuario en el sistema.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la creación de la novedad', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario asignado (FK a UsersAssignment.Id) a quien se registra la novedad de cambio de estado o evento administrativo.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario asignado a quien se le está creando una novedad', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la novedad del usuario, clave primaria de la tabla UserNovelties.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de novedades o eventos asociados a usuarios del sistema, como activaciones, desactivaciones u otras incidencias administrativas. Permite llevar el historial de cambios de estado y observaciones relevantes de cada usuario.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties';
