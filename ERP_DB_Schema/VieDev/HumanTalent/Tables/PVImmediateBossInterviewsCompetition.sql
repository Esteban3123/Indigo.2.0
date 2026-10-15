CREATE TABLE [HumanTalent].[PVImmediateBossInterviewsCompetition] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ImmediateBossInterviewId] INT             NOT NULL,
    [CompetitionId]            INT             NULL,
    [TechnicalKnowledge]       INT             NULL,
    [Score]                    DECIMAL (18, 2) NULL,
    [CreationUser]             VARCHAR (50)    NOT NULL,
    [CreationDate]             DATETIME        NOT NULL,
    [ModificationUser]         VARCHAR (50)    NULL,
    [ModificationDate]         DATETIME        NULL,
    CONSTRAINT [PK_PVImmediateBossInterviewsCompetition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVImmediateBossInterviewsCompetition_Competition] FOREIGN KEY ([CompetitionId]) REFERENCES [Learning].[Competition] ([Id]),
    CONSTRAINT [FK_PVImmediateBossInterviewsCompetition_ImmediateBossInterview] FOREIGN KEY ([ImmediateBossInterviewId]) REFERENCES [HumanTalent].[ImmediateBossInterview] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVImmediateBossInterviewsCompetition] NOCHECK CONSTRAINT [FK_PVImmediateBossInterviewsCompetition_Competition];


GO
ALTER TABLE [HumanTalent].[PVImmediateBossInterviewsCompetition] NOCHECK CONSTRAINT [FK_PVImmediateBossInterviewsCompetition_ImmediateBossInterview];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conocimientos técnicos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'TechnicalKnowledge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la competencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'CompetitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entrevista con el jefe inmediato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'ImmediateBossInterviewId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVImmediateBossInterviewsCompetition', @level2type = N'COLUMN', @level2name = N'Id';

