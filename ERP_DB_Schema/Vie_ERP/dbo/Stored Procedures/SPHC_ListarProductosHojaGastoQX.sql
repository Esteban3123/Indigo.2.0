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