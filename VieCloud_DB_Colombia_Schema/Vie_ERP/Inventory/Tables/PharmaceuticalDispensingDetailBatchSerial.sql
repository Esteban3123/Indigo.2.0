CREATE TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] (
    [Id]                               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingDetailId] INT NOT NULL,
    [PhysicalInventoryId]              INT NULL,
    [Quantity]                         INT NOT NULL,
    [OutstandingQuantity]              INT NOT NULL,
    [PhysicalInventoryCustodyId]       INT NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingDetailBatchSerial__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_PharmaceuticalDispensingDetailBatchSerial] CHECK ([OutstandingQuantity]>=(0)),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PharmaceuticalDispensingDetail] FOREIGN KEY ([PharmaceuticalDispensingDetailId]) REFERENCES [Inventory].[PharmaceuticalDispensingDetail] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody] FOREIGN KEY ([PhysicalInventoryCustodyId]) REFERENCES [Inventory].[PhysicalInventoryCustody] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [CK_PharmaceuticalDispensingDetailBatchSerial];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PharmaceuticalDispensingDetail];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventory];




GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [CK_PharmaceuticalDispensingDetailBatchSerial];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PharmaceuticalDispensingDetail];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventory];


GO



GO
CREATE NONCLUSTERED INDEX [IX_PharmaceuticalDispensingDetailBatchSerial__PharmaceuticalDispensingDetailId]
    ON [Inventory].[PharmaceuticalDispensingDetailBatchSerial]([PharmaceuticalDispensingDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) de la custodia/responsabilidad de inventario físico; asigna quien custodia el medicamento dispensado en farmacia o unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryCustodyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de inventario fisico de custodia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryCustodyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryCustodyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente (INT) del medicamento sin devolver; inicia igual a Quantity pero disminuye con cada devolución de dispensación farmacéutica (CHECK ≥ 0).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea la dispensacion este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad (INT) de unidades del medicamento/fármaco dispensadas en este detalle de lote/serie; cantidad inicial al crear la dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del inventario físico de farmacia; referencia al lote, serie o movimiento de stock del que se originó la dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del detalle de dispensación farmacéutica; vincula a la línea de medicamento/fármaco dispensado al paciente o unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la dispensacion farmacutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de cada registro de lote/serie en la tabla de detalle de dispensación farmacéutica con seguimiento de inventario físico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de lotes y seriales asociados a cada línea de dispensación farmacéutica. Registra qué unidades físicas del inventario (por lote o serial) fueron usadas en la dispensación de medicamentos, incluyendo cantidades dispensadas y pendientes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetailBatchSerial';

GO
CREATE NONCLUSTERED INDEX [IX_PharmaceuticalDispensingDetailBatchSerial_DetailId]
    ON [Inventory].[PharmaceuticalDispensingDetailBatchSerial]([PharmaceuticalDispensingDetailId] ASC)
    INCLUDE([PhysicalInventoryId], [PhysicalInventoryCustodyId], [Quantity], [OutstandingQuantity]);


GO
CREATE NONCLUSTERED INDEX [IX_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventory_Detail]
    ON [Inventory].[PharmaceuticalDispensingDetailBatchSerial]([PhysicalInventoryId] ASC, [PharmaceuticalDispensingDetailId] ASC)
    INCLUDE([Quantity], [OutstandingQuantity])
    WHERE [PhysicalInventoryId] IS NOT NULL;
