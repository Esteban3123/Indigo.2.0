
CREATE VIEW [Contract].[ViewListIPSServiceWithHomologations]
AS

select CONCAT(ips.Id, ' - ', ISNULL(ch.Id, 0)) Row, 
	ips.Id, ips.Code, ips.Name, ips.ServiceManual, 
	case ips.ServiceManual 
		when 1 then 'ISS 2001' 
		when 2 then 'ISS 2004' 
		when 3 then 'SOAT' 
		when 4 then 'Institucional'
	else '' end ServiceManualName,
	ips.ServiceClass,
	case ips.ServiceClass 
		when 1 then 'Ninguno' 
		when 2 then 'Cirujano' 
		when 3 then 'Anestesiólogo' 
		when 4 then 'Ayudante' 
		when 5 then 'Derecho Sala' 
		when 6 then 'Materiales Sutura' 
		when 7 then 'Instrumentación Quirúrgica' 
	else '' end ServiceClassName,
	ips.Presentation, 
	case ips.Presentation 
		when 1 then 'No Quirúrgico' 
		when 2 then 'Quirúrgico' 
		when 3 then 'Paquete' 		
	else '' end PresentationName,
	0 SelectOption, ips.ServiceType, ips.Status, IIF(ips.Status = 1, 'Activo', 'Inactivo') StatusName,
	CONCAT(ips.Code, ' - ', ips.Name) CodeName, 
	ch.CupsEntityId, 
	ce.Code CupsEntityCode, 
	ce.Description CupsEntityName, 
	CONCAT(ce.Code, ' - ', ce.Description) CupsEntityCodeName,
	isnull(gli.Percentage, 0) AS IVAValue
from Contract.IPSService ips
left join Contract.CupsHomologation ch on ch.IPSServiceId = ips.Id
left join Contract.CUPSEntity ce on ce.Id = ch.CupsEntityId
LEFT JOIN GeneralLedger.GeneralLedgerIVA gli (nolock) ON ips.IVAId = gli.Id