

CREATE FUNCTION [dbo].[EstadoCama] (@CodigoEstadoCama as int)
RETURNS nvarchar (30)
AS
BEGIN

declare @NomEstadoCama nvarchar(30)

SELECT @NomEstadoCama = CASE @CodigoEstadoCama WHEN '1' THEN 'Libre' WHEN '2' THEN 'Asignada' WHEN '3' THEN 'Inactiva' WHEN '4' THEN 'Bloqueada' WHEN '5' THEN 'En aislamiento' WHEN '6' THEN 'Reservada sin confirmar' WHEN '7' THEN 'Reservada confirmada'  WHEN '8' THEN 'Asignada con reserva'  END


RETURN @NomEstadoCama

END