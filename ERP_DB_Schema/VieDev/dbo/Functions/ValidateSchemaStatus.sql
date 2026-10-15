
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[ValidateSchemaStatus] (@TIPOCONSULTA AS integer, @IDHCORDQUIMIo as varchar(25), @CODPRODUC AS Varchar(50), @CICLO AS INT, @DIA AS INT)
RETURNS varchar (300)
AS
BEGIN

DECLARE @ESTADOMODIFICADO INT = 0
	
IF @TIPOCONSULTA = 1
BEGIN
	SET @ESTADOMODIFICADO = (SELECT TOP 1 State FROM EHR.HCORDCICLOS WHERE IDHCORDQUIMIo = @IDHCORDQUIMIo AND State <> 1)
END
ELSE
BEGIN	
	SET @ESTADOMODIFICADO = (SELECT TOP 1 State FROM EHR.HCORDMEDICAM WHERE IDHCORDQUIMIo = @IDHCORDQUIMIo AND State <> 1 AND CODPRODUC = @CODPRODUC AND (@CICLO = 0 OR CICLO = @CICLO) AND (@DIA = 0 OR DIA = @DIA))
END

/*
Función que me indica si el medicamento o el esquema fue modificado 1 - sin modificar, 2 - medicamento añadido, 3 - modificado, 4 - suspendido 
*/

RETURN @ESTADOMODIFICADO

END