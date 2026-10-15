

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 05/08/2020
-- Description:	Función para obtener el nombre del tipo de condición de la definición de tarifas
-- =============================================
CREATE FUNCTION [Contract].[fnGetConditionTypeName]
(
	@ConditionType tinyint
)
RETURNS varchar (50)
AS
BEGIN
	declare @ConditionTypeName varchar(50) = case @ConditionType
												when 1 then 'Horario'
												when 2 then 'Especialidad'
												when 3 then 'Unidad Funcional'
												when 4 then 'Tipo de Unidad'
												when 5 then 'Ninguna'
												when 6 then 'RIAS'
												when 7 then 'Descripción'
											end 

	RETURN @ConditionTypeName

END