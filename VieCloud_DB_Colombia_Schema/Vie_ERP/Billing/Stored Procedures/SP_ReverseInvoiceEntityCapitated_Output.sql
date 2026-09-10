-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-12
-- Description:	Procedimiento que se encarga de reversar una factura de monto fijo
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
	
	--Se declaran las variables para obtener la cabecera
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
			@IdElectronicDocument INT,
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

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	--Se obtienen los datos de la cabecera
	SELECT	@Id = t.x.value('Id[1]','int'),
			@ReversalReasonId = t.x.value('ReversalReasonId[1]','int'),
			@ReversalDescription = t.x.value('ReversalDescription[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@FilePath = t.x.value('FilePath[1]','varchar(max)'),
			@CompanyType = t.x.value('CompanyType[1]','tinyint')
	FROM @InvoiceEntityCapitatedXml.nodes('/InvoiceEntityCapitated') t(x)

	/************************************ *************************************** ************************************/

	SELECT	@OperatingUnitId = iec.OperatingUnitId,
			@Code = iec.Code,
			@PortfolioAccountReceivableId = ar.Id,
			@InvoiceId = iec.InvoiceId,
			@AnnulmentDate = Common.GETDATE()
	FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
	LEFT JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON iec.InvoiceId = ar.InvoiceId
	WHERE iec.Id = @Id

	/*************************************************  VALIDACIONES *************************************************/

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

	/*************************************************** REVERSION ***************************************************/

	UPDATE Billing.InvoiceEntityCapitated
		SET Status = @Status,
			ModificationUser = @CodeUser,
			ModificationDate = [Common].[GETDATE](),
			AnnulmentUser = @CodeUser,
			AnnulmentDate = @AnnulmentDate
	WHERE Id = @Id

	--------------------------------------------- ANULACION DE LA FACTURA ---------------------------------------------

	UPDATE i
		SET Status = 2,
			ReversalReasonId = @ReversalReasonId,
			DescriptionReversal = @ReversalDescription,
			AnnulmentUser = @CodeUser,
			AnnulmentDate = @AnnulmentDate
	FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
	JOIN Billing.Invoice i WITH (NOLOCK) ON iec.InvoiceId = i.Id
	WHERE iec.Id = @Id

	------------------------------------------  ANULACION CUENTAS POR COBRAR ------------------------------------------

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

	IF @InvoiceId IS NOT NULL BEGIN

		/***************************** REVERSIÓN DE LAS CUENTAS POR COBRAR Y DE CRUCE DE ANTICIPOS *****************************/
			EXEC [Billing].[SP_ReversePortfolioByInvoiceId_Output] @OperatingUnitId, @InvoiceId, @AnnulmentDate, @ReversalDescription, @CodeUser, @CompanyType, @Code_Output OUT, @Message_Output OUT
			
			IF @Code_Output <> 0 
			BEGIN
				SELECT	999 AS CodeResult, ISNULL(@Message_Output, 'No se pudo reversar los cruces de anticipo asociados a la factura') AS MessageResult, '' AS NotificationContract
				RETURN
			END

			SELECT @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
	END
	---------------------------------  GENERACION DE NOTA ELECTRONICA -----------------------------------

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

		INSERT INTO Billing.BillingNoteDetail
		(
			BillingNoteId, InvoiceId, InvoiceNumber, CUFE, DocumentDate, AdjusmentValue, ConceptId, BillingValue, DiscountValue
		)
		SELECT @BillingNoteId, i.Id, i.InvoiceNumber, i.CUFE, i.InvoiceDate, i.InvoiceValue, 2, i.InvoiceValue, 0
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

	set  @IdElectronicDocument = SCOPE_IDENTITY()

	INSERT INTO Billing.OutboxEvent (
                EventType, AggregateType, AggregateId, PayloadJson, OccurredAtUtc
            )
            VALUES (
                'Billing.BillingNoteConfirmed.v1',
                'BillingRecord',
                CAST(@BillingNoteId AS NVARCHAR(255)),
                CONCAT(N'{"ElectronicDocumentId":', CAST(@IdElectronicDocument AS NVARCHAR(20)), N',"BillingNoteId":', CAST(@BillingNoteId AS NVARCHAR(20)), N'}'),
                [Common].[GETDATE]()
            );

	------------------------------  MODIFICACION RECONOCIMIENTO PRESUPUESTAL ------------------------------
		
	EXEC [Portfolio].[SP_GenerateRecognitionModificationByAccountReceivableId_Output] @PortfolioAccountReceivableId, @CodeUser, @Code_Output OUT, @Message_Output OUT

	IF @Code_Output <> 0
	BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la modificación de reconocimiento presupuestal')
		RETURN
	END

	SET @Message_Output = ISNULL(@Message_Output, '')
	SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

	--------------------------------------------  COMPROBANTE CONTABLE --------------------------------------------

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
					
	--Se consume el sp que guarda el movimiento contable
		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@CodeUser 
		select 
				@Code_Output = rjv.code, 
				@Message_Output = rjv.MessageResult, 
				@JournalVoucherId = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv
					
	IF @Code_Output <> 0
	BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + ISNULL(@Message_Output, 'No se pudo generar el comprobante contable')
		RETURN
	END

	SELECT @Message_Output = CONCAT('Se generó el Comprobante contable de tipo ', jvt.Code, ' - ', jvt.Name)
	FROM Billing.InvoiceEntityCapitated iec
	JOIN Billing.SettingsBilling sb ON iec.OperatingUnitId = sb.IdOperatingUnit
	JOIN GeneralLedger.JournalVoucherTypes jvt On sb.InvoiceAnnulmentJournalVoucherTypeId = jvt.Id
	Where iec.Id = @Id

	SELECT @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

	---------------------------------------  RESULTADO ---------------------------------------

	SELECT	@CodeResult = 0, 
			@MessageResult = @Message
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa o anula una factura de capitación (monto fijo) emitida a una entidad aseguradora (EPS/ARS). Valida que la factura esté en estado ''Registrado'' (estado 2), que tenga una factura electrónica asociada, una razón y descripción de anulación, y que la cuenta por cobrar en cartera no tenga movimientos ni pagos aplicados que impidan la reversión. Si todas las validaciones pasan, actualiza el estado de la factura en [Billing].[InvoiceEntityCapitated], reversa la cuenta por cobrar en [Portfolio].[AccountReceivable] y gestiona los anticipos cruzados en [Billing].[InvoicePortfolioAdvance] y [Portfolio].[PortfolioAdvance], devolviendo un código y mensaje de resultado que indica éxito o el motivo del rechazo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseInvoiceEntityCapitated_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseInvoiceEntityCapitated_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa una factura de monto fijo (capitada): valida estado y cartera, anula el documento y la factura asociada, reversa cuentas por cobrar y cruces de anticipo, genera nota crédito electrónica, modificación de reconocimiento presupuestal y comprobante contable de reversión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura de monto fijo debe existir y estar en Status=2 (estado vigente/aplicado); de lo contrario informa el estado actual (1=Registrado, 3=Anulado, 4=Reversado).; Debe enviarse FilePath, ReversalReasonId y ReversalDescription en el XML de entrada.; La InvoiceEntityCapitated debe tener InvoiceId no nulo (debe existir factura generada).; Debe existir AccountReceivable asociada con PortfolioStatus=1 (no radicada).; La cuenta por cobrar no debe tener movimiento: ar.Value - ISNULL(@ValuePortfolioAdvance,0) debe igualar ar.Balance.; Debe existir una secuencia automática (IsManual=0) para el formulario IdForm=''2037'' con Scope ''O'' o ''OU'' coincidente con la unidad operativa, para Notas Crédito de Facturación Electrónica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.InvoiceEntityCapitated: Marca el documento capitado con el Status enviado en el XML, registra ModificationUser/Date y AnnulmentUser/Date con la fecha actual de Common.GETDATE().; [UPDATE] Billing.Invoice: La factura asociada (Invoice) se pone en Status=2 con ReversalReasonId, DescriptionReversal, AnnulmentUser y AnnulmentDate de la reversión.; [UPDATE] Portfolio.AccountReceivable: La cuenta por cobrar asociada a la factura pasa a Status=3 y Balance=0, replicando AnnulmentUser/AnnulmentDate del documento capitado.; [UPDATE] Portfolio.AccountReceivableAccounting: Pone Balance=0 en los registros contables de la cuenta por cobrar asociada.; [UPDATE] Portfolio.AccountReceivableShare: Pone Balance=0 en todas las cuotas (shares) de la cuenta por cobrar asociada.; [INSERT] Billing.BillingNote: Si la factura tiene CUFE no nulo, se genera una nota de naturaleza 2 (crédito) con Code obtenido de Common.SP_GetSequence (form 180, tipo 2037), Observations = descripción del motivo + '': '' + ReversalDescription, EntityName=''Invoice'' y CUDE=''CUDE''.; [INSERT] Billing.BillingNoteDetail: Por cada nota crédito generada se inserta el detalle con InvoiceId, InvoiceNumber, CUFE, fecha y valor de la factura, ConceptId=2, AdjusmentValue=BillingValue=InvoiceValue y DiscountValue=0.; [INSERT] Billing.ElectronicDocument: Para la nota crédito generada se crea documento electrónico con DocumentType=91, Status=1, Container=DB_NAME(), FilePath calculado por Billing.GetElectronicDocumentFilePath, CUFE=''CUFE'' y Year=YEAR(NoteDate).; [INSERT] GeneralLedger.JournalVouchers: Genera comprobante contable de reversión vía GeneralLedger.SP_CreateAndValidateJournalVoucherMovement: débito a CapitationRevenueMainAccountId y crédito a ContractAccountingStructure.AccountWithoutRadicateId por iec.TotalValue, con tercero del HealthAdministrator y centro de costo del CareGroup según configuración de la cuenta.; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve CodeResult=0 con mensaje consolidado al éxito, o CodeResult=999 con mensaje específico ante cualquier validación o subproceso fallido.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si InvoiceEntityCapitated.Status <> 2 para el Id → Retorna 999 indicando el estado actual (Registrado/Anulado/Reversado) y aborta. else Continúa con validaciones siguientes.; si Existe InvoicePortfolioAdvance con CashReceiptConcepts.IsFixedAmountInvoiceAdvance=1 para la factura → Calcula @ValuePortfolioAdvance = SUM(ipa.Value) y lo descuenta al validar movimiento de la cuenta por cobrar. else @ValuePortfolioAdvance permanece nulo (se trata como 0).; si @InvoiceId IS NOT NULL → Ejecuta Billing.SP_ReversePortfolioByInvoiceId_Output para reversar cuentas por cobrar y cruces de anticipos; si retorna código <>0 aborta con 999. else Omite la reversión de cartera por SP.; si Existe Invoice asociada con CUFE no nulo → Genera secuencia, BillingNote, BillingNoteDetail y ElectronicDocument para la nota crédito electrónica. else No se genera nota crédito electrónica.; si Portfolio.SP_GenerateRecognitionModificationByAccountReceivableId_Output retorna código <>0 → Aborta con CodeResult=999 y mensaje de fallo en modificación de reconocimiento presupuestal. else Continúa con la generación del comprobante contable.; si SP_CreateAndValidateJournalVoucherMovement retorna código <>0 → Aborta con 999 y mensaje ''Ocurrieron errores al intentar Generar el comprobante contable''. else Acumula mensaje con consecutivo y tipo del comprobante generado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseInvoiceEntityCapitated_Output';
-- GO
