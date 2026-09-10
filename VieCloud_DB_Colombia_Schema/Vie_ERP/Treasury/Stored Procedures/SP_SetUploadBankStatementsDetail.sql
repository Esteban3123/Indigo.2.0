

-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-04-14
-- Description:	Procedimiento que se encarga de copiar y pegar o la importacion de archivo para el extracto bancario
-- =====================================================
CREATE PROCEDURE [Treasury].[SP_SetUploadBankStatementsDetail]
    @UploadBankStatementsDetail AS XML	
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @UploadBankStatementsId int,			
			@TransactionDateVar DATE,
			@TransactionCode VARCHAR(60),			
			@DateTransaction VARCHAR(20),
			@BankCheck BIGINT
	
	--Tabla temporal de los detalles para el return
	DECLARE @Detail TABLE
	(			
		TransactionDateString VARCHAR(200),		
		ConsecutiveBank VARCHAR(60),
		TransactionCode VARCHAR(60),
		DescriptionTransaction VARCHAR(60),
		ValueDebitString VARCHAR(60),
		ValueCreditString VARCHAR(60),
		BankCheckString VARCHAR(60),
		PaymentReferenceOne VARCHAR(60),
		PaymentReferenceTwo VARCHAR(60),
		Status tinyint,
		Message varchar(200) NULL,
		----------------------------------------
		TransactionDate DATETIME,
		ValueDebit DECIMAL(18,2),
		ValueCredit DECIMAL(18,2),
		Movement DECIMAL(18),
		BankCheck BIGINT --BIGINT
	)

	BEGIN TRY
	
		--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail
			(
				TransactionDateString, ConsecutiveBank, TransactionCode, DescriptionTransaction, ValueDebitString, ValueCreditString,
				BankCheckString, PaymentReferenceOne, PaymentReferenceTwo, Status, Message
			)
				SELECT  t.x.value('TransactionDate[1]','VARCHAR(200)'),
						t.x.value('ConsecutiveBank[1]','VARCHAR(60)'),
						t.x.value('TransactionCode[1]','VARCHAR(60)'),
						t.x.value('DescriptionTransaction[1]','VARCHAR(60)'),
						t.x.value('ValueDebit[1]','varchar(20)'),
						t.x.value('ValueCredit[1]','varchar(20)'),	
						t.x.value('BankCheck[1]','varchar(20)'),					
						t.x.value('PaymentReferenceOne[1]','VARCHAR(60)'),
						t.x.value('PaymentReferenceTwo[1]','VARCHAR(60)'),
						t.x.value('Status[1]','TINYINT'),
						t.x.value('Message[1]','VARCHAR(200)')
				FROM @UploadBankStatementsDetail.nodes('/UploadBankStatementsDetail') t(x)

			------------------------------------------------------------------------------------------------------------------------------------------------------------------
			--Obtengo para el respectivo uso de actualización
			SELECT @TransactionDateVar  = d.TransactionDate, 
					@TransactionCode =  d.TransactionCode,
					@DateTransaction = d.TransactionDateString,
					@BankCheck = d.BankCheckString				
				FROM @Detail d WHERE d.TransactionCode = @TransactionCode

					
			/********************************************************** VALIDACIONES DATOS VACIOS *************************************************************************/
	
			--valido que se ingreso una fecha de transacción			
			IF EXISTS (SELECT D.TransactionDateString FROM @Detail d WHERE  TRY_PARSE(d.TransactionDateString AS DATE USING 'es-co') IS NULL ) 
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = CONCAT('La Fecha de transacción esta vacia, en el registro con transacción : ',D.TransactionCode),
						D.TransactionDateString =GETDATE(),
						@TransactionCode = d.TransactionCode
				FROM @Detail D						
				WHERE d.TransactionCode  = @TransactionCode 	
			END

			--valido que se ingrese un código de transacción
			IF EXISTS (SELECT D.TransactionCode FROM @Detail d WHERE d.TransactionCode = @TransactionCode AND @TransactionCode IS NULL OR @TransactionCode = '' )
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = CONCAT('El código de transacción esta vacio, en el registro con transacción : ',D.TransactionCode),
						D.TransactionDateString =GETDATE()
				FROM @Detail D						
				WHERE d.TransactionCode  = @TransactionCode 	
			END

			------------------------------------------------------------------------------------------------------------------------------------------------------------------

			/********************************************************** VALIDACIONES FORMATOS *************************************************************************/
			-- Validar que el valor ingresado sea una fecha
			UPDATE d
				SET d.Status = 2,
					d.Message = CONCAT('La Fecha de transacción no tiene un formato válido, en el registro con transacción : ',D.TransactionCode)
			FROM @Detail D
			WHERE TRY_PARSE(d.TransactionDateString AS DATE USING 'es-co') IS NULL

			-- Validar que el valor ingresado sea un valor debito
			UPDATE d
				SET d.Status = 2,
					d.Message = CONCAT('El valor debito no tiene un formato válido, en el registro con transacción : ',D.TransactionCode)
			FROM @Detail D
			WHERE ISNUMERIC(D.ValueDebitString) = 0
											
			-- Validar que el valor ingresado sea un valor credito
			UPDATE d
				SET d.Status = 2,									
					d.Message = CONCAT('El valor credito no tiene un formato válido, en el registro con transacción : ',D.TransactionCode)
			FROM @Detail D
			WHERE ISNUMERIC(D.ValueCreditString) = 0

			------------------------------------------------------------------------------------------------------------------------------------------------------------------

			/********************************************************** CONVERSIONES *************************************************************************/
			IF NOT EXISTS (	SELECT d.Status FROM @Detail d WHERE d.Status = 2 )
			BEGIN			
				UPDATE d						
						SET d.Status = 1,
							d.TransactionDate = TRY_PARSE(d.TransactionDateString AS DATE USING 'es-co')
					FROM @Detail d					
		
					-- Actualizamos el valor debito	
					UPDATE d
							SET d.Status = 1,
							d.ValueDebit = CAST(REPLACE(d.ValueDebitString,',','.') AS DECIMAL(20, 2))
					FROM @Detail d				
			
					-- Actualizamos el valor credito	
					UPDATE d
							SET d.Status = 1,
							d.ValueCredit = CAST(REPLACE(d.ValueCreditString,',','.') AS DECIMAL(20, 2))
					FROM @Detail d				

					--Actualizamos el cheque
					UPDATE d
						SET d.Status = 1,
						d.BankCheck = CAST(d.BankCheckString AS BIGINT)
					FROM @Detail d			
			END

			---Valido los datos duplicados
			IF EXISTS
			(
				SELECT 1 
				FROM @Detail d
				GROUP BY d.TransactionCode
				HAVING COUNT(1) > 1
			)
			BEGIN
				UPDATE d
				SET d.Status = 2,									
					d.Message = CONCAT('Existen datos duplicados : ',D.TransactionCode)
				FROM @Detail D				
			END	

			---------------------------------------------------------------------------------------------------------------------------------------------------------------- 			
			SELECT	TransactionDate,
					ConsecutiveBank,
					TransactionCode,
					DescriptionTransaction,
					ABS(ValueDebit) as ValueDebit,
					ABS(ValueCredit) as ValueCredit,
					BankCheck,
					PaymentReferenceOne,
					PaymentReferenceTwo,					
					Status,
					Message
			FROM @Detail d

	END TRY
	BEGIN CATCH
		  SELECT '0' ,0,0,2 As Status,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de tesorería que procesa la importación de extractos bancarios enviados en formato XML. Valida, convierte y registra cada movimiento bancario (débito, crédito, fecha de transacción, código de transacción, número de cheque y referencias de pago) detectando errores de formato o datos vacíos antes de confirmar el ingreso. Utiliza una tabla temporal interna para acumular el resultado de cada fila importada, marcando cada registro con un estado de éxito o error y un mensaje descriptivo, permitiendo así al usuario identificar exactamente cuáles movimientos del extracto fueron cargados correctamente y cuáles requieren corrección.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SetUploadBankStatementsDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SetUploadBankStatementsDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa un XML con detalles de extracto bancario, validando fechas, códigos, formatos numéricos y duplicados antes de retornar el conjunto enriquecido con estado y mensajes para su posterior carga.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SetUploadBankStatementsDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe seguir el esquema /UploadBankStatementsDetail con nodos TransactionDate, ConsecutiveBank, TransactionCode, DescriptionTransaction, ValueDebit, ValueCredit, BankCheck, PaymentReferenceOne, PaymentReferenceTwo, Status, Message.; Las fechas dentro de TransactionDate deben venir en formato cultura ''es-co'' para ser parseables con TRY_PARSE.; Los valores ValueDebit y ValueCredit deben ser numéricos (validados con ISNUMERIC) y usar coma como separador decimal (se reemplaza por punto).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SetUploadBankStatementsDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Status=1 indica registro válido y convertido; Status=2 indica registro con error y mensaje descriptivo asociado.; Los valores monetarios retornados siempre se entregan en valor absoluto (ABS) tanto para débito como para crédito.; Las conversiones de tipos solo se aplican cuando todos los registros pasan las validaciones previas.; Las fechas se interpretan siempre bajo cultura ''es-co''.; El procedimiento no persiste cambios en tablas reales; opera únicamente sobre una tabla variable y retorna el resultado.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SetUploadBankStatementsDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Extracto bancario; Detalle de transacción bancaria; Código de transacción; Valor débito; Valor crédito; Cheque bancario; Referencias de pago; Carga/importación de archivo bancario', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SetUploadBankStatementsDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] @Detail: Si TRY_PARSE(TransactionDateString AS DATE USING ''es-co'') IS NULL, marca Status=2 con mensaje ''La Fecha de transacción esta vacia/no tiene un formato válido'' y reemplaza TransactionDateString por GETDATE().; [UPDATE] @Detail: Si TransactionCode es NULL o vacío, marca Status=2 con mensaje ''El código de transacción esta vacio''.; [UPDATE] @Detail: Si ISNUMERIC(ValueDebitString)=0, marca Status=2 con mensaje ''El valor debito no tiene un formato válido''.; [UPDATE] @Detail: Si ISNUMERIC(ValueCreditString)=0, marca Status=2 con mensaje ''El valor credito no tiene un formato válido''.; [UPDATE] @Detail: Si ningún registro tiene Status=2, convierte TransactionDateString a DATE, ValueDebitString y ValueCreditString (reemplazando '','' por ''.'') a DECIMAL(20,2), BankCheckString a BIGINT, y marca Status=1.; [UPDATE] @Detail: Si existen TransactionCode duplicados (COUNT(1)>1 por TransactionCode), marca todos los registros con Status=2 y mensaje ''Existen datos duplicados''.; [RETURN_RESULT] @Detail: Retorna el detalle con TransactionDate, ConsecutiveBank, TransactionCode, DescriptionTransaction, ABS(ValueDebit), ABS(ValueCredit), BankCheck, PaymentReferenceOne, PaymentReferenceTwo, Status, Message.; [RETURN_RESULT] @Detail: Si ocurre excepción en el TRY, retorna fila con Status=2 y Message conteniendo ERROR_MESSAGE() y línea de error.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SetUploadBankStatementsDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe algún registro con TRY_PARSE(TransactionDateString AS DATE USING ''es-co'') IS NULL → Marca Status=2 indicando fecha vacía/inválida y sustituye la fecha por GETDATE(). else Continúa con las siguientes validaciones.; si TransactionCode es NULL o cadena vacía → Marca Status=2 con mensaje de código de transacción vacío.; si NOT EXISTS registros con Status=2 tras validaciones → Ejecuta conversiones de tipos (fecha, débito, crédito, cheque) y marca Status=1. else Omite las conversiones manteniendo los registros marcados como inválidos.; si Existen TransactionCode duplicados (HAVING COUNT(1)>1) → Marca Status=2 con mensaje de datos duplicados.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SetUploadBankStatementsDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SetUploadBankStatementsDetail';
-- GO
