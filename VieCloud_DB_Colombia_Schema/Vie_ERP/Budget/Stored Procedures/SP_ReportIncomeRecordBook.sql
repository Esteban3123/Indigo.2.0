-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-28
-- Description:	Procedimiento para el reporte del libro de registro de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportIncomeRecordBook]
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
		DocumentDescription VARCHAR(MAX),
		-----------------------------------------------------------------------
		RightCaused DECIMAL(18,0) DEFAULT(0),
		RightCanceled DECIMAL(18,0) DEFAULT(0),
		RightTotal DECIMAL(18,0) DEFAULT(0),
		CashCollection DECIMAL(18,0) DEFAULT(0),
		CashCollectionReturned DECIMAL(18,0) DEFAULT(0),
		CashCollectionTotal DECIMAL(18,0) DEFAULT(0),
		PaperCollection DECIMAL(18,0) DEFAULT(0),
		CollectionTotal DECIMAL(18,0) DEFAULT(0),
		OtherCollection DECIMAL(18,0) DEFAULT(0),
		PendingCollection DECIMAL(18,0) DEFAULT(0)
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
			DocumentDate, DocumenNumber, DocumentClass, DocumentNature, DocumentValue, DocumentDescription
		)
		SELECT *
		FROM
		(
				-- insertamos los datos del presupuesto inicial
				SELECT DATEFROMPARTS(bv.Year, 1, 1) DocumentDate, bv.ResolutionNumber DocumenNumber, 1 DocumentClass, 2 DocumentNature, b.InitialValue DocumentValue, 'Presupuesto Inicial' DocumentDescription
				FROM Budget.BudgetaryValidity bv
				JOIN Budget.BudgetHeader bh ON bv.Id = bh.BudgetaryValidityId
				JOIN Budget.Budget b ON bh.Id = b.BudgetHeaderId
				WHERE bh.BudgetaryValidityId = @BudgetaryValidityId AND b.Id = @BudgetId
			UNION ALL
				-- insertamos los datos de los traslados presupuestales
				SELECT bt.DocumentDate DocumentDate, bt.Code DocumenNumber, 2 DocumentClass, btd.Nature DocumentNature, btd.Value DocumentValue, bt.Observations DocumentDescription
				FROM Budget.BudgetTransfer bt
				JOIN Budget.BudgetTransferDetail btd ON bt.Id = btd.TransferId
				WHERE bt.BudgetaryValidityId = @BudgetaryValidityId AND btd.BudgetId = @BudgetId
					AND bt.Status = 2 AND bt.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las modificaciones presupuestales
				SELECT bm.DocumentDate DocumentDate, bm.Code DocumenNumber, 2 DocumentClass, bmd.Nature DocumentNature, bmd.Value DocumentValue, bm.Observations DocumentDescription
				FROM Budget.BudgetModification bm
				JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
				WHERE bm.BudgetaryValidityId = @BudgetaryValidityId AND bmd.BudgetId = @BudgetId
					AND bm.Status = 2 AND bm.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de los reconocimientos presupuestales
				SELECT r.DocumentDate DocumentDate, r.Code DocumenNumber, 7 DocumentClass, 2 DocumentNature, rd.InitialValue DocumentValue, r.Observations DocumentDescription
				FROM Budget.Recognition r
				JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
				WHERE r.BudgetaryValidityId = @BudgetaryValidityId AND rd.CategoryId = @CategoryId AND rd.RevenueTypeId = @RevenueTypeId
					AND r.Status = 2 AND r.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las modificaciones de los reconocimientos presupuestales
				SELECT rm.DocumentDate DocumentDate, rm.Code DocumenNumber, 8 DocumentClass, rmd.Nature DocumentNature, rmd.Value DocumentValue, rm.Observations DocumentDescription
				FROM Budget.RecognitionModification rm
				JOIN Budget.RecognitionModificationDetail rmd ON rm.Id = rmd.RecognitionModificationId
				JOIN Budget.RecognitionDetail rd ON rmd.RecognitionDetailId = rd.Id
				WHERE rm.BudgetaryValidityId = @BudgetaryValidityId AND rd.CategoryId = @CategoryId AND rd.RevenueTypeId = @RevenueTypeId
					AND rm.Status = 2 AND rm.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de los recaudos presupuestales
				SELECT c.DocumentDate DocumentDate, c.Code DocumenNumber, 10 DocumentClass, 2 DocumentNature, cd.InitialValue DocumentValue, c.Observations DocumentDescription
				FROM Budget.Collection c
				JOIN Budget.CollectionDetail cd ON c.Id = cd.CollectionId
				JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
				WHERE c.BudgetaryValidityId = @BudgetaryValidityId AND rd.CategoryId = @CategoryId AND rd.RevenueTypeId = @RevenueTypeId
					AND c.Status = 2 AND c.DocumentDate <= @CutoffDate
			UNION ALL
				-- insertamos los datos de las modificaciones de los recaudos presupuestales
				SELECT cm.DocumentDate DocumentDate, cm.Code DocumenNumber, 11 DocumentClass, cmd.Nature DocumentNature, cmd.Value DocumentValue, cm.Observations DocumentDescription
				FROM Budget.CollectionModification cm
				JOIN Budget.CollectionModificationDetail cmd ON cm.Id = cmd.CollectionModificationId
				JOIN Budget.CollectionDetail cd ON cmd.CollectionDetailId = cd.Id
				JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
				WHERE cm.BudgetaryValidityId = @BudgetaryValidityId AND rd.CategoryId = @CategoryId AND rd.RevenueTypeId = @RevenueTypeId
					AND cm.Status = 2 AND cm.DocumentDate <= @CutoffDate
		) b
		ORDER BY b.DocumentDate, b.DocumentClass

		/*********************************** CALCULO DE SALDOS ***********************************/

		DECLARE @Rows INT = 1, 
				@RowsId INT = 0,
				-----------------------------------------------------------------------
				@RightCaused DECIMAL(18,0) = 0,
				@RightCanceled DECIMAL(18,0) = 0,
				@CashCollection DECIMAL(18,0) = 0,
				@CashCollectionReturned DECIMAL(18,0) = 0

		WHILE @Rows > 0
		BEGIN
			SELECT TOP 1
				@RowsId = tr.Id,
				-----------------------------------------------------------------------
				@RightCaused = @RightCaused + IIF(tr.DocumentClass IN (7), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0),
				@RightCanceled = @RightCanceled + IIF(tr.DocumentClass IN (8), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0),
				@CashCollection = @CashCollection + IIF(tr.DocumentClass IN (10), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0),
				@CashCollectionReturned = @CashCollectionReturned + IIF(tr.DocumentClass IN (11), tr.DocumentValue * IIF(tr.DocumentNature = 2, 1, -1), 0)
			FROM @TableResult tr
			WHERE tr.Id > @RowsId
			ORDER BY tr.Id

			SET @Rows = @@ROWCOUNT
			IF @Rows = 0 
			BEGIN
				BREAK
			END

			UPDATE tr
				SET	tr.RightCaused = @RightCaused,
					tr.RightCanceled = @RightCanceled,
					tr.CashCollection = @CashCollection,
					tr.CashCollectionReturned = @CashCollectionReturned
			FROM @TableResult tr
			WHERE tr.Id = @RowsId
		END

		-- actualizamos totales
		UPDATE tr
			SET	tr.RightCanceled = tr.RightCanceled * -1,
				tr.RightTotal = tr.RightCaused + tr.RightCanceled,
				tr.CashCollectionReturned = tr.CashCollectionReturned * -1,
				tr.CashCollectionTotal = tr.CashCollection + tr.CashCollectionReturned,
				tr.CollectionTotal = tr.CashCollection + tr.CashCollectionReturned + tr.PaperCollection,
				tr.PendingCollection = (tr.RightCaused + tr.RightCanceled) - (tr.CashCollection + tr.CashCollectionReturned + tr.PaperCollection)
		FROM @TableResult tr

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte del libro de registro de ingresos presupuestales para una vigencia y rubro presupuestal específicos, con corte a una fecha determinada. Consolida en orden cronológico todos los movimientos que afectan el ingreso: el presupuesto inicial (valor aprobado por resolución), traslados y modificaciones presupuestales, reconocimientos de derechos causados y sus modificaciones, y recaudos (cobros efectivos) con sus respectivas modificaciones. Para cada documento calcula columnas de derechos causados, recaudos en efectivo, recaudos en especie (documentos), otros recaudos y saldo pendiente de recaudo, produciendo así un libro contable-presupuestal que permite hacer seguimiento al comportamiento de los ingresos institucionales frente a lo presupuestado en la vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportIncomeRecordBook';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportIncomeRecordBook';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte cronológico del libro de registro de ingresos consolidando presupuesto inicial, traslados, modificaciones, reconocimientos, modificaciones de reconocimiento y recaudos (con sus modificaciones) hasta una fecha de corte, calculando saldos progresivos de derechos y recaudos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con BudgetaryValidityId, BudgetId y CutoffDate válidos.; Debe existir un registro en Budget.Budget cuyo Id coincida con @BudgetId para poder obtener CategoryId y RevenueTypeId.; Debe existir el encabezado y la vigencia presupuestal asociados al BudgetId/BudgetaryValidityId para obtener el presupuesto inicial.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen documentos en estado Status=2 (aprobado/legalizado) para traslados, modificaciones, reconocimientos, modificaciones de reconocimiento, recaudos y modificaciones de recaudo.; Solo se consideran movimientos cuya DocumentDate es menor o igual a la fecha de corte (@CutoffDate); el presupuesto inicial siempre se incluye con fecha 1 de enero del año de la vigencia.; Los reconocimientos, sus modificaciones y los recaudos se filtran por la CategoryId y RevenueTypeId derivados del Budget seleccionado, garantizando coherencia con el rubro.; DocumentClass codifica el tipo de movimiento: 1=Presupuesto Inicial, 2=Traslado/Modificación presupuestal, 7=Reconocimiento, 8=Modificación de reconocimiento, 10=Recaudo, 11=Modificación de recaudo.; Los acumulados se computan en orden cronológico (DocumentDate, DocumentClass) por fila Id ascendente, produciendo saldos progresivos.; RightCanceled y CashCollectionReturned se almacenan con signo invertido (multiplicados por -1) tras el acumulado.; RightTotal = RightCaused + RightCanceled; CashCollectionTotal = CashCollection + CashCollectionReturned; CollectionTotal = CashCollectionTotal + PaperCollection; PendingCollection = (RightCaused + RightCanceled) - CollectionTotal.; PaperCollection y OtherCollection nunca se alimentan dentro del procedimiento (quedan en su valor por defecto 0).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'presupuesto inicial; vigencia presupuestal; traslado presupuestal; modificación presupuestal; reconocimiento presupuestal; modificación de reconocimiento; recaudo presupuestal; modificación de recaudo; derechos causados; derechos anulados; recaudo en efectivo; recaudo en papeles; saldo por recaudar; naturaleza débito/crédito; libro de registro de ingresos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Se insertan filas unificando 7 orígenes: presupuesto inicial (Class=1, Nature=2), traslados aprobados (Class=2), modificaciones presupuestales aprobadas (Class=2), reconocimientos aprobados (Class=7, Nature=2), modificaciones de reconocimiento aprobadas (Class=8), recaudos aprobados (Class=10, Nature=2) y modificaciones de recaudo aprobadas (Class=11), todos con Status=2 y DocumentDate<=@CutoffDate.; [UPDATE] @TableResult: Para cada fila ordenada por Id se actualizan los acumulados RightCaused, RightCanceled, CashCollection y CashCollectionReturned, sumando el valor con signo según DocumentClass y DocumentNature (Nature=2 suma, otros restan).; [UPDATE] @TableResult: Tras el acumulado se invierten signos de RightCanceled y CashCollectionReturned (×-1) y se calculan RightTotal, CashCollectionTotal, CollectionTotal y PendingCollection según las fórmulas de saldos.; [RETURN_RESULT] (resultset): Retorna SELECT * FROM @TableResult ordenado por Id; si ocurre excepción, retorna una fila con CodeResult=''999'' y MessageResult conteniendo ERROR_MESSAGE() y ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DocumentClass = 7 (reconocimiento) → Acumula en RightCaused: +Value si Nature=2 (crédito), -Value en caso contrario; si DocumentClass = 8 (modificación de reconocimiento) → Acumula en RightCanceled: +Value si Nature=2, -Value en caso contrario; si DocumentClass = 10 (recaudo) → Acumula en CashCollection: +Value si Nature=2, -Value en caso contrario; si DocumentClass = 11 (modificación de recaudo) → Acumula en CashCollectionReturned: +Value si Nature=2, -Value en caso contrario; si Error en cualquier paso del TRY → Devuelve fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y línea else Retorna SELECT * FROM @TableResult ordenado por Id', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.BudgetaryValidity; Budget.BudgetHeader; Budget.BudgetTransfer; Budget.BudgetTransferDetail; Budget.BudgetModification; Budget.BudgetModificationDetail; Budget.Recognition; Budget.RecognitionDetail; Budget.RecognitionModification; Budget.RecognitionModificationDetail; Budget.Collection; Budget.CollectionDetail; Budget.CollectionModification; Budget.CollectionModificationDetail', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeRecordBook';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeRecordBook';
-- GO
