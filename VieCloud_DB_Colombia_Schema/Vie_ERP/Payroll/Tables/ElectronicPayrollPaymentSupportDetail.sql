CREATE TABLE [Payroll].[ElectronicPayrollPaymentSupportDetail] (
    [Id]                                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicPayrollPaymentSupportId] INT           NOT NULL,
    [EntityId]                          INT           NOT NULL,
    [EntityName]                        VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ElectronicPayrollPaymentSupportDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayrollPaymentSupportDetail_ElectronicPayrollPaymentSupport] FOREIGN KEY ([ElectronicPayrollPaymentSupportId]) REFERENCES [Payroll].[ElectronicPayrollPaymentSupport] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_ElectronicPayrollPaymentSupportDetail_SupportId]
    ON [Payroll].[ElectronicPayrollPaymentSupportDetail] ([ElectronicPayrollPaymentSupportId])
    INCLUDE ([EntityId], [EntityName]);

GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad origen que generó el registro de detalle; empresa, institución o unidad funcional responsable del soporte de pago de nómina electrónica (VARCHAR 250)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad desde la cual se genero el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la entidad (documento, centro de atención, unidad funcional) que originó el registro de detalle en el soporte de pago (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento que genero el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del soporte o comprobante de pago de nómina electrónica padre; referencia a Payroll.ElectronicPayrollPaymentSupport (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollPaymentSupportId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Soporte de Pago de Nomina Electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollPaymentSupportId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollPaymentSupportId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle del soporte de pago de nómina electrónica; clave primaria del registro (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los conceptos o entidades asociados a cada soporte de pago de nómina electrónica. Registra las líneas de desglose (deducciones, devengados u otros rubros) que componen un comprobante de pago electrónico de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail';
