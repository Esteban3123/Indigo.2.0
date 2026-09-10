CREATE TABLE [GeneralLedger].[JournalVoucherTypeConsecutive] (
    [Id]                   INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [JournalVoucherTypeId] INT    NOT NULL,
    [Year]                 INT    NOT NULL,
    [Consecutive]          BIGINT NOT NULL,
    [LegalBookId]          INT    CONSTRAINT [DF_JournalVoucherTypeConsecutive_LegalBookId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_JournalVoucherTypeConsecutive] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JournalVoucherTypeConsecutive_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_JournalVoucherTypeConsecutive_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[JournalVoucherTypeConsecutive] NOCHECK CONSTRAINT [FK_JournalVoucherTypeConsecutive_LegalBook];




GO



GO
ALTER TABLE [GeneralLedger].[JournalVoucherTypeConsecutive] NOCHECK CONSTRAINT [FK_JournalVoucherTypeConsecutive_LegalBook];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_JournalVoucherTypeConsecutive]
    ON [GeneralLedger].[JournalVoucherTypeConsecutive]([JournalVoucherTypeId] ASC, [LegalBookId] ASC, [Year] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el libro legal o registro contable (diario contable, libro mayor, libro de inventarios); identifica el libro contable donde se registra el comprobante según regulaciones tributarias y de contabilidad (default=1).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo secuencial (BIGINT) del tipo de comprobante dentro del año/vigencia; identifica el orden de emisión o registro del comprobante contable para trazabilidad, auditoría y cumplimiento normativo RIPS o fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigencia o año del consecutivo; período fiscal o contable (ej: 2024) utilizado para resetear o separar secuencias de numeración de comprobantes por período anual según regulación tributaria y de auditoría.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia o año del consecutivo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el tipo de comprobante contable (diario, compra, venta, egreso, ingreso); vincula a tabla JournalVoucherTypes para determinar la naturaleza del asiento contable o transacción.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la tabla JournalVoucherTypeConsecutive; clave primaria clusterizada para auditoría y control de secuencia de comprobantes contables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivos por tipo de comprobante contable (voucher) y año. Controla la numeración secuencial de cada tipo de comprobante en el libro contable legal, garantizando la trazabilidad y orden de los registros del diario contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherTypeConsecutive';
