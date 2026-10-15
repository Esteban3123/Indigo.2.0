
-- =============================================
-- Author:		Juan Montealegre
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[PuntajeEscalaVas]
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
				@Resu = EDADPACIE + CAIDPREVI +
						TRANQUILI + DIURETICO +
						HIPOTENSO + ANTIPARKI + 
						ANTIDEPRE + ALTERAVIS + 
						ALTERAUDI + ICTUEXTRE + 
						ESTADOMEN + SEGURAYUD + 
						INSEGAYUD + IMPOSIBLE + 
						PATOLOGIA + NUTRICION
	FROM 
		HCESCDOWN 
	WHERE 
		NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente ORDER BY AUTO DESC

	-- Return the result of the function
	RETURN @Resu

END