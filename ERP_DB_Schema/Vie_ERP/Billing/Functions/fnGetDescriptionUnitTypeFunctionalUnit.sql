-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 07/11/2019
-- Description:	Devuelve la descripción del tipo de unidad
-- =============================================
CREATE FUNCTION [Billing].[fnGetDescriptionUnitTypeFunctionalUnit]
(
	@UnitType tinyint
)
RETURNS varchar(50)
AS
BEGIN
	declare @DescriptionUnitType varchar(50) = ''
	
	set @DescriptionUnitType = case @UnitType
								   when 1 then 'Urgencias'
								   when 2 then 'Hospitalizacion'
								   when 3 then 'Apoyo Dx'
								   when 4 then 'Apoyo Terapeutico'
								   when 5 then 'Unidades de Cuidado Intensivo Adulto'
								   when 6 then 'Unidades de Cuidado Intermedio Adulto'
								   when 7 then 'Unidades de Cuidado Intensivo Pediatrica'
								   when 8 then 'Unidades de Cuidado Intermedio Pediatrica'
								   when 9 then 'Unidades de Cuidado Intensivo Neonatal'
								   when 10 then 'Unidades de Cuidado Intermedio Neonatal'
								   when 11 then 'Unidades de Cuidado Basico Neonatal'
								   when 12 then 'Unidad Renal'
								   when 13 then 'Unidad Oncologica'
								   when 14 then 'Unidad Medicina Nuclear'
								   when 15 then 'Consulta Externa'
								   when 16 then 'Unidad Mental'
								   when 17 then 'Unidad de Quemados'
								   when 18 then 'Unidad de Cuidado Paliativo'
								   when 19 then 'Cirugia'
								   when 20 then 'Laboratorio'
								   when 21 then 'Cardiologia No Invasiva'
								   when 22 then 'Cardiologia Invasiva'
								   when 23 then 'Gineco-Obstetricia'
								   when 24 then 'Consulta Externa - Gineco-Obstetricia'
								   when 30 then 'Otras'
								   when 31 then 'Consulta Prioritaria'
								   else ''
							   end

	return @DescriptionUnitType
END