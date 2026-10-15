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
ALTER TABLE [Learning].[CompetitionRoleDetailC] NOCHECK CONSTRAINT [FK__Competiti__Compe__43DAD741];


GO
ALTER TABLE [Learning].[CompetitionRoleDetailC] NOCHECK CONSTRAINT [FK__Competiti__Compe__44CEFB7A];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la modificación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizo la modificación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Nivel de competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Rol de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Detalle Rol de Competencia_Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'CompetitionRoleDetailC', @level2type = N'COLUMN', @level2name = N'Id';

