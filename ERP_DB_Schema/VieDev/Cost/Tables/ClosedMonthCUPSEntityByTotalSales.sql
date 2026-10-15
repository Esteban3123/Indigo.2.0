CREATE TABLE [Cost].[ClosedMonthCUPSEntityByTotalSales] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [CUPSEntityId]     INT             NOT NULL,
    [TotalGlobalSales] DECIMAL (18, 2) NOT NULL,
    [Quantity]         INT             NOT NULL,
    [AverageSales]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CloseMonthCUPSEntityByTotalSales] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthCUPSEntityByTotalSales_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntityByTotalSales_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es el promedio de el valor de venta global y las cantidades totales por actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'AverageSales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la cantidad de cada CUPS', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el valor de venta por actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'TotalGlobalSales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del CUPS asociado a la totalidad de la venta', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del cierre de mes asociado a la totalidad de la venta', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador unico de la tabla del valor de venta totalizado de cada CUPS', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'Id';

