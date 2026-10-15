

CREATE FUNCTION [dbo].[ClaseHabitacion] (@CodigoHabitacion as int)
RETURNS nvarchar (25)
AS
BEGIN

declare @NomClaseHabitacion nvarchar(25)

SELECT @NomClaseHabitacion = CASE @CodigoHabitacion WHEN '1' THEN 'Sala de observación' WHEN '2' THEN 'Sala de procedimientos' WHEN '3' THEN 'Sala de recuperación' WHEN '4' THEN 'Habitacion 1 cama' WHEN '5' THEN 'Habitacion 2 camas' WHEN '6' THEN 'Habitacion 3 camas' WHEN '7' THEN 'Habitacion 4 camas' WHEN '8' THEN 'Suite' WHEN '9' THEN 'Habitacion especial' WHEN '10' THEN 'Uci' WHEN '11' THEN 'Otro' END


RETURN @NomClaseHabitacion

END