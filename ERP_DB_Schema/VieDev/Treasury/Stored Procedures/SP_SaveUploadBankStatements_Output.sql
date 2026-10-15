-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-04-13
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un cargue de extracto
-- =====================================================
CREATE PROCEDURE [Treasury].[SP_SaveUploadBankStatements_Output]
    @UploadBankStatementsXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @OperatingUnitId INT,			
			@Year INT,
			@Month INT,
			@Status TINYINT,
			@EntityBankAccountId INT,
			@InitialBalance DECIMAL(18,2),
			@EndingBalance DECIMAL(18,2),
			@ChangeTracker VARCHAR(30),
			------------------------------
			@IdForm INT = 2235,
			------------------------------
			@Message VARCHAR(200),
			------------------------------							
			@Code_Output INT,
			@Message_Output VARCHAR(200)
			------------------------------
			
	--Tabla temporal de los detalles
	DECLARE @Detail TABLE
	(		
		Id INT,
		UploadBankStatementsId INT,
		TransactionDate DATETIME,
		ConsecutiveBank VARCHAR(60),
		TransactionCode VARCHAR(60),
		DescriptionTransaction VARCHAR(60),
		ValueDebit DECIMAL(18, 2),
		ValueCredit DECIMAL(18, 2) ,	
		BankCheck bigint,
		PaymentReferenceOne VARCHAR(15),
		PaymentReferenceTwo VARCHAR(15),
		DocumentType INT,
		ChangeTracker VARCHAR(30)	
	)
	BEGIN TRY
	
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','INT'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
				@Code = t.x.value('Code[1]','VARCHAR(20)'),	
				@EntityBankAccountId = t.x.value('EntityBankAccountId[1]','INT'),	
				@Year = t.x.value('Year[1]','INT'),
				@Month = t.x.value('Month[1]','INT'),
				@Status = t.x.value('Status[1]','TINYINT'),
				@InitialBalance = t.x.value('InitialBalance[1]','DECIMAL(18,2)'),
				@EndingBalance = t.x.value('EndingBalance[1]','DECIMAL(18,2)'),
				@ChangeTracker = t.x.value('ChangeTracker[1]','VARCHAR(30)')
				-------------------------------------------------------				
		FROM @UploadBankStatementsXml.nodes('/UploadBankStatements') t(x)
								
		/************************************* VALIDACIONES GENERALES ************************************/	

		IF EXISTS (SELECT 1 FROM Treasury.UploadBankStatements WITH	(NOLOCK) WHERE Id = @Id AND [Status] = 3)
		BEGIN
			SELECT	'999' AS CodeMessage,
					'El cargue de extractos bancarios se encuentra Anulado' Message,
					0 AS UploadBankStatementsId,
					CAST(3 AS TINYINT) AS [Status]
			FROM Treasury.UploadBankStatements 
			WHERE Id = @Id
			RETURN
		END

		/**********************************************************************************/

		IF @Status = 3
		BEGIN
			-- Validamos que el Cargue a Anular no se encuentre relacionado a ninguna conciliación automática
			IF EXISTS(
				SELECT DISTINCT 1 
				FROM Treasury.UploadBankStatements ups
				JOIN Treasury.UploadBankStatementsDetail upsd ON ups.Id = upsd.UploadBankStatementsId
				JOIN Treasury.BankReconciliationAutomaticExtractDetail braed ON braed.UploadBankStatementsDetailId = upsd.Id
				WHERE ups.Id = @Id
				)
			BEGIN
				SELECT	'999' AS CodeMessage,
						'El cargue de extracto bancario a anular está asociado a una Conciliación Bancaria Automática' Message,
						0 AS UploadBankStatementsId,
						CAST(3 AS TINYINT) AS [Status]
				FROM Treasury.UploadBankStatements 
				WHERE Id = @Id
				RETURN
			END

			UPDATE Treasury.UploadBankStatements
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END	
		ELSE
		BEGIN			
			/***************************************** VALIDACIONES CABECERA *****************************************/				

			-- Valido que se haya ingresado una cuenta bancaria
			IF ISNULL(@EntityBankAccountId, 0) = 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'Se debe ingresar una cuenta bancaria.')
				RETURN
			END	


			/************************************ VALIDACIONES OTROS PROCESOS ************************************/		
			
			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail
				SELECT	t.x.value('Id[1]','INT'),
						t.x.value('UploadBankStatementsId[1]','INT'),
						t.x.value('TransactionDate[1]','DATETIME'),
						t.x.value('ConsecutiveBank[1]','VARCHAR(60)'),
						t.x.value('TransactionCode[1]','VARCHAR(60)'),
						t.x.value('DescriptionTransaction[1]','VARCHAR(60)'),
						t.x.value('ValueDebit[1]','DECIMAL(18,2)'),
						t.x.value('ValueCredit[1]','DECIMAL(18,2)'),
						t.x.value('BankCheck[1]','bigint'),
						t.x.value('PaymentReferenceOne[1]','VARCHAR(15)'),						
						t.x.value('PaymentReferenceTwo[1]','VARCHAR(15)'),	
						t.x.value('DocumentType[1]','INT'),
						t.x.value('ChangeTracker[1]','VARCHAR(30)')
				FROM @UploadBankStatementsXml.nodes('/UploadBankStatements/UploadBankStatementsDetail') t(x)
						
			--se eliminan los detalles marcados para su eliminación
			DELETE ubsd
			FROM Treasury.UploadBankStatementsDetail ubsd
			JOIN @Detail d ON ubsd.Id = d.Id
			WHERE @Id = ubsd.UploadBankStatementsId AND d.ChangeTracker = 'Deleted'

			DELETE d FROM @Detail d WHERE d.ChangeTracker = 'Deleted'

			
			--Se obtiene los detalles que ya estan y no han sido actualizados 
			INSERT INTO @Detail
				SELECT	ubsd.Id,
						ubsd.UploadBankStatementsId,
						ubsd.TransactionDate,
						ubsd.ConsecutiveBank,
						ubsd.TransactionCode,
						ubsd.DescriptionTransaction,
						ubsd.ValueDebit,
						ubsd.ValueCredit,
						ubsd.BankCheck,
						ubsd.PaymentReferenceOne,
						ubsd.PaymentReferenceTwo,
						ubsd.DocumentType,
						'' ChangeTracker
				FROM Treasury.UploadBankStatementsDetail ubsd
				LEFT JOIN @Detail d ON ubsd.Id = d.Id
				WHERE ubsd.UploadBankStatementsId = @Id AND d.Id IS NULL
		

			/***************************************  VALIDACIONES DETALLE ***************************************/

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail d
				LEFT JOIN Treasury.UploadBankStatementsDetail ubsd WITH (NOLOCK) ON d.Id = ubsd.Id
				WHERE d.ChangeTracker <> 'Added' AND (@Id <> ISNULL(ubsd.UploadBankStatementsId, 0))				
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Los extractos bancarios han sido alterados.'
				RETURN
			END

			/**********************************  INSERTAR / ACTUALIZAR CABECERA **********************************/
							
			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Code = '' or @Code is null
			BEGIN
				--Consultamos si la secuencia es con O o OU
				DECLARE @scope VARCHAR(5) = ''
				DECLARE @idSequenceDetail INT
				DECLARE @pattern VARCHAR(300)
				DECLARE @NextS INT
				SELECT @scope = Scope FROM Treasury.TreasurySequence
				WHERE IdForm = @IdForm

				IF @scope = 'O' --Si el ambito es por organización
				BEGIN
					SELECT TOP 1 @pattern = cs.Pattern, @NextS = bsd.[NEXT] , @idSequenceDetail = bsd.Id  
					FROM Treasury.TreasurySequenceDetail bsd 
					INNER JOIN Treasury.TreasurySequence bs ON bs.Id = bsd.IdSequenseTreasuryC
					INNER JOIN Common.Sequense cs ON cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm
					ORDER BY bsd.NEXT DESC
				END
				ELSE BEGIN --Si el ambito es por unidad operativa
					SELECT TOP 1 @pattern = cs.Pattern, @NextS = bsd.[NEXT] , @idSequenceDetail = bsd.Id  
					FROM Treasury.TreasurySequenceDetail bsd 
					INNER JOIN Treasury.TreasurySequence bs ON bs.Id = bsd.IdSequenseTreasuryC
					INNER JOIN Common.Sequense cs ON cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm and bsd.IdOperatingUnit = @OperatingUnitId
					ORDER BY bsd.NEXT DESC
				END
					
				IF (@idSequenceDetail is null)
				BEGIN
					SELECT 999 AS CodeMessage, 'Secuencia no encontrada para generar el paquete' + IIF(@IdForm = '2239', ' personalizado', '') AS Message, 0 Id, '' Code
					RETURN
				END

				SELECT @Code = dbo.GetSequence('',@pattern,@NextS)
				UPDATE Treasury.TreasurySequenceDetail set [NEXT] += 1 WHERE Id = @idSequenceDetail
			END

			IF ISNULL(@Id, 0) = 0			
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica				
				SELECT @pattern = cs.Pattern,
						@NextS = bsd.[NEXT], 
						@idSequenceDetail = bsd.Id  
				FROM Treasury.TreasurySequenceDetail bsd 
				JOIN Treasury.TreasurySequence bs ON bs.Id = bsd.IdSequenseTreasuryC 
				JOIN Common.Sequense cs ON cs.Id = bsd.IdSequense 
				WHERE bs.IdForm = @IdForm

				IF (@idSequenceDetail IS NULL)
				BEGIN
					SELECT	'999' AS CodeMessage,
							REPLACE(@Message_Output, '{0}', 'Cargue de extractos bancarios') Message,
							0 AS Id,
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END
			
				--Se inserta la cabecera
				INSERT INTO [Treasury].[UploadBankStatements]
				(
					[Code],[EntityBankAccountId],[Year],[Month],[InitialBalance],[EndingBalance],[Status],
					[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
				)
				SELECT @Code,@EntityBankAccountId,@Year,@Month,@InitialBalance,@EndingBalance,@Status,
					   @CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

				--Obtengo el id de la cabecera
				SET @Id = SCOPE_IDENTITY()						
			END			
			ELSE --Si se esta actualizando		
			BEGIN		
				UPDATE [Treasury].[UploadBankStatements]
					SET 							
						[Year]=@Year,
						[Month]=@Month,
						[InitialBalance] = @InitialBalance,
						[EndingBalance] = @EndingBalance,
						[Status] = @Status,
						[EntityBankAccountId] =  @EntityBankAccountId,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] =  [Common].[GETDATE]()
				WHERE Id = @Id					
			END
			

			/*********************************** INSERTAR / ACTUALIZAR DETALLE ***********************************/

			INSERT INTO Treasury.UploadBankStatementsDetail
			(
				UploadBankStatementsId,TransactionDate,ConsecutiveBank,TransactionCode,DescriptionTransaction,
				ValueDebit,ValueCredit,BankCheck,PaymentReferenceOne,PaymentReferenceTwo, DocumentType
			)
			SELECT	@Id, d.TransactionDate, d.ConsecutiveBank, d.TransactionCode, d.DescriptionTransaction,
					d.ValueDebit,d.ValueCredit, d.BankCheck,d.PaymentReferenceOne,d.PaymentReferenceTwo, d.DocumentType
			FROM @Detail d
			WHERE d.ChangeTracker = 'Added'

			UPDATE ubsd
				SET ubsd.TransactionDate= d.TransactionDate,
					ubsd.ConsecutiveBank=d.ConsecutiveBank,
					ubsd.TransactionCode=d.TransactionCode,
					ubsd.DescriptionTransaction=d.DescriptionTransaction,
					ubsd.ValueDebit=d.ValueDebit,
					ubsd.ValueCredit=d.ValueCredit,
					ubsd.BankCheck= d.BankCheck,
					ubsd.PaymentReferenceOne= d.PaymentReferenceOne,
					ubsd.PaymentReferenceTwo=d.PaymentReferenceTwo,
					ubsd.DocumentType=d.DocumentType
			FROM @Detail d
			JOIN Treasury.UploadBankStatementsDetail ubsd ON d.Id = ubsd.Id				
			WHERE ubsd.UploadBankStatementsId = @Id AND d.ChangeTracker = 'Modified'		
		END

		SELECT	'0' AS CodeMessage,
				CASE @Status
				   WHEN 2 THEN CONCAT('Se confirmo el cargue de extractos bancarios con el consecutivo ', @Code)
				   WHEN 3 THEN CONCAT('Se anulo el cargue de extractos bancarios con el consecutivo ', @Code)
				   ELSE 
						IIF(@ChangeTracker = 'Modified', CONCAT('Se actualizó el convenio con el consecutivo ', @Code), CONCAT('Se guardó el convenio con el consecutivo ', @Code))
			    END + ISNULL(@Message, '') AS Message,
				@Id UploadBankStatementsId,
				@Code, 
				cast(1 AS TINYINT) AS [Status]

	END TRY
	
	BEGIN CATCH	
		SELECT @CodeResult = 999, 
			   @MessageResult = CONCAT('SP_SaveUploadBankStatements_Output: Linea: ', ERROR_LINE(), ' - ', ERROR_MESSAGE())	
	END CATCH
END