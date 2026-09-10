
CREATE PROCEDURE [MedicalHistory].[ListFinishedProducts]
@TipoListar integer,
@IdOrigin varchar(25), --Puede venir el codigo del producto en caso de medicamentos ó en caso de mezclas el id origen que es el de la orden 
@Paciente varchar(25),
@Ingreso char(10),
@CargoMedicamentosQuimioterapia bit,
@CicloAplicaQuimio int,
@DiaAplicaQuimio int

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
			INNER JOIN ehr.HCORDQUIMIO O on O.ID = a.IDHCORDQUIMIO 
		    INNER JOIN ehr.HCORDCICLOSD C on C.IDHCORDQUIMIO = O.ID AND @CicloAplicaQuimio = A.CICLO AND @DiaAplicaQuimio = A.DIA
			inner join MedicalHistory.PharmaDose d on p.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation  --and p.MainDrugCode = d.ProductCode --and AppliedDose = 0
			inner join MedicalHistory.DetailPhysicalCUM cum ON cum.GroupingCodeDose = D.GroupingCodeDose   --and UsedQuantity  < cum.DispensedQuantity  
			inner join Inventory.InventoryProduct pro on  pro.Id = cum.ProductId 
			inner join Inventory.ATC atc on  atc.Id = pro.ATCId  
			INNER join HCFISIPRO fis on fis.ID = cum.IDHCFISIPRO AND fis.IPCODPACI =  @Paciente AND fis.NUMINGRES = @Ingreso AND TIPREGIST = 1 
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los productos terminados (medicamentos, mezclas, líquidos o nutrición parenteral) que han sido dispensados y están disponibles para ser registrados como aplicados a un paciente hospitalizado. Según el tipo solicitado, consulta medicamentos hospitalarios comunes, medicamentos de quimioterapia (incluyendo ciclo y día de aplicación), mezclas preparadas en estación de mezclas o nutrición parenteral, cruzando los productos susceptibles de mezcla, las dosis farmacéuticas, el detalle físico de CUM dispensado y el proceso físico del paciente. Excluye los ítems que ya fueron devueltos o anulados, y retorna por cada unidad dispensada el nombre completo del producto, el código del medicamento principal, el lote, y si la dosis fue aplicada o está pendiente. Es utilizado por enfermería o farmacia para confirmar la administración de medicamentos al paciente durante su ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'PROCEDURE', @level1name = N'ListFinishedProducts';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'PROCEDURE', @level1name = N'ListFinishedProducts';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los lotes/CUM disponibles (no devueltos) de medicamentos, mezclas o nutrición parenteral asociados a un paciente, diferenciando entre medicamentos hospitalarios, de quimioterapia, mezclas/líquidos y nutrición parenteral.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProducts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registros físicos en HCFISIPRO con TIPREGIST = 1.; Para mezclas/líquidos debe existir un registro en HCINFLIQA cuyo CODCONCEC_ORIGEN coincida con el identificador recibido.; Para quimioterapia, la orden de medicamento debe estar vinculada a una orden de quimioterapia y a un ciclo/día que coincidan con los parámetros entregados.; El producto/origen debe estar registrado como susceptible de mezcla en ProductSusceptibleMixingStation con un código de dosis que enlace a PharmaDose y DetailPhysicalCUM.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProducts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se devuelven CUM marcados como devueltos (HCDEVMEDD.PROESTADO=''2'').; Solo se consideran registros físicos de tipo TIPREGIST=1 en HCFISIPRO.; Los registros físicos deben pertenecer al paciente solicitado (IPCODPACI=@Paciente).; Los resultados se ordenan por BatchCode descendente.; La columna ''Seleccionar'' siempre se inicializa en 0 (false) en el resultado.; Los resultados se devuelven con DISTINCT, evitando duplicados por joins múltiples.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProducts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Medicamento hospitalario; Quimioterapia; Ciclo de quimioterapia; Día de aplicación; Mezclas / Líquidos; Nutrición parenteral; Dosis farmacéutica; Lote (BatchCode); CUM (Código Único de Medicamento); Aplicación de dosis (Pendiente/Aplicado); Devolución de medicamentos; Clasificación ATC; Orden médica de quimioterapia; Prescripción', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProducts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @TipoListar=1 y @CargoMedicamentosQuimioterapia=0, devuelve detalles de CUM de medicamentos hospitalarios cruzando ProductSusceptibleMixingStation con HCPRESCRA del paciente y filtrando por @IdOrigin como ProductCode.; [RETURN_RESULT] resultset: Cuando @TipoListar=1 y @CargoMedicamentosQuimioterapia=1, devuelve detalles de CUM cruzando con EHR.HCORDMEDICAM, HCORDQUIMIO y HCORDCICLOSD validando que CICLO=@CicloAplicaQuimio y DIA=@DiaAplicaQuimio.; [RETURN_RESULT] resultset: Cuando @TipoListar=2, resuelve el consecutivo de HCINFLIQA por CODCONCEC_ORIGEN=@IdOrigin y devuelve los CUM cuyo IdOrigin coincide con dicho consecutivo (mezclas/líquidos).; [RETURN_RESULT] resultset: Cuando @TipoListar es distinto de 1 y 2, devuelve los CUM de productos cuyo IdOrigin = @IdOrigin (nutrición parenteral).; [RETURN_RESULT] resultset: En todos los casos se excluyen los CUM que tengan registro en HCDEVMEDD con PROESTADO=''2'' (devueltos).; [RETURN_RESULT] resultset: AppliedDose=0 se etiqueta como ''Pendiente'' y AppliedDose=1 como ''Aplicado'' en la columna EstadoAplicacion.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProducts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoListar = 1 y @CargoMedicamentosQuimioterapia = 0 → Lista CUM de medicamentos hospitalarios uniendo con HCPRESCRA del paciente. else Si @CargoMedicamentosQuimioterapia = 1, lista CUM de medicamentos de quimioterapia validando ciclo y día contra HCORDQUIMIO/HCORDCICLOSD.; si @TipoListar = 2 → Resuelve consecutivo de HCINFLIQA por CODCONCEC_ORIGEN y lista CUM de mezclas/líquidos asociados.; si @TipoListar distinto de 1 y 2 → Lista CUM de nutrición parenteral filtrando por IdOrigin directamente.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProducts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.ProductSusceptibleMixingStation; MedicalHistory.PharmaDose; MedicalHistory.DetailPhysicalCUM; Inventory.InventoryProduct; Inventory.ATC; HCFISIPRO; HCPRESCRA; EHR.HCORDMEDICAM; ehr.HCORDQUIMIO; ehr.HCORDCICLOSD; HCINFLIQA; HCDEVMEDD', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProducts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'ListFinishedProducts';
-- GO
