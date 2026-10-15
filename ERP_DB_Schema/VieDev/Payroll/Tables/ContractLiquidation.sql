CREATE TABLE [Payroll].[ContractLiquidation] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]         INT             NOT NULL,
    [ContractId]         INT             NOT NULL,
    [RetirementDate]     DATE            NOT NULL,
    [RetirementReasonId] TINYINT         NOT NULL,
    [Status]             NCHAR (1)       NOT NULL,
    [TotalAccrued]       DECIMAL (18, 2) NOT NULL,
    [TotalDeducted]      DECIMAL (18, 2) NOT NULL,
    [TotalPaid]          DECIMAL (18, 2) NOT NULL,
    [ResolutionNumber]   VARCHAR (50)    NULL,
    [ResolutionDate]     DATE            NULL,
    CONSTRAINT [PK_ContractLiquidation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractLiquidation_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ContractLiquidation_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_ContractLiquidation_RetirementReason] FOREIGN KEY ([RetirementReasonId]) REFERENCES [Payroll].[RetirementReason] ([Id])
);


GO
ALTER TABLE [Payroll].[ContractLiquidation] NOCHECK CONSTRAINT [FK_ContractLiquidation_Contract];


GO
ALTER TABLE [Payroll].[ContractLiquidation] NOCHECK CONSTRAINT [FK_ContractLiquidation_Employee];


GO
ALTER TABLE [Payroll].[ContractLiquidation] NOCHECK CONSTRAINT [FK_ContractLiquidation_RetirementReason];

