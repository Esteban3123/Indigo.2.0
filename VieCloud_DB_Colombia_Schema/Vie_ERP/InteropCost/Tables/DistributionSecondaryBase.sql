CREATE TABLE [InteropCost].[DistributionSecondaryBase] (
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
    CONSTRAINT [PK_DistributionSecondaryBase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionSecondaryBase_DistributionSecondary] FOREIGN KEY ([DistributionSecondaryId]) REFERENCES [InteropCost].[DistributionSecondary] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT) de base múltiple para distribución: 1=Dist.A, 2=Dist.B, 3=Dist.C, 4=Dist.D. Las distribuciones múltiples deben ser consecutivas (1,2,3,4). Soporta encadenamiento de criterios de costeo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se realiza Base Multiple  1 - Distribucion (A)  2 - Distribucion (B)  3 - Distribucion (C)  4 - Distribucion (D)  Nota: Si hay distribucion miltiple tienen que ser consecutivas, 1,2,3,4', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MultipleBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita distribución por valor facturado. Asigna costos en proporción al monto de facturación o ingresos generados.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Facturado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita distribución por valor del primo/componente relacionado. Factor auxiliar en la lógica de distribución de costos complejos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'CousinValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor del primo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'CousinValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'CousinValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita distribución por valor del activo. Distribuye costos de depreciación y mantenimiento de equipos e infraestructura.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'AssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Del Activo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'AssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'AssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita distribución por valor de mano de obra (jornales, salarios). Asigna costos laborales a procedimientos y servicios.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por mano de obra', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'WorkmanshipValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita distribución por valor de suministros. Busca costos asociados a materiales, insumos y medicamentos utilizados.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la Distribucion por busqueda se realiza por Valor Suministro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'SupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que activa distribución por horas de funcionario/profesional de salud. Permite asignación de costos según jornada laboral en atención.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el tipo de distribucion de busqueda se va hacer por Horas Funcionario', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'OfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) para habilitar costeo por área en metros cuadrados. Solo válida cuando DistributionType=3 (Buscada). Facilita distribución por espacio físico.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la opcion de costeo es por Area en metros cuadrados, Solo se habilita el tipo de distribucion es Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Area';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 300) de la base de distribución, explicando criterio o componente de costeo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para distribución calculada (TINYINT): 1=Proporción, 2=Valor. Solo activa si DistributionType=2 (Calculada).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida, Solo se habilita si el tipo de distribucion es Calculada  1 - Proporcion  2 - Valor', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntos de incidencia o peso de impacto (TINYINT, 1-10). Rango que pondera la relevancia del factor en la distribución de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntos de incidencia (1 - 10)', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'ImpactPoints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución de costos (TINYINT): 1=Directa, 2=Calculada, 3=Buscada. Define el método de asignación de gastos a centros de costo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de distribucion  1 - Directa  2 - Calculada  3 - Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la distribución secundaria padre; referencia a [InteropCost].[DistributionSecondary]. Permite trazar la jerarquía de costeo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la distribucion ', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) de la base de distribución secundaria en el módulo de costeo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda las bases secundarias de distribución de costos, definiendo los criterios y componentes de valor (área, horas oficiales, insumos, mano de obra, activos, primos, facturación) que se usan para repartir costos indirectos entre unidades o centros de atención.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryBase';
