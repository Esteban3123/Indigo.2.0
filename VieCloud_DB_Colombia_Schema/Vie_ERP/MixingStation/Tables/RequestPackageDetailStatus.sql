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



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IDX_DetailStatus_Covering]
    ON [MixingStation].[RequestPackageDetailStatus]([RequestMixingStationDetailId] ASC, [Status] ASC, [BatchCode] ASC)
    INCLUDE([Id], [QualityStatus]);


GO
CREATE NONCLUSTERED INDEX [IX_RequestPackageDetailStatus]
    ON [MixingStation].[RequestPackageDetailStatus]([BatchCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RequestPackageDetailStatus_Product_Batch_Grouping]
    ON [MixingStation].[RequestPackageDetailStatus]([ProductId] ASC, [BatchCode] ASC, [GroupingCodeDose] ASC)
    INCLUDE([RequestMixingStationDetailId], [Id]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de traslado de orden (TINYINT). 0=No enviado, 1=Orden de traslado realizada. Se actualiza desde dashboard de control de calidad cuando se ejecuta traslado de producto terminado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'SendTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo se actualiza cuando en el dashboard de control de calidad se envia una orden de traslado.     0 - No se ha enviado la Orden de Traslado  1 - Se realizo la Orden de Traslado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'SendTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'SendTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del lote asignado al etiquetar (VARCHAR 50). Identificador único del lote farmacéutico para trazabilidad y control de inventario.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'BatchCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del lote al asignar etiqueta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'BatchCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'BatchCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a PhysicalInventory. ID del registro de inventario físico generado al finalizar el proceso de mezcla/preparación del paquete personalizado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto en inventarios que se creo a partir del proceso final del paquete ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a Warehouse. ID del almacén destino donde se envía el producto terminado para dispensación posterior al paciente o centro de atención.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'DispensingWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen donde se va enviar e producto terminado para su posterior dispensacion al paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'DispensingWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'DispensingWarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de liberación de calidad (TINYINT): 0=Pendiente, 1=Liberado, 2=Rechazado, 3=Reprocesado. Resultado del control y verificación de calidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'QualityStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0 - Pendiente  1 - Liberado  2 - Rechazado  3 - Reprocesado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'QualityStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'QualityStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del producto en cadena de mezcla (TINYINT): 1=PP (Producción), 2=PT (Terminado), 3=PL (Liberado), 4=PPR (Reproceso), 5=PR (Rechazado), 6=PA (Anulado).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de los productos :     1. Producto en Producción : Siglas PP  2. Producto Terminado : Siglas PT  3. Producto Liberado : Siglas PL  4. Producto Reproceso : Siglas PPR   5. Producto Rechazado : Siglas PR.  6.Producto Anulado : Siglas PA', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a RequestMixingStationDetail. ID del detalle específico de la solicitud de preparación en estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Detalle de la Solicitud', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'RequestMixingStationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el historial de estados de cada paquete (bolsa/envase) generado en la estación de mezclas para una solicitud de preparación farmacéutica. Permite rastrear el ciclo de vida del paquete: calidad, dispensación, lote, verificación y costo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de estado del paquete de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al paquete estándar asignado a esta preparación; identifica el tipo de envase o bolsa utilizada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al paquete personalizado (etiqueta o configuración especial) asignado a la preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PackagePersonalizedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PackagePersonalizedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales registradas sobre el estado del paquete; comentarios del proceso de preparación o dispensación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la verificación del rótulo o etiqueta del paquete; responsable del control de etiquetado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'VerificationTagUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'VerificationTagUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se verificó la etiqueta o rótulo del paquete de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'VerificationTagDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'VerificationTagDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (GUID) que agrupa varias dosis o paquetes pertenecientes a una misma preparación o lote de trabajo en la estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al producto o medicamento principal utilizado en la preparación de la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo unitario del producto o medicamento incluido en el paquete de mezcla; valor usado para facturación o control de costos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'ProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'ProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento del lote del producto utilizado en la preparación; fecha de expiración del medicamento mezclado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'BatchExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'BatchExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora en que se realizó o completó la preparación del paquete en la estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PreparationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatus', @level2type = N'COLUMN', @level2name = N'PreparationTime';
