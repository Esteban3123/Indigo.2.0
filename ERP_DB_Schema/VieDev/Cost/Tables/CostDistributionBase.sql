CREATE TABLE [Cost].[CostDistributionBase] (
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
    CONSTRAINT [PK_CostDistributionBase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionBase_CostGeneralExpense] FOREIGN KEY ([GeneralExpenseId]) REFERENCES [Cost].[CostGeneralExpense] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionBase] NOCHECK CONSTRAINT [FK_CostDistributionBase_CostGeneralExpense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sales:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Ninguna  2 - Proporcion  3 - Valor', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de distribucion  1 - Directa, Si es directa no maneja detalle de centros de produccion  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';

