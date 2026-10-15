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


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BillingConceptCostCenter]
    ON [Billing].[BillingConceptCostCenter]([BillingConceptId] ASC, [BranchOfficeId] ASC, [FunctionalUnitId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptCostCenter', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la sucursal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptCostCenter', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de facturacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptCostCenter', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptCostCenter', @level2type = N'COLUMN', @level2name = N'Id';

