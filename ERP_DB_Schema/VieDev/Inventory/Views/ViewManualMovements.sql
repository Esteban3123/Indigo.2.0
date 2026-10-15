




CREATE VIEW [Inventory].[ViewManualMovements]
AS

select	pdd.Id,
		f.Name as FunctionalUnit,
		pa.IPNOMCOMP as Patient,
		pa.IPCODPACI as PatientCode,
		pd.DocumentDate,
		concat(pd.CreationUser, ' ', p.Fullname) as CreationUser,
		pdd.ProductId,
		pdd.Quantity,
		pd.AdmissionNumber,
		ip.Code CodeProduct,
		concat('Salida Dispensacion: ', pd.Code) MovementType
	from Inventory.PharmaceuticalDispensing pd WITH(NOLOCK)
	inner join Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) on pd.Id = pdd.PharmaceuticalDispensingId
	inner join Payroll.FunctionalUnit f WITH(NOLOCK) on f.Id = pdd.FunctionalUnitId
	inner join ADINGRESO ad WITH(NOLOCK) on ad.NUMINGRES = pd.AdmissionNumber
	inner join INPACIENT pa WITH(NOLOCK) on pa.IPCODPACI = ad.IPCODPACI
	inner join Inventory.InventoryProduct ip WITH(NOLOCK) on pdd.ProductId= ip.Id
	left join [Security].[User] u on u.UserCode = pd.CreationUser
	left join [Security].Person p on p.Id = u.IdPerson
	where pd.EntityName = 'SavePharmaceuticalDispensing' OR pd.EntityName IS null
union all
select dd.Id,
		f.Name as FunctionalUnit,
		pa.IPNOMCOMP as Patient,
		pa.IPCODPACI as PatientCode,
		d.DocumentDate,
		concat(d.CreationUser, ' ', p.Fullname) as CreationUser,
		pdd.ProductId,
		dd.Quantity,
		d.AdmissionNumber,
		ip.Code CodeProduct,
		concat('Entrada Devolucion: ', d.Code) MovementType
from Inventory.PharmaceuticalDispensingDevolution d
inner join Inventory.PharmaceuticalDispensingDevolutionDetail dd on dd.PharmaceuticalDispensingDevolutionId = d.Id
inner join Inventory.PharmaceuticalDispensingDetailBatchSerial dbs on dbs.Id = dd.PharmaceuticalDispensingDetailBatchSerialId
inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = dbs.PharmaceuticalDispensingDetailId
inner join Payroll.FunctionalUnit f WITH(NOLOCK) on f.Id = pdd.FunctionalUnitId
inner join ADINGRESO ad WITH(NOLOCK) on ad.NUMINGRES = d.AdmissionNumber
inner join INPACIENT pa WITH(NOLOCK) on pa.IPCODPACI = ad.IPCODPACI
inner join Inventory.InventoryProduct ip WITH(NOLOCK) on pdd.ProductId= ip.Id
left join [Security].[User] u on u.UserCode = d.CreationUser
	left join [Security].Person p on p.Id = u.IdPerson
where  d.EntityName IS NULL