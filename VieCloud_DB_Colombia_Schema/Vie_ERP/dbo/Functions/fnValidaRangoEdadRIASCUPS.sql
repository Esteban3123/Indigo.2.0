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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que valida si la edad de un paciente (expresada en días) está dentro del rango de edad permitido para un servicio CUPS definido en una regla RIAS (Ruta Integral de Atención en Salud). Recibe como parámetros el identificador de la regla RIAS-CUPS y la edad del paciente en días, y consulta la tabla de detalle RIASCUPSD convirtiendo los rangos de edad mínima y máxima a días (admitiendo unidades en días, meses o años) para determinar si el paciente es elegible para el servicio. Retorna la regla RIAS aplicable que corresponde al rango etario del paciente, incluyendo datos como frecuencia de uso, cantidad por período y si requiere orden médica. Se utiliza en la validación de elegibilidad de servicios preventivos o de promoción de salud según la edad del afiliado, evitando solapamiento de rangos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnValidaRangoEdadRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnValidaRangoEdadRIASCUPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el rango de edad parametrizado (con su regla, frecuencia y requisitos de orden médica) aplicable a un CUPS dentro de una RIAS, según la edad del paciente expresada en días.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La edad del paciente debe expresarse previamente en días; Debe existir parametrización activa de rangos de edad para el RIASCUPS consultado; UNIDADRANGO debe tener un valor válido (1=Días, 2=Meses, 3=Años)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran rangos con ESTADO = 1 (activos); Se retorna a lo sumo un único rango (TOP 1) aun si existieran solapamientos en la parametrización; La edad recibida en días debe estar entre EDADMINIMA_DIAS y EDADMAXIMA_DIAS (inclusive) del rango parametrizado; La parametrización de CUPS por RIAS debe garantizar que no existan rangos de edad solapados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS; CUPS; Rango de edad; Frecuencia de atención; Orden médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.RIASCUPSD: Cuando IDRIASCUPS coincide y ESTADO=1 y la edad en días está entre EDADMINIMA_DIAS y EDADMAXIMA_DIAS, retorna el primer rango con sus reglas, frecuencia, cantidad por periodo y exigencia de orden médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UNIDADRANGO = 1 → El rango de edad se interpreta en días (EDADMINIMA y EDADMAXIMA se usan tal cual, sumando 1 al máximo); si UNIDADRANGO = 2 → El rango de edad se interpreta en meses y se convierte a días multiplicando por 30 (mínimo) y por 30 sumando 30 al máximo); si UNIDADRANGO = 3 → El rango de edad se interpreta en años y se convierte a días multiplicando por 365 (mínimo) y por 365 sumando 365 al máximo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPSD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnValidaRangoEdadRIASCUPS';
GO
