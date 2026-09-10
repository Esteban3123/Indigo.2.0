CREATE PROCEDURE  [dbo].[SPHC_ListarProductosHojaGastoQX]
(
       -- Add the parameters for the stored procedure here
	@IdHojaGastoQX as Integer

)
AS
BEGIN
SELECT  
  * 
FROM  
		  (
			SELECT 
				CASE WHEN NULLIF(H.IdNursingPackagesOrder, '') IS NULL THEN 'Paquete productos adicionales' ELSE 'Paquete quirúrgico - ' + Rtrim(Paquete.NOMBRE)  END AS Orden,
				H.ID, 
				H.IDHCHOJAGASTOQX, 
				P.CODPRODUC as CodigoProducto, 
				rtrim(P.CODPRODUC) as CodigoP, 
				rtrim(P.DESPRODUC) as Producto, 
				P.DESPRODUC as NombreP, 
				H.CANTIDADENTREGADA, 
				H.CANTIDADACEPTADADEV, 
				H.CANTIDADGASTADA, 
				ISNULL(H.CANTIDADGASTADA, 0) AS 'CantidadGastadaInicial', 
				H.CANTIDADDEVOLVER, 
				H.ORIGENSOLICITUD, 
				H.FECHAREGISTRO, 
				'Dispensación' as OrigenProducto, 
				C.IDAGEPROGQX, 
				H.CONSEKARDEX, 
				ISNULL(H.RequestType, 1) as RequestType, 
      
				/*ISNULL(H.StatusOrder,1)) as StatusOrder */
				IIF(
				CANTIDADDEVOLVER = 0, 
				2, 
				ISNULL(H.StatusOrder, 1)
				) AS StatusOrder 
			FROM 
				.dbo.HCHOJAGASTOQX C with(nolock) 
				INNER JOIN .dbo.HCHOJAGASTOQXD H with(nolock) on H.IDHCHOJAGASTOQX = C.ID 
				INNER JOIN .dbo.IHLISTPRO P on P.CODPRODUC = H.CODPRODUC 
				LEFT JOIN MedicalHistory.NursingPackagesOrder Orden with(nolock) ON Orden.Id = H.IdNursingPackagesOrder 
				LEFT JOIN .dbo.AGPAQUETES Paquete with(nolock) ON Paquete.ID  = Orden.IdAGPAQUETES	
			WHERE 
				H.IDHCHOJAGASTOQX = @IdHojaGastoQX
				AND H.ORIGENSOLICITUD = 1 
				AND H.RequestType = 1
     
	UNION ALL
		 
			SELECT 
			CASE H.RequestType WHEN 3 THEN 'Paquete enfermería - ' + Rtrim(Paquete.NOMBRE) ELSE 'Producto adicionales' END AS Orden,
				H.ID, 
				H.IDHCHOJAGASTOQX, 
				P.CODPRODUC as CodigoProducto, 
				rtrim(P.CODPRODUC) as CodigoP, 
				rtrim(P.DESPRODUC) as Producto,
				P.DESPRODUC as NombreP, 
				H.CANTIDADENTREGADA, 
				H.CANTIDADACEPTADADEV, 
				H.CANTIDADGASTADA, 
				ISNULL(H.CANTIDADGASTADA, 0) AS 'CantidadGastadaInicial', 
				H.CANTIDADDEVOLVER, 
				H.ORIGENSOLICITUD, 
				H.FECHAREGISTRO, 
				'Dispensacion' as OrigenProducto, 
				C.IDAGEPROGQX, 
				H.CONSEKARDEX, 
				ISNULL(H.RequestType, 3) as RequestType, 
      
				/*ISNULL(H.StatusOrder,1)) as StatusOrder */
				IIF(
				CANTIDADDEVOLVER = 0, 
				2, 
				ISNULL(H.StatusOrder, 1)
				) AS StatusOrder 
			FROM 
				.dbo.HCHOJAGASTOQX C with(nolock) 
				INNER JOIN .dbo.HCHOJAGASTOQXD H with(nolock) on H.IDHCHOJAGASTOQX = C.ID 
				INNER JOIN .dbo.IHLISTPRO P with(nolock) on P.CODPRODUC = H.CODPRODUC 
				LEFT JOIN MedicalHistory.NursingPackagesOrder Orden with(nolock) ON Orden.Id = H.IdNursingPackagesOrder 
				LEFT JOIN .dbo.AGPAQUETES Paquete with(nolock) ON Paquete.ID  = Orden.IdAGPAQUETES	
			   
			WHERE 
				H.IDHCHOJAGASTOQX = @IdHojaGastoQX 
				AND H.RequestType IN (2,3)
			
UNION ALL
			
			SELECT 
			 'Adicionales' AS Orden,
			  H.ID, 
			  H.IDHCHOJAGASTOQX, 
			  P.Code As CodigoProducto, 
			  P.Code as CodigoP,
			  P.Name as Producto, 
			  P.Name as NombreP, 
			  H.CANTIDADENTREGADA, 
			  H.CANTIDADACEPTADADEV, 
			  H.CANTIDADGASTADA, 
			  ISNULL(H.CANTIDADGASTADA, 0) AS 'CantidadGastadaInicial', 
			  H.CANTIDADDEVOLVER, 
			  H.ORIGENSOLICITUD, 
			  H.FECHAREGISTRO, 
			  'Inventario en consignación' as OrigenProducto, 
			  C.IDAGEPROGQX, 
			  H.CONSEKARDEX, 
			  ISNULL(H.RequestType, 1) as RequestType, 
      
			  /*ISNULL(H.StatusOrder,1)) as StatusOrder */
			  IIF(
				CANTIDADDEVOLVER = 0, 
				2, 
				ISNULL(H.StatusOrder, 1)
			  ) AS StatusOrder 
			FROM 
			 .dbo.HCHOJAGASTOQX C with(nolock) 
			  INNER JOIN .dbo.HCHOJAGASTOQXD H with(nolock) on H.IDHCHOJAGASTOQX = C.ID 
			  INNER JOIN .Inventory.InventoryProduct P with(nolock) on P.ID = H.IDPRODUCTO 
			WHERE 
			  H.IDHCHOJAGASTOQX = @IdHojaGastoQX 
			  AND H.ORIGENSOLICITUD = 2
  ) tmp 
