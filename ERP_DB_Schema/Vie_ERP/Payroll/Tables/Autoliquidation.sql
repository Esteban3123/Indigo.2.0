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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de trabajo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'WorkCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC del Periodo (Oculto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PeriodIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Básico del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias Aporte Caja de Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de aporte Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias Aporte Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de aporte Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VacationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Primas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'IncentivePaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Incapacidades', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'DisabilitiesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ICBFContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ICBFIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Sena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'SenaContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Sena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'SenaIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Caja de Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Caja de Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Riesgo Profesional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte al Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Pension Voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Pension Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Pension Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployeePensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Salud Voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor UPC Adicional (Salud)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UPCAditionalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Salud Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte Salud Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aporte de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IBC de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Fondo de Caja de Compensación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Fondo de Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Fondo de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Fondo de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Autoliquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'AutoliquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Autoliquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'Id';

