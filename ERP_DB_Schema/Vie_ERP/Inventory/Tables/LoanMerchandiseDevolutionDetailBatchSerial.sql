CREATE TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] (
    [Id]                                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDevolutionDetailId] INT NOT NULL,
    [PhysicalInventoryId]               INT NULL,
    [Quantity]                          INT NOT NULL,
    [BatchSerialId]                     INT NULL,
    CONSTRAINT [PK_LoanMerchandiseDevolutionDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_LoanMerchandiseDevolutionDetail] FOREIGN KEY ([LoanMerchandiseDevolutionDetailId]) REFERENCES [Inventory].[LoanMerchandiseDevolutionDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_LoanMerchandiseDevolutionDetail];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_PhysicalInventory];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_LoanMerchandiseDevolutionDetailBatchSerial]
    ON [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial]([LoanMerchandiseDevolutionDetailId] ASC, [PhysicalInventoryId] ASC, [BatchSerialId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote/serial, este campo solo se llena si es una entrada por devolución de un prestamo de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cantidad que se va a deolver del lote especifico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico, este campo solo se llena si es una salida por devolución de un prestamo de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDevolutionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

