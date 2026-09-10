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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que valida si la edad de un paciente (expresada en días) cumple con el rango de edad establecido en las reglas de elegibilidad RIAS para un servicio CUPS específico. Recibe como parámetros el identificador del servicio CUPS-RIAS y la edad del paciente en días, y consulta la tabla RIASCUPSD convirtiendo los rangos de edad (que pueden estar en días, meses o años) a una unidad común en días para hacer la comparación. Retorna las reglas de elegibilidad que aplican al paciente según su edad, incluyendo frecuencia de uso y número de veces permitidas por orden médica. Se usa en el proceso de verificación de servicios preventivos y de promoción de salud dentro de las Rutas Integrales de Atención en Salud (RIAS), para determinar si un paciente tiene derecho a un servicio según su grupo etario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ValidaRangoEdadRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ValidaRangoEdadRIASCUPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve las reglas de frecuencia/orden de un CUPS dentro de una RIAS aplicables a una edad expresada en días, normalizando los rangos de edad a días según su unidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir configuración en RIASCUPSD para el IDRIASCUPS consultado.; La unidad de rango debe ser 1 (días), 2 (meses) o 3 (años); otros valores producen NULL en los límites y excluyen la fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un mes se equipara a 30 días y un año a 360 días al normalizar rangos.; El límite superior se extiende una unidad (día/mes/año en días) para incluir el periodo completo.; Solo se evalúan reglas asociadas al IDRIASCUPS recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); CUPS; Rango de edad; Frecuencia de atención; Número de veces por orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RIASCUPSD: Retorna las filas de RIASCUPSD del IDRIASCUPS dado cuya edad mínima/máxima (convertida a días) contiene a la edad consultada (BETWEEN EDADMINIMA_DIAS AND EDADMAXIMA_DIAS).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UNIDADRANGO = 1 (días) → EDADMINIMA_DIAS = EDADMINIMA; EDADMAXIMA_DIAS = EDADMAXIMA + 1; si UNIDADRANGO = 2 (meses) → EDADMINIMA_DIAS = EDADMINIMA*30; EDADMAXIMA_DIAS = EDADMAXIMA*30 + 30; si UNIDADRANGO = 3 (años) → EDADMINIMA_DIAS = EDADMINIMA*360; EDADMAXIMA_DIAS = EDADMAXIMA*360 + 360 else Cualquier otro valor de UNIDADRANGO produce NULL en los límites y la fila no cumple el BETWEEN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPSD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidaRangoEdadRIASCUPS';
GO
