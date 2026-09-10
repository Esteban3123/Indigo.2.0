

-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-05-27
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una conciliación Bancaria
-- =====================================================
CREATE PROCEDURE [Treasury].[SP_SaveBankReconciliation_Output]
    @BankReconciliationXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(200) OUTPUT,
	@IdResult INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@Status TINYINT,
			@EntityBankAccountId INT,
			@DocumentDate DATE,
			@EntityBankAccountValue DECIMAL(18, 2),
			@ExtractValue DECIMAL(18, 2),
			@ExtractDocument VARCHAR(50),
			------------------------------
			@IdForm INT = 1956,
			------------------------------
			@Message VARCHAR(200),
			------------------------------							
			@Code_Output INT,
			@Message_Output VARCHAR(200)
			------------------------------
			
	--Tabla temporal de los detalles de los libros
	DECLARE @Detail TABLE
	(		
		Id INT,
		BankReconciliationId INT NOT NULL,
		DocumentType TINYINT NOT NULL,
		Nature TINYINT NOT NULL,
		Value DECIMAL(18, 2) NOT NULL,
		EntityId INT NOT NULL,
		EntityCode VARCHAR(20) NOT NULL,
		EntityName VARCHAR(250) NOT NULL,
		Reconciled BIT NOT NULL,
		ChangeTracker VARCHAR(30)	
	)

	--Tabla temporal de los detalles de los extractos
	DECLARE @ExtractDetail TABLE
	(		
		Id INT,
		BankReconciliationId INT  NOT NULL,
		DocumentType TINYINT NOT NULL,
		DocumentDate  DATETIME NOT NULL,
		Description VARCHAR(250) NOT NULL,
		DocumentNumber VARCHAR(50),
		Nature TINYINT NOT NULL,
		Value DECIMAL(18, 2) NOT NULL,
		ChangeTracker VARCHAR(30)	
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','INT'),
				@Code = t.x.value('Code[1]','VARCHAR(20)'),					
				@EntityBankAccountId = t.x.value('EntityBankAccountId[1]','INT'),
				@DocumentDate =  t.x.value('DocumentDate[1]','DATE'),
				@EntityBankAccountValue = t.x.value('EntityBankAccountValue[1]','DECIMAL(18, 2)'),
				@ExtractValue = t.x.value('ExtractValue[1]','DECIMAL(18, 2)'),
				@Status = t.x.value('Status[1]','TINYINT')
				-------------------------------------------------------				
		FROM @BankReconciliationXml.nodes('/BankReconciliation') t(x)
								
		/************************************* VALIDACIONES GENERALES ************************************/	

		IF EXISTS (SELECT 1 FROM Treasury.BankReconciliation WITH	(NOLOCK) WHERE Id = @Id AND Status <> 1)
		BEGIN
			SELECT	'999' AS CodeMessage,
					'La conciliación bancaria se encuentra: ' + IIF(Status = 2, 'Confirmado', 'Anulado') Message,
					0 AS BankReconciliationId,
					CAST(3 AS TINYINT) AS [Status]
			FROM Treasury.BankReconciliation 
			WHERE Id = @Id
			RETURN
		END

		/**********************************************************************************/	

		IF @Status = 3
		BEGIN
			UPDATE Treasury.BankReconciliation
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END	
		BEGIN			
			/***************************************** VALIDACIONES CABECERA *****************************************/				

			-- Valido que se haya ingresado una cuenta bancaria
			IF ISNULL(@EntityBankAccountId, 0) = 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'Se debe ingresar una cuenta bancaria.')
				RETURN
			END	

			-- Valido que no exista diferencia para conciliar
			IF (@EntityBankAccountValue - @ExtractValue) > 0 AND (@EntityBankAccountValue - @ExtractValue) < 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'No debe quedar ninguna diferencia en la conciliación.')
				RETURN
			END	

			/************************************ VALIDACIONES MOVIMIENTOS EN LIBROS ************************************/		
			
			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail
				SELECT	t.x.value('Id[1]','INT'),
						t.x.value('BankReconciliationId[1]','INT'),
						t.x.value('DocumentType[1]','TINYINT'),
						t.x.value('Nature[1]','TINYINT'),
						t.x.value('Value[1]','DECIMAL(18, 2)'),
						t.x.value('EntityId[1]','INT'),
						t.x.value('EntityCode[1]','VARCHAR(20)'),
						t.x.value('EntityName[1]',' VARCHAR(250)'),
						t.x.value('Reconciled[1]','BIT'),										
						t.x.value('ChangeTracker[1]','VARCHAR(30)')
				FROM @BankReconciliationXml.nodes('/BankReconciliation/BankReconciliationDetail') t(x)
						
			--se eliminan los detalles marcados para su eliminación
			DELETE brd
			FROM Treasury.BankReconciliationDetail brd
			JOIN @Detail d ON brd.Id = d.Id
			WHERE @Id = brd.BankReconciliationId AND d.ChangeTracker = 'Deleted'

			DELETE d FROM @Detail d WHERE d.ChangeTracker = 'Deleted'
			
			--Se obtiene los detalles que ya estan y no han sido actualizados 
			INSERT INTO @Detail
				SELECT	brd.Id,
						brd.BankReconciliationId,
						brd.DocumentType,
						brd.Nature,
						brd.Value,
						brd.EntityId,
						brd.EntityCode,
						brd.EntityName,
						brd.Reconciled,
						'' ChangeTracker
				FROM Treasury.BankReconciliationDetail brd
				LEFT JOIN @Detail d ON brd.Id = d.Id
				WHERE brd.BankReconciliationId = @Id AND d.Id IS NULL
		

			/***************************************  VALIDACIONES MOVIMIENTOS EN LIBROS ***************************************/

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail d
				LEFT JOIN Treasury.BankReconciliationDetail brd WITH (NOLOCK) ON d.Id = brd.Id
				WHERE d.ChangeTracker <> 'Added' AND (@Id <> ISNULL(brd.BankReconciliationId, 0))				
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Los movimientos en libros han sido alterados.'
				RETURN
			END
			
			/************************************ VALIDACIONES EN EXTRACTOS BANCARIOS ************************************/		
			
			SELECT @ExtractDocument = ed.DocumentNumber FROM @ExtractDetail ed WHERE ed.BankReconciliationId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @ExtractDetail
				SELECT	t.x.value('Id[1]','INT'),
						t.x.value('BankReconciliationId[1]','INT'),
						t.x.value('DocumentType[1]','TINYINT'),
						t.x.value('DocumentDate[1]','DATETIME'),
						t.x.value('Description[1]','VARCHAR(250)'),
						t.x.value('DocumentNumber[1]','VARCHAR(50)'),
						t.x.value('Nature[1]','TINYINT'),
						t.x.value('Value[1]','DECIMAL(18, 2)'),						
						t.x.value('ChangeTracker[1]','VARCHAR(30)')
				FROM @BankReconciliationXml.nodes('/BankReconciliation/BankReconciliationExtractDetail') t(x)
						
			--se eliminan los detalles marcados para su eliminación
			DELETE bred
			FROM Treasury.BankReconciliationExtractDetail bred
			JOIN @ExtractDetail ed ON bred.Id = ed.Id
			WHERE @Id = bred.BankReconciliationId AND ed.ChangeTracker = 'Deleted'

			DELETE ed FROM @ExtractDetail ed WHERE ed.ChangeTracker = 'Deleted'
			
			--Se obtiene los detalles que ya estan y no han sido actualizados 
			INSERT INTO @ExtractDetail
				SELECT	bred.Id,
						bred.BankReconciliationId,
						bred.DocumentType,
						bred.DocumentDate,
						bred.Description,
						bred.DocumentNumber,
						bred.Nature,
						bred.Value,				
						'' ChangeTracker
				FROM Treasury.BankReconciliationExtractDetail bred
				LEFT JOIN @ExtractDetail ed ON bred.Id = ed.Id
				WHERE bred.BankReconciliationId = @Id AND ed.Id IS NULL
		

			/***************************************  VALIDACIONES EN EXTRACTOS ***************************************/

	IF @ExtractDocument <> '' 
	BEGIN
			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM @ExtractDetail ed
				LEFT JOIN Treasury.BankReconciliationExtractDetail berd WITH (NOLOCK) ON ed.Id = berd.Id
				WHERE ed.ChangeTracker <> 'Added' AND (@Id <> ISNULL(berd.BankReconciliationId, 0))				
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Los movimientos en libros han sido alterados.'
				RETURN
			END

			-- Valido que se haya ingresado un tipo de documento
			IF NOT EXISTS (SELECT ed.DocumentType FROM @ExtractDetail ed WHERE ed.BankReconciliationId = @Id ) 
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'Debe seleccionar un tipo de documento.')
				RETURN
			END	

			-- Valido que se haya ingresado la fecha del registro
			IF NOT EXISTS (SELECT ed.DocumentDate FROM @ExtractDetail ed WHERE ed.BankReconciliationId = @Id ) 
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'Debe indicar la fecha del registro.')
				RETURN
			END	

			-- Valido que La fecha del registro no puede ser mayor a la fecha del documento
			IF EXISTS (SELECT 1 FROM @ExtractDetail ed WHERE ed.BankReconciliationId = @Id AND ed.DocumentDate > @DocumentDate ) 
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'La fecha del registro ' + @ExtractDocument + ' no puede ser mayor a la fecha del documento. ')
				RETURN
			END	

			-- Valido que se haya ingresado descripción del registro
			IF NOT EXISTS (SELECT ed.Description FROM @ExtractDetail ed WHERE ed.BankReconciliationId = @Id ) 
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'Debe indicar una descripción del registro.')
				RETURN
			END	

			-- Valido que se haya ingresado naturaleza
			IF NOT EXISTS (SELECT ed.Nature FROM @ExtractDetail ed WHERE ed.BankReconciliationId = @Id ) 
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message, 'Debe seleccionar una naturaleza.')
				RETURN
			END	

		END
			/**********************************  INSERTAR / ACTUALIZAR CABECERA **********************************/
							
			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF ISNULL(@Id, 0) = 0			
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica				
				DECLARE @idSequenceDetail INT, @pattern VARCHAR(300), @NextS INT

				SELECT @pattern = cs.Pattern,
						@NextS = bsd.[Next], 
						@idSequenceDetail = bsd.Id  
				FROM Treasury.TreasurySequenceDetail bsd 
				JOIN Treasury.TreasurySequence bs on bs.Id = bsd.IdSequenseTreasuryC 
				JOIN Common.Sequense cs on cs.Id = bsd.IdSequense 
				WHERE bs.IdForm = @IdForm

				IF (@idSequenceDetail IS NULL)
				BEGIN
					SELECT	'999' AS CodeMessage,
							REPLACE(@Message_Output, '{0}', 'Conciliación Bancaria') Message,
							0 AS Id,
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END
			
				SELECT @Code = dbo.GetSequence('',@pattern,@NextS)
				UPDATE Treasury.TreasurySequenceDetail set [Next] += 1 
				WHERE Id = @idSequenceDetail

				--Se inserta la cabecera
				INSERT INTO [Treasury].[BankReconciliation]
				(
					[Code],[EntityBankAccountId],[DocumentDate],[EntityBankAccountValue],[ExtractValue],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate]
				)
				SELECT @Code,@EntityBankAccountId,@DocumentDate,@EntityBankAccountValue,@ExtractValue,
					  @Status,@CodeUser,[Common].[GETDATE](),NULL,NULL

				--Obtengo el id de la cabecera
				SET @Id = SCOPE_IDENTITY()						
			END			
			ELSE --Si se esta actualizando		
			BEGIN		
				UPDATE [Treasury].[BankReconciliation]
					SET [EntityBankAccountId] =  @EntityBankAccountId,
						[DocumentDate] = @DocumentDate,
						[EntityBankAccountValue] = @EntityBankAccountValue,
						[ExtractValue] = @ExtractValue,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE]()						
				WHERE Id = @Id					
			END
			

			/*********************************** INSERTAR / ACTUALIZAR DETALLE LIBROS ***********************************/

			INSERT INTO Treasury.BankReconciliationDetail
			(
				BankReconciliationId,DocumentType,Nature,Value,EntityId,EntityCode,EntityName,Reconciled
			)
			SELECT	@Id,d.DocumentType,d.Nature,d.Value,d.EntityId,d.EntityCode,d.EntityName,d.Reconciled
			FROM @Detail d WHERE d.ChangeTracker = 'Added'

			UPDATE brd
				SET brd.DocumentType= d.DocumentType,					
					brd.Nature=d.Nature,
					brd.Value=d.Value,
					brd.EntityId=d.EntityId,
					brd.EntityCode=d.EntityCode,
					brd.EntityName=d.EntityName,
					brd.Reconciled=d.Reconciled
			FROM @Detail d
			JOIN Treasury.BankReconciliationDetail brd WITH(NOLOCK) ON d.Id = brd.Id				
			WHERE brd.BankReconciliationId = @Id AND d.ChangeTracker = 'Modified'		
		
		
			/*********************************** INSERTAR / ACTUALIZAR DETALLE EXTRACTO ***********************************/

			INSERT INTO Treasury.BankReconciliationExtractDetail
			(
				BankReconciliationId,DocumentType,DocumentDate,Description,DocumentNumber,Nature,Value
			)
			SELECT	@Id,d.DocumentType,d.DocumentDate,d.Description,d.DocumentNumber,d.Nature,d.Value
			FROM @ExtractDetail d WHERE d.ChangeTracker = 'Added'

			UPDATE berd
				SET berd.DocumentType= ed.DocumentType,
					berd.DocumentDate=ed.DocumentDate,
					berd.Description=ed.Description,
					berd.DocumentNumber=ed.DocumentNumber,
					berd.Nature=ed.Nature,
					berd.Value=ed.Value
			FROM @ExtractDetail ed
			JOIN Treasury.BankReconciliationExtractDetail berd WITH(NOLOCK) ON ed.Id = berd.Id				
			WHERE berd.BankReconciliationId = @Id AND ed.ChangeTracker = 'Modified'		
		END

		SELECT	'0' AS CodeMessage,
				CASE @Status
				   WHEN 2 THEN CONCAT('Se confirmo la Conciliación Bancaria con el Consecutivo ', @Code)
				   WHEN 3 THEN CONCAT('Se anulo la Conciliación Bancaria con el Consecutivo ', @Code)
				   ELSE CONCAT('Se guardó la Conciliación Bancaria con el Consecutivo ', @Code)
			   END + ISNULL(@Message, '') AS Message,
				@Id BankReconciliationId,
				cast(1 as TINYINT) as [Status]
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = 'SP_SaveBankReconciliation_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))			
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar, confirmar o anular una conciliación bancaria, recibiendo los datos de cabecera y detalle en formato XML. Gestiona los registros en las tablas BankReconciliation (cabecera de la conciliación) y BankReconciliationDetail (movimientos individuales como cheques, depósitos o transferencias), aplicando inserciones, actualizaciones y eliminaciones según el rastreador de cambios de cada línea. Valida que la conciliación no esté ya confirmada o anulada, que exista una cuenta bancaria asociada y que no queden diferencias pendientes entre el saldo en libros y el extracto bancario. Registra el usuario y la fecha de modificación en cada operación, devolviendo códigos y mensajes de resultado para informar al sistema llamador sobre el éxito o los errores encontrados.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBankReconciliation_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBankReconciliation_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste, actualiza, confirma o anula una conciliación bancaria junto con sus detalles de libros y extractos a partir de un XML, generando el consecutivo cuando es nueva.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La conciliación identificada por @Id no debe estar en estado distinto de 1 (Pendiente); si está Confirmada (2) o Anulada (3) se rechaza la operación.; Debe enviarse una cuenta bancaria (@EntityBankAccountId distinto de 0/NULL).; El valor en libros y el valor del extracto deben ser iguales (sin diferencia para conciliar).; Los detalles existentes en BD que no vienen marcados como ''Added'' no pueden tener un BankReconciliationId distinto al actual (no haber sido alterados).; Si hay documento de extracto previo, cada partida del extracto debe traer DocumentType, DocumentDate, Description y Nature; y la fecha del registro no puede ser mayor a la fecha del documento de la cabecera.; Para insertar una conciliación nueva debe existir una secuencia configurada en Treasury.TreasurySequence para IdForm = 1956.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Treasury.BankReconciliation: Cuando @Status = 3 se anula la conciliación: actualiza Status, ModificationUser y ModificationDate sobre el registro con Id = @Id.; [INSERT] Treasury.BankReconciliation: Cuando @Id es NULL o 0 inserta una nueva cabecera con el Code generado por dbo.GetSequence a partir del patrón de la secuencia del IdForm 1956.; [UPDATE] Treasury.BankReconciliation: Cuando @Id existe (>0) y @Status <> 3 actualiza la cabecera (cuenta, fecha, valores y status) con el usuario/fecha de modificación.; [UPDATE] Treasury.TreasurySequenceDetail: Tras generar un nuevo consecutivo incrementa Next en 1 sobre el detalle de secuencia usado (Id = @idSequenceDetail).; [DELETE] Treasury.BankReconciliationDetail: Elimina los detalles de libros cuyo ChangeTracker = ''Deleted'' y pertenecen a la conciliación @Id.; [INSERT] Treasury.BankReconciliationDetail: Inserta detalles de libros con ChangeTracker = ''Added'' asociados al @Id de la conciliación.; [UPDATE] Treasury.BankReconciliationDetail: Actualiza los detalles de libros con ChangeTracker = ''Modified'' que pertenecen a la conciliación @Id.; [DELETE] Treasury.BankReconciliationExtractDetail: Elimina los detalles de extracto con ChangeTracker = ''Deleted'' que pertenecen a la conciliación @Id.; [INSERT] Treasury.BankReconciliationExtractDetail: Inserta detalles de extracto con ChangeTracker = ''Added'' asociados al @Id.; [UPDATE] Treasury.BankReconciliationExtractDetail: Actualiza los detalles de extracto con ChangeTracker = ''Modified'' que pertenecen a la conciliación @Id.; [RETURN_RESULT] @resultset: Devuelve un resultset con CodeMessage=''0'' y mensaje según @Status: 2=''Se confirmó...'', 3=''Se anuló...'', otros=''Se guardó...'' incluyendo el consecutivo @Code.; [RETURN_RESULT] @resultset: Cuando la conciliación ya está confirmada (Status=2) o anulada (Status=3 distinto de 1) devuelve CodeMessage=''999'' y Status=3 sin persistir cambios.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe en Treasury.BankReconciliation un registro con Id=@Id y Status<>1 → Devuelve mensaje ''999'' indicando que está Confirmado o Anulado y termina.; si @Status = 3 (anulación) → Actualiza Status, ModificationUser y ModificationDate sobre la cabecera.; si ISNULL(@Id,0) = 0 (alta nueva) → Obtiene el patrón y siguiente valor desde TreasurySequenceDetail/TreasurySequence/Common.Sequense para IdForm=1956, genera @Code con dbo.GetSequence, incrementa Next y hace INSERT de cabecera. else Hace UPDATE de la cabecera existente.; si No se encuentra detalle de secuencia (@idSequenceDetail IS NULL) durante alta nueva → Devuelve resultset con CodeMessage=''999'' y mensaje de error de secuencia para ''Conciliación Bancaria'' y termina.; si @ExtractDocument <> '''' (existen movimientos de extracto previos) → Aplica el bloque de validaciones de extracto (alteración, tipo de documento, fecha, descripción, naturaleza, fecha <= fecha de documento).; si @Status = 2 al insertar/actualizar → Asigna ConfirmationUser=@CodeUser y ConfirmationDate=GETDATE (la cabecera se marca como confirmada). else ConfirmationUser y ConfirmationDate quedan NULL.; si Detalles existentes en BD no enviados en el XML → Se reinsertan en la tabla temporal con ChangeTracker='''' para conservarlos sin cambios.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation_Output';
-- GO
