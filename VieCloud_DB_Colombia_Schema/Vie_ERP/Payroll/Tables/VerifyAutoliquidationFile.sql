CREATE TABLE [Payroll].[VerifyAutoliquidationFile] (
    [Id]                                                INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RegisterStatus]                                    BIT            NOT NULL,
    [PayrollDateLiquidated]                             DATE           NOT NULL,
    [EmployeeId]                                        INT            NOT NULL,
    [TypeContractEmployee]                              VARCHAR (5)    NOT NULL,
    [SubTypeEmployee]                                   VARCHAR (5)    NOT NULL,
    [ForeignNotBound]                                   VARCHAR (5)    NOT NULL,
    [ColombianForeignResident]                          VARCHAR (5)    NOT NULL,
    [CodeCityBranchOffice]                              VARCHAR (10)   NOT NULL,
    [Entry]                                             VARCHAR (1)    NOT NULL,
    [Retirement]                                        VARCHAR (1)    NOT NULL,
    [TDE]                                               VARCHAR (1)    NOT NULL,
    [TAE]                                               VARCHAR (1)    NOT NULL,
    [TDP]                                               VARCHAR (1)    NOT NULL,
    [TAP]                                               VARCHAR (1)    NOT NULL,
    [VSP]                                               VARCHAR (1)    NOT NULL,
    [Correcciones]                                      VARCHAR (1)    NOT NULL,
    [VST]                                               VARCHAR (1)    NOT NULL,
    [SLN]                                               VARCHAR (1)    NOT NULL,
    [IGE]                                               VARCHAR (1)    NOT NULL,
    [LMA]                                               VARCHAR (1)    NOT NULL,
    [VAC]                                               VARCHAR (1)    NOT NULL,
    [AVP]                                               VARCHAR (1)    NOT NULL,
    [VCT]                                               VARCHAR (1)    NOT NULL,
    [IRL]                                               VARCHAR (2)    NOT NULL,
    [PensionAdministratorCode]                          VARCHAR (20)   NOT NULL,
    [TransferPensionAdministratorCode]                  VARCHAR (20)   NOT NULL,
    [EPSCode]                                           VARCHAR (20)   NOT NULL,
    [TransferEPSCode]                                   VARCHAR (20)   NOT NULL,
    [CCFCode]                                           VARCHAR (20)   NOT NULL,
    [PensionDays]                                       INT            NOT NULL,
    [HealthDays]                                        INT            NOT NULL,
    [ProfessionalRiskDays]                              INT            NOT NULL,
    [CompensationFundDays]                              INT            NOT NULL,
    [BasicSalary]                                       NUMERIC (18)   NOT NULL,
    [IntegralSalary]                                    VARCHAR (1)    NOT NULL,
    [IBCPension]                                        NUMERIC (18)   NOT NULL,
    [IBCHealth]                                         NUMERIC (18)   NOT NULL,
    [IBCProfessionalRisk]                               NUMERIC (18)   NOT NULL,
    [IBCCompensationFund]                               NUMERIC (18)   NOT NULL,
    [RateContributionPension]                           NUMERIC (6, 3) NOT NULL,
    [ValuePension]                                      NUMERIC (18)   NOT NULL,
    [VoluntaryContributionPensionValue]                 NUMERIC (18)   NOT NULL,
    [VoluntaryContributionPensionValuePatron]           NUMERIC (18)   NOT NULL,
    [TotalPensionContribution]                          NUMERIC (18)   NOT NULL,
    [PensionSolidarityFundValueContribution]            NUMERIC (18)   NOT NULL,
    [PensionSolidarityFundValueContributionSubsistence] NUMERIC (18)   NOT NULL,
    [ValueNotRetainedByVoluntaryContributions]          NUMERIC (18)   NOT NULL,
    [RateContributionHealth]                            NUMERIC (6, 3) NOT NULL,
    [ValueHealth]                                       NUMERIC (18)   NOT NULL,
    [ValueAditionalUPC]                                 NUMERIC (18)   NOT NULL,
    [AuthorizationNumberDisability]                     VARCHAR (50)   NOT NULL,
    [ValueGeneralDisability]                            NUMERIC (18)   NOT NULL,
    [AuthorizationNumberMaternityLicense]               VARCHAR (50)   NOT NULL,
    [ValueMaternityLicense]                             NUMERIC (18)   NOT NULL,
    [RateContributionProfessionalRisk]                  NUMERIC (6, 3) NOT NULL,
    [WorkCenter]                                        VARCHAR (50)   NOT NULL,
    [ValueContributionProfessionalRisk]                 NUMERIC (18)   NOT NULL,
    [RateContributorCCF]                                NUMERIC (6, 3) NOT NULL,
    [ValueContributionCCF]                              NUMERIC (18)   NOT NULL,
    [RateContributorSENA]                               NUMERIC (6, 3) NOT NULL,
    [ValueSena]                                         NUMERIC (18)   NOT NULL,
    [RateContributionICBF]                              NUMERIC (6, 3) NOT NULL,
    [ValueICBF]                                         NUMERIC (18)   NOT NULL,
    [RateContributorESAP]                               NUMERIC (6, 3) NOT NULL,
    [RateContributorEducationMinistry]                  NUMERIC (6, 3) NOT NULL,
    [MinistryCodeRiskFound]                             VARCHAR (50)   NOT NULL,
    [ProfessionalRiskCode]                              VARCHAR (20)   NOT NULL,
    [TarifaEspecialPensiones]                           INT            NOT NULL,
    [IngressDate]                                       DATE           NULL,
    [DateRetirement]                                    DATE           NULL,
    [SanctionInitialDate]                               DATE           NULL,
    [SanctionEndDate]                                   DATE           NULL,
    [AmbulatoryDisabilityInitialDate]                   DATE           NULL,
    [AmbulatoryDisabiltyEndDate]                        DATE           NULL,
    [MaternityLeaveInitialDate]                         DATE           NULL,
    [MaternityLeaveEndDate]                             DATE           NULL,
    [VacationInitialDate]                               DATE           NULL,
    [VacationEndDate]                                   DATE           NULL,
    [FechaInicioVCT]                                    DATE           NULL,
    [FechaFinVCT]                                       DATE           NULL,
    [FechaInicioIRL]                                    DATE           NULL,
    [FechaFinIRL]                                       DATE           NULL,
    [IBCOtrosParafiscales]                              NUMERIC (18)   NOT NULL,
    [TotalHours]                                        INT            NOT NULL,
    [FechaEmpleadoExterior]                             DATE           NULL,
    [Observations]                                      VARCHAR (500)  NULL,
    [IBCSLN]                                            NUMERIC (18)   NULL,
    [BaseIGE]                                           NUMERIC (18)   NULL,
    [BaseVAC]                                           NUMERIC (18)   NULL,
    [BaseIRL]                                           NUMERIC (18)   NULL,
    [ConfirmationDate]                                  DATETIME       NULL,
    [ConfirmationUser]                                  VARCHAR (20)   NULL,
    [DisconfirmationDate]                               DATETIME       NULL,
    [DisconfirmationUser]                               VARCHAR (20)   NULL,
    [BaseLMA]                                           NUMERIC (18)   NULL,
    [BaseLiquidacionSLN]                                NUMERIC (18)   NULL,
    [BaseLiquidacionIGE]                                NUMERIC (18)   NULL,
    [BaseLiquidacionLMA]                                NUMERIC (18)   NULL,
    [BaseLiquidacionVAC]                                NUMERIC (18)   NULL,
    [BaseLiquidacionIRL]                                NUMERIC (18)   NULL,
    [VSTValue]                                          NUMERIC (18)   NULL,
    [EconomicActivityARL]                               VARCHAR (7)    NULL,
    [ContractId]                                        INT            NULL,
    [ConceptClass]                                      VARCHAR (4)    NULL,
    [LicenceDays]                                       INT            NULL,
    [NoveltyContract]                                   TINYINT        NULL,
    [EmployeeIngressNovelty]                            VARCHAR (4)    NULL,
    [LicenceInitialDate]                                DATE           NULL,
    [LicenceEndDate]                                    DATE           NULL,
    [LRM]                                               VARCHAR (1)    DEFAULT (' ') NULL,
    CONSTRAINT [PK_VerifyAutoliquidationFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Si el empleado ingreso durante el periodo de nomina (CR)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'EmployeeIngressNovelty';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novedades dentro del contrato(Cambio de salario o Cargo)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'NoveltyContract';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dias de  Licencia Remunerada ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'LicenceDays';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ContractId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad económica de la ARL (Administradora de Riesgos Laborales)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'EconomicActivityARL';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la Variación trasnsitoria de salario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VSTValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de liquidación por Incapacidad por riesgo laboral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseLiquidacionIRL';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de liquidación de valoración de Activos y Cuentas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseLiquidacionVAC';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de liquidación de la Licencia de Maternidad o de paternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseLiquidacionLMA';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de liquidación en Incapacidad Temporal por Enfermedad General', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseLiquidacionIGE';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de liquidación de el Salario Líquido Neto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseLiquidacionSLN';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de la Licencia de Maternidad o de paternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseLMA';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'usuario que Desconfirmación ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'DisconfirmationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de desconfirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'DisconfirmationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base por Incapacidad por riesgo laboral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseIRL';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de valoración de Activos y Cuentas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseVAC';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de la Incapacidad Temporal por Enfermedad General', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BaseIGE';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso base de cotización de la suspensión temporal del contrato de trabajo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IBCSLN';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'Observations';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Empleo Exterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'FechaEmpleadoExterior';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas totales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TotalHours';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IBC Otros Parafiscales - IBC = (INGRESO BASE DE COTIZACIÓN )', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IBCOtrosParafiscales';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Fin de la Incapacidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'FechaFinIRL';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del Índice de Rentabilidad Líquida', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'FechaInicioIRL';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha fin de Valores Cotizados en Bolsa ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'FechaFinVCT';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicio de Valores Cotizados en Bolsa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'FechaInicioVCT';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización de las vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VacationEndDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VacationInitialDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización de la licencia por maternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'MaternityLeaveEndDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de la licencia por maternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'MaternityLeaveInitialDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización de la discapacidad ambulatoria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'AmbulatoryDisabiltyEndDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Incapacidad ambulatoria - Fecha inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'AmbulatoryDisabilityInitialDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización de la sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'SanctionEndDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de la sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'SanctionInitialDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de jubilación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'DateRetirement';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IngressDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifa Especial Pensiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TarifaEspecialPensiones';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Riesgos Profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se encontró un riesgo en el código del Ministerio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'MinistryCodeRiskFound';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa Contribuyente Ministerio de Educación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RateContributorEducationMinistry';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contribuyente de la tasa de Escuela Superior de Administración Pública ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RateContributorESAP';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueICBF';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de contribución ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RateContributionICBF';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor SENA ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueSena';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa Contribuyente SENA', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RateContributorSENA';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de contribución de la Caja de Compensación Familia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueContributionCCF';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contribuyente de la tasa de Cajas de Compensación Familiar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RateContributorCCF';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aportación de valor Riesgo profesional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueContributionProfessionalRisk';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de trabajo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'WorkCenter';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de Contribución al Riesgo Profesional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RateContributionProfessionalRisk';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Licencia de maternidad de valor', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueMaternityLicense';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Autorización Licencia de Maternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'AuthorizationNumberMaternityLicense';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Discapacidad General', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueGeneralDisability';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización por discapacidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'AuthorizationNumberDisability';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor adicional de la Unidad de pago por capitación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueAditionalUPC';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueHealth';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de contribución a la salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RateContributionHealth';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor no retenido por contribuciones voluntarias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValueNotRetainedByVoluntaryContributions';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fondo de Solidaridad de Pensiones Valor Contribución Subsistencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundValueContributionSubsistence';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la contribución al Fondo de Solidaridad de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'PensionSolidarityFundValueContribution';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contribución total a la pensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TotalPensionContribution';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la Pensión de Contribución Voluntaria Patrocinador', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VoluntaryContributionPensionValuePatron';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la pensión de contribución voluntaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VoluntaryContributionPensionValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de pensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ValuePension';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de contribución a la pensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RateContributionPension';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IBC Caja de compensación - IBC = (INGRESO BASE DE COTIZACIÓN )', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IBCCompensationFund';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IBC Riesgos profesionales - IBC = (INGRESO BASE DE COTIZACIÓN )', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IBCProfessionalRisk';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IBC salud - IBC = (INGRESO BASE DE COTIZACIÓN ) ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IBCHealth';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IBCPension - IBC = (INGRESO BASE DE COTIZACIÓN )', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IBCPension';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario Integral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IntegralSalary';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario base', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'BasicSalary';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días del Fondo de Compensación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'CompensationFundDays';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Jornadas de Riesgo Profesional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskDays';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Jornadas de salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'HealthDays';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de pensión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'PensionDays';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Certificación de Causación y Pago de Aportes al Sistema de Seguridad Social', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'CCFCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Transferencia de código EPS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TransferEPSCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la EPS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'EPSCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de administrador de pensiones de transferencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TransferPensionAdministratorCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Administradores de Pensiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'PensionAdministratorCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indemnización por Riesgos Laborales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IRL';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variación centros de trabajo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VCT';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aportes adicionales y voluntarios', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'AVP';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vacaciones ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VAC';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidación de Manuscritos Anticipados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'LMA';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso de Gestión Empresarial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'IGE';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'suspensión temporal del contrato de trabajo o licencia no remunerada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'SLN';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variación trasnsitoria de salario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VST';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correcciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'Correcciones';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variación permanente de salario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'VSP';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total Aportes Personales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TAP';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de Desempeño Profesional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TDP';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa Anual Equivalente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TAE';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Terminación de Contrato de Trabajo por Justa Causa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TDE';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Jubilación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'Retirement';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entrada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'Entry';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la oficina sucursal según la ciudad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'CodeCityBranchOffice';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Residente extranjero colombiano', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ColombianForeignResident';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtipo de Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'SubTypeEmployee';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo Contrato Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'TypeContractEmployee';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'EmployeeId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de liquidación de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'PayrollDateLiquidated';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro |1 = Confirmado  | 0 = Desconfirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'RegisterStatus';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de verificación del archivo de autoliquidación de nómina (PILA/seguridad social), donde se almacena para cada empleado y período de liquidación los datos de aportes a pensión, salud, riesgos laborales (ARL), caja de compensación, SENA, ICBF y ESAP, junto con novedades, incapacidades, licencias, vacaciones, ingresos y retiros reportados al operador de información.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase o categoría del concepto de nómina reportado en la autoliquidación (por ejemplo, tipo de novedad o grupo de concepto PILA).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ConceptClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'ConceptClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de la licencia del empleado (licencia remunerada, no remunerada u otro tipo de permiso con impacto en la autoliquidación).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'LicenceInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'LicenceInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fin o terminación de la licencia del empleado reportada en la autoliquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'LicenceEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'LicenceEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de licencia de maternidad remunerada (LRM): marca si aplica este tipo de novedad para el empleado en el período liquidado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'LRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VerifyAutoliquidationFile', @level2type = N'COLUMN', @level2name = N'LRM';
