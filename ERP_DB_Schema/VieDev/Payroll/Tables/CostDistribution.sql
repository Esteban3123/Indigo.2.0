CREATE TABLE [Payroll].[CostDistribution] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]              INT           NOT NULL,
    [PayrollEndDate]       DATE          NOT NULL,
    [LiquidationDetailId]  INT           NOT NULL,
    [JournalVoucherTypeId] INT           NOT NULL,
    [MainAccountId]        INT           NULL,
    [ThirdPartyId]         INT           NULL,
    [CostCenterId]         INT           NULL,
    [DebitValue]           DECIMAL (18)  CONSTRAINT [DF_CostDistribution_DebitValue] DEFAULT ((0)) NULL,
    [CreditValue]          DECIMAL (18)  CONSTRAINT [DF_CostDistribution_CreditValue] DEFAULT ((0)) NULL,
    [EmployeeId]           INT           NULL,
    [NumberHours]          INT           CONSTRAINT [DF_CostDistribution_NumberHours] DEFAULT ((0)) NULL,
    [Detail]               VARCHAR (500) NULL,
    CONSTRAINT [PK_CostDistribution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistribution_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistribution_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_CostDistribution_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_CostDistribution_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CostDistribution_LiquidationDetail] FOREIGN KEY ([LiquidationDetailId]) REFERENCES [Payroll].[LiquidationDetail] ([Id]),
    CONSTRAINT [FK_CostDistribution_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CostDistribution_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_CostCenter];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_Employee];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_Group];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_JournalVoucherTypes];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_LiquidationDetail];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_MainAccounts];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_ThirdParty];

