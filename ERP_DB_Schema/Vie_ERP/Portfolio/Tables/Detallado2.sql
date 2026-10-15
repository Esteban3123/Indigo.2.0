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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda los intereses del acuerdo de amortización.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'RepaymentAgreementInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los intereses financieros.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'FinancialInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el acuerdo de amortización de capital.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CapitalRepaymentAgreement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de los recargos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'SurchargesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de interés.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'InterestValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de pago de intereses.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'InterestPaymentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del cruce.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CrossingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'PaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del cruce de anticipo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'TransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del débito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el saldo general.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de caducidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero del detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Detallado2', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';

