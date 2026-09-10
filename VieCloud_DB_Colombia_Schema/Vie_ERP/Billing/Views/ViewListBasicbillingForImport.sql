

CREATE VIEW [Billing].[ViewListBasicbillingForImport]

AS
		SELECT	ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id
		,k.*
		FROM
		(
			--Cabecera
			SELECT 
					 bb.Code DocumentCode
					,bb.DocumentDate
					,cu.Id CustomerId
					,CONCAT(cu.Nit,' - ',cu.Name) CustomerCodeName
					,i.InvoiceNumber
					,bb.SaleModality
					, CASE bb.SaleModality
						WHEN 1 THEN 'Contado'
						WHEN 2 THEN 'Credito'
					END SaleModalityName
					,c.Id CurrencyId
					,c.Abbreviation CurrencyAbbreviation
					,bb.Id HeaderId
					,CAST(0 as bit) Selected
					,NULL DetailId
					,NULL DetailType
					,NULL DetailTypeName
					,NULL FunctionalUnitCodeName
					,NULL ItemCodeName
					,NULL Quantity
					,NULL UnitValue
					,NULL ValueIVA
					,NULL TotalValue
					,NULL BasicBillingId
			FROM Billing.BasicBilling bb
			JOIN Common.Customer cu ON cu.Id = bb.CustomerId
			JOIN Billing.Invoice i ON i.Id = bb.InvoiceId
			JOIN Common.Currency c ON c.Id = bb.CurrencyId
			WHERE bb.Status = 2

			UNION ALL

			--Detalles
			SELECT	
					 bb.code DocumentCode
					,NULL DocumentDate
					,NULL CustomerId
					,NULL CustomerCodeName
					,NULL InvoiceNumber
					,NULL SaleModality
					,NULL SaleModalityName
					,NULL CurrencyId
					,NULL CurrencyAbbreviation
					,NULL HeaderId
					,CAST(0 as bit) Selected
					,bbd.Id DetailId
					,bbd.DetailType
					,CASE bbd.DetailType
						WHEN 1 THEN 'Producto'
						WHEN 2 THEN 'Servicio'
						WHEN 3 THEN 'Activo Fijo'
					 END DetailTypeName
					,CONCAT(fu.Code, ' - ' ,fu.Name) FunctionalUnitCodeName
					,CASE bbd.DetailType
						WHEN 1 THEN CONCAT(ip.Code, ' - ', ip.Name)
						WHEN 2 THEN CONCAT(bc.Code, ' - ', bc.Name)
						WHEN 3 THEN CONCAT(fai.Code, ' - ', fai.Description)
					END ItemCodeName
					,bbd.Quantity
					,bbd.Price UnitValue
					,(ROUND((bbd.Value - bbd.ValueDiscount) * bbd.PercentageIVA / 100, 2)) ValueIVA
					,bbd.Value TotalValue
					,bbd.BasicBillingId
			FROM Billing.BasicBillingDetail bbd
			JOIN Billing.BasicBilling bb ON bb.Id = bbd.BasicBillingId
			JOIN Payroll.FunctionalUnit fu ON fu.Id = bbd.FunctionalUnitId
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = bbd.ProductId
			LEFT JOIN Billing.BillingConcept bc ON bc.Id = bbd.BillingConceptId
			LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fh ON fh.Id = bbd.PhysicalAssetId
			LEFT JOIN FixedAsset.FixedAssetItem fai ON fai.Id = fh.ItemId
			WHERE bb.Status = 2

			UNION ALL

			--Obsequios
			SELECT	
					 bb.code DocumentCode
					,NULL DocumentDate
					,NULL CustomerId
					,NULL CustomerCodeName
					,NULL InvoiceNumber
					,NULL SaleModality
					,NULL SaleModalityName
					,NULL CurrencyId
					,NULL CurrencyAbbreviation
					,NULL HeaderId
					,CAST(0 as bit) Selected
					,bbg.Id DetailId
					,NULL DetailType
					,'Obsequio' DetailTypeName
					,NULL FunctionalUnitCodeName
					,CONCAT(ip.Code, ' - ', ip.Name) ItemCodeName
					,bbg.Quantity
					,NULL UnitValue
					,NULL ValueIVA
					,NULL TotalValue
					,bbg.BasicBillingId
			FROM Billing.BasicBillingGifts bbg
			JOIN Billing.BasicBilling bb ON bb.Id = bbg.BasicBillingId
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = bbg.ProductId
			WHERE bb.Status = 2
		) K
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de prefacturas o documentos de facturación básica en estado aprobado (status = 2), lista para ser importada o procesada. Integra en una sola consulta tres bloques: el encabezado del documento (cliente pagador con NIT y nombre, número de factura asociada, modalidad de pago contado o crédito, moneda y código del documento), el detalle de líneas facturadas (ítems de tipo producto, servicio o activo fijo con cantidades, precios unitarios, IVA calculado y valor total, agrupados por unidad funcional), y los obsequios incluidos en la factura básica. Se usa como fuente de datos para pantallas de importación, revisión o migración de facturas básicas, permitiendo visualizar encabezado y líneas de detalle en una estructura plana lista para selección o carga masiva.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListBasicbillingForImport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListBasicbillingForImport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado las facturaciones básicas en estado 2 (aprobadas/listas para importar) junto con sus detalles y obsequios, para alimentar un proceso de importación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBasicbillingForImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Billing.BasicBilling con Status = 2.; Cada BasicBilling tiene Customer, Invoice y Currency asociados (joins inner en la cabecera).; Los detalles requieren FunctionalUnit válida (join inner en Payroll.FunctionalUnit).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBasicbillingForImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen documentos de facturación básica con Status = 2.; El IVA por línea se calcula como ROUND((Value - ValueDiscount) * PercentageIVA / 100, 2).; Las filas de cabecera no traen detalle (DetailId/Quantity/Valores en NULL) y las de detalle/obsequio no traen cabecera (CustomerId/InvoiceNumber/etc en NULL).; Los obsequios no aportan valores monetarios (UnitValue, ValueIVA, TotalValue siempre NULL).; El campo Selected siempre se inicializa en 0 (bit) para todas las filas.; El Id se asigna por ROW_NUMBER sin orden determinístico (ORDER BY (SELECT NULL)).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBasicbillingForImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación básica; Cliente/pagador; Factura; Moneda; Modalidad de venta (Contado/Crédito); Unidad funcional; Producto de inventario; Concepto de facturación; Activo fijo; Obsequio/bonificación; IVA; Descuento', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBasicbillingForImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListBasicbillingForImport: Devuelve filas de tipo cabecera, detalle y obsequio unificadas mediante UNION ALL, todas filtradas por bb.Status = 2.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBasicbillingForImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si bb.SaleModality = 1 → Etiqueta SaleModalityName como ''Contado'' else Si SaleModality = 2 entonces ''Credito''; si bbd.DetailType = 1 (Producto) → ItemCodeName se arma con InventoryProduct (ip.Code - ip.Name) y DetailTypeName=''Producto'' else Si =2 usa BillingConcept (Servicio); si =3 usa FixedAssetItem vía PhysicalAsset (Activo Fijo); si Origen = Billing.BasicBillingGifts → DetailTypeName se fija en ''Obsequio'' y ItemCodeName se arma desde InventoryProduct, sin valores monetarios', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBasicbillingForImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BasicBilling; Common.Customer; Billing.Invoice; Common.Currency; Billing.BasicBillingDetail; Payroll.FunctionalUnit; Inventory.InventoryProduct; Billing.BillingConcept; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; Billing.BasicBillingGifts', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBasicbillingForImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBasicbillingForImport';
GO
