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
    [TaxInformation]                                                NVARCHAR(700) NULL,
    [EnableMandateBilling]                                          BIT           CONSTRAINT [DF_SettingsBilling_EnableMandateBilling] DEFAULT ((0)) NOT NULL,
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si se requieren condiciones comerciales previas para procesar ventas o facturas en la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'requiresConditionsSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identifica si se requieren condiciones de venta o no', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'requiresConditionsSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'requiresConditionsSale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de moneda específica (INT FK) configurada para liquidaciones en divisa distinta a moneda local.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'SpecificCurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Id de la moneda especifica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'SpecificCurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'SpecificCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que determina si liquida folio/servicio en moneda específica (1=Sí, 0=No, sin moneda específica).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateFolioInSpecificCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece 0 - No liquida el folio en una moneda en especifico, 1 - Liquida el folio en una moneda especifica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateFolioInSpecificCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateFolioInSpecificCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que controla la generación automática de pagaré (documento de pago diferido) para cobros pendientes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'GeneratePromissoryNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genera pagaré', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'GeneratePromissoryNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'GeneratePromissoryNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que especifica si contabiliza descuentos comerciales no condicionados (0=No, 1=Sí) en comprobantes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountsConditionalCommercialDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica 0 - No se contabiliza descuento comercial No Condicionado, 1 - Si se contabiliza descuento comercial No Condicionado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountsConditionalCommercialDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountsConditionalCommercialDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que permite o bloquea la participación de ejecutivo de ventas y proveedor en transacciones (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AllowsSalesExecutiveAndSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica 0 - No permite ejecutivo en ventas y proveedor, 1 - Si permite ejecutivo en ventas y proveedor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AllowsSalesExecutiveAndSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AllowsSalesExecutiveAndSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de autorización (INT FK) para gestionar copagos y cuotas de recuperación en facturas de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationCopayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autorizaciones para copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationCopayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationCopayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar reversión de facturas genéricas/básicas de ejercicios fiscales anteriores.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalPreviousYearsGenericBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable de reversion   de facturas genericas (ej: fac basica) de vigencias anteriores (años anteriores)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalPreviousYearsGenericBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalPreviousYearsGenericBillingMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para reversión de facturas de servicios de salud de vigencias anteriores (años atrás).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalPreviousYearsMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable de reversion   de factura Salud de vigencias anteriores (años anteriores)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalPreviousYearsMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalPreviousYearsMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite máximo de líneas/ítems por factura (INT, 0=sin límite); controla granularidad de desglose.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'MaxInvoiceItems';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' límite máximo de líneas o ítems que puede tener una factura,si esta en 0 es sin limite', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'MaxInvoiceItems';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'MaxInvoiceItems';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que indica si usa Tasa Representativa del Mercado (TRM) custom diferente de TRM oficial en liquidación de servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HasCustomTRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el modulo de facturacion tiene una TRM diferente al momento de facturar, los TRM se especifican en Billing.CustomTRM    Por ahora solo se tomaria para Facturacion en liquidacion de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HasCustomTRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HasCustomTRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que especifica si maneja múltiples tarifas/valores para mismo servicio según contrato/plan (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HandlesDifferentRates';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica 0 - No se maneja diferentes tarifas, 1 - Maneja diferentes tarifas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HandlesDifferentRates';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HandlesDifferentRates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de ajuste de inventario para regalos/muestras (1=Entrada, 2=Salida) en Inventory.AdjustmentConcept.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'GiftProductOutletConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'almacena el tipo de concepto (1 - Entrada 2 - Salida) de la tabla de Inventory.AdjustmentConcept', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'GiftProductOutletConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'GiftProductOutletConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que determina si liquida/contabiliza contra cuenta madre/padre (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateMasterAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- si liquida cuenta madre , 0- no liquida cuenta madre', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateMasterAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateMasterAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar variaciones proyectadas de precios en comprobantes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProjectedVariationPriceMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable variación de precio proyectado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProjectedVariationPriceMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProjectedVariationPriceMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para contabilizar ingresos por paquetes/bundles de servicios o productos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingPackageMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable para contabilizar paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingPackageMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingPackageMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para reversar/anular paquetes ya liquidados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversionLiquidatedPackageJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante reversión paquetes liquidados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversionLiquidatedPackageJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversionLiquidatedPackageJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para liquidar paquetes de servicios/productos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidatedPackageJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante paquetes liquidados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidatedPackageJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidatedPackageJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que activa/desactiva contabilización separada de paquetes en registro contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contabiliza paquetes', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de comprobante (INT FK) para reversar reconocimiento de ingresos en ventas en consignación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalRecognitionConsignmentSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el reconocimiento de reversión de venta en consignación que se parametriza.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalRecognitionConsignmentSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReversalRecognitionConsignmentSale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de comprobante (INT FK) para reconocer ingresos en ventas en consignación (mercancía en poder de tercero).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ConsignmentSalereCognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el reconocimiento de ventas en consignación parametrizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ConsignmentSalereCognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ConsignmentSalereCognition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de bloqueo aplicado a ingresos (TINYINT): controla si ingresos quedan bloqueados hasta cumplir condición.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IncomeLockType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el tipo de bloqueo de ingreso parametrizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IncomeLockType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IncomeLockType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Credencial JWT (VARCHAR 100) proporcionada por API Okorum Technologies para autenticación y autorización en integraciones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientSecret';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relaciona el ClientSecret (Seguridad JWT) proporcionado por la Api expuesta Okorum technologies', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientSecret';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientSecret';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cliente/aplicación (VARCHAR 100) JWT desde API Okorum Technologies para integraciones externas seguras.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientIdName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relaciona el ClientID (Seguridad JWT) proporcionado por la Api expuesta Okorum technologies', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientIdName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientIdName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que habilita/deshabilita integración con MiPres (sistema de prescripciones en Colombia).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IntegrationMiPres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite seleccionar si se desea integrar con MiPres', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IntegrationMiPres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IntegrationMiPres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que activa validaciones en control de cuentas hospitalario (0=No valida, 1=Sí valida) antes de facturar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountControlValidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se parametriza 0 - No realiza validación en control de cuentas hospitalario, 1 - Si realiza validación en control de cuentas hospitalario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountControlValidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountControlValidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que valida integridad y estado de empaque/embalaje antes de facturación de productos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ValidatePackaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica 0 - No valida paquete, 1 - Valida paquete.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ValidatePackaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ValidatePackaging';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que controla/registra números de autorización de facturas o servicios (preautorizaciones).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AuthorizationNumberControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si Controla el Registro de Numero de Autorización', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AuthorizationNumberControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AuthorizationNumberControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de estado (INT FK) asignado por defecto a folios/servicios cerrados o liquidados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'StatusFolioClosedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'eSTADO DE FOLIO Cerrado POR DEFECTO', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'StatusFolioClosedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'StatusFolioClosedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de estado (INT FK) asignado por defecto a folios/servicios recién creados o abiertos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'StatusFolioNewId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'eSTADO DE FOLIO NUEVO POR DEFECTO', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'StatusFolioNewId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'StatusFolioNewId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cálculo de anticipos de impuestos (TINYINT: 0=No, 1=Solo Informar, 2=Reconocer contablemente).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CalculateTaxAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si se calculará Anticipos de Impuestos:  0 - No  1 - Solo Informar  2 - Reconocer', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CalculateTaxAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CalculateTaxAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de nota contable (INT FK) para devoluciones parciales de factura de productos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceProductDevolutionPartialConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de nota para la devolución parcial de factura de productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceProductDevolutionPartialConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceProductDevolutionPartialConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Entidad Administradora (INT FK) por defecto para facturas a pacientes particulares/privados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ParticularHealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entidad Administradora por defecto para particulares', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ParticularHealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ParticularHealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para anulación de facturas básicas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingAnnulmentJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante anulación factura básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingAnnulmentJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingAnnulmentJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de presupuesto de ingresos (INT FK) usado en reconocimiento de facturas básicas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de ingresos a usar en el reconocimiento de facturas básicas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de dependencia/unidad funcional (INT FK) para reconocimiento presupuestal de facturas básicas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingDependencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dependencia con la cual se realizará el reconocimiento en presupuesto de la factura básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingDependencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingDependencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de dependencia/unidad funcional (INT FK) para reconocimiento presupuestal de liquidaciones de servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'DependencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dependencia con la cual se realizará el reconocimiento en presupuesto de las liquidaciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'DependencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'DependencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que activa interfaz/sincronización con módulo de presupuestos en reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite identificar si se realiza interfaz con presupuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BudgetInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que permite anular facturas de periodos/ejercicios fiscales anteriores (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AnulateInvoicesPreviousPeriods';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite anular facturas de periodos anteriores', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AnulateInvoicesPreviousPeriods';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AnulateInvoicesPreviousPeriods';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de recibo de caja (INT FK) para cuadre con Facturación Básica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Recibo de Caja para cruzar con Facturación Básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingCashReceiptConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante (INT FK) para reversar distribución de ingresos de factura a entidad capitada/monto fijo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante para la Reversión Distribución de Ingresos de Factura Monto Fijo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante (INT FK) para distribuir ingresos de factura a entidad capitada por servicios o controles.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedDistributionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante para Distribución de Ingresos de Factura Monto Fijo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedDistributionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedDistributionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina contabilización de distribución de controles de capitación: 1=Cuenta ingreso capitación, 2=Cuenta asociada al registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'DistributeCapitationControls';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si la distribución de los control de capitación se contabiliza usando:  1. La cuenta contable de ingreso por capitación   2. La cuenta contable asociada a los registros de control', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'DistributeCapitationControls';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'DistributeCapitationControls';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que permite liquidar/facturar servicios ambulatorios directamente desde registro de control (atención).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateSinceControlOutPatientService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si permite liquidar desde control de servicios ambulatorios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateSinceControlOutPatientService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'LiquidateSinceControlOutPatientService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de recibo de caja (INT FK) para cuota de recuperación en controles de capitación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitedPatientAdvanceCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Recibo de Caja para el recaudo de la cuota de recuperacion para control de capitación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitedPatientAdvanceCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitedPatientAdvanceCashReceiptConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para registro de Facturación Básica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Comprobante para Facturación Básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BasicBillingJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de retención en la fuente sobre IVA (INT FK) para comprobantes de venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteIVAConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Retención de IVA en Ventas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteIVAConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteIVAConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de salud/médico (VARCHAR 20) parametrizado para dispensación Medilaser en FarmaQx.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del médico, se habilita cuando el nit de la compañia sea de FarmaQx y sirve para cuando se realice una dispensación por paciente medilaser se facture con este médico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad funcional/departamento (INT FK) parametrizado para dispensación Medilaser en FarmaQx.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional, se habilita cuando el nit de la compañia sea de FarmaQx y sirve para cuando se realice una dispensación por paciente medilaser se facture con esta unidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica origen de centro de costo en comprobantes: 1=Unidad Funcional, 2=Grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AssociateCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se va tomar el centro de costo para cuando se hacen los comprobantes contables    1 - Costo (Unidad Funcional)  2 - Costo (Grupo)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AssociateCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AssociateCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar retención en la fuente sobre ingresos laborales (Rte Fuente).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteFuenteMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable reteFuente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteFuenteMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteFuenteMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar retención de Impuesto de Industria y Comercio (Rte ICA).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteICAMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable reteICA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteICAMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteICAMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar retención sobre Impuesto al Valor Agregado (Rte IVA).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteIVAMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable reteIVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteIVAMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReteIVAMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar pasivo de Impuesto al Valor Agregado por pagar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IVAPaymentMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable iva por pagar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IVAPaymentMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IVAPaymentMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar deudores/cuentas por cobrar de clientes (pacientes/entidades).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable cliente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ClientMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que habilita/desactiva Facturación Básica para la unidad operativa (1=Aplica, 0=No aplica).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ApplyBasicBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si aplica para facturación básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ApplyBasicBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ApplyBasicBilling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante (INT FK) para reversar/contracuentar reconocimiento de ingresos en servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseRecognitionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante para reversion de reconocimiento de ingresos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseRecognitionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseRecognitionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para reconocimiento de ingresos en servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecognitionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante para Reconocimiento de Ingresos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecognitionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecognitionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal/versión (TIMESTAMP) registrada automáticamente por SQL Server en creación, modificación o evento clave del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de última modificación del registro de configuración.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login (VARCHAR 20) que realizó última modificación; permite auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación/inserción inicial del registro de configuración.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login (VARCHAR 20) que creó el registro; rastro de auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de permiso de acceso para categorías de facturación (TINYINT: 1=Por Usuario, 2=Por Grupo Atención, 3=Por Grupo+Usuario).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PermissionCategories';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Permiso para las categorias de facturacion  1 - Permiso de Usuario  2 - Permiso de Grupo de Atencion  3 - Permiso de Grupo de Atencion y Usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PermissionCategories';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PermissionCategories';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo (INT FK) asociado a cuenta contable de descuentos en cuota de recuperación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecoveryFeeDiscountCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de costo de la cuenta contable de descuento a paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecoveryFeeDiscountCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecoveryFeeDiscountCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar descuentos aplicados a cuota de recuperación de pacientes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecoveryFeeDiscountMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable para el descuento a cuota de recuperacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecoveryFeeDiscountMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RecoveryFeeDiscountMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo (INT FK) asociado a ingresos por venta de productos farmacéuticos/médicos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de Costo para cuenta contable de venta de productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de recibo de caja (INT FK) para cuadre de factura de venta de productos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Recibo de Caja para pagar la factura de venta de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesCashReceiptConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar ingresos por facturación de venta de productos/farmacoterapia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable Ingreso por factura de venta de productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductSalesMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contabilización de procedimientos quirúrgicos (TINYINT: 1=Por Procedimiento, 2=Detallada por componentes).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingForSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la contabilizacion de quirurgicos   1 - Por Procedimiento  2 - Detallada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingForSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingForSurgical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de autorización (INT FK) para facturas dirigidas a entidades capitadas/planes prepagados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'EntityCapitatedBillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la autorizacion que se debe utilizar cuando se realizen Facturas a entidades Capitadas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'EntityCapitatedBillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'EntityCapitatedBillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que requiere permiso explícito de usuario para crear pagaré/cuenta por cobrar a paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RequiresPermissionForCxCPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que se requiere un permiso de usuario para poder crear una cuenta por cobrar a un paciente (Pagare)    Este evento ocurre cuando el valor que le corresponde al paciente no puede ser pagado en su totalidad entonces se deberia poder crear un pagare', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RequiresPermissionForCxCPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RequiresPermissionForCxCPatient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador secuencial (BIGINT) de controles de capitación; se bloquea al tener registros en Invoice con DocumentType=5.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ConsecutiveControlCapitation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Consecutivo de control de capitacion, Este campo se bloquea y no puede ser modificado por el usuario cuando haya almenos un registro de control de capitacion en las tablas de factura (Invoice) Campo (DocumentType=5)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ConsecutiveControlCapitation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ConsecutiveControlCapitation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo alfabético (VARCHAR 4) para numeración de documentos de capitación (ej: ''''CAP_'''').', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PrefixConsecutiveCapitation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el prefijo del documento de capitacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PrefixConsecutiveCapitation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PrefixConsecutiveCapitation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de recibo de caja (INT FK) para cobros de particulares/pacientes privados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IndvidualAdvanceCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Recibo de Caja para la liquidacion correspondiente al recaudo de los valores facturados al particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IndvidualAdvanceCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IndvidualAdvanceCashReceiptConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de recibo de caja (INT FK) para cobros de cuota de recuperación a pacientes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PatientAdvanceCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Recibo de Caja para la liquidacion correspondiente al recaudo de la cuota de recuperacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PatientAdvanceCashReceiptConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'PatientAdvanceCashReceiptConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar pérdidas o déficit generados en contratos de capitación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationLossMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable para el registro de la perdida por Capitacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationLossMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationLossMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar ganancias o superávit generados en contratos de capitación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationProfitMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable para el registro de la utilidad por Capitacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationProfitMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationProfitMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable (INT FK) para registrar ingresos por facturas de capitación (monto fijo).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationRevenueMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable para el registro de la Factura por Capitacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationRevenueMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CapitationRevenueMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de redondeo para cálculo de cuota de recuperación (INT: 1=Peso, 10=Decena, 100=Centena, 1000=Millar).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RoundingTypeRecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Redondeo para la Cuota de Recuperacion  1= Peso  10 - Decena  100 - Centena  1000 - Milesima', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RoundingTypeRecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'RoundingTypeRecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que determina si requiere confirmación/cierre de recibos de caja antes de contabilizar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CashReceiptsConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se confirma los recibos de caja', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CashReceiptsConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'CashReceiptsConfirm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para facturación de productos farmacéuticos/suministros médicos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductInvoiceJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante para facturacion de productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductInvoiceJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ProductInvoiceJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para reversar traslados o transferencias entre centros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseTransferJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante para la reversión de traslado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseTransferJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ReverseTransferJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para anulación de facturas de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceAnnulmentJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante para anulacion de facturas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceAnnulmentJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceAnnulmentJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable (INT FK) para facturas de servicios de salud (documento base).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante para facturas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'InvoiceJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad operativa/sede (INT FK) a la que pertenecen estas configuraciones de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY) del registro de configuración de facturación de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración del módulo de facturación por unidad operativa. Define los parámetros contables, tipos de comprobantes, cuentas contables, conceptos de recaudo, reglas de autorización y opciones generales que controlan el comportamiento del proceso de facturación, cartera y liquidación en el ERP.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se debe validar que el paciente sea mayor de edad al momento de facturar o realizar una venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ValidateAgeOfMajority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ValidateAgeOfMajority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se debe generar tiquete electrónico de venta (documento equivalente electrónico) en lugar de factura tradicional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ApplyElectronicSalesTicket';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'ApplyElectronicSalesTicket';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable utilizado para registrar reversiones o anulaciones en la contabilidad general.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingVoucherReversalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingVoucherReversalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante contable utilizado para la generación o causación de documentos en la contabilidad general.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingVoucherGenerationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'AccountingVoucherGenerationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al esquema o perfil de autorización general aplicado al proceso de facturación de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'SettingsBilling', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';

GO
CREATE NONCLUSTERED INDEX [IX_SettingsBilling_IdOperatingUnit]
    ON [Billing].[SettingsBilling]([IdOperatingUnit] ASC);
