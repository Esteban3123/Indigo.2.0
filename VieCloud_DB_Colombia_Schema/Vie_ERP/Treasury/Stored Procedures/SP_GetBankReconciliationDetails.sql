-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-01-15
-- Description:	Procedimiento que se encarga de obtener los detalles de la conciliación bancaria
-- ==============================================================================================================
CREATE PROCEDURE [Treasury].[SP_GetBankReconciliationDetails]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DocumentDate DATE,
			@EntityBankAccountId INT,
			@BankReconciliationId INT

	SELECT	@BankReconciliationId = t.x.value('BankReconciliationId[1]','int'),
			@EntityBankAccountId = t.x.value('EntityBankAccountId[1]','int'),
			@DocumentDate = t.x.value('DocumentDate[1]','date')
			
	FROM @xmlCriterias.nodes('/Data') t(x)

	DECLARE @Table_Result AS TABLE
	(
		[Id] [int],
		[DocumentType] [tinyint] NOT NULL,
		[Nature] [tinyint] NOT NULL,
		[Value] [decimal](20, 4) NOT NULL,
		[EntityId] [int] NOT NULL,
		[EntityCode] [varchar](20) NOT NULL,
		[EntityName] [varchar](250) NOT NULL,
		[Reconciled] [bit] NOT NULL,
		----------------------------------
		DocumentDate DATETIME,
		ThirdPartyNitName VARCHAR(MAX),
		DocumentNumber VARCHAR(50),
		Observations VARCHAR(MAX),
		CreationUser VARCHAR(20),
		ConfirmationUser VARCHAR(20)
	)

	BEGIN TRY
		
		INSERT INTO @Table_Result
			SELECT	brd.Id, brd.DocumentType, brd.Nature, brd.Value, brd.EntityId, brd.EntityCode, brd.EntityName, brd.Reconciled,
					CASE brd.DocumentType
						WHEN 1 THEN cr.DocumentDate
						WHEN 2 THEN vt.DocumentDate
						WHEN 3 THEN tn.NoteDate
						WHEN 4 THEN c.DocumentDate
						WHEN 5 THEN ccs.DocumentDate
					END DocumentDate,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
					CASE brd.DocumentType
						WHEN 1 THEN cr.DocumentNumber
						WHEN 2 THEN 
							CASE vt.PaymentMethod
								WHEN 1 THEN CAST(vt.CheckNumber AS VARCHAR(50))
								WHEN 2 THEN vt.NoteNumber
							END
					END DocumentNumber,
					CASE brd.DocumentType
						WHEN 1 THEN cr.Detail
						WHEN 2 THEN vt.Detail
						WHEN 3 THEN tn.Description
						WHEN 4 THEN c.Description
					END Observations,
					CASE brd.DocumentType
						WHEN 1 THEN cr.CreationUser
						WHEN 2 THEN vt.CreationUser
						WHEN 3 THEN tn.CreationUser
						WHEN 4 THEN c.CreationUser
						WHEN 5 THEN ccs.CreationUser
					END CreationUser,
					CASE brd.DocumentType
						WHEN 1 THEN cr.ConfirmationUser
						WHEN 2 THEN vt.ConfirmationUser
						WHEN 3 THEN tn.ConfirmationUser
						WHEN 4 THEN c.ConfirmationUser
						WHEN 5 THEN ccs.ConfirmationUser
					END ConfirmationUser
			FROM Treasury.BankReconciliationDetail brd
			LEFT JOIN 
			(
				SELECT	pm.Id, 
						cr.DocumentDate, 
						cr.IdThirdParty, 
						CASE pm.PaymentMethodTypes
							WHEN 3 THEN pm.CardNumber
							WHEN 4 THEN pm.DepositNumber
						END DocumentNumber,
						CONCAT
						(
							ISNULL
							(
								CASE pm.PaymentMethodTypes
									WHEN 3 THEN 'Tarjeta de Crédito'
									WHEN 4 THEN 'Consignación'
								END + ': ', ''
							), cr.Detail
						) Detail,
						cr.CreationUser, 
						cr.ConfirmationUser
				FROM Treasury.CashReceipts cr 
				JOIN Treasury.PaymentMethods pm ON cr.Id = pm.IdCashReceipt
				WHERE cr.IdBankAccount = @EntityBankAccountId
					AND cr.Status IN (2, 4)
			) cr ON brd.DocumentType = 1 AND brd.EntityId = cr.Id
			LEFT JOIN Treasury.VoucherTransaction vt ON brd.DocumentType = 2 AND brd.EntityId = vt.Id
			LEFT JOIN Treasury.TreasuryNote tn ON brd.DocumentType = 3 AND brd.EntityId = tn.Id
			LEFT JOIN Treasury.Consignment c ON brd.DocumentType = 4 AND brd.EntityId = c.Id
			LEFT JOIN Treasury.ConstitutionCashSmaller ccs ON brd.DocumentType = 5 AND brd.EntityId = ccs.Id
			LEFT JOIN Common.ThirdParty tp ON tp.Id = CASE brd.DocumentType
														WHEN 1 THEN cr.IdThirdParty
														WHEN 2 THEN vt.IdThirdParty
														END
			WHERE brd.BankReconciliationId = @BankReconciliationId AND CAST(tn.NoteDate AS DATE) <= @DocumentDate

		/************************************ RECIBOS DE CAJA ************************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					1 DocumentType, 
					1 Nature,
					pm.Value, 
					pm.Id EntityId, 
					cr.Code EntityCode, 
					'PaymentMethods' EntityName, 
					1 Reconciled,
					cr.DocumentDate,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
					CASE pm.PaymentMethodTypes
						WHEN 3 THEN pm.CardNumber
						WHEN 4 THEN pm.DepositNumber
					END DocumentNumber,
					CONCAT
					(
						ISNULL
						(
							CASE pm.PaymentMethodTypes
								WHEN 3 THEN 'Tarjeta de Crédito'
								WHEN 4 THEN 'Consignación'
							END + ': ', ''
						), cr.Detail
					) Observations,
					cr.CreationUser,
					cr.ConfirmationUser
			FROM Treasury.CashReceipts cr
			JOIN Treasury.PaymentMethods pm ON cr.Id = pm.IdCashReceipt
			JOIN Common.ThirdParty tp ON cr.IdThirdParty = tp.Id
			LEFT JOIN Treasury.BankReconciliationDetail brd ON brd.DocumentType = 1 AND pm.Id = brd.EntityId AND brd.Reconciled = 1
			LEFT JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId = @EntityBankAccountId
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 1 AND pm.Id = tr.EntityId
			WHERE cr.IdBankAccount = @EntityBankAccountId
				AND cr.Status IN (2, 4)
				AND CAST(cr.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

		/********************************  COMPROBANTES DE EGRESO ********************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					2 DocumentType, 
					2 Nature,
					vt.Value, 
					vt.Id EntityId, 
					vt.Code EntityCode, 
					'VoucherTransaction' EntityName, 
					1 Reconciled,
					vt.DocumentDate,
					CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
					CASE vt.PaymentMethod
						WHEN 1 THEN CAST(vt.CheckNumber AS VARCHAR(50))
						WHEN 2 THEN vt.NoteNumber
					END DocumentNumber,
					vt.Detail Observations,
					vt.CreationUser,
					vt.ConfirmationUser
			FROM Treasury.VoucherTransaction vt
			left JOIN Common.ThirdParty tp ON vt.IdThirdParty = tp.Id
			LEFT JOIN (Treasury.BankReconciliationDetail brd
			inner JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId =  @EntityBankAccountId and brd.Reconciled = 1 and brd.DocumentType = 2) ON vt.Id = brd.EntityId 
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 2 AND vt.Id = tr.EntityId
			WHERE vt.IdEntityBankAccount =  @EntityBankAccountId
				AND vt.Status IN (2, 4)
				AND CAST(vt.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

			UNION ALL

			SELECT	0 Id, 
					2 DocumentType, 
					1 Nature,
					vtd.Value, 
					vt.Id EntityId, 
					vt.Code EntityCode, 
					'VoucherTransaction' EntityName, 
					1 Reconciled,
					vt.DocumentDate,
					NULL ThirdPartyNitName,
					CASE vt.PaymentMethod
						WHEN 1 THEN CAST(vt.CheckNumber AS VARCHAR(50))
						WHEN 2 THEN vt.NoteNumber
					END DocumentNumber,
					vt.Detail Observations,
					vt.CreationUser,
					vt.ConfirmationUser
			FROM Treasury.VoucherTransaction vt
			JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
			LEFT JOIN (Treasury.BankReconciliationDetail brd
			inner JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) 
			AND br.EntityBankAccountId = @EntityBankAccountId and brd.Reconciled = 1 and brd.DocumentType = 2) ON vt.Id = brd.EntityId LEFT JOIN @Table_Result tr ON tr.DocumentType = 2 AND vt.Id = tr.EntityId
			WHERE vtd.IdEntityBankAccount = @EntityBankAccountId
				AND vt.Status IN (2, 4)
				AND CAST(vt.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

		/****************************************  NOTAS *****************************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					3 DocumentType, 
					CASE tn.NoteType
						WHEN 1 THEN tn.Nature
						WHEN 3 THEN vt.Nature
					END Nature,
					CASE tn.NoteType
						WHEN 1 THEN tn.Value
						WHEN 3 THEN vt.Value
						WHEN 4 THEN cr.Value
						WHEN 5 THEN c.Value
					END Value, 
					tn.Id EntityId, 
					tn.Code EntityCode, 
					'TreasuryNote' EntityName, 
					1 Reconciled,
					tn.NoteDate,
					NULL ThirdPartyNitName,
					NULL DocumentNumber,
					ISNULL(CASE tn.NoteType						
						WHEN 3 THEN 'Reversión Comprobante de Egreso'
						WHEN 4 THEN 'Reversión Recibo de Caja'
						WHEN 5 THEN 'Reversión Consignación'
					END, '') + tn.Description Observations,
					tn.CreationUser,
					tn.ConfirmationUser
			FROM Treasury.TreasuryNote tn			
			LEFT JOIN Treasury.BankReconciliationDetail brd ON brd.DocumentType = 3 AND tn.Id = brd.EntityId AND brd.Reconciled = 1
			LEFT JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId = @EntityBankAccountId
			LEFT JOIN
			(
				SELECT vt.Id, 1 Nature, vt.Value, vt.IdEntityBankAccount
				FROM Treasury.VoucherTransaction vt
				WHERE vt.IdEntityBankAccount = @EntityBankAccountId

				UNION ALL

				SELECT vtd.IdVoucherTransaction, 2 Nature, vtd.Value, vtd.IdEntityBankAccount
				FROM Treasury.VoucherTransactionDetails vtd
				WHERE vtd.IdEntityBankAccount = @EntityBankAccountId
			) vt ON tn.NoteType = 3 AND tn.VoucherTransactionId = vt.Id
			LEFT JOIN
			(
				SELECT cr.Id, 2 Nature, pm.Value
				FROM Treasury.CashReceipts cr
				JOIN Treasury.PaymentMethods pm ON cr.Id = pm.IdCashReceipt
				WHERE cr.IdBankAccount = @EntityBankAccountId
			) cr ON tn.NoteType = 4 AND tn.CashReceiptId = cr.Id
			LEFT JOIN
			(
				SELECT c.Id, 2 Nature, c.Value
				FROM Treasury.Consignment c
				WHERE c.EntityBankAccountId = @EntityBankAccountId
			) c ON tn.NoteType = 5 AND tn.ConsignmentId = c.Id
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 3 AND tn.Id = tr.EntityId
			WHERE tn.EntityBankAccountId = @EntityBankAccountId OR VT.IdEntityBankAccount = @EntityBankAccountId
					AND tn.Status = 2
					AND CAST(tn.NoteDate AS DATE) <= @DocumentDate
					AND br.Id IS NULL
					AND tr.Id IS NULL

		/************************************  CONSIGNACIONES ************************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					4 DocumentType, 
					1 Nature,
					c.Value, 
					c.Id EntityId, 
					c.Code EntityCode, 
					'Consignment' EntityName, 
					1 Reconciled,
					c.DocumentDate,
					NULL ThirdPartyNitName,
					NULL DocumentNumber,
					c.Description Observations,
					c.CreationUser,
					c.ConfirmationUser
			FROM Treasury.Consignment c
			LEFT JOIN Treasury.BankReconciliationDetail brd ON brd.DocumentType = 4 AND c.Id = brd.EntityId AND brd.Reconciled = 1
			LEFT JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId = @EntityBankAccountId
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 4 AND c.Id = tr.EntityId
			WHERE c.EntityBankAccountId = @EntityBankAccountId
				AND c.Status IN (2, 4)
				AND CAST(c.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

		/***************************************  TRASLADO ***************************************/

		INSERT INTO @Table_Result
			SELECT	0 Id, 
					5 DocumentType, 
					2 Nature,
					ccs.Value, 
					ccs.Id EntityId, 
					ccs.Code EntityCode, 
					'ConstitutionCashSmaller' EntityName, 
					1 Reconciled,
					ccs.DocumentDate,
					NULL ThirdPartyNitName,
					NULL DocumentNumber,
					NULL Observations,
					ccs.CreationUser,
					ccs.ConfirmationUser
			FROM Treasury.ConstitutionCashSmaller ccs
			LEFT JOIN Treasury.BankReconciliationDetail brd ON brd.DocumentType = 5 AND ccs.Id = brd.EntityId AND brd.Reconciled = 1
			LEFT JOIN Treasury.BankReconciliation br ON brd.BankReconciliationId = br.Id AND br.Status IN (1, 2) AND br.EntityBankAccountId = @EntityBankAccountId
			LEFT JOIN @Table_Result tr ON tr.DocumentType = 5 AND ccs.Id = tr.EntityId
			WHERE ccs.EntityBankAccountId = @EntityBankAccountId
				AND ccs.Status = 2
				AND CAST(ccs.DocumentDate AS DATE) <= @DocumentDate
				AND br.Id IS NULL
				AND tr.Id IS NULL

		
	END TRY
	BEGIN CATCH	
		PRINT 'Error: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX))
		PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(MAX))

		DELETE FROM @Table_Result		
	END CATCH

	SELECT  DISTINCT Id, 
			DocumentType, 
			Nature,
			Value, 
			EntityId, 
			EntityCode, 
			EntityName, 
			Reconciled,
			DocumentDate,
			ThirdPartyNitName,
			DocumentNumber,
			Observations,
			CreationUser,
			ConfirmationUser
	FROM @Table_Result tr
	ORDER BY tr.DocumentDate
	 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene el detalle completo de una conciliación bancaria para una cuenta y corte de fecha específicos. Recibe los criterios de búsqueda en formato XML (identificador de conciliación, cuenta bancaria y fecha de corte) y consolida en un único resultado todos los movimientos relacionados: recibos de caja (con su método de pago, número de tarjeta o consignación), comprobantes de egreso (cheques o notas), notas de tesorería, consignaciones y constituciones de caja menor. Para cada movimiento indica si ya fue conciliado, su naturaleza contable (débito o crédito), el valor, el tercero (NIT y nombre), el número de documento y las observaciones. Es usado por el módulo de Tesorería para que el conciliador bancario visualice y gestione las partidas pendientes y conciliadas de una cuenta bancaria en un período determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GetBankReconciliationDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GetBankReconciliationDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los detalles de una conciliación bancaria, combinando los ítems ya registrados en la conciliación con los movimientos pendientes (recibos de caja, comprobantes de egreso, notas, consignaciones y traslados de caja menor) de la cuenta bancaria hasta una fecha de corte.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @xmlCriterias debe incluir BankReconciliationId, EntityBankAccountId y DocumentDate dentro del nodo /Data.; Debe existir la cuenta bancaria de la entidad referenciada por EntityBankAccountId para que los movimientos sean relevantes.; Para incluir documentos pendientes, éstos no deben estar ya asociados a una conciliación bancaria activa (Status 1 o 2) sobre la misma cuenta.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se consideran movimientos de la cuenta bancaria indicada (@EntityBankAccountId) en todos los bloques de inserción.; Sólo se incluyen documentos con fecha (DocumentDate/NoteDate) menor o igual a @DocumentDate (fecha de corte).; Los movimientos pendientes excluyen aquellos ya vinculados a una BankReconciliation con Status IN (1,2) sobre la misma cuenta y los ya presentes en el resultado (deduplicación vía LEFT JOIN @Table_Result).; Los documentos pendientes se marcan con Id=0 y Reconciled=1, distinguiéndose de los ya registrados en BankReconciliationDetail que conservan su Id real.; Los recibos de caja y comprobantes de egreso solo se consideran cuando su Status está en (2,4); las notas y traslados de caja menor cuando Status=2.; El procedimiento nunca propaga excepciones: en caso de error, retorna un conjunto vacío y solo imprime el mensaje.; El resultado final se entrega con DISTINCT y ordenado por fecha del documento.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Inserta todos los detalles existentes (BankReconciliationDetail) de la conciliación indicada, enriqueciendo cada ítem con datos del documento origen (CashReceipts, VoucherTransaction, TreasuryNote, Consignment, ConstitutionCashSmaller) según DocumentType (1..5), filtrando por CAST(tn.NoteDate AS DATE) <= @DocumentDate.; [INSERT] @Table_Result: Inserta recibos de caja pendientes (DocumentType=1, Nature=1, Reconciled=1) con sus métodos de pago, donde cr.IdBankAccount = @EntityBankAccountId, cr.Status IN (2,4), DocumentDate <= @DocumentDate, sin estar ya en otra conciliación activa ni en el resultado.; [INSERT] @Table_Result: Inserta comprobantes de egreso pendientes (DocumentType=2, Nature=2) cuando vt.IdEntityBankAccount = @EntityBankAccountId, vt.Status IN (2,4), DocumentDate <= @DocumentDate y no estén conciliados; adicionalmente inserta líneas (Nature=1) de VoucherTransactionDetails cuya IdEntityBankAccount coincide.; [INSERT] @Table_Result: Inserta notas de tesorería (DocumentType=3) con Nature/Value derivados según NoteType (1=propia, 3=reversión egreso, 4=reversión recibo, 5=reversión consignación), prefijando la descripción con el tipo de reversión, filtrando por NoteDate <= @DocumentDate, Status=2 y sin conciliación previa.; [INSERT] @Table_Result: Inserta consignaciones pendientes (DocumentType=4, Nature=1) con c.EntityBankAccountId = @EntityBankAccountId, c.Status IN (2,4), DocumentDate <= @DocumentDate y sin conciliación activa previa.; [INSERT] @Table_Result: Inserta constituciones de caja menor (DocumentType=5, Nature=2) con ccs.EntityBankAccountId = @EntityBankAccountId, ccs.Status = 2, DocumentDate <= @DocumentDate y sin estar ya conciliadas.; [DELETE] @Table_Result: En caso de error en el TRY, el CATCH limpia toda la tabla resultado (DELETE FROM @Table_Result) e imprime el mensaje y línea de error.; [RETURN_RESULT] @Table_Result: Retorna SELECT DISTINCT del contenido de @Table_Result ordenado por DocumentDate.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si brd.DocumentType = 1 → Toma datos del documento desde CashReceipts + PaymentMethods (incluyendo CardNumber/DepositNumber según PaymentMethodTypes 3=Tarjeta de Crédito, 4=Consignación).; si brd.DocumentType = 2 → Toma datos desde VoucherTransaction; el DocumentNumber se arma con CheckNumber si PaymentMethod=1 o NoteNumber si PaymentMethod=2.; si brd.DocumentType = 3 → Toma datos desde TreasuryNote; la naturaleza/valor depende de NoteType (1=propia, 3=desde VoucherTransaction, 4=desde CashReceipt, 5=desde Consignment).; si brd.DocumentType = 4 → Toma datos desde Consignment.; si brd.DocumentType = 5 → Toma datos desde ConstitutionCashSmaller (traslado a caja menor).; si pm.PaymentMethodTypes = 3 → Documento se identifica con CardNumber y observación se prefija ''Tarjeta de Crédito: ''. else Si =4, se usa DepositNumber y prefijo ''Consignación: ''.; si tn.NoteType IN (3,4,5) → La observación se antepone con ''Reversión Comprobante de Egreso'', ''Reversión Recibo de Caja'' o ''Reversión Consignación'' según corresponda.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetBankReconciliationDetails';
-- GO
