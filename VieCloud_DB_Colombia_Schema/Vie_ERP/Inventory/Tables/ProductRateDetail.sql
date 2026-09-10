CREATE TABLE [Inventory].[ProductRateDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductRateId]           INT             NOT NULL,
    [ProductId]               INT             NULL,
    [InitialDate]             DATETIME        NOT NULL,
    [EndDate]                 DATETIME        NOT NULL,
    [SalesValue]              NUMERIC (18, 2) NOT NULL,
    [SalesValueWithSurcharge] NUMERIC (18, 2) CONSTRAINT [DF_ProductRateDetail_SalesValueWithSurcharge] DEFAULT ((0)) NOT NULL,
    [Contracted]              BIT             CONSTRAINT [DF_ProductRateDetail_Contracted] DEFAULT ((1)) NOT NULL,
    [Quoted]                  BIT             CONSTRAINT [DF_ProductRateDetail_Quoted] DEFAULT ((0)) NOT NULL,
    [Observations]            VARCHAR (MAX)   NULL,
    [LiquidationType]         TINYINT         CONSTRAINT [DF_ProductRateDetail_LiquidationType] DEFAULT ((1)) NOT NULL,
    [RateType]                TINYINT         CONSTRAINT [DF_ProductRateDetail_RateType] DEFAULT ((1)) NOT NULL,
    [PercentageBasedOn]       TINYINT         CONSTRAINT [DF_ProductRateDetail_PercentageBasedOn] DEFAULT ((0)) NOT NULL,
    [Percentage]              NUMERIC (6, 2)  NULL,
    [CupsId]                  INT             NULL,
    [ContractDescriptionId]   INT             NULL,
    [Status]                  TINYINT         CONSTRAINT [DF_ProductRateDetail_Status] DEFAULT ((1)) NOT NULL,
    [RateClass]               TINYINT         CONSTRAINT [DF_RateClass] DEFAULT ((1)) NULL,
    [PackageId]               INT             NULL,
    [DoseType]                TINYINT         NULL,
    CONSTRAINT [PK_ProductRateDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductRateDetail_ContractDescription] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_ProductRateDetail_Cups] FOREIGN KEY ([CupsId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ProductRateDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductRateDetail_Package] FOREIGN KEY ([PackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_ProductRateDetail_ProductRate] FOREIGN KEY ([ProductRateId]) REFERENCES [Inventory].[ProductRate] ([Id])
);




GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ProductRateDetail__ProductId__InitialDate__EndDate]
    ON [Inventory].[ProductRateDetail]([ProductId] ASC, [InitialDate] ASC, [EndDate] ASC);


GO
CREATE NONCLUSTERED INDEX [INDEX_PRODUCTRATE]
    ON [Inventory].[ProductRateDetail]([ProductRateId] ASC)
    INCLUDE([ProductId], [LiquidationType], [RateType], [PercentageBasedOn], [Percentage], [CupsId], [ContractDescriptionId], [InitialDate], [EndDate], [SalesValue], [SalesValueWithSurcharge], [Contracted], [Quoted], [Observations], [Status], [RateClass], [PackageId], [DoseType]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dosis (TINYINT): 1=No Aplica, 2=Estándar, 3=Personalizada. Controla si la dosis es fija o adaptable al paciente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'DoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: No Aplica  2: Estandar  3: Personalizada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'DoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'DoseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paquete (FK → MixingStation.Package). Agrupa productos/servicios vendidos como unidad integrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'PackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de tarifa (TINYINT): 1=Producto individual, 2=Paquete. Determina si la rata cubre un artículo o un conjunto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'RateClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de tarifa  1: Producto  2: Paquete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'RateClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'RateClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (TINYINT): 1=Activo, 0=Inactivo. Indica vigencia de la tarifa en contratación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción contractual (FK → Contract.ContractDescriptions). Se usa cuando LiquidationType es Servicio o Tarifa-Servicio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la descripcion relaciona asociada al cups, este campo solo se llena si el tipo de tarifa es servicio o Tarifa - Servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS/descripción del servicio (FK → Contract.CUPSEntity). Identificador RIPS para servicios de salud; se llena si tipo de tarifa es servicio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'CupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el CUPS que se va a cargar a la factura, este campo solo se llena si el tipo de tarifa es servicio o Tarifa - Servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'CupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'CupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor porcentual (NUMERIC 6,2): aplica solo si RateType=2 (Tarifa basada en porcentaje). Cálculo dinámico de valor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje, este valor solo se solicita si RateType es tipo 2', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base del porcentaje (TINYINT): 0=N/A, 1=Costo promedio ponderado, 2=Último costo. Define la variable para calcular margen.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'PercentageBasedOn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica en que variable esta basado el porcentaje  0 - N/A  1 - Costo promedio ponderado  2 - Ultimo costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'PercentageBasedOn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'PercentageBasedOn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tarifa (TINYINT): 0=N/A, 1=Tarifa fija, 2=Basada en porcentaje. Estructura de cálculo de precio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de tarifa  0- N/A  1 - Tarifa Fija  2 - Basado en porcentaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación (TINYINT): 0=N/A, 1=Tarifa producto, 2=Servicio, 3=Tarifa-Servicio. Modo de facturación/cobro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidacion  0 - No Aplica 1 - Tarifa 2 - Servicio 3 - Tarifa - Servicio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR MAX) para notas, aclaraciones o condiciones especiales de la tarifa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=Producto cotizado/presupuestado, 0=No cotizado. Distingue cotizaciones de contratos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Quoted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el producto está cotizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Quoted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Quoted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1=Producto bajo contrato vigente, 0=No contratado. Muestra obligatoriedad de uso.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Contracted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el producto es contratado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Contracted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Contracted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de venta con recargo (NUMERIC 18,2). Precio final que incluye márgenes, impuestos o ajustes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con recargo que se va cobrar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValueWithSurcharge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de venta base (NUMERIC 18,2). Precio unitario del producto o servicio sin recargos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cierre de vigencia (DATETIME). Límite superior del período en que la tarifa es válida.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de la tarifa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de vigencia (DATETIME). Límite inferior del período en que la tarifa es válida.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de la tarifa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto (FK → Inventory.InventoryProduct). Enlace al artículo de inventario si aplica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de plantilla de tarifas (FK → Inventory.ProductRate). Agrupar detalles bajo una política de precios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ProductRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla de prooductos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ProductRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'ProductRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY). Identificador único del detalle de tarifa, cubrimiento o combinación producto-precio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de las plantillas o cubrimiento de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de tarifas por producto: registra los valores de venta vigentes para cada producto dentro de una tarifa, incluyendo precios con y sin recargo, tipo de liquidación, porcentajes aplicables y si aplica para contratos o cotizaciones. Permite gestionar el histórico de precios por período y su relación con servicios CUPS, contratos y paquetes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail';
