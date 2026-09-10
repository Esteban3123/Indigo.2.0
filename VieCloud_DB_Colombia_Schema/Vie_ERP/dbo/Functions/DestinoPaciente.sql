

CREATE FUNCTION [dbo].[DestinoPaciente] (@CodigoDestinoPaciente as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @DestinoPaciente nvarchar (60)

SELECT @DestinoPaciente = CASE @CodigoDestinoPaciente WHEN 1 THEN 'TRASLADAR A URGENCIAS' WHEN 2 THEN 'TRASLADAR A OBSERVACIÓN URGENCIAS' WHEN 3 THEN 'TRASLADAR A HOSPITALIZACION' WHEN 4 THEN 'TRASLADAR A  UCI ADULTO' WHEN 5 THEN 'TRASLADAR A UCI PEDIATRICA' WHEN 6 THEN 'TRASLADAR A UCI NEONATAL' WHEN 7 THEN 'TRASLADAR A CONSULTA EXTERNA' WHEN 8 THEN 'TRASLADAR A  CIRUGÍA' WHEN 9 THEN 'HOSPITALIZACIÓN EN CASA' WHEN 10 THEN 'REFERENCIA' WHEN 11 THEN 'MORGUE' WHEN 12 THEN 'SALIDA' WHEN 13 THEN 'CONTINUA EN LA UNIDAD' 
WHEN 15 THEN 'Retiro Voluntario' when 16 then 'Fuga' when 17 then 'SALIDA PARCIAL' when 18 then 'ESTANCIA CONJUNTA CON LA MADRE' when 19 then 'U.CUIDADO INTERMEDIO' when 20 then 'U.BÁSICA' when 21 then 'HOSPITALIZACIÓN PEDIATRÍA' when 22 then 'PRE-ALTA HOSPITALARIA'  END 

RETURN @DestinoPaciente

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte un código numérico en la descripción textual del destino o traslado del paciente al finalizar una atención. Dado un código entero, retorna el nombre del servicio o área hacia donde se dirige el paciente, como Urgencias, Hospitalización, UCI Adulto, UCI Pediátrica, UCI Neonatal, Cirugía, Consulta Externa, Morgue, Salida, Referencia, entre otros. Se usa en reportes clínicos, historias de atención y RIPS para mostrar en lenguaje humano el resultado final del episodio asistencial (destino del paciente, traslado, egreso, alta, fuga, retiro voluntario).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DestinoPaciente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DestinoPaciente';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de destino de paciente a su descripción textual asociada (traslados, salidas, ubicaciones hospitalarias).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de destino debe corresponder a uno de los valores mapeados (1-13, 15-22); valores fuera de este conjunto retornan NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de destinos está embebido en la función (no se consulta tabla); El código 14 no está mapeado y retorna NULL; La descripción retornada no excede 60 caracteres; Es una función determinista: mismo código siempre produce la misma descripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Destino del paciente; Traslado hospitalario; Urgencias; Observación; Hospitalización; UCI adulto/pediátrica/neonatal; Consulta externa; Cirugía; Hospitalización en casa; Referencia; Morgue; Retiro voluntario; Fuga del paciente; Salida parcial; Estancia conjunta con la madre; Cuidado intermedio; Unidad básica; Pre-alta hospitalaria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando el código coincide con un valor del CASE, retorna la descripción correspondiente; en caso contrario retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si código = 1 → retorna ''TRASLADAR A URGENCIAS''; si código = 2 → retorna ''TRASLADAR A OBSERVACIÓN URGENCIAS''; si código = 3 → retorna ''TRASLADAR A HOSPITALIZACION''; si código = 4 → retorna ''TRASLADAR A UCI ADULTO''; si código = 5 → retorna ''TRASLADAR A UCI PEDIATRICA''; si código = 6 → retorna ''TRASLADAR A UCI NEONATAL''; si código = 7 → retorna ''TRASLADAR A CONSULTA EXTERNA''; si código = 8 → retorna ''TRASLADAR A CIRUGÍA''; si código = 9 → retorna ''HOSPITALIZACIÓN EN CASA''; si código = 10 → retorna ''REFERENCIA''; si código = 11 → retorna ''MORGUE''; si código = 12 → retorna ''SALIDA''; si código = 13 → retorna ''CONTINUA EN LA UNIDAD''; si código = 15 → retorna ''Retiro Voluntario''; si código = 16 → retorna ''Fuga''; si código = 17 → retorna ''SALIDA PARCIAL''; si código = 18 → retorna ''ESTANCIA CONJUNTA CON LA MADRE''; si código = 19 → retorna ''U.CUIDADO INTERMEDIO''; si código = 20 → retorna ''U.BÁSICA''; si código = 21 → retorna ''HOSPITALIZACIÓN PEDIATRÍA''; si código = 22 → retorna ''PRE-ALTA HOSPITALARIA'' else retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoPaciente';
GO
