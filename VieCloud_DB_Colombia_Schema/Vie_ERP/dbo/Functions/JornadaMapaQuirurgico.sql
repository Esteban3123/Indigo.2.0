
CREATE FUNCTION [dbo].[JornadaMapaQuirurgico] (@HoraTabla AS NVARCHAR(5)
,@HoraInicialManana AS NVARCHAR(5),@HoraFinalManana AS NVARCHAR(5)
,@HoraInicialTarde AS NVARCHAR(5),@HoraFinalTarde AS NVARCHAR(5)
,@HoraInicialNoche AS NVARCHAR(5),@HoraFinalNoche AS NVARCHAR(5))
RETURNS nvarchar (25)
AS
BEGIN

declare @Jornada nvarchar(25)

IF @HoraTabla BETWEEN @HoraInicialManana AND @HoraFinalManana
	SET @Jornada ='1. Mañana'
ELSE IF @HoraTabla BETWEEN @HoraInicialTarde AND @HoraFinalTarde 
	SET @Jornada ='2. Tarde'
ELSE IF @HoraTabla BETWEEN @HoraInicialNoche AND @HoraFinalNoche
	SET @Jornada ='3. Noche'
ELSE
	 SET @Jornada ='Jornada no estipulada'
   
RETURN @Jornada

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina la jornada quirúrgica (Mañana, Tarde o Noche) a la que pertenece una cirugía o procedimiento en el mapa quirúrgico, según la hora de la tabla comparada con los rangos horarios configurados para cada jornada. Recibe una hora de referencia y los límites de inicio y fin de cada jornada del día, devolviendo una etiqueta legible como ''1. Mañana'', ''2. Tarde'' o ''3. Noche''. Se usa para clasificar y agrupar las cirugías programadas en el quirófano por franja horaria, facilitando la organización y visualización del mapa quirúrgico diario. Si la hora no cae en ningún rango definido, retorna ''Jornada no estipulada''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'JornadaMapaQuirurgico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'JornadaMapaQuirurgico';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica una hora dada en una de tres jornadas (mañana, tarde, noche) según rangos horarios parametrizados, usado en el contexto del mapa quirúrgico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'JornadaMapaQuirurgico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las horas deben venir en formato comparable como cadena (HH:MM) para que BETWEEN funcione correctamente; Los rangos de jornadas no deben solaparse para obtener una clasificación determinista', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'JornadaMapaQuirurgico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna una cadena no nula con uno de cuatro valores posibles; La evaluación es secuencial: prevalece mañana sobre tarde y tarde sobre noche en caso de solapamiento; Cuando la hora no cae en ningún rango definido, se etiqueta explícitamente como ''Jornada no estipulada''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'JornadaMapaQuirurgico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Jornada laboral (mañana, tarde, noche); Mapa quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'JornadaMapaQuirurgico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Si la hora está entre inicio y fin de mañana retorna ''1. Mañana''; si entre tarde retorna ''2. Tarde''; si entre noche retorna ''3. Noche''; en caso contrario retorna ''Jornada no estipulada''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'JornadaMapaQuirurgico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Hora dentro del rango matutino → Clasifica como ''1. Mañana'' else Evalúa el rango de tarde; si Hora dentro del rango vespertino → Clasifica como ''2. Tarde'' else Evalúa el rango nocturno; si Hora dentro del rango nocturno → Clasifica como ''3. Noche'' else Clasifica como ''Jornada no estipulada''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'JornadaMapaQuirurgico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'JornadaMapaQuirurgico';
GO
