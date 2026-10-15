CREATE TABLE [Payroll].[ResumptionHolidayDetail] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ResumptionHolidayId] INT NOT NULL,
    [VacationId]          INT NOT NULL,
    [Days]                INT NOT NULL,
    CONSTRAINT [PK_ResumptionHolidayDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResumptionHolidayDetail_ResumptionHoliday] FOREIGN KEY ([ResumptionHolidayId]) REFERENCES [Payroll].[ResumptionHoliday] ([Id]),
    CONSTRAINT [FK_ResumptionHolidayDetail_Vacation] FOREIGN KEY ([VacationId]) REFERENCES [Payroll].[Vacation] ([Id])
);


GO
ALTER TABLE [Payroll].[ResumptionHolidayDetail] NOCHECK CONSTRAINT [FK_ResumptionHolidayDetail_ResumptionHoliday];


GO
ALTER TABLE [Payroll].[ResumptionHolidayDetail] NOCHECK CONSTRAINT [FK_ResumptionHolidayDetail_Vacation];

