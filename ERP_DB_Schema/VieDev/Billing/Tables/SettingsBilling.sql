CREATE TABLE [Billing].[SettingsBilling] (
    [Id]                                                            INT           IDENTITY (1, 1) NOT NULL,
    [IdOperatingUnit]                                               INT           NOT NULL,
    [InvoiceJournalVoucherTypeId]                                   INT           NOT NULL,
    [InvoiceAnnulmentJournalVoucherTypeId]                          INT           NOT NULL,
    [ReverseTransferJournalVoucherTypeId]                           INT           CONSTRAINT [DF_SettingsBilling_ReverseTransferJournalVoucherTypeId_1] DEFAULT ((1)) NOT NULL,
    [ProductInvoiceJournalVoucherTypeId]                            INT           NOT NULL,
    [CashReceiptsConfirm]                                           BIT           NOT NULL,
    [RoundingTypeRecoveryFeeType]                                   INT           NOT NULL,
    [CapitationRevenueMainAccountId]                                INT           NOT NULL,
    [CapitationProfitMainAccountId]                                 INT           NOT NULL,
    [CapitationLossMainAccountId]                                   INT           NOT NULL,
    [PatientAdvanceCashReceiptConceptId]                            INT           NOT NULL,
    [IndvidualAdvanceCashReceiptConceptId]                          INT           NOT NULL,
    [PrefixConsecutiveCapitation]                                   VARCHAR (4)   CONSTRAINT [DF_SettingsBilling_PrefixConsecutiveCapitation] DEFAULT ('') NOT NULL,
    [ConsecutiveControlCapitation]                                  BIGINT        CONSTRAINT [DF_SettingsBilling_ConsecutiveControlCapitation] DEFAULT ((1)) NOT NULL,
    [RequiresPermissionForCxCPatient]                               BIT           NOT NULL,
    [EntityCapitatedBillingAuthorizationId]                         INT           NOT NULL,
    [AccountingForSurgical]                                         TINYINT       CONSTRAINT [DF_SettingsBilling_AccountingForSurgical] DEFAULT ((1)) NOT NULL,
    [ProductSalesMainAccountId]                                     INT           NOT NULL,
    [ProductSalesCashReceiptConceptId]                              INT           NOT NULL,
    [ProductSalesCostCenterId]                                      INT           NULL,
    [RecoveryFeeDiscountMainAccountId]                              INT           NOT NULL,
    [RecoveryFeeDiscountCostCenterId]                               INT           NULL,
    [PermissionCategories]                                          TINYINT       CONSTRAINT [DF_SettingsBilling_PermissionCategories] DEFAULT ((1)) NOT NULL,
    [CreationUser]                                                  VARCHAR (20)  NOT NULL,
    [CreationDate]                                                  DATETIME      NOT NULL,
    [ModificationUser]                                              VARCHAR (20)  NULL,
    [ModificationDate]                                              DATETIME      NULL,
    [TimeStamp]                                                     ROWVERSION    NOT NULL,
    [RecognitionJournalVoucherTypeId]                               INT           CONSTRAINT [DF_SettingsBilling_RecognitionJournalVoucherTypeId] DEFAULT ((1)) NOT NULL,
    [ReverseRecognitionJournalVoucherTypeId]                        INT           CONSTRAINT [DF_SettingsBilling_ReverseTransferJournalVoucherTypeId] DEFAULT ((12)) NOT NULL,
    [ApplyBasicBilling]                                             BIT           CONSTRAINT [DF_SettingsBilling_ApplyBasicBilling] DEFAULT ((0)) NOT NULL,
    [ClientMainAccountId]                                           INT           NULL,
    [IVAPaymentMainAccountId]                                       INT           NULL,
    [ReteIVAMainAccountId]                                          INT           NULL,
    [ReteICAMainAccountId]                                          INT           NULL,
    [ReteFuenteMainAccountId]                                       INT           NULL,
    [AssociateCostCenter]                                           TINYINT       CONSTRAINT [DF_SettingsBilling_AssociateCostCenter] DEFAULT ((2)) NOT NULL,
    [FunctionalUnitId]                                              INT           NULL,
    [HealthProfessionalCode]                                        VARCHAR (20)  NULL,
    [ReteIVAConceptId]                                              INT           NULL,
    [BasicBillingJournalVoucherTypeId]                              INT           NULL,
    [CapitedPatientAdvanceCashReceiptConceptId]                     INT           NULL,
    [LiquidateSinceControlOutPatientService]                        BIT           CONSTRAINT [DF_SettingsBilling_LiquidateSinceControlOutPatientService] DEFAULT ((1)) NOT NULL,
    [DistributeCapitationControls]                                  TINYINT       CONSTRAINT [DF_SettingsBilling_DistributeCapitationControls] DEFAULT ((1)) NOT NULL,
    [InvoiceEntityCapitatedDistributionJournalVoucherTypeId]        INT           NULL,
    [ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId] INT           NULL,
    [BasicBillingCashReceiptConceptId]                              INT           NULL,
    [AnulateInvoicesPreviousPeriods]                                BIT           CONSTRAINT [DF_SettingsBilling_AnulateInvoicesPreviousPeriods] DEFAULT ((1)) NOT NULL,
    [BudgetInterface]                                               BIT           CONSTRAINT [DF_SettingsBilling_BudgetInterface] DEFAULT ((0)) NOT NULL,
    [DependencyId]                                                  INT           NULL,
    [BasicBillingDependencyId]                                      INT           NULL,
    [BasicBillingBudgetId]                                          INT           NULL,
    [BasicBillingAnnulmentJournalVoucherTypeId]                     INT           NULL,
    [ParticularHealthAdministratorId]                               INT           NULL,
    [InvoiceProductDevolutionPartialConceptNoteId]                  INT           NULL,
    [CalculateTaxAdvance]                                           TINYINT       CONSTRAINT [DF_SettingsBilling_CalculateTaxAdvance] DEFAULT ((0)) NOT NULL,
    [StatusFolioNewId]                                              INT           NULL,
    [StatusFolioClosedId]                                           INT           NULL,
    [AuthorizationNumberControl]                                    BIT           CONSTRAINT [DF__SettingsB__Autho__74C8F2B6] DEFAULT ((0)) NOT NULL,
    [ValidatePackaging]                                             BIT           CONSTRAINT [DF_SettingsBilling_ValidatePackaging] DEFAULT ((0)) NOT NULL,
    [AccountControlValidation]                                      BIT           CONSTRAINT [DF_SettingsBilling_AccountControlValidation] DEFAULT ((0)) NOT NULL,
    [IntegrationMiPres]                                             BIT           CONSTRAINT [DF__SettingsB__Integ__1FB41C12] DEFAULT ((0)) NOT NULL,
    [ClientIdName]                                                  VARCHAR (100) NULL,
    [ClientSecret]                                                  VARCHAR (100) NULL,
    [IncomeLockType]                                                TINYINT       NOT NULL,
    [ConsignmentSalereCognition]                                    INT           NULL,
    [ReversalRecognitionConsignmentSale]                            INT           NULL,
    [AccountingPackage]                                             BIT           CONSTRAINT [DF_SettingsBilling_AccountingPackage] DEFAULT ((0)) NOT NULL,
    [LiquidatedPackageJournalVoucherTypeId]                         INT           NULL,
    [ReversionLiquidatedPackageJournalVoucherTypeId]                INT           NULL,
    [AccountingPackageMainAccountId]                                INT           NULL,
    [ProjectedVariationPriceMainAccountId]                          INT           NULL,
    [LiquidateMasterAccount]                                        BIT           CONSTRAINT [DF__SettingsB__Liqui__216825B1] DEFAULT ((0)) NOT NULL,
    [GiftProductOutletConcept]                                      INT           NULL,
    [HandlesDifferentRates]                                         BIT           CONSTRAINT [DF__SettingsB__Handl__28BF2EB6] DEFAULT ((0)) NULL,
    [HasCustomTRM]                                                  BIT           CONSTRAINT [DF_SettingsBilling_HasCustomTRM] DEFAULT ((0)) NOT NULL,
    [MaxInvoiceItems]                                               INT           CONSTRAINT [DF__SettingsB__MaxIn__08925646] DEFAULT ((0)) NOT NULL,
    [ReversalPreviousYearsMainAccountId]                            INT           NULL,
    [ReversalPreviousYearsGenericBillingMainAccountId]              INT           NULL,
    [BillingAuthorizationCopayId]                                   INT           NULL,
    [AllowsSalesExecutiveAndSupplier]                               BIT           CONSTRAINT [DF__SettingsB__Allow__44FC4608] DEFAULT ((0)) NOT NULL,
    [AccountsConditionalCommercialDiscount]                         BIT           CONSTRAINT [DF__SettingsB__Accou__6A18BC1E] DEFAULT ((0)) NOT NULL,
    [GeneratePromissoryNote]                                        BIT           CONSTRAINT [DF__SettingsB__Gener__17956E0B] DEFAULT ((0)) NOT NULL,
    [LiquidateFolioInSpecificCurrency]                              BIT           CONSTRAINT [DF__SettingsB__Liqui__32145A1D] DEFAULT ((0)) NOT NULL,
    [SpecificCurrencyId]                                            INT           NULL,
    [requiresConditionsSale]                                        BIT           CONSTRAINT [DF__SettingsB__requi__45272E91] DEFAULT ((0)) NOT NULL,
    [ValidateAgeOfMajority]                                         BIT           DEFAULT ((0)) NOT NULL,
    [ApplyElectronicSalesTicket]                                    BIT           DEFAULT ((0)) NOT NULL,
    [AccountingVoucherReversalId]                                   INT           NULL,
    [AccountingVoucherGenerationId]                                 INT           NULL,
    [BillingAuthorizationId]                                        INT           NULL,
    CONSTRAINT [PK_SettingsBilling__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsBilling_AccountingPackageMainAccount] FOREIGN KEY ([AccountingPackageMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_AdjustmentConcept_GiftProductOutletConcept] FOREIGN KEY ([GiftProductOutletConcept]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_SettingsBilling_BillingAuthorization] FOREIGN KEY ([EntityCapitatedBillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_SettingsBilling_BillingAuthorizationCopay] FOREIGN KEY ([BillingAuthorizationCopayId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_SettingsBilling_Budget] FOREIGN KEY ([BasicBillingBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts] FOREIGN KEY ([PatientAdvanceCashReceiptConceptId]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts1] FOREIGN KEY ([IndvidualAdvanceCashReceiptConceptId]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts2] FOREIGN KEY ([ProductSalesCashReceiptConceptId]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts3] FOREIGN KEY ([CapitedPatientAdvanceCashReceiptConceptId]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts4] FOREIGN KEY ([BasicBillingCashReceiptConceptId]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_CostCenter] FOREIGN KEY ([ProductSalesCostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_SettingsBilling_CostCenter1] FOREIGN KEY ([RecoveryFeeDiscountCostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_SettingsBilling_Currency] FOREIGN KEY ([SpecificCurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_SettingsBilling_Dependency] FOREIGN KEY ([DependencyId]) REFERENCES [Budget].[Dependency] ([Id]),
    CONSTRAINT [FK_SettingsBilling_Dependency1] FOREIGN KEY ([BasicBillingDependencyId]) REFERENCES [Budget].[Dependency] ([Id]),
    CONSTRAINT [FK_SettingsBilling_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_SettingsBilling_HealthAdministrator] FOREIGN KEY ([ParticularHealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes] FOREIGN KEY ([ReverseRecognitionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes_ConsignmentSalereCognition] FOREIGN KEY ([ConsignmentSalereCognition]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes_ReversalRecognitionConsignmentSale] FOREIGN KEY ([ReversalRecognitionConsignmentSale]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes1] FOREIGN KEY ([InvoiceAnnulmentJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes2] FOREIGN KEY ([InvoiceJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes3] FOREIGN KEY ([ProductInvoiceJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes4] FOREIGN KEY ([RecognitionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes5] FOREIGN KEY ([ReverseTransferJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes6] FOREIGN KEY ([BasicBillingJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes7] FOREIGN KEY ([InvoiceEntityCapitatedDistributionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes8] FOREIGN KEY ([ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes9] FOREIGN KEY ([BasicBillingAnnulmentJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_LiquidatedPackageJournalVoucherType] FOREIGN KEY ([LiquidatedPackageJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts] FOREIGN KEY ([CapitationRevenueMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts1] FOREIGN KEY ([CapitationProfitMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts2] FOREIGN KEY ([CapitationLossMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts3] FOREIGN KEY ([ProductSalesMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts4] FOREIGN KEY ([RecoveryFeeDiscountMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts5] FOREIGN KEY ([ClientMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts6] FOREIGN KEY ([IVAPaymentMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts7] FOREIGN KEY ([ReteIVAMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts8] FOREIGN KEY ([ReteICAMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_MainAccounts9] FOREIGN KEY ([ReteFuenteMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SettingsBilling_PortfolioNoteConcept] FOREIGN KEY ([InvoiceProductDevolutionPartialConceptNoteId]) REFERENCES [Portfolio].[PortfolioNoteConcept] ([Id]),
    CONSTRAINT [FK_SettingsBilling_ProjectedVariationPriceMainAccount] FOREIGN KEY ([ProjectedVariationPriceMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_RetentionConcepts] FOREIGN KEY ([ReteIVAConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_ReversalPreviousYearsGenericBillingMainAccount] FOREIGN KEY ([ReversalPreviousYearsGenericBillingMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_ReversalPreviousYearsMainAccount] FOREIGN KEY ([ReversalPreviousYearsMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingsBilling_ReversionLiquidatedPackageJournalVoucherType] FOREIGN KEY ([ReversionLiquidatedPackageJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsBilling_StatusFolioClosed] FOREIGN KEY ([StatusFolioClosedId]) REFERENCES [Billing].[ConceptsCausesStatusFolio] ([Id]),
    CONSTRAINT [FK_SettingsBilling_StatusFolioNew] FOREIGN KEY ([StatusFolioNewId]) REFERENCES [Billing].[ConceptsCausesStatusFolio] ([Id]),
    CONSTRAINT [UQ_SettingsBilling__IdOperatingUnit] UNIQUE NONCLUSTERED ([IdOperatingUnit] ASC)
);


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_AccountingPackageMainAccount];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_AdjustmentConcept_GiftProductOutletConcept];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_BillingAuthorization];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_BillingAuthorizationCopay];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_Budget];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts1];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts2];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts3];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_CashReceiptConcepts4];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_CostCenter];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_CostCenter1];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_Dependency];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_Dependency1];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_FunctionalUnit];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_HealthAdministrator];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes_ConsignmentSalereCognition];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes_ReversalRecognitionConsignmentSale];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes1];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes2];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes3];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes4];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes5];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes6];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes7];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes8];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_JournalVoucherTypes9];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_LiquidatedPackageJournalVoucherType];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts1];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts2];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts3];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts4];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts5];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts6];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts7];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts8];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_MainAccounts9];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_OperatingUnit];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_PortfolioNoteConcept];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_ProjectedVariationPriceMainAccount];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_RetentionConcepts];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_ReversalPreviousYearsGenericBillingMainAccount];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_ReversalPreviousYearsMainAccount];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_ReversionLiquidatedPackageJournalVoucherType];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_StatusFolioClosed];


