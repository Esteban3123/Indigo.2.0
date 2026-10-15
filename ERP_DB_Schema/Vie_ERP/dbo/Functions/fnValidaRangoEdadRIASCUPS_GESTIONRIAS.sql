-- =============================================
-- Author:		Rafael Eduardo patiño Cabrera
-- Create date: 19/03/2019
-- Description:	retorna los rangos a los que aplica un IDriasCups
-- =============================================
CREATE FUNCTION [dbo].[fnValidaRangoEdadRIASCUPS_GESTIONRIAS]
(	
	-- Add the parameters for the function here
	@IDRIASCUPS integer
)
RETURNS TABLE 
AS
RETURN 
(
	--retornamos rango a los que aplica una IdriasCups
	select  * from (
					select
						ID,
						IDRIASCUPS,
						REGLA,
						FRECUENCIA,
						UNIDADFRECUENCIA,
						CANTIDADPERIODO,
						case UNIDADRANGO when 1 then EDADMINIMA     --Dia
					   					 when 2 then (EDADMINIMA * 30) --Meses a dias
										 when 3 then (EDADMINIMA * 365)  --años a Dias
						End as EDADMINIMA_DIAS,
						case UNIDADRANGO when 1 then EDADMAXIMA  + 1    --Dia
					   					 when 2 then (EDADMAXIMA * 30) + 30 --Meses a dias
										 when 3 then (EDADMAXIMA * 365) + 365 --años a Dias
						End as EDADMAXIMA_DIAS,
						EDADMINIMA,
						EDADMAXIMA,
						case UNIDADRANGO when 1 then 'Dias'
										 when 2 then 'Meses'
										 when 3 then 'Años'	END as UnidadRangoEdad,
						REQUIEREORDENMED,
						NUMEROVECESORDEN,
						SEXO  
					from RIASCUPSD where IDRIASCUPS = @IDRIASCUPS and ESTADO = 1) as tmp
					



)