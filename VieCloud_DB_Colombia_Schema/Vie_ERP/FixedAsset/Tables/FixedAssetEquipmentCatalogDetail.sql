CREATE TABLE [FixedAsset].[FixedAssetEquipmentCatalogDetail] (
    [Id]                                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentCatalog]                  INT NOT NULL,
    [IdAccountingStructure]               INT NOT NULL,
    [IdLoanSpendAccountingAccount]        INT NOT NULL,
    [IdLoanLeasingSpendAccountingAccount] INT NOT NULL,
    CONSTRAINT [PK_FixedAssetEquipmentCatalogDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de gasto por depreciación/arrendamiento financiero (leasing). Referencia a estructura contable para registrar gastos de activos arrendados.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Gasto Depreciación x Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de gasto por depreciación de activos fijos. Vincula el equipo al código contable donde se registran gastos de amortización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Gasto Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la estructura contable asignada. Define el plan de cuentas y clasificación contable para el catálogo de equipos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Estructura Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del catálogo de equipos/activos fijos. Referencia a la clasificación y grupo del bien dentro del inventario de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Catálogo del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY). Clave primaria que indexa cada detalle del catálogo de equipos con su configuración contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de configuración contable de los equipos del catálogo de activos fijos. Relaciona cada equipo con las cuentas contables de gasto asociadas a préstamos y arrendamientos (leasing).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEquipmentCatalogDetail';
