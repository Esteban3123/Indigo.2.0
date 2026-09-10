CREATE TABLE [Treasury].[CardCollectionDetails] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [CardCollectionId] INT NOT NULL,
    [CashReceiptId]    INT NOT NULL,
    CONSTRAINT [PK_CardCollectionDetails_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CardCollectionDetails_CardCollections] FOREIGN KEY ([CardCollectionId]) REFERENCES [Treasury].[CardCollections] ([Id]),
    CONSTRAINT [FK_CardCollectionDetails_CashReceipts] FOREIGN KEY ([CashReceiptId]) REFERENCES [Treasury].[CashReceipts] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CardCollectionDetails_Unique]
    ON [Treasury].[CardCollectionDetails]([CardCollectionId] ASC, [CashReceiptId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de detalle en la tabla CardCollectionDetails. Clave primaria que identifica cada línea de detalle en la colección de tarjetas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la colección de tarjetas (FK a Treasury.CardCollections). Referencia a la cabecera que agrupa los detalles de cobro por tarjeta de crédito/débito.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'CardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la cabecera (FK a CardCollections)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'CardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'CardCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del recibo de efectivo o comprobante de pago (FK a Treasury.CashReceipts). Vincula cada detalle a su recibo de caja correspondiente en tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'CashReceiptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del recibo de efectivo (FK a CashReceipts)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'CashReceiptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'CashReceiptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las colecciones o recaudos realizados con tarjeta, asociando cada línea de cobro con su recibo de caja correspondiente. Permite identificar qué pagos con tarjeta componen un recaudo y a qué comprobante de ingreso pertenecen.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails';
