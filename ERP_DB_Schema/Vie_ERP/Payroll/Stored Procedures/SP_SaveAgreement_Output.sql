-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-03-08
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un convenio de nomina
-- =====================================================
CREATE PROCEDURE [Payroll].[SP_SaveAgreement_Output]
    @AgreementXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@Id INT OUTPUT,
	@Consecutive VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @GroupId INT,
			@OperatingUnitId INT, 
			@EmployeId INT,			
			@CompanyId INT,
			@ConceptId INT,
			@KindsAgreementsId INT,
			@Comments VARCHAR(250),
			@LiquidationType TINYINT,
			@TermType TINYINT,
			@AgreementValue DECIMAL(18,2),
			@NumberShares INT,
			@State VARCHAR(1),
			@StartingDate DATETIME,
			@CurrentBalance DECIMAL(18,2),
			@EndDateSuspend DATETIME,
			@CommentChangeState VARCHAR(250),
			@PaidVacation BIT,
			@TransferType TINYINT,
			@AccountReceivableAccountingId INT,
			@AccountReceivableConceptId INT,
			------------------------------
			@IdForm INT = 595,
			@DocumentTypeControl INT = 13,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------		
			@PortfolioNoteConceptSettingId int,
			@SubXml XML,			
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			------------------------------
			@InvoiceBalance  DECIMAL(18,2),
			@InvoiceNumber VARCHAR(20),
			@Code VARCHAR(20)

	--Tabla temporal de los detalles
	DECLARE @Detail TABLE
	(
		AgreementsDIdTmp INT,
		Id INT,
		AgreementsCId INT,
		ShareValuePaid DECIMAL(18,2),
		DatePayment DATETIME,
		TypePayment TINYINT,
		StateShare VARCHAR(250),
		ChangeTracker VARCHAR(30)	
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','int'),
				@Consecutive = t.x.value('Consecutive[1]','int'),				
				@GroupId = t.x.value('GroupId[1]','int'),
				@EmployeId = t.x.value('EmployeeId[1]','int'),
				@CompanyId = t.x.value('CompanyId[1]','int'),
				@ConceptId = t.x.value('ConceptId[1]','int'),
				@KindsAgreementsId = t.x.value('KindsAgreementsId[1]','int'),
				@Comments =  t.x.value('Comments[1]','varchar(250)'),
				@LiquidationType =  t.x.value('LiquidationType[1]','tinyint'),
				@TermType =   t.x.value('TermType[1]','tinyint'),
				@AgreementValue =  t.x.value('AgreementValue[1]','decimal(18,2)'),
				@NumberShares = t.x.value('NumberShares[1]','int'),
				@State =  t.x.value('State[1]','varchar(1)'),
				@StartingDate = t.x.value('StartingDate[1]','datetime'),
				@CurrentBalance = t.x.value('CurrentBalance[1]','decimal(18,2)'),
				@EndDatesuspend = t.x.value('EndDateSuspend[1]','datetime'),
				@CommentChangeState =  t.x.value('CommentChangeState[1]','varchar(250)'),
				@PaidVacation = t.x.value('PaidVacation[1]','bit'),
				@TransferType = t.x.value('TransferType[1]','tinyint'),
				@AccountReceivableAccountingId = t.x.value('AccountReceivableAccountingId[1]','int'),
				----------------------------------------------------------
				@OperatingUnitId = t.x.value('OperativeUnit[1]','int')	
				-------------------------------------------------------				
		FROM @AgreementXml.nodes('/AgreementsC') t(x)
				
		IF EXISTS (SELECT 1 FROM Payroll.AgreementsC Ac WITH (NOLOCK) WHERE Ac.Id = @Id AND Ac.State = 5)
		BEGIN 
			SELECT @Message = 'El convenio se encuentra en estado: ' +  IIF(Ac.State = 2, 'Confirmado', IIf(Ac.State = 3,'Suspendido',IIf(Ac.State = 4,'Terminado','Anulado' )))			
			FROM Payroll.AgreementsC Ac WITH (NOLOCK)
			WHERE Ac.Id = @Id

			SELECT	@CodeResult = 999, 
					@MessageResult = ISNULL(@Message, 'No existe el convenio.')
			RETURN
		END

		IF EXISTS (SELECT 1 FROM Payroll.AgreementsC Ac WITH (NOLOCK) WHERE Ac.Id = @Id AND Ac.State = 3) AND @State <> 2 
		BEGIN 
			SELECT @Message = 'El convenio se encuentra en estado: ' +  IIF(Ac.State = 2, 'Confirmado', IIf(Ac.State = 3,'Suspendido','Terminado'))
			FROM Payroll.AgreementsC Ac WITH (NOLOCK)
			WHERE Ac.Id = @Id
			
			SELECT @CodeResult = 999, 
					@MessageResult = ISNULL(@Message, 'No existe el convenio.')
			RETURN
		END
	
		IF EXISTS (SELECT 1 FROM Payroll.AgreementsC Ac WITH (NOLOCK) WHERE Ac.Id = @Id AND Ac.State = 2) AND @State <> 3 AND @State <> 5 
		BEGIN 
			SELECT @Message = 'El convenio se encuentra en estado: ' +  IIF(Ac.State = 2, 'Confirmado', 'Suspendido')
			FROM Payroll.AgreementsC Ac WITH (NOLOCK)
			WHERE Ac.Id = @Id

			SELECT @CodeResult = 999, 
					@MessageResult = ISNULL(@Message, 'No existe el convenio.')
			RETURN
		END

		/**********************************************************************************/	

		IF @State = 3
		BEGIN
			UPDATE [Payroll].[AgreementsC]
				SET [State] = @State
			WHERE Id = @Id
		END		
		BEGIN			
			/***************************************** VALIDACIONES CABECERA *****************************************/				

			-- Valido que se haya ingresado un empleado
			IF ISNULL(@EmployeId, 0) = 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe ingresar un empleado.'
				RETURN
			END

			-- Valido que se haya ingresado una compañia
			IF @CompanyId IS NULL OR @CompanyId = 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe ingresar una empresa.'
				RETURN
			END

			-- Valido que se haya ingresado una clase convenio
			IF @KindsAgreementsId IS NULL OR @KindsAgreementsId = 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe ingresar una clase de convenio.'
				RETURN
			END

			-- Valido que se haya ingresado un tipo de traslado
			IF @TransferType IS NULL AND EXISTS (SELECT 1 FROM Payroll.KindsAgreements Ka WITH (NOLOCK) WHERE Ka.Id = @KindsAgreementsId AND  AffectsAccountsReceivable = 1 )
			--@KindsAgreementsId IS NULL
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe ingresar un tipo de traslado.'
				RETURN
			END

			-- Valido que se haya ingresado una factura
			IF @AccountReceivableAccountingId IS NULL AND EXISTS (SELECT 1 FROM Payroll.KindsAgreements Ka WITH (NOLOCK) WHERE Ka.Id = @KindsAgreementsId AND  AffectsAccountsReceivable = 1 )			
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe ingresar una factura.'
				RETURN
			END
			
			-- Valido que se haya escogido un tipo de liquidacion
			IF @LiquidationType IS NULL OR @LiquidationType = 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe escoger un tipo de liquidación.'
				RETURN
			END
			
			-- Valido que se haya escogido un tipo de plazo
			IF @TermType IS NULL OR @TermType = 0
				BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe escoger un tipo de plazo.'
				RETURN
			END
					
			-- Valido que se haya escogido un concepto
			IF @ConceptId IS NULL OR @ConceptId = 0
				BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe escoger un concepto.'
				RETURN
			END

			-- Valido que se haya escogido una fecha inicial
			IF @StartingDate IS NULL
				BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Se debe escoger una fecha inicial.'
				RETURN
			END
		
			--Valido que el valor del convenio no sea menor a 0
			IF @AgreementValue < 0 
				BEGIN
				SELECT	@CodeResult = 999, 
					@MessageResult = 'El valor del convenio no debe ser menor a 0'
				RETURN
			END
			
			--Valido que el valor convenio no sea mayor al saldo de la factura			
			IF  @AgreementValue > 0 AND EXISTS (SELECT 1 FROM Payroll.KindsAgreements Ka WITH (NOLOCK) WHERE Ka.Id = @KindsAgreementsId AND ka.AffectsAccountsReceivable = 1)		
			BEGIN
				SELECT	@InvoiceNumber = ar.InvoiceNumber,
						@InvoiceBalance = ara.Balance
				FROM Portfolio.AccountReceivableAccounting ara WITH (NOLOCK)
				JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ara.AccountReceivableId = ar.Id
				WHERE ara.Id = @AccountReceivableAccountingId

				IF @AgreementValue > @InvoiceBalance
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = 'El Valor Covenio no debe ser mayor al saldo de la factura'
					RETURN
				END
			END

			-- Valido que la suma de los detalles de información de liquidación no sean mayores al saldo actual
			IF  ISNULL((SELECT SUM(ad.ShareValuePaid ) FROM @Detail ad WHERE ad.AgreementsCId = @Id ), 0) > @CurrentBalance 
			BEGIN 					
				SELECT @CodeResult = 999, 
						@MessageResult = 'Los detalles no pueden ser mayores al saldo actual'
				RETURN
			END

			/************************************ VALIDACIONES OTROS PROCESOS ************************************/		
			
			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail
				SELECT	t.x.value('AgreementsDIdTmp[1]','int'),
						t.x.value('Id[1]','int'),
						t.x.value('AgreementsCId[1]','int'),
						t.x.value('ShareValuePaid[1]','decimal(18,2)'),
						t.x.value('DatePayment[1]','datetime'),
						t.x.value('TypePayment[1]','tinyint'),
						t.x.value('StateShare[1]','varchar(250)'),						
						t.x.value('ChangeTracker[1]','varchar(30)')
				FROM @AgreementXml.nodes('/AgreementsC/AgreementsD') t(x)
						
			--se eliminan los detalles marcados para su eliminación
			DELETE Ad
			FROM Payroll.AgreementsD Ad
			JOIN @Detail d ON Ad.Id = d.Id
			WHERE @Id = Ad.AgreementsCId AND d.ChangeTracker = 'Deleted'

			DELETE d FROM @Detail d WHERE d.ChangeTracker = 'Deleted'

			/***************************************  VALIDACIONES DETALLE ***************************************/

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail d
				LEFT JOIN Payroll.AgreementsD Ag WITH (NOLOCK) ON d.Id = Ag.Id
				WHERE d.ChangeTracker <> 'Added' AND (@Id <> ISNULL(Ag.AgreementsCId, 0))				
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Los detalles del Convenio han sido alterados.'
				RETURN
			END

			/**********************************  INSERTAR / ACTUALIZAR CABECERA **********************************/
							
			IF @Id = 0	
			BEGIN
				-- Obtener y actualizar el consecutivo de la tabla Common.Consecutive
				DECLARE @tmpConsecutive TABLE(Consecutive INT)
    
				UPDATE Common.Consecutive 
				SET NumberConsecutive += 1 
				OUTPUT INSERTED.NumberConsecutive INTO @tmpConsecutive 
				WHERE [Description] = 'CONVENIOS'
    
				SELECT @Consecutive = Consecutive FROM @tmpConsecutive

				-- Validar que no exista el consecutivo
				IF EXISTS (SELECT 1 FROM Payroll.AgreementsC WHERE Consecutive = @Consecutive)
				BEGIN 
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Ya existe un consecutivo ' + CAST(@Consecutive AS VARCHAR(10)) + ' en convenios'
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Payroll].[AgreementsC]
				(
					[Consecutive],[GroupId],[EmployeeId],[CompanyId],[ConceptId],[KindsAgreementsId],[Comments],
					[LiquidationType],[TermType],[AgreementValue],[NumberShares],[State],[StartingDate],
					[CurrentBalance],[EndDateSuspend],[CommentChangeState],[PaidVacation],[TransferType],[AccountReceivableAccountingId]
				)
				SELECT	@Consecutive,@GroupId,@EmployeId,@CompanyId,@ConceptId,@KindsAgreementsId,
						@Comments,@LiquidationType,@TermType,@AgreementValue,@NumberShares,
						@State,@StartingDate,@CurrentBalance,@EndDateSuspend,@CommentChangeState,@PaidVacation,@TransferType,@AccountReceivableAccountingId

				--Obtengo el id de la cabecera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando		
			BEGIN
				UPDATE [Payroll].[AgreementsC]
					SET 
						[GroupId] = @GroupId,							
						[EmployeeId] = @EmployeId,
						[CompanyId] = @CompanyId,
						[ConceptId] = @ConceptId,
						[KindsAgreementsId] = @KindsAgreementsId,
						[Comments] = @Comments,
						[LiquidationType] = @LiquidationType,
						[TermType] = @TermType,
						[AgreementValue] = @AgreementValue,
						[NumberShares] = @NumberShares,
						[StartingDate] = @StartingDate,
						[CurrentBalance] = @CurrentBalance,
						[EndDateSuspend] = @EndDateSuspend,
						[CommentChangeState] = @CommentChangeState,
						[PaidVacation] = @PaidVacation,
						[TransferType] = @TransferType,
						[AccountReceivableAccountingId] = @AccountReceivableAccountingId,
						[State] = @State
				WHERE Id = @Id
			END
			
			/**************** VALIDO SI ESTA CONFIRMADO Y LA CLASE DE CONVENIO RECLASIFICA ******************/
			IF @State = 2
			BEGIN
				/*A la hora de confirmar un convenio, se crea como cliente el tercero "Empleado*/		
				IF EXISTS (SELECT 1 FROM Payroll.KindsAgreements Ka WITH (NOLOCK) WHERE Ka.Id = @KindsAgreementsId AND ka.AffectsAccountsReceivable = 1 AND Ka.ReclassifyAccountsReceivable = 1)
				BEGIN 	
					DECLARE @IdCustomer INT, 
							@IsManual BIT,
							@NameEmployee VARCHAR(300), 
							@EmployeNit VARCHAR(15),
							@MainAccountId INT,
							@CostCenterId INT,
							@Observation VARCHAR(300),
							@accountReceivableDocumentId INT

					/*OBTENER Id Cuenta contable*/				
					SELECT	@MainAccountId = Ma.Id,
							@AccountReceivableConceptId = Arc.Id 
					FROM Payroll.AgreementsC Ac WITH (NOLOCK)
					JOIN Payroll.KindsAgreements Ka WITH (NOLOCK) ON Ka.Id = Ac.KindsAgreementsId
					JOIN GeneralLedger.MainAccounts Ma WITH (NOLOCK) ON Ka.AccountId = Ma.Id 
					JOIN Portfolio.AccountReceivableConcept Arc WITH (NOLOCK) ON Ka.AccountReceivableConceptId = Arc.Id 
					WHERE Ac.KindsAgreementsId = @KindsAgreementsId
			
					INSERT INTO [Common].[Customer]
					(
						[Nit],[Name],[EPSCode],[ThirdPartyId],[MainAccountReceivableId],[Term],[State],[CreationUser],[CreationDate]
					)
					SELECT	tp.Nit,tp.Name,'',tp.Id,@MainAccountId,30,1,@CodeUser,[Common].[GETDATE]()						
					FROM Payroll.Employee e WITH (NOLOCK) 
					JOIN Common.ThirdParty Tp WITH (NOLOCK) ON e.ThirdPartyId = Tp.Id 
					LEFT JOIN Common.Customer C WITH (NOLOCK) ON Tp.Id = C.ThirdPartyId
					WHERE E.Id  = @EmployeId AND C.Id IS NULL

					SELECT	@EmployeNit = c.Nit,
							@NameEmployee = Tp.Name, 
							@IdCustomer = C.Id,
							@CostCenterId = e.CostCenterId 
					FROM Payroll.Employee e WITH (NOLOCK) 
					JOIN Common.ThirdParty Tp WITH (NOLOCK) ON e.ThirdPartyId = Tp.Id 
					JOIN Common.Customer C WITH (NOLOCK) ON Tp.Id = C.ThirdPartyId
					WHERE E.Id  = @EmployeId 	
	
					SET @Observation = CONCAT('Convenio Descuento de Nomina Nro ',@Consecutive,' Factura ',' ', @NameEmployee,' ',@InvoiceNumber) 
					
					---------------------------------------- GENERACION CUENTAS POR COBRAR ----------------------------------------					
					SET @SubXml = CONVERT					
						(
							XML, 
							(
								SELECT *
								FROM 
								(
									SELECT  0 Id,
											'' Code,
											@OperatingUnitId OperatingUnitId,
											a.StartingDate DocumentDate,
											c.Id CustomerId,
											ka.AccountId MainAccountId,
											CONCAT('CVE-', @InvoiceNumber) InvoiceNumber,
											30 Term,
											DATEADD(DAY, 30, a.StartingDate) ExpiredDate,
											1 Share,
											a.AgreementValue Value,
											0 DebitValue,
											a.AgreementValue CreditValue,
											@Observation Observation,
											2 Status,
											'Added' ChangeTracker
									FROM Payroll.KindsAgreements ka WITH (NOLOCK)
									JOIN Payroll.AgreementsC a WITH (NOLOCK) ON ka.Id = a.KindsAgreementsId
									JOIN Payroll.Employee e WITH (NOLOCK) ON a.EmployeeId = e.Id
									JOIN Common.Customer c WITH (NOLOCK) ON e.ThirdPartyId = c.ThirdPartyId
									WHERE a.Id = @Id
								) AccountReceivableDocument
								JOIN
								( 
									SELECT  0 Id,
											0 AccountReceivableDocumentId,
											'' Code,
											ka.AccountReceivableConceptId,
											arc.MainAccountId,
											e.ThirdPartyId,
											e.CostCenterId,
											2 Nature,
											a.AgreementValue Value,
											@Observation Observation,
											'Added' ChangeTracker
									FROM Portfolio.AccountReceivableConcept arc WITH (NOLOCK)
									JOIN Payroll.KindsAgreements ka WITH (NOLOCK) ON arc.Id = ka.AccountReceivableConceptId
									JOIN Payroll.AgreementsC a WITH (NOLOCK) ON ka.Id = a.KindsAgreementsId
									JOIN Payroll.Employee e WITH (NOLOCK) ON a.EmployeeId = e.Id
									WHERE a.Id = @Id
								) AccountReceivableDocumentDetail ON AccountReceivableDocument.Id = AccountReceivableDocumentDetail.AccountReceivableDocumentId
								For xml AUTO,TYPE, ELEMENTS
							)
						)
												
					EXEC Portfolio.SP_SaveAccountReceivableDocument_Output	@SubXml,@CodeUser, @Code_Output OUT, @Message_Output OUT, @accountReceivableDocumentId OUT, NULL 

					IF @Code_Output <> 0
					BEGIN
						SELECT	@CodeResult = 999, 
								@MessageResult = 'Ocurrieron errores al intentar crear el documento cuenta por cobrar: ' + CHAR(13) + CHAR(10) + ISNULL(@Message_Output, 'No se pudo generar el documento cuenta por cobrar')
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

					-------------------------------------------------- Nota Débito / Crédito ---------------------------------------------------------	

 					SELECT @PortfolioNoteConceptSettingId = ps.PortfolioNoteConceptId FROM Payroll.PayrollSettings ps WITH (NOLOCK)

					SET @SubXml  = 	CONVERT                  
						(
							XML, 
							(
								SELECT  *,
										(               
											SELECT  0 Id,               
													0 PortfolioNoteId,
													1 PortfolioNoteAccountReceivableAdvanceIdTmp,
													ara.AccountReceivableId,
													ars.Id AccountReceivableShareId,
													ara.MainAccountId,
													ara.Id AccountReceivableAccountingId,
													a.AgreementValue AdjusmentValue,
													0 PercentageValue,
													0 PreviousBalance,
													ara.Balance,
													@InvoiceNumber InvoiceNumber,
													ars.Number NumberShare,
													2 Nature,
													ara.Value,                   
													'Added' ChangeTracker
											FROM Payroll.AgreementsC a WITH (NOLOCK)
											JOIN Payroll.Employee e WITH (NOLOCK) ON a.EmployeeId = e.Id                                
											JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) ON a.AccountReceivableAccountingId = ara.Id
											JOIN Portfolio.AccountReceivableShare ars WITH (NOLOCK) ON ara.AccountReceivableId = ars.AccountReceivableId
											WHERE a.Id = @Id
											FOR XML PATH('PortfolioNoteAccountReceivableAdvance'), TYPE
										),
										(               
											SELECT  0 Id,
													0 PortfolioNoteId,
													@PortfolioNoteConceptSettingId PortfolioNoteConceptId,
													arc.MainAccountId,     
													e.ThirdPartyId,
													e.CostCenterId,                     
													1 Nature,
													a.AgreementValue Value,
													CONCAT('Convenio de Descuento de Nomina ', a.Consecutive) Observations,
													'Added' ChangeTracker
											FROM Portfolio.AccountReceivableConcept arc WITH (NOLOCK)
											JOIN Payroll.KindsAgreements ka WITH (NOLOCK) ON arc.Id = ka.AccountReceivableConceptId
											JOIN Payroll.AgreementsC a WITH (NOLOCK) ON ka.Id = a.KindsAgreementsId
											JOIN Payroll.Employee e WITH (NOLOCK) ON a.EmployeeId = e.Id
											WHERE a.Id = @Id                    
											FOR XML PATH('PortfolioNoteDetail'), TYPE
										)
								FROM        
								(
									SELECT  0 Id,
											'' Code,
											a.StartingDate NoteDate,
											c.Id CustomerId,
											c.ThirdPartyId,
											CONCAT(c.Nit,' - ',c.Name,' ',@InvoiceNumber,' trasladado a ',@EmployeNit,' - ',@NameEmployee,' CVE-',@InvoiceNumber) Observations,
											2 Nature,
											1 NoteType,
											@OperatingUnitId OperatingUnitId,
											2 Status,
											'Added' ChangeTracker
									FROM Payroll.AgreementsC a WITH (NOLOCK)
									JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) ON a.AccountReceivableAccountingId = ara.Id
									JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ara.AccountReceivableId = ar.Id
									JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
									JOIN Common.Customer c WITH (NOLOCK) ON c.ThirdPartyId = tp.Id
									WHERE a.Id = @Id        
								) PortfolioNote
								FOR XML AUTO,TYPE, ELEMENTS
							)
						)

					EXEC Portfolio.SP_SavePortfolioNote_Output	@SubXml,@CodeUser, 0 ,@Code_Output OUT, @Message_Output OUT,NULL,NULL 

					IF @Code_Output <> 0
					BEGIN
						SELECT	@CodeResult = 999, 
								@MessageResult = 'Ocurrieron errores al intentar crear la nota debito y credito: ' + CHAR(13) + CHAR(10) + ISNULL(@Message_Output, 'No se pudo generar la nota debito y credito')
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

					------------------------------------------------------------------------------------------------------------------------------------

					INSERT INTO [Payroll].[AgreementsAccountReceivableTransfer]
					(
						AgreementsCId, SourceAccountReceivableAccountingId, DestinationAccountReceivableAccountingId
					)
					SELECT a.Id, a.AccountReceivableAccountingId, ara.Id
					FROM [Payroll].[AgreementsC] a WITH (NOLOCK)
					JOIN Portfolio.AccountReceivableDocument ard WITH (NOLOCK) ON @accountReceivableDocumentId = ard.Id
					JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) ON ard.AccountReceivableId = ara.AccountReceivableId
					WHERE a.Id = @Id

					UPDATE a
						SET a.[AccountReceivableAccountingId] = ara.Id
					FROM [Payroll].[AgreementsC] a
					JOIN Portfolio.AccountReceivableDocument ard WITH (NOLOCK) ON @accountReceivableDocumentId = ard.Id
					JOIN Portfolio.AccountReceivableAccounting ara WITH (NOLOCK) ON ard.AccountReceivableId = ara.AccountReceivableId
					WHERE a.Id = @Id
				END				
			END

			/*********************************** INSERTAR / ACTUALIZAR DETALLE ***********************************/

			INSERT INTO Payroll.AgreementsD
			(
				AgreementsCId,ShareValuePaid, DatePayment, TypePayment, StateShare
			)
			SELECT	@Id, d.ShareValuePaid, d.DatePayment, d.TypePayment, d.StateShare
			FROM @Detail d
			WHERE d.ChangeTracker = 'Added'

			UPDATE tod
				SET tod.ShareValuePaid = d.ShareValuePaid,
					tod.DatePayment = d.DatePayment,
					tod.TypePayment = d.TypePayment,
					tod.StateShare = d.StateShare
			FROM @Detail d
			JOIN Payroll.AgreementsD tod ON d.Id = tod.Id				
			WHERE tod.AgreementsCId = @Id AND d.ChangeTracker = 'Modified'		
		END
		
		SELECT @CodeResult = 0, 
			@MessageResult = CASE @State
			WHEN 2 THEN CONCAT('Se guardó y confirmó el convenio con el consecutivo ', @Consecutive)
			WHEN 3 THEN CONCAT('Se suspendio el convenio con el consecutivo ', @Consecutive)
			WHEN 4 THEN CONCAT('Se terminó el convenio con el consecutivo ', @Consecutive)
			WHEN 5 THEN CONCAT('Se anulo el convenio con el consecutivo ', @Consecutive)
			ELSE CONCAT('Se guardó el convenio con el consecutivo ', @Consecutive)
		END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = 'SP_SaveAgreement_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END