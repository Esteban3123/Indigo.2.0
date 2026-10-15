-- =============================================
-- Author:		HECTOR RODRIGUEZ
-- Create date: 2019 05 22
-- Description:	Actualiza la cantidad pendiente por ordenar en el detalle de solicitud de compra
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ConfirmPurchaseOrderFixedAsset]
	@XmlPurchaseOrden xml
AS
BEGIN
	SET NOCOUNT ON

	--Variables para guardar la cabecera
	DECLARE @Id INT, 
			@Code VARCHAR(20),
			@BudgetaryValidityId INT,
			@ThirdParty INT,
			@IsBasedContract BIT,
			@ContractId INT,			
			@TotalValue DECIMAL(20,4),
			@ContractValue DECIMAL(20,4),
			@ExecutedValue DECIMAL(20,4),
			@Status TINYINT,
			@UserCode VARCHAR(20),
			------------------------------
			@Message VARCHAR(MAX) = '',
			------------------------------
			@SubXml XML,
			@CommitmentCode VARCHAR(20) = ''

	BEGIN TRY		
		
		--Se obtienen los datos del xml para la cabecera
		SELECT	@Id = t.x.value ('Id[1]', 'int'),
				@Status = t.x.value ('Status[1]', 'tinyint'),
				@BudgetaryValidityId = t.x.value ('BudgetaryValidityId[1]', 'int')
		FROM @XmlPurchaseOrden.nodes ('/PurchaseOrden') t (x)

		SELECT	@Code = po.Code,
				@ThirdParty = s.IdThirdParty,
				@TotalValue = ISNULL(pod.TotalValue, 0),
				@UserCode = po.ConfirmationUser
		FROM FixedAsset.FixedAssetPurchaseOrder po
		JOIN Common.SuppliersDistributionLines sdl ON po.SupplierDistributionLineId = sdl.Id
		JOIN Common.Supplier s ON sdl.IdSupplier = s.Id
		LEFT JOIN 
		(
			SELECT	fapoi.PurchaseOrderId,
					SUM(fapoi.TotalValue) TotalValue
			FROM FixedAsset.FixedAssetPurchaseOrderItem fapoi
			GROUP BY fapoi.PurchaseOrderId
		) pod ON po.Id = pod.PurchaseOrderId
		WHERE po.Id = @Id

		IF @Status = 2
		BEGIN
			IF EXISTS (SELECT 1 FROM FixedAsset.FixedAssetPurchaseOrderAvailability WHERE FixedAssetPurchaseOrderId = @Id) 
			BEGIN --Si hay disponibilidades asociadas se realiza el compromiso
				--Se valida que todos los detalles pertenezcan a la misma vigencia
				IF EXISTS
				(
					SELECT 1
					FROM Budget.Availability a
					JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
					JOIN FixedAsset.FixedAssetPurchaseOrderAvailability poa ON ad.Id = poa.AvailabilityDetailId
					WHERE poa.FixedAssetPurchaseOrderId = @Id AND ISNULL(@BudgetaryValidityId, 0) <> a.BudgetaryValidityId
				)
				BEGIN
					SELECT	'999' AS CodeMessage, 
							'Los detalles corresponden a más de una vigencia presupuestal.' AS Message, 
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END

				IF EXISTS
				(
					SELECT 1
					FROM FixedAsset.FixedAssetPurchaseOrderAvailability poa 
					WHERE poa.FixedAssetPurchaseOrderId = @Id
					GROUP BY poa.FixedAssetPurchaseOrderId
					HAVING SUM(Value) <> @TotalValue
				)
				BEGIN
					SELECT	'999' AS CodeMessage, 
							'La sumatoria de los rubros no es igual al valor de la orden de compra.' AS Message, 
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END
						
				--Se valida que, si se agrego items, y estos tienen asociado un rubro, se hayan seleccionado disponibilidades con saldo suficiente para estos rubros
				IF EXISTS 
				(
					SELECT 1
					FROM 
					(
						SELECT	faic.BudgetId, 
								SUM(pod.TotalValue) Value
						FROM FixedAsset.FixedAssetPurchaseOrderItem pod
						JOIN FixedAsset.FixedAssetItem fai ON pod.ItemId = fai.Id
						JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
						WHERE pod.PurchaseOrderId = @Id AND faic.BudgetId IS NOT NULL
						GROUP BY faic.BudgetId
					) ticd
					LEFT JOIN
					(
						SELECT	ad.BudgetId, 
								SUM(poa.Value) Value
						FROM FixedAsset.FixedAssetPurchaseOrderAvailability poa 
						JOIN Budget.AvailabilityDetail ad ON ad.Id = poa.AvailabilityDetailId
						WHERE poa.FixedAssetPurchaseOrderId = @Id
						GROUP BY ad.BudgetId
					) tica ON ticd.BudgetId = tica.BudgetId
					WHERE ticd.Value > ISNULL(tica.Value, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - ' + CONCAT(c.Code, ' - ', c.Name, ' - ' , fs.Code, ' - ', fs.Name, ' - ', rt.Code, ' - ', rt.Name) + ': ' + FORMAT(ticd.Value, 'C0', 'es-CO')
						FROM Budget.Budget b
						JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
						JOIN Budget.Category c ON b.CategoryId = c.Id
						JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						JOIN
						(
							SELECT	faic.BudgetId, 
									SUM(pod.TotalValue) Value
							FROM FixedAsset.FixedAssetPurchaseOrderItem pod
							JOIN FixedAsset.FixedAssetItem fai ON pod.ItemId = fai.Id
							JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
							WHERE pod.PurchaseOrderId = @Id AND faic.BudgetId IS NOT NULL
							GROUP BY faic.BudgetId
						) ticd ON b.Id = ticd.BudgetId
						LEFT JOIN
						(
							SELECT	ad.BudgetId, 
									SUM(poa.Value) Value
							FROM FixedAsset.FixedAssetPurchaseOrderAvailability poa 
							JOIN Budget.AvailabilityDetail ad ON ad.Id = poa.AvailabilityDetailId
							WHERE poa.FixedAssetPurchaseOrderId = @Id
							GROUP BY ad.BudgetId
						) tica ON ticd.BudgetId = tica.BudgetId
						WHERE ticd.Value > ISNULL(tica.Value, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')

					SELECT	'999' AS CodeMessage, 
							'La sumatoria de los siguientes rubros de los artículos no corresponde con el total de los rubros de las disponibilidades: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS Message, 
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END

				--Se valida que el saldo de las disponibilidades no sea menor al valor a ejecutar
				IF EXISTS 
				(
					SELECT 1
					FROM FixedAsset.FixedAssetPurchaseOrderAvailability poa 
					JOIN Budget.AvailabilityDetail ad ON ad.Id = poa.AvailabilityDetailId
					WHERE poa.FixedAssetPurchaseOrderId = @Id AND poa.Value > ad.Balance
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - El saldo de la disponibilidad ' + a.Code + ' no puede ser menor al valor a ejecutar'
							FROM FixedAsset.FixedAssetPurchaseOrderAvailability poa 
							JOIN Budget.AvailabilityDetail ad ON ad.Id = poa.AvailabilityDetailId
							JOIN Budget.Availability a ON a.Id = ad.AvailabilityId
							WHERE poa.FixedAssetPurchaseOrderId = @Id AND poa.Value > ad.Balance
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')

					SELECT '999' AS CodeMessage, 
							ISNULL(@Message, '') AS Message, 
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END

				--Se genera el xml del compromiso
				SELECT @SubXml = convert
				(
					XML, 
					(
						SELECT *
						FROM 
						(
							SELECT	0 Id, 
									'' Code, 
									@BudgetaryValidityId BudgetaryValidityId, 
									@ThirdParty ThirdPartyId, 
									2 DocumentSource, 
									po.Code Document, 
									po.PurchaseOrderDate DocumentDate, 
									1 CommitmentType, 
									po.Detail Observations, 
									2 Status, 
									po.Id EntityId, 
									po.Code EntityCode, 
									'FixedAssetPurchaseOrder' EntityName
							FROM FixedAsset.FixedAssetPurchaseOrder po
							WHERE po.Id = @Id
						) Commitment
						JOIN 
						( 
							SELECT	0 Id, 
									0 CommitmentId, 
									t.AvailabilityDetailId AvailabilityDetailId, 
									b.CategoryId CategoryId, 
									b.RevenueTypeId RevenueTypeId, 
									po.DeliverDate ExpiredDate, 
									t.Value InitialValue, 
									0 DebitModificationValue, 
									0 CreditModificationValue, 
									t.Value TotalCommitment, 
									0 ExecutedValue, 
									t.Value Balance
							FROM FixedAsset.FixedAssetPurchaseOrderAvailability t
							JOIN Budget.AvailabilityDetail ad on ad.Id = t.AvailabilityDetailId
							JOIN Budget.Budget b on b.Id = ad.BudgetId
							JOIN FixedAsset.FixedAssetPurchaseOrder po on po.Id = t.FixedAssetPurchaseOrderId
							WHERE po.Id = @Id
						) CommitmentDetail on CommitmentDetail.CommitmentId = Commitment.Id
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				--Tabla de resultado para el sp del compromiso
				DECLARE @ResultCommitment table(CodeMessage int, Message varchar(max), CommitmentId int, CommitmentCode varchar(20))

				--Se ejecuta el sp del compromiso
				insert @ResultCommitment exec Budget.SP_SaveCommitment @SubXml, '' , @UserCode

				--Se valida el resultado
				IF EXISTS (SELECT 1 FROM @ResultCommitment WHERE CodeMessage = 999)
				BEGIN
					set @Message = (SELECT Message FROM @ResultCommitment WHERE CodeMessage = 999)
					SELECT '999' AS CodeMessage, @Message AS Message, CAST(3 AS TINYINT) AS [Status]
					RETURN
				END

				--Se obtiene el código del compromiso generado
				SET @CommitmentCode = (SELECT top 1 CommitmentCode FROM @ResultCommitment)
			END

			IF EXISTS 
			(
				SELECT 1
				FROM FixedAsset.FixedAssetPurchaseOrderItem pod
				JOIN Inventory.PurchaseRequestDetail prd ON pod.PurchaseRequestDetailId = prd.Id
				WHERE pod.PurchaseOrderId = @Id AND pod.Quantity > prd.OutstandingQuantity
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - ' + CONCAT(fai.Code, ' - ', fai.Description, ', cantidad pendiente ', prd.OutstandingQuantity, ', cantidad ordenada ', pod.Quantity)
						FROM FixedAsset.FixedAssetItem fai
						JOIN FixedAsset.FixedAssetPurchaseOrderItem pod ON fai.Id = pod.ItemId
						JOIN Inventory.PurchaseRequestDetail prd ON pod.PurchaseRequestDetailId = prd.Id
						JOIN Inventory.PurchaseRequest pr ON prd.PurchaseRequestId = pr.Id
						WHERE pod.PurchaseOrderId = @Id AND pod.Quantity > prd.OutstandingQuantity
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	'999' AS CodeMessage, 
						'La cantidad de los siguientes artículos superan la cantidad solicitada: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS Message, 
						CAST(3 AS TINYINT) AS [Status]
				RETURN
			END

			UPDATE prd
				SET OutstandingQuantity = PRD.OutstandingQuantity - POD.Quantity
			FROM FixedAsset.FixedAssetPurchaseOrderItem pod
			JOIN Inventory.PurchaseRequestDetail prd ON pod.PurchaseRequestDetailId = prd.Id
			WHERE pod.PurchaseOrderId = @Id

			UPDATE Inventory.PurchaseRequest
			SET Ordered = 1
			FROM 
			(
				SELECT	PRD.PurchaseRequestId,
						TOTAL = SUM(CASE WHEN PRD.OutstandingQuantity > 0 THEN 1 ELSE 0 END)
				FROM 
				(
					SELECT DISTINCT PRD1.PurchaseRequestId
					FROM FixedAsset.FixedAssetPurchaseOrderItem pod
					JOIN Inventory.PurchaseRequestDetail PRD1 ON PRD1.ID = pod.PurchaseRequestDetailId
					WHERE pod.PurchaseOrderId = @Id
				) PR
				JOIN Inventory.PurchaseRequestDetail PRD ON PRD.PurchaseRequestId = PR.PurchaseRequestId AND PRD.Status = 1 --APROBADO					
				GROUP BY PRD.PurchaseRequestId
			) T
			WHERE PurchaseRequest.ID = T.PurchaseRequestId AND T.TOTAL = 0

			SELECT	'0' AS CodeMessage,
					'La orden de compra se confirmó correctamente con código ' + @Code + IIF(@CommitmentCode = '', '', ' y se generó el compromiso con código ' + @CommitmentCode) AS Message,
					CAST(1 AS TINYINT) AS [Status]
		END
		ELSE IF @Status = 3
		BEGIN
		
			--Se valida que al desconfirmar la orden de compra no tenga asociado un compromiso
			IF EXISTS 
			(
				SELECT 1
				FROM FixedAsset.FixedAssetPurchaseOrder fapo
				JOIN Budget.Commitment c ON c.EntityId = fapo.Id AND c.EntityName = 'FixedAssetPurchaseOrder'
				WHERE fapo.Id = @Id
			)
			BEGIN
				SELECT  '999' AS CodeMessage, 
					'La orden de compra no se puede desconfirmar porque está asociada a un compromiso' AS Message, 
						CAST(3 AS TINYINT) AS [Status]
				RETURN
			END

			-- Actualización de la cantidad pendiente en los detalles de la solicitud de compra
			UPDATE prd
				SET OutstandingQuantity = prd.OutstandingQuantity + fapoi.Quantity
			FROM FixedAsset.FixedAssetPurchaseOrderItem fapoi
			JOIN Inventory.PurchaseRequestDetail prd ON fapoi.PurchaseRequestDetailId = prd.Id
			WHERE fapoi.PurchaseOrderId = @Id

			 -- Actualización de la solicitud de compra, estableciendo la cantidad pedida a cero
			UPDATE Inventory.PurchaseRequest
			SET Ordered = 0
			FROM 
			(
				SELECT	prd.PurchaseRequestId,
						total = SUM(CASE WHEN PRD.OutstandingQuantity > 0 THEN 1 ELSE 0 END)
				FROM 
				(
					SELECT DISTINCT prd1.PurchaseRequestId
					FROM FixedAsset.FixedAssetPurchaseOrder fapo
					JOIN FixedAsset.FixedAssetPurchaseOrderItem fapoi  ON fapoi.PurchaseOrderId = fapo.Id AND fapoi.PurchaseRequestDetailId IS NOT NULL
					JOIN Inventory.PurchaseRequestDetail prd1 ON prd1.ID = fapoi.PurchaseRequestDetailId
					WHERE fapo.Id = @Id
				) pr
				JOIN Inventory.PurchaseRequestDetail prd ON prd.PurchaseRequestId = pr.PurchaseRequestId AND prd.Status = 1 --Aprovado			
				GROUP BY prd.PurchaseRequestId
			) t
			WHERE PurchaseRequest.Id = t.PurchaseRequestId 
			AND t.total > 0

		SELECT	'0' AS CodeMessage,
					'El documento se desconfirmó correctamente' AS Message,
					CAST(1 AS TINYINT) AS [Status]
		END
	END TRY
	BEGIN CATCH
		SELECT	'999' AS CodeMessage,
				'Ocurrio un error al afectar la cantidad sobrante en la solicitud de compra: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message,
				CAST(3 AS TINYINT) AS [Status]
	END CATCH
END