CREATE TABLE [Treasury].[RefundDetail] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RefundId]             INT NOT NULL,
    [VoucherTransactionId] INT NOT NULL,
    CONSTRAINT [PK_RefundDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RefundDetail_Refunds] FOREIGN KEY ([RefundId]) REFERENCES [Treasury].[Refunds] ([Id]),
    CONSTRAINT [FK_RefundDetail_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la transacción del comprobante de egreso (voucher), clave foránea que vincula al detalle del reembolso con la transacción contable de tesorería registrada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera del reembolso, clave foránea que asocia cada línea de detalle a su documento maestro de devolución o reintegro de fondos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'RefundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de ll cabecera del reembolso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'RefundId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'RefundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de detalle del reembolso, generado automáticamente por identidad incremental.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los reembolsos registrados en tesorería, asociando cada línea de devolución con la transacción de comprobante contable correspondiente.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail';
