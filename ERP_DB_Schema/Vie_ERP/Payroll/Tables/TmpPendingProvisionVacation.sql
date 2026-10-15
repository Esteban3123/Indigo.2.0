CREATE TABLE [Payroll].[TmpPendingProvisionVacation] (
    [Id]                INT             NULL,
    [Nit]               VARCHAR (100)   NULL,
    [Name]              VARCHAR (500)   NULL,
    [VacationProvision] NUMERIC (20, 2) NULL,
    [VacationValue]     NUMERIC (20, 2) NULL,
    [PendingReport]     NUMERIC (21, 2) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Informe pendiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'PendingReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'VacationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disposición de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'VacationProvision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nit', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TmpPendingProvisionVacation', @level2type = N'COLUMN', @level2name = N'Id';

