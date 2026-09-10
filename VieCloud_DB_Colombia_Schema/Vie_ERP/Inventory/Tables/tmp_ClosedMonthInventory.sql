CREATE TABLE [Inventory].[tmp_ClosedMonthInventory] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [IdRelacion]       INT             NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ProductCost]      DECIMAL (18, 2) NULL,
    [FinalProductCost] NUMERIC (18, 2) NULL,
    [SellingPrice]     NUMERIC (18, 2) NULL,
    [ProductId]        INT             NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [WareHouseId]      INT             NULL,
    CONSTRAINT [PK_Tmp_ClosedMonthInventory] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla temporal de trabajo que almacena el inventario correspondiente a un mes cerrado contablemente. Registra, por producto y almacén, la cantidad disponible junto con el costo unitario, el costo final calculado y el precio de venta asociados al cierre de periodo. Se relaciona con un registro de mes cerrado (`ClosedMonthId`) y con una relación de inventario (`IdRelacion`), sirviendo de apoyo a procesos de cálculo o consolidación de cierres mensuales.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'tmp_ClosedMonthInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'tmp_ClosedMonthInventory';
GO
