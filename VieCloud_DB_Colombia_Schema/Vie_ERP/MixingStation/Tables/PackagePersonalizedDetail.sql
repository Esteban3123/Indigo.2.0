CREATE TABLE [MixingStation].[PackagePersonalizedDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PackagePersonalizedId] INT             NOT NULL,
    [ProductId]             INT             NULL,
    [Quantity]              DECIMAL (18, 2) NULL,
    [MeasurementUnitId]     INT             NULL,
    [Volume]                NUMERIC (18, 2) NULL,
    [VolumeMeasureUnit]     INT             NULL,
    [Thinner]               BIT             NOT NULL,
    [Vehicle]               BIT             NOT NULL,
    [CreationUser]          VARCHAR (20)    CONSTRAINT [DF_PackagePersonalizedDetail_CreationUser] DEFAULT ('999') NOT NULL,
    [CreationDate]          DATETIME        CONSTRAINT [DF_PackagePersonalizedDetail_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]      VARCHAR (20)    NULL,
    [ModificationDate]      DATETIME        NULL,
    [TimeStamp]             ROWVERSION      NOT NULL,
    [Osmolarity]            DECIMAL (5, 1)  CONSTRAINT [DF_PackagePersonalizedDetail_Osmolarity] DEFAULT ((0)) NULL,
    [Density]               DECIMAL (6, 4)  CONSTRAINT [DF_PackagePersonalizedDetail_Density] DEFAULT ((0)) NOT NULL,
    [AtcId]                 INT             NULL,
    [SupplieId]             INT             NULL,
    [ComponentType]         TINYINT         NULL,
    [MainMedicine]          BIT             NULL,
    [PreparationType]       TINYINT         NULL,
    [Dilution]              DECIMAL (18, 2) NULL,
    [Concentration]         DECIMAL (18, 2) NULL,
    [AmountTime]            INT             NULL,
    [TimeUnit]              TINYINT         NULL,
    [VolumeTotal]           DECIMAL (18, 4) NULL,
    [NPTItemOrder]          TINYINT         NULL,
    [ComplementaryMedicine] BIT             NULL,
    CONSTRAINT [PK_PackagePersonalizedDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PackagePersonalizedDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_PackagePersonalized] FOREIGN KEY ([PackagePersonalizedId]) REFERENCES [MixingStation].[PackagePersonalized] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_UnitMeasure_Volume] FOREIGN KEY ([VolumeMeasureUnit]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el medicamento es principal o complementario en la preparación personalizada; booleano de fármaco primario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'MainMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el medicamento es o no principal', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'MainMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'MainMedicine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del componente en preparación: 1=Medicamento/Fármaco (ATC), 2=Insumo/Suministro, 3=Producto otro; tipo de ingrediente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de componente: 1:Medicamento, 2:Insumo, 3:Producto de tipo otro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del insumo/suministro en inventario (Inventory.InventorySupplie); referencia a material de apoyo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del medicamento por clasificación ATC (Anatomical Therapeutic Chemical); código de fármaco', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Densidad física del producto en preparación (DECIMAL 6,4); propiedad fisicoquímica g/mL', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Density';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Densidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Density';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Density';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Osmolaridad de la solución preparada (DECIMAL 5,1 mOsm/L); presión osmótica del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Osmolarity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Osmolaridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Osmolarity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Osmolarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de auditoría: instante exacto de creación, registro o modificación del detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de última actualización del registro de detalle; fecha de cambio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que realizó la última modificación; auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación inicial del detalle; momento de registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que creó el registro; auditoría de origen (defecto ''''999'''')', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el producto actúa como vehículo/diluente base en la mezcla personalizada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Vehicle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el producto es vehículo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Vehicle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Vehicle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el producto es diluyente/disolvente en la formulación personalizada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Thinner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el producto es diluyente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Thinner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Thinner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de unidad de medida volumen (mL, L, etc.); solo en formulación Volumen o Peso-Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida del volumen, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de volumen del ATC/medicamento (DECIMAL 18,2); se completa solo en formulación volumétrica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del volumen del ATC, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de unidad de medida del producto (mg, mL, comprimidos, etc.); escala de cantidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/dosis del producto en detalle (DECIMAL 18,2); volumen o peso según unidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del producto en inventario (Inventory.InventoryProduct); artículo componente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del paquete personalizado padre (MixingStation.PackagePersonalized); agrupa detalles', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'PackagePersonalizedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'PackagePersonalizedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'PackagePersonalizedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle de paquete personalizado; clave primaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los componentes que conforman un paquete personalizado de mezclas en la estación de preparación farmacéutica (MixingStation). Registra cada insumo, medicamento o vehículo con sus cantidades, volúmenes, concentraciones y parámetros de preparación para mezclas como nutrición parenteral total (NPT) u otras soluciones magistrales.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de preparación del componente (por ejemplo: infusión, bolo, dilución directa); clasifica cómo debe prepararse el ítem dentro de la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor o valor de dilución aplicado al componente; indica en qué proporción se diluye el medicamento o insumo en la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Dilution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Dilution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del componente en la mezcla (expresada normalmente en mg/mL u otra unidad farmacéutica); permite validar la dosis final del medicamento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de tiempo asociada a la administración o preparación del componente (por ejemplo: duración de la infusión); se interpreta junto con la unidad de tiempo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo correspondiente a AmountTime (minutos, horas, días, etc.); define la escala temporal de la administración o preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total resultante del componente dentro de la mezcla, incluyendo diluyente u otros vehículos; representa el volumen final a administrar o mezclar.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'VolumeTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'VolumeTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden o posición del ítem dentro de una mezcla de nutrición parenteral total (NPT); determina la secuencia de adición de los componentes durante la preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'NPTItemOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'NPTItemOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el componente es un medicamento complementario o accesorio dentro del paquete, en contraposición al medicamento principal de la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ComplementaryMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ComplementaryMedicine';
