-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-12
-- Description:	Reversa una factura de monto fijo (capitada) confirmada.
--              Flujo: Validaciones -> Anulación -> Nota crédito -> Comprobante contable
-- =============================================
CREATE PROCEDURE [Billing].[SP_ReverseInvoiceEntityCapitated_Output]
    @InvoiceEntityCapitatedXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	-- Variables cabecera
	DECLARE @Id INT,
			@OperatingUnitId INT,
			@Code VARCHAR(20),
			@ReversalReasonId INT,
			@ReversalDescription VARCHAR(MAX),
			@Status TINYINT,
			@FilePath VARCHAR(MAX),
			@InvoiceId INT,
			@AnnulmentDate DATETIME,
			@CompanyType TINYINT,
			@ValuePortfolioAdvance NUMERIC(18,2),
			-------------------------------------------------------------------
			@IsManual BIT,
			@BillingNoteId INT,
			@BillingNoteCode VARCHAR(20),
			@PortfolioAccountReceivableId INT,
			-------------------------------------------------------------------
			@JournalVoucherId INT,
			-------------------------------------------------------------------
			@Message VARCHAR(MAX),
			-------------------------------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	DECLARE @resultJournalVoucher TABLE (code VARCHAR(20), MessageResult VARCHAR(MAX), IdJournalVoucher INT)

	-- Extraer datos desde XML
	SELECT	@Id = t.x.value('Id[1]','int'),
			@ReversalReasonId = t.x.value('ReversalReasonId[1]','int'),
			@ReversalDescription = t.x.value('ReversalDescription[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@FilePath = t.x.value('FilePath[1]','varchar(max)'),
			@CompanyType = t.x.value('CompanyType[1]','tinyint')
	FROM @InvoiceEntityCapitatedXml.nodes('/InvoiceEntityCapitated') t(x)

	-- Obtener datos de la factura a reversar
	SELECT	@OperatingUnitId = iec.OperatingUnitId,
			@Code = iec.Code,
			@PortfolioAccountReceivableId = ar.Id,
			@InvoiceId = iec.InvoiceId,
			@AnnulmentDate = Common.GETDATE()
	FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
	LEFT JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON iec.InvoiceId = ar.InvoiceId
	WHERE iec.Id = @Id

	-- ==================== VALIDACIONES ====================

	IF NOT EXISTS (SELECT 1 FROM Billing.InvoiceEntityCapitated WHERE Id = @Id AND Status = 2)
	BEGIN
		SELECT @Message_Output = 'La factura monto fijo se encuentra en estado: ' + CASE Status 
																				WHEN 1 THEN 'Registrado'
																				WHEN 3 THEN 'Anulado'
																				WHEN 4 THEN 'Reversado'
																				END
		FROM Billing.InvoiceEntityCapitated
		WHERE Id = @Id

		SELECT	@CodeResult = 999, 
				@MessageResult = ISNULL(@Message_Output, 'No existe la factura monto fijo.')
		RETURN
	END

	IF @FilePath IS NULL BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'No se ha enviado la ruta de almacenamiento de la factura electrónica'
		RETURN
	END

	IF @ReversalReasonId IS NULL BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'No se ha enviado la Razón de anulación'
		RETURN
	END

	IF @ReversalDescription IS NULL BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'No se ha enviado la Descripción Razón de anulación'
		RETURN
	END

	IF EXISTS (
		SELECT 1
		FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
		WHERE iec.Id = @Id AND iec.InvoiceId IS NULL
	) BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'No se ha generado una factura a partir de éste documento'
		RETURN
	END

	IF NOT EXISTS (
		SELECT 1
		FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
		JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON iec.InvoiceId = ar.InvoiceId
		WHERE iec.Id = @Id AND ar.PortfolioStatus = 1
	) BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'No se encontró la cuenta por cobrar generada por el documento o esta ya se encuentra radicada'
		RETURN
	END

	IF EXISTS ( 	
		SELECT 1 
		FROM Billing.InvoicePortfolioAdvance ipa
		JOIN Billing.InvoiceEntityCapitated iec ON iec.InvoiceId = ipa.InvoiceId
		JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
		JOIN Treasury.CashReceiptDetails crd ON crd.Id = pa.CashReceiptDetailId
		JOIN Treasury.CashReceiptConcepts crc ON crc.Id = crd.IdCashReceiptConcept
		WHERE iec.Id = @Id AND crc.IsFixedAmountInvoiceAdvance = 1
	) BEGIN

		SELECT @ValuePortfolioAdvance = SUM(ipa.Value) 
		FROM Billing.InvoicePortfolioAdvance ipa
		JOIN Billing.InvoiceEntityCapitated iec ON iec.InvoiceId = ipa.InvoiceId
		JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
		JOIN Treasury.CashReceiptDetails crd ON crd.Id = pa.CashReceiptDetailId
		JOIN Treasury.CashReceiptConcepts crc ON crc.Id = crd.IdCashReceiptConcept
		WHERE iec.Id = @Id AND crc.IsFixedAmountInvoiceAdvance = 1
	END

	IF NOT EXISTS (
		SELECT 1
		FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
		JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON iec.InvoiceId = ar.InvoiceId
		WHERE iec.Id = @Id AND ar.PortfolioStatus = 1 AND (ar.Value - ISNULL(@ValuePortfolioAdvance,0))= ar.Balance
	) BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'La factura no se puede anular porque la cuenta por cobrar tiene movimiento.'
		RETURN
	END

	IF NOT EXISTS (
		SELECT 1
		FROM Billing.BillingSequence s WITH (NOLOCK)
		JOIN Billing.BillingSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseBillingC
		JOIN Common.Sequense cs on sd.IdSequense = cs.Id
		WHERE s.IdForm = '2037' AND s.IsManual = 0
			AND 
			(
				(s.Scope = 'O')
				OR
				(s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
			)
	) BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'No existe una secuencia automatica de Notas Crédito de Facturacion Electronica'
		RETURN
	END

	-- ==================== REVERSIÓN ====================

	UPDATE Billing.InvoiceEntityCapitated
		SET Status = @Status,
			ModificationUser = @CodeUser,
			ModificationDate = [Common].[GETDATE](),
			AnnulmentUser = @CodeUser,
			AnnulmentDate = @AnnulmentDate
	WHERE Id = @Id

	-- Anular factura

	UPDATE i
		SET Status = 2,
			ReversalReasonId = @ReversalReasonId,
			DescriptionReversal = @ReversalDescription,
			AnnulmentUser = @CodeUser,
			AnnulmentDate = @AnnulmentDate
	FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
	JOIN Billing.Invoice i WITH (NOLOCK) ON iec.InvoiceId = i.Id
	WHERE iec.Id = @Id

	-- Anular cuentas por cobrar

	UPDATE ar
		SET Status = 3,
			Balance = 0,
			AnnulmentUser = iec.AnnulmentUser,
			AnnulmentDate = iec.AnnulmentDate
	FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON iec.InvoiceId = ar.InvoiceId
	WHERE iec.Id = @Id

	UPDATE ara
		SET Balance = 0
	FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON iec.InvoiceId = ar.InvoiceId
	JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) ON ar.Id = ara.AccountReceivableId
	WHERE iec.Id = @Id

	UPDATE ars
		SET Balance = 0
	FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON iec.InvoiceId = ar.InvoiceId
	JOIN Portfolio.AccountReceivableShare ars WITH (NOLOCK) ON ar.Id = ars.AccountReceivableId
	WHERE iec.Id = @Id

	IF @InvoiceId IS NOT NULL
	BEGIN
		-- Reversar cruces de anticipo
			EXEC [Billing].[SP_ReversePortfolioByInvoiceId_Output] @OperatingUnitId, @InvoiceId, @AnnulmentDate, @ReversalDescription, @CodeUser, @CompanyType, @Code_Output OUT, @Message_Output OUT
			
			IF @Code_Output <> 0 
			BEGIN
				SELECT	999 AS CodeResult, ISNULL(@Message_Output, 'No se pudo reversar los cruces de anticipo asociados a la factura') AS MessageResult, '' AS NotificationContract
				RETURN
			END

			SELECT @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
	END

	-- Nota crédito electrónica (usa TotalValue para monto a reversar)

	IF EXISTS (SELECT 1 FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK) JOIN Billing.Invoice i WITH (NOLOCK) ON iec.InvoiceId = i.Id WHERE iec.Id = @Id AND i.CUFE IS NOT NULL)
	BEGIN
		EXEC Common.SP_GetSequence 180, 2037, @OperatingUnitId, NULL, NULL, @IsManual OUT, @BillingNoteCode OUT, @Code_Output OUT, @Message_Output OUT
		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = REPLACE(@Message_Output, '{0}', 'nota credito de facturacion electronica')
			RETURN
		END

		INSERT INTO Billing.BillingNote
		(
			Code, NoteDate, CustomerPartyId, Observations, Nature, OperatingUnitId, EntityId, EntityName, CUDE
		)
		SELECT @BillingNoteCode, i.AnnulmentDate, i.ThirdPartyId, CONCAT(brr.Description, ': ', @ReversalDescription), 2, i.OperatingUnitId, i.Id, 'Invoice', 'CUDE'
		FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
		JOIN Billing.Invoice i WITH (NOLOCK) ON iec.InvoiceId = i.Id
		LEFT JOIN Billing.BillingReversalReason brr WITH (NOLOCK) ON @ReversalReasonId = brr.Id
		WHERE iec.Id = @Id

		SET @BillingNoteId = SCOPE_IDENTITY()

		-- Detalle nota: AdjusmentValue y BillingValue = TotalValue (monto a reversar)
		INSERT INTO Billing.BillingNoteDetail
		(
			BillingNoteId, InvoiceId, InvoiceNumber, CUFE, DocumentDate, AdjusmentValue, ConceptId, BillingValue, DiscountValue
		)
		SELECT @BillingNoteId, i.Id, i.InvoiceNumber, i.CUFE, i.InvoiceDate, i.TotalValue, 2, i.TotalValue, 0
		FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
		JOIN Billing.Invoice i WITH (NOLOCK) ON iec.InvoiceId = i.Id
		LEFT JOIN Billing.BillingReversalReason brr WITH (NOLOCK) ON @ReversalReasonId = brr.Id
		WHERE iec.Id = @Id

		INSERT INTO Billing.ElectronicDocument
		(
			DianVersion, OperatingUnitId, CustomerPartyId, EntityId, EntityName, DocumentDate, DocumentType, Status, CreationDate, Container, FilePath, Prefix, DocumentNumber, CUFE, Year
		)
		SELECT TOP 1
			gls.DianVersion, bn.OperatingUnitId, bn.CustomerPartyId, bn.Id, 'BillingNote', bn.NoteDate, 91, 1, Common.[GETDATE](), DB_NAME(), Billing.[GetElectronicDocumentFilePath](@FilePath, ou.UnitCode, bn.NoteDate, 91, bn.Code), NULL, bn.Code, 'CUFE', YEAR(bn.NoteDate)
		FROM Billing.BillingNote bn WITH (NOLOCK)
		JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
		JOIN GeneralLedger.GeneralLedgerSettings gls WITH (NOLOCK) ON bn.OperatingUnitId = gls.IdOperatingUnit
		JOIN Common.OperatingUnit ou WITH (NOLOCK) On bn.OperatingUnitId = ou.Id
		WHERE bn.Id = @BillingNoteId
	END

	-- Modificación reconocimiento presupuestal
		
	EXEC [Portfolio].[SP_GenerateRecognitionModificationByAccountReceivableId_Output] @PortfolioAccountReceivableId, @CodeUser, @Code_Output OUT, @Message_Output OUT

	IF @Code_Output <> 0
	BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la modificación de reconocimiento presupuestal')
		RETURN
	END

	SET @Message_Output = ISNULL(@Message_Output, '')
	SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

	-- Comprobante contable de reversión

	SET @SubXml = CONVERT
	(
		XML, 
		(
			SELECT *
			FROM 
			(
				SELECT	0 Id,
						0 Consecutive,
						sb.InvoiceAnnulmentJournalVoucherTypeId IdJournalVoucher,
						iec.AnnulmentDate VoucherDate, 
						'False' Imported,
						2 Status,
						CONCAT('Comprobante que reversa la factura capitada No. ', @Code) Detail, 
						'InvoiceEntityCapitated' EntityName,
						@Code EntityCode,
						@Id EntityId,
						0 IsClosedYear
				FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
				JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON iec.OperatingUnitId = sb.IdOperatingUnit
				WHERE iec.Id = @Id
			) JournalVoucher
			JOIN
			( 
					SELECT	0 Id,
							0 IdAccounting,
							ma.Id IdMainAccount,
							IIF(ma.HandlesThirdParty = 1, ha.ThirdPartyId, NULL) IdThirdParty, 
							IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) IdCostCenter, 									
							iec.TotalValue DebitValue, 
							0 CreditValue
					FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON iec.CareGroupId = cg.Id
					JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
					JOIN Contract.HealthAdministrator ha WITH (NOLOCK) ON c.HealthAdministratorId = ha.Id
					JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON iec.OperatingUnitId = sb.IdOperatingUnit
					JOIN GeneralLedger.MainAccounts ma ON sb.CapitationRevenueMainAccountId = ma.Id
					WHERE iec.Id = @Id
				UNION ALL
					SELECT	0 Id,
							0 IdAccounting,
							ma.Id IdMainAccount,
							IIF(ma.HandlesThirdParty = 1, ha.ThirdPartyId, NULL) IdThirdParty, 
							IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) IdCostCenter, 									
							0 DebitValue, 
							iec.TotalValue CreditValue
					FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
					JOIN Contract.CareGroup cg WITH (NOLOCK) ON iec.CareGroupId = cg.Id
					JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
					JOIN Contract.HealthAdministrator ha WITH (NOLOCK) ON c.HealthAdministratorId = ha.Id
					JOIN Contract.ContractAccountingStructure cas WITH (NOLOCK) ON cg.ContractAccountingStructureId = cas.Id
					JOIN GeneralLedger.MainAccounts ma ON cas.AccountWithoutRadicateId = ma.Id
					WHERE iec.Id = @Id
			) JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
			For xml AUTO,TYPE, ELEMENTS
		)
	)

	INSERT @resultJournalVoucher EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml, @CodeUser
	SELECT @Code_Output = rjv.code, @Message_Output = rjv.MessageResult, @JournalVoucherId = rjv.IdJournalVoucher
	FROM @resultJournalVoucher rjv

	IF @Code_Output <> 0
	BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + ISNULL(@Message_Output, 'No se pudo generar el comprobante contable')
		RETURN
	END

	SELECT @Message_Output = CONCAT('Se generó el Comprobante contable ', jv.Consecutive, ' de tipo ', jvt.Code, ' - ', jvt.Name)
	FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
	JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) On jv.IdJournalVoucher = jvt.Id
	WHERE jv.Id = @JournalVoucherId

	SELECT @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

	-- Resultado

	SELECT	@CodeResult = 0, 
			@MessageResult = @Message
END