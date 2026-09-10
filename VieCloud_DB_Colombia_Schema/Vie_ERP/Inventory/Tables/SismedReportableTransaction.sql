CREATE TABLE [Inventory].[SismedReportableTransaction] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BatchId]          INT           NOT NULL,
    [ProductId]        INT           NOT NULL,
    [TransactionType]  TINYINT       NOT NULL,
    [SourceDocumentId] INT           NOT NULL,
    [TransactionDate]  DATE          NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [ExclusionReason]  TINYINT       NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_SismedReportableTransaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SismedReportableTransaction_SismedConsolidationBatch] FOREIGN KEY ([BatchId]) REFERENCES [Inventory].[SismedConsolidationBatch] ([Id]),
    CONSTRAINT [FK_SismedReportableTransaction_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [UQ_SismedReportableTransaction_Source] UNIQUE ([BatchId], [TransactionType], [SourceDocumentId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada transaccion de medicamento evaluada por un lote de consolidacion SISMED (Circular 021 de 2026): su clasificacion final Reportable/Excluido y, cuando aplica, el motivo de exclusion. Es la fuente que consumira la generacion del archivo MED101.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedReportableTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lote de consolidacion (Inventory.SismedConsolidationBatch) al que pertenece esta transaccion.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedReportableTransaction', @level2type = N'COLUMN', @level2name = N'BatchId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto de medicamento asociado a la transaccion (Inventory.InventoryProduct).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedReportableTransaction', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de transaccion (TINYINT): 1-Compra (origen Inventory.EntranceVoucherDetail), 2-Venta por servicio clinico (origen Billing.ServiceOrderDetail), 3-Venta directa de farmacia (origen Inventory.DocumentInvoiceProductSalesDetail). Se separan los origenes de venta porque tienen espacios de Id independientes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedReportableTransaction', @level2type = N'COLUMN', @level2name = N'TransactionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la linea de detalle de origen que genero la transaccion (una fila por producto, no por documento); su tabla depende de TransactionType: EntranceVoucherDetail.Id para compras, ServiceOrderDetail.Id o DocumentInvoiceProductSalesDetail.Id para ventas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedReportableTransaction', @level2type = N'COLUMN', @level2name = N'SourceDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento de origen (fecha de la factura o del comprobante de entrada).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedReportableTransaction', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificacion final de reportabilidad (TINYINT): 1-Reportable, 2-Excluido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedReportableTransaction', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de exclusion regulatoria (TINYINT), solo aplica cuando Status es Excluido: 1-Preparacion Magistral, 2-Traslado Interno. Nulo cuando Status es Reportable.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedReportableTransaction', @level2type = N'COLUMN', @level2name = N'ExclusionReason';
GO