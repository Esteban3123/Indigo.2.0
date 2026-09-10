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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Promedio de venta unitaria calculado como TotalGlobalSales / Quantity por CUPS y período de cierre. Tipo: DECIMAL(18,2). Representa el valor medio de cada procedimiento, actividad o servicio facturado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'AverageSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es el promedio de el valor de venta global y las cantidades totales por actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'AverageSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'AverageSales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de unidades, registros o procedimientos CUPS ejecutados en el período cerrado. Tipo: INT. Sinónimos: número de actos, volumen de servicios, transacciones CUPS.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la cantidad de cada CUPS', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de venta, ingresos o facturación acumulada por actividad CUPS en el mes cerrado. Tipo: DECIMAL(18,2). Refleja el monto global antes de glosas, descuentos o ajustes contractuales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'TotalGlobalSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor de venta por actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'TotalGlobalSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'TotalGlobalSales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del CUPS (código, procedimiento, actividad o servicio de salud) asociado al registro de ventas. FK a [Contract].[CUPSEntity]. Vincula cada línea de venta con su descripción de procedimiento.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS asociado a la totalidad de la venta', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del período de cierre mensual al que pertenecen los totales de venta. FK a [Cost].[ClosedMonth]. Agrupa ventas por mes contable, facturación o período de corte.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cierre de mes asociado a la totalidad de la venta', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) que representa cada combinación única de ClosedMonthId y CUPSEntityId con su totalización de venta global y promedio unitario por actividad.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador unico de la tabla del valor de venta totalizado de cada CUPS', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolidado de ventas por código CUPS y entidad en meses cerrados: registra el total de ventas, cantidad de servicios facturados y promedio de venta por procedimiento o servicio (CUPS) agrupado por entidad y mes cerrado contablemente, asociado opcionalmente a un contrato específico.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByTotalSales';