ORDER BY 
  tmp.Orden

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los productos e insumos registrados en una hoja de gasto quirúrgico (sala de cirugía), dado el identificador de la hoja. Consolida tres orígenes distintos mediante UNION: productos dispensados desde farmacia asociados a paquetes quirúrgicos o adicionales (tipo de solicitud 1), productos de paquetes de enfermería o adicionales de enfermería (tipos 2 y 3), y productos provenientes de inventario en consignación. Para cada ítem devuelve el nombre del paquete o categoría al que pertenece, el código y descripción del producto, las cantidades entregadas, aceptadas en devolución, gastadas y a devolver, la fecha de registro, el origen de la solicitud, el consecutivo de kárdex y el estado de la orden (calculado según si la cantidad a devolver es cero). Es el procedimiento central para visualizar el detalle de consumo de materiales e insumos en una intervención quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos consumidos en una hoja de gasto quirúrgica, clasificándolos por origen (paquete quirúrgico, paquete de enfermería, productos adicionales o inventario en consignación).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la hoja de gasto quirúrgica identificada por el parámetro de entrada; Los productos referenciados deben existir en el catálogo (IHLISTPRO o Inventory.InventoryProduct según el origen)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los productos con ORIGENSOLICITUD=1 y RequestType=1 corresponden al paquete quirúrgico o sus adicionales; Los productos con RequestType IN (2,3) corresponden a paquetes de enfermería o productos adicionales de dispensación; Los productos con ORIGENSOLICITUD=2 provienen siempre de inventario en consignación; Si no hay cantidad por devolver el ítem se reporta como StatusOrder=2 (cerrado/finalizado); RequestType por defecto es 1 para dispensación quirúrgica y 3 para enfermería cuando es NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hoja de gasto quirúrgica; Paquete quirúrgico; Paquete de enfermería; Productos adicionales; Dispensación; Inventario en consignación; Kardex; Programación quirúrgica; Devolución de productos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la unión de tres conjuntos: productos de dispensación con ORIGENSOLICITUD=1 y RequestType=1 (paquete quirúrgico o adicionales), productos de dispensación con RequestType IN (2,3) (paquete enfermería o adicionales), y productos con ORIGENSOLICITUD=2 desde inventario en consignación; ordenados por la etiqueta de Orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si H.IdNursingPackagesOrder IS NULL o vacío en el primer bloque (ORIGENSOLICITUD=1, RequestType=1) → Etiqueta el grupo como ''Paquete productos adicionales'' else Etiqueta como ''Paquete quirúrgico - '' + nombre del paquete; si H.RequestType = 3 en el segundo bloque → Etiqueta como ''Paquete enfermería - '' + nombre del paquete else Etiqueta como ''Producto adicionales''; si CANTIDADDEVOLVER = 0 → StatusOrder se fuerza a 2 else Se conserva StatusOrder original (o 1 si es NULL); si ORIGENSOLICITUD = 2 → El producto se considera ''Inventario en consignación'' y se resuelve contra Inventory.InventoryProduct por IDPRODUCTO else Se considera ''Dispensación'' y se resuelve contra IHLISTPRO por CODPRODUC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAGASTOQX; dbo.HCHOJAGASTOQXD; dbo.IHLISTPRO; MedicalHistory.NursingPackagesOrder; dbo.AGPAQUETES; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosHojaGastoQX';
-- GO
