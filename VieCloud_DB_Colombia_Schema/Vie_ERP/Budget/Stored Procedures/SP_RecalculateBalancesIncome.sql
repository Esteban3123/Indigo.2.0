
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [Budget].[SP_RecalculateBalancesIncome]
	@ValidityId INT,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		/*************************************** RESULTADO ***************************************/

		SELECT	0 AS CodeResult, 
				'La vigencia presupuestal de ingresos se recalculo correctamente.' AS MessageResult
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recalcula los saldos de ingresos para una vigencia presupuestal específica. Recibe el identificador de la vigencia presupuestal y el código del usuario que ejecuta la acción. Está diseñado para ajustar o corregir los balances del presupuesto de ingresos cuando se detectan inconsistencias o se requiere una actualización manual. Devuelve un código y mensaje de resultado indicando si el recálculo fue exitoso o si ocurrió un error.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_RecalculateBalancesIncome';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_RecalculateBalancesIncome';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Stub/placeholder que simula el recálculo de saldos de la vigencia presupuestal de ingresos devolviendo un mensaje de éxito o de error capturado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un conjunto resultado con columnas CodeResult y MessageResult.; En caso de éxito retorna CodeResult=0; ante excepción retorna CodeResult=999 con el mensaje de error y la línea.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'vigencia presupuestal; ingresos; presupuesto', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): En el bloque TRY retorna CodeResult=0 y mensaje ''La vigencia presupuestal de ingresos se recalculo correctamente.''; [RETURN_RESULT] (resultset): En CATCH retorna CodeResult=999 concatenando ERROR_MESSAGE() y ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesIncome';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_RecalculateBalancesIncome';
-- GO
