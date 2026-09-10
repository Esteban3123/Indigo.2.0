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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de trabajo, unidad funcional o sede donde labora el empleado (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'WorkCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de trabajo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'WorkCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'WorkCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización del período de liquidación (NUMERIC 18, PII Ofuscado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PeriodIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC del Periodo (Oculto)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PeriodIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PeriodIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario básico mensual del empleado, base para cálculo de aportes (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Básico del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días cotizados a Caja de Compensación en el período (SMALLINT)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundEnrollmentDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias Aporte Caja de Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundEnrollmentDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días cotizados a Riesgos Profesionales, ARP en el período (SMALLINT)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskEnrollmentDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de aporte Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskEnrollmentDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días cotizados a Salud, EPS en el período (SMALLINT)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthEnrollmentDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias Aporte Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthEnrollmentDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días cotizados a Pensión, AFP en el período (SMALLINT)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionEnrollmentDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de aporte Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionEnrollmentDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionEnrollmentDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor liquidado por vacaciones, descanso remunerado (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VacationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VacationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VacationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de primas, bonificaciones e incentivos pagados (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'IncentivePaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Primas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'IncentivePaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'IncentivePaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de incapacidades, licencias por enfermedad pagadas (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'DisabilitiesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Incapacidades', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'DisabilitiesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'DisabilitiesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte al Instituto Colombiano de Bienestar Familiar (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ICBFContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ICBFContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ICBFContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización para ICBF (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ICBFIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ICBFIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ICBFIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte al Servicio Nacional de Aprendizaje (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'SenaContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Sena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'SenaContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'SenaContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización para SENA (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'SenaIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC Sena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'SenaIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'SenaIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte a Caja de Compensación (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Caja de Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización para Caja de Compensación (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC Caja de Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte a Riesgos Profesionales, ARP (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Riesgo Profesional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización para Riesgos Profesionales (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de cesantías, fondo de desempleo liquidado (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización para Cesantías (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UnemploymentIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte al Fondo de Solidaridad Pensional (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte al Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de aporte voluntario a pensión complementaria (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Pension Voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryPensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte patronal a pensión, AFP (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Pension Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployerPensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte del empleado a pensión, AFP (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployeePensionContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Pension Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployeePensionContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployeePensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del aporte a pensión (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización para Pensión, AFP (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de aporte voluntario a salud, EPS (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryHealthContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Salud Voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryHealthContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'VoluntaryHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de UPC adicional en salud, sobre-cotización (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UPCAditionalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor UPC Adicional (Salud)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UPCAditionalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'UPCAditionalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte patronal a salud, EPS (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Salud Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployerHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del aporte del empleado a salud, EPS (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte Salud Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'EmployeeHealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del aporte a salud (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aporte de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthContributionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthContributionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización para Salud, EPS (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IBC de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Caja de Compensación afiliada (INT, FK nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Fondo de Caja de Compensación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'CompensationFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Fondo de Riesgos Profesionales, ARP (INT, FK nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Fondo de Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la EPS o Fondo de Salud afiliado (INT, FK nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Fondo de Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'HealthFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la AFP o Fondo de Pensiones afiliado (INT, FK nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Fondo de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionFundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'PensionFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato laboral del empleado (INT, FK obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo o unidad de nómina (INT, FK obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de autoliquidación (CHAR 1: A=Activo, C=Cancelado, etc.)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de procesamiento o liquidación del período (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'AutoliquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Autoliquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'AutoliquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'AutoliquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la autoliquidación (INT, PK IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Autoliquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de autoliquidaciones de aportes a seguridad social y parafiscales por empleado y período. Consolida las bases de cotización (IBC) y valores aportados a salud, pensión, riesgos profesionales, caja de compensación, SENA e ICBF, tanto por parte del empleador como del empleado, para cada contrato laboral en un ciclo de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Autoliquidation';
