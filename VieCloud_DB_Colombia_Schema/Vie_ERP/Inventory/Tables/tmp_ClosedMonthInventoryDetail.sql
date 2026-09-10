CREATE TABLE [Inventory].[tmp_ClosedMonthInventoryDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [Quantity]               INT             NOT NULL,
    [WarehouseId]            INT             NOT NULL,
    [BatchSerialId]          INT             NULL,
    [ClosedMonthInventoryId] INT             NOT NULL,
    [CostTotal]              DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_Tmp_Inventory_ClosedMonthInventoryDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla temporal que almacena el detalle de inventarios de meses cerrados, registrando cantidades, costos totales y su asociación con un almacén específico y un lote o número de serie. Sirve como área de trabajo transitoria durante el proceso de cierre mensual de inventario, vinculando cada línea de detalle con su registro de cierre mensual correspondiente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'tmp_ClosedMonthInventoryDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'tmp_ClosedMonthInventoryDetail';
GO
