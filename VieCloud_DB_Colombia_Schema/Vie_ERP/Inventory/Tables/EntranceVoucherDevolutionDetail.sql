CREATE TABLE [Inventory].[EntranceVoucherDevolutionDetail] (
    [Id]                                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherDevolutionId]        INT NOT NULL,
    [EntranceVoucherDetailBatchSerialId] INT NOT NULL,
    [Quantity]                           INT NOT NULL,
    [DevolutionCauseId]                  INT NULL,
    CONSTRAINT [PK_EntranceVoucherDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDevolutionDetail_DevolutionCause] FOREIGN KEY ([DevolutionCauseId]) REFERENCES [Inventory].[DevolutionCause] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolutionDetail_EntranceVoucherDetailBatchSerial] FOREIGN KEY ([EntranceVoucherDetailBatchSerialId]) REFERENCES [Inventory].[EntranceVoucherDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolutionDetail_EntranceVoucherDevolution] FOREIGN KEY ([EntranceVoucherDevolutionId]) REFERENCES [Inventory].[EntranceVoucherDevolution] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la causa o motivo de devolución (defecto, vencimiento, daño, sobrante). Referencia a Inventory.DevolutionCause. Nullable, permite clasificar por qué se devuelve el lote/serial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DevolutionCauseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la causa de devolución asociada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DevolutionCauseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DevolutionCauseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del lote/serial a devolver en esta línea de devolución. Tipo INT, debe ser positivo y menor o igual al stock disponible del detalle original.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de entrada con lote/serial específico. Referencia FK a Inventory.EntranceVoucherDetailBatchSerial, vincula la devolución al movimiento de compra original.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del detalle del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante (orden) de devolución principal. Referencia FK a Inventory.EntranceVoucherDevolution, agrupa múltiples líneas de devolución de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de esta línea de detalle de devolución. Clave primaria de la tabla EntranceVoucherDevolutionDetail.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las devoluciones de comprobantes de entrada al inventario. Registra cada ítem devuelto, indicando el lote o serial afectado, la cantidad devuelta y el motivo de la devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail';
