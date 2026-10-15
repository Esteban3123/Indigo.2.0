CREATE  VIEW [Billing].[ViewAdmissionsToAccountControlAmbulatory]
AS

	

select DISTINCT ing.NUMINGRES AdmissionNumber, 
ing.IFECHAING AdmissionDate, 
pac.IPCODPACI PatientIdentification, 
pac.IPNOMCOMP PatientName, 
RTRIM(LTRIM(pac.IPCODPACI)) + ' - ' + RTRIM(LTRIM(pac.IPNOMCOMP)) PatientDescription,
fu.UFUCODIGO FunctionalUnitCode, 
fu.UFUDESCRI FunctionalUnitName,
RTRIM(LTRIM(fu.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FuntionalUnitDescription,
cc.CODCENATE CareCenterCode,
cc.NOMCENATE CareCenterName,
RTRIM(LTRIM(cc.CODCENATE)) + ' - ' + RTRIM(LTRIM(cc.NOMCENATE)) CareCenterDescription
from .ADINGRESO ing with(nolock)
inner join .INPACIENT pac with(nolock) on pac.IPCODPACI = ing.IPCODPACI
inner join .INUNIFUNC fu with(nolock) on fu.UFUCODIGO = ing.UFUCODIGO
inner join .ADCENATEN cc with(nolock) on cc.CODCENATE = ing.CODCENATE
left join Billing.RevenueControl rc with(nolock) ON rc.AdmissionNumber = LTRIM(RTRIM(ing.NUMINGRES))
left join Billing.RevenueControlDetail rcd with(nolock) on rcd.RevenueControlId = rc.Id
left join (select sodd.RevenueControlDetailId, sod.* 
			from Billing.ServiceOrderDetailDistribution sodd with(nolock)
			inner join Billing.ServiceOrderDetail sod with(nolock) on sodd.ServiceOrderDetailId = sod.Id
			) sod1 on sod1.RevenueControlDetailId = rcd.Id
where (ing.IINGREPOR = 2 or ing.IINGREPOR = 3) and ing.IESTADOIN in (' ', 'P') and ing.CUPSPENDFAC = 1