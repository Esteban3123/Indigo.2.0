CREATE TABLE [HumanTalent].[PolicyManagement] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]       INT           NOT NULL,
    [PolicyId]         INT           NOT NULL,
    [InitialDate]      DATETIME      NOT NULL,
    [FinalDate]        DATETIME      NOT NULL,
    [InsurancePolicy]  VARCHAR (100) NOT NULL,
    [InsuredValue]     VARCHAR (50)  NOT NULL,
    [Observation]      VARCHAR (300) NULL,
    [CreationUser]     VARCHAR (50)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (50)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_PolicyManagement] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PolicyManagement_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_PolicyManagement_Policy] FOREIGN KEY ([PolicyId]) REFERENCES [HumanTalent].[Policy] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PolicyManagement] NOCHECK CONSTRAINT [FK_PolicyManagement_Employee];


GO
ALTER TABLE [HumanTalent].[PolicyManagement] NOCHECK CONSTRAINT [FK_PolicyManagement_Policy];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor asegurado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'InsuredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Póliza De Seguro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'InsurancePolicy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la política', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'PolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PolicyManagement', @level2type = N'COLUMN', @level2name = N'Id';

