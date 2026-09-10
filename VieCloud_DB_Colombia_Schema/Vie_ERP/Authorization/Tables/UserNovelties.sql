-- =============================================================================
-- Novedades (incapacidad, permiso, vacaciones, etc.) registradas para un usuario
-- asignado en Authorization.UsersAssignment.
-- =============================================================================

CREATE TABLE [Authorization].[UserNovelties] (
    [Id]              INT IDENTITY(1,1) NOT NULL,
    [AssignedUserId]  INT NOT NULL,
    [NoveltyDate]     DATETIME NOT NULL CONSTRAINT [DF_UserNovelties_NoveltyDate] DEFAULT (GETUTCDATE()),
    [Description]     NVARCHAR(300) NOT NULL,
    [IsUserActive]    BIT NOT NULL CONSTRAINT [DF_UserNovelties_IsUserActive] DEFAULT ((0)),
    [TypeNovely]      TINYINT NULL,
    [EndDate]         DATETIME NULL,
    CONSTRAINT [PK_UserNovelties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserNovelties_UsersAssignment]
        FOREIGN KEY ([AssignedUserId])
        REFERENCES [Authorization].[UsersAssignment] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_UserNovelties_AssignedUserId]
    ON [Authorization].[UserNovelties] ([AssignedUserId] ASC)
    INCLUDE ([NoveltyDate])
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de novedad; clave primaria IDENTITY; INT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario asignado (Authorization.UsersAssignment) al que pertenece la novedad; FK a UsersAssignment.Id; INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario asignado al que pertenece la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que se registra la novedad; DATETIME NOT NULL DEFAULT GETUTCDATE().', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la novedad reportada para el usuario asignado (ej. incapacidad, permiso, vacaciones); NVARCHAR(300) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del usuario derivado de la novedad (activo/inactivo para asignación) al momento de registrarla; BIT NOT NULL DEFAULT 0.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del usuario (activo/inactivo) asociado a la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novedades (incapacidad, permiso, vacaciones u otra causa) registradas para un usuario asignado en Authorization.UsersAssignment; cada novedad puede reflejar un cambio en el estado activo/inactivo del usuario para la asignación automática.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de novedad. Valores permitidos: 1 = Incapacidad, 2 = Permiso, 3 = Vacaciones, 4 = Otro; TINYINT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'TypeNovely';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de novedad: 1=Incapacidad, 2=Permiso, 3=Vacaciones, 4=Otro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'TypeNovely';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'TypeNovely';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de la novedad (ej. fin de incapacidad, permiso o vacaciones); DATETIME NULL. Se registra en la novedad y no en Authorization.AccountManagementParameters.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'EndDate';
GO