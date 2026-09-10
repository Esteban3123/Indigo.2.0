CREATE TABLE [MixingStation].[PackageDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PackageId]             INT             NOT NULL,
    [ProductId]             INT             NULL,
    [Quantity]              DECIMAL (18, 2) NULL,
    [MeasurementUnitId]     INT             NULL,
    [Thinner]               BIT             NOT NULL,
    [Vehicle]               BIT             NOT NULL,
    [CreationUser]          VARCHAR (20)    CONSTRAINT [DF_PackageDetail_CreationUser] DEFAULT ('999') NOT NULL,
    [CreationDate]          DATETIME        CONSTRAINT [DF_PackageDetail_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]      VARCHAR (20)    NULL,
    [ModificationDate]      DATETIME        NULL,
    [TimeStamp]             ROWVERSION      NOT NULL,
    [Osmolarity]            DECIMAL (5, 1)  CONSTRAINT [DF_PackageDetail_Osmolarity] DEFAULT ((0)) NULL,
    [Density]               DECIMAL (6, 4)  CONSTRAINT [DF_PackageDetail_Density] DEFAULT ((0)) NOT NULL,
    [AtcId]                 INT             NULL,
    [SupplieId]             INT             NULL,
    [ComponentType]         TINYINT         NULL,
    [MainMedicine]          BIT             NULL,
    [Volume]                NUMERIC (18, 2) NULL,
    [VolumeMeasureUnit]     INT             NULL,
    [PreparationType]       TINYINT         NULL,
    [Dilution]              DECIMAL (18, 4) NULL,
    [Concentration]         DECIMAL (18, 4) NULL,
    [AmountTime]            INT             NULL,
    [TimeUnit]              TINYINT         NULL,
    [VolumeTotal]           DECIMAL (18, 2) NULL,
    [NPTItemOrder]          TINYINT         NULL,
    [ComplementaryMedicine] BIT             NULL,
    CONSTRAINT [PK_PackageDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PackageDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_PackageDetail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_PackageDetail_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PackageDetail_Package] FOREIGN KEY ([PackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_PackageDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PackageDetail_UnitMeasure_Volume] FOREIGN KEY ([VolumeMeasureUnit]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total acumulado del paquete en preparación, suma de volúmenes de componentes (medicamentos, insumos, diluyentes), expresado en unidad de medida estándar (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'VolumeTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el volumen total paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'VolumeTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'VolumeTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para duración de infusión o administración (TINYINT: 1=minutos, 2=horas, 3=días), referencia a catálogo de unidades', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la unidad tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de tiempo (duración) para infusión o administración del medicamento/insumo (INT), se combina con TimeUnit', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la cantidad de tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del principio activo o componente en el paquete (DECIMAL 18,4), expresada en mg/mL u otra unidad de medida farmacéutica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la concentacion ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de dilución aplicado al medicamento o componente durante preparación (DECIMAL 18,4), razón de diluyente a producto base', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Dilution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Dilución', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Dilution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Dilution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de preparación farmacéutica (TINYINT): 0=No aplica, 1=Reconstitución, 2=Dilución, 3=Reconstitución + Dilución', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de preparación:  0 - No aplica  1 - Reconstitución  2 - Dilución  3 - Reconstitución + Dilución', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'PreparationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de medida del volumen del componente (INT, FK a InventoryMeasurementUnit), solo se completa si formulación es Volumen o Peso-Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida del volumen, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de volumen del medicamento ATC o componente farmacéutico (NUMERIC 18,2), aplicable a formulaciones tipo Volumen o Peso-Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del volumen del ATC, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de medicamento principal o base del paquete (BIT: 1=sí es medicamento principal, 0=no), distinto de complementarios o diluyentes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'MainMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el medicamento es o no principal', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'MainMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'MainMedicine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de componente en el paquete (TINYINT): 1=Medicamento, 2=Insumo, 3=Producto otro, 4=Medicamento diluyente NPT, 5=Insumo para NPT', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de componente: 
1: Medicamento, 
2: Insumo, 
3: Producto de tipo otro
4: Medicamento adicional NPT (Diluyentes)
5: Producto de Tipo Insumo para NPT', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del insumo o suministro en inventario (INT, FK a InventorySupplie), cuando el componente es insumo hospitalario o complementario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del medicamento según clasificación ATC (INT, FK a Inventory.ATC), código de fármaco cuando el componente es medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Densidad específica del medicamento, insumo o solución preparada (DECIMAL 6,4), valor por defecto 0, expresada en g/mL', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Density';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Densidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Density';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Density';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Osmolaridad de la solución o medicamento (DECIMAL 5,1, mOsm/L), parámetro crítico para compatibilidad y tolerancia, valor por defecto 0', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Osmolarity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Osmolaridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Osmolarity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Osmolarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP), registra automáticamente instante exacto de creación, modificación o evento en el registro de detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de detalle del paquete (DATETIME), auditoria de cambios', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario (VARCHAR 20) que realizó la última modificación o actualización del detalle, auditoria de cambios', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de detalle del paquete (DATETIME, default getdate()), auditoria de trazabilidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario (VARCHAR 20, default ''''999'''') que creó el registro de detalle, auditoria de trazabilidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de producto vehículo o excipiente base (BIT: 1=es vehículo, 0=no), portador inerte en formulaciones', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Vehicle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el producto es vehículo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Vehicle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Vehicle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de producto diluyente o disolvente (BIT: 1=es diluyente, 0=no), usado en reconstitución o dilución de medicamentos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Thinner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el producto es diluyente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Thinner';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Thinner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de medida del producto principal (INT, FK a InventoryMeasurementUnit), ejemplo mg, mL, unidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica del producto o componente en el paquete (DECIMAL 18,2), expresada en la unidad definida por MeasurementUnitId', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto de inventario incluido en el detalle del paquete (INT, FK a InventoryProduct), medicamento, insumo o material', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paquete maestro al que pertenece este detalle (INT, FK a MixingStation.Package), vinculación con preparación compuesta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'PackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle del paquete (INT IDENTITY 1,1), clave primaria, sinónimo: línea de paquete, item de preparación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los componentes (medicamentos, diluyentes, vehículos e insumos) que conforman cada paquete o fórmula de preparación en la estación de mezclas (NPT, quimioterapia u otras preparaciones magistrales), incluyendo cantidades, concentraciones, unidades de medida y parámetros farmacéuticos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden o posición del ítem dentro de la fórmula de Nutrición Parenteral Total (NPT); indica la secuencia en que se agrega el componente durante la preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'NPTItemOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'NPTItemOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el medicamento es complementario o accesorio dentro de la fórmula (verdadero/falso); distingue los componentes secundarios del medicamento principal de la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ComplementaryMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ComplementaryMedicine';