GO
ALTER TABLE [Billing].[SettingsBilling] NOCHECK CONSTRAINT [FK_SettingsBilling_StatusFolioNew];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'identifica si se requieren condiciones de venta o no', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'requiresConditionsSale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Id de la moneda especifica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'SpecificCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece 0 - No liquida el folio en una moneda en especifico, 1 - Liquida el folio en una moneda especifica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateFolioInSpecificCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Genera pagaré', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'GeneratePromissoryNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica 0 - No se contabiliza descuento comercial No Condicionado, 1 - Si se contabiliza descuento comercial No Condicionado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountsConditionalCommercialDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica 0 - No permite ejecutivo en ventas y proveedor, 1 - Si permite ejecutivo en ventas y proveedor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AllowsSalesExecutiveAndSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autorizaciones para copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationCopayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable de reversion   de facturas genericas (ej: fac basica) de vigencias anteriores (años anteriores)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalPreviousYearsGenericBillingMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable de reversion   de factura Salud de vigencias anteriores (años anteriores)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalPreviousYearsMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' límite máximo de líneas o ítems que puede tener una factura,si esta en 0 es sin limite', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'MaxInvoiceItems';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el modulo de facturacion tiene una TRM diferente al momento de facturar, los TRM se especifican en Billing.CustomTRM    Por ahora solo se tomaria para Facturacion en liquidacion de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HasCustomTRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica 0 - No se maneja diferentes tarifas, 1 - Maneja diferentes tarifas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HandlesDifferentRates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el reconocimiento de reversión de venta en consignación que se parametriza.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalRecognitionConsignmentSale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el reconocimiento de ventas en consignación parametrizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ConsignmentSalereCognition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el tipo de bloqueo de ingreso parametrizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IncomeLockType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si se parametriza 0 - No realiza validación en control de cuentas hospitalario, 1 - Si realiza validación en control de cuentas hospitalario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountControlValidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica 0 - No valida paquete, 1 - Valida paquete.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ValidatePackaging';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se calculará Anticipos de Impuestos:  0 - No  1 - Solo Informar  2 - Reconocer', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CalculateTaxAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si la distribución de los control de capitación se contabiliza usando:  1. La cuenta contable de ingreso por capitación   2. La cuenta contable asociada a los registros de control', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'DistributeCapitationControls';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se va tomar el centro de costo para cuando se hacen los comprobantes contables    1 - Costo (Unidad Funcional)  2 - Costo (Grupo)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AssociateCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Permiso para las categorias de facturacion  1 - Permiso de Usuario  2 - Permiso de Grupo de Atencion  3 - Permiso de Grupo de Atencion y Usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PermissionCategories';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la contabilizacion de quirurgicos   1 - Por Procedimiento  2 - Detallada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingForSurgical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica que se requiere un permiso de usuario para poder crear una cuenta por cobrar a un paciente (Pagare)    Este evento ocurre cuando el valor que le corresponde al paciente no puede ser pagado en su totalidad entonces se deberia poder crear un pagare', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RequiresPermissionForCxCPatient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Redondeo para la Cuota de Recuperacion  1= Peso  10 - Decena  100 - Centena  1000 - Milesima', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RoundingTypeRecoveryFeeType';

