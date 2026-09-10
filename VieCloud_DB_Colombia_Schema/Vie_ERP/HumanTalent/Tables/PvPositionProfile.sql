CREATE TABLE [HumanTalent].[PvPositionProfile] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId] INT NOT NULL,
    [PositionsId]       INT NOT NULL,
    CONSTRAINT [PK_PvPositionProfile_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PvPositionProfile_Positions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la posición o cargo laboral. Clave foránea que referencia la tabla Positions, vinculando el perfil a su puesto de trabajo específico (médico, enfermero, administrativo, etc.).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las posiciones ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del encabezado o maestro del perfil del cargo. Agrupa las competencias, responsabilidades y requisitos asociados a una posición laboral.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de perfil del cargo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla. Identificador único de la relación entre perfil de cargo y posición laboral en el sistema de gestión de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona perfiles de cargo con los cargos o posiciones disponibles en la organización. Permite definir qué perfiles (conjuntos de competencias y requisitos) están asociados a cada puesto de trabajo en el área de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile';
