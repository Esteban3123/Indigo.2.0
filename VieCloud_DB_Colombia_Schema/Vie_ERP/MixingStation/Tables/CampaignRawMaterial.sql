CREATE TABLE [MixingStation].[CampaignRawMaterial] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestPackageDetailStatusId] INT             NOT NULL,
    [ProductId]                    INT             NULL,
    [AtcId]                        INT             NULL,
    [SupplieId]                    INT             NULL,
    [ExpendQuantity]               DECIMAL (18, 6) NOT NULL,
    [MeasurementUnitId]            INT             NOT NULL,
    [ProductValidationId]          INT             NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [BatchSerialId]                INT             NULL,
    CONSTRAINT [PK_CampaignRawMaterial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignRawMaterial_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_InventoryProduct] FOREIGN KEY ([ProductValidationId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_RequestPackageDetailStatus] FOREIGN KEY ([RequestPackageDetailStatusId]) REFERENCES [MixingStation].[RequestPackageDetailStatus] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_Supplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);




GO



GO



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto validado entregado en Campaign Validation y utilizado en la gestión de materia prima; referencia a Inventory.InventoryProduct (FK); clave para rastrear productos aprobados en mezclado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ProductValidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de los Productos entregados de Campaign Validation y utilizados en la Gestion de Materia ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ProductValidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ProductValidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (ml, mg, litro, gramo, etc.) en la que se registra el consumo o gasto del producto; referencia a Inventory.InventoryMeasurementUnit (FK)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida en la que se gasto el producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de materia prima gastada o consumida en el detalle del paquete de campaña; decimal de 18,6 posiciones; refleja el volumen real utilizado en producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ExpendQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad de materia prima gastada en el detalle del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ExpendQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ExpendQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del suministro/proveedor cuando el detalle del paquete proviene de una fuente de suministro; referencia a Inventory.InventorySupplie (FK); nullable', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del sumistro cuando el detalle del paquete es por suministro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la clasificación ATC (Anatomical Therapeutic Chemical) cuando el detalle del paquete está clasificado por código ATC; referencia a Inventory.ATC (FK); nullable', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del atc cuando el detalle del paquete es por atc', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto de inventario cuando el detalle del paquete se gestiona por producto individual; referencia a Inventory.InventoryProduct (FK); nullable', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del producto cuando el detalle del paquete es por producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paquete/campaña específica en producción y su estado actual; referencia a MixingStation.RequestPackageDetailStatus (FK); vincula cada materia prima consumida con su solicitud de paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica el packete en concreto que se esta produciendo ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Materias primas utilizadas en cada campaña de mezcla o preparación farmacéutica. Registra qué insumos, productos o principios activos (ATC) se consumen en cada detalle de solicitud de paquete, con su cantidad gastada, unidad de medida y validación del producto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de materia prima en la campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró el consumo de la materia prima.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el registro de la materia prima en la campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación al registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al lote o número de serie del insumo o producto utilizado en la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
