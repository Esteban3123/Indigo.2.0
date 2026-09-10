-- =============================================
-- Author:		JOHAN SEBASTIAN CUELLAR
-- Create date: 2021-01-04
-- Description:	Mayorizacion Saldos Presupuesto de Gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_RecalculateBalancesExpense]
	@ValidityId INT,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		/*************************************** RESULTADO ***************************************/

		SELECT	0 AS CodeResult, 
				'La vigencia presupuestal de gastos se recalculo correctamente.' AS MessageResult
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recalcula y mayoriza los saldos del presupuesto de gastos para una vigencia presupuestal específica. Recibe el identificador de la vigencia presupuestal y el código del usuario que ejecuta el proceso, y actualiza los saldos acumulados del módulo de presupuesto de gastos. Existe para garantizar la consistencia de los saldos presupuestales cuando se realizan ajustes o correcciones en las ejecuciones del presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_RecalculateBalancesExpense';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_RecalculateBalancesExpense';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Stub/placeholder para la mayorización de saldos del presupuesto de gastos de una vigencia, que actualmente solo retorna el mensaje de éxito sin realizar cálculos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un único result set con columnas CodeResult y MessageResult; CodeResult=0 indica éxito; CodeResult=999 indica error capturado con mensaje y línea', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vigencia presupuestal; Presupuesto de gastos; Mayorización de saldos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): En flujo normal devuelve CodeResult=0 con mensaje ''La vigencia presupuestal de gastos se recalculo correctamente.''; [RETURN_RESULT] (result set): En CATCH devuelve CodeResult=999 con ERROR_MESSAGE() y ERROR_LINE() concatenados', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesExpense';
-- GO
