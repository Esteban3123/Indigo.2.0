

/*******************************************************************************************************************
Nombre: ViewInventarioAjuste
Tipo:Vista
Observacion:Ajuste de inventario
Profesional: Nilsson Miguel Galindo Lopez
Fecha:29-06-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:28-05-2024
Ovservaciones: ahora en la union con la tabla Inventory.AdjustmentConcept, es un left join
--------------------------------------
Vercion 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewInventarioAjuste] AS

SELECT        
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 CASE A.OperatingUnitId WHEN 1 THEN 'Bogota' END AS Sede, 
 A.DocumentDate AS Fecha, 
 A.Code AS Documento, 
 CASE A.AdjustmentType WHEN 1 THEN 'Entrada' 
					  WHEN 2 THEN 'Salida' END AS Tipo, 
 B.Code AS CodAlm, 
 B.Name AS Almacen, 
 C.Code AS [Codigo de Concepto], 
 C.Name AS Concepto,
 P.Code AS [Codidogo de Producto], 
 P.Name AS Producto, 
 DA.Quantity AS Cantidad, 
 DA.UnitValue AS [Valor Unitario], 
 DA.Quantity * DA.UnitValue AS [Valor Total], 
 P.ProductCost AS [Costo Promedio], 
 A.Description AS Detalle, 
 U.UserCode+'-'+U1.Fullname AS [Usuario Creacion Ajuste],
 CAST(A.DocumentDate  AS date) AS 'FECHA BUSQUEDA',
 YEAR(A.DocumentDate) AS 'AÑO BUSQUEDA',
 MONTH(A.DocumentDate) AS 'MES BUSQUEDA',
 CONCAT(FORMAT(MONTH(A.DocumentDate), '00') ,' - ', 
	    CASE MONTH(A.DocumentDate) 
	     WHEN 1 THEN 'ENERO'
   	     WHEN 2 THEN 'FEBRERO'
	     WHEN 3 THEN 'MARZO'
	     WHEN 4 THEN 'ABRIL'
	     WHEN 5 THEN 'MAYO'
	     WHEN 6 THEN 'JUNIO'
	     WHEN 7 THEN 'JULIO'
	     WHEN 8 THEN 'AGOSTO'
	     WHEN 9 THEN 'SEPTIEMBRE'
	     WHEN 10 THEN 'OCTUBRE'
	     WHEN 11 THEN 'NOVIEMBRE'
	     WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM            
Inventory.InventoryAdjustment AS A 
INNER JOIN Inventory.InventoryAdjustmentDetail AS DA ON DA.InventoryAdjustmentId = A.Id AND A.Status = 2 
INNER JOIN Inventory.Warehouse AS B ON A.WarehouseId = B.Id 
LEFT JOIN Inventory.AdjustmentConcept AS C ON A.AdjustmentConceptId = C.Id 
LEFT JOIN Inventory.InventoryProduct AS P ON DA.ProductId = P.Id 
LEFT JOIN Security.[User] AS U ON A.CreationUser = U.UserCode 
LEFT JOIN Security.Person AS U1 ON U.IdPerson = U1.Id
--where a.Code=2319
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida los ajustes de inventario confirmados (Status = 2) para consumo en herramientas de análisis. Combina el encabezado del ajuste con su detalle por producto, incluyendo tipo de movimiento (entrada/salida), almacén, concepto de ajuste, cantidades, valor unitario, valor total y costo promedio del producto. Expone campos auxiliares de filtro temporal (fecha, año, mes con nombre en español) y el usuario creador del ajuste, identificando la compañía mediante `DB_NAME()`.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los ajustes de inventario aprobados con sus detalles, almacén, concepto, producto, valores y usuario creador, para análisis por fecha/mes/año.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ajuste de inventario debe tener Status = 2 (aprobado/confirmado) para aparecer en el reporte; Debe existir al menos un detalle (InventoryAdjustmentDetail) asociado al ajuste', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ajustes con Status = 2; ID_COMPANY se deriva dinámicamente de DB_NAME() truncado a 9 caracteres; ULT_ACTUAL se calcula con la zona horaria ''Pakistan Standard Time'' convertida a DATETIME; Valor Total siempre es Quantity * UnitValue del detalle; La relación con AdjustmentConcept, InventoryProduct, User y Person es opcional (LEFT JOIN), por lo que estos campos pueden ser NULL; La relación con Warehouse es obligatoria (INNER JOIN): solo se muestran ajustes con almacén válido', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ajuste de inventario; Entrada de inventario; Salida de inventario; Almacén/Bodega; Concepto de ajuste; Producto de inventario; Costo promedio; Sede/Unidad operativa; Usuario creador del ajuste', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewInventarioAjuste: Devuelve una fila por cada detalle de ajuste de inventario cuyo encabezado tenga Status=2, enriquecida con almacén, concepto, producto y usuario', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.OperatingUnitId = 1 → Sede = ''Bogota'' else Sede = NULL (no hay otros valores mapeados); si A.AdjustmentType = 1 → Tipo = ''Entrada'' else Si AdjustmentType = 2 → ''Salida''; otros valores → NULL; si MONTH(A.DocumentDate) entre 1 y 12 → Se concatena el número de mes con su nombre en español (ENERO..DICIEMBRE)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryAdjustment; Inventory.InventoryAdjustmentDetail; Inventory.Warehouse; Inventory.AdjustmentConcept; Inventory.InventoryProduct; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioAjuste';
GO
