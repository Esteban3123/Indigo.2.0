CREATE TABLE [Treasury].[DischargeBill] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherTransactionD]         INT             NOT NULL,
    [IdAccountPayable]              INT             NOT NULL,
    [IdAccountPayableShare]         INT             NOT NULL,
    [AdvancedValue]                 DECIMAL (18, 2) NOT NULL,
    [AdvancePercent]                DECIMAL (5, 2)  NOT NULL,
    [IdPaymentConcept]              INT             NOT NULL,
    [BaseValueDiscount]             DECIMAL (18, 2) NOT NULL,
    [DiscountPercent]               DECIMAL (5, 2)  NOT NULL,
    [PaymentOrderValue]             DECIMAL (18)    CONSTRAINT [DF_DischargeBill_PaymentOrderValue] DEFAULT ((0)) NOT NULL,
    [ValueInCurrencyHeader]         DECIMAL (18, 5) CONSTRAINT [DF__Discharge__Value__2641E967] DEFAULT ((0)) NULL,
    [TRMValue]                      NUMERIC (20, 5) CONSTRAINT [DF__Discharge__TRMVa__27360DA0] DEFAULT ((1)) NOT NULL,
    [ValueDiscountInCurrencyHeader] DECIMAL (18, 5) CONSTRAINT [DF__Discharge__Value__291E5612] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_DischargeBill] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DischargeBill_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_DischargeBill_AccountPayableShares] FOREIGN KEY ([IdAccountPayableShare]) REFERENCES [Payments].[AccountPayableShares] ([Id]),
    CONSTRAINT [FK_DischargeBill_TreasuryPaymentConcept] FOREIGN KEY ([IdPaymentConcept]) REFERENCES [Treasury].[TreasuryPaymentConcepts] ([Id]),
    CONSTRAINT [FK_DischargeBill_VoucherTransactionD] FOREIGN KEY ([IdVoucherTransactionD]) REFERENCES [Treasury].[VoucherTransactionDetails] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de descuento convertido a la moneda de la cabecera del comprobante. DECIMAL(18,5), permite búsqueda por glosas, deducciones o descuentos en divisas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'ValueDiscountInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de descuento en la moneda de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'ValueDiscountInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'ValueDiscountInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cambio representativo del mercado (TRM) aplicado para conversión de valores. NUMERIC(20,5), default 1. Búsqueda: tasa de cambio, moneda extranjera, conversión.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor en el tipo de la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total convertido a la moneda de la cabecera del comprobante de egreso. DECIMAL(18,5), búsqueda: monto en divisa, factura en moneda extranjera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor en la moneda de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor neto que interfacea con presupuesto al generar la orden de pago. DECIMAL(18), default 0. Búsqueda: monto para pagar, orden de egreso, interfaz presupuestal.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'PaymentOrderValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor por el cual se hará interface en presupuesto en la generación de la orden de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'PaymentOrderValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'PaymentOrderValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento o deducción aplicado a la factura. DECIMAL(5,2). Búsqueda: rebaja, glosa, descuento porcentual.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'DiscountPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'DiscountPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'DiscountPercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base sobre el cual se calcula el descuento o deducción. DECIMAL(18,2). Búsqueda: monto base, valor antes de glosa, factura sin descuento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'BaseValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor base  de descuento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'BaseValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'BaseValueDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de pago (FK Treasury.TreasuryPaymentConcepts). INT. Búsqueda: tipo de pago, concepto, naturaleza de la transacción.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdPaymentConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del concepto de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdPaymentConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdPaymentConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de abono o anticipo aplicado a la cuota de factura. DECIMAL(5,2). Búsqueda: porcentaje de pago, adelanto, cuota.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'AdvancePercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'porcentaje de abono', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'AdvancePercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'AdvancePercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de abono o anticipo aplicado a la cuota de la factura por pagar. DECIMAL(18,2). Búsqueda: adelanto, pago parcial, cuota pagada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'AdvancedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abono aplicado a la cuota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'AdvancedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'AdvancedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuota o compartición de la factura por pagar (FK Payments.AccountPayableShares). INT. Búsqueda: cuota, compartida, factura por cuota.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de las coutas de las facturas', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura de pago o cuentas por pagar (FK Payments.AccountPayable). INT. Búsqueda: factura, cuotas, cuentas por pagar, acreedor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la factura de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del comprobante de egreso o voucher de salida (FK Treasury.VoucherTransactionDetails). INT. Búsqueda: comprobante, egreso, detalle de gasto.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del detalle del comprobante de egreso relacionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la factura de egresos o descargo. INT PRIMARY KEY IDENTITY. Búsqueda: factura de egreso, comprobante, descargo de pago.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la factura de egresos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de liquidación y descargo de facturas en tesorería. Registra los valores de anticipo, descuentos y órdenes de pago asociados a cuentas por pagar en el proceso de egreso o cierre de facturación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'DischargeBill';
