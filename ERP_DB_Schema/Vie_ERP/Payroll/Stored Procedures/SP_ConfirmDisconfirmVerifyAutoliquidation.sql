-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 24-10-2019
-- Description:	Procedimiento Confirmar o Desconfirmar la tabla de Verificación de Autoliquidación
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ConfirmDisconfirmVerifyAutoliquidation]
	@WorkCenterCode VARCHAR(50),
	@PayrollDateLiquidated date,
	@FlagConfirm BIT, -- (1 - Confirmar, 0 - Desconfirmar)
	@ActionUser VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY

		IF (SELECT count(*) FROM Payroll.VerifyAutoliquidationFile WHERE WorkCenter = @WorkCenterCode and PayrollDateLiquidated = @PayrollDateLiquidated) = 0 BEGIN
			SELECT 999 AS CodeResult, 'No hay datos para esta Fecha y este Centro de Trabajo' AS MessageResult
			RETURN
		END
		
		IF (SELECT count(*) FROM Payroll.WorkCenter WHERE Code = @WorkCenterCode) = 0 BEGIN
			SELECT 999 AS CodeResult, 'No existe ningún Centro de Trabajo con ese Código' AS MessageResult
			RETURN
		END

		IF @FlagConfirm = 1 BEGIN

			IF (SELECT count(*) FROM Payroll.VerifyAutoliquidationFile WHERE WorkCenter = @WorkCenterCode and PayrollDateLiquidated = @PayrollDateLiquidated AND RegisterStatus = @FlagConfirm) > 0 BEGIN
				SELECT 999 AS CodeResult, 'Ya se encuentra Confirmado este Archivo' AS MessageResult
				RETURN
			END

			-- SE CONFIRMA EL REGISTRO
			UPDATE Payroll.VerifyAutoliquidationFile
					SET RegisterStatus = 1, ConfirmationUser = @ActionUser, ConfirmationDate = [Common].[GETDATE]()
			WHERE WorkCenter = @WorkCenterCode and PayrollDateLiquidated = @PayrollDateLiquidated

			SELECT 0 AS CodeResult, 'Se ha Confirmado Correctamente' AS MessageResult
			RETURN

		END ELSE BEGIN

			IF (SELECT count(*) FROM Payroll.VerifyAutoliquidationFile WHERE WorkCenter = @WorkCenterCode and PayrollDateLiquidated = @PayrollDateLiquidated AND RegisterStatus = @FlagConfirm) > 0 BEGIN
				SELECT 999 AS CodeResult, 'Ya se encuentra Desconfirmado este Archivo' AS MessageResult
				RETURN
			END

			UPDATE Payroll.VerifyAutoliquidationFile
				SET RegisterStatus = 0, DisconfirmationUser = @ActionUser, DisconfirmationDate = [Common].[GETDATE]()
			WHERE WorkCenter = @WorkCenterCode and PayrollDateLiquidated = @PayrollDateLiquidated

			SELECT 0 AS CodeResult, 'Se ha Desconfirmado Correctamente' AS MessageResult
			RETURN
		END

	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)) AS MessageResult
	END CATCH
END