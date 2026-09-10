CREATE TABLE [Inventory].[RemissionEntranceDetailBatchSerial] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionEntranceDetailId] INT NOT NULL,
    [BatchSerialId]             INT NULL,
    [Quantity]                  INT NOT NULL,
    [OutstandingQuantity]       INT NOT NULL,
    CONSTRAINT [PK_RemissionEntranceDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_RemissionEntranceDetailBatchSerial] CHECK ([Quantity]>(0)),
    CONSTRAINT [CK_RemissionEntranceDetailBatchSerial_1] CHECK ([OutstandingQuantity]>=(0) AND [OutstandingQuantity]<=[Quantity]),
    CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_RemissionEntranceDetail] FOREIGN KEY ([RemissionEntranceDetailId]) REFERENCES [Inventory].[RemissionEntranceDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [CK_RemissionEntranceDetailBatchSerial];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [CK_RemissionEntranceDetailBatchSerial_1];




GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [CK_RemissionEntranceDetailBatchSerial];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [CK_RemissionEntranceDetailBatchSerial_1];


GO



GO



GO
CREATE TRIGGER [Inventory].[tgg_ValidateBeforeDeleteRemissionEntranceDetailBatchSerialIfTheRemissionEntranceIsNotConfirmed]
   ON  [Inventory].[RemissionEntranceDetailBatchSerial] 
   AFTER DELETE
AS 
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from<br>
    -- interfering with SELECT statements.<br>
    SET NOCOUNT ON;

	IF EXISTS (
		SELECT re.Id
		FROM Inventory.RemissionEntrance re
		JOIN Inventory.RemissionEntranceDetail red ON re.Id = red.RemissionEntranceId
		JOIN DELETED redbs ON red.Id = redbs.RemissionEntranceDetailId
		WHERE re.Status = 2
	)
	BEGIN
		THROW 51000, 'Error generado por control del lote de eliminacion desde trigger', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente o disponible del artículo en esta serie/lote. Inicialmente igual a Quantity, disminuye con cada devolución de remisión o consumo en remisión de salida. Rango: 0 hasta Quantity (validado por CHECK).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea la remision este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la remision o se incluya el item en una remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de unidades del artículo en esta serie/lote recibidas en la remisión de entrada. Debe ser mayor a cero (CHECK Quantity > 0).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la serie de lote o número de serie del producto (FK a BatchSerial). Número de identificación único del lote, serial, vencimiento o trazabilidad del artículo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identidicacion  de la  serie de lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de la remisión de entrada (FK a RemissionEntranceDetail). Vincula cada serial/lote a su línea de ingreso de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de serie/lote en detalle de remisión de entrada. Clave primaria de la tabla RemissionEntranceDetailBatchSerial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de lotes y números de serie asociados a cada línea de una remisión de entrada de inventario. Registra las cantidades recibidas y las cantidades pendientes por legalizar para cada lote o serial de un ítem en proceso de ingreso al almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial';
