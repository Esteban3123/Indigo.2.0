
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-19
-- Description:	Devuelve el tiempo transcurrido
-- =============================================
CREATE FUNCTION [Authorization].[GetRequestElapsedTime]
(
	@RequestDate DATETIME,				-- Estado de la solicitud
	@RequestUnitTime INT				-- Unidad de tiempo del proceso
)
RETURNS int
AS
BEGIN
	RETURN	ISNULL(	CASE @RequestUnitTime 
						WHEN 1 THEN DATEDIFF(MINUTE, @requestDate, GETDATE()) --Minutos
						WHEN 2 THEN DATEDIFF(HOUR, @requestDate, GETDATE()) --Horas
						WHEN 3 THEN DATEDIFF(DAY, @requestDate, GETDATE()) --Días
					END, 0)
END