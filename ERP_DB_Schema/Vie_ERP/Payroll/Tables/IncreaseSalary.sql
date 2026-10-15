CREATE TABLE [Payroll].[IncreaseSalary] (
    [Id]                    INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Status]                BIT            NOT NULL,
    [GroupId]               INT            NOT NULL,
    [ContractId]            INT            NOT NULL,
    [ContractInitialNumber] INT            NOT NULL,
    [EmployeeId]            INT            NOT NULL,
    [ModificationReasonId]  INT            NOT NULL,
    [PercentageIncrease]    DECIMAL (6, 4) NULL,
    [BasicSalary]           NUMERIC (18)   NOT NULL,
    [NewSalary]             NUMERIC (18)   NOT NULL,
    [PositionId]            INT            NOT NULL,
    [FunctionalUnitId]      INT            NOT NULL,
    [CreationUser]          VARCHAR (20)   NOT NULL,
    [CreationDate]          DATETIME       NOT NULL,
    [ConfirmationUser]      VARCHAR (20)   NULL,
    [ConfirmationDate]      DATETIME       NULL,
    CONSTRAINT [PK_IncreaseSalary] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IncreaseSalary_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_IncreaseSalary_ContractModificationReason] FOREIGN KEY ([ModificationReasonId]) REFERENCES [Payroll].[ContractModificationReason] ([Id]),
    CONSTRAINT [FK_IncreaseSalary_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_IncreaseSalary_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_IncreaseSalary_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_IncreaseSalary_IncreaseSalary] FOREIGN KEY ([Id]) REFERENCES [Payroll].[IncreaseSalary] ([Id]),
    CONSTRAINT [FK_IncreaseSalary_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Payroll].[IncreaseSalary] NOCHECK CONSTRAINT [FK_IncreaseSalary_Contract];


GO
ALTER TABLE [Payroll].[IncreaseSalary] NOCHECK CONSTRAINT [FK_IncreaseSalary_Employee];


GO
ALTER TABLE [Payroll].[IncreaseSalary] NOCHECK CONSTRAINT [FK_IncreaseSalary_FunctionalUnit];


GO
ALTER TABLE [Payroll].[IncreaseSalary] NOCHECK CONSTRAINT [FK_IncreaseSalary_Group];


GO
ALTER TABLE [Payroll].[IncreaseSalary] NOCHECK CONSTRAINT [FK_IncreaseSalary_IncreaseSalary];


GO
ALTER TABLE [Payroll].[IncreaseSalary] NOCHECK CONSTRAINT [FK_IncreaseSalary_Position];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha  de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cargo del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cargo del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevo salario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'NewSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario base', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de aumento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'PercentageIncrease';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de motivo de modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'ModificationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número inicial del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'ContractInitialNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Del Grupo ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncreaseSalary', @level2type = N'COLUMN', @level2name = N'Id';

