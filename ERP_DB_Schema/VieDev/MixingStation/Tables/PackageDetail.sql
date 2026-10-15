CREATE TABLE [MixingStation].[PackageDetail] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PackageId]         INT             NOT NULL,
    [ProductId]         INT             NULL,
    [Quantity]          DECIMAL (18, 2) NULL,
    [MeasurementUnitId] INT             NULL,
    [Thinner]           BIT             NOT NULL,
    [Vehicle]           BIT             NOT NULL,
    [CreationUser]      VARCHAR (20)    CONSTRAINT [DF_PackageDetail_CreationUser] DEFAULT ('999') NOT NULL,
    [CreationDate]      DATETIME        CONSTRAINT [DF_PackageDetail_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]  VARCHAR (20)    NULL,
    [ModificationDate]  DATETIME        NULL,
    [TimeStamp]         ROWVERSION      NOT NULL,
    [Osmolarity]        DECIMAL (5, 1)  CONSTRAINT [DF_PackageDetail_Osmolarity] DEFAULT ((0)) NULL,
    [Density]           DECIMAL (6, 4)  CONSTRAINT [DF_PackageDetail_Density] DEFAULT ((0)) NOT NULL,
    [AtcId]             INT             NULL,
    [SupplieId]         INT             NULL,
    [ComponentType]     TINYINT         NULL,
    [MainMedicine]      BIT             NULL,
    [Volume]            NUMERIC (18, 2) NULL,
    [VolumeMeasureUnit] INT             NULL,
    [PreparationType]   TINYINT         NULL,
    [Dilution]          DECIMAL (18, 4) NULL,
    [Concentration]     DECIMAL (18, 4) NULL,
    [AmountTime]        INT             NULL,
    [TimeUnit]          TINYINT         NULL,
    [VolumeTotal]       DECIMAL (18, 2) NULL,
    [NPTItemOrder]      TINYINT         NULL,
    CONSTRAINT [PK_PackageDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PackageDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_PackageDetail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_PackageDetail_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PackageDetail_Package] FOREIGN KEY ([PackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_PackageDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PackageDetail_UnitMeasure_Volume] FOREIGN KEY ([VolumeMeasureUnit]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [MixingStation].[PackageDetail] NOCHECK CONSTRAINT [FK_PackageDetail_ATC];


GO
ALTER TABLE [MixingStation].[PackageDetail] NOCHECK CONSTRAINT [FK_PackageDetail_InventorySupplie];


GO
ALTER TABLE [MixingStation].[PackageDetail] NOCHECK CONSTRAINT [FK_PackageDetail_MeasurementUnit];


GO
ALTER TABLE [MixingStation].[PackageDetail] NOCHECK CONSTRAINT [FK_PackageDetail_Package];


GO
ALTER TABLE [MixingStation].[PackageDetail] NOCHECK CONSTRAINT [FK_PackageDetail_Product];


GO
ALTER TABLE [MixingStation].[PackageDetail] NOCHECK CONSTRAINT [FK_PackageDetail_UnitMeasure_Volume];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo el volumen total paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'VolumeTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo la unidad tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la cantidad de tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la concentacion ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Dilución', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'Dilution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de preparación:  0 - No aplica  1 - Reconstitución  2 - Dilución  3 - Reconstitución + Dilución', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'PreparationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de componente: 
1: Medicamento, 
2: Insumo, 
3: Producto de tipo otro
4: Medicamento adicional NPT (Diluyentes)
5: Producto de Tipo Insumo para NPT', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackageDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';

