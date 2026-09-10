CREATE TABLE [Inventory].[ProductGroup] (
    [Id]                                           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                         VARCHAR (20)   NOT NULL,
    [Name]                                         VARCHAR (100)  NOT NULL,
    [GroupClass]                                   TINYINT        NOT NULL,
    [SubclassCode]                                 TINYINT        NOT NULL,
    [IncomeAccountId]                              INT            NOT NULL,
    [InventoryAccountPayableConceptId]             INT            NOT NULL,
    [DeclarantRetentionAccountPayableConceptId]    INT            CONSTRAINT [DF_ProductGroup_DeclarantRetentionAccountPayableConceptId] DEFAULT ((73)) NOT NULL,
    [NotDeclarantRetentionAccountPayableConceptId] INT            CONSTRAINT [DF_ProductGroup_NotDeclarantRetentionAccountPayableConceptId] DEFAULT ((73)) NOT NULL,
    [CostCenterId]                                 INT            NOT NULL,
    [ReferenceInputDebitAccountId]                 INT            NOT NULL,
    [ReferenceInputCreditAccountId]                INT            NOT NULL,
    [ReferenceOutputDebitAccountId]                INT            NOT NULL,
    [ReferenceOutputCreditAccountId]               INT            NOT NULL,
    [ExcludeFreightCosts]                          BIT            NOT NULL,
    [ProductReplacementTime]                       INT            NOT NULL,
    [ProductsSourcingTime]                         INT            NOT NULL,
    [SecurityPercentage]                           NUMERIC (5, 2) NOT NULL,
    [BudgetCategoryId]                             INT            NULL,
    [Status]                                       BIT            NOT NULL,
    [CreationUser]                                 VARCHAR (20)   NOT NULL,
    [CreationDate]                                 DATETIME       NOT NULL,
    [ModificationUser]                             VARCHAR (20)   NULL,
    [ModificationDate]                             DATETIME       NULL,
    [TimeStamp]                                    ROWVERSION     NOT NULL,
    [InventoryCostMainAccountId]                   INT            NULL,
    [ReteFuenteConceptId]                          INT            NULL,
    [ConsignmentMerchandiseDebitAccountId]         INT            CONSTRAINT [DF__ProductGr__Consi__0829D8A0] DEFAULT ((2649)) NOT NULL,
    [ConsignmentMerchandiseCreditAccountId]        INT            CONSTRAINT [DF__ProductGr__Consi__091DFCD9] DEFAULT ((2649)) NOT NULL,
    [CounterpartCostConsignedInventoryId]          INT            CONSTRAINT [DF_ProductGroup_CounterpartCostConsignedInventory] DEFAULT ((2649)) NOT NULL,
    [IncomeRecognitionMainAccountId]               INT            NULL,
    [IVAAccountId]                                 INT            NULL,
    [WithholdingTaxAccountId]                      INT            NULL,
    [WithholdingICAConceptId]                      INT            NULL,
    [WithholdingICAAccountId]                      INT            NULL,
    [AffectBudget]                                 BIT            CONSTRAINT [DF_ProductGroup_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                                     INT            NULL,
    [AccountingPackageMainAccountId]               INT            NULL,
    [FavorableDeviationMainAccountId]              INT            NULL,
    [VariationPVMainAccountId]                     INT            NULL,
    [EconomicActivityId]                           INT            NULL,
    CONSTRAINT [PK_ProductGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductGroup_AccountingPackageMainAccount] FOREIGN KEY ([AccountingPackageMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_AccountPayableConcepts] FOREIGN KEY ([DeclarantRetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_AccountPayableConcepts1] FOREIGN KEY ([InventoryAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_AccountPayableConcepts2] FOREIGN KEY ([NotDeclarantRetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_ProductGroup_Category] FOREIGN KEY ([BudgetCategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_ProductGroup_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ProductGroup_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_ProductGroup_FavorableDeviationMainAccount] FOREIGN KEY ([FavorableDeviationMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts] FOREIGN KEY ([IncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts1] FOREIGN KEY ([InventoryCostMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts10] FOREIGN KEY ([ConsignmentMerchandiseDebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts11] FOREIGN KEY ([ConsignmentMerchandiseCreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts12] FOREIGN KEY ([CounterpartCostConsignedInventoryId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts2] FOREIGN KEY ([IncomeRecognitionMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts3] FOREIGN KEY ([IVAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts4] FOREIGN KEY ([WithholdingTaxAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts5] FOREIGN KEY ([WithholdingICAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts6] FOREIGN KEY ([ReferenceInputDebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts7] FOREIGN KEY ([ReferenceInputCreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts8] FOREIGN KEY ([ReferenceOutputDebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts9] FOREIGN KEY ([ReferenceOutputCreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_RetentionConcepts] FOREIGN KEY ([ReteFuenteConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_RetentionConcepts2] FOREIGN KEY ([WithholdingICAConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_VariationPVMainAccount] FOREIGN KEY ([VariationPVMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductGroup__Code]
    ON [Inventory].[ProductGroup]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT a GeneralLedger.MainAccounts para contabilizar variaciones de precio de venta (PV), cambios en margen o revaluación de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'VariationPVMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contabilización variación de precios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'VariationPVMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'VariationPVMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT a GeneralLedger.MainAccounts para registrar desviaciones favorables, diferencias positivas en costo o rendimiento vs presupuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'FavorableDeviationMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contabilización variación favorable', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'FavorableDeviationMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'FavorableDeviationMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT a GeneralLedger.MainAccounts para contabilización de paquetes, kits o bundles de productos/servicios vendidos como unidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'AccountingPackageMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contabilización de paquetes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'AccountingPackageMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'AccountingPackageMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a Budget.Budget; identifica presupuesto de gastos a aplicar en reconocimiento de facturas, genera registros contables automáticos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de gastos a usar en el reconocimiento de facturas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0=No afecta, 1=Sí afecta); indica si el grupo de producto/servicio impacta presupuesto; si activo (1), la factura crea reconocimiento automático', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts para retención de ICA en ventas, impuesto al consumo a nivel local/municipal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Retencion de ICA en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a Payments.AccountPayableConcepts para concepto de retención de ICA en ventas, vinculado con cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de Retencion de ICA en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts para retención en la fuente (renta) en ventas, impuesto retenido a cliente o proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Retencion de la Fuente en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts para IVA en ventas, impuesto al valor agregado generado en la transacción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IVAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable IVA en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IVAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IVAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts para reconocimiento de ingresos NIIF, diferimiento de ingresos según criterio de facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta para reconocimiento de ingresos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts, contrapartida de costo para dispensación, devoluciones e inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CounterpartCostConsignedInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta que será la contrapartida al costo para la contabilización de la dispensación, devoluciones del inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CounterpartCostConsignedInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CounterpartCostConsignedInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts, cuenta crédito (pasivo) para inventario en consignación del consignatario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseCreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta credito para la contabilización del inventario en consignación por parte del consignatario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseCreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseCreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts, cuenta débito (activo) para inventario en consignación del consignatario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseDebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta debito para la contabilización del inventario en consignación por parte del consignatario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseDebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseDebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a Payments.AccountPayableConcepts para concepto de retención en la fuente en ventas, gestión de impuestos sobre ingresos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReteFuenteConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de Retencion de la Fuente en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReteFuenteConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReteFuenteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts para costo de inventario, registro del costo de productos/suministros vendidos o dispensados', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'InventoryCostMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable costo inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'InventoryCostMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'InventoryCostMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP generado automáticamente; marca exacta de creación, modificación o evento en la tabla ProductGroup, auditoría temporal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME nullable; fecha y hora de último cambio en el registro del grupo de productos, nulo si nunca se modificó', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) nullable; usuario que realizó la última modificación en el grupo, nulo si nunca se editó', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME NOT NULL; fecha y hora de creación del registro del grupo de productos, trazabilidad inicial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) NOT NULL; usuario que creó el grupo de productos, auditoría de origen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0=Inactivo, 1=Activo); estado del grupo de productos, controla si se puede usar en transacciones de compra, venta, dispensación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro 1 - Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a Budget.Category, clasificación o rubro de gastos (capítulo presupuestario) para control de gasto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'BudgetCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del  rubro de gastos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'BudgetCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'BudgetCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(5,2) porcentaje de seguridad/amortiguador aplicado al costo o stock mínimo del grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'SecurityPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de seguridad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'SecurityPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'SecurityPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, días estimados de abastecimiento (lead time) para adquisición de productos del grupo desde proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ProductsSourcingTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de abastecimiento de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ProductsSourcingTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ProductsSourcingTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, días para reposición o rotación de inventario del grupo, gestión de stock y obsolescencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ProductReplacementTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de reposicion de productos (Dias)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ProductReplacementTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ProductReplacementTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT (0=Incluir flete, 1=Excluir); especifica si se asigna costo de flete/transporte a suministros del grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ExcludeFreightCosts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realiza el proceso de costo a los productos (Suministros) asociados al grupo (EXCLUIR COSTO EN SUMINISTROS)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ExcludeFreightCosts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ExcludeFreightCosts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts, cuenta crédito de remisión/salida de inventario del grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceOutputCreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta credito de la remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceOutputCreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceOutputCreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts, cuenta débito de remisión/salida de inventario del grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceOutputDebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta debito de la remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceOutputDebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceOutputDebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts, cuenta crédito de remisión/entrada de inventario del grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceInputCreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta credito de la remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceInputCreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceInputCreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts, cuenta débito de remisión/entrada de inventario del grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceInputDebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta debito de la remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceInputDebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceInputDebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a Payroll.CostCenter, identifica centro de costo donde se asignan gastos e ingresos del grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a Payments.AccountPayableConcepts, concepto de retención en fuente para proveedores no declarantes, debe ser tipo Específico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'NotDeclarantRetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pargar para la retencion en la fuente,en proveedores que sean no declarantes, Este concepto debe ser de tipo Especifico y debe Manejar Retencion y No debe ser de tipo 383 y 384', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'NotDeclarantRetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'NotDeclarantRetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a Payments.AccountPayableConcepts, concepto de retención en fuente para proveedores declarantes, debe ser tipo Específico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'DeclarantRetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pargar para la retencion en la fuente,en proveedores que sean Declarantes, Este concepto debe ser de tipo Especifico y debe Manejar Retencion y No debe ser de tipo 383 y 384', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'DeclarantRetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'DeclarantRetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a Payments.AccountPayableConcepts, concepto de cuentas por pagar de inventarios, genera comprobante en entrada y dispensación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'InventoryAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de concepo de la cuenta por pagar de inventarios, Esa cuenta es usada para generar el comprobsante contable en los procesos de comprobante de entrada  y dispensacion farmaceuica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'InventoryAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'InventoryAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia INT FK a GeneralLedger.MainAccounts, cuenta contable de ingresos/ventas afectada por transacciones del grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable que se va afectar en los ingresos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT clasificación detallada: 1=Insumos Hospitalarios, 2=Material Quirúrgico, 3=Medicamentos; granularidad dentro de GroupClass', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'SubclassCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de subatencion o subclase  1 - Insumos Hospitalarios  2 - Material Quirurgico  3 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'SubclassCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'SubclassCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT tipo de productos del grupo: 1=Producto (bien tangible), 2=Servicio (bien intangible); define tratamiento contable y fiscal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'GroupClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la clase de productos del grupo  1 - Producto  2 - Servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'GroupClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'GroupClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100) nombre descriptivo del grupo de productos/servicios, visible en interfaces, reportes y búsquedas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) código único identificador del grupo, usado en órdenes, facturas, presupuestos y auditoría', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY(1,1), identificador único autoincrementable de la tabla ProductGroup, clave primaria clustered', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos de productos del inventario, con su clasificación contable, cuentas de débito/crédito para entradas y salidas, parámetros de reposición y abastecimiento, retenciones en la fuente, IVA, ICA, costos de consignación y configuración presupuestal. Permite agrupar productos bajo un mismo esquema contable y de control de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad económica asociada al grupo de productos, usada para clasificar las operaciones según el código de actividad económica (CIIU) para efectos tributarios y de retenciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
