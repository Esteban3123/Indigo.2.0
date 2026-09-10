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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma o desconfirma el archivo de verificación de autoliquidación de nómina (PILA/seguridad social) para un centro de trabajo y un período de liquidación específicos. Recibe el código del centro de trabajo, la fecha de liquidación de nómina, un indicador de acción (confirmar o desconfirmar) y el usuario que ejecuta la acción. Actualiza el estado del registro en la tabla VerifyAutoliquidationFile, registrando el usuario y la fecha de confirmación o desconfirmación según corresponda. Incluye validaciones previas para verificar que existan datos para la fecha y el centro de trabajo indicados, que el centro de trabajo sea válido, y que el archivo no se encuentre ya en el estado solicitado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma o desconfirma el archivo de verificación de autoliquidación de nómina para un centro de trabajo y fecha de liquidación específicos, marcando el estado y registrando usuario/fecha de la acción.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en Payroll.VerifyAutoliquidationFile para el centro de trabajo y fecha de liquidación indicados; El código de centro de trabajo debe existir en Payroll.WorkCenter; Si se confirma (Flag=1), no debe haber registros ya con RegisterStatus=1 para esa combinación; Si se desconfirma (Flag=0), no debe haber registros ya con RegisterStatus=0 para esa combinación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de confirmación/desconfirmación se obtiene siempre desde [Common].[GETDATE](), no desde GETDATE() nativo; No se realizan actualizaciones si el archivo ya está en el estado solicitado (idempotencia bloqueada con mensaje); Toda excepción es capturada y devuelta como CodeResult=999 con ERROR_MESSAGE y línea, sin propagar la excepción; Las operaciones aplican a todas las filas que coinciden con WorkCenter + PayrollDateLiquidated (operación masiva por archivo)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autoliquidación de nómina; Verificación de archivo de autoliquidación; Centro de trabajo; Confirmación/Desconfirmación de archivo; Fecha de liquidación de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Payroll.VerifyAutoliquidationFile: Cuando @FlagConfirm=1 y aún no está confirmado: se asigna RegisterStatus=1, ConfirmationUser=@ActionUser y ConfirmationDate=[Common].[GETDATE]() para el WorkCenter y PayrollDateLiquidated dados; [UPDATE] Payroll.VerifyAutoliquidationFile: Cuando @FlagConfirm=0 y aún no está desconfirmado: se asigna RegisterStatus=0, DisconfirmationUser=@ActionUser y DisconfirmationDate=[Common].[GETDATE]() para el WorkCenter y PayrollDateLiquidated dados; [RETURN_RESULT] (resultset): Devuelve CodeResult=0 con mensaje de éxito (''Se ha Confirmado/Desconfirmado Correctamente'') o CodeResult=999 con el mensaje de error correspondiente (datos inexistentes, centro inválido, ya confirmado/desconfirmado, o ERROR_MESSAGE de CATCH)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existen filas en VerifyAutoliquidationFile para WorkCenter y PayrollDateLiquidated → Retorna 999 ''No hay datos para esta Fecha y este Centro de Trabajo'' y termina; si No existe el WorkCenter en Payroll.WorkCenter → Retorna 999 ''No existe ningún Centro de Trabajo con ese Código'' y termina; si @FlagConfirm = 1 → Si ya hay registros con RegisterStatus=1 retorna ''Ya se encuentra Confirmado''; en caso contrario actualiza a estado confirmado else Si ya hay registros con RegisterStatus=0 retorna ''Ya se encuentra Desconfirmado''; en caso contrario actualiza a estado desconfirmado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.VerifyAutoliquidationFile; Payroll.WorkCenter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmDisconfirmVerifyAutoliquidation';
-- GO
