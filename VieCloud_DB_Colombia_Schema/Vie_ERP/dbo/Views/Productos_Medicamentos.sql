

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo consolidado de medicamentos e insumos farmacéuticos activos del sistema. Integra la clasificación ATC (código anatómico-terapéutico-químico), la Denominación Común Internacional (DCI o principio activo genérico), el grupo farmacológico, las vías de administración permitidas, la forma farmacéutica, concentración, tipo de formulación (peso, volumen o unidad), unidades de medida, y datos de inventario del producto como código CUM, registro sanitario, precios (costo promedio, último costo, precio de venta en farmacia), fabricante o proveedor, grupo de facturación, grupo y subgrupo del producto, indicadores de si es medicamento POS, de control, diluyente o de consumo, y los lotes vigentes con su fecha de vencimiento. Se usa para consultas de farmacia, gestión de inventario, formulación clínica, reportería de medicamentos, validación POS y control de registros sanitarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Productos_Medicamentos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Productos_Medicamentos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de productos y medicamentos activos con su clasificación ATC, DCI, forma farmacéutica, vías de administración, grupo farmacológico, datos comerciales, registro sanitario y lotes vigentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los medicamentos deben tener Status = 1 (activos) en Inventory.ATC para ser incluidos; Debe existir correspondencia entre el código ATC y un producto en IHLISTPRO (INNER JOIN); El medicamento debe tener al menos una vía de administración asignada en ATCAdministrationRoute; El medicamento debe tener un grupo farmacológico y una forma farmacéutica asignados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen medicamentos activos (ATC.Status = 1); Solo se reportan lotes vigentes (no vencidos a la fecha actual) en el campo de lotes; El nombre de la DCI se trunca a 60 caracteres; Los flags binarios (POS, control, consumo, registro sanitario, etc.) se traducen a ''Si''/''No'' para presentación; Un medicamento puede aparecer múltiples veces si tiene varias vías de administración asociadas en ATCAdministrationRoute', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Clasificación ATC; DCI (Denominación Común Internacional); Vía de administración; Forma farmacéutica; Grupo farmacológico; POS (Plan Obligatorio de Salud); Patologías POS; Medicamento de consumo; Medicamento diluyente; Registro sanitario; Producto de control; CUM (Código Único de Medicamentos); Costo promedio / Último costo / Precio de venta; Fabricante/Proveedor; Lote y fecha de vencimiento; Grupo de facturación; Unidad de empaque; Concentración del medicamento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve únicamente medicamentos cuyo ATC.Status = 1; [RETURN_RESULT] N/A: En LoteFechaVencimiento concatena solo lotes cuya ExpirationDate (o GETDATE() si es NULL) sea >= fecha actual; si no hay lotes vigentes devuelve cadena vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FormulationType = 1 → Tipo de formulación = ''1-Peso'' else 2=Volumen, 3=Peso y Volumen, 4=Unidad de Administración; si AutomaticCalculation = 1 → CalculoAutomatico = ''Si'' else ''No''; si DiluentProduct = 1 → MedicamentoDiluyente = ''Si'' else ''No''; si ATC.POSProduct = 1 → Pos = ''Si'' (medicamento incluido en POS) else ''No''; si ATC.AllPOSPathologies = 1 → AplicaTodasPatologia = ''Si'' else ''No''; si ATC.Consumption = 1 → MedicamentoDeConsumo = ''Si'' else ''No''; si HandlesHealthRegistration = 1 → RegistroSanitario = ''Si'' else ''No''; si ProductControl = 1 → ProductoDeControl = ''Si'' (producto controlado) else ''No''; si IP.POSProduct = 1 → ProductoPos = ''Si'' else ''No''; si BatchSerial.ExpirationDate IS NULL → Se asume GETDATE() como fecha de vencimiento para evaluar vigencia del lote else Se usa la fecha real de expiración', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.AdministrationRoute; Inventory.DCI; IHLISTPRO; Inventory.ATCAdministrationRoute; Inventory.PharmacologicalGroup; Inventory.InventoryRiskLevel; Inventory.InventoryMeasurementUnit; Inventory.ATCEntity; Inventory.PharmaceuticalForm; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup; Billing.BillingGroup; Inventory.Manufacturer; Inventory.PackagingUnit; Inventory.BatchSerial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Medicamentos';
GO
