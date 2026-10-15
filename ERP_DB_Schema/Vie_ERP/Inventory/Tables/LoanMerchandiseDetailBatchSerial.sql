CREATE TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDetailId] INT NOT NULL,
    [BatchSerialId]           INT NULL,
    [PhysicalInventoryId]     INT NULL,
    [Quantity]                INT NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_LoanMerchandiseDetail] FOREIGN KEY ([LoanMerchandiseDetailId]) REFERENCES [Inventory].[LoanMerchandiseDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_LoanMerchandiseDetail];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] SET (LOCK_ESCALATION = AUTO);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico, este campo solo se llena si el prestamo es de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote/serial, este campo solo se llena si es de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle de la mercancía del préstamo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

