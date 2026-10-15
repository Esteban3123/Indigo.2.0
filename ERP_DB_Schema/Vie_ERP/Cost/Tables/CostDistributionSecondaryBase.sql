CREATE TABLE [Cost].[CostDistributionSecondaryBase] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryId] INT           NOT NULL,
    [DistributionType]        TINYINT       NOT NULL,
    [ImpactPoints]            TINYINT       NOT NULL,
    [MeasurementUnit]         TINYINT       NOT NULL,
    [Description]             VARCHAR (300) NOT NULL,
    [Area]                    BIT           NOT NULL,
    [OfficialHours]           BIT           NOT NULL,
    [SupplyValue]             BIT           NOT NULL,
    [WorkmanshipValue]        BIT           NOT NULL,
    [AssetValue]              BIT           NOT NULL,
    [CousinValue]             BIT           NOT NULL,
    [InvoiceValue]            BIT           NOT NULL,
    [MultipleBase]            TINYINT       NOT NULL,
    CONSTRAINT [PK_CostDistributionSecondaryBase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionSecondaryBase_CostDistributionSecondary] FOREIGN KEY ([DistributionSecondaryId]) REFERENCES [Cost].[CostDistributionSecondary] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionSecondaryBase] NOCHECK CONSTRAINT [FK_CostDistributionSecondaryBase_CostDistributionSecondary];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la Distribucion por busqueda se realiza por Valor Facturado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la Distribucion por busqueda se realiza por Valor del primo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'CousinValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la Distribucion por busqueda se realiza por Valor Del Activo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'AssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la Distribucion por busqueda se realiza por mano de obra', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la Distribucion por busqueda se realiza por Valor Suministro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el tipo de distribucion de busqueda se va hacer por Horas Funcionario', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la opcion de costeo es por Area en metros cuadrados, Solo se habilita el tipo de distribucion es Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Proporcion  2 - Valor', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntos de incidencia (1 - 10)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de distribucion  1 - Directa  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la distribucion ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Id';

