-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-03-19
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un documento de cuenta por cobrar
-- =====================================================
CREATE PROCEDURE [Portfolio].[SP_SaveAccountReceivableDocument_Output]
    @AccountReceivableDocumentXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT, 
	@MessageResult VARCHAR(MAX)  OUTPUT, 
	--------------------------------------------
	@Id INT OUTPUT, 
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @OperatingUnitId INT, 					
			@DocumentDate DATETIME,
			@CustomerId INT,
			@ThirdPartyId INT,
			@MainAccountId INT,
			@CostCenterId INT,
			@InvoiceNumber VARCHAR(20),
			@Term INT,
			@ExpiredDate DATETIME,
			@Share INT,
			@Value DECIMAL(18,2),
			@DebitValue DECIMAL(18,2),
			@CreditValue DECIMAL(18,2),
			@Observation VARCHAR(300),
			@AccountReceivableId INT,
			@AccountReceivableCode VARCHAR(20),
			@Status TINYINT,
			@CurrencyId INT,
			------------------------------			
			@IdForm INT = 1522,
			@DocumentTypeControl INT = 3,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@JournalVoucherId INT,
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)
			
	--Tabla temporal de los detalles
	DECLARE @Detail TABLE
	(
		Id INT,
		AccountReceivableDocumentId INT,
		AccountReceivableConceptId INT NOT NULL,
		MainAccountId INT NOT NULL,
		ThirdPartyId INT,
		CostCenterId INT,
		Nature TINYINT NOT NULL,
		Value DECIMAL(18,2) NOT NULL,
		Observation VARCHAR(500),
		ChangeTracker VARCHAR(30)	
	)

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT  @Id = t.x.value('Id[1]','INT'),
				@Code = t.x.value('Code[1]','VARCHAR(20)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
				@DocumentDate = t.x.value('DocumentDate[1]','DATETIME'),
				@CustomerId = t.x.value('CustomerId[1]','INT'),
				@MainAccountId = t.x.value('MainAccountId[1]','INT'),
				@CostCenterId  = t.x.value('CostCenterId[1]','INT'),
				@InvoiceNumber = t.x.value('InvoiceNumber[1]','VARCHAR(20)'),
				@Term =  t.x.value('Term[1]','INT'),
				@ExpiredDate = t.x.value('ExpiredDate[1]','DATETIME'),
				@Share = t.x.value('Share[1]','INT'),
				@Value =  t.x.value('Value[1]','DECIMAL(18,2)'),
				@DebitValue =  t.x.value('DebitValue[1]','DECIMAL(18,2)'),
				@CreditValue = t.x.value('CreditValue[1]','DECIMAL(18,2)'),
				@Observation = t.x.value('Observation[1]','VARCHAR(300)'),
				@AccountReceivableId = t.x.value('AccountReceivableId[1]','INT'),
				@CurrencyId = t.x.value('CurrencyId[1]','INT'),
				@Status  = t.x.value('Status[1]','TINYINT')							
				-------------------------------------------------------				
		FROM @AccountReceivableDocumentXml.nodes('/AccountReceivableDocument') t(x)
				
		/*************************************************  VALIDACIONES *************************************************/
		
		IF EXISTS (SELECT 1 FROM Portfolio.AccountReceivableDocument WHERE Id = @Id AND Status <> 1)
		BEGIN
			SELECT	@CodeResult = 999, 
					@CodeResult = 'El documento cuenta por cobrar se encuentra en estado: ' + CASE Status 
																								WHEN 2 THEN 'Registrado'
																								WHEN 3 THEN 'Confirmado'
																								WHEN 4 THEN 'Anulado'
																							  END
			FROM Portfolio.AccountReceivableDocument
			WHERE Id = @Id
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE Portfolio.AccountReceivableDocument
			SET Status = @Status,
					ModificationUser = @CodeUser,
					ModificationDate = [Common].[GETDATE](),
					AnnulmentUser = @CodeUser,
					AnnulmentDate = [Common].[GETDATE]()
			WHERE Id = @Id
		END	
		ELSE
		BEGIN			
			/***************************************** VALIDACIONES CABECERA *****************************************/				

			-- Valido que se encuentre la unidad operativa
			IF NOT EXISTS (SELECT 1 FROM Portfolio.SettingPortfolio WITH (NOLOCK) WHERE OperatingUnitId = @OperatingUnitId)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = CONCAT('No se encontraron parametros de cartera para la unidad operativa',ISNULL(@OperatingUnitId,0))
				RETURN
			END
			
			-- Valido que se haya ingresado un cliente valido
			IF NOT EXISTS (SELECT 1 FROM Common.Customer WITH (NOLOCK) WHERE Id = @CustomerId)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El cliente no existe'
				RETURN
			END

			-- Valido que el cliente este activo
			IF EXISTS (SELECT 1 FROM Common.Customer WITH (NOLOCK) WHERE Id = @CustomerId AND State = 0)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El cliente no se encuentra activo'
				RETURN
			END

			-- Valido que el número de cuotas sean mayores a 0
			IF @Share <= 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El número de cuotas debe ser mayor a 0'
				RETURN
			END

			-- Valido que el número de plazos (días)
			IF @Term <= 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El número de Plazo (Días) debe ser mayor a 0'
				RETURN
			END
			
			-- Valido que el valor deber ser mayor a 0
			IF @Value <= 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El valor debe ser mayor a 0'
				RETURN
			END

			--Valido de que no exista el mismo número de factura registrada
			IF EXISTS (SELECT 1 FROM Portfolio.AccountReceivable ar WITH (NOLOCK) WHERE ar.InvoiceNumber = @InvoiceNumber)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Existe el mismo número de factura'
				RETURN
			END
			

			/************************************ VALIDACIONES OTROS PROCESOS ************************************/		
			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail
				SELECT	t.x.value('Id[1]','INT'),
						t.x.value('AccountReceivableDocumentId[1]','INT'),
						t.x.value('AccountReceivableConceptId[1]','INT'),
						t.x.value('MainAccountId[1]','INT'),
						t.x.value('ThirdPartyId[1]','INT'),
						t.x.value('CostCenterId[1]','INT'),
						t.x.value('Nature[1]','TINYINT'),						
						t.x.value('Value[1]','NUMERIC(18,2)'),		
						t.x.value('Observation[1]','VARCHAR(500)'),
						t.x.value('ChangeTracker[1]','VARCHAR(30)')
			FROM @AccountReceivableDocumentXml.nodes('/AccountReceivableDocument/AccountReceivableDocumentDetail') t(x)
						
			--se eliminan los detalles marcados para su eliminación
			DELETE Ad
			FROM Portfolio.AccountReceivableDocumentDetail Ad
			JOIN @Detail d ON Ad.Id = d.Id
			WHERE @Id = Ad.AccountReceivableDocumentId AND d.ChangeTracker = 'Deleted'

			DELETE d FROM @Detail d WHERE d.ChangeTracker = 'Deleted'

			/***************************************  VALIDACIONES DETALLE ***************************************/

			-- Valido que existan conceptos		
			IF NOT EXISTS (SELECT 1 FROM @Detail)
			BEGIN 					
				SELECT @CodeResult = 999, 
						@MessageResult = 'Debe crear mínimo un detalle para el documento cuenta por cobrar'
				RETURN
			END

			--valido que existen detalles asociados a un concepto inexistente
			IF EXISTS 
			(
				SELECT 1
				FROM @Detail d 
				LEFT JOIN Portfolio.AccountReceivableConcept arc WITH (NOLOCK) ON d.AccountReceivableConceptId = arc.Id
				WHERE arc.Id IS NULL
			)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Existen detalles asociados a un concepto inexistente'
				RETURN
			END

			--valido si existen conceptos de cuentas por cobrar activos
			IF EXISTS 
			(
				SELECT 1
				FROM @Detail d 
				JOIN Portfolio.AccountReceivableConcept arc WITH (NOLOCK) ON d.AccountReceivableConceptId = arc.Id
				WHERE arc.Status = 0
			)
			BEGIN
				SELECT @Message = STUFF((
					SELECT DISTINCT CONCAT(CHAR(13), CHAR(10), '- ', arc.Code, ' - ', arc.Name)
					FROM @Detail d 
					JOIN Portfolio.AccountReceivableConcept arc WITH (NOLOCK) ON d.AccountReceivableConceptId = arc.Id
					WHERE arc.Status = 0
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = '999',
						@MessageResult = 'Los siguientes conceptos de cuentas por cobrar no se encuentran activos: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--El documento cuenta por cobrar tiene detalles en 0 o negativos
			IF EXISTS (SELECT 1 FROM @Detail d WHERE d.AccountReceivableDocumentId = @Id AND d.Value <= 0)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'El documento cuenta por cobrar tiene detalles en 0 o negativos')
				RETURN
			END
		
			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail d
				LEFT JOIN Portfolio.AccountReceivableDocumentDetail Ag WITH (NOLOCK) ON d.Id = Ag.Id
				WHERE d.ChangeTracker <> 'Added' AND (@Id <> ISNULL(Ag.AccountReceivableDocumentId, 0))				
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Los detalles del documento de cuenta por cobrar han sido alterados'
				RETURN
			END

			-- Valido la suma de los conceptos segun la naturaleza 	
			IF @Value <> ISNULL((SELECT SUM(Value * IIF(Nature = 1, -1, 1)) FROM @Detail), 0)
			BEGIN 					
				SELECT @CodeResult = 999, 
						@MessageResult = 'El valor del documento de cuenta por cobrar no corresponde al de sus detalles'
				RETURN
			END

			/**********************************  INSERTAR / ACTUALIZAR CABECERA **********************************/
				
			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END
			
			IF @Id = 0
			BEGIN 
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 160, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'documento cuenta por cobrar')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO Portfolio.AccountReceivableDocument
				(
					[Code],[OperatingUnitId],[DocumentDate],[CustomerId],[MainAccountId],[CostCenterId],[InvoiceNumber],[Term],[ExpiredDate],
					[Share],[Value],[DebitValue],[CreditValue],[Observation],[AccountReceivableId],[Status],[CreationUser],[CreationDate],
					[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],[CurrencyId]
				)
				SELECT	@Code,@OperatingUnitId,@DocumentDate,@CustomerId,@MainAccountId,@CostCenterId,@InvoiceNumber,@Term,@ExpiredDate,
						@Share,@Value,@DebitValue,@CreditValue,@Observation,@AccountReceivableId,@Status,@CodeUser,[Common].[GETDATE](),
						@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate, @CurrencyId

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE  Portfolio.AccountReceivableDocument
					SET [OperatingUnitId]  = @OperatingUnitId,
						[DocumentDate]  = @DocumentDate,
						[CustomerId] = @CustomerId,
						[MainAccountId] = @MainAccountId,
						[CostCenterId] = @CostCenterId,
						[InvoiceNumber]  = @InvoiceNumber,
						[Term] = @Term,
						[ExpiredDate] = @ExpiredDate,
						[Share] = @Share,
						[Value] = @Value,
						[DebitValue] = @DebitValue,
						[CreditValue] = @CreditValue,
						[Observation] = @Observation,
						[AccountReceivableId] =  @AccountReceivableId,	
						[CurrencyId] = @CurrencyId,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END
		
			/*********************************** INSERTAR / ACTUALIZAR DETALLE ***********************************/

			INSERT INTO Portfolio.AccountReceivableDocumentDetail
			(
				AccountReceivableDocumentId,AccountReceivableConceptId,MainAccountId,
				ThirdPartyId,CostCenterId,Nature,Value,Observation

			)
			SELECT	@Id,d.AccountReceivableConceptId, d.MainAccountId,
					d.ThirdPartyId,d.CostCenterId,d.Nature,d.Value,d.Observation
			FROM @Detail d
			WHERE d.ChangeTracker = 'Added'

			UPDATE Ardd
				SET Ardd.AccountReceivableConceptId = d.AccountReceivableConceptId,
					Ardd.MainAccountId = d.MainAccountId,
					Ardd.ThirdPartyId = d.ThirdPartyId,
					Ardd.CostCenterId = d.CostCenterId,
					Ardd.Nature = d.Nature,
					Ardd.Value = d.Value,
					Ardd.Observation = d.Observation
			FROM @Detail d
			JOIN Portfolio.AccountReceivableDocumentDetail Ardd ON d.Id = Ardd.Id				
			WHERE Ardd.AccountReceivableDocumentId = @Id AND d.ChangeTracker = 'Modified'		
		END

		/*************************************************  CONFIRMACION *************************************************/		

		IF @Status = 2
		BEGIN
			---------------------------------------- GENERACION CUENTAS POR COBRAR ----------------------------------------

			EXEC Common.SP_GetSequence 160, 682, @OperatingUnitId, NULL, NULL, @IsManual OUT, @AccountReceivableCode OUT, @Code_Output OUT, @Message_Output OUT
			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = REPLACE(@Message_Output, '{0}', 'Cuentas Por Cobrar')
				RETURN
			END

			INSERT INTO [Portfolio].[AccountReceivable]
			(
				[Code],[AccountReceivableType],[ThirdPartyId],[CustomerId],[InvoiceNumber],[AccountReceivableDate],
				[Term],[ExpiredDate],[Observations],[PortfolioStatus],[OpeningBalance],[PaymentAgreement],[RegistrationAdjusted],
				[CostCenterId],[OperatingUnitId],[NumberShares],[Value],[Balance],
				[Status],[AccountWithoutRadicateId],AffectBudget,[CreationUser],[CreationDate],[CurrencyId]
			)
			SELECT @AccountReceivableCode,2,c.ThirdPartyId,c.Id,ard.InvoiceNumber,ard.DocumentDate,
				   ard.Term,ard.ExpiredDate,ISNULL(ard.Observation,''),3,0,0,0,
				   ard.CostCenterId,ard.OperatingUnitId,ard.Share,ard.Value,ard.Value,
				   2,ard.MainAccountId,0,@CodeUser,[Common].[GETDATE](), ard.CurrencyId
			FROM Portfolio.AccountReceivableDocument ard WITH (NOLOCK)
			JOIN Common.Customer c WITH (NOLOCK) ON ard.CustomerId = c.Id
			WHERE ard.Id = @Id
			
			SET @AccountReceivableId = SCOPE_IDENTITY()
					
			INSERT INTO Portfolio.AccountReceivableShare
			(
				AccountReceivableId,Number,ExpiredDate,[Value],Balance,DebitValue,CreditValue,TransferValue,PaymentValue,
				CrossingValue,InterestValue,SurchargesValue,CapitalRepaymentAgreement,FinancialInterest,RepaymentAgreementInterest
			)
			SELECT	@AccountReceivableId,1,ar.ExpiredDate,ar.Value,ar.Balance,0,0,0,0,
					0,0,0,0,0,0
			FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
			WHERE ar.Id = @AccountReceivableId

			INSERT INTO Portfolio.AccountReceivableAccounting
			(
				AccountReceivableId,MainAccountId,ThirdPartyId,CostCenterId,[Value],Balance
			)
			SELECT	@AccountReceivableId,ma.Id,
					IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL),
					IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL),
					ar.Value,ar.Balance
			FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ar.AccountWithoutRadicateId = ma.Id
			WHERE ar.Id = @AccountReceivableId

			SET @Message_Output = CONCAT('Código Cuenta por Cobrar ',  @AccountReceivableCode)
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
								sp.JournalVoucherTypeDocumentAccountReceivableId IdJournalVoucher,
								ard.DocumentDate VoucherDate, 
								'False' Imported,
								2 Status,
								CONCAT('Documento Cuenta x Cobrar Factura: ', ard.InvoiceNumber, ': ', ard.Observation) Detail, 
								'AccountReceivableDocument' EntityName,
								@Code EntityCode,
								@Id EntityId,
								0 IsClosedYear,
								@CurrencyId as CurrencyId
						FROM Portfolio.AccountReceivableDocument ard WITH (NOLOCK)
						JOIN Portfolio.SettingPortfolio sp WITH (NOLOCK) ON ard.OperatingUnitId = sp.OperatingUnitId
						WHERE ard.Id = @Id
					) JournalVoucher
					JOIN
					( 
							SELECT	0 Id,
									0 IdAccounting,
									ma.Id IdMainAccount,
									IIF(ma.HandlesThirdParty = 1, c.ThirdPartyId, NULL) IdThirdParty, 
									IIF(ma.HandlesCostCenter = 1, ard.CostCenterId, NULL) IdCostCenter, 									
									ard.Value DebitValue, 
									0 CreditValue
							FROM Portfolio.AccountReceivableDocument ard WITH (NOLOCK)
							JOIN Common.Customer c WITH (NOLOCK) ON ard.CustomerId = c.Id
							JOIN GeneralLedger.MainAccounts ma ON ard.MainAccountId = ma.Id
							WHERE ard.Id = @Id
						UNION ALL
							SELECT	0 Id,
									0 IdAccounting,
									ma.Id IdMainAccount,
									IIF(ma.HandlesThirdParty = 1, ardd.ThirdPartyId, NULL) IdThirdParty, 
									IIF(ma.HandlesCostCenter = 1, ardd.CostCenterId, NULL) IdCostCenter, 									
									IIF(ardd.Nature = 1, ardd.Value, 0) DebitValue, 
									IIF(ardd.Nature = 1, 0, ardd.Value) CreditValue
							FROM Portfolio.AccountReceivableDocumentDetail ardd WITH (NOLOCK)
							JOIN GeneralLedger.MainAccounts ma ON ardd.MainAccountId = ma.Id
							WHERE ardd.AccountReceivableDocumentId = @Id
					) JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
					For xml AUTO,TYPE, ELEMENTS
				)
			)
			
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

			--actualizo el documento de cuenta por cobrar despues de la creación de cuenta por cobrar 
			UPDATE Portfolio.AccountReceivableDocument 
				SET [AccountReceivableId] = @AccountReceivableId
			WHERE Id= @Id

			SELECT @Message_Output = CONCAT('Se generó el Comprobante contable de tipo ', jvt.Code, ' - ', jvt.Name)
			FROM Portfolio.AccountReceivableDocument ard
			JOIN Portfolio.SettingPortfolio sp ON ard.OperatingUnitId = sp.OperatingUnitId
			JOIN GeneralLedger.JournalVoucherTypes jvt On sp.JournalVoucherTypeDocumentAccountReceivableId = jvt.Id
			Where ard.Id = @Id

			SELECT @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/********************************** *************************************** **********************************/

		IF @Status = 1
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Portfolio.PortfolioControl WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Portfolio.PortfolioControl (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentTypeControl, @CodeUser, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Portfolio.PortfolioControl WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code
		END

		---------------------------------------------------- RESULTADO ----------------------------------------------------

		SELECT @CodeResult = 0, 
			@MessageResult = CASE @Status
				WHEN 2 THEN CONCAT('Se guardó y confirmó el documento cuenta por cobrar con código ', @Code)
				WHEN 3 THEN CONCAT('Se anuló el documento cuenta por cobrar con código ', @Code)
				ELSE CONCAT('Se guardó el documento cuenta por cobrar con código ', @Code)
		END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		---------------------------------------------------- RESULTADO ----------------------------------------------------		
		SELECT @CodeResult = 999, 
			   @MessageResult = 'SP_SaveAccountReceivableDocument_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar, confirmar o anular documentos de cuentas por cobrar en el módulo de cartera (facturas, notas débito/crédito). Recibe los datos del documento en formato XML, valida la existencia y estado del cliente pagador, la configuración de cartera para la unidad operativa, el número de factura, el valor y las cuotas; si el documento ya existe y está confirmado o anulado, rechaza la operación. Según el estado recibido, confirma/anula el documento actualizando la tabla AccountReceivableDocument con usuario y fecha de modificación o anulación, o bien registra/actualiza el encabezado y el detalle contable (AccountReceivableDocumentDetail) con las líneas de movimiento débito/crédito, generando el comprobante contable (journal voucher) correspondiente. Retorna códigos de resultado, mensajes de error y el identificador y código del documento procesado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAccountReceivableDocument_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAccountReceivableDocument_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste, actualiza, confirma o anula un documento de cuenta por cobrar con sus detalles contables y, al confirmar, genera la cuenta por cobrar, sus cuotas, distribución contable y el comprobante contable asociado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El documento sólo puede modificarse si su Status actual es 1 (en edición); si está en estado Registrado/Confirmado/Anulado se rechaza; Debe existir configuración de cartera (Portfolio.SettingPortfolio) para la unidad operativa indicada; El cliente debe existir en Common.Customer y tener State distinto de 0 (activo); Share (cuotas) y Term (días de plazo) deben ser mayores a 0; Value del documento debe ser mayor a 0; El InvoiceNumber no puede existir previamente en Portfolio.AccountReceivable; Debe enviarse al menos un detalle no marcado como ''Deleted''; Todos los AccountReceivableConceptId del detalle deben existir y tener Status distinto de 0; Ningún detalle puede tener Value <= 0; Los detalles no marcados como ''Added'' deben pertenecer al mismo documento (no haber sido alterados de origen); La suma de Value*signo según Nature (Nature=1 resta, otra suma) en los detalles debe igualar el Value del encabezado', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.AccountReceivableDocument: Si @Status = 3 se anula el documento: actualiza Status, ModificationUser/Date, AnnulmentUser/Date con el usuario y fecha actual; [DELETE] Portfolio.AccountReceivableDocumentDetail: Elimina los detalles cuyo ChangeTracker = ''Deleted'' y pertenezcan al documento; [INSERT] Portfolio.AccountReceivableDocument: Cuando @Id = 0, obtiene el código vía Common.SP_GetSequence (formato 160/@IdForm=1522) e inserta el encabezado; si @Status=2 setea ConfirmationUser/Date con el usuario y fecha actuales; [UPDATE] Portfolio.AccountReceivableDocument: Cuando @Id <> 0 actualiza el encabezado existente con los datos del XML, ModificationUser/Date y, si @Status=2, ConfirmationUser/Date; [INSERT] Portfolio.AccountReceivableDocumentDetail: Inserta los detalles con ChangeTracker=''Added'' asociados al documento; [UPDATE] Portfolio.AccountReceivableDocumentDetail: Actualiza los detalles con ChangeTracker=''Modified'' que pertenezcan al documento; [INSERT] Portfolio.AccountReceivable: Si @Status=2, genera código vía SP_GetSequence (160/682) e inserta la cuenta por cobrar con AccountReceivableType=2, PortfolioStatus=3, Status=2, AccountWithoutRadicateId=MainAccountId del encabezado, Balance=Value y AffectBudget=0; [INSERT] Portfolio.AccountReceivableShare: Si @Status=2, crea una única cuota (Number=1) con ExpiredDate y Value de la cuenta por cobrar y los demás valores monetarios en 0; [INSERT] Portfolio.AccountReceivableAccounting: Si @Status=2, registra distribución contable con la MainAccount del encabezado; ThirdPartyId/CostCenterId sólo si la cuenta tiene HandlesThirdParty=1 / HandlesCostCenter=1 respectivamente; [UPDATE] Portfolio.AccountReceivableDocument: Tras crear la cuenta por cobrar, actualiza AccountReceivableId del documento con el SCOPE_IDENTITY() generado; [INSERT] Portfolio.PortfolioControl: Si @Status=1 y no existe registro previo para DocumentType=3 y DocumentNumber=@Code, inserta control con usuario y fecha del documento; [DELETE] Portfolio.PortfolioControl: Si @Status <> 1, elimina el registro de control para DocumentType=3 y DocumentNumber=@Code; [INSERT] GeneralLedger.JournalVouchers: Si @Status=2, arma XML con el encabezado (JournalVoucherTypeDocumentAccountReceivableId de SettingPortfolio) y el detalle contable (débito por la MainAccount del encabezado y débito/crédito según Nature de cada detalle) e invoca GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve 0 con mensaje según acción (guardó / guardó y confirmó / anuló) o 999 con el detalle de la validación o error capturado en CATCH', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAccountReceivableDocument_Output';
-- GO
