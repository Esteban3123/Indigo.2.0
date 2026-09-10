CREATE TABLE [Inventory].[KardexBackup] (
    [Id]                      INT             NOT NULL,
    [MovementType]            TINYINT         NOT NULL,
    [ThirdPartyId]            INT             NULL,
    [WarehouseId]             INT             NOT NULL,
    [ProductId]               INT             NOT NULL,
    [BatchSerialId]           INT             NULL,
    [DocumentDate]            DATETIME        NOT NULL,
    [Quantity]                INT             NOT NULL,
    [Value]                   NUMERIC (20, 4) NOT NULL,
    [PreviousCost]            NUMERIC (20, 4) NOT NULL,
    [AverageCost]             NUMERIC (20, 4) NOT NULL,
    [PreviousAverageCost]     NUMERIC (20, 4) NOT NULL,
    [PreviousAmountProduct]   INT             NOT NULL,
    [PreviousAmountWarehouse] INT             NOT NULL,
    [PreviousAmountBatch]     INT             NOT NULL,
    [EntityId]                INT             NOT NULL,
    [EntityCode]              VARCHAR (20)    NOT NULL,
    [EntityName]              VARCHAR (250)   NOT NULL,
    [ImportedEntityId]        INT             NULL,
    [ImportedEntityCode]      VARCHAR (20)    NULL,
    [ImportedEntityName]      VARCHAR (250)   NULL,
    [AffectInventory]         BIT             NOT NULL,
    [CreationUser]            VARCHAR (20)    NOT NULL,
    [CreationDate]            DATETIME        NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de respaldo de registros del kardex de inventario, que almacena movimientos de productos por almacén con información de cantidades, costos promedio actuales y anteriores, y saldos previos por lote y bodega. Registra el tipo de movimiento, tercero involucrado, documento de referencia y si el movimiento afecta el inventario. Conserva datos de la entidad originante y opcionalmente de una entidad importada, junto con auditoría de creación.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'KardexBackup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'KardexBackup';
GO
