CREATE TABLE [Budget].[PrivateBudgetItemsStructureDetail] (
    [Id]                            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PrivateBudgetItemsStructureId] INT NOT NULL,
    [MainAccountId]                 INT NULL,
    [CostCenterId]                  INT NULL,
    [ThirdPartyId]                  INT NULL,
    CONSTRAINT [PK_PrivateBudgetItemsStructureDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_PrivateBudgetItemsStructure] FOREIGN KEY ([PrivateBudgetItemsStructureId]) REFERENCES [Budget].[PrivateBudgetItemsStructure] ([Id]),
    CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Budget].[PrivateBudgetItemsStructureDetail] NOCHECK CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_CostCenter];


GO
ALTER TABLE [Budget].[PrivateBudgetItemsStructureDetail] NOCHECK CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_MainAccounts];


GO
ALTER TABLE [Budget].[PrivateBudgetItemsStructureDetail] NOCHECK CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_PrivateBudgetItemsStructure];


GO
ALTER TABLE [Budget].[PrivateBudgetItemsStructureDetail] NOCHECK CONSTRAINT [FK_PrivateBudgetItemsStructureDetail_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'PrivateBudgetItemsStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudgetItemsStructureDetail', @level2type = N'COLUMN', @level2name = N'Id';

