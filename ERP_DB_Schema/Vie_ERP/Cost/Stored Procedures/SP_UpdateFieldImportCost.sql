-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 2/12/2016
-- Description:	Procedimiento que se encarga de actualizar el campo de import en la tabla CostLogisticsProductionCenterRecordDetail
-- =============================================
CREATE PROCEDURE [Cost].[SP_UpdateFieldImportCost] 
	@ObjectXml as Xml
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX)

	EXEC [Cost].[SP_UpdateFieldImportCost_Output] 
		@ObjectXml,
		-----------------------------------------
		@CodeMessage OUTPUT, 
		@Message OUTPUT

	SELECT	@CodeMessage as CodeMessage, 
			@Message as Message
END