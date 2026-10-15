CREATE TABLE [Payroll].[VacationPeriod] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]        INT          NOT NULL,
    [ContractId]        INT          NOT NULL,
    [InitialDatePeriod] DATE         NOT NULL,
    [EndDatePeriod]     DATE         NOT NULL,
    [VacationDays]      TINYINT      NOT NULL,
    [PendingDays]       TINYINT      NOT NULL,
    [TakenDays]         TINYINT      NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    [TimeStamp]         ROWVERSION   NOT NULL,
    CONSTRAINT [PK_VacationPeriod__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationPeriod_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_VacationPeriod_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[VacationPeriod] NOCHECK CONSTRAINT [FK_VacationPeriod_Contract];


GO
ALTER TABLE [Payroll].[VacationPeriod] NOCHECK CONSTRAINT [FK_VacationPeriod_Employee];

