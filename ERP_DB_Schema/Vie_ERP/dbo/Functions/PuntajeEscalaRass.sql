

-- =============================================
-- Author:		Juan Montealegre
-- Create date: 16-12-2013
-- Description:	Funcion que retorna el puntaje de la escala
-- =============================================
CREATE FUNCTION [dbo].[PuntajeEscalaRass] 
(
	-- Add the parameters for the function here
	@NumeroIngreso char(10),
	@CodigoPaciente varchar(25)
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Resu int

	-- Add the T-SQL statements to compute the return value here
	SELECT TOP 1 @Resu =  PUNTAJESC  FROM HCESCRASS with(nolock) WHERE NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente ORDER BY CODCONCEC DESC

	-- Return the result of the function
	RETURN @Resu

END