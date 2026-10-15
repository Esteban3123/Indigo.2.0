
create FUNCTION [Budget].[fnCollectionType] (@CollectionTypeId as int)
RETURNS nvarchar (30)
AS
BEGIN

declare @CollectionTypeDescription nvarchar(50)

SELECT @CollectionTypeDescription = case  @CollectionTypeId when  '1'  then 'Cuotas a cargo de Pacientes'
when '2' then 'Cuotas a cargo de Pacientes' when '3' then 'Ventas de Contado Servicios' 
when '7' then 'Ventas de Contado Productos' else 'Otros' end

RETURN @CollectionTypeDescription

END