CREATE TABLE [Payroll].[BankFileDetail] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankFileId]                INT           NOT NULL,
    [LiquidationId]             INT           NOT NULL,
    [GroupId]                   INT           NOT NULL,
    [PositionId]                INT           NOT NULL,
    [FunctionalUnitId]          INT           NOT NULL,
    [ContractId]                INT           NOT NULL,
    [EmployeeId]                INT           NOT NULL,
    [EmployeeBankId]            INT           NOT NULL,
    [EmployeeBankTypeAccount]   INT           NOT NULL,
    [EmployeeBankAccountNumber] VARCHAR (100) NOT NULL,
    [BasicSalary]               NUMERIC (18)  NOT NULL,
    [TotalAccrued]              NUMERIC (18)  NOT NULL,
    [TotalDeducted]             NUMERIC (18)  NOT NULL,
    [TotalPaid]                 NUMERIC (18)  NOT NULL,
    CONSTRAINT [PK_BankFileDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankFileDetail_Bank] FOREIGN KEY ([EmployeeBankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_BankFileDetail_BankFile] FOREIGN KEY ([BankFileId]) REFERENCES [Payroll].[BankFile] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_BankFileDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Liquidation] FOREIGN KEY ([LiquidationId]) REFERENCES [Payroll].[Liquidation] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Bank];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_BankFile];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Contract];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Employee];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_FunctionalUnit];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Group];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Liquidation];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Position];

