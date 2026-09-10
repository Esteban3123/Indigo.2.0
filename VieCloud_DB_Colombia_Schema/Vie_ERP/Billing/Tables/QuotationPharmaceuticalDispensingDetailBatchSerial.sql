CREATE TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] (
    [Id]                                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuotationPharmaceuticalDispensingDetailId] INT NOT NULL,
    [PhysicalInventoryId]                       INT NULL,
    [Quantity]                                  INT NOT NULL,
    [OutstandingQuantity]                       INT NOT NULL,
    [PhysicalInventoryCustodyId]                INT NULL,
    CONSTRAINT [PK_QuotationPharmaceuticalDispensingDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody] FOREIGN KEY ([PhysicalInventoryCustodyId]) REFERENCES [Inventory].[PhysicalInventoryCustody] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_QuotationPharmaceuticalDispensingDetail] FOREIGN KEY ([QuotationPharmaceuticalDispensingDetailId]) REFERENCES [Billing].[QuotationPharmaceuticalDispensingDetail] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de custodia/responsable del inventario físico; referencia al custodio o depósito que guarda el lote/serie del medicamento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryCustodyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de inventario fisico de custodia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryCustodyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryCustodyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente (INT) del medicamento; inicia igual a Quantity al crear la dispensación y disminuye con cada devolución o ajuste de dispensación farmacéutica realizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea la dispensacion este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad (INT) de unidades de medicamento/fármaco dispensadas en este registro de lote/serie; valor inicial e inmutable del movimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del registro de inventario físico; referencia al lote, serie o movimiento de existencia en bodega/farmacia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de dispensación farmacéutica asociado; referencia a línea de medicamento/fármaco en cotización de dispensación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'QuotationPharmaceuticalDispensingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la dispensacion farmacutica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'QuotationPharmaceuticalDispensingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'QuotationPharmaceuticalDispensingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de lote/serie en la tabla de detalle de dispensación farmacéutica por cotización.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lotes y series del detalle de dispensación farmacéutica en cotizaciones. Registra qué lote o número de serie del inventario físico se asignó a cada ítem de medicamento o insumo dispensado en una cotización, junto con las cantidades dispensadas y pendientes por despachar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetailBatchSerial';
