CREATE TABLE [Cost].[CostSetting] (
    [Id]                                    INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                                  INT     NOT NULL,
    [Month]                                 INT     NOT NULL,
    [AccountingCosts]                       BIT     CONSTRAINT [DF_CostSetting_AccountingCosts] DEFAULT ((0)) NOT NULL,
    [JournalVoucherTypeId]                  INT     NULL,
    [CostEstimateLabor]                     TINYINT CONSTRAINT [DF_CostInteropCostSetting_CostEstimateLabor] DEFAULT ((1)) NOT NULL,
    [AccountPayableConceptsId]              INT     NOT NULL,
    [ValidateActivities]                    BIT     CONSTRAINT [DF_CostSetting_ValidateActivities] DEFAULT ((0)) NOT NULL,
    [ActivityCalculationBy]                 TINYINT CONSTRAINT [DF_CostSetting_ActivityCalculationBy] DEFAULT ((1)) NOT NULL,
    [ProvisionJournalVoucherTypeId]         INT     NULL,
    [ProvisionReversalJournalVoucherTypeId] INT     NULL,
    [AverageStandardCostActivity]           BIT     CONSTRAINT [DF_CostSetting_AverageStandardCostActivity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostInteropCostSetting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostInteropCostSetting_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CostSetting_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptsId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_CostSetting_ProvisionJournalVoucherType] FOREIGN KEY ([ProvisionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CostSetting_ProvisionReversalJournalVoucherType] FOREIGN KEY ([ProvisionReversalJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);


GO
ALTER TABLE [Cost].[CostSetting] NOCHECK CONSTRAINT [FK_CostInteropCostSetting_JournalVoucherTypes];


GO
ALTER TABLE [Cost].[CostSetting] NOCHECK CONSTRAINT [FK_CostSetting_AccountPayableConcepts];


GO
ALTER TABLE [Cost].[CostSetting] NOCHECK CONSTRAINT [FK_CostSetting_ProvisionJournalVoucherType];


GO
ALTER TABLE [Cost].[CostSetting] NOCHECK CONSTRAINT [FK_CostSetting_ProvisionReversalJournalVoucherType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de tipo de comprobante contable de reversión de provisiones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionReversalJournalVoucherTypeId';

