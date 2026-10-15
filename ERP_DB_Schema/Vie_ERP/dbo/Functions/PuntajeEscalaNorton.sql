

-- =============================================
-- Author:		Juan Montealegre
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[PuntajeEscalaNorton]
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
	SELECT TOP 1 
				@Resu = ESTFISGEN + ESTMENTAL +
						MOVILIDAD + ACTIVIDAD + 
						INCTINENCI 
	FROM 
		HCESCNTON 
	WHERE 
		NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente ORDER BY CODCONCEC DESC

	-- Return the result of the function
	RETURN @Resu

END