CREATE TABLE [HumanTalent].[CheckList] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT          NOT NULL,
    [CheckListDocumentsId]        INT          NOT NULL,
    [Verification]                TINYINT      NULL,
    [Observation]                 VARCHAR (50) NULL,
    [CreationUser]                VARCHAR (50) NOT NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [ModificationUser]            VARCHAR (50) NULL,
    [ModificationDate]            DATETIME     NULL,
    CONSTRAINT [PK_CheckList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckList_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id]),
    CONSTRAINT [FK_CheckList_CheckListDocuments] FOREIGN KEY ([CheckListDocumentsId]) REFERENCES [HumanTalent].[CheckListDocuments] ([Id])
);


GO
ALTER TABLE [HumanTalent].[CheckList] NOCHECK CONSTRAINT [FK_CheckList_CandidatesSelectionProcess];


GO
ALTER TABLE [HumanTalent].[CheckList] NOCHECK CONSTRAINT [FK_CheckList_CheckListDocuments];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciónes', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para seleccionar la verificacion : 1=Si, 2=No, 3= N/A', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Verification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de documentos de lista de verificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CheckListDocumentsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de procesos de selección de candidatos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'CheckList', @level2type = N'COLUMN', @level2name = N'Id';

