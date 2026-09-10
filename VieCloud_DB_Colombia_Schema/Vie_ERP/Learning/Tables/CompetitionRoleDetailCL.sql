CREATE TABLE [Learning].[CompetitionRoleDetailCL] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CompetitionRoleId]  INT          NOT NULL,
    [CompetitionLevelId] INT          NOT NULL,
    [CreationUser]       VARCHAR (20) NOT NULL,
    [CreationDate]       DATETIME     NOT NULL,
    CONSTRAINT [PK_CompetitionRoleDetailCL] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__Competiti__Compe__45C31FB3] FOREIGN KEY ([CompetitionLevelId]) REFERENCES [Learning].[CompetitionLevels] ([Id]),
    CONSTRAINT [FK__Competiti__Compe__46B743EC] FOREIGN KEY ([CompetitionRoleId]) REFERENCES [Learning].[CompetitionRole] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de detalle de rol-nivel de competencia; tipo DATETIME, auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro; VARCHAR(20), identificación del operador para trazabilidad de cambios', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel de competencia asociado; clave foránea a CompetitionLevels, define el grado o escala de competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CompetitionLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Niveles de Competencias', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CompetitionLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CompetitionLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rol de competencia asociado; clave foránea a CompetitionRole, vincula el rol profesional o funcional', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Rol de Competencias', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de rol-nivel de competencia; clave primaria IDENTITY, relación muchos-a-muchos entre rol y nivel', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Detalle Rol de Competencia_Nivel de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de niveles de competencia asignados a cada rol en el módulo de aprendizaje. Relaciona un rol con su nivel de competencia específico dentro de un esquema de formación o evaluación.', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL';
