CREATE TABLE [HumanTalent].[AutomaticPreselectionResult] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateSelectionProcessId] INT          NOT NULL,
    [AcademicInformation]         TINYINT      NOT NULL,
    [Position]                    TINYINT      NOT NULL,
    [Sector]                      TINYINT      NOT NULL,
    [ExperienceMonths]            TINYINT      NOT NULL,
    [CreationUser]                VARCHAR (20) NOT NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [ModificationUser]            VARCHAR (20) NULL,
    [ModificationDate]            DATETIME     NULL,
    [Result]                      INT          NULL,
    [Area]                        TINYINT      NULL,
    CONSTRAINT [PK_AutomaticPreselectionResult] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AutomaticPreselectionResult_CandidatesSelectionProcess] FOREIGN KEY ([CandidateSelectionProcessId]) REFERENCES [HumanTalent].[CandidatesSelectionProcess] ([Id])
);


GO
ALTER TABLE [HumanTalent].[AutomaticPreselectionResult] NOCHECK CONSTRAINT [FK_AutomaticPreselectionResult_CandidatesSelectionProcess];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Candidato Cumple con el cargo 1.Si, 2.No', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la compración la experiencia del  candidato y el perfil  del cargo: 0 - No cumple 0%, 1- Cumple 25% ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'ExperienceMonths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la compración entre el cargo del  candidato y el perfil del cargo : 0 - No cumple 0%, 1- Cumple 25% ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Sector';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la compración entre el cargo del  candidato y el perfil del cargo : 0 - No cumple 0%, 1- Cumple 25% ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Position';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la compración entre la formación academica del  candidato y el perfil del cargo: 0 - No cumple 0%, 1- Cumple 25% ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'AcademicInformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de candidato asociado a un proceso de selección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AutomaticPreselectionResult', @level2type = N'COLUMN', @level2name = N'Id';

