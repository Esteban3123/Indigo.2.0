CREATE TABLE [Payroll].[ResumptionHoliday] (
    [Id]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]    INT          NOT NULL,
    [RequestedDays] INT          NOT NULL,
    [InitialDate]   DATE         NOT NULL,
    [EndDate]       DATE         NOT NULL,
    [EntryDate]     DATE         NOT NULL,
    [CreationUser]  VARCHAR (20) NOT NULL,
    [CreationDate]  DATETIME     NOT NULL,
    [TimeStamp]     ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ResumptionHoliday] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResumptionHoliday_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[ResumptionHoliday] NOCHECK CONSTRAINT [FK_ResumptionHoliday_Employee];

