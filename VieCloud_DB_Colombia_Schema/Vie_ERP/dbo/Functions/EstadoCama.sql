

CREATE FUNCTION [dbo].[EstadoCama] (@CodigoEstadoCama as int)
RETURNS nvarchar (30)
AS
BEGIN

declare @NomEstadoCama nvarchar(30)

SELECT @NomEstadoCama = CASE @CodigoEstadoCama WHEN '1' THEN 'Libre' WHEN '2' THEN 'Asignada' WHEN '3' THEN 'Inactiva' WHEN '4' THEN 'Bloqueada' WHEN '5' THEN 'En aislamiento' WHEN '6' THEN 'Reservada sin confirmar' WHEN '7' THEN 'Reservada confirmada'  WHEN '8' THEN 'Asignada con reserva'  END

RETURN @NomEstadoCama

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte el código numérico del estado de una cama hospitalaria en su descripción legible en español. Recibe un número entero que representa el estado y devuelve uno de los siguientes valores: Libre, Asignada, Inactiva, Bloqueada, En aislamiento, Reservada sin confirmar, Reservada confirmada o Asignada con reserva. Se usa en consultas de gestión de camas y hospitalización para mostrar el estado actual de cada cama en lenguaje humano, evitando que las aplicaciones y reportes trabajen con códigos numéricos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoCama';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoCama';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de estado de cama a su descripción textual estandarizada para uso hospitalario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un código numérico de estado de cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de estados de cama es fijo y abarca solo los códigos 1 a 8; Cualquier código fuera del rango 1-8 retorna NULL; El nombre devuelto nunca excede 30 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Estado de cama; Asignación de cama; Reserva de cama; Aislamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Mapea el código de entrada a una de las ocho descripciones (Libre, Asignada, Inactiva, Bloqueada, En aislamiento, Reservada sin confirmar, Reservada confirmada, Asignada con reserva); si no coincide, retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si código = 1 → retorna ''Libre''; si código = 2 → retorna ''Asignada''; si código = 3 → retorna ''Inactiva''; si código = 4 → retorna ''Bloqueada''; si código = 5 → retorna ''En aislamiento''; si código = 6 → retorna ''Reservada sin confirmar''; si código = 7 → retorna ''Reservada confirmada''; si código = 8 → retorna ''Asignada con reserva''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCama';
GO
