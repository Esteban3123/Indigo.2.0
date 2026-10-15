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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja depreciación por Distribución', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'HandlesDepreciationbyDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion  1 - Activo Fijo  2 - Intangibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Classification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En desuso por actualización.  Ahora se usará el parámetro IvaCost definido en SettingFixedAssetByLegalBook para determinar la forma en que se contabiliza y se lleva el valor del Iva en el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVADeductible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'TimeStamp';

