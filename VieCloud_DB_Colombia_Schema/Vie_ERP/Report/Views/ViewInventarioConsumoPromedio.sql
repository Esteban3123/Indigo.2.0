
    /*******************************************************************************************************************
Nombre: InventarioConsumoPromedio
Tipo:Vista
Observacion:consumo por mes y promedio de cada seis meses 
Profesional: Nilsson Miguel Galindo Lopez
Fecha:28-06-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: 
Fecha:
Ovservaciones: 
--------------------------------------
Vercion 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewInventarioConsumoPromedio] AS

with
CTE_SaldoAlmacenes as
(
SELECT 
pr.Code AS Código, pr.Name AS Producto, 
CASE pr.ProductTypeId WHEN '1' THEN 'MEDICAMENTO' WHEN '2' THEN 'DISPOSITIVO MÉDICO' WHEN '3' THEN 'ELEMENTOS DE CONSUMO' WHEN '4' THEN 'NUTRICION ESPECIAL' WHEN '5' THEN 'EQUIPOBIOMEDICO' WHEN '6' THEN 'INSUMO LABORATORIO' WHEN '7' THEN 'MEDICAMENTO VITAL NO DISPONIBLE'
END AS [Tipo Producto], Med.Code + ' -' + Med.Name AS Medicamento, ATC.Code AS CodATC, ATC.Name AS ATC, pr.CodeCUM AS [C.U.M], pr.CodeAlternativeTwo AS [Código Alterno 2], sg.Name AS SubGrupo, ue.Abbreviation AS Unidad, gf.Name AS [Grupo Facturación], 
CASE pr.ProductControl WHEN '0' THEN 'No' WHEN '1' THEN 'Si' END AS [Producto Control], pr.ProductCost AS CostoPromedio, pr.FinalProductCost AS Ultimocosto, CASE pr.Status WHEN '1' THEN 'Activo' WHEN '0' THEN 'Inactivo' END AS Estado, inf.Quantity AS Cantidad, 
inf.Quantity * pr.ProductCost AS VrTotal, al.Id AS IdAlm, al.Code AS CódigoAlmacén, al.Name AS Almacén, al.Prefix, CASE WHEN al.Name LIKE 'BOG%' THEN 'Bogota' WHEN al.Name LIKE 'CAL%' THEN 'Cali' END AS Sede, 
CASE ProductWithPriceControl WHEN 0 THEN '' WHEN 1 THEN 'Regulado' END AS Precio
FROM   Inventory.InventoryProduct AS pr  LEFT OUTER JOIN
             Inventory.PhysicalInventory AS inf  ON inf.ProductId = pr.Id LEFT OUTER JOIN
             Inventory.Warehouse AS al ON al.Id = inf.WarehouseId LEFT OUTER JOIN
             Inventory.ATC AS Med  ON Med.Id = pr.ATCId LEFT OUTER JOIN
             Inventory.ProductSubGroup AS sg ON sg.Id = pr.ProductSubGroupId LEFT OUTER JOIN
             Inventory.PackagingUnit AS ue ON ue.Id = pr.PackagingUnitId LEFT OUTER JOIN
             Billing.BillingGroup AS gf ON gf.Id = pr.BillingGroupId LEFT OUTER JOIN
             Inventory.ATCEntity AS ATC ON Med.ATCEntityId = ATC.Id
WHERE (inf.Quantity <> '0')
)

SELECT        
Sede, 
Cod_Producto, 
Producto, 
Medicamento, 
CodSubgrupo, 
CodATC, 
ATC, 
CodigoCUM, TipoProducto, Ene_A, Feb_A, Mar_A, Abr_A, May_A, Jun_A, Jul_A, Ago_A, Sep_A, Oct_A, Nov_A, Dic_A, Ene_V, Feb_V, Mar_V, Abr_V, May_V,
Jun_V, Jul_V, Ago_V, Sep_V, Oct_V, Nov_V, Dic_V, 
CASE month(getdate()) WHEN '1' THEN ((ISNULL(Ago_V, 0)) + (ISNULL(Sep_V, 0)) + (ISNULL(Oct_V, 0)) + (ISNULL(Nov_V, 0)) + (ISNULL(Dic_V, 0)) + (ISNULL(Ene_V, 0))) / 6 
					  WHEN '2' THEN ((ISNULL(Sep_V, 0)) + (ISNULL(Oct_V, 0)) + (ISNULL(Nov_V, 0)) + (ISNULL(Dic_V, 0)) + (ISNULL(Ene_V, 0)) + (ISNULL(Feb_V, 0))) / 6 
					  WHEN '3' THEN ((ISNULL(Oct_V, 0)) + (ISNULL(Nov_V, 0)) + (ISNULL(Dic_V, 0)) + (ISNULL(Ene_V, 0)) + (ISNULL(Feb_V, 0)) + (ISNULL(Mar_V, 0))) / 6 
					  WHEN '4' THEN ((ISNULL(Nov_V, 0)) + (ISNULL(Dic_v, 0)) + (ISNULL(Ene_V, 0)) + (ISNULL(Feb_V, 0)) + (ISNULL(Mar_V, 0)) + (ISNULL(Abr_V, 0))) / 6 
					  WHEN '5' THEN ((ISNULL(Dic_V, 0)) + (ISNULL(Ene_A, 0)) + (ISNULL(Feb_V, 0)) + (ISNULL(Mar_V, 0)) + (ISNULL(Abr_V, 0)) + (ISNULL(May_V, 0))) / 6 
					  WHEN '6' THEN ((ISNULL(Ene_V, 0)) + (ISNULL(Feb_V, 0)) + (ISNULL(Mar_V, 0)) + (ISNULL(Abr_V, 0)) + (ISNULL(May_V, 0)) + (ISNULL(Jun_V, 0))) / 6 
					  WHEN '7' THEN ((ISNULL(Feb_V, 0)) + (ISNULL(Mar_V, 0)) + (ISNULL(Abr_V, 0)) + (ISNULL(May_V, 0)) + (ISNULL(Jun_V, 0)) + (ISNULL(Jul_V, 0))) / 6 
					  WHEN '8' THEN ((ISNULL(Mar_V, 0)) + (ISNULL(Abr_V, 0)) + (ISNULL(May_V, 0)) + (ISNULL(Jun_V, 0)) + (ISNULL(Jul_V, 0)) + (ISNULL(Ago_V, 0))) / 6 
					  WHEN '9' THEN ((ISNULL(Abr_V, 0)) + (ISNULL(May_V, 0)) + (ISNULL(Jun_V, 0)) + (ISNULL(Jul_V, 0)) + (ISNULL(Ago_V, 0)) + (ISNULL(Sep_V, 0))) / 6 
					  WHEN '10' THEN ((ISNULL(May_V, 0)) + (ISNULL(Jun_V, 0)) + (ISNULL(Jul_V, 0)) + (ISNULL(Ago_V, 0)) + (ISNULL(Sep_V, 0)) + (ISNULL(Oct_V, 0))) / 6 
					  WHEN '11' THEN ((ISNULL(Jun_V, 0)) + (ISNULL(Jul_V, 0)) + (ISNULL(Ago_V, 0)) + (ISNULL(Sep_V, 0)) + (ISNULL(Oct_V, 0)) + (ISNULL(Nov_V, 0))) / 6 
					  WHEN '12' THEN ((ISNULL(Jul_V, 0)) + (ISNULL(Ago_V, 0)) + (ISNULL(Sep_V, 0)) + (ISNULL(Oct_V, 0)) + (ISNULL(Nov_V, 0)) + (ISNULL(Dic_V, 0))) / 6 END AS [Prom_Ulti_Semestre],
Estado
FROM 
(
	SELECT        
	UO.UnitName AS Sede, 
	CASE WHEN (MONTH(DocumentDate)) = '1' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Ene_A' 
		 WHEN (MONTH(DocumentDate)) = '2' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Feb_A' 
		 WHEN (MONTH(DocumentDate)) = '3' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Mar_A' 
		 WHEN (MONTH(DocumentDate)) = '4' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Abr_A' 
		 WHEN (MONTH(DocumentDate)) = '5' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'May_A' 
		 WHEN (MONTH(DocumentDate)) = '6' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Jun_A' 
		 WHEN (MONTH(DocumentDate)) = '7' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Jul_A' 
		 WHEN (MONTH(DocumentDate)) = '8' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Ago_A' 
		 WHEN (MONTH(DocumentDate)) = '9' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Sep_A' 
		 WHEN (MONTH(DocumentDate)) = '10' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Oct_A' 
		 WHEN (MONTH(DocumentDate)) = '11' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Nov_A' 
		 WHEN (MONTH(DocumentDate)) = '12' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Dic_A' 
		 WHEN (MONTH(DocumentDate)) = '1' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Ene_V'
		 WHEN (MONTH(DocumentDate)) = '2' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Feb_V' 
		 WHEN (MONTH(DocumentDate)) = '3' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Mar_V' 
		 WHEN (MONTH(DocumentDate)) = '4' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Abr_V' 
		 WHEN (MONTH(DocumentDate)) = '5' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'May_V' 
		 WHEN (MONTH(DocumentDate)) = '6' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Jun_V' 
		 WHEN (MONTH(DocumentDate)) = '7' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Jul_V' 
		 WHEN (MONTH(DocumentDate)) = '8' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Ago_V' 
		 WHEN (MONTH(DocumentDate)) = '9' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Sep_V' 
		 WHEN (MONTH(DocumentDate)) = '10' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Oct_V' 
		 WHEN (MONTH(DocumentDate)) = '11' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Nov_V' 
		 WHEN (MONTH(DocumentDate)) = '12' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Dic_V' END AS Mes, 
pr.Code AS Cod_Producto, 
pr.Name AS Producto, 
Med.Code + ' - ' + Med.Name AS [Medicamento], 
sg.Code + ' - ' + sg.Name AS [CodSubgrupo], 
ATC.Code AS [CodATC], ATC.Name AS ATC, 
pr.CodeCUM AS [CodigoCUM], 
CASE pr.ProductTypeId WHEN '1' THEN 'Medicamento' WHEN '2' THEN 'DispositivoMedico' WHEN '3' THEN 'Elemento Consumo' WHEN '4' THEN 'Nutricion Especial' WHEN '5' THEN 'Equipo Biomedico' WHEN '6' THEN
'Insumo Laboratorio' WHEN '7' THEN 'Med VitalNO Disponible' END AS TipoProducto, sum(DI.Quantity - DI.ReturnedQuantity) AS Cantidad, 
CASE pr.Status WHEN '1' THEN 'Activo' WHEN '0' THEN 'Inactivo' END AS Estado
FROM            Inventory.PharmaceuticalDispensing AS D INNER JOIN
                Inventory.PharmaceuticalDispensingDetail AS DI ON DI.PharmaceuticalDispensingId = D .Id INNER JOIN
                .ADINGRESO AS I ON I.NUMINGRES = D .AdmissionNumber INNER JOIN
                .INPACIENT AS P ON P.IPCODPACI = I.IPCODPACI INNER JOIN
                Inventory.InventoryProduct AS pr ON pr.Id = DI.ProductId INNER JOIN
                Inventory.ProductSubGroup AS sg ON sg.Id = pr.ProductSubGroupId INNER JOIN
                Payroll.FunctionalUnit AS un ON un.Id = DI.FunctionalUnitId INNER JOIN
                Billing.ServiceOrder AS O ON O.EntityCode = D .Code INNER JOIN
                Common.OperatingUnit AS UO ON UO.Id = D .OperatingUnitId AND UO.Id = 1 LEFT OUTER JOIN
                Inventory.ATC AS Med ON Med.Id = pr.ATCId LEFT OUTER JOIN
                Inventory.ATCEntity ATC ON Med.ATCEntityId = ATC.Id
WHERE        CONVERT(NVARCHAR(10), D .DocumentDate, 20) > CONVERT(VARCHAR(10), GETDATE() - 367, 20) AND D .STATUS = '2' AND DI.Quantity <> DI.ReturnedQuantity AND sg.code <> 'OSTEO001' AND 
                        sg.code <> 'PROTE001'
GROUP BY UO.UnitName, (MONTH(DocumentDate)), pr.Code, pr.Name, Med.Code + ' - ' + Med.Name, sg.Code, ATC.Code, ATC.Name, CodeCUM, pr.ProductTypeId, sg.Code + ' - ' + sg.Name, YEAR(DocumentDate), d .code, 
                        pr.status
UNION ALL
SELECT        
UO.UnitName AS Sede, 
CASE WHEN (MONTH(DO.DocumentDate)) = '1' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Ene_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '2' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Feb_A'
	 WHEN (MONTH(DO.DocumentDate)) = '3' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Mar_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '4' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Abr_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '5' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'May_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '6' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Jun_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '7' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Jul_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '8' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Ago_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '9' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Sep_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '10' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Oct_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '11' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Nov_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '12' AND YEAR(DocumentDate) <> YEAR(getdate()) THEN 'Dic_A' 
	 WHEN (MONTH(DO.DocumentDate)) = '1' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Ene_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '2' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Feb_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '3' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Mar_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '4' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Abr_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '5' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'May_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '6' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Jun_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '7' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Jul_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '8' AND YEAR(DocumentDate)  = YEAR(getdate()) THEN 'Ago_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '9' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Sep_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '10' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Oct_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '11' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Nov_V' 
	 WHEN (MONTH(DO.DocumentDate)) = '12' AND YEAR(DocumentDate) = YEAR(getdate()) THEN 'Dic_V' END AS Mes, 
pr.Code AS Cod_Producto, 
pr.Name AS Producto, 
Med.Code + ' - ' + Med.Name AS Medicamento, 
sg.Code + ' - ' + sg.Name AS [Código Subgrupo], 
ATC.Code AS CodATC, 
ATC.Name AS ATC, 
pr.CodeCUM AS [Código CUM], 
CASE pr.ProductTypeId WHEN '1' THEN 'Medicamento' 
					  WHEN '2' THEN 'DispositivoMedico' 
					  WHEN '3' THEN 'Elemento Consumo' 
					  WHEN '4' THEN 'Nutricion Especial' 
					  WHEN '5' THEN 'Equipo Biomedico' 
					  WHEN '6' THEN 'Insumo Laboratorio' 
					  WHEN '7' THEN 'Med VitalNO Disponible' END AS TipoProducto, 
sum(n.OutstandingQuantity) AS Cantidad, 
CASE pr.Status WHEN '1' THEN 'Activo' 
			   WHEN '0' THEN 'Inactivo' END AS Estado
FROM            Inventory.TransferOrder AS DO INNER JOIN
                        Inventory.TransferOrderDetail AS DI ON DI.TransferOrderId = DO.Id INNER JOIN
                        Inventory.InventoryProduct AS pr ON pr.Id = DI.ProductId INNER JOIN
                        Inventory.ProductSubGroup AS sg ON sg.Id = pr.ProductSubGroupId INNER JOIN
                        Inventory.Warehouse AS a ON a.Id = DO.SourceWarehouseId INNER JOIN
                        Common.OperatingUnit AS UO ON UO.Id = DO.OperatingUnitId AND UO.Id = 1 INNER JOIN
                        Inventory.TransferOrderDetailBatchSerial AS N ON N .TransferOrderDetailId = DI.ID LEFT OUTER JOIN
                        Inventory.ATC AS Med ON Med.Id = pr.ATCId LEFT OUTER JOIN
                        Inventory.ATCEntity ATC ON Med.ATCEntityId = ATC.Id LEFT OUTER JOIN
                        CTE_SaldoAlmacenes AS infa ON infa.Código = pr.Code AND 
                        Infa.CódigoAlmacén = a.Code
WHERE CONVERT(NVARCHAR(10), DO.DocumentDate, 20) > CONVERT(VARCHAR(10), 
GETDATE() - 367, 20) AND DO.STATUS = '2' AND n.OutstandingQuantity <> '0' AND DO.OrderType = '2' AND sg.code <> 'OSTEO001' AND 
sg.code <> 'PROTE001' AND DO.OrderType = '2'
GROUP BY
UO.UnitName, (MONTH(DO.DocumentDate)), pr.Code, pr.Name, pr.CodeAlternative, pr.CodeAlternativeTwo, sg.Code, Med.Code + ' - ' + Med.Name, ATC.Code, ATC.Name, CodeCUM, pr.ProductTypeId, 
sg.Code + ' - ' + sg.Name, pr.status, YEAR(DocumentDate), infa.CostoPromedio, infa.Ultimocosto, infa.Cantidad, infa.Unidad) source PIVOT (SUM(Cantidad) FOR SOURCE.Mes IN (Ene_V, Feb_V, Mar_V, Abr_V, May_V, 
Jun_V, Jul_V, Ago_V, Sep_V, Oct_V, Nov_V, Dic_V, Ene_A, Feb_A, Mar_A, Abr_A, May_A, Jun_A, Jul_A, Ago_A, Sep_A, Oct_A, Nov_A, Dic_A)) AS pivotable
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a análisis de consumo de medicamentos e insumos por sede (Bogotá/Cali). Consolida dispensaciones farmacéuticas y traslados de inventario de los últimos 13 meses, pivotando cantidades consumidas por mes (año anterior y año vigente). Calcula dinámicamente el promedio de consumo de los últimos seis meses según el mes actual, combinando saldos físicos en bodega con los movimientos de despacho, para apoyar la planeación y reposición de inventario farmacéutico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el consumo mensual de productos de inventario (medicamentos e insumos) en los últimos ~12 meses, separando año vigente y anterior, y calcula el promedio móvil de los últimos seis meses según el mes actual.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de las tablas externas referenciadas como ''.ADINGRESO'' e ''.INPACIENT'' (referencias con base/esquema vacíos) en el contexto de ejecución; La unidad operativa con Id = 1 debe existir en Common.OperatingUnit; Los productos deben tener relación con ProductSubGroup y opcionalmente con ATC/ATCEntity; Las dispensaciones deben estar vinculadas a una ServiceOrder vía EntityCode = D.Code', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran documentos con DocumentDate posterior a GETDATE()-367 días (ventana móvil de ~12 meses); Solo se consideran dispensaciones y traslados con STATUS = ''2'' (estado finalizado/aprobado); Se excluyen siempre los subgrupos con código ''OSTEO001'' y ''PROTE001''; En dispensaciones la cantidad neta consumida es Quantity - ReturnedQuantity y se descartan filas donde Quantity = ReturnedQuantity (consumo cero); En traslados solo se consideran órdenes con OrderType = ''2'' y OutstandingQuantity distinta de cero; Solo se reportan datos de la unidad operativa con Id = 1; El CTE de saldos solo incluye productos con cantidad física distinta de cero (inf.Quantity <> 0); Las cantidades mensuales se agregan mediante PIVOT por SUM sobre las 24 columnas Ene_V..Dic_V y Ene_A..Dic_A; El cálculo del promedio para el mes 5 usa Ene_A en lugar de Ene_V (posible inconsistencia respecto al patrón de los demás meses, pero el código siempre lo aplica así)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consumo de inventario; Dispensación farmacéutica; Traslado de inventario; Saldo de almacén; Clasificación ATC; CUM; Subgrupo de producto; Tipo de producto (medicamento, dispositivo, insumo, etc.); Sede (Bogotá/Cali); Promedio móvil semestral de consumo', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve por sede y producto las cantidades consumidas por mes (24 columnas año vigente _V y año anterior _A) más el promedio del último semestre, uniendo dispensaciones farmacéuticas y traslados de tipo 2 con STATUS=''2'' en los últimos 367 días, excluyendo subgrupos OSTEO001 y PROTE001', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si YEAR(DocumentDate) = YEAR(GETDATE()) → El consumo del mes se clasifica en columna del año vigente (sufijo _V: Ene_V..Dic_V) else Se clasifica en columna del año anterior (sufijo _A: Ene_A..Dic_A); si MONTH(GETDATE()) determina el mes actual → El promedio del último semestre se calcula sumando los 6 meses previos (incluyendo el actual) de columnas _V y dividiendo entre 6; si Origen del consumo: dispensación farmacéutica (PharmaceuticalDispensing) vs traslado (TransferOrder con OrderType=''2'') → Se hace UNION ALL entre ambos flujos para totalizar el consumo mensual por producto y sede', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.ATC; Inventory.ATCEntity; Inventory.ProductSubGroup; Inventory.PackagingUnit; Billing.BillingGroup; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Payroll.FunctionalUnit; Billing.ServiceOrder; Common.OperatingUnit; Inventory.TransferOrder; Inventory.TransferOrderDetail; Inventory.TransferOrderDetailBatchSerial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioConsumoPromedio';
GO
