-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 02/06/2020
-- Description:	Procedimiento que se encarga de guardar la trazabilidad de tramites
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveTraceabilityPaperwork]
    @Xml AS xml
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AnnexesConsecutives VARCHAR(20),
			@AnnexId INT			

	EXEC [Authorization].[SP_SaveTraceabilityPaperwork_Output] 
		@Xml,
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		--------------------------------------------		
		@AnnexesConsecutives OUTPUT,
		@AnnexId OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult, 		
			@AnnexesConsecutives as AnnexesConsecutives,
			@AnnexId as AnnexId
END