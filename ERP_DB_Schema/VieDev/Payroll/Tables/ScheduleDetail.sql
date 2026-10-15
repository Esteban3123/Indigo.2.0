CREATE TABLE [Payroll].[ScheduleDetail] (
    [Id]                       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]                  INT            NOT NULL,
    [EmployeeId]               INT            NOT NULL,
    [ContractId]               INT            NOT NULL,
    [CompanyId]                INT            NOT NULL,
    [BranchOfficeId]           INT            NOT NULL,
    [FunctionalUnitId]         INT            NOT NULL,
    [CenterCostId]             INT            NOT NULL,
    [ScheduleFunctionalUnitId] INT            NOT NULL,
    [PayrollLiquidationNumber] VARCHAR (50)   NULL,
    [Letter]                   VARCHAR (2)    NOT NULL,
    [DateDetail]               DATE           NOT NULL,
    [TotalNumberHours]         NUMERIC (5, 2) NOT NULL,
    [ScheduleTemplateId]       INT            NULL,
    [Status]                   INT            NOT NULL,
    [State]                    BIT            NOT NULL,
    CONSTRAINT [PK_HourCalendar] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HourCalendar_CenterCost] FOREIGN KEY ([CenterCostId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_HourCalendar_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_HourCalendar_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_FunctionalUnit] FOREIGN KEY ([ScheduleFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_ScheduleTemplate] FOREIGN KEY ([ScheduleTemplateId]) REFERENCES [Payroll].[ScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_HourCalendar_CenterCost];


GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_HourCalendar_Employee];


GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_HourCalendar_FunctionalUnit];


GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_Contract];


GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_FunctionalUnit];


GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_Group];


GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_ScheduleTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ContractId';

