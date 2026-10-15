
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