-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-28
-- Description:	Procedimiento para el reporte del libro de registro de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportExpenseRecordBook]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE	@BudgetaryValidityId INT,
			@BudgetId INT,
			@CutoffDate DATETIME,
			---------------------------
			@CategoryId INT,
			@RevenueTypeId INT

	DECLARE @TableResult TABLE
	(
		Id INT IDENTITY(1,1),
		DocumentDate DATETIME,
		DocumenNumber VARCHAR(100),
		DocumentClass TINYINT,
		DocumentNature TINYINT,
		DocumentValue DECIMAL(18,0) DEFAULT(0),		
		DocumentTypeName VARCHAR(300),
		DocumentCode VARCHAR(100),
		ThirdPartyId INT,
		DocumentThirdParty VARCHAR(500),
		DocumentDescription VARCHAR(MAX),
		-----------------------------------------------------------------------
		BudgetTotal DECIMAL(18,0) DEFAULT(0),
		AvailabilityTotal DECIMAL(18,0) DEFAULT(0),
		BudgetPending DECIMAL(18,0) DEFAULT(0),
		CommitmentTotal DECIMAL(18,0) DEFAULT(0),
		AvailabilityPending DECIMAL(18,0) DEFAULT(0),
		ObligationTotal DECIMAL(18,0) DEFAULT(0),
		CommitmentPending DECIMAL(18,0) DEFAULT(0),
		PaymentOrderTotal DECIMAL(18,0) DEFAULT(0),
		ObligationPending DECIMAL(18,0) DEFAULT(0)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@BudgetId = t.x.value('BudgetId[1]','int'),
				@CutoffDate = t.x.value('CutoffDate[1]','datetime')
		FROM @xmlCriterias.nodes('/Data') t(x)

		SELECT	@CategoryId = b.CategoryId,
				@RevenueTypeId = b.RevenueTypeId
		FROM Budget.Budget b
		WHERE b.Id = @BudgetId

		/**********************************  OBTENCION DE DATOS **********************************/

		INSERT INTO @TableResult 
		(
			DocumentDate, DocumenNumber, DocumentClass, DocumentNature, DocumentValue, 
			DocumentTypeName, DocumentCode, ThirdPartyId, DocumentDescription
		)
		SELECT *
		FROM
		(
				-- insertamos los datos del presupuesto inicial
				SELECT	DATEFROMPARTS(bv.Year, 1, 1) DocumentDate, bv.ResolutionNumber DocumenNumber, 1 DocumentClass, 2 DocumentNature, b.InitialValue DocumentValue,
						NULL DocumentTypeName, bv.ResolutionNumber DocumentCode, NULL DocumentThirdParty, 'Presupuesto Inicial' DocumentDescription						
				FROM Budget.BudgetaryValidity bv
				JOIN Budget.BudgetHeader bh ON bv.Id = bh.BudgetaryValidityId
				JOIN Budget.Budget b ON bh.Id = b.BudgetHeaderId
				WHERE bh.BudgetaryValidityId = @BudgetaryValidityId AND b.Id = @BudgetId
			UNION ALL
				-- insertamos los datos de los traslados presupuestales
				SELECT	bt.DocumentDate, bt.Code, 2, btd.Nature, btd.Value, 
						NULL, bt.Document, NULL, bt.Observations
				FROM Budget.BudgetTransfer bt
				JOIN Budget.BudgetTransferDetail btd ON bt.Id = btd.TransferId
				WHERE bt.BudgetaryValidityId = @BudgetaryValidityId AND btd.BudgetId = @BudgetId
					AND bt.Status = 2 AND bt.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las modificaciones presupuestales
				SELECT	bm.DocumentDate, bm.Code, 2, bmd.Nature, bmd.Value, 
						NULL, bm.Document, NULL, bm.Observations
				FROM Budget.BudgetModification bm
				JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
				WHERE bm.BudgetaryValidityId = @BudgetaryValidityId AND bmd.BudgetId = @BudgetId
					AND bm.Status = 2 AND bm.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las disponibilidades presupuestales
				SELECT	a.DocumentDate, a.Code, 11, 2, ad.InitialValue, 
						CASE a.AvailabilityType
							WHEN 1 THEN 'Ninguno'
							WHEN 2 THEN 'Disponibilidad'
							WHEN 3 THEN 'Vigencia Futura'
						END, NULL, NULL, a.Observations
				FROM Budget.Availability a
				JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
				WHERE a.BudgetaryValidityId = @BudgetaryValidityId AND ad.BudgetId = @BudgetId
					AND a.Status = 2 AND a.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las modificaciones de las disponibilidades presupuestales
				SELECT	am.DocumentDate, am.Code, 11, amd.Nature, amd.Value, 
						'Modificación Disponibilidad', NULL, NULL, am.Observations
				FROM Budget.AvailabilityModification am
				JOIN Budget.AvailabilityModificationDetail amd ON am.Id = amd.AvailabilityModificationId
				JOIN Budget.AvailabilityDetail ad ON amd.AvailabilityDetailId = ad.Id
				WHERE am.BudgetaryValidityId = @BudgetaryValidityId AND ad.BudgetId = @BudgetId
					AND am.Status = 2 AND am.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de los compromisos presupuestales
				SELECT	c.DocumentDate, c.Code, 13, 2, cd.InitialValue, 
						CASE c.CommitmentType
							WHEN 1 THEN 'Compromiso'
							WHEN 2 THEN 'Reserva'
						END, c.Document, c.ThirdPartyId, c.Observations
				FROM Budget.Commitment c
				JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
				WHERE c.BudgetaryValidityId = @BudgetaryValidityId AND cd.CategoryId = @CategoryId AND cd.RevenueTypeId = @RevenueTypeId
					AND c.Status = 2 AND c.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las modificaciones de los compromisos presupuestales
				SELECT	cm.DocumentDate, cm.Code, 13, cmd.Nature, cmd.Value, 
						'Modificación Compromiso', cm.Document, c.ThirdPartyId, cm.Observations
				FROM Budget.CommitmentModification cm
				JOIN Budget.CommitmentModificationDetail cmd ON cm.Id = cmd.CommitmentModificationId
				JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
				JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
				WHERE cm.BudgetaryValidityId = @BudgetaryValidityId AND cd.CategoryId = @CategoryId AND cd.RevenueTypeId = @RevenueTypeId
					AND cm.Status = 2 AND cm.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las obligaciones presupuestales
				SELECT	o.DocumentDate, o.Code, 15, 2, od.InitialValue, 
						CASE o.ObligationType
							WHEN 1 THEN 'Obligación'
							WHEN 2 THEN 'Cuenta por Pagar'
						END, o.Document, o.ThirdPartyId, o.Observations
				FROM Budget.Obligation o
				JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
				WHERE o.BudgetaryValidityId = @BudgetaryValidityId AND od.CategoryId = @CategoryId AND od.RevenueTypeId = @RevenueTypeId
					AND o.Status = 2 AND o.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las modificaciones de las obligaciones presupuestales
				SELECT	om.DocumentDate, om.Code, 15, omd.Nature, omd.Value, 
						'Modificación Obligación', om.Document, o.ThirdPartyId, om.Observations
				FROM Budget.ObligationModification om
				JOIN Budget.ObligationModificationDetail omd ON om.Id = omd.ObligationModificationId
				JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
				JOIN Budget.Obligation o ON od.ObligationId = o.Id
				WHERE om.BudgetaryValidityId = @BudgetaryValidityId AND od.CategoryId = @CategoryId AND od.RevenueTypeId = @RevenueTypeId
					AND om.Status = 2 AND om.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las ordenes de pago presupuestales
				SELECT	po.DocumentDate, po.Code, 17, 2, pod.InitialValue, 
						CASE po.PaymentOrderType
							WHEN 1 THEN 'Orden Pago'
						END, po.Document, po.ThirdPartyId, po.Observations
				FROM Budget.PaymentOrder po
				JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
				JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
				WHERE po.BudgetaryValidityId = @BudgetaryValidityId AND od.CategoryId = @CategoryId AND od.RevenueTypeId = @RevenueTypeId
					AND po.Status = 2 AND po.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las modificaciones de las ordenes de pago presupuestales
				SELECT	rr.DocumentDate, rr.Code, 17, 1, rrd.Value, 
						'Reintegro', rr.Document, o.ThirdPartyId, rr.Observations
				FROM Budget.ReimbursementResource rr
				JOIN Budget.ReimbursementResourceDetaill rrd ON rr.Id = rrd.ReimbursementResourceId
				JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
				JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
				JOIN Budget.Obligation o ON od.ObligationId = o.Id
				WHERE rr.BudgetaryValidityId = @BudgetaryValidityId AND od.CategoryId = @CategoryId AND od.RevenueTypeId = @RevenueTypeId
					AND rr.Status = 2 AND rr.DocumentDate <= @CutoffDate
		) b
		ORDER BY b.DocumentDate, b.DocumentClass

		/*********************************** CALCULO DE SALDOS ***********************************/

		DECLARE @Rows INT = 1, 
				@RowsId INT = 0,
				-----------------------------------------------------------------------
				@BudgetTotal DECIMAL(18,0) = 0,
				@AvailabilityTotal DECIMAL(18,0) = 0,
				@CommitmentTotal DECIMAL(18,0) = 0,
				@ObligationTotal DECIMAL(18,0) = 0,
				@PaymentOrderTotal DECIMAL(18,0) = 0

		WHILE @Rows > 0
		BEGIN
			SELECT TOP 1
				@RowsId = tr.Id,
				-----------------------------------------------------------------------
				@BudgetTotal = @BudgetTotal + IIF(tr.DocumentClass IN (1,2), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0),
				@AvailabilityTotal = @AvailabilityTotal + IIF(tr.DocumentClass IN (11), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0),
				@CommitmentTotal = @CommitmentTotal + IIF(tr.DocumentClass IN (13), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0),
				@ObligationTotal = @ObligationTotal + IIF(tr.DocumentClass IN (15), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0),
				@PaymentOrderTotal = @PaymentOrderTotal + IIF(tr.DocumentClass IN (17), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0)
			FROM @TableResult tr
			WHERE tr.Id > @RowsId
			ORDER BY tr.Id

			SET @Rows = @@ROWCOUNT
			IF @Rows = 0 
			BEGIN
				BREAK
			END

			UPDATE tr
				SET	tr.BudgetTotal = @BudgetTotal,
					tr.AvailabilityTotal = @AvailabilityTotal,
					tr.CommitmentTotal = @CommitmentTotal,
					tr.ObligationTotal = @ObligationTotal,
					tr.PaymentOrderTotal = @PaymentOrderTotal
			FROM @TableResult tr
			WHERE tr.Id = @RowsId
		END

		-- actualizamos totales
		UPDATE tr
			SET	tr.BudgetPending = tr.BudgetTotal - tr.AvailabilityTotal,
				tr.AvailabilityPending = tr.AvailabilityTotal - tr.CommitmentTotal,
				tr.CommitmentPending = tr.CommitmentTotal - tr.ObligationTotal,
				tr.ObligationPending = tr.ObligationTotal - tr.PaymentOrderTotal
		FROM @TableResult tr

		-- actualizamos terceros
		UPDATE tr
			SET	tr.DocumentThirdParty = CONCAT(tp.Nit, ' - ', tp.Name)
		FROM @TableResult tr
		JOIN Common.ThirdParty tp ON tr.ThirdPartyId = tp.Id

		/*************************************** RESULTADO ***************************************/

		-- Retornamos el resultado
		SELECT * 
		FROM @TableResult
		ORDER BY Id
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END