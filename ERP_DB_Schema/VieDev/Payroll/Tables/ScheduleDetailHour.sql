CREATE TABLE [Payroll].[ScheduleDetailHour] (
    [Id]                        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleDetailId]          INT            NOT NULL,
    [DateTimeInitial]           DATETIME       NOT NULL,
    [DateTimeEnding]            DATETIME       NOT NULL,
    [TotalNumberHours]          NUMERIC (5, 2) NOT NULL,
    [NextDay]                   BIT            NOT NULL,
    [Event]                     BIT            NOT NULL,
    [Approved]                  BIT            NOT NULL,
    [AppliedLiquidationConcept] BIT            NOT NULL,
    [State]                     BIT            NOT NULL,
    [EventLastMonth]            BIT            CONSTRAINT [DF_ScheduleDetailHour_EventLastMonth] DEFAULT ((0)) NOT NULL,
    [PaidEventLastMonth]        DATE           NULL,
    CONSTRAINT [PK_ScheduleDetailHour__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleDetailHour_ScheduleDetail] FOREIGN KEY ([ScheduleDetailId]) REFERENCES [Payroll].[ScheduleDetail] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleDetailHour] NOCHECK CONSTRAINT [FK_ScheduleDetailHour_ScheduleDetail];

