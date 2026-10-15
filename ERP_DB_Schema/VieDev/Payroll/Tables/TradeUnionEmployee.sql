CREATE TABLE [Payroll].[TradeUnionEmployee] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TradeUnionId] INT          NOT NULL,
    [EmployeeId]   INT          NOT NULL,
    [CreationUser] VARCHAR (20) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    [TimeStamp]    ROWVERSION   NOT NULL,
    CONSTRAINT [PK_TradeUnionEmployee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TradeUnionEmployee_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_TradeUnionEmployee_TradeUnion] FOREIGN KEY ([TradeUnionId]) REFERENCES [Payroll].[TradeUnion] ([Id])
);


GO
ALTER TABLE [Payroll].[TradeUnionEmployee] NOCHECK CONSTRAINT [FK_TradeUnionEmployee_Employee];


GO
ALTER TABLE [Payroll].[TradeUnionEmployee] NOCHECK CONSTRAINT [FK_TradeUnionEmployee_TradeUnion];

