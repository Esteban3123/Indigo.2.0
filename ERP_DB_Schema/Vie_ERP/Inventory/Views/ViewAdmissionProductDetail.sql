



CREATE VIEW [Inventory].[ViewAdmissionProductDetail]
AS
select pd.Id as HeaderId, pdd.id as DetailId , pd.AdmissionNumber,pdd.FunctionalUnitId, pdd.ProductId, 1 as DispensingType, pd.Code , pd.DocumentDate ,pd.CreationUser,pdd.Quantity,  RTRIM ( tp.Nit) +' - '+ RTRIM ( tp.Name) as  HealthProfessionalNitName  
from Inventory.PharmaceuticalDispensing pd with (nolock)
inner join Inventory.PharmaceuticalDispensingDetail pdd with (nolock) on pdd.PharmaceuticalDispensingId = pd.id 
inner join Common.ThirdParty tp  with (nolock) on pdd.OrderedHealthProfessionalThirdPartyId = tp.id
where pd.Status = 2
union all

select pdd.Id as HeaderId,pddd.id as DetailId , pd.AdmissionNumber,pddet.FunctionalUnitId, pddet.ProductId, 2 as DispensingType ,pdd.Code , pdd.DocumentDate ,pdd.CreationUser,pddd.Quantity, RTRIM ( tp.Nit) +' - '+ RTRIM ( tp.Name) as  HealthProfessionalNitName
from Inventory.PharmaceuticalDispensingDevolution pdd with (nolock) 
inner join Inventory.PharmaceuticalDispensingDevolutionDetail pddd  with (nolock) on pddd.PharmaceuticalDispensingDevolutionId = pdd.id 
inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs with (nolock) on pddd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.id 
inner join Inventory.PharmaceuticalDispensingDetail pddet with (nolock) on pddbs.PharmaceuticalDispensingDetailId = pddet.Id 
inner join Inventory.PharmaceuticalDispensing pd with (nolock) on pddet.PharmaceuticalDispensingId = pd.id
inner join Common.ThirdParty tp with (nolock) on pddet.OrderedHealthProfessionalThirdPartyId = tp.id
where pdd.Status = 2