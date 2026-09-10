CREATE TABLE [Learning].[CompetitionRoleDetailC] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CompetitionRoleId]  INT          NOT NULL,
    [CompetitionId]      INT          NOT NULL,
    [CompetitionLevelId] INT          NOT NULL,
    [CreationUser]       VARCHAR (20) NOT NULL,
    [CreationDate]       DATETIME     NOT NULL,
    [ModificationUser]   VARCHAR (20) NULL,
    [ModificationDate]   DATETIME     NULL,
    CONSTRAINT [PK_CompetitionRoleDetailC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__Competiti__Compe__43DAD741] FOREIGN KEY ([CompetitionId]) REFERENCES [Learning].[Competition] ([Id]),
    CONSTRAINT [FK__Competiti__Compe__44CEFB7A] FOREIGN KEY ([CompetitionRoleId]) REFERENCES [Learning].[CompetitionRole] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del detalle de rol de competencia (DATETIME, NULL si no se ha modificado)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la modificación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación que realizó la última modificación del registro (VARCHAR 20, NULL si no se ha modificado)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizo la modificación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del detalle de rol de competencia (DATETIME, auditoria)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación que creó el registro del detalle de rol de competencia (VARCHAR 20, auditoria)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel o grado de competencia asociado (INT, referencia a tabla Learning.CompetitionLevel)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Nivel de competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la competencia (INT, FK a Learning.Competition, define qué competencia se evalúa)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rol de competencia (INT, FK a Learning.CompetitionRole, vincula rol profesional con competencia)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Rol de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle que relaciona rol de competencia con nivel y competencia específica (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Detalle Rol de Competencia_Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de roles dentro de competencias de aprendizaje: asocia un rol específico a una competencia y su nivel correspondiente, permitiendo definir qué nivel de competencia se espera según el rol del participante.', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC';
