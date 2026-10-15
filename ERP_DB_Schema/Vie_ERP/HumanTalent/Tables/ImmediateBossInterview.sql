CREATE TABLE [HumanTalent].[ImmediateBossInterview] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT             NOT NULL,
    [ProfileCompliance]           TINYINT         NULL,
    [ProcessContinuation]         TINYINT         NULL,
    [SelectionHead]               TINYINT         NULL,
    [Observation]                 VARCHAR (300)   NULL,
    [Total]                       DECIMAL (18, 2) NULL,
    [CreationUser]                VARCHAR (50)    NOT NULL,
    [CreationDate]                DATETIME        NOT NULL,
    [ModificationUser]            VARCHAR (50)    NULL,
    [ModificationDate]            DATETIME        NULL,
    CONSTRAINT [PK_ImmediateBossInterview] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ImmediateBossInterview_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id])
);


GO
ALTER TABLE [HumanTalent].[ImmediateBossInterview] NOCHECK CONSTRAINT [FK_ImmediateBossInterview_CandidatesSelectionProcess];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creaciónd de Usuarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Total';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que determina el orden de la selección teniendo como opciones: 1: 1, 2:2 y3:Descartado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'SelectionHead';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que determina si el candidato continua o no en el proceso => 0: No y 1:Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ProcessContinuation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que determina si el candidato cumple con el perfil => 0: No y 1:Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'ProfileCompliance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de procesos de selección de candidatos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ImmediateBossInterview', @level2type = N'COLUMN', @level2name = N'Id';

