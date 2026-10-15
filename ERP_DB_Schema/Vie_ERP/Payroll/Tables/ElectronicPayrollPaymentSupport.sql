CREATE TABLE [Payroll].[ElectronicPayrollPaymentSupport] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Prefix]          VARCHAR (5)   NULL,
    [Consecutive]     VARCHAR (20)  NOT NULL,
    [EmployeePartyId] INT           NOT NULL,
    [Year]            INT           NOT NULL,
    [Month]           TINYINT       NOT NULL,
    [Observations]    VARCHAR (MAX) NULL,
    [CUNE]            VARCHAR (500) NULL,
    [QR]              VARCHAR (500) NULL,
    CONSTRAINT [PK_ElectronicPayrollPaymentSupport] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayrollPaymentSupport_ThirdParty] FOREIGN KEY ([EmployeePartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[ElectronicPayrollPaymentSupport] NOCHECK CONSTRAINT [FK_ElectronicPayrollPaymentSupport_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Bidimensional QR del CUDE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Unico de Nomina Electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'CUNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle del Soporte de pago de nomina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes o Periodo de reporte del documento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año del documento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero del emplado de la transacción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'EmployeePartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del Soporte de pago de nomina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo del Soporte de pago de nomina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Soporte de pago de nomina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Id';

