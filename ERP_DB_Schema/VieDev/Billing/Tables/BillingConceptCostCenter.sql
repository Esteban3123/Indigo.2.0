CREATE TABLE [Billing].[BillingConceptCostCenter] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingConceptId] INT NOT NULL,
    [BranchOfficeId]   INT NOT NULL,
    [FunctionalUnitId] INT NOT NULL,
    [CostCenterId]     INT NOT NULL,
    CONSTRAINT [PK_BillingConceptCostCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingConceptCostCenter_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_BillingConceptCostCenter_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_BillingConceptCostCenter_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_BillingConceptCostCenter_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [Billing].[BillingConceptCostCenter] NOCHECK CONSTRAINT [FK_BillingConceptCostCenter_BillingConcept];


GO
ALTER TABLE [Billing].[BillingConceptCostCenter] NOCHECK CONSTRAINT [FK_BillingConceptCostCenter_BranchOffice];


GO
ALTER TABLE [Billing].[BillingConceptCostCenter] NOCHECK CONSTRAINT [FK_BillingConceptCostCenter_CostCenter];


GO
ALTER TABLE [Billing].[BillingConceptCostCenter] NOCHECK CONSTRAINT [FK_BillingConceptCostCenter_FunctionalUnit];

