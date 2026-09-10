CREATE TABLE [Payments].[PaymentTransferDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PaymentTransferId]      INT             NOT NULL,
    [AccountPayableId]       INT             NOT NULL,
    [AccountPayableShareId]  INT             NOT NULL,
    [MainAccountId]          INT             NOT NULL,
    [CostCenterId]           INT             NULL,
    [Value]                  DECIMAL (18, 2) NOT NULL,
    [RetentionConceptRTFId]  INT             NULL,
    [PercentageRTF]          NUMERIC (5, 2)  NULL,
    [ValueRTF]               DECIMAL (18, 2) NULL,
    [RetentionConceptICAId]  INT             NULL,
    [ValueICA]               DECIMAL (18, 2) NULL,
    [PercentageICA]          NUMERIC (5, 2)  NULL,
    [ValueDiscount]          DECIMAL (18, 2) NULL,
    [ValueOther]             DECIMAL (18, 2) NULL,
    [ValueUse]               DECIMAL (18, 2) NULL,
    [TRMValue]               NUMERIC (20, 5) CONSTRAINT [DF__PaymentTr__TRMVa__1FE9F193] DEFAULT ((0)) NOT NULL,
    [ValueInCurrencyInvoice] NUMERIC (18, 2) CONSTRAINT [DF__PaymentTr__Value__144334BD] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PaymentTransferDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentTransferDetail_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_AccountPayableShares] FOREIGN KEY ([AccountPayableShareId]) REFERENCES [Payments].[AccountPayableShares] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_PaymentTransfer] FOREIGN KEY ([PaymentTransferId]) REFERENCES [Payments].[PaymentTransfer] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_RetentionConcepts] FOREIGN KEY ([RetentionConceptRTFId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_PaymentTransferDetail_RetentionConcepts_ICA] FOREIGN KEY ([RetentionConceptICAId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PaymentTransferDetail__PaymentTransferId__AccountPayableId__AccountPayableShareId]
    ON [Payments].[PaymentTransferDetail]([PaymentTransferId] ASC, [AccountPayableId] ASC, [AccountPayableShareId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la factura convertido a la moneda de la factura original (NUMERIC 18,2). Monto total en divisa de origen del documento por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la factura con la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa Representativa del Mercado (TRM) aplicada al traslado de pago (NUMERIC 20,5). Factor de conversión de moneda extranjera para cálculo en pesos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor de la Tasa Representativa del Mercado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de aprovechamiento o utilización aplicado al detalle del pago (DECIMAL 18,2). Monto destinado a reducción o crédito por uso.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de aprovechamiento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros valores o conceptos adicionales no clasificados en categorías estándar (DECIMAL 18,2). Ajustes, sobretasas o conceptos complementarios.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros Valores', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueOther';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de descuento, bonificación o rebaja aplicada al monto por pagar (DECIMAL 18,2). Reducción en factura por pronto pago, volumen o bonificación comercial.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de descuento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de Impuesto al Consumo (ICA) a aplicar sobre el valor base (NUMERIC 5,2). Tasa municipal/distrital de retención impositiva en Colombia.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PercentageICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje aplicado al valor ICA', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PercentageICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PercentageICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del Impuesto al Consumo (ICA) retenido o aplicado en el traslado (DECIMAL 18,2). Monto de retención ICA según tarifa municipal.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del ICA', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención ICA configurado en GeneralLedger.RetentionConcepts (INT FK). Referencia a tipo/categoría de retención de ICA.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptICAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto aplicado al ICA', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptICAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptICAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la Retención en la Fuente (RTF) o retencion tributaria descontada del pago (DECIMAL 18,2). Monto de retención fiscal según tarifa de renta/IVA.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la retencion en la fuente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de Retención en la Fuente (RTF) aplicado al valor del pago (NUMERIC 5,2). Tasa de retención tributaria según concepto fiscal.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PercentageRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje aplicado a la retefuente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PercentageRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PercentageRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de Retención en la Fuente configurado en GeneralLedger.RetentionConcepts (INT FK). Referencia a tipo de retención tributaria.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptRTFId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto aplicado a la retencion a la fuente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptRTFId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptRTFId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base o principal que se afecta contablemente en el traslado de pago (DECIMAL 18,2). Monto del documento por pagar antes de impuestos y retenciones.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se va afectar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo o unidad funcional que se imputa (INT FK nullable). Centro de responsabilidad, sede, servicio o departamento en Payroll.CostCenter.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal afectada (INT FK). Referencia a cuenta de Mayor o auxiliar de cuentas por pagar en GeneralLedger.MainAccounts.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuota o distribución compartida de la cuenta por pagar (INT FK). Referencia a Payments.AccountPayableShares para asignación proporcional.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las Cuentas por pagar compartidas', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar raíz afectada en el traslado (INT FK). Referencia a Payments.AccountPayable que origina el pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar que se va afectar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o encabezado del traslado de pago (INT FK). Referencia a Payments.PaymentTransfer que agrupa detalles.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PaymentTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del traslado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PaymentTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'PaymentTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de traslado de pago (INT PK IDENTITY). Clave primaria, secuencia única por registro de distribución detallada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de los traslados de pagos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada línea dentro de una transferencia de pago: indica a qué cuenta por pagar y cuota se aplica el pago, con el valor pagado y los descuentos, retenciones (Retefuente e ICA) y otros conceptos deducidos, incluyendo la tasa de cambio cuando la factura está en moneda extranjera.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferDetail';
