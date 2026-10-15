CREATE TABLE [Inventory].[ProductRateGeneralCondition] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [ProductRateGeneralId] INT             NOT NULL,
    [InitialValue]         DECIMAL (18, 2) NULL,
    [EndValue]             DECIMAL (18, 2) NULL,
    [SalesValue]           NUMERIC (18, 2) CONSTRAINT [DF_ProductRateGeneralCondition_SalesValue] DEFAULT ((0)) NOT NULL,
    [Percentage]           NUMERIC (6, 2)  NULL,
    CONSTRAINT [PK_ProductRateGeneralCondition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductRateGeneralCondition_ProductRateGeneral] FOREIGN KEY ([ProductRateGeneralId]) REFERENCES [Inventory].[ProductRateGeneral] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductRateGeneralCondition] NOCHECK CONSTRAINT [FK_ProductRateGeneralCondition_ProductRateGeneral];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del porcentaje, este valor solo se solicita si la regla es por porcentaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del producto, solo se llena con un valor diferente a cero si es tarifa fija', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Final, solo se llena si la condicion es por valor total (ConditionType 2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'EndValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Inicial, solo se llena si la condicion es por valor total (ConditionType 2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación general de la tarifa del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'ProductRateGeneralId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'Id';

