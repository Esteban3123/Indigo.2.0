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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta bancaria del proveedor (FK a SupplierBankAccount). Especifica qué cuenta bancaria del proveedor se utilizará para recibir el pago programado. Referencia: [Common].[SupplierBankAccount].[Id]', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la programacion de pagos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor (FK a Supplier). Proveedor asociado cuya información de cuenta bancaria se utilizará en la programación de pagos. Referencia: [Common].[Supplier].[Id]', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del proveedor relacionado, de donde se tomara la info de la cuenta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la programación de pagos (FK a SchedulePayment). Vincula esta relación a la programación de pagos específica a la cual se asigna la información de pago/cuenta bancaria. Referencia: [Treasury].[SchedulePayment].[Id]', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la programacion de pagos a la cual se relacionará informacion de pago ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY 1,1) de la relación entre programación de pagos, proveedor y cuenta bancaria. Clave primaria clustered de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona cada pago programado (schedule de tesorería) con la cuenta bancaria del proveedor a la que se debe girar el dinero. Permite saber a qué banco y número de cuenta se debe transferir cada obligación de pago pendiente.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentBankAccount';
