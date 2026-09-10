CREATE TABLE [dbo].[PvPositionProfile] (
    [Id]                INT NOT NULL,
    [PositionProfileId] INT NOT NULL,
    [PositionsId]       INT NOT NULL,
    CONSTRAINT [PK_PvPositionProfile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PvPositionProfile_PositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id]),
    CONSTRAINT [FK_PvPositionProfile_Positions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id])
);




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre perfiles de cargo y posiciones dentro del sistema de recursos humanos o seguridad. Cada registro asocia un perfil de puesto con una posición específica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PvPositionProfile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PvPositionProfile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asociación entre perfil y posición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del perfil de cargo o puesto al que pertenece la relación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la posición o cargo específico asociado al perfil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionsId';
