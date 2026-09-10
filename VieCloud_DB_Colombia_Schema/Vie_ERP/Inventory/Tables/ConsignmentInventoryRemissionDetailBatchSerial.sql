CREATE TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] (
    [Id]                                               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConsignmentInventoryRemissionDetailId]            INT NOT NULL,
    [BatchSerialId]                                    INT NULL,
    [ConsignmentInventoryRemissionDetailBatchSerialId] INT NULL,
    [Quantity]                                         INT NOT NULL,
    [OutstandingQuantity]                              INT NOT NULL,
    [ReturnedQuantity]                                 INT CONSTRAINT [DF_ConsignmentInventoryRemissionDetailBatchSerial_ReturnedQuantity] DEFAULT ((0)) NOT NULL,
    [UsedQuantity]                                     INT CONSTRAINT [DF_ConsignmentInventoryRemissionDetailBatchSerial_UsedQuantity] DEFAULT ((0)) NOT NULL,
    [LegalizedQuantity]                                INT CONSTRAINT [DF_ConsignmentInventoryRemissionDetailBatchSerial_LegalizedQuantity] DEFAULT ((0)) NOT NULL,
    [ReplacementQuantity]                              INT CONSTRAINT [DF_ConsignmentInventoryRemissionDetailBatchSerial_ReplenishmentQuantity] DEFAULT ((0)) NOT NULL,
    [DecreaseQuantity]                                 INT CONSTRAINT [DF__Consignme__Decre__38367C70] DEFAULT ((0)) NOT NULL,
    [ConsignmentTransferQuantity]                      INT CONSTRAINT [DF_ConsignmentInventoryRemissionDetailBatchSerial_ConsignmentTransferQuantity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ConsignmentInventoryRemissionDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial] CHECK ([Quantity]>(0)),
    CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_1] CHECK ([OutstandingQuantity]>=(0) AND [OutstandingQuantity]<=[Quantity]),
    CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_Quantity] CHECK ([Quantity]=((([OutstandingQuantity]+[UsedQuantity])+[ReturnedQuantity])+[ConsignmentTransferQuantity])),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetailBatchSerial_ConsignmentInventoryRemissionDetail] FOREIGN KEY ([ConsignmentInventoryRemissionDetailId]) REFERENCES [Inventory].[ConsignmentInventoryRemissionDetail] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetailBatchSerial_ConsignmentInventoryRemissionDetailBatchSerial] FOREIGN KEY ([ConsignmentInventoryRemissionDetailBatchSerialId]) REFERENCES [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] ([Id])
);


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_1];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_Quantity];




GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_1];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_Quantity];




GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_1];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_Quantity];


GO



GO



GO





GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_1];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_Quantity];


GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [UX_ConsignmentInventoryRemissionDetailBatchSerial_ConsignmentInventoryRemissionDetailId]
    ON [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial]([ConsignmentInventoryRemissionDetailId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ConsignmentInventoryRemissionDetailBatchSerial_Detail_Batch_Outstanding]
    ON [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial]([ConsignmentInventoryRemissionDetailId] ASC, [BatchSerialId] ASC, [OutstandingQuantity] ASC)
    INCLUDE([UsedQuantity]);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-06-07
-- Description:	Se valida que la cantidad legalizada en el lote sea igual a la cantidad legalizada en los controles de inventario en consignación
-- =============================================
CREATE TRIGGER [Inventory].[tgg_ValidateLegalizedQuantityFromDetailControls]
   ON  [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] 
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Se valida que la cantidad disponible del lote corresponda con lo registrado en DetailControls
	IF EXISTS
	(
		SELECT	cirdbs.Id
		FROM INSERTED cirdbs
		LEFT JOIN 
		(
			SELECT	cirdc.ConsignmentInventoryRemissionDetailId, cirdc.BatchSerialId, 
					SUM(cirdc.Quantity * IIF(cirdc.MovementType = 1, -1, 1)) QuantityUsed
			FROM Inventory.ConsignmentInventoryRemissionDetailControl cirdc
			GROUP BY cirdc.ConsignmentInventoryRemissionDetailId, cirdc.BatchSerialId
		) cirdc ON cirdbs.ConsignmentInventoryRemissionDetailId = cirdc.ConsignmentInventoryRemissionDetailId AND ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(cirdc.BatchSerialId, 0)
		WHERE cirdbs.OutstandingQuantity <> (cirdbs.Quantity - ISNULL(cirdc.QuantityUsed, 0))
	)
	BEGIN
	DECLARE @Ids varchar(100)
		SELECT @Ids = STRING_AGG(cirdbs.Id,' , ')
		FROM INSERTED cirdbs
		LEFT JOIN 
		(
			SELECT	cirdc.ConsignmentInventoryRemissionDetailId, cirdc.BatchSerialId, 
					SUM(cirdc.Quantity * IIF(cirdc.MovementType = 1, -1, 1)) QuantityUsed
			FROM Inventory.ConsignmentInventoryRemissionDetailControl cirdc
			GROUP BY cirdc.ConsignmentInventoryRemissionDetailId, cirdc.BatchSerialId
		) cirdc ON cirdbs.ConsignmentInventoryRemissionDetailId = cirdc.ConsignmentInventoryRemissionDetailId AND ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(cirdc.BatchSerialId, 0)
		WHERE cirdbs.OutstandingQuantity <> (cirdbs.Quantity - ISNULL(cirdc.QuantityUsed, 0));
		--THROW 51000, 'Error generado por control desde trigger. La cantidad disponible del lote no corresponde con lo registrado en la tabla de control-A', 1
		THROW 51000, @Ids, 1

	END
	ELSE	
	BEGIN
		--Se valida que la cantidad retornada del lote corresponda con lo registrado en DetailControls
		IF EXISTS
		(
			SELECT	cirdbs.Id
			FROM INSERTED cirdbs
			LEFT JOIN 
			(
				SELECT	cirdc.ConsignmentInventoryRemissionDetailId, cirdc.BatchSerialId, 
						SUM(cirdc.Quantity) ReturnedQuantity
				FROM Inventory.ConsignmentInventoryRemissionDetailControl cirdc
				WHERE cirdc.EntityName = 'RemissionDevolution'
				GROUP BY cirdc.ConsignmentInventoryRemissionDetailId, cirdc.BatchSerialId
			) cirdc ON cirdbs.ConsignmentInventoryRemissionDetailId = cirdc.ConsignmentInventoryRemissionDetailId AND ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(cirdc.BatchSerialId, 0)
			WHERE cirdbs.ReturnedQuantity <> ISNULL(cirdc.ReturnedQuantity, 0)
		)
		BEGIN
			THROW 51000, 'Error generado por control desde trigger. La cantidad retornada del lote no corresponde con lo registrado en la tabla de control-B', 1
		END
		ELSE	
		BEGIN
			--Se valida que la cantidad retornada del lote corresponda con lo registrado en la devolucion de la remision
			IF EXISTS
			(
				SELECT 
					1
				FROM INSERTED cirdbs
				JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc ON cirdbs.ConsignmentInventoryRemissionDetailId = cirdc.ConsignmentInventoryRemissionDetailId
				JOIN Inventory.RemissionDevolution rd ON cirdc.EntityId = rd.Id
				JOIN Inventory.RemissionDevolutionDetail rdd ON rd.Id = rdd.RemissionDevolutionId AND cirdc.EntityDetailId = rdd.Id
				WHERE cirdc.EntityName = 'RemissionDevolution'
				GROUP BY rdd.Id, rdd.Quantity
				HAVING SUM(rdd.Quantity) <> SUM(cirdc.Quantity)
			)
			BEGIN
				THROW 51000, 'Error generado por control desde trigger. La cantidad retornada del lote no corresponde con lo registrado en la tabla de control-C', 1
			END
			ELSE	
			BEGIN
				--Se valida que la cantidad legalizada del lote corresponda con lo registrado en DetailControls
				IF EXISTS
				(
					SELECT	cirdbs.Id
					FROM INSERTED cirdbs
					LEFT JOIN 
					(
						SELECT 
							cirdc.ConsignmentInventoryRemissionDetailId, BatchSerialId, 
							SUM(cirdc.Quantity * IIF(cirdc.MovementType = 1, -1, 1) - cirdc.QuantityPendingLegalization) LegalizedQuantity
						FROM Inventory.ConsignmentInventoryRemissionDetailControl cirdc
						WHERE cirdc.EntityName IN ('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution', 'TransferOrder', 'TransferOrderDevolution', 'BasicBilling')
						GROUP BY cirdc.ConsignmentInventoryRemissionDetailId, BatchSerialId
					) cirdc ON cirdbs.ConsignmentInventoryRemissionDetailId = cirdc.ConsignmentInventoryRemissionDetailId AND ISNULL(cirdbs.BatchSerialId, 0) = ISNULL(cirdc.BatchSerialId, 0)
					WHERE cirdbs.LegalizedQuantity <> ISNULL(cirdc.LegalizedQuantity, 0)
				)
				BEGIN
					THROW 51000, 'Error generado por control desde trigger. La cantidad legalizada del lote no corresponde con lo registrado en la tabla de control-D', 1
				END
			END			
		END
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad a disminuir del límite máximo de consignación permitido (INT, default 0); ajuste de techo de remisión', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'DecreaseQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad a disminuir al limite maximo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'DecreaseQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'DecreaseQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada en movimiento de reposición desde inventario remisión de consignación (INT, default 0); replenishment de stock', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ReplacementQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad solicitada en reposición desde el inventario remisión de inventario en consignación bajo el movimiento reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ReplacementQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ReplacementQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad legalizada/comprobada de entrada (INT, default 0); vinculada a comprobantes de entrada; no puede exceder UsedQuantity', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LegalizedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad legalizada (comprobante de entrada) del Item, Cuando se crea la remision este campo es igual 0, pero este campo va aumentando cada vez que se realice una legalizacion de productos usados a través del formulario comprobante de entrada. Este valor no puede ser mayor de la cantidad usada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LegalizedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LegalizedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad dispensada/facturada del ítem (INT, default 0); aumenta por dispensación o factura, disminuye por reversión de dispensación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'UsedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad usada del Item, Cuando se crea la remision este campo es igual 0, pero este campo va aumentando cada vez que se haga una dispensación o factura del item. Disminuye si se realiza una reversión de la dispensación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'UsedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'UsedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad devuelta del ítem en remisión consignada (INT, default 0); refleja devoluciones al proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad devuelta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente del ítem en remisión; inicia igual a Quantity, disminuye por devoluciones/dispensaciones/ventas, aumenta por reversiones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea la remision este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la remision, se dispense o se venda el item. Aumenta con las reversiones de las dispensaciones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad inicial (INT) registrada al crear el detalle de remisión; suma validada de pendiente+usado+devuelto+transferido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad inicial con la que se registro el detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote/serie origen en reposición (FK autorreferencial, INT nullable); aplicable solo si movimiento es de tipo reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote del detalle de la Remision de Inventario en Consignación, solo se solicita si el tipo de movimiento es de reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) al lote/serie del producto en inventario; referencias a Inventory.BatchSerial para trazabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) al detalle de la remisión de inventario en consignación; identifica el movimiento padre', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de lote/serie en detalle de remisión de inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de lotes y seriales asociados a remisiones de inventario en consignación. Registra las cantidades por lote/serial de cada ítem en consignación: cuántas unidades se enviaron, están pendientes, fueron usadas, legalizadas, devueltas, repuestas, disminuidas o transferidas a otra consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades trasladadas o transferidas a otra consignación desde este lote/serial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentTransferQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'ConsignmentTransferQuantity';
