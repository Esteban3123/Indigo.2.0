

CREATE FUNCTION [dbo].[ClaseHabitacion] (@CodigoHabitacion as int)
RETURNS nvarchar (25)
AS
BEGIN

declare @NomClaseHabitacion nvarchar(25)

SELECT @NomClaseHabitacion = CASE @CodigoHabitacion WHEN '1' THEN 'Sala de observación' WHEN '2' THEN 'Sala de procedimientos' WHEN '3' THEN 'Sala de recuperación' WHEN '4' THEN 'Habitacion 1 cama' WHEN '5' THEN 'Habitacion 2 camas' WHEN '6' THEN 'Habitacion 3 camas' WHEN '7' THEN 'Habitacion 4 camas' WHEN '8' THEN 'Suite' WHEN '9' THEN 'Habitacion especial' WHEN '10' THEN 'Uci' WHEN '11' THEN 'Otro' END

RETURN @NomClaseHabitacion

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte un código numérico de tipo de habitación en su nombre descriptivo legible. Dado un número del 1 al 11, retorna el nombre correspondiente: Sala de observación, Sala de procedimientos, Sala de recuperación, Habitación 1 cama, Habitación 2 camas, Habitación 3 camas, Habitación 4 camas, Suite, Habitación especial, UCI u Otro. Se usa para mostrar la clase o categoría de habitación hospitalaria en reportes, listados de camas y procesos de admisión o ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ClaseHabitacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ClaseHabitacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de habitación a su descripción textual estandarizada para uso hospitalario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseHabitacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de clases de habitación está hardcodeado con 11 valores fijos (1 al 11); Si el código no está en el rango definido, retorna NULL; El nombre devuelto nunca excede 25 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseHabitacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Clase de habitación; Sala de observación; Sala de procedimientos; Sala de recuperación; Suite; UCI (Unidad de Cuidados Intensivos); Habitación hospitalaria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseHabitacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si código = 1 → Devuelve ''Sala de observación''; si código = 2 → Devuelve ''Sala de procedimientos''; si código = 3 → Devuelve ''Sala de recuperación''; si código = 4 → Devuelve ''Habitacion 1 cama''; si código = 5 → Devuelve ''Habitacion 2 camas''; si código = 6 → Devuelve ''Habitacion 3 camas''; si código = 7 → Devuelve ''Habitacion 4 camas''; si código = 8 → Devuelve ''Suite''; si código = 9 → Devuelve ''Habitacion especial''; si código = 10 → Devuelve ''Uci''; si código = 11 → Devuelve ''Otro'' else Devuelve NULL si el código no coincide con ninguno de los valores 1-11', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseHabitacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseHabitacion';
GO
