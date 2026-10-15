CREATE TABLE [Payroll].[Autoliquidation] (
    [Id]                                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AutoliquidationDate]                    DATE         NOT NULL,
    [State]                                  CHAR (1)     NOT NULL,
    [GroupId]                                INT          NOT NULL,
    [ContractId]                             INT          NOT NULL,
    [PensionFundId]                          INT          NULL,
    [HealthFundId]                           INT          NULL,
    [ProfessionalRiskFundId]                 INT          NULL,
    [CompensationFundId]                     INT          NULL,
    [HealthIBC]                              NUMERIC (18) NULL,
    [HealthContributionValue]                NUMERIC (18) NULL,
    [EmployeeHealthContributionValue]        NUMERIC (18) NULL,
    [EmployerHealthContributionValue]        NUMERIC (18) NULL,
    [UPCAditionalValue]                      NUMERIC (18) NULL,
    [VoluntaryHealthContributionValue]       NUMERIC (18) NULL,
    [PensionIBC]                             NUMERIC (18) NULL,
    [PensionContributionValue]               NUMERIC (18) NULL,
    [EmployeePensionContributionValue]       NUMERIC (18) NULL,
    [EmployerPensionContributionValue]       NUMERIC (18) NULL,
    [VoluntaryPensionContributionValue]      NUMERIC (18) NULL,
    [PensionSolidarityFundContributionValue] NUMERIC (18) NULL,
    [UnemploymentIBC]                        NUMERIC (18) NULL,
    [UnemploymentValue]                      NUMERIC (18) NULL,
    [ProfessionalRiskIBC]                    NUMERIC (18) NULL,
    [ProfessionalRiskContributionValue]      NUMERIC (18) NULL,
    [CompensationFundIBC]                    NUMERIC (18) NULL,
    [CompensationFundContributionValue]      NUMERIC (18) NULL,
    [SenaIBC]                                NUMERIC (18) NULL,
    [SenaContributionValue]                  NUMERIC (18) NULL,
    [ICBFIBC]                                NUMERIC (18) NULL,
    [ICBFContributionValue]                  NUMERIC (18) NULL,
    [DisabilitiesValue]                      NUMERIC (18) NULL,
    [IncentivePaymentValue]                  NUMERIC (18) NULL,
    [VacationValue]                          NUMERIC (18) NULL,
    [PensionEnrollmentDays]                  SMALLINT     NULL,
    [HealthEnrollmentDays]                   SMALLINT     NULL,
    [ProfessionalRiskEnrollmentDays]         SMALLINT     NULL,
    [CompensationFundEnrollmentDays]         SMALLINT     NULL,
    [BasicSalary]                            NUMERIC (18) NULL,
    [PeriodIBC]                              NUMERIC (18) NULL,
    [WorkCenterId]                           INT          NULL,
    CONSTRAINT [PK_Autoliquidation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Autoliquidation_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_Autoliquidation_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[Autoliquidation] NOCHECK CONSTRAINT [FK_Autoliquidation_Contract];


GO
ALTER TABLE [Payroll].[Autoliquidation] NOCHECK CONSTRAINT [FK_Autoliquidation_Group];

