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
    CONSTRAINT [PK_ConsignmentInventoryRemissionDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial] CHECK ([Quantity]>(0)),
    CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_1] CHECK ([OutstandingQuantity]>=(0) AND [OutstandingQuantity]<=[Quantity]),
    CONSTRAINT [CK_ConsignmentInventoryRemissionDetailBatchSerial_Quantity] CHECK ([Quantity]=(([OutstandingQuantity]+[UsedQuantity])+[ReturnedQuantity])),
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
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetailBatchSerial_ConsignmentInventoryRemissionDetail];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetailBatchSerial_ConsignmentInventoryRemissionDetailBatchSerial];


GO
CREATE NONCLUSTERED INDEX [UX_ConsignmentInventoryRemissionDetailBatchSerial_ConsignmentInventoryRemissionDetailId]
    ON [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial]([ConsignmentInventoryRemissionDetailId] ASC);


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad a disminuir al limite maximo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'DecreaseQuantity';

