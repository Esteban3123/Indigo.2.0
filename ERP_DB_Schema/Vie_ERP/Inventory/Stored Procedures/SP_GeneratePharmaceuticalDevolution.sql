-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 26/01/2016
-- Description:	Procedimiento almacena para guardar o confirmar las devoluciones
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GeneratePharmaceuticalDevolution]
	@DevolutionXml xml,
	@AnnulateXml xml,
	@User varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CodeMessage VARCHAR(10),
			@Message VARCHAR(MAX),
			@DevolutionId INT,
			@Status TINYINT

	EXEC [Inventory].[SP_GeneratePharmaceuticalDevolution_Output] 
		@DevolutionXml, 
		@AnnulateXml, 
		@User, 
		--------------------------------------------
		@CodeMessage OUTPUT, 
		@Message OUTPUT, 
		--------------------------------------------
		@DevolutionId OUTPUT, 
		@Status OUTPUT

	SELECT	@CodeMessage AS CodeMessage, 
			@Message AS Message, 
			@DevolutionId as DevolutionId, 
			@Status as Status
END