

CREATE VIEW [dbo].[Productos_Medicamentos]
AS
SELECT DCI.Code AS CodigoDCI,substring(RTRIM(DCI.Name),1,60) as NombreDCI, ATCE.Code as CodigoATC,RTRIM(ATCE.Name) as ATC,ATC.Code as CodigoMedicamento, RTRIM(ATC.Name) as NombreMedicamento,
PGR.Code as CodigoGrupoFarma,PGR.Name as GrupoFarmacologico    ,arr.Code as CodigoVia,ARR.Name AS ViaAdmin,
PF.Code as CodigoForma ,PF.Name AS FormaFarmaceutica,Concentration as Concetracion,
case FormulationType when 1 then '1-Peso' when 2 then '2-Volumen' when 3 then '3-Peso y Volumen' when 4 then '4-Unidad de Administración' end as TipoFormulacion,
Weight as Peso,IMUp.Code AS CodigoUnidadPeso,IMUp.Name AS UnidadPeso, Volume,IMUv.Code AS CodigoUnidadVolumen,IMUv.Name AS UnidadVolumen,
IMUu.Code AS CodigoUnidadAdmin,IMUu.Name AS UnidadAdministracion, case AutomaticCalculation when 1 then 'Si' else 'No' end CalculoAutomatico,
case DiluentProduct when 1 then 'Si' else 'No' end MedicamentoDiluyente,
case atc.POSProduct when 1 then 'Si' else 'No' end as 'Pos',
case atc.AllPOSPathologies when 1 then 'Si' else 'No' end as AplicaTodasPatologia, case ATC.Consumption when 1 then 'Si' else 'No' end as MedicamentoDeConsumo,
IP.Code as CodigoProducto,IP .Name as NombreProducto,IP.CodeCUM as CodigoCUM, IP.CodeAlternative as CodigoAlternativo, ip.CodeAlternativeTwo AS CodigoAlternativo2, pg.Code as CodigoGrupo ,pg.Name as Grupo, psg.Code as CodigoSubGrupo,psg.Name as NombreSubGrupo,
case HandlesHealthRegistration when 1 then 'Si' else 'No' end as RegistroSanitario, HealthRegistration as NroRegistroSanitario,
cast(ExpirationDate AS date ) as FechaExpedicionRegitro,
BG.Code AS CodigoGrupoFacturacion,BG.Name as NombreGrupoFac,case  ProductControl when 1 then 'Si' else 'No' end ProductoDeControl,
case ip.POSProduct when 1 then 'Si' else 'No' end ProductoPos,ip.ProductCost as CostoPromedio, ip.FinalProductCost as UltimoCosto, ip.SellingPrice as PrecioVentaFarmacia,
M.Name as 'Fabricante/Proveedor',PU.Code as CodigoPaquete, PU.Name as NombrePaquete, ISNULL(STUFF((SELECT DISTINCT ', ' + CONCAT(bs.BatchCode, ' - ', bs.ExpirationDate) FROM Inventory.BatchSerial bs WHERE bs.productId = ip.Id AND CAST(ISNULL(bs.ExpirationDate, GETDATE()) AS DATE) >= CAST(GETDATE() AS DATE) FOR XML PATH ('')), 1,2, ''), '') LoteFechaVencimiento
FROM Inventory.ATC as ATC INNER JOIN
Inventory .AdministrationRoute AS AR on ATC.AdministrationRouteId =AR.Id INNER JOIN
Inventory .DCI AS DCI ON ATC.DCIId =DCI.Id  INNER JOIN
.IHLISTPRO  as P on P.CODPRODUC =atc.code INNER JOIN
Inventory .ATCAdministrationRoute as atcr on atcr.ATCId =ATC.Id inner join
Inventory .PharmacologicalGroup as PGR on PGR.Id =ATC.PharmacologicalGroupId inner join
Inventory .AdministrationRoute AS ARR ON ARR.Id =atcr.AdministrationRouteId LEFT OUTER JOIN
Inventory.InventoryRiskLevel as IRL on ATC.InventoryRiskLevelId =IRL.Id LEFT OUTER JOIN
Inventory .InventoryMeasurementUnit AS IMUp ON ATC.WeightMeasureUnit =IMUp.Id LEFT OUTER JOIN
Inventory .InventoryMeasurementUnit AS IMUv ON ATC.VolumeMeasureUnit  =IMUv.Id LEFT OUTER JOIN
Inventory .InventoryMeasurementUnit AS IMUu ON ATC.AdministrationUnitId  =IMUu.Id LEFT OUTER JOIN
Inventory .ATCEntity as ATCE ON ATC.ATCEntityId =ATCE.Id inner join
Inventory .PharmaceuticalForm AS PF ON PF.id =ATC.PharmaceuticalFormId left outer join
Inventory .InventoryProduct as IP on IP.ATCId =ATC.Id left outer join
Inventory .ProductGroup as pg on pg.Id =IP.ProductGroupId  left outer join
Inventory .ProductSubGroup psg on psg.Id =IP.ProductSubGroupId left outer join
.Billing .BillingGroup AS BG ON BG.Id =IP.BillingGroupId left outer join
Inventory .Manufacturer AS M ON M.Id =IP.ManufacturerId left outer join
Inventory .PackagingUnit as PU on PU.Id =IP.PackagingUnitId 
where ATC.Status =1