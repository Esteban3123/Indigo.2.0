CREATE TABLE [Treasury].[SchedulePaymentBankAccount] (
    [Id]                    INT IDENTITY (1, 1) NOT NULL,
    [SchedulePaymentId]     INT NOT NULL,
    [SupplierId]            INT NOT NULL,
    [SupplierBankAccountId] INT NOT NULL,
    CONSTRAINT [PK_SchedulePaymentBankAccount_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchedulePaymentBankAccount_SchedulePayment] FOREIGN KEY ([SchedulePaymentId]) REFERENCES [Treasury].[SchedulePayment] ([Id]),
    CONSTRAINT [FK_SchedulePaymentBankAccount_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_SchedulePaymentBankAccount_SupplierBankAccount] FOREIGN KEY ([SupplierBankAccountId]) REFERENCES [Common].[SupplierBankAccount] ([Id]),
    CONSTRAINT [AK_HeaderSupplier] UNIQUE NONCLUSTERED ([SchedulePaymentId] ASC, [SupplierId] ASC)
);


GO
ALTER TABLE [Treasury].[SchedulePaymentBankAccount] NOCHECK CONSTRAINT [FK_SchedulePaymentBankAccount_SchedulePayment];


GO
ALTER TABLE [Treasury].[SchedulePaymentBankAccount] NOCHECK CONSTRAINT [FK_SchedulePaymentBankAccount_Supplier];


GO
ALTER TABLE [Treasury].[SchedulePaymentBankAccount] NOCHECK CONSTRAINT [FK_SchedulePaymentBankAccount_SupplierBankAccount];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la programacion de pagos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del proveedor relacionado, de donde se tomara la info de la cuenta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la programacion de pagos a la cual se relacionará informacion de pago ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'Id';

