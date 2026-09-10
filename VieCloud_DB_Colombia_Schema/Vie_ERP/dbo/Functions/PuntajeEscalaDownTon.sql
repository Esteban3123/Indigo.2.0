CREATE FUNCTION [dbo].[PuntajeEscalaDownTon]
(
	-- Add the parameters for the function here
	@NumeroIngreso char(10),
	@CodigoPaciente varchar(25)
)
RETURNS int
AS
BEGIN

DECLARE @Resultado int

set @Resultado = ( select top 1 RESULTADO from 
							(
									SELECT TOP 1  AUTO as 'Organizador', ( EDADPACIE + CAIDPREVI + TRANQUILI + DIURETICO + HIPOTENSO + ANTIPARKI +  ANTIDEPRE + ALTERAVIS +  ALTERAUDI + ICTUEXTRE +  ESTADOMEN + SEGURAYUD +  INSEGAYUD + IMPOSIBLE +  PATOLOGIA + NUTRICION + SELECIMED ) as RESULTADO 
									FROM  HCESCDOWN  a 
									WHERE  a.NUMINGRES = @NumeroIngreso AND a.IPCODPACI =@CodigoPaciente 
									order by AUTO DESC 
								union ALL
									select top 1 ID as 'Organizador', RESULTADO 
									FROM  HCESCALAS b 
									WHERE   b.NUMINGRES = @NumeroIngreso AND b.IPCODPACI =@CodigoPaciente and b.TIPOESCALA = '113'  
									order by ID DESC 
							) x order by Organizador DESC 
)

	RETURN @Resultado

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el puntaje total de la Escala de Downton para evaluación de riesgo de caídas de un paciente en un ingreso específico. Recibe como parámetros el número de ingreso y la cédula del paciente, y retorna un valor entero que representa el nivel de riesgo. Para obtener el resultado, primero intenta sumar los factores individuales registrados en la tabla HCESCDOWN (edad, caídas previas, uso de tranquilizantes, diuréticos, hipotensores, antiparkinsonianos, antidepresivos, alteraciones visuales y auditivas, ictus en extremidades, estado mental y tipo de marcha), y como segunda fuente consulta la tabla HCESCALAS buscando la escala de tipo ''113''; finalmente devuelve el registro más reciente disponible entre ambas fuentes. Es utilizada para determinar el nivel de riesgo de caída del paciente y apoyar decisiones clínicas de seguridad durante la hospitalización o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaDownTon';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PuntajeEscalaDownTon';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el puntaje más reciente de la escala Downton (riesgo de caídas) de un paciente en un ingreso, consolidando dos fuentes de registro posibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaDownTon';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de la escala Downton para el paciente e ingreso, ya sea en HCESCDOWN o en HCESCALAS con TIPOESCALA=''113''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaDownTon';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la escala Downton, identificada por TIPOESCALA=''113'' en HCESCALAS.; Siempre se retorna un único valor entero correspondiente al registro más reciente por identificador.; El cálculo desde HCESCDOWN suma exactamente 17 factores de riesgo de caída.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaDownTon';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Escala Downton; Riesgo de caídas; Paciente; Ingreso hospitalario; Caídas previas; Medicación (tranquilizantes, diuréticos, hipotensores, antiparkinsonianos, antidepresivos); Alteraciones visuales y auditivas; Estado mental; Patología; Nutrición', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaDownTon';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve el RESULTADO del registro con mayor ''Organizador'' (AUTO en HCESCDOWN o ID en HCESCALAS) entre ambas fuentes para el paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaDownTon';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen HCESCDOWN: registro existente para NUMINGRES e IPCODPACI → Calcula RESULTADO como suma de 17 ítems clínicos (EDADPACIE, CAIDPREVI, TRANQUILI, DIURETICO, HIPOTENSO, ANTIPARKI, ANTIDEPRE, ALTERAVIS, ALTERAUDI, ICTUEXTRE, ESTADOMEN, SEGURAYUD, INSEGAYUD, IMPOSIBLE, PATOLOGIA, NUTRICION, SELECIMED) tomando el de mayor AUTO; si Origen HCESCALAS con TIPOESCALA=''113'' para NUMINGRES e IPCODPACI → Toma el RESULTADO ya calculado del registro de mayor ID; si Existen registros en ambas fuentes → Selecciona el de mayor valor de Organizador (AUTO vs ID) como resultado final', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaDownTon';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESCDOWN; dbo.HCESCALAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaDownTon';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PuntajeEscalaDownTon';
GO
