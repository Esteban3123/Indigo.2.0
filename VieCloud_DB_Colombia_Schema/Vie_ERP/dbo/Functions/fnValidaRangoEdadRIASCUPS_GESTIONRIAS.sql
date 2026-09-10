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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que retorna las reglas de elegibilidad y rangos de edad aplicables a un servicio CUPS dentro de una Ruta Integral de Atención en Salud (RIAS), dado su identificador interno. Consulta la tabla RIASCUPSD filtrando solo registros activos y normaliza las edades mínima y máxima a días, independientemente de si la unidad original estaba expresada en días, meses o años, facilitando comparaciones uniformes. También expone atributos como frecuencia de uso permitida, cantidad por período, sexo al que aplica y si el servicio requiere orden médica. Se usa para validar si un paciente cumple los criterios de edad y condiciones de la RIAS antes de autorizar o agendar un servicio preventivo o de atención en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los rangos de edad activos (normalizados a días) y reglas asociadas aplicables a un detalle de RIAS-CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en RIASCUPSD con el IDRIASCUPS suministrado y ESTADO = 1 para retornar filas.; UNIDADRANGO debe tomar valores 1 (días), 2 (meses) o 3 (años); otros valores producen NULL en las edades convertidas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran reglas con ESTADO = 1 (activas).; Las edades siempre se exponen también normalizadas en días para comparaciones homogéneas.; El límite superior del rango se amplía una unidad para incluir el periodo completo (mes/año/día siguiente).; Un mes se modela como 30 días y un año como 365 días (no contempla bisiestos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS; CUPS; Rango de edad; Frecuencia de atención; Orden médica; Sexo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RIASCUPSD: Filtra por IDRIASCUPS = parámetro y ESTADO = 1, devolviendo edades convertidas a días según UNIDADRANGO (1=días, 2=meses*30, 3=años*365) y la edad máxima incrementada en una unidad de rango (+1 día, +30 días o +365 días).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UNIDADRANGO = 1 → EDADMINIMA_DIAS = EDADMINIMA; EDADMAXIMA_DIAS = EDADMAXIMA + 1; UnidadRangoEdad = ''Dias''; si UNIDADRANGO = 2 → EDADMINIMA_DIAS = EDADMINIMA*30; EDADMAXIMA_DIAS = EDADMAXIMA*30 + 30; UnidadRangoEdad = ''Meses''; si UNIDADRANGO = 3 → EDADMINIMA_DIAS = EDADMINIMA*365; EDADMAXIMA_DIAS = EDADMAXIMA*365 + 365; UnidadRangoEdad = ''Años''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPSD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS_GESTIONRIAS';
GO
