CREATE TABLE [Payroll].[CostDistributions] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [JournalVoucherTypeId]      INT          NULL,
    [GroupId]                   INT          NOT NULL,
    [EmployeeId]                INT          NOT NULL,
    [ContractId]                INT          NOT NULL,
    [ConceptId]                 INT          NOT NULL,
    [CompanyId]                 INT          NULL,
    [CompanyNit]                VARCHAR (50) NULL,
    [EmployeeNit]               VARCHAR (50) NULL,
    [PayrollDateLiquidated]     DATE         NOT NULL,
    [AccountingStatementNumber] VARCHAR (50) NOT NULL,
    [AccruedAccount]            VARCHAR (20) NULL,
    [DeductedAccount]           VARCHAR (20) NULL,
    [NumberHours]               INT          NULL,
    [TotalConceptValue]         NUMERIC (18) NULL,
    [CostCenterId]              INT          NOT NULL,
    [SubCostCenterId]           INT          NULL,
    [DeductedValue]             NUMERIC (18) NULL,
    [AccruedValue]              NUMERIC (18) NULL,
    [InabilityCollect]          NUMERIC (18) NULL,
    [SpendingInability]         NUMERIC (18) NULL,
    [Status]                    BIT          NOT NULL,
    CONSTRAINT [PK_CostDistributions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributions_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_CostDistributions_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_CostDistributions_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistributions_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_CostDistributions_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_CostDistributions_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_Concept];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_Contract];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_CostCenter];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_Employee];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_Group];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_JournalVoucherTypes];

