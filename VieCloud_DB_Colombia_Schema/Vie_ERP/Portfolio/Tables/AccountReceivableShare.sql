CREATE TABLE [Portfolio].[AccountReceivableShare] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId]        INT             NOT NULL,
    [Number]                     INT             NOT NULL,
    [ExpiredDate]                DATETIME        NOT NULL,
    [Value]                      NUMERIC (18, 2) NOT NULL,
    [Balance]                    NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_Balance] DEFAULT ((0)) NOT NULL,
    [DebitValue]                 NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_DebitValue] DEFAULT ((0)) NOT NULL,
    [CreditValue]                NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_CreditValue] DEFAULT ((0)) NOT NULL,
    [TransferValue]              NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_TransferValue] DEFAULT ((0)) NOT NULL,
    [PaymentValue]               NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_PaymentValue] DEFAULT ((0)) NOT NULL,
    [CrossingValue]              NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableShare_CrossingValue] DEFAULT ((0)) NOT NULL,
    [InterestPaymentDate]        DATETIME        NULL,
    [InterestValue]              NUMERIC (18)    NULL,
    [SurchargesValue]            NUMERIC (18)    NULL,
    [CapitalRepaymentAgreement]  NUMERIC (18)    NULL,
    [FinancialInterest]          NUMERIC (18)    NULL,
    [RepaymentAgreementInterest] NUMERIC (18)    NULL,
    CONSTRAINT [PK_AccountReceivableShare__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableShare_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivableShare] NOCHECK CONSTRAINT [FK_AccountReceivableShare_AccountReceivable];


GO
CREATE NONCLUSTERED INDEX [IX_AccountReceivableShare__AccountReceivableId]
    ON [Portfolio].[AccountReceivableShare]([AccountReceivableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interés generado por acuerdo de pago, NUMERIC(18), aplicable a cuotas con plan de repago negociado en cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'RepaymentAgreementInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interes - Acuerdo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'RepaymentAgreementInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'RepaymentAgreementInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interés financiero pactado en acuerdo de pago, NUMERIC(18), componente de costo financiero de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'FinancialInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interes financiero - Acuerdo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'FinancialInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'FinancialInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de capital incluido en acuerdo de pago, NUMERIC(18), porción principal de la cuota a pagar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CapitalRepaymentAgreement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Capital - Acuerdo de pago', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CapitalRepaymentAgreement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CapitalRepaymentAgreement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de recargos, NUMERIC(18), multas o incrementos por mora o incumplimiento de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'SurchargesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Recargos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'SurchargesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'SurchargesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de intereses generados, NUMERIC(18,2), costo financiero acumulado sobre saldo de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'InterestValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Intereses', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'InterestValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'InterestValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de liquidación/cálculo de intereses, DATETIME NULL, cuándo se registró el interés en la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'InterestPaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de liquidacion de intereses', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'InterestPaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'InterestPaymentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor afectado por cruce de cuentas, NUMERIC(18,2), monto compensado entre deudor/acreedor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se ha afectado a través de los cruce de cuentas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor abono/pago realizado, NUMERIC(18,2) DEFAULT(0), monto pagado contra la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'PaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Abono', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'PaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'PaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor trasladado, NUMERIC(18,2) DEFAULT(0), monto movido entre cuotas o cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'TransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Traslado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'TransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'TransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor crédito, NUMERIC(18,2) DEFAULT(0), abono por ajuste o rectificación en favor del deudor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor débito/cargo, NUMERIC(18,2) DEFAULT(0), monto cargado o afectado a la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor debito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente de la cuota, NUMERIC(18,2) DEFAULT(0), diferencia entre valor original y pagos realizados', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el saldo ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la cuota, NUMERIC(18,2), monto inicial pactado para esta porción de la factura por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la cuota, DATETIME, plazo límite para el pago sin mora', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuota, INT, identificador secuencial dentro de la factura por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a cabecera de cuenta por cobrar, INT NOT NULL, referencia a [Portfolio].[AccountReceivable]', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de cuota, INT IDENTITY PK, clave primaria de la cuota en cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuotas o plazos de pago asociados a cuentas por cobrar. Registra el cronograma de pagos de una obligación financiera, incluyendo valores esperados, saldos pendientes, abonos, débitos, créditos, intereses y recargos por cada cuota o share de la deuda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableShare';
