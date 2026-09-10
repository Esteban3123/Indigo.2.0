CREATE TABLE [Payroll].[TmpPendingProvisionVacation] (
    [Id]                INT             NULL,
    [Nit]               VARCHAR (100)   NULL,
    [Name]              VARCHAR (500)   NULL,
    [VacationProvision] NUMERIC (20, 2) NULL,
    [VacationValue]     NUMERIC (20, 2) NULL,
    [PendingReport]     NUMERIC (21, 2) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto pendiente de reportar en nómina; valor numérico (21,2) de vacaciones no reportadas en liquidación o informe de pago pendiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'PendingReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Informe pendiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'PendingReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'PendingReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor económico acumulado de vacaciones; monto numérico (20,2) en pesos que representa el costo total de días de descanso remunerado pendiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'VacationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'VacationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'VacationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Provisión contable de vacaciones; reserva numérica (20,2) constituida para cubrir obligación de pago de vacaciones no disfrutadas del profesional de salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'VacationProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disposición de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'VacationProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'VacationProvision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del empleado, profesional de salud o trabajador; cadena de texto (500) que identifica al afiliado en nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT o número de identificación tributaria del empleado; identificador único (VARCHAR 100) equivalente a cédula o documento de identidad PII', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT) de la fila temporal de provisión de vacaciones; clave primaria de la tabla temporal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla temporal de nómina que registra las provisiones de vacaciones pendientes por empleado, utilizada para calcular y reportar los valores de vacaciones acumulados y los saldos aún no liquidados.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation';
