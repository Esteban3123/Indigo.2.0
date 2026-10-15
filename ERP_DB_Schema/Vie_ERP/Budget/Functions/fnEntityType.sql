
create FUNCTION [Budget].[fnEntityType] (@EntityTypeId as int)
RETURNS nvarchar (30)
AS
BEGIN

declare @EntityTypeDescription nvarchar(50)

SELECT @EntityTypeDescription = case  @EntityTypeId when '1' then 'EPS Contributivo' when '2' then 'EPS Subsidiado' when '3' then 'ET Vinculados Municipios'
when '4' then  'ET Vinculados Departamentos' when '5' then 'ARL Riesgos Laborales' when '6' then 'MP Medicina Prepagada'
when '7' then 'IPS Privada' when '8' then 'IPS Publica' when '9' then 'Regimen Especial' when '10' then 'Accidentes de transito'
when '11' then 'Fosyga' when '12' then 'Otros' when '99' then 'Particulares' end

RETURN @EntityTypeDescription

END