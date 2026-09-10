

CREATE VIEW [Inventory].[ViewAdmissionProduct]
AS
SELECT pdd.FunctionalUnitId,pdd.ProductId,pd.AdmissionNumber, p.Code + ' - ' + p.Name AS ProductName, 
fu.Code + ' - ' + fu.Name AS FunctionalUnitName, SUM(pdd.Quantity) AS DispensingQuantity, 
SUM(pdd.ReturnedQuantity) AS DevolutionQuantity, SUM(pdd.Quantity) - SUM(pdd.ReturnedQuantity) AS Quantity
FROM            Inventory.PharmaceuticalDispensingDetail AS pdd with (nolock)
INNER JOIN Inventory.PharmaceuticalDispensing AS pd with (nolock) ON pdd.PharmaceuticalDispensingId = pd.Id 
INNER JOIN  Inventory.InventoryProduct AS p with (nolock) ON pdd.ProductId = p.Id                          
INNER JOIN  Payroll.FunctionalUnit AS fu  with (nolock) ON pdd.FunctionalUnitId = fu.Id		     
 where pd.Status = 2
GROUP BY pdd.ProductId, p.Code, p.Name, fu.Code, fu.Name, pdd.FunctionalUnitId, pd.AdmissionNumber
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen de medicamentos e insumos dispensados por ingreso hospitalario y unidad funcional. Consolida los registros de dispensación farmacéutica activos (estado 2 = aprobado/ejecutado) cruzando el detalle de cada ítem despachado con el catálogo de productos y las unidades funcionales, mostrando por número de ingreso del paciente y área de atención: el código y nombre del producto, la cantidad total dispensada, la cantidad devuelta y el saldo neto consumido. Sirve para reportes de consumo de farmacia por hospitalización, control de inventario clínico y trazabilidad de medicamentos entregados a pacientes ingresados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewAdmissionProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewAdmissionProduct';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por admisión, producto y unidad funcional las cantidades dispensadas y devueltas (y su neto) de las dispensaciones farmacéuticas confirmadas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en PharmaceuticalDispensing con Status = 2 (dispensación confirmada/efectiva).; Cada detalle debe tener producto y unidad funcional válidos (joins INNER).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan dispensaciones con Status = 2.; La cantidad neta entregada por agrupación = total dispensado - total devuelto.; La agregación se hace por unidad funcional, producto y número de admisión.; Los nombres de producto y unidad funcional se presentan como ''Código - Nombre''.; Lecturas con NOLOCK: pueden incluir datos no confirmados (lectura sucia).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Devolución de medicamentos; Admisión del paciente; Producto de inventario; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewAdmissionProduct: Cuando pd.Status = 2, retorna por (FunctionalUnitId, ProductId, AdmissionNumber) la suma de Quantity como DispensingQuantity, la suma de ReturnedQuantity como DevolutionQuantity y su diferencia como Quantity neta.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pd.Status = 2 → Se incluye el detalle de dispensación en la agregación. else Se excluye del resultado (no aparecen dispensaciones en otros estados).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensing; Inventory.InventoryProduct; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAdmissionProduct';
GO
