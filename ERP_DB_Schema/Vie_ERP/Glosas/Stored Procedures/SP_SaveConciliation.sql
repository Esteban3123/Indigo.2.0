-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-09-30
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una conciliacion
-- =============================================
CREATE PROCEDURE [Glosas].[SP_SaveConciliation]
    @ConciliationXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@ConciliationConsecutive NUMERIC(18,0),
			-------------------------------------------------------------------
			@CodeResult INT,
			@MessageResult VARCHAR(MAX)


	EXEC [Glosas].[SP_SaveConciliation_Output]
		@ConciliationXml,
		@CodeUser,
		-----------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT,
		@Id OUTPUT, 
		@ConciliationConsecutive OUTPUT

	SELECT	@CodeResult CodeResult, 
			@MessageResult MessageResult,
			@Id Id,
			@ConciliationConsecutive Code
END