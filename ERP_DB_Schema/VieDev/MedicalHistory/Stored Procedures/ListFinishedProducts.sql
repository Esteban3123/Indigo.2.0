
CREATE PROCEDURE [MedicalHistory].[ListFinishedProducts]
@TipoListar integer,
@IdOrigin varchar(25), --Puede venir el codigo del producto en caso de medicamentos ó en caso de mezclas el id origen que es el de la orden 
@Paciente varchar(25),
@Ingreso char(10),
@CargoMedicamentosQuimioterapia bit

AS
BEGIN
	SET NOCOUNT ON;

if @TipoListar =  1 begin --Medicamentos
	if @CargoMedicamentosQuimioterapia = 0 begin --Medicamentos hospitalarios 	
		select distinct cast(0 as bit) 'Seleccionar', cum.Id as IdDetailPhysicalCUM, p.MainDrugCode, p.FullProductName, cum.BatchCode, AppliedDose, case AppliedDose when 0 then 'Pendiente' when 1 then 'Aplicado' end as EstadoAplicacion  
		from MedicalHistory.ProductSusceptibleMixingStation p 
			inner join HCPRESCRA a on p.IdOrigin = a.ID and p.Origin = 'HCPRESCRA' and a.IPCODPACI = @Paciente
			inner join MedicalHistory.PharmaDose d on p.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation  --and p.MainDrugCode = d.ProductCode --and AppliedDose = 0
			inner join MedicalHistory.DetailPhysicalCUM cum ON cum.GroupingCodeDose = D.GroupingCodeDose   --and UsedQuantity  < cum.DispensedQuantity  
			inner join Inventory.InventoryProduct pro on  pro.Id = cum.ProductId 
			inner join Inventory.ATC atc on  atc.Id = pro.ATCId  
			INNER join HCFISIPRO fis on fis.ID = cum.IDHCFISIPRO AND fis.IPCODPACI =  @Paciente AND TIPREGIST = 1 
			where a.IPCODPACI = @Paciente and d.ProductCode = @IdOrigin
			AND NOT EXISTS(SELECT top 1 IdDetailPhysicalCUM FROM HCDEVMEDD dev WHERE dev.IdDetailPhysicalCUM = cum.Id AND PROESTADO IN ('2'))
		ORDER BY cum.BatchCode DESC
	end
	else begin --Medicamentos quimioterapia
		select distinct cast(0 as bit) 'Seleccionar', cum.Id as IdDetailPhysicalCUM, p.MainDrugCode, p.FullProductName, cum.BatchCode, AppliedDose, case AppliedDose when 0 then 'Pendiente' when 1 then 'Aplicado' end as EstadoAplicacion  
		from MedicalHistory.ProductSusceptibleMixingStation p 
			inner join EHR.HCORDMEDICAM a on p.IdOrigin = a.ID and p.Origin = 'HCORDMEDICAM'--and a.IPCODPACI = @Paciente
			inner join MedicalHistory.PharmaDose d on p.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation  --and p.MainDrugCode = d.ProductCode --and AppliedDose = 0
			inner join MedicalHistory.DetailPhysicalCUM cum ON cum.GroupingCodeDose = D.GroupingCodeDose   --and UsedQuantity  < cum.DispensedQuantity  
			inner join Inventory.InventoryProduct pro on  pro.Id = cum.ProductId 
			inner join Inventory.ATC atc on  atc.Id = pro.ATCId  
			INNER join HCFISIPRO fis on fis.ID = cum.IDHCFISIPRO AND fis.IPCODPACI =  @Paciente AND TIPREGIST = 1 
			where d.ProductCode = @IdOrigin
			AND NOT EXISTS(SELECT top 1 IdDetailPhysicalCUM FROM HCDEVMEDD dev WHERE dev.IdDetailPhysicalCUM = cum.Id AND PROESTADO IN ('2'))
		ORDER BY cum.BatchCode DESC
	end
