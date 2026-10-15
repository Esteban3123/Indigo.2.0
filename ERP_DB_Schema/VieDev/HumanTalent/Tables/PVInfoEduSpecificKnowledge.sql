CREATE TABLE [HumanTalent].[PVInfoEduSpecificKnowledge] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]       INT           NULL,
    [SpecificKnowledge] VARCHAR (800) NULL,
    [CreationUser]      VARCHAR (20)  NOT NULL,
    [CreationDate]      DATETIME      NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [EmployeeId]        INT           NULL,
    CONSTRAINT [PK__PVInfoEd__3214EC07A74881E6] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PVInfoEduCandidateIdIE] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVInfoEduSpecificKnowledge] NOCHECK CONSTRAINT [fk_PVInfoEduCandidateIdIE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación  del  empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVInfoEduSpecificKnowledge', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVInfoEduSpecificKnowledge', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVInfoEduSpecificKnowledge', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVInfoEduSpecificKnowledge', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVInfoEduSpecificKnowledge', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conocimiento específico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVInfoEduSpecificKnowledge', @level2type = N'COLUMN', @level2name = N'SpecificKnowledge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVInfoEduSpecificKnowledge', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVInfoEduSpecificKnowledge', @level2type = N'COLUMN', @level2name = N'Id';

