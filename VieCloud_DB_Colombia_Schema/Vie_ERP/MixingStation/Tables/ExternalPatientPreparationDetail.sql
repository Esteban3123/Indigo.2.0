CREATE TABLE [MixingStation].[ExternalPatientPreparationDetail] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [ExternalPatientPreparationId] INT             NOT NULL,
    [itemType]                     INT             NOT NULL,
    [AtcId]                        INT             NULL,
    [SupplieId]                    INT             NULL,
    [ProductId]                    INT             NULL,
    [ComponentType]                INT             NOT NULL,
    [Quantity]                     DECIMAL (18, 2) NULL,
    [MeasurementUnitId]            INT             NULL,
    [Volume]                       DECIMAL (18, 2) NULL,
    [VolumeMeasureUnitId]          INT             NULL,
    CONSTRAINT [PK_ExternalPatientPreparationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExternalPatientPreparationDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_ExternalPatientPreparationDetail_ExternalPatientPreparation] FOREIGN KEY ([ExternalPatientPreparationId]) REFERENCES [MixingStation].[ExternalPatientPreparation] ([Id]),
    CONSTRAINT [FK_ExternalPatientPreparationDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ExternalPatientPreparationDetail_Supplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_ExternalPatientPreparationDetail_UnitMeasurement] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_ExternalPatientPreparationDetail_VolumeMeasureUnitId] FOREIGN KEY ([VolumeMeasureUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida de volumen (ml, l, cc) para medicamentos o insumos preparados; referencia a InventoryMeasurementUnit. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena unidad de medida de volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de medicamento expresada en volumen (ml, litros, cc); valor decimal para preparaciones con componente volumétrico. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena cantidad de medicamento en volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de medida: peso (gr, mg) o unidad de administración (comprimidos, ampollas, dosis); referencia a InventoryMeasurementUnit. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda unidades de medida de peso o en su defecto unidades de administración (En el caso de los insumos)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica en peso o unidad de administración (ej: 30 para 30 gr); aplica para medicamento principal con preparación peso/peso-volumen/unidad. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad, guarda el valor de la cantidad en unidad de medida de peso o unidad de administración, ejemplo 30 gr -> Guarda el número 30 (Es decir para medicamentos principales con tipo de preparación peso o peso /volumen o Unidad de administración)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del componente en preparación: 0=No aplica, 1=Medicamento principal, 2=Reconstituyente, 3=Vehículo; indica rol en fórmula. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de componente:
0. No aplica
1. Medicamento principal
2. Reconstituyente
3. Vehiculo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto farmacéutico del inventario; referencia a InventoryProduct para medicamentos. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del insumo/suministro del inventario; referencia a InventorySupplie para materiales no farmacéuticos. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de clasificación ATC (Anatomical Therapeutic Chemical) del medicamento; referencia a ATC para codificación internacional. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Atc
', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ítem relacionado a categorización: 1=Medicamento; discrimina si el detalle corresponde a fármaco o insumo. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'itemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de componente relacionado a su categorización:
1. Mecicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'itemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'itemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la preparación externa (cabecera); referencia a ExternalPatientPreparation para asociar detalle a orden de preparación. PII=No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ExternalPatientPreparationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la preparación externa (Cabecera)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ExternalPatientPreparationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'ExternalPatientPreparationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los componentes (medicamentos, insumos o productos) que conforman cada preparación farmacéutica para pacientes externos en la estación de mezclas. Registra cantidades, volúmenes y unidades de medida de cada ítem incluido en la preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de detalle de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalPatientPreparationDetail', @level2type = N'COLUMN', @level2name = N'Id';
