



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