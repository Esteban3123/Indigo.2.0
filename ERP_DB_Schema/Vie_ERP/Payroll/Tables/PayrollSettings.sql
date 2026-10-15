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
    [AllowedMonthsForExtemporaneousAdjustments]     TINYINT       CONSTRAINT [DF_PayrollSettings_AllowedMonthsForExtemporaneousAdjustments]  DEFAULT ((0)) NOT NULL,
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
EXECUTE sp_addextendedproperty @name=N'MS_Description', @value=N'Meses permitidos para ajustes extemporáneos de cargo y salario básico. Rango: 0 a 6 meses hacia atrás.' , @level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'PayrollSettings', @level2type=N'COLUMN',@level2name=N'AllowedMonthsForExtemporaneousAdjustments';

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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula Preaviso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PreNoticeFormula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula intereses de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'SeveranceInterestFormula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto Liq Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'VacationSettlementConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto Liq Prima', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'BonusSettlementConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto Liq preaviso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PreNoticeSettlementConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto Liq Intereses cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'SeveranceInterestConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de liquidación de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'SeveranceConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro que indica la moneda', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parámetro que valida si liquida o no cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'LiquidatesSeverancePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distribución de nómina por:   1 - Asignada al contrato   2 - Costeo por cuadro de turnos    Prorrateo por unidad funcional del empleado en el contrato o por cuadro de turnos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PayrollDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Aportes:       A. Aportante con 200 o más cotizantes    B. Aportante con menos de 200 cotizantes    C. Aportante Mipyme que se acoge a Ley 590 de 2000    D. Aportante beneficiario del artículo 5° de la Ley 1429 de 2010    I. Independiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContributionClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de CashFlowConcept de adm efectivo que se usará para relacionar el concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'CashFlowConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de PortfolioNoteConcept  que se que se usará para relacionar el concepto nota de Cxc', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PortfolioNoteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de NoteConcepts de adm efectivo que se que se usará para relacionar el concepto nota adm efectivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'NoteConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Minutos de retraso de entrada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IngressDelayMinutes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidar Contratos por Fórmula (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'LiquidateContractByFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite si el sitema liquida la Retención por Fracción o no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'RetentionFraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Genera Comprobante de Egreso en Liquidación de Contrato (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'GenerateVoucherTransactionContractLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Genera Comprobante de Egreso en Vacaciones (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'GenerateVoucherTransactionVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el tercero usado para la contabilización de las incapacidades:  1 - Empleado  2 - Tercero Entidad Prestacion  ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'InabilityAccountedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Vacaciones con el cual se hará el comprobante contable (Aplica para Vacaciones con Pago Inmediato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdVacationConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de Egreso para el Comprobante de Egreso de las Liquidaciones de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdExpenseConceptsLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de Egreso para el Comprobante de Egreso de las Vacaciones con Pago Inmediato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdExpenseConceptsVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Método de pago (Liquidación de Contrato) Nota débito = 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PaymentMethodLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Egreso (Liquidación de Contrato): 1-Afecta Banco; 2- Afecta Caja Menor; 3 - Afecta Caja Mayor', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ExpenseTypeLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria de la entidad para Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccountLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Caja para Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdCashRegisterLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Método de pago (Vacaciones con Pago Inmediato) Nota débito = 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PaymentMethodVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Egreso (Vacaciones Pago Inmediato): 1-Afecta Banco; 2- Afecta Caja Menor; 3 - Afecta Caja Mayor', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ExpenseTypeVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria de la entidad para Vacaciones con Pago Inmediato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccountVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Caja para Vacaciones con Pago Inmediato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdCashRegisterVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Cesantias para Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationUnemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Método de pago Nota débito = 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Egreso: 1-Afecta Banco; 2- Afecta Caja Menor; 3 - Afecta Caja Mayor', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ExpenseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria de la entidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Caja', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Concepto de Cuentas x Cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdAccountReceivableConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto Intereses de Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdConceptInterestUnemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto de Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdConceptUnemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario mínimo legal el año pasado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'MinimunLegalSalaryLastYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor sanitario del transporte el año pasado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'TransportHealthValueLastYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Retention Concept', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'IdRetentionConcepts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Forma de Cálculo de Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'UnemploymentTypeCalculated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '83 - Mi Planilla  84 - Aportes en Línea  86 - Asopagos  87 - Fedecajas  88 - Simple  89 - Arus  ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PILAOperators';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Prima de Navidad (Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationChristmasIncentive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Prima de Servicios (Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationServiceIncentive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Incremento Vacacional(Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationIncreaseVacational';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Bonificación Especial para Recreación(Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationEspecialBonification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Prima de Vacaciones (Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationIncentiveVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Vacaciones (Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Auxilio de Alimentos (Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationFoodValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Auxilio de Transporte (Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationTransportValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Bonificación por Año de Servicio (Liquidación de Contrato)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ContractLiquidationYearBonification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se utiliza para saber si el aporte de Salud y Pensión en Vacaciones utiliza el Salario del Contrato o el IBC del Mes Anterior:   1. Sueldo Contrato  2. IBC Mes Anterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'HealthPensionIBCVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la entidad paga las vacaciones adelantadas pero no se afecta el ibc reportado a la pila  1 - Si  0 - No    - En el caso de ser si, este afecta el formulario de vacaciones ya que se solicita la fecha inicial real de vacaciones y fecha final real de vacaciones y estas fechas reales son utilizadas para saber la pila en que fecha va a reportar las vacaciones  - En caso de ser No, no se solicitan las fechas reales en vacaciones y la pila reporta la novedad en la fecha en que se solicitan', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'HolidayWithoutAnticipateIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuentas x Cobrar Incapacidades', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'NoveltyMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Nómina para el Pago de las Primas de Diciembre - Navidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ChristmasIncentivePaymentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Nómina para el Pago de las Primas de Junio - Servicios', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ServicesIncentivePaymentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Se paga hasta el último día Hábil (Viernes o sábado)  2. Se paga hasta el último día previo a ingresar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'WeekendVacationPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aporte a Salud para RETENCIÓN EN LA FUENTE  1. Toma el Aporte de Salud del Mes en curso  2. Toma el promedio de Aportes de Salud del Año anterior. Si el empleado no tiene datos, toma los que están parametrizados en Talento Humano', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'RtfHealthContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Fin del Periodo de Prima de Navidad/Diciembre (Usado para que el sistema sepa la Fecha de Fin para la búsqueda de los acumulados, y del periodo mismo de la prima)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'EndDateChristmasIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Inicio del Periodo de Prima de Navidad/Diciembre (Usado para que el sistema sepa la Fecha de Inicio para la búsqueda de los acumulados , y del periodo mismo de la prima)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'InitialDateChristmasIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Fin del Periodo de Prima de Servicios/Junio (Usado para que el sistema sepa la Fecha de Fin para la búsqueda de los acumulados, y del periodo mismo de la prima)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'EndDateServicesIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Inicio del Periodo de Prima de Servicios/Junio (Usado para que el sistema sepa la Fecha de Inicio para la búsqueda de los acumulados , y del periodo mismo de la prima)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'InitialDateServicesIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prima de Navidad (Sector Público) o Prima de Diciembre (Sector Privado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ChristmasIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prima de Servicios (Sector Público) o Prima de Junio (Sector Privado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'ServicesIncentivePaymentFormulates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del tercero que es el Jefe de Talento Humano', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'PayrollChiefThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula de Incremento Vacacional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'VacationalIncreaseFormulates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula de Prima de Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'VacationIncentiveFormulates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula de Bonificacion de Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'BonificationVacationFormulates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula de Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'VacationFormulates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica Impuesto del CREE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'CreeTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSettings', @level2type = N'COLUMN', @level2name = N'Id';

