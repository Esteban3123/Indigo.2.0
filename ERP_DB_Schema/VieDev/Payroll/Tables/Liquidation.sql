CREATE TABLE [Payroll].[Liquidation] (
    [Id]                                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]                                       INT             NOT NULL,
    [EmployeeId]                                    INT             NOT NULL,
    [CostCenterId]                                  INT             NULL,
    [InitialContractNumber]                         INT             NULL,
    [ContractId]                                    INT             NOT NULL,
    [WorkCenterId]                                  INT             CONSTRAINT [DF_Liquidation_WorkCenterId] DEFAULT ((3)) NOT NULL,
    [RegisterStatus]                                CHAR (1)        NOT NULL,
    [PayrollDateLiquidated]                         DATE            NOT NULL,
    [LiquidationPeriod]                             CHAR (1)        NOT NULL,
    [BasicSalary]                                   NUMERIC (18)    NOT NULL,
    [SalaryType]                                    CHAR (1)        NOT NULL,
    [PayrollDays]                                   TINYINT         NOT NULL,
    [DaysWorked]                                    TINYINT         NOT NULL,
    [ProvisionDays]                                 TINYINT         NULL,
    [RecargoNocturno]                               NUMERIC (18)    NULL,
    [RecargoNocturnoFestivo]                        NUMERIC (18)    NULL,
    [Overtime]                                      NUMERIC (18)    NULL,
    [EveningOvertime]                               NUMERIC (18)    NULL,
    [DiurnalOvertime]                               NUMERIC (18)    NULL,
    [HoursHolidays]                                 TINYINT         NULL,
    [HolidaysEveningHours]                          TINYINT         NULL,
    [ValueTransportingRelief]                       NUMERIC (18)    NULL,
    [VacationDays]                                  TINYINT         NULL,
    [VacationValueEnjoy]                            NUMERIC (18)    NULL,
    [VacationValueLiquidated]                       NUMERIC (18)    NULL,
    [BonusValueServices]                            NUMERIC (18)    NULL,
    [ValueOtherBonuses]                             NUMERIC (18)    NULL,
    [PensionFundId]                                 INT             NULL,
    [PensionContributionValue]                      NUMERIC (18)    NULL,
    [PensionContributionDays]                       SMALLINT        NULL,
    [PensionEnrollmentDays]                         SMALLINT        NULL,
    [EmployerPensionContributionValue]              NUMERIC (18)    NULL,
    [VoluntaryPensionFundId]                        INT             NULL,
    [VoluntaryContributionPensionValue]             NUMERIC (18)    NULL,
    [PensionSolidarityFundId]                       INT             NULL,
    [PensionSolidarityFundValueContribution]        NUMERIC (18)    NULL,
    [PensionSolidarityFundContributionDays]         SMALLINT        NULL,
    [PensionSolidarityFundEnrollmentDays]           SMALLINT        NULL,
    [EmployeeHealthContributionValue]               NUMERIC (18)    NULL,
    [HealthFundId]                                  INT             NULL,
    [QuoteHealthDays]                               SMALLINT        NULL,
    [AffiliateHealthDays]                           SMALLINT        NULL,
    [EmployerHealthContributionValue]               NUMERIC (18)    NULL,
    [AdditionalPUTValue]                            NUMERIC (18)    NULL,
    [HealthJCBLicenses]                             NUMERIC (18)    NULL,
    [HealthContributionLicensesValue]               NUMERIC (18)    NULL,
    [HealthLicensesDays]                            TINYINT         NULL,
    [VoluntaryHealthFundId]                         INT             NULL,
    [VoluntaryHealthContributionValue]              NUMERIC (18)    NULL,
    [TotalBaseRetention]                            NUMERIC (18)    NULL,
    [ExemptValueRetention]                          NUMERIC (18)    NULL,
    [RealBaseRetention]                             NUMERIC (18)    NULL,
    [CalculatedWithholdingValue]                    NUMERIC (18)    NULL,
    [RetentionPercentageApplied]                    TINYINT         NULL,
    [AccumulatedOtherAccrued]                       NUMERIC (18)    NULL,
    [AccumulatedOtherDeducted]                      NUMERIC (18)    NULL,
    [DeductingAccumulated]                          NUMERIC (18)    NULL,
    [PeriodJCB]                                     NUMERIC (18)    NULL,
    [PensionJCB]                                    NUMERIC (18)    NULL,
    [HealthJCB]                                     NUMERIC (18)    NULL,
    [AccumulatedBenefit]                            NUMERIC (18)    NULL,
    [AccumulatedDisabilityValue]                    NUMERIC (18)    NULL,
    [DisabilityDays]                                TINYINT         NULL,
    [AmbulatoryDisabilityDays]                      TINYINT         NULL,
    [AmbulatoryDisabilityInitialDate]               DATE            NULL,
    [AmbulatoryDisabilityEndDate]                   DATE            NULL,
    [AmbulatoryDisabilityAuthorizationNumber]       VARCHAR (30)    NULL,
    [AmbulatoryDisabilityValue]                     NUMERIC (18)    NULL,
    [DisabilityHospitalInitialDate]                 DATE            NULL,
    [DisabilityHospitalEndDate]                     DATE            NULL,
    [DisabilityHospitalReleasedNumber]              VARCHAR (30)    NULL,
    [DisabilityHospitalDays]                        TINYINT         NULL,
    [DisabilityHospitalValue]                       NUMERIC (18)    NULL,
    [MaternityLeaveDays]                            NUMERIC (18)    NULL,
    [MaternityLeaveInitialDate]                     DATE            NULL,
    [MaternityLeaveEndDate]                         DATE            NULL,
    [MaternityLeaveAutorizationNumber]              VARCHAR (30)    NULL,
    [MaternityLeaveValue]                           NUMERIC (18)    NULL,
    [VacationInitialDate]                           DATE            NULL,
    [VacationEndDate]                               DATE            NULL,
    [LicenseDays]                                   TINYINT         NULL,
    [LicenseValue]                                  NUMERIC (18)    NULL,
    [UnpaidLicenseValue]                            NUMERIC (18)    NULL,
    [UnpaidLicenseAutorizarionNumber]               VARCHAR (30)    NULL,
    [UnpaidLicenseDays]                             TINYINT         NULL,
    [UnpaidLicenseInitialDate]                      DATE            NULL,
    [UnpaidLicenseEndDate]                          DATE            NULL,
    [SanctionInitialDate]                           DATE            NULL,
    [SanctionEndDate]                               DATE            NULL,
    [SanctionValue]                                 NUMERIC (18)    NULL,
    [SanctionDays]                                  TINYINT         NULL,
    [OccupationalRisksContributionValue]            NUMERIC (18)    NULL,
    [QuotedOccupationalRisksDays]                   TINYINT         CONSTRAINT [DF_Liquidation_QuotedOccupationalRisksDays] DEFAULT ((0)) NOT NULL,
    [OccupationalRisksDisabilityValue]              NUMERIC (18)    NULL,
    [OccupationalRisksDays]                         TINYINT         NULL,
    [OccupationalRisksDisabilityAutorizationNumber] VARCHAR (30)    NULL,
    [OccupationalRisksDisabilityInitialDate]        DATE            NULL,
    [OccupationalRisksDisabilityEndDate]            DATE            NULL,
    [OccupationalRisksFundId]                       INT             NULL,
    [PermissionsValue]                              NUMERIC (18)    NULL,
    [UnemploymentAccumulated]                       NUMERIC (18)    NULL,
    [CompensationAccumulated]                       NUMERIC (18)    NULL,
    [VacationHealthJCB]                             TINYINT         NULL,
    [VacationHealthContributionValueEmployee]       NUMERIC (18)    NULL,
    [VacationHealthContributionValueEmployer]       NUMERIC (18)    NULL,
    [VacationPensionJCB]                            TINYINT         NULL,
    [VacationPensionContributionValueEmployee]      NUMERIC (18)    NULL,
    [VacationPensionContributionValueEmployer]      NUMERIC (18)    NULL,
    [AccountingVouchersNumber]                      INT             NULL,
    [TotalAccrued]                                  NUMERIC (18)    NOT NULL,
    [TotalDeducted]                                 NUMERIC (18)    NOT NULL,
    [TotalPaid]                                     NUMERIC (18)    NOT NULL,
    [PermissionDays]                                TINYINT         NULL,
    [SenaContributionValue]                         NUMERIC (18)    NULL,
    [FamilyCompensationFundContributionValue]       NUMERIC (18)    NULL,
    [ICBFContributionValue]                         NUMERIC (18)    NULL,
    [ParafiscalContribution]                        NUMERIC (18)    NULL,
    [ProvisionsValue]                               NUMERIC (18)    NULL,
    [ProvisionVacation]                             NUMERIC (18)    NULL,
    [ProvisionIncentive]                            NUMERIC (18)    NULL,
    [ProvisionInterestsUnemployment]                NUMERIC (18)    NULL,
    [VacationNumberBussinesDays]                    TINYINT         NULL,
    [CompletePayroll]                               BIT             NOT NULL,
    [PayrollProcessDate]                            DATE            NULL,
    [PayrollConfirmationDate]                       DATE            NOT NULL,
    [RetirementLiquidationDate]                     DATE            NULL,
    [PayrollProcessUser]                            INT             NOT NULL,
    [PayrollConfirmationUser]                       INT             NOT NULL,
    [QuotedCompensationDays]                        TINYINT         CONSTRAINT [DF_Liquidation_QuotedCompensationDays] DEFAULT ((0)) NOT NULL,
    [IBCUnemployment]                               NUMERIC (18)    NULL,
    [IBCUnemploymentNoSanctions]                    NUMERIC (18)    NULL,
    [IBCSENA]                                       NUMERIC (18)    NULL,
    [IBCCompensationFund]                           NUMERIC (18)    NULL,
    [IBCICBF]                                       NUMERIC (18)    NULL,
    [IBCVacation]                                   NUMERIC (18)    NULL,
    [IBCIncentivePayment]                           NUMERIC (18)    NULL,
    [IBCOccupationalRisks]                          NUMERIC (18)    CONSTRAINT [DF_Liquidation_IBCOccupationalRisks] DEFAULT ((0)) NOT NULL,
    [BankId]                                        INT             NULL,
    [BankAccountNumber]                             VARCHAR (20)    NULL,
    [UnemployementFundId]                           INT             NULL,
    [HousingDeductionValue]                         DECIMAL (18)    CONSTRAINT [DF_Liquidation_HousingDeductionValue] DEFAULT ((0)) NOT NULL,
    [DependentsDeduction]                           DECIMAL (18)    CONSTRAINT [DF_Liquidation_DependentsDeduction] DEFAULT ((0)) NOT NULL,
    [ProcedureTypeRTF]                              TINYINT         CONSTRAINT [DF_Liquidation_ProcedureTypeRTF] DEFAULT ((0)) NOT NULL,
    [DeductionsAndRentExents]                       NUMERIC (18)    NULL,
    [DependentsSupplementary]                       DECIMAL (18, 2) CONSTRAINT [DF_Liquidation_DependentsSupplementary] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Liquidation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Liquidation_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_Liquidation_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_Liquidation_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_Liquidation_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Liquidation_Fund] FOREIGN KEY ([HealthFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_Liquidation_Fund1] FOREIGN KEY ([PensionFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_Liquidation_Fund2] FOREIGN KEY ([OccupationalRisksFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_Liquidation_Fund3] FOREIGN KEY ([VoluntaryHealthFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_Liquidation_Fund4] FOREIGN KEY ([VoluntaryPensionFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_Liquidation_FundUnemployment] FOREIGN KEY ([UnemployementFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_Liquidation_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_Liquidation_WorkCenter] FOREIGN KEY ([WorkCenterId]) REFERENCES [Payroll].[WorkCenter] ([Id])
);


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Bank];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Contract];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_CostCenter];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Employee];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Fund];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Fund1];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Fund2];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Fund3];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Fund4];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_FundUnemployment];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_Group];


GO
ALTER TABLE [Payroll].[Liquidation] NOCHECK CONSTRAINT [FK_Liquidation_WorkCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que almacena la renta complementaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DependentsSupplementary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que almacena el valor de la renta exenta del mes - Retefuente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DeductionsAndRentExents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cotizaciones de días de compensación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'QuotedCompensationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de contribución a la pensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionContributionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recargo Nocturno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'RecargoNocturno';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de provisión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ProvisionDays';

