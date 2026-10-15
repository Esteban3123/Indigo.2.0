CREATE TABLE [MixingStation].[RequestPackageDetailStatus] (
    [Id]                           INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestMixingStationDetailId] INT              NOT NULL,
    [PackageId]                    INT              NULL,
    [PackagePersonalizedId]        INT              NULL,
    [Status]                       TINYINT          NOT NULL,
    [QualityStatus]                TINYINT          CONSTRAINT [DF_RequestPackageDetailStatus_QualityStatus] DEFAULT ((0)) NOT NULL,
    [DispensingWarehouseId]        INT              NULL,
    [PhysicalInventoryId]          INT              NULL,
    [BatchCode]                    VARCHAR (50)     NULL,
    [Observations]                 VARCHAR (200)    NULL,
    [SendTo]                       TINYINT          CONSTRAINT [DF_RequestPackageDetailStatus_SendTo] DEFAULT ((0)) NOT NULL,
    [VerificationTagUser]          VARCHAR (20)     NULL,
    [VerificationTagDate]          DATETIME         NULL,
    [GroupingCodeDose]             UNIQUEIDENTIFIER NULL,
    [ProductId]                    INT              NULL,
    [ProductCost]                  DECIMAL (18, 2)  NULL,
    [PreparationTime]              TIME (7)         NULL,
    [BatchExpirationDate]          DATETIME         NULL,
    CONSTRAINT [PK_RequestPackageDetailStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestPackageDetailStatus_Package] FOREIGN KEY ([PackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_RequestPackageDetailStatus_PackagePersonalized] FOREIGN KEY ([PackagePersonalizedId]) REFERENCES [MixingStation].[PackagePersonalized] ([Id]),
    CONSTRAINT [FK_RequestPackageDetailStatus_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_RequestPackageDetailStatus_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_RequestPackageDetailStatus_RequestMixingStationDetail] FOREIGN KEY ([RequestMixingStationDetailId]) REFERENCES [MixingStation].[RequestMixingStationDetail] ([Id]),
    CONSTRAINT [FK_RequestPackageDetailStatus_Warehouse] FOREIGN KEY ([DispensingWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatus] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatus_Package];


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatus] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatus_PackagePersonalized];


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatus] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatus_PhysicalInventory];


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatus] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatus_Product];


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatus] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatus_RequestMixingStationDetail];


GO
ALTER TABLE [MixingStation].[RequestPackageDetailStatus] NOCHECK CONSTRAINT [FK_RequestPackageDetailStatus_Warehouse];


GO
CREATE NONCLUSTERED INDEX [IDX_DetailStatus_Covering]
    ON [MixingStation].[RequestPackageDetailStatus]([RequestMixingStationDetailId] ASC, [Status] ASC, [BatchCode] ASC)
    INCLUDE([Id], [QualityStatus]);


GO
CREATE NONCLUSTERED INDEX [IX_RequestPackageDetailStatus]
    ON [MixingStation].[RequestPackageDetailStatus]([BatchCode] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo se actualiza cuando en el dashboard de control de calidad se envia una orden de traslado.     0 - No se ha enviado la Orden de Traslado  1 - Se realizo la Orden de Traslado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'SendTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del lote al asignar etiqueta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'BatchCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto en inventarios que se creo a partir del proceso final del paquete ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen donde se va enviar e producto terminado para su posterior dispensacion al paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'DispensingWarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 - Pendiente  1 - Liberado  2 - Rechazado  3 - Reprocesado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'QualityStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de los productos :     1. Producto en Producción : Siglas PP  2. Producto Terminado : Siglas PT  3. Producto Liberado : Siglas PL  4. Producto Reproceso : Siglas PPR   5. Producto Rechazado : Siglas PR.  6.Producto Anulado : Siglas PA', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Detalle de la Solicitud', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';

