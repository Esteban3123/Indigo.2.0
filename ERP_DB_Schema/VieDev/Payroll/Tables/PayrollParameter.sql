CREATE TABLE [Payroll].[PayrollParameter] (
    [Id]                                     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LegalSalaryMinimum]                     NUMERIC (18)   NOT NULL,
    [InstitutionalMinimumSalary]             NUMERIC (18)   NOT NULL,
    [TransportHelpValue]                     NUMERIC (18)   NOT NULL,
    [EmployeePensionContributionPercentage]  NUMERIC (6, 3) NOT NULL,
    [EmployeeHealthContributionPercentage]   NUMERIC (6, 3) NOT NULL,
    [EmployerPensionContributionPercentage]  NUMERIC (6, 3) NOT NULL,
    [EmployerHealthContributionPercentage]   NUMERIC (6, 3) NOT NULL,
    [HealthContributionMaximunSalary]        TINYINT        NOT NULL,
    [RTFExemptPercentage]                    NUMERIC (6, 3) NOT NULL,
    [UVTValue]                               NUMERIC (18)   NULL,
    [LiquidateVacation]                      BIT            NOT NULL,
    [MaxVacationByYear]                      INT            CONSTRAINT [DF_PayrollParameter_MaxVacationByYear] DEFAULT ((1)) NOT NULL,
    [VacationDays]                           TINYINT        CONSTRAINT [DF_PayrollParameter_VacationDays] DEFAULT ((15)) NOT NULL,
    [VacationAdvanced]                       BIT            CONSTRAINT [DF_PayrollParameter_VacationAdvanced] DEFAULT ((1)) NOT NULL,
    [MaxPremiumByYear]                       INT            CONSTRAINT [DF_PayrollParameter_MaxPremiumByYear] DEFAULT ((2)) NULL,
    [MaximunDiscountPercentage]              NUMERIC (6, 3) CONSTRAINT [DF_PayrollParameter_MaximunDiscountPercentage] DEFAULT ((100)) NOT NULL,
    [EducationHealthDiscountPercentage]      NUMERIC (6, 3) NOT NULL,
    [HousingDeductionMaximumValue]           NUMERIC (18)   NOT NULL,
    [AverageMonthVacation]                   TINYINT        CONSTRAINT [DF_PayrollParameter_AverageMonthVacation] DEFAULT ((6)) NOT NULL,
    [SaturdayBusinessDay]                    BIT            NOT NULL,
    [SundayBusinessDay]                      BIT            NOT NULL,
    [SenaContributionPercentage]             NUMERIC (6, 3) NOT NULL,
    [ICBFContributionPercentage]             NUMERIC (6, 3) NOT NULL,
    [CompensationFundContributionPercentage] NUMERIC (6, 3) NOT NULL,
    [InitialTimeOrdinaryDay]                 TIME (7)       NOT NULL,
    [EndTimeOrdinaryDay]                     TIME (7)       NOT NULL,
    [AproximationValue]                      INT            NULL,
    [InterfaceName]                          VARCHAR (50)   NULL,
    [PayrollVoucherCode]                     VARCHAR (10)   NULL,
    [PayrollVoucherName]                     VARCHAR (50)   NULL,
    [ProvisionVoucherCode]                   VARCHAR (10)   NULL,
    [ProvisionVoucherName]                   VARCHAR (50)   NULL,
    [PrestacionVoucherCode]                  VARCHAR (10)   NULL,
    [PrestacionVoucherName]                  VARCHAR (50)   NULL,
    [PayrollAccount]                         VARCHAR (20)   NULL,
    [AccountedBy]                            CHAR (1)       NULL,
    [Day31]                                  BIT            NULL,
    [AverageMonthVacationCompensation]       TINYINT        NULL,
    [IdPayrollVoucherType]                   INT            NULL,
    [IdProvisionVoucherType]                 INT            NULL,
    [IdPrestacionVoucherType]                INT            NULL,
    [IdPayrollAccount]                       INT            NULL,
    [LiquidationContractAccount]             VARCHAR (20)   NULL,
    [IdLiquidationContractAccount]           INT            NULL,
    [IdLiquidationVacationAccount]           INT            NULL,
    [LiquidationVacationAccount]             VARCHAR (20)   NULL,
    [IdIncentiveVoucherType]                 INT            NULL,
    [IdContractLiquidationVoucherType]       INT            NULL,
    [IdUnemploymentVoucherType]              INT            NULL,
    [LiquidaMinimumInstitutionalSalary]      BIT            CONSTRAINT [DF__PayrollPa__Liqui__376C7569] DEFAULT ((0)) NOT NULL,
    [SpecialPensionRateIndicator]            TINYINT        CONSTRAINT [DF_PayrollParameter_SpecialPensionRateIndicator] DEFAULT ((0)) NOT NULL,
    [IdIncentivePaymentConcept1]             INT            NULL,
    [IncentivePaymentFormula1]               VARCHAR (MAX)  NULL,
    [StartDateIncentivePayment1]             DATETIME       NULL,
    [EndDateIncentivePayment1]               DATETIME       NULL,
    [IdIncentivePaymentConcept2]             INT            NULL,
    [IncentivePaymentFormula2]               VARCHAR (MAX)  NULL,
    [StartDateIncentivePayment2]             DATETIME       NULL,
    [EndDateIncentivePayment2]               DATETIME       NULL,
    [LimitSMMLContributive]                  DECIMAL (5, 2) NULL,
    CONSTRAINT [PK_Payroll Parameter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PayrollParameter_Concept1] FOREIGN KEY ([IdIncentivePaymentConcept1]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollParameter_Concept2] FOREIGN KEY ([IdIncentivePaymentConcept2]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes] FOREIGN KEY ([IdPayrollVoucherType]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes1] FOREIGN KEY ([IdProvisionVoucherType]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes2] FOREIGN KEY ([IdPrestacionVoucherType]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes3] FOREIGN KEY ([IdIncentiveVoucherType]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes4] FOREIGN KEY ([IdContractLiquidationVoucherType]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes5] FOREIGN KEY ([IdUnemploymentVoucherType]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_PayrollParameter_MainAccounts] FOREIGN KEY ([IdPayrollAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PayrollParameter_MainAccounts1] FOREIGN KEY ([IdLiquidationContractAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PayrollParameter_MainAccounts2] FOREIGN KEY ([IdLiquidationVacationAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_Concept1];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_Concept2];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes1];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes2];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes3];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes4];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_JournalVoucherTypes5];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_MainAccounts];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_MainAccounts1];


GO
ALTER TABLE [Payroll].[PayrollParameter] NOCHECK CONSTRAINT [FK_PayrollParameter_MainAccounts2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Fin de Prima 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndDateIncentivePayment2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Inicio de Prima 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'StartDateIncentivePayment2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Prima 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IncentivePaymentFormula2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del Concepto de Prima 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentivePaymentConcept2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Fin de Prima 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndDateIncentivePayment1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Inicio de Prima 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'StartDateIncentivePayment1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula de Prima 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IncentivePaymentFormula1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del Concepto de Prima 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentivePaymentConcept1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica la tarifa especial de pension así:  0. Sin riesgo  1. Actividades de alto riesgo   2. Senadores   3. CTI   4. Aviadores', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SpecialPensionRateIndicator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de contrato de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationContractAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor UVT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'UVTValue';

