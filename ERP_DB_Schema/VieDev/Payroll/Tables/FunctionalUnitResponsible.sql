CREATE TABLE [Payroll].[FunctionalUnitResponsible] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FunctionalUnitId] INT NOT NULL,
    [UserId]           INT NOT NULL,
    CONSTRAINT [PK_FunctionalUnitResponsible] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FunctionalUnitResponsible_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [Payroll].[FunctionalUnitResponsible] NOCHECK CONSTRAINT [FK_FunctionalUnitResponsible_FunctionalUnit];

