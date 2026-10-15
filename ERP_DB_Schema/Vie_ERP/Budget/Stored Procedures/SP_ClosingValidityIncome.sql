-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 2017-01-05
-- Description:	Proceso de Cierre Presupuestal de Ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ClosingValidityIncome]
	@ValidityId INT,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	--Declaracion de variables necesarias
	DECLARE @Year INT,
			@IncomeMonth INT,
			@Status TINYINT,
			@NextValidityId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	BEGIN TRY
		--Se obtiene el año actual a cerrar
		SELECT @Year = [Year], @IncomeMonth = IncomeMonth, @Status = Status
		FROM Budget.BudgetaryValidity 
		WHERE Id = @ValidityId

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
		IF @IncomeMonth < 13 
		BEGIN
			SELECT 999 AS CodeResult, 'Debe cerrar todos los periodos de la vigencia presupuestal' AS MessageResult
			RETURN
		END

		--Valido que no se haya realizado el cierre anual de la vigencia
		IF @IncomeMonth > 13 
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
		IF EXISTS (SELECT 1 FROM Budget.BudgetTransfer WHERE BudgetaryValidityId = @ValidityId AND DocumentSource = 1 AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.BudgetTransfer 
					WHERE BudgetaryValidityId = @ValidityId AND DocumentSource = 1 AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes traslados presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones presupuestales sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.BudgetModification WHERE BudgetaryValidityId = @ValidityId AND DocumentSource = 1 AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.BudgetModification 
					WHERE BudgetaryValidityId = @ValidityId AND DocumentSource = 1 AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan reconocimientos sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.Recognition WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Recognition
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes reconocimientos presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de reconocimientos sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.RecognitionModification WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.RecognitionModification
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de reconocimientos presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan recaudos sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.Collection WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Collection
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes recaudos presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de recaudos sin confirmar
		IF EXISTS (SELECT 1 FROM Budget.CollectionModification WHERE BudgetaryValidityId = @ValidityId AND Status = 1)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.CollectionModification
					WHERE BudgetaryValidityId = @ValidityId AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de recaudos presupuestales se encuentran sin confirmar ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		/******************************** VALIDACION AÑO VIGENCIA ********************************/

		--Valido que no existan traslados con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.BudgetTransfer WHERE DocumentSource = 1 AND Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.BudgetTransfer 
					WHERE DocumentSource = 1 AND Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes traslados presupuestales no corresponden con la vigencia' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones presupuestales con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.BudgetModification WHERE DocumentSource = 1 AND Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.BudgetModification 
					WHERE DocumentSource = 1 AND Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan reconocimientos con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.Recognition WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId AND RecognitonType <> 3)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Recognition
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId AND RecognitonType <> 3
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes reconocimientos presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de reconocimientos con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.RecognitionModification WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.RecognitionModification
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de reconocimientos presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan recaudos con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.Collection WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.Collection
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Los siguientes recaudos presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que no existan modificaciones de recaudos con una vigencia diferente
		IF EXISTS (SELECT 1 FROM Budget.CollectionModification WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + CONCAT(' - ', Code)
					FROM Budget.CollectionModification
					WHERE Status = 2 AND YEAR(DocumentDate) = @Year AND BudgetaryValidityId <> @ValidityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'Las siguientes modificaciones de recaudos presupuestales no corresponden con la vigencia ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
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
				JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
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
					WHERE bt.BudgetaryValidityId = @ValidityId AND bt.Status = 2 AND bt.DocumentSource = 1
					GROUP BY btd.BudgetId
				UNION ALL
					SELECT bmd.BudgetId, SUM(IIF(bmd.Nature = 1, bmd.Value, 0)) DebitValue, SUM(IIF(bmd.Nature = 2, bmd.Value, 0)) CreditValue
					FROM Budget.BudgetModification bm
					JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
					WHERE bm.BudgetaryValidityId = @ValidityId AND bm.Status = 2 AND bm.DocumentSource = 1
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
						JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
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
							WHERE bt.BudgetaryValidityId = @ValidityId AND bt.Status = 2 AND bt.DocumentSource = 1
							GROUP BY btd.BudgetId
						UNION ALL
							SELECT bmd.BudgetId, SUM(IIF(bmd.Nature = 1, bmd.Value, 0)) DebitValue, SUM(IIF(bmd.Nature = 2, bmd.Value, 0)) CreditValue
							FROM Budget.BudgetModification bm
							JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
							WHERE bm.BudgetaryValidityId = @ValidityId AND bm.Status = 2 AND bm.DocumentSource = 1
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

		--Valido que el total de los reconocimientos coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT rd.Id, rd.InitialValue, rd.TotalRecognition TotalValue
				FROM Budget.Recognition r
				JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
				WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
			) b
			FULL JOIN
			(
				SELECT rmd.RecognitionDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
				FROM Budget.RecognitionModification rm
				JOIN Budget.RecognitionModificationDetail rmd ON rm.Id = rmd.RecognitionModificationId
				WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
				GROUP BY rmd.RecognitionDetailId
			) bm ON b.Id = bm.Id
			WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Reconocimiento ' + r.Code + ': Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Total (' + FORMAT(ISNULL(b.TotalValue, 0), 'C0', 'es-CO') + ') - Total Real(' + FORMAT(ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT rd.Id, rd.InitialValue, rd.TotalRecognition TotalValue
						FROM Budget.Recognition r
						JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
					) b
					FULL JOIN
					(
						SELECT rmd.RecognitionDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
						FROM Budget.RecognitionModification rm
						JOIN Budget.RecognitionModificationDetail rmd ON rm.Id = rmd.RecognitionModificationId
						WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
						GROUP BY rmd.RecognitionDetailId
					) bm ON b.Id = bm.Id
					LEFT JOIN Budget.RecognitionDetail rd ON ISNULL(b.Id, bm.Id) = rd.Id
					LEFT JOIN Budget.Recognition r ON rd.RecognitionId = r.Id
					LEFT JOIN Budget.RevenueType rt ON rd.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON rd.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor total de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el total de los recaudos coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT rd.Id, rd.InitialValue, rd.Balance TotalValue
				FROM Budget.Collection r
				JOIN Budget.CollectionDetail rd ON r.Id = rd.CollectionId
				WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
			) b
			FULL JOIN
			(
				SELECT rmd.CollectionDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
				FROM Budget.CollectionModification rm
				JOIN Budget.CollectionModificationDetail rmd ON rm.Id = rmd.CollectionModificationId
				WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
				GROUP BY rmd.CollectionDetailId
			) bm ON b.Id = bm.Id
			WHERE ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0) <> ISNULL(b.TotalValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Recaudo ' + c.Code + ': Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Total (' + FORMAT(ISNULL(b.TotalValue, 0), 'C0', 'es-CO') + ') - Total Real(' + FORMAT(ISNULL(b.InitialValue, 0) + ISNULL(bm.CreditValue, 0) - ISNULL(bm.DebitValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT rd.Id, rd.InitialValue, rd.Balance TotalValue
						FROM Budget.Collection r
						JOIN Budget.CollectionDetail rd ON r.Id = rd.CollectionId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
					) b
					FULL JOIN
					(
						SELECT rmd.CollectionDetailId Id, SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, SUM(IIF(rmd.Nature = 2, rmd.Value, 0)) CreditValue
						FROM Budget.CollectionModification rm
						JOIN Budget.CollectionModificationDetail rmd ON rm.Id = rmd.CollectionModificationId
						WHERE rm.BudgetaryValidityId = @ValidityId AND rm.Status = 2
						GROUP BY rmd.CollectionDetailId
					) bm ON b.Id = bm.Id
					LEFT JOIN Budget.CollectionDetail cd ON ISNULL(b.Id, bm.Id) = cd.Id
					LEFT JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
					LEFT JOIN Budget.Collection c ON cd.CollectionId = c.Id
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
				JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
				WHERE bh.BudgetaryValidityId = @ValidityId
			) b
			FULL JOIN
			(
				SELECT rd.CategoryId, rd.RevenueTypeId, SUM(rd.TotalRecognition) ExecutedValue
				FROM Budget.Recognition r
				JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
				WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
				GROUP BY rd.CategoryId, rd.RevenueTypeId
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
						JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
						WHERE bh.BudgetaryValidityId = @ValidityId
					) b
					FULL JOIN
					(
						SELECT rd.CategoryId, rd.RevenueTypeId, SUM(rd.TotalRecognition) ExecutedValue
						FROM Budget.Recognition r
						JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
						GROUP BY rd.CategoryId, rd.RevenueTypeId
					) rd ON b.CategoryId = rd.CategoryId AND b.RevenueTypeId = rd.RevenueTypeId
					LEFT JOIN Budget.RevenueType rt ON ISNULL(b.RevenueTypeId, rd.RevenueTypeId) = rt.Id
					LEFT JOIN Budget.Category cat ON ISNULL(b.CategoryId, rd.CategoryId) = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.ExecutedValue, 0) <> ISNULL(rd.ExecutedValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor ejecutado de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		--Valido que el valor ejecutado de los reconocimientos coincidan con los detalles
		IF EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT rd.Id, rd.ExecutedValue
				FROM Budget.Recognition r
				JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
				WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
			) b
			FULL JOIN
			(
				SELECT cd.RecognitionDetailId Id, SUM(cd.Balance) ExecutedValue
				FROM Budget.Collection c
				JOIN Budget.CollectionDetail cd ON c.Id = cd.CollectionId
				WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2
				GROUP BY cd.RecognitionDetailId
			) cd ON b.Id = cd.Id
			WHERE ISNULL(b.ExecutedValue, 0) <> ISNULL(cd.ExecutedValue, 0)
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT CHAR(13) + CHAR(10) + 'Reconocimiento ' + r.Code + ': - Rubro ' + cat.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Ejecutado (' + FORMAT(ISNULL(b.ExecutedValue, 0), 'C0', 'es-CO') + ') - Ejecutado Real(' + FORMAT(ISNULL(cd.ExecutedValue, 0), 'C0', 'es-CO') + ')'
					FROM 
					(
						SELECT rd.Id, rd.ExecutedValue
						FROM Budget.Recognition r
						JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
						WHERE r.BudgetaryValidityId = @ValidityId AND r.Status = 2
					) b
					FULL JOIN
					(
						SELECT cd.RecognitionDetailId Id, SUM(cd.Balance) ExecutedValue
						FROM Budget.Collection c
						JOIN Budget.CollectionDetail cd ON c.Id = cd.CollectionId
						WHERE c.BudgetaryValidityId = @ValidityId AND c.Status = 2
						GROUP BY cd.RecognitionDetailId
					) cd ON b.Id = cd.Id
					LEFT JOIN Budget.RecognitionDetail rd ON ISNULL(b.Id, cd.Id) = rd.Id
					LEFT JOIN Budget.Recognition r ON rd.RecognitionId = r.Id
					LEFT JOIN Budget.RevenueType rt ON rd.RevenueTypeId = rt.Id
					LEFT JOIN Budget.Category cat ON rd.CategoryId = cat.Id
					LEFT JOIN Budget.FinancialSource fs ON cat.FinancialSourceId = fs.Id
					WHERE ISNULL(b.ExecutedValue, 0) <> ISNULL(cd.ExecutedValue, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeResult, 'El valor ejecutado de los siguientes rubros no coincide con sus detalles ' + CHAR(13) + CHAR(10) + @Message AS MessageResult
			RETURN
		END

		/*************************************** RESULTADO ***************************************/

		UPDATE bv
			SET bv.IncomeMonth = bv.IncomeMonth + 1,
				bv.Status = IIF(bv.ExpenseMonth <= 13, bv.Status, 3),
				bv.ClosureUser = IIF(bv.ExpenseMonth <= 13, bv.ClosureUser, @UserCode),
				bv.ClosureDate = IIF(bv.ExpenseMonth <= 13, bv.ClosureDate, [Common].[GETDATE]())
		FROM Budget.BudgetaryValidity bv
		WHERE bv.Id = @ValidityId

		SELECT	0 AS CodeResult, 
				'La vigencia presupuestal de ingresos se cerro correctamente.' + 
					IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + @Message) AS MessageResult
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
	END CATCH
END