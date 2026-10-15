CREATE TABLE [HumanTalent].[PVAdditionalInformation_EndowmenstSize] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]      INT          NULL,
    [EmployeeId]       INT          NULL,
    [EndowmentsSizeId] INT          NULL,
    [CreationUser]     VARCHAR (50) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (50) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_PVAdditionalInformation_EndowmenstSize] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVAdditionalInformation_EndowmenstSize_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id]),
    CONSTRAINT [FK_PVAdditionalInformation_EndowmenstSize_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_PVAdditionalInformation_EndowmenstSize_EndowmentsSize] FOREIGN KEY ([EndowmentsSizeId]) REFERENCES [HumanTalent].[EndowmentsSize] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVAdditionalInformation_EndowmenstSize] NOCHECK CONSTRAINT [FK_PVAdditionalInformation_EndowmenstSize_Candidate];


GO
ALTER TABLE [HumanTalent].[PVAdditionalInformation_EndowmenstSize] NOCHECK CONSTRAINT [FK_PVAdditionalInformation_EndowmenstSize_Employee];


GO
ALTER TABLE [HumanTalent].[PVAdditionalInformation_EndowmenstSize] NOCHECK CONSTRAINT [FK_PVAdditionalInformation_EndowmenstSize_EndowmentsSize];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVAdditionalInformation_EndowmenstSize', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVAdditionalInformation_EndowmenstSize', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVAdditionalInformation_EndowmenstSize', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVAdditionalInformation_EndowmenstSize', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de tamaño de las dotaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVAdditionalInformation_EndowmenstSize', @level2type = N'COLUMN', @level2name = N'EndowmentsSizeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVAdditionalInformation_EndowmenstSize', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVAdditionalInformation_EndowmenstSize', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVAdditionalInformation_EndowmenstSize', @level2type = N'COLUMN', @level2name = N'Id';

