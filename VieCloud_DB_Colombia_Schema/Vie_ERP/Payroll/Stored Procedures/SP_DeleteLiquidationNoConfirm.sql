-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 21/10/2015
-- Description:	Elimina las Liquidaciones de Nomina NO Confirmadas
-- =============================================
CREATE PROCEDURE [Payroll].[SP_DeleteLiquidationNoConfirm]
	@PayrollDate date,
	@GroupId int,
	@EmployeeNit VARCHAR(50)
WITH RECOMPILE
AS 
BEGIN
	SET NOCOUNT ON

	DECLARE @PayrollDateTmp date = @PayrollDate
	DECLARE @GroupIdTmp int = @GroupId
	DECLARE @EmployeeNitTmp VARCHAR(50) =  @EmployeeNit

	BEGIN TRY

		IF @EmployeeNitTmp = '' BEGIN
			
			DELETE FROM Payroll.[Message] WHERE LiquitadionId in (SELECT Id FROM Payroll.Liquidation WHERE RegisterStatus = '' and GroupId = @GroupIdTmp and PayrollDateLiquidated = @PayrollDateTmp)

			DELETE FROM Payroll.LiquidationDetail WHERE PayrollId in (SELECT Id FROM Payroll.Liquidation WHERE RegisterStatus = '' and GroupId = @GroupIdTmp and PayrollDateLiquidated = @PayrollDateTmp)

			DELETE FROM Payroll.Liquidation WHERE RegisterStatus = '' and GroupId = @GroupIdTmp and PayrollDateLiquidated = @PayrollDateTmp
		END
		ELSE BEGIN
			
			DECLARE @IdEmployee int
			DECLARE @Idliquidation int

			SELECT @IdEmployee = E.Id 
			FROM Payroll.Employee E WITH(NoLock)
			INNER JOIN Common.ThirdParty TP WITH(NoLock) ON TP.Id = E.thirdPartyId
			WHERE TP.Nit = @EmployeeNitTmp

			SELECT @Idliquidation = Id FROM Payroll.Liquidation where RegisterStatus = '' and GroupId = @GroupIdTmp and PayrollDateLiquidated = @PayrollDateTmp and EmployeeId = @IdEmployee

			DELETE FROM Payroll.[Message] where LiquitadionId = @Idliquidation

			DELETE FROM Payroll.LiquidationDetail where PayrollId = @Idliquidation

			DELETE FROM Payroll.Liquidation where Id = @Idliquidation

		END
	END TRY
	BEGIN CATCH
		RETURN 3
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina liquidaciones de nómina que aún no han sido confirmadas (aprobadas) para un período de pago y grupo de nómina específicos. Puede operar sobre todos los empleados del grupo o sobre un empleado particular identificado por su NIT/cédula. Antes de borrar el encabezado de liquidación, elimina en cascada los mensajes de proceso y el detalle de conceptos liquidados (devengados y deducciones) asociados, garantizando integridad referencial. Se usa durante el proceso de pre-liquidación cuando se requiere deshacer o reliquidar una nómina pendiente de aprobación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteLiquidationNoConfirm';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteLiquidationNoConfirm';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina liquidaciones de nómina no confirmadas (RegisterStatus vacío) junto con sus mensajes y detalles asociados, ya sea para todo un grupo en una fecha o limitado a un empleado específico.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteLiquidationNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado identificado por Nit debe existir en Payroll.Employee vinculado a Common.ThirdParty cuando se proporciona Nit no vacío; Las liquidaciones a eliminar deben tener RegisterStatus = '''' (no confirmadas)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteLiquidationNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se eliminan liquidaciones con RegisterStatus = '''' (no confirmadas); las confirmadas nunca son afectadas; El borrado respeta el orden de dependencia: primero Message, luego LiquidationDetail y por último Liquidation; Cualquier error es silenciado retornando el código 3 sin propagar la excepción', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteLiquidationNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina; Liquidación no confirmada; Empleado; Tercero (Nit); Grupo de nómina; Fecha de liquidación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteLiquidationNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Payroll.Message: Cuando Nit es vacío: elimina mensajes cuyo LiquitadionId pertenezca a liquidaciones con RegisterStatus='''' del grupo y fecha indicados; [DELETE] Payroll.LiquidationDetail: Cuando Nit es vacío: elimina detalles cuyo PayrollId pertenezca a liquidaciones con RegisterStatus='''' del grupo y fecha indicados; [DELETE] Payroll.Liquidation: Cuando Nit es vacío: elimina liquidaciones con RegisterStatus='''' del grupo y fecha indicados; [DELETE] Payroll.Message: Cuando se especifica Nit: elimina mensajes asociados a la liquidación no confirmada del empleado en el grupo y fecha indicados; [DELETE] Payroll.LiquidationDetail: Cuando se especifica Nit: elimina detalles asociados a la liquidación no confirmada del empleado en el grupo y fecha indicados; [DELETE] Payroll.Liquidation: Cuando se especifica Nit: elimina la liquidación no confirmada del empleado en el grupo y fecha indicados; [RETURN_RESULT] -: Retorna 3 si ocurre cualquier excepción durante el borrado (BEGIN CATCH RETURN 3)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteLiquidationNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EmployeeNit = '''' → Borra masivamente Message, LiquidationDetail y Liquidation para todas las liquidaciones no confirmadas del grupo y fecha else Resuelve EmployeeId vía ThirdParty.Nit, obtiene el Id de la liquidación no confirmada del empleado y borra solo sus Message, LiquidationDetail y Liquidation', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteLiquidationNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteLiquidationNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteLiquidationNoConfirm';
-- GO
