
CREATE FUNCTION [dbo].[AG_Estado] (@Estado as int)
RETURNS varchar (15)
AS
BEGIN

declare @grupo varchar(15)
SET @grupo=
     CASE WHEN @Estado=0 THEN 'Asignada' 
          WHEN @Estado=1 THEN 'Cumplida' 
		  WHEN @Estado=2 THEN 'Incumplida'
		  WHEN @Estado=3 THEN 'Preasignada'
	 END 
RETURN @grupo
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte el código numérico del estado de una cita o agenda en su descripción legible en español. Recibe un número entero (0, 1, 2 o 3) y retorna el texto correspondiente: Asignada, Cumplida, Incumplida o Preasignada. Se utiliza en el módulo de agendamiento para mostrar el estado actual de una cita médica de forma comprensible para el usuario, evitando mostrar códigos numéricos en reportes y consultas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AG_Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AG_Estado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de estado de cita/agenda a su descripción textual correspondiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de estado recibido debe estar en el rango 0-3 para obtener una descripción; otros valores devuelven NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de estados reconocidos se limita a cuatro valores: Asignada, Cumplida, Incumplida y Preasignada.; La descripción retornada nunca excede 15 caracteres.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estado de cita de agenda; Asignada; Cumplida; Incumplida; Preasignada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Estado = 0 → Devuelve ''Asignada''; si Estado = 1 → Devuelve ''Cumplida''; si Estado = 2 → Devuelve ''Incumplida''; si Estado = 3 → Devuelve ''Preasignada'' else Devuelve NULL para cualquier otro valor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Estado';
GO
