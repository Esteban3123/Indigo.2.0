-- =============================================
-- Author:		Juan Carlos Bermudez Gutierrez
-- Create date: 2015-11-09
-- Description:	Procedimiento el cual se encarga de guardar un reconocimiento
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveRecognition]
	@RecognitionXML AS XML,
	@RecognitionDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveRecognition_Output] @RecognitionXML, @RecognitionDetailForDeleteXml, @CodeUser, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeMessage, 
		@MessageResult AS Message, 
		@Id as Id, 
		@Code as Code
END