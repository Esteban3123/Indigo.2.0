CREATE TABLE [Payroll].[BlockSchedule] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FunctionalUnitId] INT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    CONSTRAINT [PK_BlockSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BlockSchedule_BlockSchedule] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [Payroll].[BlockSchedule] NOCHECK CONSTRAINT [FK_BlockSchedule_BlockSchedule];

