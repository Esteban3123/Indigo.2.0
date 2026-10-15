CREATE TABLE [Inventory].[ProductRateGeneral] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ProductRateId]     INT             NOT NULL,
    [RuleType]          TINYINT         NOT NULL,
    [ProductTypeId]     INT             NULL,
    [ProductGroupId]    INT             NULL,
    [ProductSubGroupId] INT             NULL,
    [InitialDate]       DATETIME        NOT NULL,
    [EndDate]           DATETIME        NOT NULL,
    [RateType]          TINYINT         NOT NULL,
    [PercentageBasedOn] TINYINT         NOT NULL,
    [ConditionType]     TINYINT         NOT NULL,
    [Percentage]        NUMERIC (6, 2)  NULL,
    [SalesValue]        NUMERIC (18, 2) CONSTRAINT [DF_ProductRateGeneral_SalesValue] DEFAULT ((0)) NOT NULL,
    [Observations]      VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ProductRateGeneral] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductRateGeneral_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_ProductRateGeneral_ProductRateId] FOREIGN KEY ([ProductRateId]) REFERENCES [Inventory].[ProductRate] ([Id]),
    CONSTRAINT [FK_ProductRateGeneral_ProductSubGroup] FOREIGN KEY ([ProductSubGroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id]),
    CONSTRAINT [FK_ProductRateGeneral_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [Inventory].[ProductType] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductRateGeneral] NOCHECK CONSTRAINT [FK_ProductRateGeneral_ProductGroup];


GO
ALTER TABLE [Inventory].[ProductRateGeneral] NOCHECK CONSTRAINT [FK_ProductRateGeneral_ProductRateId];


GO
ALTER TABLE [Inventory].[ProductRateGeneral] NOCHECK CONSTRAINT [FK_ProductRateGeneral_ProductSubGroup];


GO
ALTER TABLE [Inventory].[ProductRateGeneral] NOCHECK CONSTRAINT [FK_ProductRateGeneral_ProductTypeId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del producto, solo se llena con un valor diferente a cero si es tarifa fija', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del porcentaje, este valor solo se solicita si RateType es tipo 2 y la condicion es ninguna (1)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de condicion para calcular el precio del producto  1  - Ninguna  2 - Valor Total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica en que variable esta basado el porcentaje  0 - N/A  1 - Costo promedio ponderado  2 - Ultimo costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'PercentageBasedOn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de tarifa  1 - Tarifa Fija  2 - Basado en porcentaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final de la tarifa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial de la tarifa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del SubGrupo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de regla para la agrupacion de los productos  1 - Tipo Producto  2 - Grupo Producto  3 - Subgrupo Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'RuleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tarifa del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Id';

