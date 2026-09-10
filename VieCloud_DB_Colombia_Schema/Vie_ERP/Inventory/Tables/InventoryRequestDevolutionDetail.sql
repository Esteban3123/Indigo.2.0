CREATE TABLE [Inventory].[InventoryRequestDevolutionDetail] (
    [Id]                           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryRequestDevolutionId] INT NOT NULL,
    [InventoryRequestDetailId]     INT NOT NULL,
    [Quantity]                     INT NOT NULL,
    CONSTRAINT [PK_InventoryRequestDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryRequestDevolutionDetail_InventoryRequestDetail] FOREIGN KEY ([InventoryRequestDetailId]) REFERENCES [Inventory].[InventoryRequestDetail] ([Id]),
    CONSTRAINT [FK_InventoryRequestDevolutionDetail_InventoryRequestDevolution] FOREIGN KEY ([InventoryRequestDevolutionId]) REFERENCES [Inventory].[InventoryRequestDevolution] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades devueltas en el detalle de devolución de solicitud de inventario. Tipo: INT. Representa el número de artículos/productos retornados al almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de solicitud de inventario asociado a la devolución. Clave foránea (FK) que referencia [Inventory].[InventoryRequestDetail]. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la devolución de solicitud de inventario padre a la cual pertenece este detalle. Clave foránea (FK) que referencia [Inventory].[InventoryRequestDevolution]. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'InventoryRequestDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la fila de detalle de devolución de solicitud de inventario. Tipo: INT. Clave primaria (PK) de la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las devoluciones de solicitudes de inventario. Registra cada ítem devuelto, indicando a qué devolución y a qué línea de solicitud original pertenece, junto con la cantidad devuelta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequestDevolutionDetail';
