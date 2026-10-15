CREATE TABLE [HumanTalent].[SelectionProcess] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonalRequisitionId] INT           NOT NULL,
    [State]                 INT           NOT NULL,
    [Published]             TINYINT       NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [ProcessClosingDate]    DATETIME      NULL,
    [DocumentPath]          VARCHAR (MAX) NULL,
    CONSTRAINT [PK__Selectio__3214EC07C371E002] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PersonalRequisition_SelectionProcess] FOREIGN KEY ([PersonalRequisitionId]) REFERENCES [HumanTalent].[PersonalRequisition] ([Id])
);


GO
ALTER TABLE [HumanTalent].[SelectionProcess] NOCHECK CONSTRAINT [fk_PersonalRequisition_SelectionProcess];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta del documento', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'DocumentPath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cierre del proceso', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'ProcessClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que Modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de publicación: 0-No publicado, 1- Publicado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'Published';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado: 1- activo, 2- suspendido, 3-cancelado, 4-cerrado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Requisición de personal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SelectionProcess', @level2type = N'COLUMN', @level2name = N'Id';

