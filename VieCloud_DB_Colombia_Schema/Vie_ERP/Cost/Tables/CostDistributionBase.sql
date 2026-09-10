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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador TINYINT (1-4) que especifica si se aplica distribución múltiple consecutiva: 1=Distribución (A), 2=Distribución (B), 3=Distribución (C), 4=Distribución (D). Nota: distribuciones múltiples deben ser consecutivas (1,2,3,4). Costeo, asignación de gastos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT booleano (True=Sí, False=No) que habilita la inclusión de ventas en la base de distribución del gasto general. Ingresos, facturación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sales:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT booleano que especifica si la distribución por búsqueda se realiza según el Valor del Activo. Costeo de activos fijos, depreciación, equipos médicos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'AssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Del Activo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'AssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'AssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT booleano que especifica si la distribución por búsqueda se calcula según Valor de Mano de Obra. Costeo por labor, personal, profesionales de la salud.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por mano de obra', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT booleano que especifica si la distribución por búsqueda se calcula según Valor de Suministros. Costeo de materiales, insumos, medicamentos, reactivos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Suministro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT booleano que especifica si el tipo de distribución por búsqueda se realiza por Horas del Funcionario. Costeo por horas trabajadas, personal administrativo y asistencial.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el tipo de distribucion de busqueda se va hacer por Horas Funcionario', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT booleano que especifica si la opción de costeo es por Área en metros cuadrados. Solo se habilita cuando el tipo de distribución es Buscada. Asignación por infraestructura, espacios físicos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la opcion de costeo es por Area en metros cuadrados, Solo se habilita el tipo de distribucion es Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo VARCHAR(300) con descripción textual de la base de distribución del gasto general. Notas, detalles, comentarios de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador TINYINT que define la unidad de medida: 1=Ninguna, 2=Proporción, 3=Valor. Solo se habilita si el tipo de distribución es Calculada. Parámetro de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Ninguna  2 - Proporcion  3 - Valor', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación TINYINT (rango 1-10) que representa los puntos de incidencia o peso relativo de la base de distribución en el cálculo del gasto general. Ponderación, factor de impacto.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntos de incidencia (1 - 10)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador TINYINT que define el tipo de distribución: 1=Directa (sin detalle de centros de producción), 2=Calculada, 3=Buscada. Método de asignación de costos, centros de atención, unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de distribucion  1 - Directa, Si es directa no maneja detalle de centros de produccion  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT (FK a Cost.CostGeneralExpense) del gasto general asociado a esta base de distribución. Referencia a gastos indirectos, comunes, servicios generales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'GeneralExpenseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del gasto general', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'GeneralExpenseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'GeneralExpenseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único INT (PK, IDENTITY) de la base de distribución del gasto general. Clave principal de la tabla CostDistributionBase.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion del gasto', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bases de distribución de costos generales: define cómo se reparten los gastos generales entre áreas o centros de costo, indicando el tipo de distribución, la unidad de medida y los factores que participan en el prorrateo (horas hombre, insumos, activos, mano de obra, ventas, entre otros).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBase';
