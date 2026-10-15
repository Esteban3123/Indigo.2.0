CREATE TABLE [Cost].[CostIntermediateDistributionBase] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IntermediateDistributionId] INT           NOT NULL,
    [DistributionType]           TINYINT       NOT NULL,
    [ImpactPoints]               TINYINT       NOT NULL,
    [MeasurementUnit]            TINYINT       NOT NULL,
    [Description]                VARCHAR (300) NOT NULL,
    [Sales]                      BIT           NOT NULL,
    [QuantitiesProduced]         BIT           NOT NULL,
    [MultipleBase]               TINYINT       NOT NULL,
    [ServiceType]                TINYINT       NOT NULL,
    CONSTRAINT [PK_CostIntermediateDistributionBase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostIntermediateDistributionBase_CostIntermediateDistribution] FOREIGN KEY ([IntermediateDistributionId]) REFERENCES [Cost].[CostIntermediateDistribution] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Servicio 
1: Laboratorios, 
2: Patologías 
3: Imágenes Diagnosticas, 
4: Procedimientos no Qx, 
5: Procedimientos Qx, 
6: Interconsultas, 
7: Ninguno, 
8: Consulta Externa, 
9. Hemocomponentes', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si el tipo de distribucion de busqueda se va hacer por cantidades producidas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'QuantitiesProduced';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si la opcion de costeo es por ventas, Solo se habilita el tipo de distribucion es Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Proporcion  2 - Valor', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntos de incidencia (1 - 10)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribucion  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la distribucion intermedia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Id';

