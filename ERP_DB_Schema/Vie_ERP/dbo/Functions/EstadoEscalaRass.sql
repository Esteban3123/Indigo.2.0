

-- =============================================
-- Author:		Juan Montealegre
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- ==========================================
CREATE FUNCTION [dbo].[EstadoEscalaRass]
(
	-- Add the parameters for the function here
	@NumeroIngreso char(10),
	@CodigoPaciente varchar(25)
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Resu char

	-- Add the T-SQL statements to compute the return value here
	SELECT 
				@Resu = ESCARASS
	FROM 
		ADINGRESO
	WHERE 
		NUMINGRES = @NumeroIngreso AND IPCODPACI =@CodigoPaciente 

	-- Return the result of the function
	RETURN @Resu

END