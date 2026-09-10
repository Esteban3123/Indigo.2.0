

CREATE Procedure [GeneralLedger].[SP_SaveJournalVoucherOffline_ByMovementID]
	@MovementId INT,
	@Status TINYINT = 2
As
BEGIN
--BEGIN TRY
--	--se refactoriza el sp de comprobantes contables creando un nuevo sp que devuelva valores concretos y no una tabla debido a problemas de ejecucion de sp anidados
	Declare @CodeMessage Int,
		@Message Varchar(Max),
		@IdJournalVoucherResult INT

--	Exec [GeneralLedger].[SP_SaveJournalVoucher_ByMovementId_Output] @MovementId, @status, @CodeMessage Output, @Message Output, @IdJournalVoucherResult OUTPUT

--	if @CodeMessage = '0' begin
--		delete from GeneralLedger.AccountingMovementPending where AccountingMovementId = @MovementId
--	end else begin
--		update GeneralLedger.AccountingMovementPending set FailedMessage = @Message, Failed = 1 where AccountingMovementId = @MovementId
--	end

	SELECT	@CodeMessage = 999, @Message = 'Procedimiento incorrecto', @IdJournalVoucherResult = 0
	Return
--END TRY
--BEGIN CATCH
--	SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)) AS [Message], 0 IdJournalVoucher
--	RETURN
--END CATCH
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento destinado a generar y guardar comprobantes contables (vouchers del libro mayor) de forma offline a partir de un movimiento contable identificado por su ID. Recibe el identificador del movimiento (@MovementId) y un estado (@Status) para controlar el flujo de contabilización. Actualmente el procedimiento está desactivado (código comentado) y retorna siempre el código 999 con el mensaje ''Procedimiento incorrecto'', indicando que está fuera de servicio o en proceso de refactorización. En su versión operativa, llamaba a SP_SaveJournalVoucher_ByMovementId_Output para crear el comprobante y luego actualizaba la tabla de movimientos contables pendientes (AccountingMovementPending) marcándolos como procesados o fallidos según el resultado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveJournalVoucherOffline_ByMovementID';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveJournalVoucherOffline_ByMovementID';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procedimiento desactivado que siempre retorna un código de error indicando que el procedimiento es incorrecto, sin ejecutar lógica contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherOffline_ByMovementID';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre asigna CodeMessage=999, Message=''Procedimiento incorrecto'' e IdJournalVoucherResult=0 antes de retornar.; No realiza ninguna operación sobre tablas (toda la lógica original está comentada).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherOffline_ByMovementID';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable (Journal Voucher); Movimiento contable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherOffline_ByMovementID';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucherOffline_ByMovementID';
-- GO
