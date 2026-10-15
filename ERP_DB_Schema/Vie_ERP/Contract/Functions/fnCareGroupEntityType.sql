-- =============================================
-- Author:      <HECTOR RODRIGUEZ RUBIANO>
-- Create Date: <14/01/2022>
-- Description: <Devuelve la descripcion del tipo de entidad de un grupo de atencion>
-- =============================================
CREATE FUNCTION [Contract].[fnCareGroupEntityType]
(
    -- Add the parameters for the function here
    @EntityType tinyint
)
RETURNS varchar(50)
AS
BEGIN
    -- Declare the return variable here
    DECLARE @Result varchar(50)

    -- Add the T-SQL statements to compute the return value here
	SELECT @Result =
	CASE @EntityType
    WHEN 1 THEN 'EPS Contributivo'
        WHEN 2 THEN 'EPS Subsidiado'
        WHEN 3 THEN 'ET Vinculados Municipios'
        WHEN 4 THEN 'ET Vinculados Departamentos'
        WHEN 5 THEN 'ARL Riesgos Laborales'
        WHEN 6 THEN 'MP Medicina Prepagada'
        WHEN 7 THEN 'IPS Privada'
        WHEN 8 THEN 'IPS Publica'
        WHEN 9 THEN 'Regimen Especial'
        WHEN 11 THEN 'Fosyga'
        WHEN 12 THEN 'Otros'
        WHEN 13 THEN 'Aseguradoras'
		ELSE CAST(@EntityType AS VARCHAR(50))
	END

    -- Return the result of the function
    RETURN @Result
END