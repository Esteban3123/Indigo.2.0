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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de tesorería que permite crear, actualizar, confirmar o anular un cargue de extracto bancario, procesando tanto la cabecera (cuenta bancaria, período mes/año, saldos inicial y final) como el detalle de transacciones individuales (débitos, créditos, referencias de pago) enviados en formato XML. Gestiona el ciclo de vida completo del extracto bancario: valida que no esté anulado antes de modificarlo, impide anular extractos ya vinculados a una conciliación bancaria automática, y sincroniza altas, modificaciones y eliminaciones de líneas de movimiento en las tablas Treasury.UploadBankStatements y Treasury.UploadBankStatementsDetail. Retorna como parámetros de salida el identificador del cargue, el código de resultado y el mensaje de éxito o error, siendo el punto central del proceso de carga y conciliación bancaria en el módulo de Tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveUploadBankStatements_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveUploadBankStatements_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guardar, actualizar, confirmar o anular un cargue de extractos bancarios (cabecera y detalle), generando consecutivo según configuración de secuencia y validando integridad frente a conciliaciones automáticas.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /UploadBankStatements con los datos de cabecera y opcionalmente /UploadBankStatementsDetail.; Para guardar/confirmar (Status<>3) debe informarse EntityBankAccountId distinto de 0.; Debe existir configuración en Treasury.TreasurySequence para IdForm=2235 y, si el Scope=''OU'', un Treasury.TreasurySequenceDetail asociado a la unidad operativa indicada.; El cargue no debe estar previamente anulado (Status=3) en Treasury.UploadBankStatements.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El consecutivo (Code) se genera una sola vez por cargue: si ya viene informado se respeta, si no se genera y se incrementa NEXT en TreasurySequenceDetail.; Un cargue anulado (Status=3) no se vuelve a procesar.; No se permite anular un cargue que esté vinculado a una conciliación bancaria automática.; Un detalle solo se inserta si ChangeTracker=''Added'', se actualiza si es ''Modified'' y se elimina si es ''Deleted''.; Los detalles existentes no enviados en el XML se conservan (se reinsertan en la tabla temporal con ChangeTracker='''').; El IdForm utilizado para resolver la secuencia es siempre 2235.; El alcance de la secuencia se determina por Treasury.TreasurySequence.Scope: ''O'' = organización (sin filtrar unidad operativa), de lo contrario se filtra por OperatingUnitId.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Treasury.UploadBankStatements: Cuando @Status=3 y el cargue no está asociado a ninguna conciliación bancaria automática, se actualiza Status, ModificationUser y ModificationDate (anulación).; [INSERT] Treasury.UploadBankStatements: Cuando @Id=0 y el Status no es 3, se inserta una nueva cabecera con el Code generado por secuencia; si @Status=2 además se setea ConfirmationUser/ConfirmationDate=GETDATE.; [UPDATE] Treasury.UploadBankStatements: Cuando @Id>0 y @Status<>3, se actualizan año, mes, saldos, estado, cuenta bancaria, usuarios y fechas; si Status=2 se confirma estampando ConfirmationUser/ConfirmationDate.; [DELETE] Treasury.UploadBankStatementsDetail: Se eliminan los detalles cuyo ChangeTracker=''Deleted'' coincidan en Id y pertenezcan al cargue @Id.; [INSERT] Treasury.UploadBankStatementsDetail: Para cada detalle del XML con ChangeTracker=''Added'' se inserta una nueva línea asociada a @Id.; [UPDATE] Treasury.UploadBankStatementsDetail: Para cada detalle con ChangeTracker=''Modified'' se actualizan los campos de la transacción (fecha, consecutivo, código, valores débito/crédito, cheque, referencias, tipo documento).; [UPDATE] Treasury.TreasurySequenceDetail: Cuando se genera un nuevo Code (Code vacío o nulo), se incrementa NEXT en 1 sobre el detalle de secuencia utilizado.; [RETURN_RESULT] @Output: Devuelve CodeMessage=''999'' con mensaje y Status=3 cuando el cargue ya está anulado o cuando está asociado a conciliación automática y se intenta anular; CodeMessage=''0'' con mensaje contextual (guardó/actualizó/confirmó/anuló) cuando la operación es exitosa.; [RAISERROR] @Output: En caso de excepción en el TRY, retorna CodeResult=999 con el mensaje ''SP_SaveUploadBankStatements_Output: Linea: <ERROR_LINE> - <ERROR_MESSAGE>''.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El cargue (Id=@Id) ya tiene Status=3 en Treasury.UploadBankStatements → Retorna mensaje ''999'' indicando que está anulado y termina sin modificar nada. else Continúa con la lógica de anulación o guardado/actualización.; si @Status=3 (anulación) → Verifica que el cargue NO esté ligado a Treasury.BankReconciliationAutomaticExtractDetail; si lo está retorna ''999'', si no, actualiza Status a 3. else Ejecuta validaciones de cabecera y procesa detalle (insert/update/delete).; si @EntityBankAccountId IS NULL o 0 (en flujo de guardado) → Retorna CodeResult=999 ''Se debe ingresar una cuenta bancaria.'' y termina.; si Existen detalles editados cuyo UploadBankStatementsId no coincide con @Id (registros alterados) → Retorna CodeResult=999 ''Los extractos bancarios han sido alterados.'' y termina.; si @Code es vacío o nulo → Genera el consecutivo consultando Treasury.TreasurySequence: si Scope=''O'' usa la secuencia global; si no, filtra por @OperatingUnitId. else Conserva el Code recibido.; si No se encuentra configuración de secuencia (idSequenceDetail IS NULL) → Retorna ''999'' ''Secuencia no encontrada para generar el paquete'' y termina.; si @Id IS NULL o 0 → Inserta nueva cabecera y obtiene Id con SCOPE_IDENTITY(). else Actualiza la cabecera existente.; si @Status=2 al guardar → Asigna ConfirmationUser=@CodeUser y ConfirmationDate=GETDATE() (confirmación). else ConfirmationUser/ConfirmationDate quedan en NULL.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveUploadBankStatements_Output';
-- GO
