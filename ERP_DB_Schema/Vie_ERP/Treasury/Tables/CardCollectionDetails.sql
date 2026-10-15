CREATE TABLE [Treasury].[CardCollectionDetails] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CardCollectionId]  INT NOT NULL,
    [CashReceiptId]     INT NOT NULL,
    CONSTRAINT [PK_CardCollectionDetails__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CardCollectionDetails_CardCollections] FOREIGN KEY ([CardCollectionId]) REFERENCES [Treasury].[CardCollections] ([Id]),
    CONSTRAINT [FK_CardCollectionDetails_CashReceipts] FOREIGN KEY ([CashReceiptId]) REFERENCES [Treasury].[CashReceipts] ([Id])
);


GO
ALTER TABLE [Treasury].[CardCollectionDetails] NOCHECK CONSTRAINT [FK_CardCollectionDetails_CardCollections];


GO
ALTER TABLE [Treasury].[CardCollectionDetails] NOCHECK CONSTRAINT [FK_CardCollectionDetails_CashReceipts];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CardCollectionDetails_Unique]
    ON [Treasury].[CardCollectionDetails]([CardCollectionId] ASC, [CashReceiptId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación de la tabla detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la cabecera (FK a CardCollections)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'CardCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del recibo de efectivo (FK a CashReceipts)', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCollectionDetails', @level2type = N'COLUMN', @level2name = N'CashReceiptId';
