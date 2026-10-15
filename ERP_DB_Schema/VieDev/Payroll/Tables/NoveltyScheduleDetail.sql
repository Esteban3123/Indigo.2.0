CREATE TABLE [Payroll].[NoveltyScheduleDetail] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]                  INT          NOT NULL,
    [EmployeeId]               INT          NOT NULL,
    [CompanyId]                INT          NOT NULL,
    [BranchOfficeId]           INT          NOT NULL,
    [FunctionalUnitId]         INT          NOT NULL,
    [CostCenterId]             INT          NOT NULL,
    [ScheduleFunctionalUnitId] INT          NOT NULL,
    [PayrollLiquidationNumber] VARCHAR (50) NULL,
    [Letter]                   VARCHAR (2)  NOT NULL,
    [DateDetail]               DATE         NOT NULL,
    [Period]                   CHAR (7)     NOT NULL,
    [TotalNumberHours]         INT          NOT NULL,
    [ScheduleTemplateId]       INT          NULL,
    [Status]                   INT          NOT NULL,
    [State]                    BIT          NOT NULL,
    [PayrollDate]              DATE         NULL,
    CONSTRAINT [PK_NoveltyScheduleDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoveltyScheduleDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_NoveltyScheduleDetail_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_NoveltyScheduleDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_NoveltyScheduleDetail_FunctionalUnit1] FOREIGN KEY ([ScheduleFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetail] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetail_CostCenter];


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetail] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetail_Employee];


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetail] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetail_FunctionalUnit];


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetail] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetail_FunctionalUnit1];

