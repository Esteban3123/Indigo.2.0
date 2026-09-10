
/*-----------------------------------------------------------------------------------
Persona que modifico: Amira Gil Meneses
Fecha:25/04/2024
Observaciones: Se modifican los codigos de la tabla Inventory.Warehouse para ODO
--------------------------------------------------------------------------------------*/

CREATE view [Report].[ViewInventarioSaldoAlmacenes] as

SELECT        
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
*,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM
(
SELECT
PR.Id AS IDPr, 
pr.Code AS Código, pr.Name AS Producto, TP.Name AS [TipoProducto], 
Med.Code AS [Cod Med], 
Med.Name AS Medicamento, ATC.Code AS ATC, ATC.Name AS NombreATC, pr.CodeCUM AS CUM, 
pr.CodeAlternativeTwo AS [CódigoAlterno2], sg.Name AS SubGrupo, ue.Abbreviation AS Unidad, gf.Name AS [GrupoFacturación], CASE pr.ProductControl WHEN '0' THEN 'No' WHEN '1' THEN 'Si' END AS [ProdControl],
pr.ProductCost AS CostoPromedio, pr.FinalProductCost AS Ultimocosto, CASE pr.ProductWithPriceControl WHEN 0 THEN '' WHEN 1 THEN 'SI' END AS Regulado, 
CASE pr.Status WHEN '1' THEN 'Activo' WHEN '0' THEN 'Inactivo' END AS Estado, 
inf.Quantity AS Cantidad,
al.Code AS CodAlmacen
FROM            
Inventory.InventoryProduct AS pr LEFT OUTER JOIN
Inventory.PhysicalInventory AS inf ON inf.ProductId = pr.Id LEFT OUTER JOIN
Inventory.Warehouse AS al ON al.Id = inf.WarehouseId LEFT OUTER JOIN
Inventory.ATC AS Med ON Med.Id = pr.ATCId LEFT OUTER JOIN
Inventory.ProductSubGroup AS sg ON sg.Id = pr.ProductSubGroupId LEFT OUTER JOIN
Inventory.PackagingUnit AS ue ON ue.Id = pr.PackagingUnitId LEFT OUTER JOIN
Billing.BillingGroup AS gf ON gf.Id = pr.BillingGroupId LEFT JOIN
Inventory.ATCEntity ATC ON Med.ATCEntityId = ATC.Id JOIN
Inventory.ProductType TP ON PR.ProductTypeId = TP.Id
WHERE
al.code IN ('001','002','003','004','005','006','007','008','009','010','011','012','013','014','015','016','017','018','019','020','021','022',
'023','024','025','026','027','028','029','030','031','032','033','034','035','036','037','038','039','040','041','042','043','044','045','046',
'047','048','049','050','051','052','053','055','056','057','058','059','060','061','062','063','064','065','066','068','069','070','071','073',
'074','080','081','082','083','084','085','086','087','088','089','090','091','092','093','094','054','072','075','077','078','079','095','096',
'097','098','099','100','101','102','103','104','105','106','107','108','109','110','111','112','114','115','116','117','118','119','120','121',
'122','123','124','125','126','127','128','129','130','131','132','133','134','135','136','137','138','139','140','141','142','143','144','145',
'146','147','148','149','150','151','152','153','154','155','156','157','158','159','160','161','162','163','164','165','166','167','168','169',
'170','171','172','173','174','175','176','177','178','179','180','181','182','183','184','185','186','187','188','189','190','191','192','193',
'194','195','196','197','198','199','200','201','202','203','204','205','206','207','208','209','210','211') AND INF.Quantity > 0) source PIVOT (sum(Cantidad) 
FOR source.CodAlmacen IN ([001],[002],[003],[004],[005],[006],[007],[008],[009],[010],[011],[012],[013],[014],[015],[016],[017],[018],[019],[020],
[021],[022],[023],[024],[025],[026],[027],[028],[029],[030],[031],[032],[033],[034],[035],[036],[037],[038],[039],[040],[041],[042],[043],[044],
[045],[046],[047],[048],[049],[050],[051],[052],[053],[055],[056],[057],[058],[059],[060],[061],[062],[063],[064],[065],[066],[068],[069],[070],
[071],[073],[074],[080],[081],[082],[083],[084],[085],[086],[087],[088],[089],[090],[091],[092],[093],[094],[054],[072],[075],[077],[078],[079],
[095],[096],[097],[098],[099],[100],[101],[102],[103],[104],[105],[106],[107],[108],[109],[110],[111],[112],[114],[115],[116],[117],[118],[119],
[120],[121],[122],[123],[124],[125],[126],[127],[128],[129],[130],[131],[132],[133],[134],[135],[136],[137],[138],[139],[140],[141],[142],[143],
[144],[145],[146],[147],[148],[149],[150],[151],[152],[153],[154],[155],[156],[157],[158],[159],[160],[161],[162],[163],[164],[165],[166],[167],
[168],[169],[170],[171],[172],[173],[174],[175],[176],[177],[178],[179],[180],[181],[182],[183],[184],[185],[186],[187],[188],[189],[190],[191],
[192],[193],[194],[195],[196],[197],[198],[199],[200],[201],[202],[203],[204],[205],[206],[207],[208],[209],[210],[211])) AS pivotable
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consumo analítico que presenta el saldo de inventario físico de cada producto (medicamentos e insumos) pivotado por almacén: cada columna corresponde a un código de bodega (001–211) con la cantidad disponible. Consolida atributos del producto como clasificación ATC, CUM, subgrupo, unidad de empaque, grupo de facturación, costos y estado, filtrando únicamente bodegas con existencias positivas. Incluye el nombre de la base de datos y la fecha/hora en zona horaria de Pakistán para trazabilidad de la extracción.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida el saldo de inventario por producto pivotando las cantidades existentes en cada almacén (códigos 001 a 211) en columnas, junto con datos descriptivos del producto y la fecha de actualización.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las bodegas a reportar deben existir en Inventory.Warehouse con códigos numéricos en el rango definido (001..211, con algunas exclusiones).; Inventory.PhysicalInventory debe registrar cantidades por producto y bodega.; Cada producto debe tener un ProductType asociado (JOIN obligatorio con Inventory.ProductType).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El ID_COMPANY siempre se obtiene del nombre de la base de datos actual (DB_NAME()) truncado a 9 caracteres.; La marca de tiempo ULT_ACTUAL siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Solo se incluyen productos con existencia positiva (Quantity > 0) en alguna bodega del rango permitido.; Los almacenes con códigos 067, 076 y otros omitidos en la lista (p.ej. 113) quedan excluidos del reporte por no estar enumerados.; El JOIN obligatorio con ProductType excluye productos sin tipo definido; los demás catálogos (ATC, SubGrupo, Unidad, GrupoFacturación) son LEFT JOIN y permiten valores nulos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'inventario; saldo por almacén; producto; medicamento; clasificación ATC; CUM; subgrupo de producto; unidad de empaque; grupo de facturación; costo promedio; último costo; producto regulado; control de precios; tipo de producto', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Result set de la vista: Solo se incluyen filas donde al.code esté en la lista enumerada de almacenes y INF.Quantity > 0; las cantidades se pivotean sumándose por código de almacén.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pr.ProductControl = ''0'' → Se reporta ''No'' en columna ProdControl else Si ProductControl = ''1'' se reporta ''Si''; si pr.ProductWithPriceControl = 1 → Se reporta ''SI'' en columna Regulado else Si = 0 se reporta cadena vacía; si pr.Status = ''1'' → Se reporta ''Activo'' en columna Estado else Si Status = ''0'' se reporta ''Inactivo''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.ATC; Inventory.ProductSubGroup; Inventory.PackagingUnit; Billing.BillingGroup; Inventory.ATCEntity; Inventory.ProductType', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSaldoAlmacenes';
GO
