CREATE TABLE [HumanTalent].[PVPublicSelectionProcess] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SelectionProcessId] INT            NOT NULL,
    [Type]               INT            NOT NULL,
    [Description]        VARCHAR (1600) NOT NULL,
    [StartDate]          DATETIME       NOT NULL,
    [FinalDate]          DATETIME       NOT NULL,
    [CreationUser]       VARCHAR (20)   NOT NULL,
    [CreationDate]       DATETIME       NOT NULL,
    [ModificationUser]   VARCHAR (50)   NULL,
    [ModificationDate]   DATETIME       NULL,
    CONSTRAINT [PK_PVPublicSelectionProcess] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVPublicSelectionProcess_SelectionProcess] FOREIGN KEY ([SelectionProcessId]) REFERENCES [HumanTalent].[SelectionProcess] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVPublicSelectionProcess] NOCHECK CONSTRAINT [FK_PVPublicSelectionProcess_SelectionProcess];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio de la publicación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de redacción de la publicación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Typo de convocatorio: 1- Interna, 2-Externa, 3-Mixta', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'SelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPublicSelectionProcess', @level2type = N'COLUMN', @level2name = N'Id';

