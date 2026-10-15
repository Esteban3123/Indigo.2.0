-- =============================================
-- Author:		Juan Carlos Bermudez Gutierrez
-- Create date: 06-11-2015
-- Description:	Procedimiento el cual se encarga de guardar un recaudo
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveCollection]
	@CollectionXML AS XML,
	@CollectionDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveCollection_Output] @CollectionXML, @CollectionDetailForDeleteXml, @CodeUser, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeMessage, 
		@MessageResult AS Message, 
		@Id as Id, 
		@Code as Code
END