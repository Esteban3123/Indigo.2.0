

CREATE view [MixingStation].[ViewListApplicationDetail] 
as 

--select concat('HCFARMEPD', '-', d.ID) Row, d.ID Id,
--LTRIM(RTRIM(d.IPCODPACI)) PatientCode, LTRIM(RTRIM(pat.IPNOMCOMP)) PatientName, LTRIM(RTRIM(d.IPCODPACI)) + ' - ' + LTRIM(RTRIM(pat.IPNOMCOMP)) PatientCodeName,
--RTRIM(LTRIM(d.CODCENATE)) CareCenterCode, RTRIM(LTRIM(cc.NOMCENATE)) CareCenterName, RTRIM(LTRIM(d.CODCENATE)) + ' - ' + RTRIM(LTRIM(cc.NOMCENATE)) CareCenterCodeName,
--RTRIM(LTRIM(d.UFUCODIGO)) FunctionalUnitCode, RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitName, RTRIM(LTRIM(d.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitCodeName,
--d.CODPRODUC ProductCode, d.CANPEDPRO Quantity, ISNULL(d.DOSISPROD, 0) Dosage, d.CODUNIMED MeasurementUnitCode, d.OBSERVATIONSCM Observations,
--1 SourceType, ISNULL(cama.NUMCAMHOS, '') Bed, 0 AdministrationRouteId, 'INTRAVENOSA' AdministrationRouteCodeName,
--ISNULL(h.PREESTADO, 0) StatusHCPRESCRA,
--	case ISNULL(h.PREESTADO, 0)
--		when 3 then 'Tratamiento Descontinuado'
--		when 4 then 'Tratamiento Suspendido'
--		when 7 then 'Tratamiento Terminado por Salida del Paciente'
--		else ''
--	end StatusNameHCPRESCRA
--from dbo.HCFARMEPD d
--inner join dbo.HCFARMEPC c on c.CODCONCEC = d.CODCONCEC
--inner join dbo.ADINGRESO ing on c.NUMINGRES = ing.NUMINGRES
--inner join dbo.INPACIENT pat on pat.IPCODPACI = d.IPCODPACI
--inner join dbo.ADCENATEN cc on cc.CODCENATE = d.CODCENATE
--inner join dbo.INUNIFUNC fu on fu.UFUCODIGO = d.UFUCODIGO
--left join CHCAMASHO cama ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = cama.CODICAMAS
--left join (
--	select a.IPCODPACI, a.NUMINGRES, a.CODPRODUC, MAX(a.ID) HCPRESCRAId
--	from .HCPRESCRA a 
--	inner join .HCPRESCRD d on d.IPCODPACI = a.IPCODPACI and d.CODPRODUC = a.CODPRODUC and d.NUMINGRES = a.NUMINGRES
--	where d.TRATMODIF = 1
--	group by a.IPCODPACI, a.NUMINGRES, a.CODPRODUC
--) res on res.IPCODPACI = d.IPCODPACI and d.NUMINGRES = d.NUMINGRES and res.CODPRODUC = d.CODPRODUC
--left join .HCPRESCRA h WITH(NOLOCK) on h.ID = res.HCPRESCRAId
--where d.SENDTO in (1,3) and d.VIEPROCESSED = 0

select concat('PharmaDose', '-', phd.Id) Row, phd.GroupingCodeDose Id,
	LTRIM(RTRIM(pat.IPCODPACI)) PatientCode, LTRIM(RTRIM(pat.IPNOMCOMP)) PatientName, LTRIM(RTRIM(pat.IPCODPACI)) + ' - ' + LTRIM(RTRIM(pat.IPNOMCOMP)) PatientCodeName,
	RTRIM(LTRIM(hcp.CODCENATE)) CareCenterCode, RTRIM(LTRIM(cc.NOMCENATE)) CareCenterName, RTRIM(LTRIM(hcp.CODCENATE)) + ' - ' + RTRIM(LTRIM(cc.NOMCENATE)) CareCenterCodeName,
	RTRIM(LTRIM(hcp.UFUCODIGO)) FunctionalUnitCode, RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitName, RTRIM(LTRIM(hcp.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitCodeName,
	phd.ProductCode ProductCode, 1 Quantity, ISNULL(phd.Dose, 0) Dosage, phd.MeasurementUnitCode MeasurementUnitCode, '' Observations,
	1 SourceType, ISNULL(cama.NUMCAMHOS, '') Bed, 0 AdministrationRouteId, 'INTRAVENOSA' AdministrationRouteCodeName,
	ISNULL(h.PREESTADO, 0) StatusHCPRESCRA,
	case ISNULL(h.PREESTADO, 0)
		when 3 then 'Tratamiento Descontinuado'
		when 4 then 'Tratamiento Suspendido'
		when 7 then 'Tratamiento Terminado por Salida del Paciente'
		else ''
	end StatusNameHCPRESCRA
from MedicalHistory.PharmaDose phd
Inner Join MedicalHistory.ProductSusceptibleMixingStation psms with(nolock) on psms.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation
Left Join HCFARMEPC hcp with(nolock) on phd.IDHCFARMEPC = hcp.CODCONCEC
Left join dbo.ADINGRESO ing on hcp.NUMINGRES = ing.NUMINGRES
Left join dbo.INPACIENT pat on pat.IPCODPACI = hcp.IPCODPACI
Left join dbo.ADCENATEN cc on cc.CODCENATE = hcp.CODCENATE
Left join dbo.INUNIFUNC fu on fu.UFUCODIGO = hcp.UFUCODIGO
left join CHCAMASHO cama ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = cama.CODICAMAS
left join HCPRESCRA h on psms.Origin = 'HCPRESCRA' And psms.IdOrigin = h.ID

union all

select concat('RequestUnitDoseExternalCareCenterPatientDetails', '-', rpd.Id) Row, Cast(rpd.Id as Varchar(11)) Id,
pecc.IdentificationNumber PatientCode, pecc.Name + ' ' + pecc.LastName PatientName, pecc.IdentificationNumber + ' - ' + pecc.Name + ' ' + pecc.LastName PatientCodeName,
ecc.Code CareCenterCode, ecc.Description CareCenterName, ecc.Code + ' - ' + ecc.Description CareCenterCodeName,
rp.ExternalFunctionalUnitCode FunctionalUnitCode, '' FunctionalUnitName, rp.ExternalFunctionalUnitCode FunctionalUnitCodeName,
a.Code ProductCode, rpd.Quantity Quantity, rpd.Dosage Dosage, mu.Code MeasurementUnitCode, rpd.Observations,
2 SourceType,
rp.Bed,
rpd.AdministrationRouteId, ar.Code + ' - ' + ar.Name AdministrationRouteCodeName, '' StatusHCPRESCRA, '' StatusNameHCPRESCRA
from MixingStation.RequestUnitDoseExternalCareCenterPatientDetails rpd
inner join MixingStation.RequestUnitDoseExternalCareCenterPatient rp on rp.Id = rpd.RequestUnitDoseExternalCareCenterPatientId
inner join MixingStation.RequestUnitDoseExternalCareCenter rcc on rcc.Id = rp.RequestUnitDoseExternalCareCenterId
inner join MixingStation.PatientExternalCareCenter pecc on pecc.Id = rp.PatientExternalCareCenterId
inner join MixingStation.ExternalCareCenter ecc on ecc.Id = rcc.ExternalCareCenterId
inner join Inventory.ATC a on a.Id = rpd.ATCId
inner join Inventory.InventoryMeasurementUnit mu on mu.Id = rpd.MeasurementUnitId
inner join Inventory.AdministrationRoute ar on ar.Id = rpd.AdministrationRouteId
where rpd.SendTo is null