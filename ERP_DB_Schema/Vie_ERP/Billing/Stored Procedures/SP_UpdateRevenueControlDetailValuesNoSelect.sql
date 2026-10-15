
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2015-09-18
-- Description:	Recalcula los valores de un folio
-- =============================================
CREATE PROCEDURE [Billing].[SP_UpdateRevenueControlDetailValuesNoSelect]
	-- Add the parameters for the stored procedure here
	@REVENUECONTROLDETAILID AS INT,
	@OperativeUnitId AS INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StatusResult AS BIT, 
			@MessageResult AS VARCHAR(100)

	EXEC [Billing].[SP_UpdateRevenueControlDetailValues_Output] @REVENUECONTROLDETAILID, @OperativeUnitId, @StatusResult OUT, @MessageResult OUT

	--SELECT @StatusResult as StatusResult, @MessageResult as MessageResult
	RETURN
END