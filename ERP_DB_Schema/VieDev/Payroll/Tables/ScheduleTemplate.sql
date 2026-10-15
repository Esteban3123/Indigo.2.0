CREATE TABLE [Payroll].[ScheduleTemplate] (
    [Id]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]    VARCHAR (3)  NOT NULL,
    [GroupId] INT          NOT NULL,
    [Letter]  VARCHAR (2)  NOT NULL,
    [Name]    VARCHAR (75) NULL,
    [State]   BIT          NOT NULL,
    CONSTRAINT [PK_ScheduleTemplate__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ShiftsTemplate_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleTemplate] NOCHECK CONSTRAINT [FK_ShiftsTemplate_Group];

