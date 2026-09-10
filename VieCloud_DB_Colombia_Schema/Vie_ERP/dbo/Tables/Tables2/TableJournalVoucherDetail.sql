CREATE TABLE [dbo].[TableJournalVoucherDetail] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdJournalVoucher] INT             NULL,
    [IdMainAccount]    INT             NOT NULL,
    [IdThirdParty]     INT             NULL,
    [IdCostCenter]     INT             NULL,
    [DebitValue]       DECIMAL (18, 2) NOT NULL,
    [CreditValue]      DECIMAL (18, 2) NOT NULL,
    [Detail]           VARCHAR (MAX)   NULL,
    [IdRetention]      INT             NULL,
    [RetentionRate]    DECIMAL (5, 2)  NULL,
    [BaseValue]        DECIMAL (18)    NULL,
    [BillingValue]     DECIMAL (18)    NULL,
    CONSTRAINT [PK_TableJournalVoucherDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líneas de detalle de comprobantes contables (vouchers). Cada registro representa un movimiento débito o crédito de un comprobante, con su cuenta contable, tercero, centro de costos y valores de retención asociados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de la línea de detalle del comprobante contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al comprobante contable (voucher, asiento contable) al que pertenece esta línea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdJournalVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdJournalVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal afectada en este movimiento (plan de cuentas).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercero asociado a la línea contable: proveedor, cliente, paciente o entidad relacionada con el movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costos al que se imputa este movimiento contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor débito del movimiento contable en esta línea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor crédito del movimiento contable en esta línea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o concepto del movimiento contable; observaciones o glosa de la línea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de retención aplicada en esta línea (retefuente, reteiva, reteica u otra retención tributaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'IdRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o tarifa de retención aplicada sobre el valor base de la transacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'RetentionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'RetentionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base sobre el cual se calcula la retención o el movimiento contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado o de facturación asociado a esta línea del comprobante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TableJournalVoucherDetail', @level2type = N'COLUMN', @level2name = N'BillingValue';
