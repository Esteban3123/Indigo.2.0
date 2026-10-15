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
ALTER TABLE [Inventory].[ProductRateDetail] NOCHECK CONSTRAINT [FK_ProductRateDetail_ContractDescription];


GO
ALTER TABLE [Inventory].[ProductRateDetail] NOCHECK CONSTRAINT [FK_ProductRateDetail_Cups];


GO
ALTER TABLE [Inventory].[ProductRateDetail] NOCHECK CONSTRAINT [FK_ProductRateDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[ProductRateDetail] NOCHECK CONSTRAINT [FK_ProductRateDetail_Package];


GO
ALTER TABLE [Inventory].[ProductRateDetail] NOCHECK CONSTRAINT [FK_ProductRateDetail_ProductRate];


GO
CREATE NONCLUSTERED INDEX [INDEX_PRODUCTRATE]
    ON [Inventory].[ProductRateDetail]([ProductRateId] ASC)
    INCLUDE([ProductId], [LiquidationType], [RateType], [PercentageBasedOn], [Percentage], [CupsId], [ContractDescriptionId], [InitialDate], [EndDate], [SalesValue], [SalesValueWithSurcharge], [Contracted], [Quoted], [Observations], [Status], [RateClass], [PackageId], [DoseType]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: No Aplica  2: Estandar  3: Personalizada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'DoseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de tarifa  1: Producto  2: Paquete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'RateClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica en que variable esta basado el porcentaje  0 - N/A  1 - Costo promedio ponderado  2 - Ultimo costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'PercentageBasedOn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de tarifa  0- N/A  1 - Tarifa Fija  2 - Basado en porcentaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidacion  0 - No Aplica 1 - Tarifa 2 - Servicio 3 - Tarifa - Servicio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';

