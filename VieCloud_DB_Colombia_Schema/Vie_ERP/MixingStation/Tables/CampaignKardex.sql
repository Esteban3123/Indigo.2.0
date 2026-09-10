CREATE TABLE [MixingStation].[CampaignKardex] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]  INT             NOT NULL,
    [MovementType]      TINYINT         NOT NULL,
    [MovementDate]      DATETIME        NOT NULL,
    [EntityName]        VARCHAR (100)   NOT NULL,
    [Description]       VARCHAR (500)   NOT NULL,
    [ProductId]         INT             NOT NULL,
    [BatchSerialId]     INT             NULL,
    [Quantity]          DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId] INT             NOT NULL,
    [CreationUser]      VARCHAR (20)    NOT NULL,
    [CreationDate]      DATETIME        NOT NULL,
    CONSTRAINT [PK_CampaignKardex] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignKardex_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_CampaignKardex_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_CampaignKardex_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_CampaignKardex_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de movimiento de inventario en campaña: 1=Entrada (ingreso/recepción de producto), 2=Salida (egreso/consumo/distribución). TINYINT. Indica si el registro es entrada o salida de producto en el kardex de la campaña de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'MovementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de movimiento  1 - Entrada  2 - Salida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'MovementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'MovementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Kardex de movimientos de campañas de mezclas: registra cada entrada y salida de productos (insumos, medicamentos o materiales) asociados al detalle de una campaña de preparación en la estación de mezclas, permitiendo trazabilidad de stock por lote y unidad de medida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del movimiento de kardex.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle de la campaña de mezcla al que pertenece este movimiento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que ocurrió el movimiento de entrada o salida del producto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'MovementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'MovementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad o proceso que generó el movimiento (por ejemplo: área, servicio o módulo origen).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o concepto del movimiento: explica el motivo o detalle de la transacción de kardex.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto o insumo involucrado en el movimiento (medicamento, material de mezcla, etc.).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lote o número de serie del producto movilizado; permite trazabilidad por lote.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de producto que entró o salió en el movimiento, expresada en la unidad de medida correspondiente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida en que se expresa la cantidad (unidades, mililitros, gramos, etc.).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que registró el movimiento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el registro del movimiento en el sistema.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignKardex', @level2type = N'COLUMN', @level2name = N'CreationDate';
