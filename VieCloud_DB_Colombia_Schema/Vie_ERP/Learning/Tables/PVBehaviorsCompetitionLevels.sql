CREATE TABLE [Learning].[PVBehaviorsCompetitionLevels] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CompetitionLevelsId] INT           NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Description]         VARCHAR (500) NULL,
    [CreationUser]        VARCHAR (209) NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    CONSTRAINT [PK_PVBehaviorsCompetitionLevels] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVBehaviorsCompetitionLevels_CompetitionLevels] FOREIGN KEY ([CompetitionLevelsId]) REFERENCES [Learning].[CompetitionLevels] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de conducta; tipo DATETIME, auditoría de cambios en comportamientos y competencias', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó la última modificación; auditoría de cambios en conductas', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro; tipo DATETIME, trazabilidad de conducta en sistema de aprendizaje', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 209) del usuario que creó el registro de conducta; auditoría de origen', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que realiza el registro', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la conducta o comportamiento esperado; tipo VARCHAR 500, complementa el código para búsqueda semántica de competencias', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la conducta', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la conducta (VARCHAR 20), identificador independiente del nivel de competencia; clave para referencias transversales', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la conducta, debe ser unico independientemente del nivel', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK que referencia la tabla CompetitionLevels; asocia la conducta al nivel de competencia o desempeño', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CompetitionLevelsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CompetitionLevelsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CompetitionLevelsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (INT IDENTITY 1,1); clave primaria de la tabla de conductas y competencias', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Niveles de competencia asociados a comportamientos en el módulo de aprendizaje. Permite clasificar y describir los distintos grados o niveles que puede alcanzar un comportamiento dentro de una competencia evaluada.', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels';
