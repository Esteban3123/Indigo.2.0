CREATE TABLE [Payroll].[NoveltyScheduleDetailHour] (
    [Id]                        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NoveltyScheduleDetailId]   INT      NOT NULL,
    [DateTimeInitial]           DATETIME NOT NULL,
    [DateTimeEnding]            DATETIME NOT NULL,
    [TotalNumberHours]          INT      NOT NULL,
    [NextDay]                   INT      NOT NULL,
    [AppliedLiquidationConcept] BIT      NOT NULL,
    [State]                     BIT      NOT NULL,
    CONSTRAINT [PK_NoveltyScheduleDetailHour] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoveltyScheduleDetailHour_NoveltyScheduleDetail] FOREIGN KEY ([NoveltyScheduleDetailId]) REFERENCES [Payroll].[NoveltyScheduleDetail] ([Id])
);


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetailHour] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetailHour_NoveltyScheduleDetail];

