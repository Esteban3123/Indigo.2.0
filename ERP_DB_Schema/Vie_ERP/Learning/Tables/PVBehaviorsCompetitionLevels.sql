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
ALTER TABLE [Learning].[PVBehaviorsCompetitionLevels] NOCHECK CONSTRAINT [FK_PVBehaviorsCompetitionLevels_CompetitionLevels];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que realiza el registro', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la conducta', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la conducta, debe ser unico independientemente del nivel', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'CompetitionLevelsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'PVBehaviorsCompetitionLevels', @level2type = N'COLUMN', @level2name = N'Id';

