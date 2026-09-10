CREATE TABLE [Treasury].[CashReceiptAccountReceivableShare] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CashReceiptAccountReceivableId] INT             NOT NULL,
    [AccountReceivableShareId]       INT             NOT NULL,
    [Value]                          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CashReceiptAccountReceivableShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptAccountReceivableShare_AccountReceivableShare] FOREIGN KEY ([AccountReceivableShareId]) REFERENCES [Portfolio].[AccountReceivableShare] ([Id]),
    CONSTRAINT [FK_CashReceiptAccountReceivableShare_CashReceiptAccountReceivable] FOREIGN KEY ([CashReceiptAccountReceivableId]) REFERENCES [Treasury].[CashReceiptAccountReceivable] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) a afectar, asignar o imputar a la cuota de cuenta por cobrar; monto del pago, abono o acreditación en pesos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se va afectar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cuota/plazo de la cuenta por cobrar a afectar; referencia a Portfolio.AccountReceivableShare para vinculación de pagos a cuotas específicas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuota de la cuenta por pagar que se va afectar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle/línea del recibo de caja de cuenta por cobrar; referencia a Treasury.CashReceiptAccountReceivable para trazabilidad del ingreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CashReceiptAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detallle del recibo de caja de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CashReceiptAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CashReceiptAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la relación entre recibo de caja y cuota de cuenta por cobrar; clave primaria de la tabla de asignación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la distribución o reparto de un recibo de caja entre las diferentes cuotas o participaciones de cuentas por cobrar, indicando qué monto de un pago recibido corresponde a cada porción de deuda o factura pendiente.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare';
