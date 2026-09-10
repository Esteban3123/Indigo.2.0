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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte del libro de registro de gastos (egresos) presupuestales para una vigencia y rubro específico, consolidando en orden cronológico todos los movimientos que afectan el presupuesto de gasto: presupuesto inicial, traslados, modificaciones, disponibilidades presupuestales (CDP), compromisos, obligaciones y órdenes de pago. Recibe criterios de búsqueda en XML (vigencia presupuestal, rubro o partida presupuestal y fecha de corte) y compone los datos cruzando las tablas de encabezado de presupuesto (BudgetHeader), vigencia (BudgetaryValidity), traslados (BudgetTransfer/BudgetTransferDetail) y demás documentos del ciclo presupuestal del gasto. El resultado es una tabla detallada con fecha, número y código del documento, clase, naturaleza (débito/crédito), valor, tipo de documento, tercero y descripción, útil para auditoría, control fiscal y seguimiento de la ejecución presupuestal de egresos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExpenseRecordBook';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExpenseRecordBook';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el libro de registro de movimientos presupuestales (ingresos) de un rubro y vigencia hasta una fecha de corte, listando cronológicamente presupuesto inicial, traslados, modificaciones, disponibilidades, compromisos, obligaciones, órdenes de pago y reintegros con saldos acumulados.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener BudgetaryValidityId, BudgetId y CutoffDate en el nodo /Data; Debe existir un registro en Budget.Budget con Id = @BudgetId del cual se obtienen CategoryId y RevenueTypeId para filtrar compromisos/obligaciones/órdenes de pago; El BudgetId debe pertenecer a la BudgetaryValidityId indicada (vínculo a través de BudgetHeader)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen documentos con Status = 2 (aprobado/legalizado) en todos los orígenes (transferencias, modificaciones, disponibilidades, compromisos, obligaciones, órdenes de pago, reembolsos); Solo se consideran movimientos con DocumentDate <= @CutoffDate (fecha de corte); Todos los movimientos están restringidos a la vigencia presupuestal indicada (@BudgetaryValidityId); Los compromisos, obligaciones, órdenes de pago y reembolsos se filtran por CategoryId y RevenueTypeId derivados del Budget consultado, garantizando trazabilidad al rubro; El presupuesto inicial se registra con DocumentDate = primer día del año de la vigencia (DATEFROMPARTS(bv.Year,1,1)); Los saldos pendientes se calculan como diferencias acumuladas: BudgetPending=Budget-Availability, AvailabilityPending=Availability-Commitment, CommitmentPending=Commitment-Obligation, ObligationPending=Obligation-PaymentOrder; Los totales son acumulados secuencialmente fila por fila ordenadas por DocumentDate, DocumentClass (libro corrido); Nature=2 suma al total; cualquier otro valor resta (multiplicador -1); El reintegro (ReimbursementResource) se registra siempre con Nature=1 (resta sobre PaymentOrder); El tercero se muestra como concatenación ''NIT - Nombre'' tomada de Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'presupuesto inicial; vigencia presupuestal; traslado presupuestal; modificación presupuestal; disponibilidad presupuestal (CDP); vigencia futura; compromiso presupuestal; reserva; obligación presupuestal; cuenta por pagar; orden de pago; reintegro/reembolso; tercero (NIT); categoría y tipo de ingreso; fecha de corte', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT: Retorna el contenido de @TableResult ordenado por Id (cronológico por DocumentDate, DocumentClass) con totales acumulados y saldos pendientes por cada nivel (presupuesto, disponibilidad, compromiso, obligación, orden de pago); [RETURN_RESULT] RESULT: En caso de excepción capturada por CATCH, retorna conjunto único con CodeResult=''999'' y MessageResult con descripción de error y línea', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DocumentClass IN (1,2) (presupuesto inicial o modificaciones/traslados) → Acumula en BudgetTotal con signo según Nature (2=suma, otro=resta); si DocumentClass = 11 (disponibilidades y sus modificaciones) → Acumula en AvailabilityTotal con signo según Nature; si DocumentClass = 13 (compromisos y sus modificaciones) → Acumula en CommitmentTotal con signo según Nature; si DocumentClass = 15 (obligaciones y sus modificaciones) → Acumula en ObligationTotal con signo según Nature; si DocumentClass = 17 (órdenes de pago y reintegros/reembolsos) → Acumula en PaymentOrderTotal con signo según Nature (reintegros tienen Nature=1, restan); si AvailabilityType: 1/2/3 → Etiqueta como ''Ninguno''/''Disponibilidad''/''Vigencia Futura'' respectivamente; si CommitmentType: 1/2 → Etiqueta como ''Compromiso''/''Reserva''; si ObligationType: 1/2 → Etiqueta como ''Obligación''/''Cuenta por Pagar''; si Error en cualquier punto del TRY → Retorna fila única con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.BudgetaryValidity; Budget.BudgetHeader; Budget.BudgetTransfer; Budget.BudgetTransferDetail; Budget.BudgetModification; Budget.BudgetModificationDetail; Budget.Availability; Budget.AvailabilityDetail; Budget.AvailabilityModification; Budget.AvailabilityModificationDetail; Budget.Commitment; Budget.CommitmentDetail; Budget.CommitmentModification; Budget.CommitmentModificationDetail; Budget.Obligation; Budget.ObligationDetail; Budget.ObligationModification; Budget.ObligationModificationDetail; Budget.PaymentOrder; Budget.PaymentOrderDetail; Budget.ReimbursementResource; Budget.ReimbursementResourceDetaill; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseRecordBook';
-- GO
