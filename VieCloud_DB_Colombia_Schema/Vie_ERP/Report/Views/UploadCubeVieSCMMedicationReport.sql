

--CREATE PROCEDURE [Inventory].[ReporteMedicamentos]
--AS

CREATE view [Report].[UploadCubeVieSCMMedicationReport] as

	WITH movimientos AS 
	(
		SELECT DISTINCT productid FROM inventory.physicalinventory
	)


	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		DCI.Code AS 'CODIGO DCI',--CodigoDCI,
		substring(RTRIM(DCI.Name),1,60) as 'NOMBRE DCI',--NombreDCI, 
		ATCE.Code as 'CODIGO ATC',--CodigoATC,
		RTRIM(ATCE.Name) as 'ATC',--ATC,
		ATC.Code as 'CODIGO MEDICAMENTO',--CodigoMedicamento, 
		RTRIM(ATC.Name) as 'NOMBRE MEDICAMENTO',--NombreMedicamento, 
		CASE atc.status 
			WHEN 1 THEN 'Activo' 
			WHEN 0 THEN 'Inactivo' END AS 'ESTADO MEDICAMENTO',--EstadoMeidcamento,
		PGR.Code as 'CODIGO GRUPO FARMA',--CodigoGrupoFarma,
		PGR.Name as 'GRUPO FARMACOLOGICO',--GrupoFarmacologico,
		arr.Code as 'CODIGO VIA',--CodigoVia,
		ARR.Name AS 'VIA ADMIN',--ViaAdmin,
		PF.Code as 'CODIGO FORMA',--CodigoForma,
		PF.Name AS 'FORMA FARMACEUTICA',--FormaFarmaceutica,
		Concentration as 'CONCENTRACION',--Concentracion,
		case FormulationType 
			when 1 then '1-Peso'
			when 2 then '2-Volumen' 
			when 3 then '3-Peso y Volumen' 
			when 4 then '4-Unidad de Administración' end as 'TIPO FORMULACION',--TipoFormulacion,
		Weight as 'PESO',--Peso,
		IMUp.Code AS 'CODIGO UNIDAD PESO',--CodigoUnidadPeso,
		IMUp.Name AS 'UNIDAD PESO',-- UnidadPeso, 
		Volume 'VOLUMEN',--,
		IMUv.Code AS 'CODIGO UNIDAD VOLUMEN',--CodigoUnidadVolumen,
		IMUv.Name AS 'UNDAD VOLUMEN',--UnidadVolumen,
		IMUu.Code AS 'CODIGO UNIDAD ADMIN',--CodigoUnidadAdmin,
		IMUu.Name AS 'UNIDAD ADMINISTRACION',--UnidadAdministracion, 
		case AutomaticCalculation when 1 then 'Si' else 'No' end 'CALCULO AUTOMATICO',--CalculoAutomatico,
		case DiluentProduct when 1 then 'Si' else 'No' end 'MEDICAMENTO DILUYENTE',--MedicamentoDiluyente, 
		case atc.POSProduct when 1 then 'Si' else 'No' end as 'POS',--'Pos', 
		CASE atc.conditioned WHEN 1 THEN 'SI' WHEN 0 THEN 'NO' END AS 'CONDICIONADO',--condicionado,
		CASE atc.unirs WHEN 1 THEN 'SI' WHEN 0 THEN 'NO' END AS 'UNIRS',--UNIRS, 
		case atc.AllPOSPathologies when 1 then 'Si' else 'No' end as 'APLICA TOAS LAS PATOLOGIAS',--AplicaTodasPatologia, 
		case ATC.Consumption when 1 then 'Si' else 'No' end as 'MEDICAMENTOS DE CONSUMO',--MedicamentoDeConsumo,
		IP.Code as CodigoProducto,IP .Name as 'NOMBRE PRODUCTO',--NombreProducto,
		CASE ip.status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END AS 'ESTADO PRODUCTO',--[EstadoProducto],
		IP.CodeCUM as 'CODIGO CUM',--CodigoCUM, 
		IP.CodeAlternative as 'CODIGO ALTERNATIVO',--CodigoAlternativo, 
		ip.CodeAlternativeTwo AS 'CODIGO ALTERNATIVO2',--CodigoAlternativo2, 
		pg.Code as 'CODIGO GRUPO',--CodigoGrupo ,
		pg.Name as 'GRUPO',--Grupo, 
		psg.Code as 'CODIGO SUBGRUPO',--CodigoSubGrupo,
		psg.Name as 'NOMBRE SUBGRUPO',--NombreSubGrupo,
		case HandlesHealthRegistration when 1 then 'Si' else 'No' end as 'REGISTRO SANITARIO',--RegistroSanitario, 
		HealthRegistration as 'NRO REGISTRO SANITARIO',--NroRegistroSanitario, 
		cast(ExpirationDate AS date ) as 'FECHA EXPEDICION REGISTRO',--FechaExpedicionRegistro, 
		BG.Code AS 'CODIGO GRUPO FACTURACION',--CodigoGrupoFacturacion,
		BG.Name as 'NOMBRE GRUPO FACTURACION',--NombreGrupoFac,
		case  ProductControl when 1 then 'Si' else 'No' end 'PRODUCTO DE CONTROL',--ProductoDeControl, 
		case ip.POSProduct when 1 then 'Si' else 'No' end 'PRODUCTO POS',--ProductoPos,
		ip.ProductCost as 'COSTO PROMEDIO',--CostoPromedio, 
		ip.FinalProductCost as 'ULTIMO COSTO',--UltimoCosto, 
		ip.SellingPrice as 'PRECIO VENTA FARMACIA',--PrecioVentaFarmacia,
		M.Name as 'FABRICANTE/PROVEEDOR',--'FabricanteProveedor',
		PU.Code as 'CODIGO PAQUETE',--CodigoPaquete, 
		PU.Name as 'NOMBRE PAQUETE',--NombrePaquete,
		CASE WHEN mov.productid IS NULL THEN 'No' ELSE 'Si' END AS 'TIENE ROTACION',--[TieneRotacion],
		CAST(GETDATE() as date) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
	FROM Inventory.ATC as ATC INNER JOIN
	Inventory.AdministrationRoute AS AR on ATC.AdministrationRouteId =AR.Id INNER JOIN
	Inventory.DCI AS DCI ON ATC.DCIId =DCI.Id  INNER JOIN
	.IHLISTPRO  as P on P.CODPRODUC =atc.code INNER JOIN
	Inventory.ATCAdministrationRoute as atcr on atcr.ATCId =ATC.Id inner join
	Inventory.PharmacologicalGroup as PGR on PGR.Id =ATC.PharmacologicalGroupId inner join
	Inventory.AdministrationRoute AS ARR ON ARR.Id =atcr.AdministrationRouteId LEFT OUTER JOIN
	Inventory.InventoryRiskLevel as IRL on ATC.InventoryRiskLevelId =IRL.Id LEFT OUTER JOIN
	Inventory.InventoryMeasurementUnit AS IMUp ON ATC.WeightMeasureUnit =IMUp.Id LEFT OUTER JOIN
	Inventory.InventoryMeasurementUnit AS IMUv ON ATC.VolumeMeasureUnit  =IMUv.Id LEFT OUTER JOIN
	Inventory.InventoryMeasurementUnit AS IMUu ON ATC.AdministrationUnitId  =IMUu.Id LEFT OUTER JOIN
	Inventory.ATCEntity as ATCE ON ATC.ATCEntityId =ATCE.Id inner join
	Inventory.PharmaceuticalForm AS PF ON PF.id =ATC.PharmaceuticalFormId left outer join
	Inventory.InventoryProduct as IP on IP.ATCId =ATC.Id left outer join
	Inventory.ProductGroup as pg on pg.Id =IP.ProductGroupId  left outer join
	Inventory.ProductSubGroup psg on psg.Id =IP.ProductSubGroupId left outer join
	Billing.BillingGroup AS BG ON BG.Id =IP.BillingGroupId left outer join
	Inventory.Manufacturer AS M ON M.Id =IP.ManufacturerId left outer join
	Inventory.PackagingUnit as PU on PU.Id =IP.PackagingUnitId 
	LEFT JOIN movimientos AS mov ON ip.id = mov.productid

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolidar un reporte maestro de medicamentos con su clasificación ATC/DCI, vías de administración, forma farmacéutica, datos de producto, costos, fabricante, grupo de facturación e indicador de rotación según movimientos de inventario físico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el catálogo externo IHLISTPRO accesible, ya que se cruza por código de producto (CODPRODUC) mediante INNER JOIN, condicionando qué medicamentos aparecen.; El servidor debe soportar la zona horaria ''Pakistan Standard Time'' para el cálculo de la marca temporal.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen medicamentos (ATC) con vía de administración principal, DCI, grupo farmacológico, forma farmacéutica y entrada en el catálogo externo IHLISTPRO (vía INNER JOIN).; Cada medicamento aparece tantas veces como vías de administración asociadas tenga en ATCAdministrationRoute.; El identificador de compañía corresponde al nombre actual de la base de datos truncado a 9 caracteres.; La fecha de búsqueda corresponde a la fecha actual y la marca de actualización se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El indicador de rotación depende exclusivamente de existencia del producto en inventario físico (no de cantidades).; Productos sin información comercial (InventoryProduct, grupo, subgrupo, facturación, fabricante, empaque) se reportan igualmente con valores nulos vía LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Clasificación ATC; DCI (Denominación Común Internacional); Vía de administración; Forma farmacéutica; Grupo farmacológico; Producto POS; Registro sanitario; Grupo de facturación; Fabricante/Proveedor; Unidad de empaque; Inventario físico / Rotación; Producto de control; Medicamento diluyente; Patologías POS; Costo promedio y precio de venta de farmacia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un dataset tabular para alimentación de cubo/reporte de medicamentos; no modifica datos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Estado del medicamento = 1 → Se reporta como ''Activo'' else Se reporta como ''Inactivo'' cuando es 0; si FormulationType en {1,2,3,4} → Se etiqueta como Peso, Volumen, Peso y Volumen, o Unidad de Administración respectivamente; si El producto existe en Inventory.PhysicalInventory → Se marca ''TIENE ROTACION'' = ''Si'' else Se marca ''TIENE ROTACION'' = ''No''; si Banderas booleanas (AutomaticCalculation, DiluentProduct, POSProduct, conditioned, unirs, AllPOSPathologies, Consumption, HandlesHealthRegistration, ProductControl) = 1 → Se traducen a ''Si''/''SI'' else Se traducen a ''No''/''NO''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventory; Inventory.ATC; Inventory.AdministrationRoute; Inventory.DCI; IHLISTPRO; Inventory.ATCAdministrationRoute; Inventory.PharmacologicalGroup; Inventory.InventoryRiskLevel; Inventory.InventoryMeasurementUnit; Inventory.ATCEntity; Inventory.PharmaceuticalForm; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup; Billing.BillingGroup; Inventory.Manufacturer; Inventory.PackagingUnit', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationReport';
GO
