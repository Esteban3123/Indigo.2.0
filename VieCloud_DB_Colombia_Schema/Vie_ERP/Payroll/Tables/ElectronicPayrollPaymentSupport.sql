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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código QR bidimensional (VARCHAR 500) del CUNE embebido, código de barras para lectura óptica y verificación del soporte de pago electrónico.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Bidimensional QR del CUDE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único de Nómina Electrónica (VARCHAR 500), identificador fiscal del documento de pago DIAN (obligatorio para validación legal).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'CUNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Unico de Nomina Electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'CUNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'CUNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle, notas o comentarios (VARCHAR MAX) del soporte de pago de nómina electrónica, información complementaria del registro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del Soporte de pago de nomina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes o período (TINYINT 1-12) de reporte del soporte de pago de nómina electrónica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes o Periodo de reporte del documento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año (INT) del documento de soporte de pago de nómina electrónica, período fiscal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año del documento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/empleado (FK a [Common].[ThirdParty]) vinculado a la transacción de pago de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'EmployeePartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero del emplado de la transacción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'EmployeePartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'EmployeePartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo (VARCHAR 20) del soporte de pago de nómina electrónica, secuencia documental obligatoria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del Soporte de pago de nomina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo alfabético (VARCHAR 5) del soporte de pago de nómina electrónica, parte del identificador documental.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo del Soporte de pago de nomina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Prefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del soporte de pago de nómina electrónica, clave primaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Soporte de pago de nomina electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Soporte de pago de nómina electrónica por empleado y período. Registra el comprobante digital de pago generado para cada trabajador, incluyendo el código único de nómina electrónica (CUNE), el código QR de verificación y datos de consecutivo para el mes y año liquidado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupport';
