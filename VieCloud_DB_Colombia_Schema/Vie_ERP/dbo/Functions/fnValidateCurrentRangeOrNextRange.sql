
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina qué rango de edad aplica para un servicio CUPS dentro de la Ruta Integral de Atención en Salud (RIAS), dado el identificador del servicio CUPS-RIAS y la edad del paciente expresada en días. Retorna el detalle del rango vigente (en el que cae la edad actual del paciente) o, si la edad aún no alcanza ningún rango activo, el próximo rango que le correspondería. Consulta la tabla RIASCUPSD filtrando únicamente reglas activas y que no requieren orden médica, convirtiendo las edades mínima y máxima a días según la unidad configurada (días, meses o años). Se usa para validar elegibilidad de servicios preventivos y de promoción en salud según la edad del paciente dentro de los programas RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnValidateCurrentRangeOrNextRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnValidateCurrentRangeOrNextRange';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el rango de edad aplicable (actual o el siguiente) para un servicio RIAS-CUPS dado, según la edad del paciente en días y el identificador del servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidateCurrentRangeOrNextRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un detalle en RIASCUPSD para el IDRIASCUPS dado con ESTADO = 1 y REQUIEREORDENMED = 0; La unidad de rango (UNIDADRANGO) debe ser 1 (días), 2 (meses) o 3 (años); valores distintos producirán NULL en los límites calculados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidateCurrentRangeOrNextRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran detalles activos (ESTADO = 1); Solo se consideran detalles que NO requieren orden médica (REQUIEREORDENMED = 0); Conversión fija: 1 mes = 30 días, 1 año = 365 días; El límite máximo del rango se extiende un periodo adicional (día/mes/año) sobre EDADMAXIMA; Nunca devuelve un rango ya pasado respecto a la edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidateCurrentRangeOrNextRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; edad del paciente; RIAS-CUPS; rangos de edad; frecuencia de atención; orden médica; regla de prestación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidateCurrentRangeOrNextRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RIASCUPSD: Retorna TOP 1 fila del detalle RIAS-CUPS cuyo rango de edad contiene la edad del paciente, o en su defecto el rango siguiente más próximo (AgeDaysMinimum > @AgeDays), ordenado por AgeDaysMinimum ascendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidateCurrentRangeOrNextRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UNIDADRANGO = 1 → Edades mínima/máxima se interpretan en días (máxima + 1); si UNIDADRANGO = 2 → Edades se convierten de meses a días multiplicando por 30 (máxima * 30 + 30); si UNIDADRANGO = 3 → Edades se convierten de años a días multiplicando por 365 (máxima * 365 + 365); si @AgeDays BETWEEN AgeDaysMinimum AND AgeDaysMaximum → Se devuelve el rango actual al que pertenece la edad else Si AgeDaysMinimum > @AgeDays, se devuelve el siguiente rango futuro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidateCurrentRangeOrNextRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPSD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidateCurrentRangeOrNextRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidateCurrentRangeOrNextRange';
GO
