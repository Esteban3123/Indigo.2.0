-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 03-10-2017
-- Description:	Devuelve la categoria del tipo de unidad
-- =============================================
CREATE FUNCTION [Billing].[fnGetUnitType]
(
	@UnitType tinyint
)
RETURNS int
AS
BEGIN
	declare @UnitTypeFilter int
	if @UnitType = 1 or @UnitType = 23 begin
		set @UnitTypeFilter = 1 --1 - Urgencias
	end
	else if @UnitType = 2 or @UnitType = 5 or @UnitType = 6 or @UnitType = 7 or @UnitType = 8 or @UnitType = 9 or @UnitType = 10 or @UnitType = 11 or @UnitType = 16 or @UnitType = 17 or @UnitType = 18 begin
		set @UnitTypeFilter = 2 --2 - Hospitalizacion
	end
	else if @UnitType = 19 begin
		set @UnitTypeFilter = 3 --3 - Quirofanos
	end
	else begin
		set @UnitTypeFilter = 4 --4 - Servicios Ambulatorios
	end
	return @UnitTypeFilter
END