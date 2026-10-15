-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-19
-- Description:	Devuelve la unidad de tiempo de la solicitud
-- =============================================
CREATE FUNCTION [Authorization].[GetRequestUnitTime]
(
	@Status TINYINT,				-- Estado de la solicitud
	@RequestUnit TINYINT,			-- Tiempo solicitud
	@RadicatedUnit TINYINT,			-- Tiempo radicación
	@DeliveryServiceUnit TINYINT	-- Tiempo autorización
)
RETURNS int
AS
BEGIN
	RETURN	ISNULL(	CASE @Status
						WHEN 1 THEN @RequestUnit
						WHEN 2 THEN @RadicatedUnit
						WHEN 3 THEN @RadicatedUnit
						WHEN 5 THEN @DeliveryServiceUnit
						WHEN 12 THEN @RequestUnit
						WHEN 13 THEN @RadicatedUnit
					END, 0)
END