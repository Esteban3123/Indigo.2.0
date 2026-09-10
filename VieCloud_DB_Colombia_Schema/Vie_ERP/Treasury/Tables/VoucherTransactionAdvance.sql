CREATE TABLE [Treasury].[VoucherTransactionAdvance] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherTransactionD] INT             NOT NULL,
    [PortfolioAdvanceId]    INT             NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [Percentage]            NUMERIC (5, 2)  NOT NULL,
    CONSTRAINT [PK_VoucherTransactionAdvance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VoucherTransactionAdvance_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_VoucherTransactionAdvance_VoucherTransactionDetails] FOREIGN KEY ([IdVoucherTransactionD]) REFERENCES [Treasury].[VoucherTransactionDetails] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de aplicación del anticipo sobre el valor total del comprobante de egreso. Tipo: NUMERIC(5,2). Rango 0-100%.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del anticipo aplicado en la transacción de egreso. Tipo: DECIMAL(18,2). Monto en pesos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del anticipo del cartera/portafolio relacionado. Clave foránea a Portfolio.PortfolioAdvance. Anticipo de paciente o contratante.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Anticipo del Paciente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del comprobante de egreso (voucher) asociado. Clave foránea a Treasury.VoucherTransactionDetails. Vínculo con egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del detalle del comprobante de egreso relacionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y secuencial (IDENTITY) del registro de adelanto aplicado en transacción de comprobante. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los anticipos aplicados a transacciones de comprobantes de tesorería, vinculando cada movimiento contable con un anticipo de cartera, su valor y el porcentaje correspondiente.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance';
