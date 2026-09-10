CREATE TABLE [Portfolio].[Detallado2] (
    [AccountReceivableId]        NVARCHAR (255) NULL,
    [Number]                     NVARCHAR (255) NULL,
    [ExpiredDate]                DATETIME       NULL,
    [Value]                      FLOAT (53)     NULL,
    [Balance]                    FLOAT (53)     NULL,
    [DebitValue]                 NVARCHAR (255) NULL,
    [CreditValue]                NVARCHAR (255) NULL,
    [TransferValue]              NVARCHAR (255) NULL,
    [PaymentValue]               NVARCHAR (255) NULL,
    [CrossingValue]              NVARCHAR (255) NULL,
    [InterestPaymentDate]        NVARCHAR (255) NULL,
    [InterestValue]              NVARCHAR (255) NULL,
    [SurchargesValue]            NVARCHAR (255) NULL,
    [CapitalRepaymentAgreement]  NVARCHAR (255) NULL,
    [FinancialInterest]          NVARCHAR (255) NULL,
    [RepaymentAgreementInterest] NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intereses del acuerdo de amortización, capital e intereses pactados en el plan de pago (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'RepaymentAgreementInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los intereses del acuerdo de amortización.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'RepaymentAgreementInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'RepaymentAgreementInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intereses financieros generados por mora o financiación de la deuda, cálculo de tasa (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'FinancialInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena los intereses financieros.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'FinancialInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'FinancialInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acuerdo de amortización de capital, término pactado para el pago del principal de la factura/glosa (NVARCHAR).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CapitalRepaymentAgreement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el acuerdo de amortización de capital.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CapitalRepaymentAgreement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CapitalRepaymentAgreement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de recargos por mora, sanciones o ajustes administrativos aplicados a la cuenta (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'SurchargesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de los recargos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'SurchargesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'SurchargesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de interés acumulado o causado en el período contable (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'InterestValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de interés.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'InterestValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'InterestValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha pactada de pago de intereses en el acuerdo o factura (NVARCHAR, formato fecha).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'InterestPaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de pago de intereses.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'InterestPaymentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'InterestPaymentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del cruce o compensación de saldos entre cuentas (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del cruce.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CrossingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del pago realizado por el deudor, abono a la deuda (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'PaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'PaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'PaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del anticipo o traslado de fondos entre cuentas por cobrar (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'TransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del cruce de anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'TransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'TransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del crédito otorgado, línea de atención o servicio facturado (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del débito o cargo realizado a la cuenta por cobrar (NVARCHAR, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del débito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo general de la cuenta, diferencia entre créditos y débitos pendientes (FLOAT, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el saldo general.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del detalle de la transacción o rubro en el portafolio (FLOAT, valores monetarios).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la factura, glosa o acuerdo de pago (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de caducidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del detalle, identificador único del registro en Detallado2 (NVARCHAR).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la cuenta por cobrar, FK a tabla principal de cartera, cédula/factura (NVARCHAR, PII).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del portafolio de cartera por cuenta por cobrar: registra movimientos financieros de cada obligación pendiente, incluyendo valores originales, saldos, débitos, créditos, pagos, intereses, recargos y acuerdos de pago de capital.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2';
