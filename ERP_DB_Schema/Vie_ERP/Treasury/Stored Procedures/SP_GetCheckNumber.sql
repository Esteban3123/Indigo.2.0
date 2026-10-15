CREATE PROCEDURE [Treasury].[SP_GetCheckNumber]
	-- Add the parameters for the stored procedure here
	@EntityBanckAccountId INT,
	@OperatingUnitId INT,
	@UserCode VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets FROM
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	BEGIN try
	
		DECLARE @CheckbookControl BIT
		DECLARE @currentCheckNumber BIGINT = 0
		DECLARE @EntitybankAccountCodeName VARCHAR(100)
		DECLARE @CheckbookId INT
		DECLARE @OutstandingCheck BIGINT
		DECLARE @CheckNumberToAssign BIGINT
		DECLARE @CheckNumberInitial BIGINT
		DECLARE @CheckNumberEnd BIGINT
		DECLARE @Mensaje VARCHAR(1000) = ''

		SELECT @CheckbookControl = CheckBookControl FROM Treasury.SettingsTreasury WHERE IdOperatingUnit = @OperatingUnitId
		IF @CheckbookControl IS NULL BEGIN
			SELECT '999' AS StatusResult, 'No se encuentra establecido un control de chequera para la unidad operativa seleccionada' AS MessageResult, NULL AS CheckNumberToAssign, NULL AS CheckbookId, NULL AS CheckNumberEnd, NULL AS CheckbookControl
			RETURN
		END

		SELECT @EntitybankAccountCodeName = CONCAT(Number, ' - ', b.Name) FROM Treasury.EntityBankAccounts eb JOIN Payroll.Bank b ON eb.IdBank = b.Id WHERE eb.Id = @EntityBanckAccountId
		
		--Todo esto se hace solo si hay control de tesoreria
		IF @CheckbookControl = 1 BEGIN			
			--Buscamos la chequera activa de la cuenta bancaria
			SELECT TOP 1 @currentCheckNumber = ISNULL(CurrentNumber, 0), @CheckbookId = ISNULL(Id, -1), @CheckNumberInitial = InitialNumber, @CheckNumberEnd = EndNumber FROM Treasury.Checkbooks WHERE IdEntityBanckAccount = @EntityBanckAccountId AND Status = 1
			IF ISNULL(@currentCheckNumber, 0) = 0 BEGIN
				SELECT '999' AS StatusResult, 'No se encontró una chequera activa para la cuenta bancaria ('+ @EntitybankAccountCodeName +')' AS MessageResult, NULL AS CheckNumberToAssign, NULL AS CheckbookId, NULL AS CheckNumberEnd, NULL AS CheckbookControl
				RETURN
			END

			--Buscamos un cheque para la chequera activa que no se encuentre bloqueado, que sea mayor o igual al cheque siguiente y que no supere al cheque máximo
			DECLARE @checkValid AS BIT = 0
			SET @CheckNumberToAssign = @currentCheckNumber - 1
			WHILE @checkValid = 0 BEGIN
				SET @CheckNumberToAssign += 1
				--Se valida que no este bloqueado
				IF ISNULL((SELECT COUNT(*) FROM Treasury.CheckBlock WHERE IdCheckbook = @CheckbookId AND CheckNumber = @CheckNumberToAssign), 0) = 0 BEGIN
					---Se valida que el numero de cheque no se encuentra ya en un comprobante de egreso
					IF (SELECT COUNT(*) FROM Treasury.VoucherTransaction WHERE CheckNumber = @CheckNumberToAssign AND IdEntityBankAccount = @EntityBanckAccountId) = 0 BEGIN						
						SET @checkValid = 1						
					END
				END
				IF @CheckNumberToAssign > @CheckNumberEnd BEGIN
					SET @checkValid = 2					
				END
			END

			--Buscamos el menor cheque pendiente y que no este asignado en un comprobante de egreso
			SELECT TOP 1 @OutstandingCheck = ISNULL(CheckNumber, -1) 
			FROM Treasury.OutstandingChecks oc
			WHERE IdCheckBook = @CheckbookId 
				AND CheckNumber >= @CheckNumberInitial
				AND NOT EXISTS
				(
					SELECT vt.Id
					FROM Treasury.VoucherTransaction vt
					WHERE vt.CheckNumber = oc.CheckNumber AND IdEntityBankAccount = @EntityBanckAccountId
				)
			ORDER BY CheckNumber

			--Si existen cheques pendientes
			IF ISNULL(@OutstandingCheck, -1) <> -1 BEGIN
				--Validamos que el número a asignar sea mayor o igual al numero pendiente
				IF @CheckNumberToAssign >= @OutstandingCheck BEGIN
					--Si es asi, usamos el cheque pendiente
					SET @CheckNumberToAssign = @OutstandingCheck
					DELETE Treasury.OutstandingChecks WHERE CheckNumber = @CheckNumberToAssign AND IdCheckBook = @CheckbookId
				END
			END
			ELSE IF @checkValid = 2 BEGIN
				SELECT '999' AS StatusResult, 'No hay cheques disponibles para la chequera activa' AS MessageResult, NULL AS CheckNumberToAssign, NULL AS CheckbookId, NULL AS CheckNumberEnd, NULL AS CheckbookControl
				RETURN
			END
					
			--Bloqueamos el cheque y lo retornamos al formulario
			INSERT INTO Treasury.CheckBlock (IdCheckbook, CheckNumber, CodUser) VALUES (@CheckbookId, @CheckNumberToAssign, @UserCode)
			SELECT '000' AS StatusResult, @Mensaje AS MessageResult, @CheckNumberToAssign AS CheckNumberToAssign, @CheckbookId AS CheckbookId, @CheckNumberEnd AS CheckNumberEnd, @CheckbookControl AS CheckbookControl
		END
		ELSE BEGIN
			PRINT @CheckNumberToAssign

			SELECT TOP 1 @currentCheckNumber = ISNULL(CurrentNumber, 0), @CheckbookId = ISNULL(Id, -1), @CheckNumberEnd = EndNumber 
			FROM Treasury.Checkbooks WHERE IdEntityBanckAccount = @EntityBanckAccountId AND Status = 1
			SELECT '000' AS StatusResult, '' AS MessageResult, CAST(ISNULL(@currentCheckNumber,1) AS BIGINT) AS CheckNumberToAssign, @CheckbookId AS CheckbookId, CAST(ISNULL(@CheckNumberEnd,999999999999999999) AS BIGINT) AS CheckNumberEnd, @CheckbookControl AS CheckbookControl
		END
	
	END TRY
	BEGIN CATCH
		SELECT '999' AS StatusResult, ERROR_MESSAGE() AS MessageResult, NULL AS CheckNumberToAssign, NULL AS CheckbookId, NULL AS CheckNumberEnd, NULL AS CheckbookControl
	END CATCH
END