

CREATE VIEW [dbo].[Productos_Insumos]
AS
SELECT        t.Name AS Tipo, ISU.Code AS CodigoInsumo, ISU.SupplieName AS NombreInsumo, P.Code AS CodigoProducto, P.CodeAlternative AS CodigoAlternativo, P.CodeAlternativeTwo AS CodigoAlternativo2, P.Name, 
                         P.Description AS NombreProducto, PG.Code AS CodigoGrupo, PG.Name AS NombreGrupo, PSG.Code AS CodigoSubGrupo, PSG.Name AS NombreSubGrupo, PU.Code AS CodigoUnidadEmpaque, 
                         PU.Name AS NombreUnidadEmpaque, M.Code AS CodigoProveedor, M.Name AS Proveedor, IVA.Code AS CodigoIva, IVA.Name AS NombreIva, 
                         CASE HandlesHealthRegistration WHEN 1 THEN 'Si' ELSE 'No' END AS ManejaRegistro, P.HealthRegistration AS Registro, P.ExpirationDate AS FechaVencimiento, BG.Code AS CodigoGrupoFac, BG.Name AS NombreGrupoFac, 
                         CASE ProductControl WHEN 1 THEN 'Si' ELSE 'No' END AS InsumoControl, CASE P.POSProduct WHEN 1 THEN 'Si' ELSE 'no' END AS ProductoPos, P.ProductCost AS Costo, P.FinalProductCost AS UltimoCosto, 
                         P.SellingPrice AS PrecioVenta, P.ControlCostPercentage AS PorcentajeControl, R.Code AS CodigoRiesgo, R.Name AS NombreRiesgo, P.SerialNumber, 
                         CASE P.Consumption WHEN 1 THEN 'Si' ELSE 'No' END AS ProductoComoConsumo,ISNULL(STUFF((SELECT DISTINCT ', ' + CONCAT(bs.BatchCode, ' - ', bs.ExpirationDate) FROM Inventory.BatchSerial bs WHERE bs.productId = p.Id AND CAST(ISNULL(bs.ExpirationDate, GETDATE()) AS DATE) >= CAST(GETDATE() AS DATE) FOR XML PATH ('')), 1,2, ''), '') LoteFechaVencimiento

FROM            Inventory.InventoryProduct AS P LEFT OUTER JOIN
                         Inventory.InventorySupplie AS ISU ON ISU.Id = P.SupplieId LEFT OUTER JOIN
                         Inventory.ProductGroup AS PG ON P.ProductGroupId = PG.Id LEFT OUTER JOIN
                         Inventory.ProductSubGroup AS PSG ON P.ProductSubGroupId = PSG.Id LEFT OUTER JOIN
                         Inventory.PackagingUnit AS PU ON P.PackagingUnitId = PU.Id LEFT OUTER JOIN
                         Inventory.Manufacturer AS M ON P.ManufacturerId = M.Id LEFT OUTER JOIN
                         GeneralLedger.GeneralLedgerIVA AS IVA ON P.IVAId = IVA.Id LEFT OUTER JOIN
                         Billing.BillingGroup AS BG ON P.BillingGroupId = BG.Id LEFT OUTER JOIN
                         Inventory.InventoryRiskLevel AS R ON P.InventoryRiskLevelId = R.Id LEFT OUTER JOIN
                         Inventory.ProductType AS t ON P.ProductTypeId = t.Id 
					
WHERE        (P.ProductTypeId NOT IN (1)) AND (P.Status = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el catálogo completo de productos de inventario activos, excluyendo los de tipo medicamento (tipo 1), integrando información de insumos, dispositivos médicos y demás artículos gestionados en farmacia y suministros. Combina datos de clasificación (grupo, subgrupo, tipo de producto), presentación (unidad de empaque), fabricante o laboratorio, IVA, grupo de facturación y nivel de riesgo, junto con precios, costos y códigos de identificación (código principal, alternativo y de insumo). Adicionalmente expone los lotes vigentes con sus fechas de vencimiento asociadas a cada producto, permitiendo consultas de inventario en tiempo real. Es utilizada para reportería, búsqueda de productos, consulta de catálogo de insumos y dispositivos médicos, y como fuente de información para procesos de facturación, farmacia y control de almacén.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Productos_Insumos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Productos_Insumos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el catálogo de productos activos del inventario (excluyendo un tipo específico) con sus atributos comerciales, contables, de riesgo y lotes vigentes para consulta unificada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Insumos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos deben tener Status = 1 (activos); Los productos deben pertenecer a un ProductTypeId distinto de 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Insumos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone productos activos (Status=1); Excluye siempre los productos de ProductTypeId=1; Los lotes mostrados nunca están vencidos respecto a la fecha actual; Si no existen lotes vigentes, LoteFechaVencimiento es cadena vacía en lugar de NULL; Las relaciones con catálogos (grupo, subgrupo, fabricante, IVA, riesgo, etc.) son opcionales (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Insumos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto; Insumo; Grupo de producto; Subgrupo de producto; Unidad de empaque; Fabricante/Proveedor; IVA; Grupo de facturación; Nivel de riesgo; Registro sanitario; Fecha de vencimiento; Lote/Serial; Costo; Precio de venta; Producto POS; Producto de consumo; Insumo de control', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Insumos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.InventoryProduct: Devuelve productos donde Status=1 y ProductTypeId<>1, enriquecidos con insumo, grupo, subgrupo, unidad de empaque, fabricante, IVA, grupo de facturación, nivel de riesgo y tipo; [RETURN_RESULT] Inventory.BatchSerial: Concatena (BatchCode - ExpirationDate) por producto solo cuando ExpirationDate (o GETDATE() si es NULL) >= fecha actual, es decir, lotes no vencidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Insumos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HandlesHealthRegistration = 1 → Marca ManejaRegistro = ''Si'' else Marca ManejaRegistro = ''No''; si ProductControl = 1 → Marca InsumoControl = ''Si'' else Marca InsumoControl = ''No''; si POSProduct = 1 → Marca ProductoPos = ''Si'' else Marca ProductoPos = ''no''; si Consumption = 1 → Marca ProductoComoConsumo = ''Si'' else Marca ProductoComoConsumo = ''No''; si BatchSerial.ExpirationDate IS NULL o >= fecha actual → Incluye el lote en la concatenación LoteFechaVencimiento else Excluye lotes vencidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Insumos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.InventorySupplie; Inventory.ProductGroup; Inventory.ProductSubGroup; Inventory.PackagingUnit; Inventory.Manufacturer; GeneralLedger.GeneralLedgerIVA; Billing.BillingGroup; Inventory.InventoryRiskLevel; Inventory.ProductType; Inventory.BatchSerial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Insumos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Productos_Insumos';
GO
