
-- ========================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 7/12/2018
-- Description:	Función que me retorna el rango por el cual aplica la edad del paciente y el idRiasCups, 
--				ya sea el rango en el que se encuentra la edad o el rango que sigue
-- ========================================================================================================
CREATE FUNCTION [dbo].[fnValidateCurrentRangeOrNextRange]
(	
	@AgeDays integer,
	@RiasCupsId integer
)
RETURNS TABLE 
AS
RETURN 
(
	select  top 1 * 
	from 
	(
		select
			ID as RiasCupsDetailId,
			IDRIASCUPS as RiasCupsId,
			REGLA as [Rule],
			FRECUENCIA as Frequency,
			UNIDADFRECUENCIA as UnitFrequency,
			CANTIDADPERIODO as PeriodQuantity,
			case UNIDADRANGO when 1 then EDADMINIMA   --Dia
					   			when 2 then (EDADMINIMA * 30)  --Meses a dias
								when 3 then (EDADMINIMA * 365)  --años a Dias
			End as AgeDaysMinimum,
			case UNIDADRANGO when 1 then EDADMAXIMA + 1    --Dia
					   			when 2 then (EDADMAXIMA * 30) + 30 --Meses a dias
								when 3 then (EDADMAXIMA * 365) + 365 --años a Dias
			End as AgeDaysMaximum,
			EDADMINIMA as AgeMinimum,
			EDADMAXIMA as AgeMaximum,
			case UNIDADRANGO when 1 then 'Dias'
								when 2 then 'Meses'
								when 3 then 'Años'	END as UnitRangeAge,
			REQUIEREORDENMED as RequiredMedicalOrder
		from RIASCUPSD where IDRIASCUPS = @RiasCupsId and REQUIEREORDENMED = 0 and ESTADO = 1
	) as tmp
	where (@AgeDays BETWEEN tmp.AgeDaysMinimum AND tmp.AgeDaysMaximum) or (tmp.AgeDaysMinimum > @AgeDays)
	order by tmp.AgeDaysMinimum
)