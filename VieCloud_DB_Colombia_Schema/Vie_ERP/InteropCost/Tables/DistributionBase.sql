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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de distribución múltiple (TINYINT): 1=Distribución A, 2=Distribución B, 3=Distribución C, 4=Distribución D; valores deben ser consecutivos (1,2,3,4) sin gaps.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de distribución por ventas (BIT); especifica si la asignación de costos se basa en ingresos o volumen de ventas.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ventas', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Sales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de distribución por valor del activo (BIT); especifica si se distribuye según valor, depreciación o inversión en bienes fijos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'AssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Del Activo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'AssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'AssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de distribución por mano de obra (BIT); especifica si se distribuye según costo de servicios, trabajo o labor profesional.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por mano de obra', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de distribución por valor de suministros (BIT); especifica si se distribuye según el costo de materiales, insumos o consumibles.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Suministro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de distribución por horas de funcionario (BIT); especifica si la asignación se basa en tiempo/jornada laboral del personal.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el tipo de distribucion de busqueda se va hacer por Horas Funcionario', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de distribución por área (BIT, habilitado solo si tipo es Buscada); especifica si el costeo se realiza por metros cuadrados.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la opcion de costeo es por Area en metros cuadrados, Solo se habilita el tipo de distribucion es Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 300) de la base de distribución; etiqueta o nombre que identifica el criterio de reparto del gasto.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida base (TINYINT, habilitada solo si distribución es Calculada): 1=Proporción, 2=Valor; define si se distribuye por porcentaje o importe absoluto.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Proporcion  2 - Valor', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntos de incidencia o peso relativo (TINYINT, rango 1-10); factor de impacto en la distribución proporcional del gasto.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntos de incidencia (1 - 10)', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución de costos (TINYINT): 1=Directa (sin detalle de centros de producción), 2=Calculada, 3=Buscada; determina la metodología de asignación.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de distribucion  1 - Directa, Si es directa no maneja detalle de centros de produccion  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del gasto general (INT, FK → InteropCost.GeneralExpense.Id); vincula la base de distribución al gasto que será distribuido.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'GeneralExpenseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del gasto general', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'GeneralExpenseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'GeneralExpenseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) de la base de distribución del gasto general; clave primaria de la tabla DistributionBase.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion del gasto', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de distribución de gastos generales para el cálculo de costos: define cómo se reparten los gastos indirectos entre áreas, unidades o servicios, indicando el tipo de distribución, la unidad de medida y los componentes que participan en el reparto (horas, insumos, mano de obra, activos, ventas, entre otros).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBase';
