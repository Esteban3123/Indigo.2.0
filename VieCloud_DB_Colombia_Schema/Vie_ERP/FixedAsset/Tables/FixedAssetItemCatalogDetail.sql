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



GO



GO



GO



GO



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje decimal (0-100, 4 decimales) de distribución del gasto contable del activo fijo entre estructuras o centros.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'DistributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Distribución', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'DistributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'DistributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (FK→CostCenter) al que se asigna el activo cuando AccountingStructureId es NULL.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal (FK→MainAccounts, default 3869) para gasto de renting financiero/alquiler de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanFinancialRentingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cuenta contable gasto renting financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanFinancialRentingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanFinancialRentingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal (FK→MainAccounts, default 3869) para registrar gasto de comodato/préstamo de equipos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'ExpenseLoanAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable gasto comodato', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'ExpenseLoanAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'ExpenseLoanAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal (FK→MainAccounts) para gasto de depreciación asociado a leasing operativo de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanLeasingSpendAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Gasto Depreciación x Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanLeasingSpendAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanLeasingSpendAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal (FK→MainAccounts) para gasto de depreciación en comodatos/préstamos de equipos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanSpendAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Gasto Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanSpendAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'LoanSpendAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de estructura contable para asignación de gastos. Si es NULL, debe usarse CostCenterId; FK→AccountingStructure.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Estructura Contable. Si es Null indica que debe llevar un id de Centro de Costos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a catálogo de equipos/activos fijos (FK→FixedAssetItemCatalog). Identifica el tipo de equipo o bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Catálogo del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la fila de detalle de catálogo de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de configuración contable y de centros de costo asociados a cada ítem del catálogo de activos fijos. Define las cuentas contables que se usan para préstamos, arrendamientos (leasing), renting financiero y gastos, junto con el porcentaje de distribución por centro de costo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogDetail';
