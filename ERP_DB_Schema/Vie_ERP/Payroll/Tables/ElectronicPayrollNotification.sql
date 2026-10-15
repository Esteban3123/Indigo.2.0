CREATE TABLE [Payroll].[ElectronicPayrollNotification] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicPayrollId] INT           NOT NULL,
    [Email]               VARCHAR (100) NOT NULL,
    [Status]              BIT           NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ShippingDate]        DATETIME      NULL,
    CONSTRAINT [PK_ElectronicPayrollNotification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayrollNotification_ElectronicPayroll] FOREIGN KEY ([ElectronicPayrollId]) REFERENCES [Payroll].[ElectronicPayroll] ([Id])
);


GO
ALTER TABLE [Payroll].[ElectronicPayrollNotification] NOCHECK CONSTRAINT [FK_ElectronicPayrollNotification_ElectronicPayroll];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del envío', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del envío      0. Aún no enviado      1. Enviado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electrónico del destinatario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento electronico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del envío del documento electronico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Id';

