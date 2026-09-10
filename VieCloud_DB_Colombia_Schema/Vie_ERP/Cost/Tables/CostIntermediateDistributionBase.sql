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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio clínico: 1=Laboratorios, 2=Patologías, 3=Imágenes Diagnósticas, 4=Procedimientos no quirúrgicos, 5=Procedimientos quirúrgicos, 6=Interconsultas, 7=Ninguno, 8=Consulta Externa, 9=Hemocomponentes. TINYINT, clasificación para costeo por unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio 
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
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de distribución múltiple consecutiva (TINYINT): 1=Distribución A, 2=Distribución B, 3=Distribución C, 4=Distribución D. Nota: si aplica múltiple, deben ser consecutivas (1,2,3,4 sin saltos).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT booleano: especifica si la distribución se calcula por cantidades producidas (1=Sí, 0=No). Solo aplicable a distribución tipo Buscada.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'QuantitiesProduced';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el tipo de distribucion de busqueda se va hacer por cantidades producidas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'QuantitiesProduced';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'QuantitiesProduced';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT booleano: habilita costeo por ventas (1=Sí, 0=No). Solo se activa cuando DistributionType es Buscada.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la opcion de costeo es por ventas, Solo se habilita el tipo de distribucion es Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(300): descripción textual o nombre de la configuración de distribución intermedia, para documentación y búsqueda de la regla de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT unidad de medida para cálculo: 1=Proporción, 2=Valor. Solo habilitado cuando DistributionType es Calculada.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Proporcion  2 - Valor', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT: puntos de incidencia o peso de impacto (rango 1-10), usado en fórmula de distribución de costos indirectos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntos de incidencia (1 - 10)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT tipo de algoritmo de distribución: 2=Calculada (por fórmula/proporción), 3=Buscada (por búsqueda de cantidades o ventas).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de distribucion  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT: clave foránea a [Cost].[CostIntermediateDistribution] (Id), vincula esta configuración base a su distribución intermedia padre.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la distribucion intermedia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY: identificador único (PK) de la tabla CostIntermediateDistributionBase, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bases de distribución intermedia de costos: define los criterios y unidades de medida utilizados para repartir costos entre centros de atención o unidades funcionales en el proceso de costeo por absorción o distribución secundaria.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBase';
