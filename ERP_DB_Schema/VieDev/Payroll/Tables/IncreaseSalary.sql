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

