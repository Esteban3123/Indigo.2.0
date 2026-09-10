CREATE TABLE [Treasury].[PaymentMethods] (
    [Id]                           INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashReceipt]                INT                                                                       NOT NULL,
    [PaymentMethodTypes]           TINYINT                                                                   NOT NULL,
    [Value]                        DECIMAL (18, 2)                                                           NOT NULL,
    [IdBank]                       INT                                                                       NULL,
    [CheckNumber]                  VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Check_Ofuscado", 0)')   NULL,
    [DepositDate]                  DATETIME                                                                  NULL,
    [IdEntityBankAccount]          INT                                                                       NULL,
    [DepositNumber]                VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Deposit_Ofuscado", 0)') NULL,
    [DepositType]                  TINYINT                                                                   NULL,
    [IdCard]                       INT                                                                       NULL,
    [CardNumber]                   VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Card_Ofuscado", 0)')    NULL,
    [BaseValue]                    DECIMAL (18, 2)                                                           NULL,
    [CommissionValue]              DECIMAL (18, 2)                                                           NULL,
    [PercentageCommission]         DECIMAL (5, 2)                                                            NULL,
    [RTFValue]                     DECIMAL (18, 2)                                                           NULL,
    [PercentageRTF]                DECIMAL (5, 2)                                                            NULL,
    [ICAValue]                     DECIMAL (18, 2)                                                           NULL,
    [PercentageICA]                DECIMAL (5, 2)                                                            NULL,
    [IdCostCenter]                 INT                                                                       NULL,
    [CurrencyId]                   INT                                                                       NULL,
    [TRM]                          NUMERIC (20, 5)                                                           NULL,
    [ValueInCurrencyHeader]        NUMERIC (20, 5)                                                           CONSTRAINT [DF__PaymentMe__Value__017A790C] DEFAULT ((0)) NOT NULL,
    [IdAgreementsRedemptionPoints] INT                                                                       NULL,
    [TransactionNumber]            VARCHAR (30)                                                              NULL,
    [TransactionDate]              DATETIME                                                                  NULL,
    [RedemptionPoints]             INT                                                                       NULL,
    [CardType]                     TINYINT                                                                   NULL,
    CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentMethods_AgreementsRedemptionPoints] FOREIGN KEY ([IdAgreementsRedemptionPoints]) REFERENCES [Treasury].[AgreementsRedemptionPoints] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Bank] FOREIGN KEY ([IdBank]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Cards] FOREIGN KEY ([IdCard]) REFERENCES [Treasury].[Cards] ([Id]),
    CONSTRAINT [FK_PaymentMethods_CashReceipts] FOREIGN KEY ([IdCashReceipt]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_PaymentMethods_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentMethods_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PaymentMethods_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);

GO
ADD SENSITIVITY CLASSIFICATION TO
    [Treasury].[PaymentMethods].[CheckNumber]
    WITH (LABEL = 'Sensitive - Financial', INFORMATION_TYPE = 'Financial');

GO
ADD SENSITIVITY CLASSIFICATION TO
    [Treasury].[PaymentMethods].[DepositNumber]
    WITH (LABEL = 'Sensitive - Financial', INFORMATION_TYPE = 'Financial');

GO
ADD SENSITIVITY CLASSIFICATION TO
    [Treasury].[PaymentMethods].[CardNumber]
    WITH (LABEL = 'Sensitive - Financial', INFORMATION_TYPE = 'Financial');

GO
CREATE NONCLUSTERED INDEX [IX_PaymentMethods_IdCashReceipt]
    ON [Treasury].[PaymentMethods]([IdCashReceipt] ASC)
    INCLUDE([PaymentMethodTypes], [Value]);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de puntos a redimir, canje de puntos acumulados en acuerdos de lealtad o programas de beneficios (INT)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'RedemptionPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de puntos a redimir', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'RedemptionPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'RedemptionPoints';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la transacción de pago, cuando se ejecutó el movimiento en el sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la transaccion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TransactionDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de transacción del medio de pago, identificador único del movimiento en pasarela/banco (VARCHAR 30)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TransactionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la transaccion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TransactionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TransactionNumber';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato o acuerdo de puntos de redención vinculado al pago (INT, FK → Treasury.AgreementsRedemptionPoints)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdAgreementsRedemptionPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato de puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdAgreementsRedemptionPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdAgreementsRedemptionPoints';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor convertido a la moneda de la cabecera del recibo de caja, monto normalizado para reportes (NUMERIC 20,5, default 0)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor convertido a la moneda de la cabecera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la Tasa Representativa del Mercado (TRM) al momento de la conversión monetaria (NUMERIC 20,5)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor del trm de la moneda al que fue convertido el valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'TRM';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda de la caja, divisa en que se registró el pago (INT, FK → Common.Currency)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CurrencyId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo al que se asigna el pago, unidad de gestión financiera (INT, FK → Payroll.CostCenter)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCostCenter';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de Impuesto al Consumo de la Actividad Financiera (ICA) retenido sobre el pago (DECIMAL 5,2)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Reteica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageICA';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos del Impuesto al Consumo de la Actividad Financiera (ICA) retenido en la transacción (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'ICAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Reteica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'ICAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'ICAValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de Retención en la Fuente (RTF/Retefuente) aplicado al pago (DECIMAL 5,2)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del retefuente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageRTF';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos de la Retención en la Fuente (RTF/Retefuente) descontado de la transacción (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retefuente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'RTFValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de comisión cobrada por el medio de pago o intermediario (DECIMAL 5,2)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageCommission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de la comision', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageCommission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PercentageCommission';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos de la comisión del medio de pago, descuento aplicado al recibidor (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CommissionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la comisión', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CommissionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CommissionValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base o neto sobre el cual se calcula la comisión, retenciones e impuestos (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor de la base con el que se obtiene el valor de la comision', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'BaseValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tarjeta de crédito/débito enmascarado (PII, Card_Ofuscado), VARCHAR 30 masked', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de la tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CardNumber';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tarjeta de pago registrada en el sistema (INT, FK → Treasury.Cards)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCard';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de consignación: 1=Local, 2=Nacional, clasificación de depósito bancario (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de consignación: 1 = Local, 2 = Nacional', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositType';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de consignación o depósito bancario enmascarado (PII, Deposit_Ofuscado), VARCHAR 30 masked', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de consignación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositNumber';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta bancaria de la entidad receptora del depósito (INT, FK → Treasury.EntityBankAccounts)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de consignación o depósito en el banco, cuándo se acreditó el dinero (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de consignación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'DepositDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del banco emisor o receptor del pago (INT, FK → Payroll.Bank)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdBank';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del pago, monto en pesos recibido por cada forma de pago (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'Value';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de forma de pago: 1=Efectivo, 2=Cheque, 3=Tarjeta, 4=Consignación, 5=Redención de puntos (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de formas de pago: 1 = Efectivo, 2 = Cheque, 3 = Tarjeta, 4  = Consignación, 5 = Redencion de puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del recibo de caja al que pertenece el pago, movimiento de tesorería (INT, FK → Treasury.CashReceipts)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCashReceipt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCashReceipt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'IdCashReceipt';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de método de pago, clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los métodos de pago utilizados en cada recibo de caja del módulo de tesorería. Registra cómo se pagó una transacción: efectivo, cheque, tarjeta, depósito u otros medios, incluyendo valores, comisiones, impuestos y puntos redimidos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de la tarjeta: 1 - Debito, 2- Credito. Solo se llena cuando el metodo de pago es tarjeta' , @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'PaymentMethods', @level2type = N'COLUMN', @level2name = N'CardType';
