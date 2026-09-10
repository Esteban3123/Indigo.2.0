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
    [VacationDiscretionaryDays]              BIT            CONSTRAINT [DF_PayrollParameter_VacationDiscretionaryDays] DEFAULT ((0)) NOT NULL,
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fin del segundo incentivo/prima. Tipo: DATETIME. Delimita período de pago segunda prima o bonificación especial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndDateIncentivePayment2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Fin de Prima 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndDateIncentivePayment2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndDateIncentivePayment2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del segundo incentivo/prima. Tipo: DATETIME. Marca comienzo de período para segunda prima o bonificación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'StartDateIncentivePayment2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inicio de Prima 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'StartDateIncentivePayment2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'StartDateIncentivePayment2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula de cálculo segunda prima/incentivo. Tipo: VARCHAR(MAX). Expresión matemática para liquidar segundo incentivo o bonificación especial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IncentivePaymentFormula2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fórmula de Prima 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IncentivePaymentFormula2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IncentivePaymentFormula2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del concepto nómina para segunda prima/incentivo. Tipo: INT. FK→[Payroll].[Concept]. Vincula segunda prima a concepto contable de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentivePaymentConcept2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Concepto de Prima 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentivePaymentConcept2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentivePaymentConcept2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fin del primer incentivo/prima. Tipo: DATETIME. Delimita período de pago primera prima o bonificación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndDateIncentivePayment1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Fin de Prima 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndDateIncentivePayment1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndDateIncentivePayment1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del primer incentivo/prima. Tipo: DATETIME. Marca comienzo período para primera prima o bonificación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'StartDateIncentivePayment1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inicio de Prima 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'StartDateIncentivePayment1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'StartDateIncentivePayment1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula de cálculo primera prima/incentivo. Tipo: VARCHAR(MAX). Expresión matemática para liquidar primer incentivo o bonificación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IncentivePaymentFormula1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fórmula de Prima 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IncentivePaymentFormula1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IncentivePaymentFormula1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del concepto nómina para primera prima/incentivo. Tipo: INT. FK→[Payroll].[Concept]. Vincula primera prima a concepto contable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentivePaymentConcept1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Concepto de Prima 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentivePaymentConcept1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentivePaymentConcept1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tarifa especial pensional. Tipo: TINYINT. Valores: 0=Sin riesgo, 1=Alto riesgo, 2=Senadores, 3=CTI, 4=Aviadores. Aplica cotización diferencial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SpecialPensionRateIndicator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica la tarifa especial de pension así:  0. Sin riesgo  1. Actividades de alto riesgo   2. Senadores   3. CTI   4. Aviadores', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SpecialPensionRateIndicator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SpecialPensionRateIndicator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidar salario mínimo institucional en nómina. Tipo: BIT. Valores: 1=Sí, 0=No. Determina si se usa SMMI en cálculos de prestaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidaMinimumInstitutionalSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquida Salario Mín Institucional SI=1 ó NO=0    ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidaMinimumInstitutionalSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidaMinimumInstitutionalSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID tipo comprobante de desempleo. Tipo: INT. FK→[GeneralLedger].[JournalVoucherTypes]. Comprobante contable para liquidación de cesantías y desempleo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdUnemploymentVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id. Tipo de comprobante de desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdUnemploymentVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdUnemploymentVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID tipo comprobante liquidación contrato. Tipo: INT. FK→[GeneralLedger].[JournalVoucherTypes]. Comprobante para liquidación final de contrato de trabajo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdContractLiquidationVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de comprobante de liquidación del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdContractLiquidationVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdContractLiquidationVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID tipo comprobante incentivos/primas. Tipo: INT. FK→[GeneralLedger].[JournalVoucherTypes]. Comprobante contable para registro de primas y bonificaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentiveVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IdIncentiveVoucherType', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentiveVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentiveVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable vacaciones liquidación. Tipo: VARCHAR(20). Cuenta Mayor para registrar vacaciones no disfrutadas en terminación de contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationVacationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de vacaciones de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationVacationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationVacationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID cuenta vacaciones liquidación. Tipo: INT. FK→[GeneralLedger].[MainAccounts]. Referencia contable para vacaciones compensadas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdLiquidationVacationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Liquidation Vacation Account', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdLiquidationVacationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdLiquidationVacationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID cuenta liquidación contrato. Tipo: INT. FK→[GeneralLedger].[MainAccounts]. Referencia contable para liquidación final de contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdLiquidationContractAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta de contrato de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdLiquidationContractAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdLiquidationContractAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable liquidación contrato. Tipo: VARCHAR(20). Cuenta Mayor para registro de indemnizaciones y liquidación de relación laboral.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationContractAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de contrato de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationContractAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationContractAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID cuenta contable nómina. Tipo: INT. FK→[GeneralLedger].[MainAccounts]. Cuenta Mayor para registro de nómina nativa. Solo Nómina Nativa.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPayrollAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta Contable de Nómina (Solo para Nómina Nativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPayrollAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPayrollAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID tipo comprobante prestaciones. Tipo: INT. FK→[GeneralLedger].[JournalVoucherTypes]. Comprobante contable para provisión de prestaciones sociales. Solo Nómina Nativa.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPrestacionVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tipo de Comprobante de Prestaciones (Solo para Nómina Nativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPrestacionVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPrestacionVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID tipo comprobante provisiones. Tipo: INT. FK→[GeneralLedger].[JournalVoucherTypes]. Comprobante contable para provisión de beneficios. Solo Nómina Nativa.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdProvisionVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tipo de Comprobante de Provisiones (Solo para Nómina Nativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdProvisionVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdProvisionVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID tipo comprobante nómina. Tipo: INT. FK→[GeneralLedger].[JournalVoucherTypes]. Comprobante contable para registro de nómina. Solo Nómina Nativa.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPayrollVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tipo de Comprobante de Nomina (Solo para Nómina Nativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPayrollVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPayrollVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Meses a promediar vacaciones compensadas. Tipo: TINYINT. Período sobre el cual se calcula promedio para liquidación de vacaciones no gozadas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AverageMonthVacationCompensation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Meses a promedir vacaciones compensadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AverageMonthVacationCompensation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AverageMonthVacationCompensation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Incluir día 31 en cálculos de liquidación. Tipo: BIT. Aplica en días de vacaciones, licencias no remuneradas y sanciones en liquidación de contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'Day31';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si los cálculos de Días de Vacaciones, Licencias NO Remuneradas y Sanciones en Liquidación de Contrato, tienen en cuenta el día 31 de un Mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'Day31';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'Day31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercero para contabilización. Tipo: CHAR(1). Valores: 1=Tercero empleado, 2=Tercero empresa. Define quién contabiliza el concepto nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AccountedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Tercero del Empleado 2 - Tercero de la Empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AccountedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AccountedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable nómina. Tipo: VARCHAR(20). Número de cuenta Mayor para registro de conceptos de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comprobante prestaciones. Tipo: VARCHAR(50). Descripción del comprobante contable para provisión de prestaciones sociales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PrestacionVoucherName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del bono de Prestacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PrestacionVoucherName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PrestacionVoucherName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código comprobante prestaciones. Tipo: VARCHAR(10). Identificador interno del comprobante contable para prestaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PrestacionVoucherCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comprobante Prestación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PrestacionVoucherCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PrestacionVoucherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comprobante provisiones. Tipo: VARCHAR(50). Descripción del comprobante contable para aprovisionamiento de beneficios.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ProvisionVoucherName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del comprobante de aprovisionamiento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ProvisionVoucherName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ProvisionVoucherName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código comprobante provisiones. Tipo: VARCHAR(10). Identificador interno del comprobante contable de provisiones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ProvisionVoucherCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comprobante Provisión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ProvisionVoucherCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ProvisionVoucherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comprobante nómina. Tipo: VARCHAR(50). Descripción del comprobante contable para registro de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollVoucherName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del comprobante de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollVoucherName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollVoucherName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código comprobante nómina. Tipo: VARCHAR(10). Identificador interno del comprobante contable de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollVoucherCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comprobante Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollVoucherCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollVoucherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interfaz integración. Tipo: VARCHAR(50). Identificador del proceso de integración con sistemas externos (SAP, Cognos, etc).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InterfaceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Interfaz', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InterfaceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InterfaceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor aproximación redondeo. Tipo: INT. Número de decimales o factor para redondeo de cálculos nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AproximationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor aproximado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AproximationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AproximationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final jornada ordinaria. Tipo: TIME. Hora de término de la jornada laboral estándar (ej: 17:00).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndTimeOrdinaryDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora final para la jornada ordinaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndTimeOrdinaryDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndTimeOrdinaryDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial jornada ordinaria. Tipo: TIME. Hora de inicio de la jornada laboral estándar (ej: 08:00).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InitialTimeOrdinaryDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo incicial para la jornada ordinario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InitialTimeOrdinaryDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InitialTimeOrdinaryDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aporte Caja de Compensación. Tipo: NUMERIC(6,3). Tarifa patronal para Caja de Compensación Familiar (CCF).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'CompensationFundContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Aporte Caja Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'CompensationFundContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'CompensationFundContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aporte ICBF. Tipo: NUMERIC(6,3). Tarifa patronal para Instituto Colombiano de Bienestar Familiar.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ICBFContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Aporte ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ICBFContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ICBFContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aporte SENA. Tipo: NUMERIC(6,3). Tarifa patronal para Servicio Nacional de Aprendizaje.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SenaContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Aporte SENA', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SenaContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SenaContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Domingo es día hábil. Tipo: BIT. Valores: 1=Sí, 0=No. Determina si domingos se consideran laborales en el período.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SundayBusinessDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Domingo Día Hábil SI - NO', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SundayBusinessDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SundayBusinessDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sábado es día hábil. Tipo: BIT. Valores: 1=Sí, 0=No. Determina si sábados se consideran laborales en jornada ordinaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SaturdayBusinessDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sábado Día Hábil SI - NO', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SaturdayBusinessDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SaturdayBusinessDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Meses a promediar vacaciones disfrutadas. Tipo: TINYINT. Período para cálculo promedio de salario en vacaciones gozadas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AverageMonthVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Meses a promedir vacaciones disfrutadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AverageMonthVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AverageMonthVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo deducción vivienda. Tipo: NUMERIC(18). Límite de deducción permitida para crédito de vivienda del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'HousingDeductionMaximumValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Maximo de Deduccion Vivienda', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'HousingDeductionMaximumValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'HousingDeductionMaximumValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje descuento educación y salud. Tipo: NUMERIC(6,3). Tarifa de descuento para aportes a fondos de educación y cobertura sanitaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EducationHealthDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Descuento por Estudio y Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EducationHealthDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EducationHealthDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje máximo descuento/deducción. Tipo: NUMERIC(6,3). Límite máximo de descuentos totales sobre salario líquido.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaximunDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje descuento maximo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaximunDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaximunDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número máximo primas por año. Tipo: INT. Cantidad de primas permitidas anualmente (ej: 2 para navidad y mitad año).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaxPremiumByYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero maximo de primas durante un año', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaxPremiumByYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaxPremiumByYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permite pago vacaciones anticipadas. Tipo: BIT. Valores: 1=Sí, 0=No. Habilita liquidación anticipada de vacaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationAdvanced';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite el grupo que se pagen vacaciones adelantadas ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationAdvanced';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationAdvanced';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días vacaciones anuales. Tipo: TINYINT. Cantidad de días de descanso remunerado por año (default: 15 días colombianos).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de vacaciones que puede disfrutar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número máximo vacaciones por año. Tipo: INT. Períodos vacacionales permitidos anualmente.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaxVacationByYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero maximo de Vacaciones durante un año', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaxVacationByYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaxVacationByYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquida vacaciones en nómina. Tipo: BIT. Valores: 1=Sí, 0=No. Determina si vacaciones se provisionen o liquiden directamente.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidateVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquida Vacaciones SI - NO', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidateVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidateVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor UVT Unidad de Valor Tributario. Tipo: NUMERIC(18). Valor vigente de UVT para cálculos fiscales (actualización anual).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'UVTValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor UVT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'UVTValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'UVTValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje exención RTF. Tipo: NUMERIC(6,3). Porcentaje excento de Renta Temporal por Franja (tributación especial).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'RTFExemptPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Excento RTF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'RTFExemptPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'RTFExemptPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salarios máximos para cotización salud/pensión. Tipo: TINYINT. Factor tope para cálculo de aportes a seguridad social.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'HealthContributionMaximunSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Salarios Maximos para liquidar salud y pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'HealthContributionMaximunSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'HealthContributionMaximunSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aporte salud patrono. Tipo: NUMERIC(6,3). Tarifa de contribución del empleador a EPS/seguro salud.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Aporte Salud Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aporte pensión patrono. Tipo: NUMERIC(6,3). Tarifa de aporte del empleador a AFP/fondo pensional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Aporte Pension Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aporte salud empleado. Tipo: NUMERIC(6,3). Tarifa de descuento al trabajador para cobertura sanitaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Aporte Salud Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aporte pensión empleado. Tipo: NUMERIC(6,3). Tarifa de descuento al trabajador para fondo de pensiones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployeePensionContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Aporte Pension Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployeePensionContributionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployeePensionContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor auxilio de transporte. Tipo: NUMERIC(18). Monto diario/mensual no gravable para movilización (actualización anual).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Auxilio de Tranporte', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario mínimo institucional. Tipo: NUMERIC(18). SMMI: piso salarial establecido por la organización (puede ser mayor a legal).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InstitutionalMinimumSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Mínimo Institucional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InstitutionalMinimumSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InstitutionalMinimumSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario mínimo legal vigente. Tipo: NUMERIC(18). SMLV: piso salarial oficial establecido por el gobierno colombiano.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LegalSalaryMinimum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Mínimo Legal Vigente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LegalSalaryMinimum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LegalSalaryMinimum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autoincremental parámetros nómina. Tipo: INT IDENTITY(1,1). Clave primaria única para tabla de configuración PayrollParameter.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros generales de nómina: salario mínimo legal, porcentajes de aportes a salud y pensión (empleado y empleador), auxilio de transporte, reglas de vacaciones y primas, descuentos permitidos, configuración contable y demás variables que controlan el cálculo y liquidación de la nómina institucional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite en número de salarios mínimos mensuales legales vigentes (SMMLV) hasta el cual se aplica la base de cotización contributiva en seguridad social.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LimitSMMLContributive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LimitSMMLContributive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Habilita el calendario de marcación discrecional de días para la liquidación de vacaciones tipo Disfrutar. Tipo: BIT. Valores: 1=Sí, 0=No. Solo aplica para grupos con cultura Costa Rica (CRC); en los demás no se muestra el control ni tiene efecto.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationDiscretionaryDays';
