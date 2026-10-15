
-- =============================================
-- Author:		HECTOR RODRIGUEZ
-- Create date: 2019 05 08
-- Description:	Actualiza la cantidad pendiente por ordenar en el detalle de solicitud de compra
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ConfirmPurchaseOrder]
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
			@ManageProducts BIT,			
			@ContractValue DECIMAL(20,4),
			@ExecutedValue DECIMAL(20,4),
			@TotalValue DECIMAL(20,4),
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
				@IsBasedContract = po.IsBasedContract, 
				@ContractId = po.ContractId,
				@ThirdParty = s.IdThirdParty,
				@TotalValue = po.TotalValue,
				@UserCode = po.ConfirmationUser
		FROM Inventory.PurchaseOrder po
		JOIN Common.Supplier s ON po.SupplierId = s.Id
		WHERE po.Id = @Id

		IF @Status = 2
		BEGIN
			--Se valida si la orden de compra es basada en un contrato
			IF @IsBasedContract = 1
			BEGIN
				SELECT	@ManageProducts = ic.ManageProducts,
						@ContractValue = ic.TotalValue + ic.ModificationValue
				FROM Inventory.InventoryContract ic
				WHERE ic.Id = @ContractId

				IF @ManageProducts = 1
				BEGIN -- Si el contrato maneja productos se valida que las cantidades de la orden de compra no superen las cantidades del contrato
					IF EXISTS 
					(
						SELECT 1
						FROM Inventory.PurchaseOrderDetail pod
						LEFT JOIN Inventory.InventoryContractDetail cd ON pod.ProductId = cd.ProductId AND cd.InventoryContractId = @ContractId
						WHERE pod.PurchaseOrderId = @Id AND pod.Quantity > ISNULL(cd.OutstandingQuantity, 0)
					)
					BEGIN
						SELECT @Message = STUFF((
								SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - ' + CONCAT(p.Code, ' - ', p.Name)
								FROM Inventory.InventoryProduct p
								JOIN Inventory.PurchaseOrderDetail pod ON p.Id = pod.ProductId
								LEFT JOIN Inventory.InventoryContractDetail cd ON pod.ProductId = cd.ProductId AND cd.InventoryContractId = @ContractId
								WHERE pod.PurchaseOrderId = @Id AND pod.Quantity > ISNULL(cd.OutstandingQuantity, 0)
								FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

						SELECT	'999' AS CodeMessage, 
								'La cantidad de los siguientes productos superan la cantidad del contrato: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS Message, 
								CAST(3 AS TINYINT) AS [Status]
						RETURN
					END
				END

				SELECT @ExecutedValue = SUM(e.TotalValue)
				FROM 
				(
					SELECT	ISNULL
							(
								ROUND((ISNULL(pod.Quantity, 0) - ISNULL(podd.DevolutionQuantity, 0)) * pod.Value, 0) + ROUND((ISNULL(pod.Quantity, 0) - ISNULL(podd.DevolutionQuantity, 0)) * pod.Value * (pod.IvaPercentage) / 100, 0), 
								po.TotalValue
							) TotalValue
					FROM Inventory.PurchaseOrder po
					LEFT JOIN Inventory.PurchaseOrderDetail pod ON po.Id = pod.PurchaseOrderId
					LEFT JOIN
					(
						SELECT podd.PurchaseOrderDetailId, SUM(podd.Quantity) DevolutionQuantity
						FROM Inventory.PurchaseOrderDevolution pod
						JOIN Inventory.PurchaseOrderDevolutionDetail podd ON pod.Id = podd.PurchaseOrderDevolutionId
						WHERE pod.Status = 2
						GROUP BY podd.PurchaseOrderDetailId
					) podd ON pod.Id = podd.PurchaseOrderDetailId
					WHERE po.Id <> @Id AND po.Status = 2 AND po.ContractId = @ContractId

					UNION ALL

					SELECT evd.TotalValue
					FROM Inventory.EntranceVoucher ev
					JOIN Inventory.EntranceVoucherDetail evd ON ev.Id = evd.EntranceVoucherId
					JOIN Inventory.InventoryContractDetail icd ON evd.ContractDetailId = icd.Id
					WHERE ev.Status = 2 AND icd.InventoryContractId = @ContractId
				) e

				--Se valida que no se supere el valor del contrato
				IF (ISNULL(@ExecutedValue, 0) + ISNULL(@TotalValue, 0)) > ISNULL(@ContractValue, 0)
				BEGIN
					SELECT	'999' AS CodeMessage, 
							'Se ha superado el valor del contrato (Valor del contrato: ' + FORMAT(ISNULL(@ContractValue, 0), 'C0', 'es-CO') + ', Saldo: ' + FORMAT((ISNULL(@ContractValue, 0) - ISNULL(@ExecutedValue, 0)), 'C0', 'es-CO') + ').' AS Message, 
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END

				-- Si los productos manejan rubros se valida que no se supere el valor por rubro
				IF EXISTS 
				(
					SELECT 1
					FROM
					(
						select temp1.BudgetId, temp1.Value + ISNULL(temp2.Value, 0) Value
						from
						(
							SELECT ad.BudgetId, SUM(Ica.Value) Value
							FROM Inventory.InventoryContractAvailability Ica
							JOIN Budget.AvailabilityDetail ad ON Ica.AvailabilityDetailId = ad.Id
							WHERE Ica.InventoryContractId = @ContractId
							GROUP BY ad.BudgetId
						) temp1
						left join
						(
							select ad.BudgetId, SUM(cma.Value) Value
							from Inventory.InventoryContractModification cm
							inner join Inventory.InventoryContractModificationAvailability cma on cma.InventoryContractModificationId = cm.Id
							inner join Budget.AvailabilityDetail ad on ad.Id = cma.AvailabilityDetailId
							where cm.Status = 2 and cm.ContractId = @ContractId
							Group By ad.BudgetId
						) temp2 on temp2.BudgetId = temp1.BudgetId
						Group By temp1.BudgetId, temp1.Value, temp2.Value
					) ic
					JOIN
					(
						SELECT pg.BudgetId, SUM(pg.TotalValue) Value
						FROM
						(
								SELECT	pg.BudgetId, 
										ROUND((pod.Quantity - ISNULL(podd.DevolutionQuantity, 0)) * pod.Value, 0) +
										ROUND((pod.Quantity - ISNULL(podd.DevolutionQuantity, 0)) * pod.Value * (pod.IvaPercentage) / 100, 0) TotalValue
								FROM Inventory.PurchaseOrder po
								JOIN Inventory.PurchaseOrderDetail pod ON po.Id = pod.PurchaseOrderId
								JOIN Inventory.InventoryProduct ip ON pod.ProductId = ip.Id
								JOIN Inventory.ProductGroup pg ON IP.ProductGroupId = pg.Id
								LEFT JOIN
								(
									SELECT podd.PurchaseOrderDetailId, SUM(podd.Quantity) DevolutionQuantity
									FROM Inventory.PurchaseOrderDevolution pod
									JOIN Inventory.PurchaseOrderDevolutionDetail podd ON pod.Id = podd.PurchaseOrderDevolutionId
									WHERE pod.Status = 2
									GROUP BY podd.PurchaseOrderDetailId
								) podd ON pod.Id = podd.PurchaseOrderDetailId
								WHERE pg.AffectBudget = 1 AND pg.BudgetId IS NOT NULL
									AND 
									(
										po.Id = @Id OR (po.Id <> @Id AND po.Status = 2)
									) AND po.ContractId = @ContractId
							UNION ALL
								SELECT pg.BudgetId, evd.TotalValue
								FROM Inventory.EntranceVoucher ev
								JOIN Inventory.EntranceVoucherDetail evd ON ev.Id = evd.EntranceVoucherId
								JOIN Inventory.InventoryContractDetail icd ON evd.ContractDetailId = icd.Id
								JOIN Inventory.InventoryProduct ip ON evd.ProductId = ip.Id
								JOIN Inventory.ProductGroup pg ON IP.ProductGroupId = pg.Id
								WHERE pg.AffectBudget = 1 AND pg.BudgetId IS NOT NULL
									AND ev.Status = 2 AND icd.InventoryContractId = @ContractId
						) pg
						JOIN
						(
							SELECT pg.BudgetId
							FROM Inventory.PurchaseOrder po
							JOIN Inventory.PurchaseOrderDetail pod ON po.Id = pod.PurchaseOrderId
							JOIN Inventory.InventoryProduct ip ON pod.ProductId = ip.Id
							JOIN Inventory.ProductGroup pg ON IP.ProductGroupId = pg.Id
							WHERE po.Id = @Id
							GROUP BY pg.BudgetId
						) b ON pg.BudgetId = b.BudgetId
						GROUP BY pg.BudgetId
					) icd ON ic.BudgetId = icd.BudgetId
					WHERE icd.Value > ic.Value
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Contratado (' + FORMAT(ic.Value, 'C0', 'es-CO') + ') - Ejecutado(' + FORMAT(icd.Value, 'C0', 'es-CO') + ')'
							FROM 
							(
								SELECT ad.BudgetId, SUM(Ica.Value) Value
								FROM Inventory.InventoryContractAvailability Ica
								JOIN Budget.AvailabilityDetail ad ON Ica.AvailabilityDetailId = ad.Id
								WHERE Ica.InventoryContractId = @ContractId
								GROUP BY ad.BudgetId
							) ic
							JOIN
							(
								SELECT pg.BudgetId, SUM(pg.TotalValue) Value
								FROM
								(
										SELECT	pg.BudgetId, 
												ROUND((pod.Quantity - ISNULL(podd.DevolutionQuantity, 0)) * pod.Value, 0) +
												ROUND((pod.Quantity - ISNULL(podd.DevolutionQuantity, 0)) * pod.Value * (pod.IvaPercentage) / 100, 0) TotalValue
										FROM Inventory.PurchaseOrder po
										JOIN Inventory.PurchaseOrderDetail pod ON po.Id = pod.PurchaseOrderId
										JOIN Inventory.InventoryProduct ip ON pod.ProductId = ip.Id
										JOIN Inventory.ProductGroup pg ON IP.ProductGroupId = pg.Id
										LEFT JOIN
										(
											SELECT podd.PurchaseOrderDetailId, SUM(podd.Quantity) DevolutionQuantity
											FROM Inventory.PurchaseOrderDevolution pod
											JOIN Inventory.PurchaseOrderDevolutionDetail podd ON pod.Id = podd.PurchaseOrderDevolutionId
											WHERE pod.Status = 2
											GROUP BY podd.PurchaseOrderDetailId
										) podd ON pod.Id = podd.PurchaseOrderDetailId
										WHERE pg.AffectBudget = 1 AND pg.BudgetId IS NOT NULL
											AND 
											(
												po.Id = @Id OR (po.Id <> @Id AND po.Status = 2)
											) AND po.ContractId = @ContractId
									UNION ALL
										SELECT pg.BudgetId, evd.TotalValue
										FROM Inventory.EntranceVoucher ev
										JOIN Inventory.EntranceVoucherDetail evd ON ev.Id = evd.EntranceVoucherId
										JOIN Inventory.InventoryContractDetail icd ON evd.ContractDetailId = icd.Id
										JOIN Inventory.InventoryProduct ip ON evd.ProductId = ip.Id
										JOIN Inventory.ProductGroup pg ON IP.ProductGroupId = pg.Id
										WHERE pg.AffectBudget = 1 AND pg.BudgetId IS NOT NULL
											AND ev.Status = 2 AND icd.InventoryContractId = @ContractId
								) pg
								JOIN
								(
									SELECT pg.BudgetId
									FROM Inventory.PurchaseOrder po
									JOIN Inventory.PurchaseOrderDetail pod ON po.Id = pod.PurchaseOrderId
									JOIN Inventory.InventoryProduct ip ON pod.ProductId = ip.Id
									JOIN Inventory.ProductGroup pg ON IP.ProductGroupId = pg.Id
									WHERE po.Id = @Id
									GROUP BY pg.BudgetId
								) b ON pg.BudgetId = b.BudgetId
								GROUP BY pg.BudgetId
							) icd ON ic.BudgetId = icd.BudgetId
							JOIN Budget.Budget b ON ic.BudgetId = b.Id
							JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
							JOIN Budget.Category c ON b.CategoryId = c.Id
							LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
							WHERE icd.Value > ic.Value
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT	'999' AS CodeMessage, 
							'Se ha superado el valor de los siguientes rubros del contrato: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS Message, 
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END

				--Se actualizan las cantidades disponibles del contrato con las cantidades seleccionadas en el detalle de la orden de compra
				UPDATE cd 
					SET cd.OutstandingQuantity -= pod.Quantity
				FROM Inventory.InventoryContractDetail cd
				JOIN Inventory.PurchaseOrderDetail pod ON cd.ProductId = pod.ProductId AND pod.PurchaseOrderId = @Id 
				WHERE cd.InventoryContractId = @ContractId
			END
			ELSE
			BEGIN
				IF EXISTS (SELECT 1 FROM Inventory.PurchaseOrderAvailability WHERE PurchaseOrderId = @Id) 
				BEGIN --Si no esta basado en un contrato y hay disponibilidades asociadas se realiza el compromiso
					--Se valida que todos los detalles pertenezcan a la misma vigencia
					IF EXISTS
					(
						SELECT 1
						FROM Budget.Availability a
						JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
						JOIN Inventory.PurchaseOrderAvailability poa ON ad.Id = poa.AvailabilityDetailId
						WHERE poa.PurchaseOrderId = @Id AND ISNULL(@BudgetaryValidityId, 0) <> a.BudgetaryValidityId
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
						FROM Inventory.PurchaseOrderAvailability poa 
						WHERE poa.PurchaseOrderId = @Id
						GROUP BY poa.PurchaseOrderId
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
							SELECT	pg.BudgetId, 
									SUM(pod.TotalValue) Value
							FROM Inventory.PurchaseOrderDetail pod
							JOIN Inventory.InventoryProduct p ON pod.ProductId = p.Id
							JOIN Inventory.ProductGroup pg ON p.ProductGroupId = pg.Id
							WHERE pod.PurchaseOrderId = @Id AND pg.BudgetId IS NOT NULL
							GROUP BY pg.BudgetId
						) ticd
						LEFT JOIN
						(
							SELECT	ad.BudgetId, 
									SUM(poa.Value) Value
							FROM Inventory.PurchaseOrderAvailability poa 
							JOIN Budget.AvailabilityDetail ad ON ad.Id = poa.AvailabilityDetailId
							WHERE poa.PurchaseOrderId = @Id
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
								SELECT	pg.BudgetId, 
										SUM(pod.TotalValue) Value
								FROM Inventory.PurchaseOrderDetail pod
								JOIN Inventory.InventoryProduct p ON pod.ProductId = p.Id
								JOIN Inventory.ProductGroup pg ON p.ProductGroupId = pg.Id
								WHERE pod.PurchaseOrderId = @Id AND pg.BudgetId IS NOT NULL
								GROUP BY pg.BudgetId
							) ticd ON b.Id = ticd.BudgetId
							LEFT JOIN
							(
								SELECT	ad.BudgetId, 
										SUM(poa.Value) Value
								FROM Inventory.PurchaseOrderAvailability poa 
								JOIN Budget.AvailabilityDetail ad ON ad.Id = poa.AvailabilityDetailId
								WHERE poa.PurchaseOrderId = @Id
								GROUP BY ad.BudgetId
							) tica ON ticd.BudgetId = tica.BudgetId
							WHERE ticd.Value > ISNULL(tica.Value, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')

						SELECT	'999' AS CodeMessage, 
								'La sumatoria de los siguientes rubros de los productos no corresponde con el total de los rubros de las disponibilidades: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS Message, 
								CAST(3 AS TINYINT) AS [Status]
						RETURN
					END

					--Se valida que el saldo de las disponibilidades no sea menor al valor a ejecutar
					IF EXISTS 
					(
						SELECT 1
						FROM Inventory.PurchaseOrderAvailability poa 
						JOIN Budget.AvailabilityDetail ad ON ad.Id = poa.AvailabilityDetailId
						WHERE poa.PurchaseOrderId = @Id AND poa.Value > ad.Balance
					)
					BEGIN
						SELECT @Message = STUFF((
								SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - El saldo de la disponibilidad ' + a.Code + ' no puede ser menor al valor a ejecutar'
								FROM Inventory.PurchaseOrderAvailability poa 
								JOIN Budget.AvailabilityDetail ad ON ad.Id = poa.AvailabilityDetailId
								JOIN Budget.Availability a ON a.Id = ad.AvailabilityId
								WHERE poa.PurchaseOrderId = @Id AND poa.Value > ad.Balance
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
										po.DocumentDate DocumentDate, 
										1 CommitmentType, 
										CONCAT(po.Description, CHAR(13) + CHAR(10), 'Vigencia Hasta: ', CONVERT(VARCHAR, po.DeliveredDate, 106))  Observations, 
										1 Status, 
										po.Id EntityId, 
										po.Code EntityCode, 
										'PurchaseOrder' EntityName
								FROM Inventory.PurchaseOrder po
								WHERE po.Id = @Id
							) Commitment
							JOIN 
							( 
								SELECT	0 Id, 
										0 CommitmentId, 
										t.AvailabilityDetailId AvailabilityDetailId, 
										b.CategoryId CategoryId, 
										b.RevenueTypeId RevenueTypeId, 
										DATEFROMPARTS(YEAR(po.DeliveredDate), 12, 31) ExpiredDate, 
										t.Value InitialValue, 
										0 DebitModificationValue, 
										0 CreditModificationValue, 
										t.Value TotalCommitment, 
										0 ExecutedValue, 
										t.Value Balance
								FROM Inventory.PurchaseOrderAvailability t
								JOIN Budget.AvailabilityDetail ad on ad.Id = t.AvailabilityDetailId
								JOIN Budget.Budget b on b.Id = ad.BudgetId
								JOIN Inventory.PurchaseOrder po on po.Id = t.PurchaseOrderId
								WHERE po.Id = @Id
							) CommitmentDetail on CommitmentDetail.CommitmentId = Commitment.Id
							For xml AUTO,TYPE, ELEMENTS
						)
					)

					--Tabla de resultado para el sp del compromiso
					DECLARE @ResultCommitment table(CodeMessage int, Message varchar(max), CommitmentId int, CommitmentCode varchar(20))

					--Se ejecuta el sp del compromiso
					insert @ResultCommitment exec Budget.SP_SaveCommitment @SubXml, '', @UserCode

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
			END

			IF EXISTS 
			(
				SELECT 1
				FROM Inventory.PurchaseOrderDetail pod
				JOIN Inventory.PurchaseRequestDetail prd ON pod.PurchaseRequestDetailId = prd.Id
				WHERE pod.PurchaseOrderId = @Id AND pod.Quantity > prd.OutstandingQuantity
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - ' + CONCAT(p.Code, ' - ', p.Name, ', cantidad pendiente ', prd.OutstandingQuantity, ', cantidad ordenada ', pod.Quantity)
						FROM Inventory.InventoryProduct p
						JOIN Inventory.PurchaseOrderDetail pod ON p.Id = pod.ProductId
						JOIN Inventory.PurchaseRequestDetail prd ON pod.PurchaseRequestDetailId = prd.Id
						JOIN Inventory.PurchaseRequest pr ON prd.PurchaseRequestId = pr.Id
						WHERE pod.PurchaseOrderId = @Id AND pod.Quantity > prd.OutstandingQuantity
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	'999' AS CodeMessage, 
						'La cantidad de los siguientes productos superan la cantidad solicitada: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS Message, 
						CAST(3 AS TINYINT) AS [Status]
				RETURN
			END

			UPDATE prd
				SET OutstandingQuantity = PRD.OutstandingQuantity - POD.Quantity
			FROM Inventory.PurchaseOrderDetail pod
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
					FROM Inventory.PurchaseOrderDetail pod
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
				FROM Inventory.PurchaseOrder po
				JOIN Budget.Commitment c ON c.EntityId = po.Id AND c.EntityName = 'PurchaseOrder'
				WHERE po.Id = @Id
			)
			BEGIN
				SELECT	'999' AS CodeMessage, 
						'La orden de compra no se puede desconfirmar porque esta asociada a un compromiso' AS Message, 
						CAST(3 AS TINYINT) AS [Status]
				RETURN
			END

			DECLARE @DocumentOrigin AS VARCHAR(20) --Para obtener el código si la orden esta en una remisión o comprobante

			--Se valida que al desconfirmar la orden de compra no tenga una remisión de entrada asociada
			--IF EXISTS 
			--(
			--	select 1
			--	from [Inventory].[RemissionEntranceDetail] 
			--	where SourceCode = @code
			--		AND RemissionSource = 2

			--)
			--BEGIN
			--	SET @DocumentOrigin = (SELECT TOP 1 re.Code
			--						   FROM Inventory.RemissionEntranceDetail red
			--						   JOIN Inventory.RemissionEntrance re ON re.Id = red.RemissionEntranceId
			--						   WHERE SourceCode = @code
			--						   	AND RemissionSource = 2)

			--	SELECT	'999' AS CodeMessage, 
			--			  'La orden de compra no se puede desconfirmar porque esta asociada a la remisión de entrada '+ @DocumentOrigin AS Message, 
			--			CAST(3 AS TINYINT) AS [Status]

			--	RETURN
			--END

			----Se valida que al desconfirmar la orden de compra no tenga un comprobante de entrada asociado
			--IF EXISTS 
			--(
			--	select 1 
			--	from [Inventory].[EntranceVoucherDetail] evd 
			--	where evd.SourceCode = @code
			--		AND evd.EntranceSource = 2
			--)
			--BEGIN
			--	SET @DocumentOrigin = (SELECT TOP 1 ev.Code
			--						   FROM Inventory.EntranceVoucherDetail evd 
			--						   JOIN Inventory.EntranceVoucher ev ON ev.Id = evd.EntranceVoucherId
			--						   WHERE evd.SourceCode = @Code
			--							AND evd.EntranceSource = 2)

			--	SELECT	'999' AS CodeMessage, 
			--			'La orden de compra no se puede desconfirmar porque esta asociada al comprobante de entrada '+ @DocumentOrigin AS Message, 
			--			CAST(3 AS TINYINT) AS [Status] 
			--	RETURN
			--END

			UPDATE prd
				SET OutstandingQuantity = PRD.OutstandingQuantity + POD.Quantity
			FROM Inventory.PurchaseOrderDetail pod
			JOIN Inventory.PurchaseRequestDetail prd ON pod.PurchaseRequestDetailId = prd.Id
			WHERE pod.PurchaseOrderId = @Id

			UPDATE Inventory.PurchaseRequest
			SET Ordered = 0
			FROM 
			(
				SELECT	PRD.PurchaseRequestId,
						TOTAL = SUM(CASE WHEN PRD.OutstandingQuantity > 0 THEN 1 ELSE 0 END)
				FROM 
				(
					SELECT DISTINCT PRD1.PurchaseRequestId
					FROM Inventory.PurchaseOrder PO
					JOIN Inventory.PurchaseOrderDetail POD ON POD.PurchaseOrderId = PO.Id AND POD.PurchaseRequestDetailId IS NOT NULL
					JOIN Inventory.PurchaseRequestDetail PRD1 ON PRD1.ID = POD.PurchaseRequestDetailId
					WHERE PO.ID = @Id
				) PR
				JOIN Inventory.PurchaseRequestDetail PRD ON PRD.PurchaseRequestId = PR.PurchaseRequestId AND PRD.Status = 1 --APROBADO
				GROUP BY PRD.PurchaseRequestId
			) T
			WHERE PurchaseRequest.ID = T.PurchaseRequestId
			AND T.TOTAL > 0
			
			SELECT	'0' AS CodeMessage,
					'El registro se desconfirmó correctamente' AS Message,
					CAST(1 AS TINYINT) AS [Status]
		END
	END TRY
	BEGIN CATCH
		SELECT	'999' AS CodeMessage,
				'Ocurrio un error al afectar la cantidad sobrante en la solicitud de compra: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message,
				CAST(3 AS TINYINT) AS [Status]
	END CATCH
END