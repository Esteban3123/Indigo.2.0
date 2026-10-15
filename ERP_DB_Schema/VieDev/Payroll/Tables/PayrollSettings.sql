CREATE TABLE [Payroll].[PayrollSettings] (
    [Id]                                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CreeTax]                                       BIT           NOT NULL,
    [VacationFormulates]                            VARCHAR (MAX) NULL,
    [BonificationVacationFormulates]                VARCHAR (MAX) NULL,
    [VacationIncentiveFormulates]                   VARCHAR (MAX) NULL,
    [VacationalIncreaseFormulates]                  VARCHAR (MAX) NULL,
    [PayrollChiefThirdPartyId]                      INT           NULL,
    [ServicesIncentivePaymentFormulates]            VARCHAR (MAX) NULL,
    [ChristmasIncentivePayment]                     VARCHAR (MAX) NULL,
    [InitialDateServicesIncentivePayment]           DATE          NULL,
    [EndDateServicesIncentivePayment]               DATE          NULL,
    [InitialDateChristmasIncentivePayment]          DATE          NULL,
    [EndDateChristmasIncentivePayment]              DATE          NULL,
    [RtfHealthContribution]                         TINYINT       CONSTRAINT [DF_PayrollSettings_RtfHealthContribution] DEFAULT ((1)) NULL,
    [WeekendVacationPaid]                           TINYINT       NULL,
    [ServicesIncentivePaymentConceptId]             INT           NULL,
    [ChristmasIncentivePaymentConceptId]            INT           NULL,
    [NoveltyMainAccountId]                          INT           NULL,
    [HolidayWithoutAnticipateIBC]                   BIT           CONSTRAINT [DF_PayrollSettings_HolidayWithoutAnticipateIBC] DEFAULT ((0)) NOT NULL,
    [HealthPensionIBCVacation]                      TINYINT       NULL,
    [ContractLiquidationYearBonification]           VARCHAR (MAX) NULL,
    [ContractLiquidationTransportValue]             VARCHAR (MAX) NULL,
    [ContractLiquidationFoodValue]                  VARCHAR (MAX) NULL,
    [ContractLiquidationVacation]                   VARCHAR (MAX) NULL,
    [ContractLiquidationIncentiveVacation]          VARCHAR (MAX) NULL,
    [ContractLiquidationEspecialBonification]       VARCHAR (MAX) NULL,
    [ContractLiquidationIncreaseVacational]         VARCHAR (MAX) NULL,
    [ContractLiquidationServiceIncentive]           VARCHAR (MAX) NULL,
    [ContractLiquidationChristmasIncentive]         VARCHAR (MAX) NULL,
    [PILAOperators]                                 TINYINT       NULL,
    [UnemploymentTypeCalculated]                    TINYINT       NULL,
    [IdRetentionConcepts]                           INT           NULL,
    [TransportHealthValueLastYear]                  NUMERIC (18)  NULL,
    [MinimunLegalSalaryLastYear]                    NUMERIC (18)  NULL,
    [IdConceptUnemployment]                         INT           NULL,
    [IdConceptInterestUnemployment]                 INT           NULL,
    [IdAccountReceivableConcept]                    INT           NULL,
    [IdCashRegister]                                INT           NULL,
    [IdEntityBankAccount]                           INT           NULL,
    [ExpenseType]                                   TINYINT       NULL,
    [PaymentMethod]                                 TINYINT       NULL,
    [ContractLiquidationUnemployment]               VARCHAR (MAX) NULL,
    [IdCashRegisterVacation]                        INT           NULL,
    [IdEntityBankAccountVacation]                   INT           NULL,
    [ExpenseTypeVacation]                           TINYINT       NULL,
    [PaymentMethodVacation]                         TINYINT       NULL,
    [IdCashRegisterLiquidation]                     INT           NULL,
    [IdEntityBankAccountLiquidation]                INT           NULL,
    [ExpenseTypeLiquidation]                        TINYINT       NULL,
    [PaymentMethodLiquidation]                      TINYINT       NULL,
    [IdExpenseConceptsVacation]                     INT           NULL,
    [IdExpenseConceptsLiquidation]                  INT           NULL,
    [IdVacationConcept]                             INT           NULL,
    [InabilityAccountedBy]                          TINYINT       CONSTRAINT [DF_PayrollSettings_InabilityAccountedBy] DEFAULT ((1)) NOT NULL,
    [GenerateVoucherTransactionVacation]            BIT           CONSTRAINT [DF_PayrollSettings_GenerateVoucherTransactionVacation] DEFAULT ((0)) NOT NULL,
    [GenerateVoucherTransactionContractLiquidation] BIT           CONSTRAINT [DF_PayrollSettings_GenerateVoucherTransactionContractLiquidation] DEFAULT ((0)) NOT NULL,
    [RetentionFraction]                             BIT           CONSTRAINT [DF_PayrollSettings_RetentionFraction] DEFAULT ((0)) NOT NULL,
    [LiquidateContractByFormulate]                  BIT           CONSTRAINT [DF_PayrollSettings_LiquidateContractByFormulate] DEFAULT ((0)) NOT NULL,
    [IngressDelayMinutes]                           TINYINT       CONSTRAINT [DF_PayrollSettings_DelayMinutes] DEFAULT ((30)) NOT NULL,
    [NoteConceptsId]                                INT           NULL,
    [PortfolioNoteConceptId]                        INT           NULL,
    [CashFlowConceptId]                             INT           NULL,
    [ContributionClass]                             VARCHAR (1)   CONSTRAINT [DF__PayrollSe__Contr__56B01698] DEFAULT ('A') NOT NULL,
    [PayrollDistribution]                           TINYINT       CONSTRAINT [DF_PayrollSettings_PayrollDistribution] DEFAULT ((1)) NOT NULL,
    [LiquidatesSeverancePayments]                   BIT           CONSTRAINT [DF__PayrollSe__Liqui__39407A99] DEFAULT ((1)) NOT NULL,
    [CurrencyId]                                    INT           NULL,
    [SeveranceConceptId]                            INT           NULL,
    [SeveranceInterestConceptId]                    INT           NULL,
    [PreNoticeSettlementConceptId]                  INT           NULL,
    [BonusSettlementConceptId]                      INT           NULL,
    [VacationSettlementConceptId]                   INT           NULL,
    [SeveranceInterestFormula]                      VARCHAR (MAX) NULL,
    [PreNoticeFormula]                              VARCHAR (MAX) NULL,
    [WithholdingBiweeklyDiscount]                   BIT           CONSTRAINT [DF_PayrollSettings_WithholdingBiweeklyDiscount] DEFAULT ((0)) NOT NULL,
    [WithholdingTaxRefundConceptId]                 INT           NULL,
    [HandlesLiquidationFirstTwoDays]                BIT           CONSTRAINT [DF_PayrollSettings_HasLiquidationPatrono] DEFAULT ((0)) NOT NULL,
    [FirstTwoDaysFormulate]                         VARCHAR (MAX) NULL,
    [IsFullSalaryBenefitFactor]                     BIT           CONSTRAINT [DF_PayrollSettings_IsFullSalaryBenefitFactor] DEFAULT ((0)) NULL,
    [ConceptBenefitFactortId]                       INT           NULL,
    CONSTRAINT [PK_PayrollSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PayrollSettings_AccountReceivableConcept] FOREIGN KEY ([IdAccountReceivableConcept]) REFERENCES [Portfolio].[AccountReceivableConcept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_BonusSettlementConcept] FOREIGN KEY ([BonusSettlementConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_CashFlowConcept] FOREIGN KEY ([CashFlowConceptId]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_CashRegisters] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_PayrollSettings_CashRegisters1] FOREIGN KEY ([IdCashRegisterVacation]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_PayrollSettings_CashRegisters2] FOREIGN KEY ([IdCashRegisterLiquidation]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_PayrollSettings_Concept] FOREIGN KEY ([ServicesIncentivePaymentConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_Concept1] FOREIGN KEY ([ChristmasIncentivePaymentConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_Concept2] FOREIGN KEY ([IdConceptUnemployment]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_Concept3] FOREIGN KEY ([IdConceptInterestUnemployment]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_Concept4] FOREIGN KEY ([IdVacationConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_ConceptBenefitFactorId] FOREIGN KEY ([ConceptBenefitFactortId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_ConceptBenefitFactortId] FOREIGN KEY ([ConceptBenefitFactortId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PayrollSettings_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_PayrollSettings_EntityBankAccounts1] FOREIGN KEY ([IdEntityBankAccountVacation]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_PayrollSettings_EntityBankAccounts2] FOREIGN KEY ([IdEntityBankAccountLiquidation]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_PayrollSettings_ExpenseConcepts] FOREIGN KEY ([IdExpenseConceptsLiquidation]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_PayrollSettings_ExpenseConcepts1] FOREIGN KEY ([IdExpenseConceptsVacation]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_PayrollSettings_MainAccounts] FOREIGN KEY ([NoveltyMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PayrollSettings_NoteConcepts] FOREIGN KEY ([NoteConceptsId]) REFERENCES [Treasury].[NoteConcepts] ([Id]),
    CONSTRAINT [FK_PayrollSettings_PortfolioNoteConcept] FOREIGN KEY ([PortfolioNoteConceptId]) REFERENCES [Portfolio].[PortfolioNoteConcept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_PreNoticeSettlementConcept] FOREIGN KEY ([PreNoticeSettlementConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_RetentionConcepts] FOREIGN KEY ([IdRetentionConcepts]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_PayrollSettings_SeveranceConcept] FOREIGN KEY ([SeveranceConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_SeveranceInterestConcept] FOREIGN KEY ([SeveranceInterestConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_ThirdParty] FOREIGN KEY ([PayrollChiefThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PayrollSettings_VacationSettlementConcept] FOREIGN KEY ([VacationSettlementConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollSettings_WithholdingTaxRefundConcept] FOREIGN KEY ([WithholdingTaxRefundConceptId]) REFERENCES [Payroll].[Concept] ([Id])
);


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_AccountReceivableConcept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_BonusSettlementConcept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_CashFlowConcept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_CashRegisters];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_CashRegisters1];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_CashRegisters2];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_Concept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_Concept1];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_Concept2];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_Concept3];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_Concept4];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_Currency];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_EntityBankAccounts];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_EntityBankAccounts1];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_EntityBankAccounts2];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_ExpenseConcepts];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_ExpenseConcepts1];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_MainAccounts];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_NoteConcepts];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_PortfolioNoteConcept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_PreNoticeSettlementConcept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_RetentionConcepts];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_SeveranceConcept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_SeveranceInterestConcept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_ThirdParty];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_VacationSettlementConcept];


GO
ALTER TABLE [Payroll].[PayrollSettings] NOCHECK CONSTRAINT [FK_PayrollSettings_WithholdingTaxRefundConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto para incapacidades con salario integral ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ConceptBenefitFactortId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'30% Factor prestacional-Incapacidad Salario Integral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IsFullSalaryBenefitFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maneja cáculo liquidación para los dos primeros días', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'HandlesLiquidationFirstTwoDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto devolución retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'WithholdingTaxRefundConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retención del descuento quincenal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'WithholdingBiweeklyDiscount';

