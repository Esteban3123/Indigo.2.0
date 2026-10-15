-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 2017-01-05
-- Description:	Proceso de Cierre Presupuestal de Gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ClosingValidityExpense]
	@ValidityId INT,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	--Declaracion de variables necesarias
	DECLARE @Year INT,
			@ExpenseMonth INT,
			@Status TINYINT,
			@IsESE BIT,
			@NextValidityId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	BEGIN TRY
		--Se obtiene el año actual a cerrar
		SELECT @Year = bv.[Year], @ExpenseMonth = bv.ExpenseMonth, @Status = bv.Status, @IsESE = be.ESE
		FROM Budget.BudgetaryValidity bv
		JOIN Budget.BudgetaryEntity be ON bv.BudgetaryEntityId = be.Id
		WHERE bv.Id = @ValidityId

		--Se obtiene la siguiente vigencia presupuestal
		SELECT @NextValidityId = Id
		FROM Budget.BudgetaryValidity 
		WHERE Year = (@Year + 1)

		/********************************** VALIDACION VIGENCIA **********************************/

		--Valido que haya arrojado resultado de la vigencia
		IF @Year IS NULL
		BEGIN
			SELECT 999 AS CodeResult, 'La vigencia presupuestal no existe' AS MessageResult
			RETURN
		END

		--Valido que todos los meses de la vigencia de ingresos se hayan cerrado
		IF @ExpenseMonth < 13 
		BEGIN
			SELECT 999 AS CodeResult, 'Debe cerrar todos los periodos de la vigencia presupuestal' AS MessageResult
			RETURN
		END

		--Valido que no se haya realizado el cierre anual de la vigencia
		IF @ExpenseMonth > 13 
		BEGIN
			SELECT 999 AS CodeResult, 'La vigencia presupuestal ya se encuentra cerrada' AS MessageResult
			RETURN
		END

		--Valido que se haya creado la siguiente vigencia
		IF @NextValidityId IS NULL
		BEGIN
			SELECT 999 AS CodeResult, CONCAT('Debe crear la vigencia presupuestal del año ', (@Year + 1)) AS MessageResult
			RETURN
		END

		/*********************************** VALIDACION ESTADO ***********************************/

		--Valido que no existan traslados presupuestales sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.BudgetTransfer WHERE BudgetaryValidityId = @ValidityId AND DocumentSource = 2 AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.BudgetTransfer 
					WHERE BudgetaryValidityId = @ValidityId AND DocumentSource = 2 AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes traslados presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones presupuestales sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.BudgetModification WHERE BudgetaryValidityId = @ValidityId AND DocumentSource = 2 AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.BudgetModification 
					WHERE BudgetaryValidityId = @ValidityId AND DocumentSource = 2 AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan disponibilidades sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.Availability WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Availability
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes disponibilidades presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de disponibilidades sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.AvailabilityModification WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.AvailabilityModification
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de disponibilidades presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan compromisos sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.Commitment WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Commitment
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes compromisos presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de compromisos sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.CommitmentModification WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.CommitmentModification
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de compromisos presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan obligaciones sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.Obligation WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Obligation
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes obligaciones presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de obligaciones sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.ObligationModification WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.ObligationModification
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de obligaciones presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan ordenes de pago sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.PaymentOrder WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.PaymentOrder
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes ordenes de pago presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan reintegros sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.ReimbursementResource WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.ReimbursementResource
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes reintegros presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		/******************************** VALIDACION AÑO VIGENCIA ********************************/

		--Valido que no existan traslados con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.BudgetTransfer WHERE DocumentSource = 2 AND Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.BudgetTransfer 
					WHERE DocumentSource = 2 AND Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes traslados presupuestales no corresponden con la vigencia' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones presupuestales con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.BudgetModification WHERE DocumentSource = 2 AND Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.BudgetModification 
					WHERE DocumentSource = 2 AND Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan disponibilidades con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.Availability WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Availability
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes disponibilidades presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de disponibilidades con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.AvailabilityModification WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.AvailabilityModification
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de disponibilidades presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan compromisos con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.Commitment WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId AND CommitmentType <> 2)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Commitment
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId AND CommitmentType <> 2
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes compromisos presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de compromisos con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.CommitmentModification WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.CommitmentModification
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de compromisos presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan obligaciones con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.Obligation WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId AND ObligationType <> 2)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Obligation
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId AND ObligationType <> 2
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes obligaciones presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de obligaciones con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.ObligationModification WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.ObligationModification
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de obligaciones presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan ordenes de pago con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.PaymentOrder WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.PaymentOrder
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes ordenes de pago presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan reintegros con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.ReimbursementResource WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.ReimbursementResource
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes reintegros presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		/***********************************  VALIDACION TOTAL ***********************************/

		--Valido que el total de los rubros presupuestales coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT b.Id, b.InitialValue, b.TotalBudget TotalValue
				FROM Budget.Budget b
				JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 2
				WHERE bh.BudgetaryValidityId = @ValidityId
			) b
			FULL JOIN
			(
				SELECT bm.Id, SUM(bm.DebitValue) DebitValue, SUM(bm.CreditValue) CreditValue
				FROM
				(
					SELECT btd.BudgetId Id, SUM(IIF(btd.Nature = 1, btd.Value, 0)) DebitValue, SUM(IIF(btd.Nature = 2, btd.Value, 0)) CreditValue
					FROM Budget.BudgetTransfer bt
					JOIN Budget.BudgetTransferDetail btd ON bt.Id = btd.TransferId
					WHERE bt.BudgetaryValidityId = @ValidityId AND bt.Status = 2 AND bt.DocumentSource = 2
					GROUP BY btd.BudgetId
				UNION ALL
					SELECT bmd.BudgetId, SUM(IIF(bmd.Nature = 1, bmd.Value, 0)) DebitValue, SUM(IIF(bmd.Nature = 2, bmd.Value, 0)) CreditValue
					FROM Budget.BudgetModification bm
					JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
					WHERE bm.BudgetaryValidityId = @ValidityId AND bm.Status = 2 AND bm.DocumentSource = 2
					GROUP BY bmd.BudgetId
				) bm
				GROUP BY bm.Id
			) bm ON b.Id = bm.Id
			WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Total (' + FORMAT(ISNULL(b.TotalValue, 0), 'C0', 'es-CO') + ') - Total Real(' + FORMAT(ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT b.Id, b.InitialValue, b.TotalBudget TotalValue
						FROM Budget.Budget b
						JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 2
						WHERE bh.BudgetaryValidityId = @ValidityId
					) b
					FULL JOIN
					(
						SELECT bm.Id, SUM(bm.DebitValue) DebitValue, SUM(bm.CreditValue) CreditValue
						FROM
						(
							SELECT btd.BudgetId Id, SUM(IIF(btd.Nature = 1, btd.Value, 0)) DebitValue, SUM(IIF(btd.Nature = 2, btd.Value, 0)) CreditValue
							FROM Budget.BudgetTransfer bt
							JOIN Budget.BudgetTransferDetail btd ON bt.Id = btd.TransferId
							WHERE bt.BudgetaryValidityId = @ValidityId AND bt.Status = 2 AND bt.DocumentSource = 2
							GROUP BY btd.BudgetId
						UNION ALL
							SELECT bmd.BudgetId, SUM(IIF(bmd.Nature = 1, bmd.Value, 0)) DebitValue, SUM(IIF(bmd.Nature = 2, bmd.Value, 0)) CreditValue
							FROM Budget.BudgetModification bm
							JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
							WHERE bm.BudgetaryValidityId = @ValidityId AND bm.Status = 2 AND bm.DocumentSource = 2
							GROUP BY bmd.BudgetId
						) bm
						GROUP BY bm.Id
					) bm ON b.Id = bm.Id
					LEFT JOIN Budget.Budget bt ON ISNULL(b.Id, bm.Id) = bt.Id
					LEFT JOIN Budget.RevenueType rt ON bt.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON bt.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor total de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el total de las disponibilidades coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT rd.Id, rd.InitialValue, rd.TotalAvailability TotalValue
				FROM Budget.Availability r
				JOIN Budget.AvailabilityDetail rd ON r.Id = rd.AvailabilityId
				WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
			) b
			FULL JOIN
			(
				SELECT rmd.AvailabilityDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
				FROM Budget.AvailabilityModification rm
				JOIN Budget.AvailabilityModificationDetail rmd ON rm.Id = rmd.AvailabilityModificationId
				WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
				GROUP BY rmd.AvailabilityDetailId
			) bm ON b.Id = bm.Id
			WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Disponibilidad ' + r.Code + ': Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Total (' + FORMAT(ISNULL(b.TotalValue, 0), 'C0', 'es-CO') + ') - Total Real(' + FORMAT(ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT rd.Id, rd.InitialValue, rd.TotalAvailability TotalValue
						FROM Budget.Availability r
						JOIN Budget.AvailabilityDetail rd ON r.Id = rd.AvailabilityId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
					) b
					FULL JOIN
					(
						SELECT rmd.AvailabilityDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
						FROM Budget.AvailabilityModification rm
						JOIN Budget.AvailabilityModificationDetail rmd ON rm.Id = rmd.AvailabilityModificationId
						WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
						GROUP BY rmd.AvailabilityDetailId
					) bm ON b.Id = bm.Id
					LEFT JOIN Budget.AvailabilityDetail rd ON ISNULL(b.Id, bm.Id) = rd.Id
					LEFT JOIN Budget.Availability r ON rd.AvailabilityId = r.Id
					LEFT JOIN Budget.Budget bt ON rd.BudgetId = bt.Id					
					LEFT JOIN Budget.RevenueType rt ON bt.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON bt.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor total de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el total de los compromisos coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT rd.Id, rd.InitialValue, rd.TotalCommitment TotalValue
				FROM Budget.Commitment r
				JOIN Budget.CommitmentDetail rd ON r.Id = rd.CommitmentId
				WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
			) b
			FULL JOIN
			(
				SELECT rmd.CommitmentDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
				FROM Budget.CommitmentModification rm
				JOIN Budget.CommitmentModificationDetail rmd ON rm.Id = rmd.CommitmentModificationId
				WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
				GROUP BY rmd.CommitmentDetailId
			) bm ON b.Id = bm.Id
			WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Compromiso ' + r.Code + ': Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Total (' + FORMAT(ISNULL(b.TotalValue, 0), 'C0', 'es-CO') + ') - Total Real(' + FORMAT(ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT rd.Id, rd.InitialValue, rd.TotalCommitment TotalValue
						FROM Budget.Commitment r
						JOIN Budget.CommitmentDetail rd ON r.Id = rd.CommitmentId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
					) b
					FULL JOIN
					(
						SELECT rmd.CommitmentDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
						FROM Budget.CommitmentModification rm
						JOIN Budget.CommitmentModificationDetail rmd ON rm.Id = rmd.CommitmentModificationId
						WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
						GROUP BY rmd.CommitmentDetailId
					) bm ON b.Id = bm.Id
					LEFT JOIN Budget.CommitmentDetail rd ON ISNULL(b.Id, bm.Id) = rd.Id
					LEFT JOIN Budget.Commitment r ON rd.CommitmentId = r.Id			
					LEFT JOIN Budget.RevenueType rt ON rd.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON rd.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor total de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el total de las obligaciones coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT rd.Id, rd.InitialValue, rd.TotalObligation TotalValue
				FROM Budget.Obligation r
				JOIN Budget.ObligationDetail rd ON r.Id = rd.ObligationId
				WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
			) b
			FULL JOIN
			(
				SELECT rmd.ObligationDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
				FROM Budget.ObligationModification rm
				JOIN Budget.ObligationModificationDetail rmd ON rm.Id = rmd.ObligationModificationId
				WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
				GROUP BY rmd.ObligationDetailId
			) bm ON b.Id = bm.Id
			WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Obligación ' + r.Code + ': Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Total (' + FORMAT(ISNULL(b.TotalValue, 0), 'C0', 'es-CO') + ') - Total Real(' + FORMAT(ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT rd.Id, rd.InitialValue, rd.TotalObligation TotalValue
						FROM Budget.Obligation r
						JOIN Budget.ObligationDetail rd ON r.Id = rd.ObligationId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
					) b
					FULL JOIN
					(
						SELECT rmd.ObligationDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
						FROM Budget.ObligationModification rm
						JOIN Budget.ObligationModificationDetail rmd ON rm.Id = rmd.ObligationModificationId
						WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
						GROUP BY rmd.ObligationDetailId
					) bm ON b.Id = bm.Id
					LEFT JOIN Budget.ObligationDetail rd ON ISNULL(b.Id, bm.Id) = rd.Id
					LEFT JOIN Budget.Obligation r ON rd.ObligationId = r.Id			
					LEFT JOIN Budget.RevenueType rt ON rd.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON rd.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor total de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el total de las ordenes de pago coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT rd.Id, rd.InitialValue, rd.TotalPaymentOrder TotalValue
				FROM Budget.PaymentOrder r
				JOIN Budget.PaymentOrderDetail rd ON r.Id = rd.PaymentOrderId
				WHERE r.BudgetaryValidityId = @ValidityId AND r.Status IN (2,4)
			) b
			FULL JOIN
			(
				SELECT rmd.PaymentOrderDetailId Id, SUM(rmd.Value) DebitValue, SUM(0) CreditValue
				FROM Budget.ReimbursementResource rm
				JOIN Budget.ReimbursementResourceDetaill rmd ON rm.Id = rmd.ReimbursementResourceId
				WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
				GROUP BY rmd.PaymentOrderDetailId
			) bm ON b.Id = bm.Id
			WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Orden de Pago ' + c.Code + ': Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Total (' + FORMAT(ISNULL(b.TotalValue, 0), 'C0', 'es-CO') + ') - Total Real(' + FORMAT(ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT rd.Id, rd.InitialValue, rd.TotalPaymentOrder TotalValue
						FROM Budget.PaymentOrder r
						JOIN Budget.PaymentOrderDetail rd ON r.Id = rd.PaymentOrderId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status IN (2,4)
					) b
					FULL JOIN
					(
						SELECT rmd.PaymentOrderDetailId Id, SUM(rmd.Value) DebitValue, SUM(0) CreditValue
						FROM Budget.ReimbursementResource rm
						JOIN Budget.ReimbursementResourceDetaill rmd ON rm.Id = rmd.ReimbursementResourceId
						WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
						GROUP BY rmd.PaymentOrderDetailId
					) bm ON b.Id = bm.Id
					LEFT JOIN Budget.PaymentOrderDetail cd ON ISNULL(b.Id, bm.Id) = cd.Id
					LEFT JOIN Budget.PaymentOrder c ON cd.PaymentOrderId = c.Id
					LEFT JOIN Budget.ObligationDetail rd ON cd.ObligationDetailId = rd.Id					
					LEFT JOIN Budget.RevenueType rt ON rd.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON rd.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor total de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		/*********************************  VALIDACION EJECUTADO *********************************/

		--Valido que el valor ejecutado de los rubros presupuestales coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT b.CategoryId, b.RevenueTypeId, b.ExecutedValue
				FROM Budget.Budget b
				JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 2
				WHERE bh.BudgetaryValidityId = @ValidityId
			) b
			FULL JOIN
			(
				SELECT d.CategoryId, d.RevenueTypeId, SUM(d.ExecutedValue) ExecutedValue
				FROM
				(
						SELECT b.CategoryId, b.RevenueTypeId, SUM(rd.TotalAvailability) ExecutedValue
						FROM Budget.Availability r
						JOIN Budget.AvailabilityDetail rd ON r.Id = rd.AvailabilityId
						JOIN Budget.Budget b ON rd.BudgetId = b.Id
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
						GROUP BY b.CategoryId, b.RevenueTypeId
					UNION ALL
						SELECT rd.CategoryId, rd.RevenueTypeId, SUM(rd.TotalCommitment) ExecutedValue
						FROM Budget.Commitment r
						JOIN Budget.CommitmentDetail rd ON r.Id = rd.CommitmentId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2 AND r.CommitmentType <> 1
						GROUP BY rd.CategoryId, rd.RevenueTypeId
					UNION ALL
						SELECT rd.CategoryId, rd.RevenueTypeId, SUM(rd.TotalObligation) ExecutedValue
						FROM Budget.Obligation r
						JOIN Budget.ObligationDetail rd ON r.Id = rd.ObligationId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2 AND r.ObligationType <> 1
						GROUP BY rd.CategoryId, rd.RevenueTypeId
				) d
				GROUP BY d.CategoryId, d.RevenueTypeId
			) rd ON b.CategoryId = rd.CategoryId AND b.RevenueTypeId = rd.RevenueTypeId
			WHERE ISNULL(b.ExecutedValue, 0) <> ISNULL(rd.ExecutedValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Ejecutado (' + FORMAT(ISNULL(b.ExecutedValue, 0), 'C0', 'es-CO') + ') - Ejecutado Real(' + FORMAT(ISNULL(rd.ExecutedValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT b.CategoryId, b.RevenueTypeId, b.ExecutedValue
						FROM Budget.Budget b
						JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 2
						WHERE bh.BudgetaryValidityId = @ValidityId
					) b
					FULL JOIN
					(
						SELECT d.CategoryId, d.RevenueTypeId, SUM(d.ExecutedValue) ExecutedValue
						FROM
						(
								SELECT b.CategoryId, b.RevenueTypeId, SUM(rd.TotalAvailability) ExecutedValue
								FROM Budget.Availability r
								JOIN Budget.AvailabilityDetail rd ON r.Id = rd.AvailabilityId
								JOIN Budget.Budget b ON rd.BudgetId = b.Id
								WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
								GROUP BY b.CategoryId, b.RevenueTypeId
							UNION ALL
								SELECT rd.CategoryId, rd.RevenueTypeId, SUM(rd.TotalCommitment) ExecutedValue
								FROM Budget.Commitment r
								JOIN Budget.CommitmentDetail rd ON r.Id = rd.CommitmentId
								WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2 AND r.CommitmentType <> 1
								GROUP BY rd.CategoryId, rd.RevenueTypeId
							UNION ALL
								SELECT rd.CategoryId, rd.RevenueTypeId, SUM(rd.TotalObligation) ExecutedValue
								FROM Budget.Obligation r
								JOIN Budget.ObligationDetail rd ON r.Id = rd.ObligationId
								WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2 AND r.ObligationType <> 1
								GROUP BY rd.CategoryId, rd.RevenueTypeId
						) d
						GROUP BY d.CategoryId, d.RevenueTypeId
					) rd ON b.CategoryId = rd.CategoryId AND b.RevenueTypeId = rd.RevenueTypeId
					LEFT JOIN Budget.RevenueType rt ON ISNULL(b.RevenueTypeId, rd.RevenueTypeId) = rt.Id
					LEFT JOIN Budget.Category cat ON ISNULL(b.CategoryId, rd.CategoryId) = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.ExecutedValue, 0) <> ISNULL(rd.ExecutedValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor ejecutado de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el valor ejecutado de las disponibilidades coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT ad.Id, ad.ExecutedValue
				FROM Budget.Availability a
				JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
				WHERE a.BudgetaryValidityId = @ValidityId AND a.Status = 2
			) d
			FULL JOIN
			(
				SELECT cd.AvailabilityDetailId Id, SUM(cd.TotalCommitment) ExecutedValue
				FROM Budget.Commitment c
				JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
				WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2
				GROUP BY cd.AvailabilityDetailId
			) od ON d.Id = od.Id
			WHERE ISNULL(d.ExecutedValue, 0) <> ISNULL(od.ExecutedValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + 'Disponibilidad ' + a.Code + ': - Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Ejecutado (' + FORMAT(ISNULL(d.ExecutedValue, 0), 'C0', 'es-CO') + ') - Ejecutado Real(' + FORMAT(ISNULL(od.ExecutedValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT ad.Id, ad.ExecutedValue
						FROM Budget.Availability a
						JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
						WHERE a.BudgetaryValidityId = @ValidityId AND a.Status = 2
					) d
					FULL JOIN
					(
						SELECT cd.AvailabilityDetailId Id, SUM(cd.TotalCommitment) ExecutedValue
						FROM Budget.Commitment c
						JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
						WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2
						GROUP BY cd.AvailabilityDetailId
					) od ON d.Id = od.Id
					LEFT JOIN Budget.AvailabilityDetail ad ON ISNULL(d.Id, od.Id) = ad.Id
					LEFT JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
					LEFT JOIN Budget.Budget b ON ad.BudgetId = b.Id
					LEFT JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON b.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(d.ExecutedValue, 0) <> ISNULL(od.ExecutedValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor ejecutado de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el valor ejecutado de los compromisos coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT cd.Id, cd.ExecutedValue
				FROM Budget.Commitment c
				JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
				WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2
			) d
			FULL JOIN
			(
				SELECT od.CommitmentDetailId Id, SUM(od.TotalObligation) ExecutedValue
				FROM Budget.Obligation o
				JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
				WHERE o.BudgetaryValidityId = @ValidityId AND o.Status = 2
				GROUP BY od.CommitmentDetailId
			) od ON d.Id = od.Id
			WHERE ISNULL(d.ExecutedValue, 0) <> ISNULL(od.ExecutedValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + 'Compromiso ' + c.Code + ': - Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Ejecutado (' + FORMAT(ISNULL(d.ExecutedValue, 0), 'C0', 'es-CO') + ') - Ejecutado Real(' + FORMAT(ISNULL(od.ExecutedValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT cd.Id, cd.ExecutedValue
						FROM Budget.Commitment c
						JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
						WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2
					) d
					FULL JOIN
					(
						SELECT od.CommitmentDetailId Id, SUM(od.TotalObligation) ExecutedValue
						FROM Budget.Obligation o
						JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
						WHERE o.BudgetaryValidityId = @ValidityId AND o.Status = 2
						GROUP BY od.CommitmentDetailId
					) od ON d.Id = od.Id
					LEFT JOIN Budget.CommitmentDetail cd ON ISNULL(d.Id, od.Id) = cd.Id
					LEFT JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
					LEFT JOIN Budget.RevenueType rt ON cd.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON cd.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(d.ExecutedValue, 0) <> ISNULL(od.ExecutedValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor ejecutado de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el valor ejecutado de las obligaciones coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT od.Id, od.ExecutedValue
				FROM Budget.Obligation o
				JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
				WHERE o.BudgetaryValidityId = @ValidityId AND o.Status = 2
			) d
			FULL JOIN
			(
				SELECT pod.ObligationDetailId Id, SUM(pod.TotalPaymentOrder) ExecutedValue
				FROM Budget.PaymentOrder po
				JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
				WHERE po.BudgetaryValidityId = @ValidityId AND po.Status = 2
				GROUP BY pod.ObligationDetailId
			) pod ON d.Id = pod.Id
			WHERE ISNULL(d.ExecutedValue, 0) <> ISNULL(pod.ExecutedValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + 'Obligación ' + o.Code + ': - Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Ejecutado (' + FORMAT(ISNULL(d.ExecutedValue, 0), 'C0', 'es-CO') + ') - Ejecutado Real(' + FORMAT(ISNULL(pod.ExecutedValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT od.Id, od.ExecutedValue
						FROM Budget.Obligation o
						JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
						WHERE o.BudgetaryValidityId = @ValidityId AND o.Status = 2
					) d
					FULL JOIN
					(
						SELECT pod.ObligationDetailId Id, SUM(pod.TotalPaymentOrder) ExecutedValue
						FROM Budget.PaymentOrder po
						JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
						WHERE po.BudgetaryValidityId = @ValidityId AND po.Status = 2
						GROUP BY pod.ObligationDetailId
					) pod ON d.Id = pod.Id
					LEFT JOIN Budget.ObligationDetail od ON ISNULL(d.Id, pod.Id) = od.Id
					LEFT JOIN Budget.Obligation o ON od.ObligationId = o.Id
					LEFT JOIN Budget.RevenueType rt ON od.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON od.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(d.ExecutedValue, 0) <> ISNULL(pod.ExecutedValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor ejecutado de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		/**************************** VALIDACION DOCUMENTOS CON SALDO ****************************/

		--Valido que no existan disponibilidades con saldo
		IF EXISTS 
		( 
			SELECT 1 
			FROM Budget.Availability a 
			JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
			WHERE a.BudgetaryValidityId = @ValidityId AND a.Status = 2 AND ad.Balance > 0
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + 'Disponibilidad ' + a.Code + ': - Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Saldo (' + FORMAT(ISNULL(ad.Balance, 0), 'C0', 'es-CO') + ')'
					FROM Budget.Availability a 
					JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
					JOIN Budget.Budget b ON ad.BudgetId = b.Id
					LEFT JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON b.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE a.BudgetaryValidityId = @ValidityId AND a.Status = 2 AND ad.Balance > 0
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes rubros tienen saldo disponible ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		IF @IsESE = 1
		BEGIN
			--Valido que no existan compromisos con saldo
			IF EXISTS 
			( 
				SELECT 1 
				FROM Budget.Commitment c
				JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
				WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2 AND cd.Balance > 0
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT CHAR(13) + CHAR(10) + 'Compromiso ' + c.Code + ': - Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Saldo (' + FORMAT(ISNULL(cd.Balance, 0), 'C0', 'es-CO') + ')'
						FROM Budget.Commitment c
						JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
						JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
						JOIN Budget.Budget b ON ad.BudgetId = b.Id
						LEFT JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
						LEFT JOIN Budget.Category cat ON b.CategoryId = cat.Id
						LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
						WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2 AND cd.Balance > 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeResult, 'Los siguientes rubros tienen saldo disponible ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
				RETURN
			END
		END

		IF @IsESE = 0
		BEGIN
			/***************************** COMPROMISOS VIGENCIA ANTERIOR *****************************/

			DECLARE @CommitmentRows INT = 1,
					@CommitmentId INT = 0

			WHILE @CommitmentRows > 0
			BEGIN
				SELECT TOP 1
					@CommitmentId = c.Id
				FROM Budget.Commitment c
				JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
				WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2 AND c.CommitmentType <> 2
					AND cd.Balance > 0
					AND c.Id > @CommitmentId
				ORDER BY c.Id

				SET @CommitmentRows = @@ROWCOUNT
				IF @CommitmentRows = 0 
				BEGIN
					BREAK
				END

				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT *
						FROM 
						(
							SELECT	0 Id, 
									'' Code, 
									@NextValidityId BudgetaryValidityId, 
									c.ThirdPartyId, 
									1 DocumentSource, 
									'Reserva creada por el Compromiso ' + c.Code Document, 
									c.DocumentDate DocumentDate, 
									2 CommitmentType, 
									c.Observations, 
									2 Status
							FROM Budget.Commitment c
							WHERE c.Id = @CommitmentId
						) Commitment
						JOIN 
						( 
							SELECT	0 Id, 
									0 CommitmentId, 
									NULL AvailabilityDetailId, 
									cd.Id CategoryId, 
									rtd.Id RevenueTypeId, 
									cmd.ExpiredDate, 
									cmd.Balance InitialValue
							FROM Budget.CommitmentDetail cmd
							JOIN Budget.RevenueType rt ON cmd.RevenueTypeId = rt.Id
							JOIN Budget.Category c ON cmd.CategoryId = c.Id
							JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
							-------------------------------------------------------------
							LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @NextValidityId 
							LEFT JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @NextValidityId
							LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @NextValidityId	
							WHERE cmd.CommitmentId = @CommitmentId AND cmd.Balance > 0
						) CommitmentDetail ON CommitmentDetail.CommitmentId = Commitment.Id
						FOR XML AUTO,TYPE, ELEMENTS
					)
				)

				EXEC [Budget].[SP_SaveCommitment_Output] @SubXml, '', @UserCode, @Code_Output OUT, @Message_Output OUT, NULL, NULL

				IF @Code_Output <> 0
				BEGIN
					SELECT 999 AS CodeResult, ISNULL(@Message_Output, 'Error al generar compromisos de vigencia anterior') AS MessageResult
					RETURN
				END

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			END

			/***************************** OBLIGACIONES VIGENCIA ANTERIOR *****************************/

			DECLARE @ObligationRows INT = 1,
					@ObligationId INT = 0

			WHILE @ObligationRows > 0
			BEGIN
				SELECT TOP 1
					@ObligationId = o.Id
				FROM Budget.Obligation o
				JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
				WHERE o.BudgetaryValidityId = @ValidityId AND o.Status = 2 --AND o.ObligationType <> 2
					AND od.Balance > 0
					AND o.Id > @ObligationId
				ORDER BY o.Id

				SET @ObligationRows = @@ROWCOUNT
				IF @ObligationRows = 0 
				BEGIN
					BREAK
				END

				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT 
							Obligation.*,
							ObligationDetail.*
						FROM
						(
							SELECT 
								0 Id,
								'' Code,
								@NextValidityId BudgetaryValidityId,
								o.DocumentDate,
								o.ThirdPartyId,
								'CxP Generada por la Obligacion ' + o.Code Document,
								2 ObligationType,
								o.Observations,
								2 Status
							FROM Budget.Obligation o
							WHERE o.Id = @ObligationId
						) Obligation
						JOIN
						( 
							SELECT
								0 ObligationId,
								NULL CommitmentDetailId,
								cd.Id CategoryId,
								rtd.Id RevenueTypeId,
								od.ExpiredDate,
								od.Balance InitialValue
							FROM Budget.ObligationDetail od
							JOIN Budget.RevenueType rt ON od.RevenueTypeId = rt.Id
							JOIN Budget.Category c ON od.CategoryId = c.Id
							JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
							-------------------------------------------------------------
							LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @NextValidityId 
							LEFT JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @NextValidityId
							LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @NextValidityId	
							WHERE od.ObligationId = @CommitmentId AND od.Balance > 0
						) ObligationDetail ON Obligation.Id = ObligationDetail.ObligationId
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				EXEC [Budget].[SP_SaveObligation_Output] @SubXml, '', @UserCode, @Code_Output OUT, @Message_Output OUT, NULL, NULL

				IF @Code_Output <> 0
				BEGIN
					SELECT 999 AS CodeResult, ISNULL(@Message_Output, 'Error al generar obligaciones de vigencia anterior') AS MessageResult
					RETURN
				END

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			END
		END

		/*************************************** RESULTADO ***************************************/

		UPDATE bv
			SET bv.ExpenseMonth = bv.ExpenseMonth + 1,
				bv.Status = IIF(bv.IncomeMonth <= 13, bv.Status, 3),
				bv.ClosureUser = IIF(bv.IncomeMonth <= 13, bv.ClosureUser, @UserCode),
				bv.ClosureDate = IIF(bv.IncomeMonth <= 13, bv.ClosureDate, [Common].[GETDATE]())
		FROM Budget.BudgetaryValidity bv
		WHERE bv.Id = @ValidityId

		SELECT	0 AS CodeResult, 
				'La vigencia presupuestal de gastos se cerro correctamente.' + 
					IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + @Message) AS MessageResult
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
	END CATCH
END