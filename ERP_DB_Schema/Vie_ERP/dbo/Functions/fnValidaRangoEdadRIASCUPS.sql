-- =============================================
-- Author:		Rafael Eduardo patiño Cabrera
-- Create date: 21/11/2018
-- Description:	rango de edad x cups
-- =============================================
CREATE FUNCTION [dbo].[fnValidaRangoEdadRIASCUPS]
(	
	-- Add the parameters for the function here
	@EdadDias integer,
	@IDRIASCUPS integer
)
RETURNS TABLE 
AS
RETURN 
(
	--aseguramos que la consulta siempre retorno 1 solo rango, de igual forma desde la parametrizacion de CUPS  x RIAS se debe garantizar
	--que no haya rangos de edades solapados
	select  top 1 * from (
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
						NUMEROVECESORDEN  
					from RIASCUPSD where IDRIASCUPS = @IDRIASCUPS and ESTADO = 1) as tmp
					where  @EdadDias BETWEEN tmp.EDADMINIMA_DIAS AND tmp.EDADMAXIMA_DIAS  



)