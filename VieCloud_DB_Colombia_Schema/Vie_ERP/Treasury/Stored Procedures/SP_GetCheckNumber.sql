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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el próximo número de cheque disponible para emitir un pago desde una cuenta bancaria de la entidad, teniendo en cuenta la configuración de control de chequeras de la unidad operativa. Si el control de chequera está activo, busca en la chequera activa (Checkbooks) el siguiente número válido que no esté bloqueado (CheckBlock) ni ya usado en un comprobante de egreso (VoucherTransaction), dando prioridad a cheques pendientes en tránsito (OutstandingChecks); una vez identificado, lo bloquea preventivamente para evitar duplicados. Si no hay control de chequera, simplemente devuelve el número actual registrado en la chequera. Se usa en el proceso de emisión de cheques y órdenes de pago del módulo de Tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GetCheckNumber';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GetCheckNumber';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina y reserva el siguiente número de cheque disponible para una cuenta bancaria, respetando el control de chequera por unidad operativa, los cheques bloqueados, los ya usados y los pendientes.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Treasury.SettingsTreasury para la unidad operativa indicada con CheckBookControl definido; La cuenta bancaria (@EntityBanckAccountId) debe existir en Treasury.EntityBankAccounts y estar vinculada a un banco en Payroll.Bank; Para flujo con control de chequera, debe existir una chequera con Status=1 y CurrentNumber>0 asociada a la cuenta bancaria; El @UserCode debe identificar al usuario que realiza el bloqueo del cheque', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la chequera con Status=1 (activa) de la cuenta bancaria indicada; El número de cheque asignado nunca puede ser mayor a EndNumber de la chequera; Un número de cheque ya bloqueado en CheckBlock o ya usado en VoucherTransaction para la misma cuenta nunca se asigna nuevamente; Cuando se reutiliza un cheque pendiente (OutstandingChecks), este se elimina del registro de pendientes para no volver a ofrecerse; Si no hay control de chequera, no se inserta bloqueo y se entrega directamente el CurrentNumber de la chequera (o 1 si es nulo); Cualquier excepción se captura y se devuelve StatusResult=''999'' con el ERROR_MESSAGE(), sin propagar', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Chequera; Cheque; Control de chequera por unidad operativa; Cuenta bancaria de entidad; Cheque bloqueado; Cheque pendiente (outstanding); Comprobante de egreso; Tesorería', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Treasury.CheckBlock: Cuando hay control de chequera y se determina un @CheckNumberToAssign válido, se inserta (IdCheckbook, CheckNumber, CodUser) para bloquear preventivamente ese cheque a nombre del usuario; [DELETE] Treasury.OutstandingChecks: Cuando se reutiliza un cheque pendiente (@CheckNumberToAssign >= @OutstandingCheck), se elimina la fila WHERE CheckNumber=@CheckNumberToAssign AND IdCheckBook=@CheckbookId; [RETURN_RESULT] (resultset): Devuelve siempre un resultset con columnas StatusResult, MessageResult, CheckNumberToAssign, CheckbookId, CheckNumberEnd y CheckbookControl; ''000'' indica éxito y ''999'' error o condición no satisfecha', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CheckbookControl IS NULL (no existe configuración de control de chequera para la unidad operativa) → Retorna StatusResult=''999'' con mensaje ''No se encuentra establecido un control de chequera...'' y termina sin asignar cheque; si @CheckbookControl = 1 (hay control de chequera activo) → Busca chequera activa, itera buscando próximo cheque válido (no bloqueado, no usado en VoucherTransaction), considera cheques pendientes (OutstandingChecks), bloquea el cheque elegido en CheckBlock else Sin control de chequera: solo devuelve CurrentNumber y EndNumber de la chequera activa, sin bloquear ni validar; si Bajo control de chequera, @currentCheckNumber=0 tras buscar Checkbooks con Status=1 → Retorna StatusResult=''999'' con mensaje ''No se encontró una chequera activa para la cuenta bancaria...'' y termina; si Existe cheque pendiente (@OutstandingCheck <> -1) y @CheckNumberToAssign >= @OutstandingCheck → Reasigna @CheckNumberToAssign = @OutstandingCheck y elimina la fila correspondiente de Treasury.OutstandingChecks; si No hay cheques pendientes y la iteración alcanzó @CheckNumberEnd (@checkValid=2) → Retorna StatusResult=''999'' con mensaje ''No hay cheques disponibles para la chequera activa'' y termina; si Cheque candidato existe en Treasury.CheckBlock (bloqueado) o en Treasury.VoucherTransaction (ya usado en comprobante de egreso) para la misma cuenta → Incrementa @CheckNumberToAssign y continúa el bucle hasta encontrar uno válido o superar EndNumber', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.SettingsTreasury; Treasury.EntityBankAccounts; Payroll.Bank; Treasury.Checkbooks; Treasury.CheckBlock; Treasury.VoucherTransaction; Treasury.OutstandingChecks', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GetCheckNumber';
-- GO
