CREATE TABLE [HumanTalent].[PVPoProInformalEducation] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId]     INT          NOT NULL,
    [EducationLevelId]      INT          NOT NULL,
    [AcademicInformationId] INT          NOT NULL,
    [Required]              INT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    CONSTRAINT [PK__PVPoProI__3214EC0767E0CBF2] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PVPoProInfEduAcademicInformation] FOREIGN KEY ([AcademicInformationId]) REFERENCES [HumanTalent].[AcademicInformation] ([id]),
    CONSTRAINT [fk_PVPoProInfEduEducationLevel] FOREIGN KEY ([EducationLevelId]) REFERENCES [HumanTalent].[EducationLevel] ([Id]),
    CONSTRAINT [fk_PVPoProInfEduPositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVPoProInformalEducation] NOCHECK CONSTRAINT [fk_PVPoProInfEduAcademicInformation];


GO
ALTER TABLE [HumanTalent].[PVPoProInformalEducation] NOCHECK CONSTRAINT [fk_PVPoProInfEduEducationLevel];


GO
ALTER TABLE [HumanTalent].[PVPoProInformalEducation] NOCHECK CONSTRAINT [fk_PVPoProInfEduPositionProfile];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se parametriza para ver si es obligatorio o no', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'Required';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la información académica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'AcademicInformationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del nivel de educación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'EducationLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del perfil de puesto', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProInformalEducation', @level2type = N'COLUMN', @level2name = N'Id';

