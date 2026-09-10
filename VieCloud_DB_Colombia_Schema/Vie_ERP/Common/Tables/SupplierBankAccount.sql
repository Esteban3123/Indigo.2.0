CREATE TABLE [Common].[SupplierBankAccount] (
    [Id]             INT                                                                   IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierId]     INT                                                                   NOT NULL,
    [BankId]         INT                                                                   NOT NULL,
    [Type]           TINYINT                                                               NOT NULL,
    [Number]         VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "BankAccount", 0)') NOT NULL,
    [PaymentDefault] BIT                                                                   NOT NULL,
    [CurrencyId]     INT                                                                   NULL,
    [State]          TINYINT                                                               CONSTRAINT [DF__SupplierB__State__53F470D5] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SupplierBankAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupplierBankAccount_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_SupplierBankAccount_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_SupplierBankAccount_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[SupplierBankAccount].[Number]
    WITH (LABEL = 'Sensitive - Financial', INFORMATION_TYPE = 'Financial');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cuenta bancaria del proveedor: 1=Activo, 2=Inactivo. TINYINT, valor por defecto 1.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1-Activo  2-Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Moneda de la cuenta bancaria del proveedor. Referencia FK a [Common].[Currency]. INT nullable.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cuenta bancaria predeterminada para pagos al proveedor. BIT: solo una por proveedor puede ser activa (default). Usado en programación de pagos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'PaymentDefault';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que cuanta bancaria se va a pagar por defecto en programacion de pagos, cada proveedor solo puede tener uno por defecto', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'PaymentDefault';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'PaymentDefault';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta bancaria del proveedor. VARCHAR(100), enmascarado PII: partial(0, ''''BankAccount'''', 0). Identificación ofuscada en consultas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuenta bancaria: 1=Ahorro, 2=Corriente. TINYINT requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cuenta: 1-Ahorro, 2-Corriente', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del banco. INT NOT NULL. Referencia FK a [Payroll].[Bank]([Id]).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del banco', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor propietario de la cuenta. INT NOT NULL. Referencia FK a [Common].[Supplier]([Id]).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la cuenta bancaria del proveedor. INT IDENTITY(1,1) PRIMARY KEY.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuentas bancarias registradas para cada proveedor, incluyendo el banco, tipo de cuenta, número enmascarado, moneda y si es la cuenta predeterminada para pagos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount';
