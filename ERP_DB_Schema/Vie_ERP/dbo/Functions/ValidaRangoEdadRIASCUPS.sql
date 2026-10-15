-- =============================================
-- Author:		Rafael Eduardo patiño Cabrera
-- Create date: 21/11/2018
-- Description:	rango de edad x cups
-- =============================================
CREATE FUNCTION [dbo].[ValidaRangoEdadRIASCUPS]
(	
	-- Add the parameters for the function here
	@EdadDias integer,
	@IDRIASCUPS integer
)
RETURNS TABLE 
AS
RETURN 
(
	select * from (
					select 
						ID,
						REGLA,
						FRECUENCIA,
						UNIDADFRECUENCIA,
						NUMEROVECESORDEN,
						case UNIDADRANGO when 1 then EDADMINIMA     --Dia
					   					 when 2 then EDADMINIMA * 30 --Meses a dias
										 when 3 then EDADMINIMA * 360 --años a Dias
						End as EDADMINIMA_DIAS,
						case UNIDADRANGO when 1 then EDADMAXIMA + 1    --Dia
					   					 when 2 then EDADMAXIMA * 30 + 30 --Meses a dias
										 when 3 then EDADMAXIMA * 360 + 360 --años a Dias
						End as EDADMAXIMA_DIAS
					from RIASCUPSD where IDRIASCUPS = @IDRIASCUPS) as tmp
					where  @EdadDias BETWEEN tmp.EDADMINIMA_DIAS AND tmp.EDADMAXIMA_DIAS  

)