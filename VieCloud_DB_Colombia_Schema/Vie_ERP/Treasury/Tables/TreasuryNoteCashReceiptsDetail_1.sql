CREATE TABLE [Treasury].[TreasuryNoteCashReceiptsDetail] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [TreasuryNoteId] INT NOT NULL,
    [CashReceiptsId] INT NOT NULL,
    CONSTRAINT [PK_TreasuryNoteCashReceiptsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryNoteCashReceiptsDetail_CashReceipts] FOREIGN KEY ([CashReceiptsId]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_TreasuryNoteCashReceiptsDetail_TreasuryNote] FOREIGN KEY ([TreasuryNoteId]) REFERENCES [Treasury].[TreasuryNote] ([Id]),
    CONSTRAINT [UQ_TreasuryNoteCashReceiptsDetail] UNIQUE NONCLUSTERED ([TreasuryNoteId] ASC, [CashReceiptsId] ASC)
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Recibo de Caja (FK → Treasury.CashReceipts). Vincula el ingreso, recaudo, pago recibido o depósito de efectivo registrado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion a CashReceipts.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'CashReceiptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Nota de Tesorería (FK → Treasury.TreasuryNote). Vincula el documento de tesorería, movimiento, comprobante o asiento contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'TreasuryNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion a TreasuryNote.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'TreasuryNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'TreasuryNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY). Clave primaria de la relación puente entre Nota de Tesorería y Recibo de Caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'LLave primaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla puente de asociación entre Notas de Tesorería y Recibos de Caja. Vincula documentos de tesorería con ingresos/recaudos de efectivo registrados. Relación muchos-a-muchos con restricción de unicidad.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla puente: relación Nota de Tesorería ↔ Recibo de Caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNoteCashReceiptsDetail';

