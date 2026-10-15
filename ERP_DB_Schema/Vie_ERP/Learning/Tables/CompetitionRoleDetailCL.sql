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
ALTER TABLE [Learning].[CompetitionRoleDetailCL] NOCHECK CONSTRAINT [FK__Competiti__Compe__45C31FB3];


GO
ALTER TABLE [Learning].[CompetitionRoleDetailCL] NOCHECK CONSTRAINT [FK__Competiti__Compe__46B743EC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Niveles de Competencias', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CompetitionLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Rol de Competencias', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Detalle Rol de Competencia_Nivel de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailCL', @level2type = N'COLUMN', @level2name = N'Id';

