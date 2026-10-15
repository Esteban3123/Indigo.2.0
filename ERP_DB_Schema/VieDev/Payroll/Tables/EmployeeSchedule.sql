CREATE TABLE [Payroll].[EmployeeSchedule] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]      INT          NOT NULL,
    [ContractId]      INT          NOT NULL,
    [InitialDate]     DATE         NOT NULL,
    [InitialHourDate] TIME (7)     NOT NULL,
    [EndDate]         DATE         NOT NULL,
    [EndHourDate]     TIME (7)     NOT NULL,
    [Hours]           TINYINT      NOT NULL,
    [CreationUser]    VARCHAR (20) NOT NULL,
    [CreationDate]    DATETIME     NOT NULL,
    CONSTRAINT [PK_EmployeeSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmployeeSchedule_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_EmployeeSchedule_Employee] FOREIGN KEY ([Id]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[EmployeeSchedule] NOCHECK CONSTRAINT [FK_EmployeeSchedule_Contract];


GO
ALTER TABLE [Payroll].[EmployeeSchedule] NOCHECK CONSTRAINT [FK_EmployeeSchedule_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'Hours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha hora fin', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'EndHourDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha fin', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha hora inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'InitialHourDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeSchedule', @level2type = N'COLUMN', @level2name = N'Id';

