-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-12-14
-- Description:	Procedimiento que se encarga de confirmar el archivo plano de banco
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ConfirmBankFile] 
    @Id AS INT,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		/********************************************************** VALIDACIONES **********************************************************/

		IF EXISTS (SELECT 1 FROM Payroll.BankFile bb WHERE bb.Id = @Id AND bb.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(bb.Status = 2, 'Confirmado', 'Anulado') as Message
			FROM Payroll.BankFile bb
			WHERE bb.Id = @Id
			RETURN
		END

		/************************************************************************************************************************************/

		DECLARE @ConfirmationUser VARCHAR(20) = @CodeUser
		DECLARE @ConfirmationDate DATETIME = [Common].[GETDATE]()

		--Confirmamos el archivo plano
		UPDATE [Payroll].[BankFile]
			SET [Status] = 2,
				[ConfirmationUser] = @ConfirmationUser,
				[ConfirmationDate] = @ConfirmationDate
		WHERE Id = @Id

		SELECT 0 AS CodeMessage, 'Se confirmó correctamente' as Message
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + CHAR(13) + CHAR(10) + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message
	END CATCH
END