end else if @TipoListar =  2 begin --Mezclas / Liquidos
	
	declare @IDHCINFLIQA AS INTEGER = (SELECT CONSECUTI FROM HCINFLIQA WHERE CODCONCEC_ORIGEN = @IdOrigin )
	select distinct cast(0 as bit) 'Seleccionar', cum.Id as IdDetailPhysicalCUM, p.MainDrugCode, p.FullProductName, cum.BatchCode, AppliedDose, case AppliedDose when 0 then 'Pendiente' when 1 then 'Aplicado' end as EstadoAplicacion  
	from MedicalHistory.ProductSusceptibleMixingStation p 
		inner join MedicalHistory.PharmaDose d on p.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation  --and p.MainDrugCode = d.ProductCode --and AppliedDose = 0
		inner join MedicalHistory.DetailPhysicalCUM cum ON cum.GroupingCodeDose = D.GroupingCodeDose   --and UsedQuantity  < cum.DispensedQuantity  
		inner join Inventory.InventoryProduct pro on  pro.Id = cum.ProductId 
		inner join Inventory.ATC atc on  atc.Id = pro.ATCId  
		INNER join HCFISIPRO fis on fis.ID = cum.IDHCFISIPRO AND IPCODPACI =  @Paciente AND TIPREGIST = 1 
	    where p.IdOrigin = @IDHCINFLIQA
		AND NOT EXISTS(SELECT top 1 IdDetailPhysicalCUM FROM HCDEVMEDD dev WHERE dev.IdDetailPhysicalCUM = cum.Id AND PROESTADO IN ('2'))
	ORDER BY cum.BatchCode DESC	
end else begin --Nutricion parenteral

 select distinct cast(0 as bit) 'Seleccionar', cum.Id as IdDetailPhysicalCUM, p.MainDrugCode, p.FullProductName, cum.BatchCode, AppliedDose, case AppliedDose when 0 then 'Pendiente' when 1 then 'Aplicado' end as EstadoAplicacion  
	from MedicalHistory.ProductSusceptibleMixingStation p 
		inner join MedicalHistory.PharmaDose d on p.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation  --and p.MainDrugCode = d.ProductCode --and AppliedDose = 0
		inner join MedicalHistory.DetailPhysicalCUM cum ON cum.GroupingCodeDose = D.GroupingCodeDose   --and UsedQuantity  < cum.DispensedQuantity  
		inner join Inventory.InventoryProduct pro on  pro.Id = cum.ProductId 
		inner join Inventory.ATC atc on  atc.Id = pro.ATCId  
		INNER join HCFISIPRO fis on fis.ID = cum.IDHCFISIPRO AND IPCODPACI =  @Paciente AND TIPREGIST = 1 
	    where p.IdOrigin = @IdOrigin
		AND NOT EXISTS(SELECT top 1 IdDetailPhysicalCUM FROM HCDEVMEDD dev WHERE dev.IdDetailPhysicalCUM = cum.Id AND PROESTADO IN ('2'))
	ORDER BY cum.BatchCode DESC
end
END


/*

select CONSECUTI, * from HCINFLIQA where CONSECUTI = 692
select IdOrigin, * from MedicalHistory.ProductSusceptibleMixingStation where IdOrigin = 692
select GroupingCodeDose, * from MedicalHistory.PharmaDose where CodeSusceptibleMixingStation = 'B2F1C5BE-231D-47A7-818B-3F469A9A1BC6'
select * from MedicalHistory.DetailPhysicalCUM where GroupingCodeDose = 'AA94A7D1-75C2-44F0-8A36-7580317BF0A3' 
select * from MedicalHistory.DetailPhysicalCUM where GroupingCodeDose = '06FA8984-9369-434F-97EE-152A4FC0404B' 
select * from HCFISIPRO where IPCODPACI = '0101'

select * from HCINFLIQC where IPCODPACI = '0101'  and CODCONCEC = '82808'--Cabecera
select * from HCINFCONC where CODCONCEC = '82808' --Medicamentos
select * from HCINFLIQD where CODCONCEC = '82808' --Vehiculo - Diluyente
select * from HCINFLIQA  where CODCONCEC = '82808'  --Administrción - informacion generica  --692

select * from HCINFLIQC where IPCODPACI = '0101'  and CODCONCEC = '82809'--Cabecera
select * from HCINFCONC where CODCONCEC = '82809' --Medicamentos
select * from HCINFLIQD where CODCONCEC = '82809' --Vehiculo - Diluyente
select * from HCINFLIQA  where CODCONCEC = '82809'  --Administrción - informacion generica  --692
*/