





CREATE VIEW [Contract].[ViewListDefinitionRateDetailSurgicalProcedures]
AS

select drdsp.Id, drdsp.DefinitionRateDetailId,
drdsp.IPSServiceId, drdsp.SurgicalProcedureServiceId,
ips.Code IPSServiceCode, ips.Name IPSServiceName,
case ips.ServiceClass 
	when 1 then 'Ninguno' 
	when 2 then 'Cirujano' 
	when 3 then 'Anesteciologo' 
	when 4 then 'Ayudante' 
	when 5 then 'Derecho Sala' 
	when 6 then 'Materiales Sutura' 
	when 7 then 'Instrumentacion Quirurgica' 
	else ''
end ServiceClassName
from Contract.DefinitionRateDetailSurgicalProcedures drdsp
inner join Contract.IPSService ips on ips.Id = drdsp.IPSServiceId