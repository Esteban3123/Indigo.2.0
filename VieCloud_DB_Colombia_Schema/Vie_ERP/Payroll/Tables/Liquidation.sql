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
    [PreviousMonthIBC]                              NUMERIC (18)    NULL,
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
CREATE NONCLUSTERED INDEX [IX_Liquidation__GroupId]
    ON [Payroll].[Liquidation]([GroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Liquidation__EmployeeId]
    ON [Payroll].[Liquidation]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Liquidation__ContractId]
    ON [Payroll].[Liquidation]([ContractId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Liquidation__PensionFundId]
    ON [Payroll].[Liquidation]([PensionFundId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que almacena la renta complementaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DependentsSupplementary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que almacena el valor de la renta exenta del mes - Retefuente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DeductionsAndRentExents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Procedimiento para Retención en la Fuente: 1 - Procedimiento 1; 2 - Procedimiento 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ProcedureTypeRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor que aplica por deduccion por dependientes, este valor se utiliza para el calculo del procedimiento 1 y 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DependentsDeduction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor deduccion vivienda, este campo se obtiene del empleado y es utilizado para el calculo del procedimiento 1 y 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'HousingDeductionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Fondo de Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'UnemployementFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del banco', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC para riesgos profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'IBCOccupationalRisks';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Pago de incentivos ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'IBCIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Vacation', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'IBCVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'IBCICBF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fondo de compensación de IBC', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'IBCCompensationFund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC SENA', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'IBCSENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Cesantías SIN DÍAS DE SANCION', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'IBCUnemploymentNoSanctions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'IBCUnemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cotizaciones de días de compensación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'QuotedCompensationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que confirmó la nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PayrollConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que proceso la nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PayrollProcessUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Liquidación de Retiro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'RetirementLiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de confirmacion de la Nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PayrollConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Proceso de la Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PayrollProcessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nomina Completa 1->30 dias 0-> menor a 30 dias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'CompletePayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Dias Habiles en Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationNumberBussinesDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Provision Intereses de cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ProvisionInterestsUnemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Provicion prima', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ProvisionIncentive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor provision de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ProvisionVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Provisiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ProvisionsValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Parafiscales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ParafiscalContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ICBFContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Caja de Compensación Familiar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'FamilyCompensationFundContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de Contribución SENA', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'SenaContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Permiso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PermissionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'TotalPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del Comprobante Contable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AccountingVouchersNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de Aporte de Pension en Vacaciones Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationPensionContributionValueEmployer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de Aporte Pension en Vacaciones Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationPensionContributionValueEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC de Pension en Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationPensionJCB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aporte de Salud en Vacaciones Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationHealthContributionValueEmployer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aporte de Salud en Vacaciones empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationHealthContributionValueEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC de Salud en Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationHealthJCB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumulado Indemnizaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'CompensationAccumulated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumulado de Cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentAccumulated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de los Permisos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PermissionsValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Fondo de Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRisksFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Incapacidad de Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRisksDisabilityEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de Incapacidad de Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRisksDisabilityInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de aporte a Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRisksDisabilityAutorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de la incapacidad por accidente laboral o  riesgo profesional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRisksDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Incapacidad por  Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRisksDisabilityValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de riesgos laborales citados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'QuotedOccupationalRisksDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor del aporte a riesgos profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'OccupationalRisksContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'SanctionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'SanctionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'SanctionEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la Sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'SanctionInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Licencia no Remunerada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'UnpaidLicenseEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Licencia no Remunerada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'UnpaidLicenseInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de Licencia no Remunerada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'UnpaidLicenseDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Autorización de la Licencia no Remunerada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'UnpaidLicenseAutorizarionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Licencia no Remunerada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'UnpaidLicenseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Licencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'LicenseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Licencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'LicenseDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Licencia de Maternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'MaternityLeaveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Autorizacion de la Licencia de Maternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'MaternityLeaveAutorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Licencia de Maternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'MaternityLeaveEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la Licencia de Maternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'MaternityLeaveInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Licencia de Maternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'MaternityLeaveDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Incapacidad Hospitalaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DisabilityHospitalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de la Incapacidad Hospitalaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DisabilityHospitalDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Autorizacion de la Incapacidad Hospitalaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DisabilityHospitalReleasedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Incapacidad Hospitalaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DisabilityHospitalEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la Incapacidad Hospitalaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DisabilityHospitalInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Incapacidad Ambulatoria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AmbulatoryDisabilityValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Autorización de la Incapacidad Ambulatoria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AmbulatoryDisabilityAuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Incapacidad Ambulatoria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AmbulatoryDisabilityEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la Incapacidad Ambulatoria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AmbulatoryDisabilityInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de Incapacidades Ambulatoria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AmbulatoryDisabilityDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Incapacidades', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DisabilityDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Acumulado de Incapacidades', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AccumulatedDisabilityValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumulado de Prestaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AccumulatedBenefit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'HealthJCB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC De Pensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionJCB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC del Periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PeriodJCB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumulado de Deducidos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DeductingAccumulated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumulado de Otros Deducidos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AccumulatedOtherDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumulado de Otros Devengados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AccumulatedOtherAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de Retención Aplicado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'RetentionPercentageApplied';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Calculado de Retención en la Fuente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'CalculatedWithholdingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Real de Retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'RealBaseRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Excento de Retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ExemptValueRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base Total de Retención', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'TotalBaseRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte a Salud Voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Fondo de Salud Voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryHealthFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Salud por Licencias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'HealthLicensesDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Salud por Licencias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'HealthContributionLicensesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC de Salud para Licencias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'HealthJCBLicenses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor UPC Adicionales (Salud)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AdditionalPUTValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Salud Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Afiliación a Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'AffiliateHealthDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Cotización de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'QuoteHealthDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Fondo de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'HealthFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte de Salud del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Afiliación al Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Afiliación al Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundContributionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte al Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundValueContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Pensión Voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryContributionPensionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Fondo de Pensión Voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Aporte de Pensión del Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Afilicación a Pensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de contribución a la pensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionContributionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Pensión Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id fondo de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PensionFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Otras Primas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ValueOtherBonuses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de Prima de Servicios', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'BonusValueServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Vacaciones Liquidadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationValueLiquidated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Vacaciones Disfrutadas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationValueEnjoy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'VacationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Auxilio de Tranporte', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ValueTransportingRelief';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas Nocturnas Festivas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'HolidaysEveningHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas Festivas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'HoursHolidays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas Extras Diurnas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DiurnalOvertime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas Extras Nocturnas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'EveningOvertime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas Extras', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'Overtime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recargo Nocturno Festivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'RecargoNocturnoFestivo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recargo Nocturno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'RecargoNocturno';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de provisión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ProvisionDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días Trabajados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'DaysWorked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PayrollDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Salario: 1 - Normal, 2 - Integral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'SalaryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Básico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo de Liquidación 1 - Primera Quincena, 2 - Segunda Quincena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'LiquidationPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Nómina Liquidada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PayrollDateLiquidated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro: "" Sin confirmar, "C" Confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'RegisterStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de trabajo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'WorkCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Contrato Inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro detallado de cada liquidación de nómina por empleado y período: contiene devengados (salario, horas extras, recargos, auxilio de transporte, vacaciones, primas), deducciones (salud, pensión, retención en la fuente, sanciones, licencias no remuneradas), aportes parafiscales (SENA, ICBF, caja de compensación, riesgos laborales), provisiones contables y totales finales pagados. Es el núcleo del procesamiento de nómina de la organización.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IBC del mes anterior tomado de PeriodJCB de la última liquidación confirmada del mes previo del mismo empleado/contrato. Usado en el microservicio PILA para calcular C42/C43 en líneas de novedades (VAC_LR). Para salario integral ya incluye el factor 0.7. NUMERIC(18,0) NULL.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Liquidation', @level2type = N'COLUMN', @level2name = N'PreviousMonthIBC';
