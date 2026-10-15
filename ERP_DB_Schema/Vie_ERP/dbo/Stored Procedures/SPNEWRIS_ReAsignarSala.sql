
/***********************************************************************************************
Modified By: Andres Ramirez
Date: 2020.01.22
Description: Consulta para sala con fecha y hora, y cambiar estado dentro de la tabla RISORDENES - Web 
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPNEWRIS_ReAsignarSala] 
(
@Auto int,
@Estado int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

UPDATE RISORDENES SET ESTADO = @Estado, IDSALA = NULL, FECHORAIN = NULL, FECHORAFI = NULL --,FECAGENDO = NULL, USUAGENDO = @Usuario
WHERE AUTO = @Auto

END