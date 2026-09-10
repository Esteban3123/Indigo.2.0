CREATE TABLE [Payroll].[EmployeeNEW] (
    [Id]                            INT             NULL,
    [ThirdPartyId]                  INT             NOT NULL,
    [AdmissionDate]                 DATE            NOT NULL,
    [HousingDeductionValue]         DECIMAL (18)    NULL,
    [EducationDeductionValue]       DECIMAL (18)    NULL,
    [ProfessionalRiskPercentage]    DECIMAL (8, 4)  NULL,
    [AverageYearHealth]             DECIMAL (18)    NULL,
    [Pensionary]                    BIT             NOT NULL,
    [EmployeeTypeId]                INT             NULL,
    [PensionaryStatus]              BIT             NULL,
    [PensionaryTypeId]              INT             NULL,
    [TradeUnion]                    TINYINT         NOT NULL,
    [RetiredForeign]                BIT             NULL,
    [CostCenterId]                  INT             NULL,
    [WorkCenterId]                  INT             NULL,
    [VacationLastDateLiquidation]   DATE            NULL,
    [PhotoPath]                     VARCHAR (100)   NULL,
    [EmployeeFootprint]             VARBINARY (MAX) NULL,
    [AllowJobReference]             BIT             NOT NULL,
    [ProcedureTypeRTF]              TINYINT         NULL,
    [UserModified]                  VARCHAR (10)    NOT NULL,
    [DateModified]                  DATE            NOT NULL,
    [State]                         BIT             NOT NULL,
    [DeclarantType]                 TINYINT         NOT NULL,
    [HealthContributorRTF]          NUMERIC (18)    NULL,
    [Relocation]                    BIT             NULL,
    [PermanentInability]            BIT             NULL,
    [InitialDatePermanentInability] DATE            NULL,
    [EndDatePermanentInability]     DATE            NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de staging o migración que almacena registros de empleados dentro del módulo de nómina, capturando datos laborales como fecha de admisión, centro de costo, centro de trabajo y última liquidación de vacaciones. Incluye información de deducciones (vivienda, educación), porcentaje de riesgo profesional, afiliación a pensión y sindicato, así como datos de incapacidad permanente. La ausencia de clave primaria definida y el sufijo `NEW` sugieren que es una tabla temporal de carga o reemplazo de una tabla principal de empleados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'EmployeeNEW';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'EmployeeNEW';
GO
