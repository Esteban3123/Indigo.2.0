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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquida Salario Mín Institucional SI=1 ó NO=0    ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidaMinimumInstitutionalSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id. Tipo de comprobante de desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdUnemploymentVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de comprobante de liquidación del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdContractLiquidationVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IdIncentiveVoucherType', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdIncentiveVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta de vacaciones de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationVacationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Liquidation Vacation Account', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdLiquidationVacationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta de contrato de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdLiquidationContractAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de contrato de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidationContractAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cuenta Contable de Nómina (Solo para Nómina Nativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPayrollAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tipo de Comprobante de Prestaciones (Solo para Nómina Nativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPrestacionVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tipo de Comprobante de Provisiones (Solo para Nómina Nativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdProvisionVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tipo de Comprobante de Nomina (Solo para Nómina Nativa)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'IdPayrollVoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Meses a promedir vacaciones compensadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AverageMonthVacationCompensation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si los cálculos de Días de Vacaciones, Licencias NO Remuneradas y Sanciones en Liquidación de Contrato, tienen en cuenta el día 31 de un Mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'Day31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Tercero del Empleado 2 - Tercero de la Empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AccountedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del bono de Prestacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PrestacionVoucherName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comprobante Prestación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PrestacionVoucherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del comprobante de aprovisionamiento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ProvisionVoucherName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comprobante Provisión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ProvisionVoucherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del comprobante de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollVoucherName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comprobante Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'PayrollVoucherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Interfaz', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InterfaceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor aproximado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AproximationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora final para la jornada ordinaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EndTimeOrdinaryDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo incicial para la jornada ordinario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InitialTimeOrdinaryDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Aporte Caja Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'CompensationFundContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Aporte ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'ICBFContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Aporte SENA', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SenaContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Domingo Día Hábil SI - NO', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SundayBusinessDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sábado Día Hábil SI - NO', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'SaturdayBusinessDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Meses a promedir vacaciones disfrutadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'AverageMonthVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Maximo de Deduccion Vivienda', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'HousingDeductionMaximumValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Descuento por Estudio y Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EducationHealthDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje descuento maximo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaximunDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero maximo de primas durante un año', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaxPremiumByYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite el grupo que se pagen vacaciones adelantadas ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationAdvanced';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de vacaciones que puede disfrutar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'VacationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero maximo de Vacaciones durante un año', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'MaxVacationByYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquida Vacaciones SI - NO', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LiquidateVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor UVT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'UVTValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Excento RTF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'RTFExemptPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Salarios Maximos para liquidar salud y pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'HealthContributionMaximunSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Aporte Salud Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Aporte Pension Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Aporte Salud Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Aporte Pension Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'EmployeePensionContributionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Auxilio de Tranporte', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'TransportHelpValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Mínimo Institucional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'InstitutionalMinimumSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Mínimo Legal Vigente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'LegalSalaryMinimum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollParameter', @level2type = N'COLUMN', @level2name = N'Id';

