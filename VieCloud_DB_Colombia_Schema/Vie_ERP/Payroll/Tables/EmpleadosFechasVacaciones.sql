CREATE TABLE [Payroll].[EmpleadosFechasVacaciones] (
    [Nit]                         VARCHAR (50) NULL,
    [VacationLastDateLiquidation] VARCHAR (50) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla auxiliar que registra, por empleado identificado mediante su NIT, la última fecha en que se realizó la liquidación de vacaciones. Sirve como referencia histórica para el proceso de nómina al calcular o verificar períodos vacacionales pendientes o ya liquidados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'EmpleadosFechasVacaciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'EmpleadosFechasVacaciones';
GO
