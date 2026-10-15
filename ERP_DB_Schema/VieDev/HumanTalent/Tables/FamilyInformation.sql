CREATE TABLE [HumanTalent].[FamilyInformation] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]          INT          NULL,
    [KinshipId]            INT          NOT NULL,
    [IdentificationNumber] VARCHAR (25) NOT NULL,
    [DocumentType]         INT          NOT NULL,
    [Name]                 VARCHAR (50) NOT NULL,
    [Gender]               INT          NOT NULL,
    [DateBirth]            DATE         NOT NULL,
    [Age]                  INT          NOT NULL,
    [Ocupation]            VARCHAR (40) NULL,
    [Institution]          VARCHAR (40) NULL,
    [CreationUser]         VARCHAR (20) NOT NULL,
    [CreationDate]         DATETIME     NOT NULL,
    [ModificationUser]     VARCHAR (20) NULL,
    [ModificationDate]     DATETIME     NULL,
    [EmployeeId]           INT          NULL,
    CONSTRAINT [PK__FamilyIn__3214EC070D5D337D] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_FICandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id]),
    CONSTRAINT [fk_FIKinshipId] FOREIGN KEY ([KinshipId]) REFERENCES [Payroll].[Kinship] ([Id])
);


GO
ALTER TABLE [HumanTalent].[FamilyInformation] NOCHECK CONSTRAINT [fk_FICandidateId];


GO
ALTER TABLE [HumanTalent].[FamilyInformation] NOCHECK CONSTRAINT [fk_FIKinshipId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Institución', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'Institution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ocupación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'Ocupation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'DateBirth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del familiar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento  0 = CC | 1 = CE  | 2 = TI |3 = RC |4 = PA | 5 = AS| 6 = MS| 7 = NIT| 8 = NU| 9 = CN| 10 = CD| 11 = SC| 12 = PE ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Parentesco', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'KinshipId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FamilyInformation', @level2type = N'COLUMN', @level2name = N'Id';

