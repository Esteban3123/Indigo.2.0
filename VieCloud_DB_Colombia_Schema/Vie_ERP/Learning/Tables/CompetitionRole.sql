CREATE TABLE [Learning].[CompetitionRole] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_CompetitionRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__Competit__A25C5AA76325154D] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del rol de competencia (DATETIME, NULL si no ha sido modificado)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del rol de competencia (VARCHAR 20, NULL si no ha sido modificado)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del rol de competencia (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el rol de competencia (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del rol de competencia: 1 = Activo, 0 = Inactivo (BIT, controla visibilidad y disponibilidad)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del rol de competencia (VARCHAR 80, ej: Enfermero, Médico Especialista)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Rol de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del rol de competencia (VARCHAR 20, clave única, referencia en asignaciones)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Rol de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y secuencial del rol de competencia (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Rol de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Roles o perfiles de competencia utilizados en el módulo de aprendizaje. Permite clasificar a los participantes según su rol dentro de un proceso de competencia o evaluación (por ejemplo: evaluador, evaluado, juez).', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRole';
