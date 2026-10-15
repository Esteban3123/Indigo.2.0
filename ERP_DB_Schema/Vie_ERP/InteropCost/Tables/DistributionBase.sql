CREATE TABLE [InteropCost].[DistributionBase] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GeneralExpenseId] INT           NOT NULL,
    [DistributionType] TINYINT       NOT NULL,
    [ImpactPoints]     TINYINT       NOT NULL,
    [MeasurementUnit]  TINYINT       NOT NULL,
    [Description]      VARCHAR (300) NOT NULL,
    [Area]             BIT           NOT NULL,
    [OfficialHours]    BIT           NOT NULL,
    [SupplyValue]      BIT           NOT NULL,
    [WorkmanshipValue] BIT           NOT NULL,
    [AssetValue]       BIT           NOT NULL,
    [Sales]            BIT           NOT NULL,
    [MultipleBase]     TINYINT       NOT NULL,
    CONSTRAINT [PK_DistributionBase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionBase_GeneralExpense] FOREIGN KEY ([GeneralExpenseId]) REFERENCES [InteropCost].[GeneralExpense] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionBase] NOCHECK CONSTRAINT [FK_DistributionBase_GeneralExpense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ventas', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la Distribucion por busqueda se realiza por Valor Del Activo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'AssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la Distribucion por busqueda se realiza por mano de obra', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la Distribucion por busqueda se realiza por Valor Suministro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el tipo de distribucion de busqueda se va hacer por Horas Funcionario', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la opcion de costeo es por Area en metros cuadrados, Solo se habilita el tipo de distribucion es Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Proporcion  2 - Valor', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntos de incidencia (1 - 10)', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de distribucion  1 - Directa, Si es directa no maneja detalle de centros de produccion  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del gasto general', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'GeneralExpenseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la base de distribucion del gasto', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Id';

