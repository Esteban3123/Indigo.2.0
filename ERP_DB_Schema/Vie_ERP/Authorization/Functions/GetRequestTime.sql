-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-19
-- Description:	Devuelve el tiempo de la solicitud
-- =============================================
CREATE FUNCTION [Authorization].[GetRequestTime]
(
	@Status TINYINT,		-- Estado de la solicitud
	@Request INT,			-- Tiempo solicitud
	@Radicated INT,			-- Tiempo radicación
	@DeliveryService INT	-- Tiempo autorización
)
RETURNS int
AS
BEGIN
	RETURN	ISNULL(	CASE @Status
						WHEN 1 THEN @Request
						WHEN 2 THEN @Radicated
						WHEN 3 THEN @Radicated
						WHEN 5 THEN @DeliveryService
						WHEN 12 THEN @Request
						WHEN 13 THEN @Radicated
					END, 0)
END