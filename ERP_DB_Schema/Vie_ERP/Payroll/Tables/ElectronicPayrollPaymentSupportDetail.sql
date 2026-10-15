CREATE TABLE [Payroll].[ElectronicPayrollPaymentSupportDetail] (
    [Id]                                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicPayrollPaymentSupportId] INT           NOT NULL,
    [EntityId]                          INT           NOT NULL,
    [EntityName]                        VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ElectronicPayrollPaymentSupportDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayrollPaymentSupportDetail_ElectronicPayrollPaymentSupport] FOREIGN KEY ([ElectronicPayrollPaymentSupportId]) REFERENCES [Payroll].[ElectronicPayrollPaymentSupport] ([Id])
);


GO
ALTER TABLE [Payroll].[ElectronicPayrollPaymentSupportDetail] NOCHECK CONSTRAINT [FK_ElectronicPayrollPaymentSupportDetail_ElectronicPayrollPaymentSupport];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad desde la cual se genero el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento que genero el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Soporte de Pago de Nomina Electronica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollPaymentSupportId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollPaymentSupportDetail', @level2type = N'COLUMN', @level2name = N'Id';

