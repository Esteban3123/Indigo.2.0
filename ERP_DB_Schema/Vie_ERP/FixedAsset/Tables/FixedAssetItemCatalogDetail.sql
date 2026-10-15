CREATE TABLE [FixedAsset].[FixedAssetItemCatalogDetail] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ItemCatalogId]                 INT             NOT NULL,
    [AccountingStructureId]         INT             NULL,
    [LoanSpendAccountId]            INT             NOT NULL,
    [LoanLeasingSpendAccountId]     INT             NOT NULL,
    [ExpenseLoanAccountId]          INT             CONSTRAINT [DF_FixedAssetItemCatalogDetail_ExpenseLoanAccountId] DEFAULT ((3869)) NOT NULL,
    [LoanFinancialRentingAccountId] INT             CONSTRAINT [DF_FixedAssetItemCatalogDetail_LoanFinancialRentingAccountId] DEFAULT ((3869)) NOT NULL,
    [CostCenterId]                  INT             NULL,
    [DistributionPercentage]        DECIMAL (20, 4) NULL,
    CONSTRAINT [PK_FixedAssetItemCatalogDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_AccountingStructure] FOREIGN KEY ([AccountingStructureId]) REFERENCES [Payroll].[AccountingStructure] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_EquipmentCatalog] FOREIGN KEY ([ItemCatalogId]) REFERENCES [FixedAsset].[FixedAssetItemCatalog] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_MainAccounts] FOREIGN KEY ([LoanLeasingSpendAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_MainAccounts1] FOREIGN KEY ([LoanSpendAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalogDetail_MainAccounts] FOREIGN KEY ([ExpenseLoanAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalogDetail_MainAccounts1] FOREIGN KEY ([LoanFinancialRentingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogDetail] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_AccountingStructure];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogDetail] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_CostCenter];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogDetail] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_EquipmentCatalog];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogDetail] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_MainAccounts];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogDetail] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentCatalogDetail_MainAccounts1];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogDetail] NOCHECK CONSTRAINT [FK_FixedAssetItemCatalogDetail_MainAccounts];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogDetail] NOCHECK CONSTRAINT [FK_FixedAssetItemCatalogDetail_MainAccounts1];


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetItemCatalogDetail__AccountingStructureId__ItemCatalogId]
    ON [FixedAsset].[FixedAssetItemCatalogDetail]([AccountingStructureId] ASC, [ItemCatalogId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetItemCatalogDetail__ItemCatalogId]
    ON [FixedAsset].[FixedAssetItemCatalogDetail]([ItemCatalogId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetItemCatalogDetail]
    ON [FixedAsset].[FixedAssetItemCatalogDetail]([ItemCatalogId] ASC, [AccountingStructureId] ASC) WHERE ([AccountingStructureId] IS NOT NULL);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de Distribución', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'DistributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cuenta contable gasto renting financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanFinancialRentingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable gasto comodato', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'ExpenseLoanAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Gasto Depreciación x Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanLeasingSpendAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Gasto Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanSpendAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Estructura Contable. Si es Null indica que debe llevar un id de Centro de Costos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Catálogo del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'Id';

