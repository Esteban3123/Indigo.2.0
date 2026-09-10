CREATE TABLE [Treasury].[CheckCashingControlDetail] (
    [Id]                    INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCheckCashingControl] INT     NOT NULL,
    [IdVoucherTransaction]  INT     NOT NULL,
    [IdCheckBook]           INT     NOT NULL,
    [CheckNumber]           BIGINT  NOT NULL,
    [PreviousCheckStatus]   TINYINT NOT NULL,
    [CurrentCheckStatus]    TINYINT NOT NULL,
    CONSTRAINT [PK_CheckCashingControlDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckCashingControlDetail_Checkbooks] FOREIGN KEY ([IdCheckBook]) REFERENCES [Treasury].[Checkbooks] ([Id]),
    CONSTRAINT [FK_CheckCashingControlDetail_CheckCashingControl] FOREIGN KEY ([IdCheckCashingControl]) REFERENCES [Treasury].[CheckCashingControl] ([Id]),
    CONSTRAINT [FK_CheckCashingControlDetail_VoucherTransaction] FOREIGN KEY ([IdVoucherTransaction]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);
GO
CREATE NONCLUSTERED INDEX [IX_CheckCashingControlDetail_IdCheckCashingControl]
    ON [Treasury].[CheckCashingControlDetail]([IdCheckCashingControl] ASC);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del cheque (TINYINT): situación vigente del documento (emitido, cobrado, anulado, devuelto, compensado). Indica el estado en el que se encuentra el cheque en el momento del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'CurrentCheckStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de cheque actual', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'CurrentCheckStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'CurrentCheckStatus';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado anterior del cheque (TINYINT): situación previa del documento antes del cambio. Permite auditar transiciones de estado (emitido→cobrado, cobrado→devuelto, etc.).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'PreviousCheckStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de cheque anterior', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'PreviousCheckStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'PreviousCheckStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de chequera (INT, FK Treasury.Checkbooks): referencia a la chequera o talonario de origen del cheque. Vincula el documento a su libro de control.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdCheckBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de chequera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdCheckBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdCheckBook';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de comprobante de egreso (INT, FK Treasury.VoucherTransaction): referencia al comprobante contable de administración de efectivo. Vincula el cheque a la transacción de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdVoucherTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de comprobante de egreso de administración de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdVoucherTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdVoucherTransaction';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de control de cheques (INT, FK Treasury.CheckCashingControl): referencia al lote o proceso de control de cobro/compensación. Agrupa detalles en un evento de control.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdCheckCashingControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de control de cheques', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdCheckCashingControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'IdCheckCashingControl';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro (INT, PK): identificador único de esta línea de detalle en el control de cheques. Clave primaria de la tabla de auditoría.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los cheques procesados en cada control de cobro de caja (canje de cheques). Registra el estado anterior y actual de cada cheque involucrado en una transacción de tesorería, permitiendo trazabilidad del cambio de estado de los cheques.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingControlDetail';
