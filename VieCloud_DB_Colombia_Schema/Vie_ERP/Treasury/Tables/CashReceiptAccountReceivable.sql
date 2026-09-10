CREATE TABLE [Treasury].[CashReceiptAccountReceivable] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [CashReceiptDetailId]   INT             NOT NULL,
    [AccountReceivableId]   INT             NOT NULL,
    [InvoiceNumber]         VARCHAR (20)    NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [ValueInCurrencyHeader] NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_CashReceiptAccountReceivable__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptAccountReceivable_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_CashReceiptAccountReceivable_CashReceiptDetails] FOREIGN KEY ([CashReceiptDetailId]) REFERENCES [Treasury].[CashReceiptDetails] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor convertido a la moneda del encabezado del recibo de caja NUMERIC(20,5). Conversión monetaria aplicada según tasa de cambio del día de ingreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del encabezado de la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en moneda local del pago aplicado a la factura. Monto que se abona contra la deuda, factura o cuenta por cobrar pendiente.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se va a pagar de la factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura VARCHAR(20). Identificador comercial del documento de cobro: comprobante fiscal, factura de venta, recibo de facturación o número de radicado RIPS.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar afectada (FK a Portfolio.AccountReceivable). Referencia la factura o deuda que se está cancelando o abonando.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar que se va afectar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del recibo de caja (FK a Treasury.CashReceiptDetails). Vincula cada pago recibido con su línea de ingreso en tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación intermedia entre recibo de caja y cuenta por cobrar. Clave principal de la tabla de auditoría de pagos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla intermedia', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la aplicación de pagos en efectivo (recibos de caja) a cuentas por cobrar específicas, vinculando cada detalle de pago con la factura correspondiente y el valor abonado, tanto en moneda local como en la moneda del encabezado del recibo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable';

GO
CREATE NONCLUSTERED INDEX [IX_CashReceiptAccountReceivable_AccountReceivableId]
    ON [Treasury].[CashReceiptAccountReceivable]([AccountReceivableId] ASC)
    INCLUDE([CashReceiptDetailId], [Value]);
