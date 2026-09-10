CREATE TABLE [Portfolio].[AccountReceivableIVACollected] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId]            INT             NOT NULL,
    [CashReceiptAccountReceivableId] INT             NULL,
    [PortfolioTransferDetailId]      INT             NULL,
    [Value]                          DECIMAL (18, 2) NOT NULL,
    [Status]                         BIT             NOT NULL,
    CONSTRAINT [PK_AccountReceivableIVACollected] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableIVACollected_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableIVACollected_CashReceiptAccountReceivable] FOREIGN KEY ([CashReceiptAccountReceivableId]) REFERENCES [Treasury].[CashReceiptAccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableIVACollected_PortfolioTransferDetail] FOREIGN KEY ([PortfolioTransferDetailId]) REFERENCES [Portfolio].[PortfolioTransferDetail] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del impuesto IVA cobrado: 0=Reversado/Anulado, 1=Confirmado/Vigente. Indica si el pago de IVA fue validado o cancelado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del pago:  0 - Reversado  1 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos del Impuesto al Valor Agregado (IVA) recaudado/pagado por el deudor. Tipo DECIMAL(18,2).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de IVA pagado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del cruce de anticipo o transferencia de cartera utilizado para cancelar el IVA. Referencia FK a PortfolioTransferDetail.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'PortfolioTransferDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del cruce de anticipo que se uso para pagar el IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'PortfolioTransferDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'PortfolioTransferDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del recibo de caja/ingreso de efectivo aplicado al pago del IVA. Referencia FK a CashReceiptAccountReceivable en Treasury.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'CashReceiptAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del recibo de caja que se uso para pagar el IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'CashReceiptAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'CashReceiptAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura, cuenta por cobrar o deuda asociada al IVA recaudado. Referencia FK a AccountReceivable. Clave para rastrear deudor/paciente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del registro de IVA cobrado. Clave primaria de la transacción de recaudación de impuesto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del IVA recaudado asociado a cuentas por cobrar en cartera. Vincula cada cuenta por cobrar con el recibo de caja o la transferencia de cartera correspondiente, indicando el valor del IVA cobrado y si el registro está activo o inactivo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected';
