CREATE TABLE [MixingStation].[QuantityRemaining] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]             INT             NOT NULL,
    [ProductId]                    INT             NOT NULL,
    [BatchSerialId]                INT             NULL,
    [Quantity]                     DECIMAL (20, 6) NOT NULL,
    [SpendQuantity]                DECIMAL (20, 6) NOT NULL,
    [Status]                       TINYINT         NOT NULL,
    [Balance]                      DECIMAL (20, 6) NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [WareHouseId]                  INT             CONSTRAINT [DF__QuantityR__WareH__1B646841] DEFAULT ((155)) NOT NULL,
    [OpeningDate]                  DATETIME        CONSTRAINT [DF__QuantityR__Openi__1C588C7A] DEFAULT ('2022-07-06') NOT NULL,
    [RemnantVolume]                DECIMAL (18, 4) CONSTRAINT [DF_QuantityRemaining_RemnantVolume] DEFAULT ((0)) NULL,
    [UnitRemnantVolume]            DECIMAL (18)    CONSTRAINT [DF_QuantityRemaining_UnitRemnantVolume] DEFAULT ((19)) NOT NULL,
    [PreparationType]              TINYINT         CONSTRAINT [DF_QuantityRemaining_PreparationType] DEFAULT ((0)) NOT NULL,
    [Stability]                    INT             CONSTRAINT [DF_QuantityRemaining_Stability] DEFAULT ((0)) NOT NULL,
    [UnitTimeStability]            TINYINT         CONSTRAINT [DF_QuantityRemaining_UnitTimeStability] DEFAULT ((0)) NOT NULL,
    [Concentration]                DECIMAL (18, 4) CONSTRAINT [DF_QuantityRemaining_Concentration] DEFAULT ('0') NOT NULL,
    [UnitQuantity]                 INT             CONSTRAINT [DF_QuantityRemaining_UnitQuantity] DEFAULT ('2') NOT NULL,
    [CauseReprocessingRejectionId] INT             NULL,
    [Observations]                 VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_QuantityRemaining] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuantityRemaining_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_QuantityRemaining_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_QuantityRemaining_CauseReprocessingRejection] FOREIGN KEY ([CauseReprocessingRejectionId]) REFERENCES [MixingStation].[CauseReprocessingRejection] ([Id]),
    CONSTRAINT [FK_QuantityRemaining_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_QuantityRemaining_Warehouse] FOREIGN KEY ([WareHouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y notas adicionales registradas sobre la preparación, rechazo o gestión del remanente de producto en estación de mezcla (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda las observaciones ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la causa o motivo de baja, rechazo o reprocesamiento del lote (FK a CauseReprocessingRejection, INT)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CauseReprocessingRejectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del motivo de baja', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CauseReprocessingRejectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CauseReprocessingRejectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad unitaria o número de unidades del producto en la preparación (INT, default 2)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del producto preparado en estación de mezcla, expresada en unidad de medida definida (DECIMAL 18,4)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo de estabilidad: 0=Días, 1=Horas, 2=Meses (TINYINT, default 0)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitTimeStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estabilidad de la unidad de tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitTimeStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitTimeStability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de estabilidad o vigencia del producto después de preparación en estación de mezcla (INT días/horas, default 0)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Stability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estabilidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Stability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Stability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de preparación realizada: 0=Estándar, 1=Especial, 2=Rechazo (TINYINT, default 0)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de preparación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'PreparationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del volumen remanente: mL, L, etc. (DECIMAL 18, default 19)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitRemnantVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen remanente de la unidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitRemnantVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitRemnantVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen sobrante o remanente del producto después del gasto en estación de mezcla (DECIMAL 18,4 mL/L)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'RemnantVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen remanente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'RemnantVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'RemnantVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de apertura del producto o inicio de uso en estación de mezcla (DATETIME, default 2022-07-06)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'OpeningDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora de Apertura', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'OpeningDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'OpeningDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén donde se registra el remanente de producto (FK a Warehouse, INT, default 155)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'WareHouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion Con Almacenes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'WareHouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'WareHouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de cantidad remanente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la creación del movimiento de remanente (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de usuario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo final = Cantidad sobrante menos Cantidad gastada (DECIMAL 20,6)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo = cantidad sobrante - cantidad gastada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo, 2=Anulado (TINYINT, 0-255)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Activo  2-Anulado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad gastada o consumida del producto en la preparación (DECIMAL 20,6)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'SpendQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Gastada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'SpendQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'SpendQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad inicial o sobrante disponible del producto antes del gasto (DECIMAL 20,6)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad sobrante', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote o número de serie del producto en inventario (FK a BatchSerial, INT nullable)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tiene relación con la tabla BatchSerial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto inventariado utilizado en la preparación (FK a InventoryProduct, INT)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla InventoryProduct', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de campaña o preparación asociada (FK a CampaignDetail, INT)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla CampaignDetail', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo del registro de cantidad remanente (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de cantidades remanentes o sobrantes de productos en la estación de mezclas, asociadas a campañas de preparación. Controla el saldo disponible, la cantidad gastada, la estabilidad y las condiciones de cada lote o preparación pendiente de uso o reprocesamiento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining';
