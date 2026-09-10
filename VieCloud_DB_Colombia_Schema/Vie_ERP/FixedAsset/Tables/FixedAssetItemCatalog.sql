CREATE TABLE [FixedAsset].[FixedAssetItemCatalog] (
    [Id]                                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                         VARCHAR (20)  NOT NULL,
    [Description]                                  VARCHAR (MAX) NOT NULL,
    [Active]                                       BIT           NOT NULL,
    [IncomeAccountPayableConceptId]                INT           NULL,
    [IncomeAccountId]                              INT           NOT NULL,
    [IncomeLeasingAccountId]                       INT           NULL,
    [DebitLoanAccountId]                           INT           NULL,
    [CreditLoanAccountId]                          INT           NULL,
    [DepreciationAccountId]                        INT           NULL,
    [DebitValorizationAccountId]                   INT           NULL,
    [CreditValorizationAccountId]                  INT           NULL,
    [DebitDevaluationAccountId]                    INT           NULL,
    [CreditDevaluationAccountId]                   INT           NULL,
    [NetIncomeAccountId]                           INT           NULL,
    [LossMainAccountId]                            INT           NULL,
    [ReplacementCreditMainAccountId]               INT           NULL,
    [WarehouseAssetsMainAccountId]                 INT           CONSTRAINT [DF_FixedAssetItemCatalog_WarehouseAssetsMainAccountId] DEFAULT ((2619)) NULL,
    [MaintenanceAssetsMainAccountId]               INT           CONSTRAINT [DF_FixedAssetItemCatalog_MaintenanceAssetsMainAccountId] DEFAULT ((2619)) NULL,
    [DeclarantRetentionAccountPayableConceptId]    INT           NULL,
    [NotDeclarantRetentionAccountPayableConceptId] INT           NULL,
    [Status]                                       BIT           NOT NULL,
    [CreationUser]                                 VARCHAR (20)  NOT NULL,
    [CreationDate]                                 DATETIME      NOT NULL,
    [ModificationUser]                             VARCHAR (20)  NULL,
    [ModificationDate]                             DATETIME      NULL,
    [TimeStamp]                                    ROWVERSION    NOT NULL,
    [IVADeductible]                                BIT           CONSTRAINT [DF_FixedAssetItemCatalog_IVADeductible] DEFAULT ((0)) NOT NULL,
    [DepreciationLeasingAccountId]                 INT           CONSTRAINT [DF_FixedAssetItemCatalog_DepreciationLeasingAccountId] DEFAULT ((3935)) NULL,
    [LoanLeasingAccountId]                         INT           CONSTRAINT [DF_FixedAssetItemCatalog_LoanLeasingAccountId] DEFAULT ((4)) NULL,
    [CreditEquipmentPlantAcumulatedAccountId]      INT           NULL,
    [AccumulatedDeteriorationAccountId]            INT           CONSTRAINT [DF_FixedAssetItemCatalog_AccumulatedDeteriorationAccountId] DEFAULT ((4)) NULL,
    [FinancialRentingAccountId]                    INT           CONSTRAINT [DF_FixedAssetItemCatalog_FinancialRentingAccountId] DEFAULT ((0)) NULL,
    [IVAAccountId]                                 INT           NULL,
    [WithholdingTaxConceptId]                      INT           NULL,
    [WithholdingTaxAccountId]                      INT           NULL,
    [WithholdingICAConceptId]                      INT           NULL,
    [WithholdingICAAccountId]                      INT           NULL,
    [AffectBudget]                                 BIT           CONSTRAINT [DF_FixedAssetItemCatalog_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                                     INT           NULL,
    [Classification]                               TINYINT       CONSTRAINT [DF__FixedAsse__Class__698EAC39] DEFAULT ((1)) NOT NULL,
    [HandlesDepreciationbyDistribution]            BIT           CONSTRAINT [DF_FixedAssetItemCatalog_HandlesDepreciationbyDistribution] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetItemCatalog__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentCatalog_MainAccounts] FOREIGN KEY ([CreditLoanAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EquipmentCatalog_MainAccounts1] FOREIGN KEY ([DebitLoanAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EquipmentCatalog_MainAccounts2] FOREIGN KEY ([IncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EquipmentCatalog_MainAccounts3] FOREIGN KEY ([IncomeLeasingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts] FOREIGN KEY ([DepreciationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts1] FOREIGN KEY ([DebitValorizationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts2] FOREIGN KEY ([CreditValorizationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts3] FOREIGN KEY ([DebitDevaluationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts4] FOREIGN KEY ([CreditDevaluationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_AccountPayableConcepts] FOREIGN KEY ([DeclarantRetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_AccountPayableConcepts1] FOREIGN KEY ([NotDeclarantRetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_AccountPayableConcepts2] FOREIGN KEY ([IncomeAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts] FOREIGN KEY ([NetIncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts1] FOREIGN KEY ([LossMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts10] FOREIGN KEY ([IVAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts11] FOREIGN KEY ([WithholdingTaxAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts12] FOREIGN KEY ([WithholdingICAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts2] FOREIGN KEY ([ReplacementCreditMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts3] FOREIGN KEY ([WarehouseAssetsMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts4] FOREIGN KEY ([MaintenanceAssetsMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts5] FOREIGN KEY ([DepreciationLeasingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts6] FOREIGN KEY ([CreditEquipmentPlantAcumulatedAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts7] FOREIGN KEY ([LoanLeasingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts8] FOREIGN KEY ([AccumulatedDeteriorationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts9] FOREIGN KEY ([FinancialRentingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_RetentionConcepts] FOREIGN KEY ([WithholdingTaxConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_RetentionConcepts1] FOREIGN KEY ([WithholdingICAConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetItemCatalog__Code]
    ON [FixedAsset].[FixedAssetItemCatalog]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el activo fijo maneja depreciación por distribución (1=Sí, 0=No). BIT. Determina si la depreciación se calcula y contabiliza por distribución de centros de costo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'HandlesDepreciationbyDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja depreciación por Distribución', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'HandlesDepreciationbyDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'HandlesDepreciationbyDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del activo: 1=Activo Fijo, 2=Intangibles, 3=Bienes Controlables. TINYINT. Catégoriza el tipo de bien para propósitos contables y de control.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Classification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion:  1 - Activo Fijo  2 - Intangibles  3 - Bienes Controlables', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Classification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Classification';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del presupuesto de gastos asociado al reconocimiento de facturas de activos fijos. INT FK→Budget.Budget. Usada en radicar facturas con creación automática de reconocimientos presupuestales.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de gastos a usar en el reconocimiento de facturas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el catálogo de activos afecta presupuesto (1=Sí, 0=No). BIT. Al radicar factura con AffectBudget=1, crea automáticamente reconocimiento presupuestal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de retención de ICA en ventas. INT FK→GeneralLedger.MainAccounts. Registra impuesto municipal sobre actividad comercial retenido.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Retencion de ICA en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención de ICA en ventas. INT FK→Payments.AccountPayableConcepts. Especifica tipo y tratamiento de retención ICA.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de Retencion de ICA en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de retención en la fuente en ventas. INT FK→GeneralLedger.MainAccounts. Registra impuesto retenido al momento de la venta.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Retencion de la Fuente en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención en la fuente en ventas. INT FK→Payments.AccountPayableConcepts. Define parámetros de retención fiscal en venta de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingTaxConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de Retencion de la Fuente en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingTaxConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingTaxConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de IVA en ventas. INT FK→GeneralLedger.MainAccounts. Contabiliza impuesto al valor agregado generado por la venta.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable IVA en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de depreciación para renting financiero. INT. Acumula depreciación de activos bajo esquema de arrendamiento financiero.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'FinancialRentingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cuenta depreciación renting financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'FinancialRentingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'FinancialRentingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable para deterioro acumulado de propiedad, planta y equipo. INT FK→GeneralLedger.MainAccounts. Registra pérdida de valor por uso, obsolescencia o daño.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AccumulatedDeteriorationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable para el deterioro de propiedad, planta y equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AccumulatedDeteriorationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AccumulatedDeteriorationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta crédito para deterioro acumulado de propiedad, planta y equipo. INT FK→GeneralLedger.MainAccounts. Acredita el deterioro acumulado en el lado crédito.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditEquipmentPlantAcumulatedAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Crédito Deterioro Acumulado de Propiedad, Planta y Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditEquipmentPlantAcumulatedAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditEquipmentPlantAcumulatedAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de depreciación para activos en comodato (préstamo de uso). INT FK→GeneralLedger.MainAccounts. Default=4. Registra depreciación de bienes recibidos sin transferencia de propiedad.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'LoanLeasingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable de depreciación para los activos comodatos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'LoanLeasingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'LoanLeasingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable que se acredita al depreciar activos arrendados. INT FK→GeneralLedger.MainAccounts. Default=3935. Acumula depreciación de bienes bajo arrendamiento.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DepreciationLeasingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta contable que se acredita al momento de depreciar un activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DepreciationLeasingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DepreciationLeasingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deprecated: indica si IVA es deducible (1=Sí, 0=No). BIT. En desuso; usar parámetro IvaCost en SettingFixedAssetByLegalBook para contabilización de IVA en activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVADeductible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En desuso por actualización.  Ahora se usará el parámetro IvaCost definido en SettingFixedAssetByLegalBook para determinar la forma en que se contabiliza y se lleva el valor del Iva en el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVADeductible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVADeductible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de control de concurrencia (TIMESTAMP SQL Server). Registra automáticamente el instante de creación, modificación o cambio del registro para auditoría y sincronización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro. DATETIME. Información de auditoría para trazabilidad de cambios en el catálogo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que realizó la última modificación. VARCHAR(20). Auditoría de quién modificó el catálogo de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro. DATETIME. Información de auditoría del momento de ingreso en el sistema.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó el registro. VARCHAR(20). Auditoría de quién ingresó el catálogo al sistema.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del catálogo: 1=Activo, 0=Inactivo. BIT. Indica si el catálogo está habilitado para uso en transacciones.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuentas por pagar para retención en la fuente, proveedores no declarantes. INT FK→Payments.AccountPayableConcepts. Debe ser tipo Específico, manejar retención, no ser 383/384.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'NotDeclarantRetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pargar para la retencion en la fuente,en proveedores que sean no declarantes, Este concepto debe ser de tipo Especifico y debe Manejar Retencion y No debe ser de tipo 383 y 384', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'NotDeclarantRetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'NotDeclarantRetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuentas por pagar para retención en la fuente, proveedores declarantes. INT FK→Payments.AccountPayableConcepts. Debe ser tipo Específico, manejar retención, no ser 383/384.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DeclarantRetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pargar para la retencion en la fuente,en proveedores que sean Declarantes, Este concepto debe ser de tipo Especifico y debe Manejar Retencion y No debe ser de tipo 383 y 384', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DeclarantRetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DeclarantRetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de activos fijos en mantenimiento. INT FK→GeneralLedger.MainAccounts. Default=2619. Agrupa bienes retirados temporalmente del uso para reparación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'MaintenanceAssetsMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la cuenta contable de Activos en Mantenimiento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'MaintenanceAssetsMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'MaintenanceAssetsMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de activos fijos en bodega/almacén. INT FK→GeneralLedger.MainAccounts. Default=2619. Contabiliza bienes disponibles aún no puestos en servicio.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WarehouseAssetsMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la cuenta contable de Activos en Bodega', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WarehouseAssetsMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WarehouseAssetsMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta crédito de reposición de activos fijos. INT FK→GeneralLedger.MainAccounts. Registra crédito por reemplazo o cambio de bienes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ReplacementCreditMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cuenta Credito de Reposicion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ReplacementCreditMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ReplacementCreditMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de pérdida del ejercicio por disposición de activos. INT FK→GeneralLedger.MainAccounts. Registra pérdidas en la venta o baja de bienes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'LossMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cuenta contable de Perdida del ejercicio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'LossMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'LossMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de ganancia neta/ingresos del ejercicio por venta de activos. INT FK→GeneralLedger.MainAccounts. Registra utilidades en la disposición de bienes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'NetIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta contable de la ganancia del ejercicio o Ingresos Netos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'NetIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'NetIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable crédito de desvalorización de activos. INT FK→GeneralLedger.MainAccounts. Acumula disminuciones de valor por evento excepcional.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditDevaluationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta Contable Credito de la Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditDevaluationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditDevaluationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable débito de desvalorización de activos. INT FK→GeneralLedger.MainAccounts. Registra pérdida de valor en el lado débito.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitDevaluationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta Contable Débito de la Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitDevaluationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitDevaluationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable crédito de valorización de activos. INT FK→GeneralLedger.MainAccounts. Acumula incrementos de valor revaluado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditValorizationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta Contable Credito de la Valorizacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditValorizationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditValorizationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable débito de valorización de activos. INT FK→GeneralLedger.MainAccounts. Registra aumento de valor revaluado en el lado débito.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitValorizationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta Contable Debito de la Valorizacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitValorizationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitValorizationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable crédito de depreciación acumulada. INT FK→GeneralLedger.MainAccounts. Acumula gasto de depreciación periódica de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DepreciationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Cuenta Crédito de Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DepreciationAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DepreciationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable crédito de comodato (préstamo de uso). INT FK→GeneralLedger.MainAccounts. Registra bienes recibidos en préstamo sin transferencia de propiedad.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditLoanAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Cuenta Crédito de Comodato', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditLoanAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditLoanAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable débito de comodato. INT FK→GeneralLedger.MainAccounts. Registra bienes entregados en préstamo de uso.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitLoanAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Cuenta Débito de Comodato', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitLoanAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitLoanAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de ingresos por arrendamiento financiero. INT FK→GeneralLedger.MainAccounts. Registra ingresos generados por arrendamiento de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeLeasingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cuenta de Ingreso de Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeLeasingAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeLeasingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de Propiedad, Planta y Equipo. INT FK→GeneralLedger.MainAccounts. Cuenta principal que registra activos fijos en el balance.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de Propiedad, Planta y Equipo. Se selecciona.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuentas por pagar de tipo General. INT FK→Payments.AccountPayableConcepts. Tipo Específico, maneja retención fiscal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuenta por pagar, este debe ser de tipo General', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el catálogo de artículos está activo (1=Sí, 0=No). BIT. Estado de disponibilidad para uso en transacciones de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Active';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Active';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Active';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del catálogo de artículos de activos fijos. VARCHAR(MAX). Identifica y documenta el tipo, características y propósito del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del catalogo de Articulos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del catálogo de artículos. VARCHAR(20). Identificador alfanumérico para búsqueda y referencia rápida de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Catálogo de Articulos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único del catálogo de artículos de activos fijos. INT IDENTITY. Clave primaria para relacionar activos específicos con su clasificación contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Catálogo de Articulos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos o categorías de activos fijos. Define la configuración contable de cada clase de activo (maquinaria, equipos, vehículos, etc.), incluyendo las cuentas contables asociadas para ingreso, depreciación, valorización, desvalorización, arrendamiento financiero (leasing/renting), retenciones, IVA y presupuesto.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog';
