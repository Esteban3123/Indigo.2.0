CREATE TABLE [HumanTalent].[PVPositionsDocuments] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionsId]          INT          NOT NULL,
    [CheckListDocumentsId] INT          NOT NULL,
    [CreationUser]         VARCHAR (50) NOT NULL,
    [CreationDate]         DATETIME     NOT NULL,
    [ModificationUser]     VARCHAR (50) NULL,
    [ModificationDate]     DATETIME     NULL,
    CONSTRAINT [PK_PVPositionsDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVPositionsDocuments_CheckListDocuments] FOREIGN KEY ([CheckListDocumentsId]) REFERENCES [HumanTalent].[CheckListDocuments] ([Id]),
    CONSTRAINT [FK_PVPositionsDocuments_Positions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVPositionsDocuments] NOCHECK CONSTRAINT [FK_PVPositionsDocuments_CheckListDocuments];


GO
ALTER TABLE [HumanTalent].[PVPositionsDocuments] NOCHECK CONSTRAINT [FK_PVPositionsDocuments_Positions];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de los documentos de la lista de verificación ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CheckListDocumentsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la posición ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'Id';

