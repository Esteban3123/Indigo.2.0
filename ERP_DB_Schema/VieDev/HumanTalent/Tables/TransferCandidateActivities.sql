CREATE TABLE [HumanTalent].[TransferCandidateActivities] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT          NOT NULL,
    [EmployeeCode]                VARCHAR (20) NOT NULL,
    [FormTemplateId]              INT          NULL,
    [CreationUser]                VARCHAR (20) NOT NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [ModificationUser]            VARCHAR (20) NULL,
    [ModificationDate]            DATETIME     NULL,
    [BossInterview]               TINYINT      NULL,
    [State]                       TINYINT      NULL,
    CONSTRAINT [PK_TransferCandidateActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferCandidateActivities_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id]),
    CONSTRAINT [FK_TransferCandidateActivities_FormsTemplate] FOREIGN KEY ([FormTemplateId]) REFERENCES [HumanTalent].[FormsTemplate] ([Id])
);


GO
ALTER TABLE [HumanTalent].[TransferCandidateActivities] NOCHECK CONSTRAINT [FK_TransferCandidateActivities_CandidatesSelectionProcess];


GO
ALTER TABLE [HumanTalent].[TransferCandidateActivities] NOCHECK CONSTRAINT [FK_TransferCandidateActivities_FormsTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado 1.Registrado 2.Confimar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tuvo entrevista con el Jefe 1.Si, 2.No', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'BossInterview';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla o formulario que debe realizar al candato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'FormTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del empleado al que se le trasfiere dicha actividad para que la realice al candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'EmployeeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de candidato vinculadoa un proceso de selección en especifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'TransferCandidateActivities', @level2type = N'COLUMN', @level2name = N'Id';

