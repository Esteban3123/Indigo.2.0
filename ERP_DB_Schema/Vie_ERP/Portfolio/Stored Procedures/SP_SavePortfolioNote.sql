-- =============================================
-- Author:		Carlos Ernesto Cordoba
-- Create date:	2016-07-11
-- Description:	Procedimiento para guardar, actualizar, confirmar o anular la nota
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SavePortfolioNote]
    @PortfolioNoteXml AS XML,
    @User VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT
AS
BEGIN
    SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Portfolio].[SP_SavePortfolioNote_Output] @PortfolioNoteXml, @User, @CompanyType, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@Id as Id, 
		@Code as Code  
END