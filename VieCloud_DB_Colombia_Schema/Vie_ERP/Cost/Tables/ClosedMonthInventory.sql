CREATE TABLE [Cost].[ClosedMonthInventory] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]        INT             NOT NULL,
    [CostCenterId]         INT             NOT NULL,
    [CostInventoryGroupId] INT             NOT NULL,
    [AverageCost]          NUMERIC (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthInventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventory_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventory_CostInventoryGroup] FOREIGN KEY ([CostInventoryGroupId]) REFERENCES [Cost].[CostInventoryGroup] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo promedio ponderado del grupo de artículos/productos calculado al cierre de mes. Tipo: NUMERIC(24,6). Refleja el promedio ponderado de costos de cierre considerando equivalencias y valorización de inventario.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio del grupo de artículos, este se calcula mediante el promedio ponderado del costo promedio de cierre de mes de los productos usando las equivalencias establecidas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del grupo de productos o artículos de inventario. Referencia a Cost.CostInventoryGroup. Clasifica artículos por categoría de costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de producto', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del centro de costo o unidad funcional responsable. Referencia a Payroll.CostCenter. Permite asignar costos a departamentos, servicios o centros de atención.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del mes cerrado contable. Referencia a Cost.ClosedMonth. Período fiscal en el que se registra el inventario y su valorización.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de este registro de inventario cerrado. Clave primaria clustered de la tabla ClosedMonthInventory.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del costo promedio de inventario por centro de costo y grupo de inventario al cierre de cada mes contable. Permite consultar el valor histórico de los ítems de inventario (medicamentos, insumos) al momento del cierre periódico.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory';
