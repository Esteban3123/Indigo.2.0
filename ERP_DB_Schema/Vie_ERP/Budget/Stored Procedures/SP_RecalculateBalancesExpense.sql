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