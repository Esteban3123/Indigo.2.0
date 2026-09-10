CREATE TABLE [Billing].[BillingConceptAccount] (
    [Id]                             INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingConceptId]               INT     NOT NULL,
    [UnitType]                       TINYINT NOT NULL,
    [EntityIncomeAccountId]          INT     NOT NULL,
    [IndividualIncomeAccountId]      INT     NOT NULL,
    [FeesExpensesAccountId]          INT     NULL,
    [IncomeRecognitionMainAccountId] INT     NULL,
    [DiscountAccountId]              INT     NULL,
    CONSTRAINT [PK_BillingConceptAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingConceptAccount_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_DiscountAccount] FOREIGN KEY ([DiscountAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_MainAccounts] FOREIGN KEY ([EntityIncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_MainAccounts1] FOREIGN KEY ([IndividualIncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_MainAccounts2] FOREIGN KEY ([FeesExpensesAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_MainAccounts3] FOREIGN KEY ([IncomeRecognitionMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable en libro mayor donde se registran movimientos de descuentos, rebajas y deducciones aplicadas a facturas. Referencia FK a GeneralLedger.MainAccounts. Nullable (puede no aplicarse descuento).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable, donde se registra el valor de los movimientos de descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable NIF (Norma Internacional de Información Financiera) para reconocimiento de ingresos según estándares contables. Referencia FK a GeneralLedger.MainAccounts. Nullable según política de reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta nif para reconocimiento de ingresos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable para registrar gastos de honorarios, aranceles y prestadores de servicios profesionales de salud. Referencia FK a GeneralLedger.MainAccounts. Nullable si el concepto no genera gastos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'FeesExpensesAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable para Gastos de Honorarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'FeesExpensesAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'FeesExpensesAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable para ingresos generados por particulares, pacientes particulares o pagadores individuales. Referencia FK a GeneralLedger.MainAccounts.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'IndividualIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable para ingresos a particulares', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'IndividualIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'IndividualIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable para ingresos de la entidad prestadora, provenientes de contratos, asegurados, terceros pagadores y entidades. Referencia FK a GeneralLedger.MainAccounts.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'EntityIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de ingresos de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'EntityIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'EntityIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad funcional del centro de atención donde aplica el concepto: 1=Urgencias, 2=Hospitalización, 3=Quirófanos, 4=Servicios Ambulatorios. TINYINT para clasificación de ingresos por unidad de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'UnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de unidad  1 - Urgencias  2 - Hospitalizacion  3 - Quirofanos  4 - Servicios Ambulatorios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'UnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de facturación asociado (cargos, servicios, procedimientos, honorarios, medicamentos). Referencia FK a Billing.BillingConcept.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de facturacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) de la configuración contable por concepto de facturación y unidad de servicio. IDENTITY(1,1) autoincremental.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona cada concepto de facturación con las cuentas contables correspondientes según el tipo de unidad: cuenta de ingresos entidad, cuenta de ingresos individual, cuenta de honorarios/gastos, cuenta de reconocimiento de ingresos y cuenta de descuentos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount';

GO
CREATE NONCLUSTERED INDEX [IX_BillingConceptAccount_BillingConceptId_UnitType]
    ON [Billing].[BillingConceptAccount]([BillingConceptId] ASC, [UnitType] ASC)
    INCLUDE([DiscountAccountId]);
