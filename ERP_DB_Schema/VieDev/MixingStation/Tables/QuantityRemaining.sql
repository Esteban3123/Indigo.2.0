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
ALTER TABLE [MixingStation].[QuantityRemaining] NOCHECK CONSTRAINT [FK_QuantityRemaining_BatchSerial];


GO
ALTER TABLE [MixingStation].[QuantityRemaining] NOCHECK CONSTRAINT [FK_QuantityRemaining_CampaignDetail];


GO
ALTER TABLE [MixingStation].[QuantityRemaining] NOCHECK CONSTRAINT [FK_QuantityRemaining_InventoryProduct];


GO
ALTER TABLE [MixingStation].[QuantityRemaining] NOCHECK CONSTRAINT [FK_QuantityRemaining_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda las observaciones ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del motivo de baja', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CauseReprocessingRejectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estabilidad de la unidad de tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitTimeStability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estabilidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Stability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de preparación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'PreparationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen remanente de la unidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'UnitRemnantVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen remanente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'RemnantVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora de Apertura', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'OpeningDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion Con Almacenes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'WareHouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de usuario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo = cantidad sobrante - cantidad gastada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Activo  2-Anulado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Gastada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'SpendQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad sobrante', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'tiene relación con la tabla BatchSerial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla InventoryProduct', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla CampaignDetail', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'QuantityRemaining', @level2type = N'COLUMN', @level2name = N'Id';

