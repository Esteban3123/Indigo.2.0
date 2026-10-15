CREATE TABLE [HumanTalent].[VaccineManagement] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]       INT           NOT NULL,
    [VaccineId]        INT           NOT NULL,
    [VaccineSchema]    VARCHAR (50)  NOT NULL,
    [VaccineDate]      DATETIME      NOT NULL,
    [Result]           VARCHAR (150) NULL,
    [Observation]      VARCHAR (150) NULL,
    [CreationUser]     VARCHAR (50)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (50)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_VaccineManagement] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VaccineManagement_Vaccine] FOREIGN KEY ([VaccineId]) REFERENCES [HumanTalent].[Vaccine] ([Id])
);


GO
ALTER TABLE [HumanTalent].[VaccineManagement] NOCHECK CONSTRAINT [FK_VaccineManagement_Vaccine];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de resultados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la vacuna', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'VaccineDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esquema de la vacuna', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'VaccineSchema';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la vacuna', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'VaccineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'VaccineManagement', @level2type = N'COLUMN', @level2name = N'Id';

