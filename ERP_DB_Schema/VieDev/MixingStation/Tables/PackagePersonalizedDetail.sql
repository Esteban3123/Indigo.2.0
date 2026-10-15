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
    CONSTRAINT [PK_PackagePersonalizedDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PackagePersonalizedDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_PackagePersonalized] FOREIGN KEY ([PackagePersonalizedId]) REFERENCES [MixingStation].[PackagePersonalized] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PackagePersonalizedDetail_UnitMeasure_Volume] FOREIGN KEY ([VolumeMeasureUnit]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [MixingStation].[PackagePersonalizedDetail] NOCHECK CONSTRAINT [FK_PackagePersonalizedDetail_ATC];


GO
ALTER TABLE [MixingStation].[PackagePersonalizedDetail] NOCHECK CONSTRAINT [FK_PackagePersonalizedDetail_InventorySupplie];


GO
ALTER TABLE [MixingStation].[PackagePersonalizedDetail] NOCHECK CONSTRAINT [FK_PackagePersonalizedDetail_MeasurementUnit];


GO
ALTER TABLE [MixingStation].[PackagePersonalizedDetail] NOCHECK CONSTRAINT [FK_PackagePersonalizedDetail_PackagePersonalized];


GO
ALTER TABLE [MixingStation].[PackagePersonalizedDetail] NOCHECK CONSTRAINT [FK_PackagePersonalizedDetail_Product];


GO
ALTER TABLE [MixingStation].[PackagePersonalizedDetail] NOCHECK CONSTRAINT [FK_PackagePersonalizedDetail_UnitMeasure_Volume];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el medicamento es o no principal', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'MainMedicine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de componente: 1:Medicamento, 2:Insumo, 3:Producto de tipo otro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Densidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Density';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Osmolaridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Osmolarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si el producto es vehículo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Vehicle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si el producto es diluyente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Thinner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida del volumen, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del volumen del ATC, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'PackagePersonalizedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalizedDetail', @level2type = N'COLUMN', @level2name = N'Id';

