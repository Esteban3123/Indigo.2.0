

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