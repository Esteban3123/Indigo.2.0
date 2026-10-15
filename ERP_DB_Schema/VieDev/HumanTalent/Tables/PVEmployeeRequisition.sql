CREATE TABLE [HumanTalent].[PVEmployeeRequisition] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonalRequisitionId] INT          NOT NULL,
    [EmployeeSubstituteId]  INT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    CONSTRAINT [PK__PVEmploy__3214EC079B072470] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_Employee_ER] FOREIGN KEY ([EmployeeSubstituteId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVEmployeeRequisition] NOCHECK CONSTRAINT [fk_Employee_ER];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEmployeeRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEmployeeRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEmployeeRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEmployeeRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado a sustituir', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEmployeeRequisition', @level2type = N'COLUMN', @level2name = N'EmployeeSubstituteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de la requisición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEmployeeRequisition', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEmployeeRequisition', @level2type = N'COLUMN', @level2name = N'Id';

