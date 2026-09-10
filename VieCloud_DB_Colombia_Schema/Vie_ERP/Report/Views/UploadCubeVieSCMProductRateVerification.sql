--CREATE PROCEDURE [Contract].[ReporteVerificacionTarifasProductos]
	-- Add the parameters for the stored procedure here
--	@product CHAR(20)
--AS

CREATE view [Report].[UploadCubeVieSCMProductRateVerification] as

	--IF '202020'='Medicamento' --@product = 'Medicamento'
	--	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	--	    pr.Code 'CODIGO TARIFA',--[CodigoTarifa],
	--		pr.Name 'TARIFA',--[Tarifa],
	--		ip.Code 'CODIGO PRODUCTO',--[CodigoProducto],
	--		ip.Name 'PRODUCTO',--[Producto],
	--		prd.SalesValue 'VALOR',--[Valor],
	--		IIF(ISNULL(prd.Id, 0) > 0, 'Si', 'No') 'CUBIERTO',--[Cubierto],
	--		IIF(ISNULL(prd.Contracted, 0) = 1, 'Si', 'No') 'CONTRATADO',--[Contratado],
	--		IIF(ISNULL(prd.Quoted, 0) = 1, 'Si', 'No') 'COTIZADO',--[Cotizado],
	--		prd.Observations 'OBSERVACIO',--[Observacion],
	--		cg.Code 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion],
	--		cg.Name 'GRUPO ATENCION',--[GrupoAtencion],
	--		c.ContractNumber 'NRO CONTRATO',--[NroContrato],
	--		prd.InitialDate 'FECHA INICIAL',--[FechaInicial] ,
	--		prd.EndDate 'FECHA FINAL',--[FechaFinal]
	--	    cast(prd.InitialDate as date) 'FECHA BUSQUEDA',
	--	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	--	FROM Inventory.InventoryProduct ip
	--	LEFT JOIN Inventory.ProductRateDetail prd ON ip.Id = prd.ProductId
	--	LEFT JOIN Inventory.ProductRate pr ON prd.ProductRateId = pr.id
	--	INNER JOIN Contract.CareGroup cg ON pr.Id = cg.ProductRateId AND cg.status = 1
	--	INNER JOIN Contract.Contract c ON cg.ContractId = c.Id
	--	WHERE ip.producttypeid = 1
	--ELSE 
		SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		    pr.Code 'CODIGO TARIFA',--[CodigoTarifa],
			pr.Name 'TARIFA',--[Tarifa],
			ip.Code 'CODIGO PRODUCTO',--[CodigoProducto],
			ip.Name 'PRODUCTO',--[Producto],
			prd.SalesValue 'VALOR',--[Valor],
			IIF(ISNULL(prd.Id, 0) > 0, 'Si', 'No') 'CUBIERTO',--[Cubierto],
			IIF(ISNULL(prd.Contracted, 0) = 1, 'Si', 'No') 'CONTRATADO',--[Contratado],
			IIF(ISNULL(prd.Quoted, 0) = 1, 'Si', 'No') 'COTIZADO',--[Cotizado],
			prd.Observations 'OBSERVACION',--[Observacion],
			cg.Code 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion],
			cg.Name 'GRUPO ATENCION',--[GrupoAtencion],
			c.ContractNumber 'NRO CONTRATO',--[NroContrato],
			prd.InitialDate 'FECHA INICIAL',--[FechaInicial],
			prd.EndDate 'FECHA FINAL',--[FechaFinal]
		    cast(prd.InitialDate as date) 'FECHA BUSQUEDA',
		    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
		FROM Inventory.InventoryProduct ip
		LEFT JOIN Inventory.ProductRateDetail prd ON ip.Id = prd.ProductId
		LEFT JOIN Inventory.ProductRate pr ON prd.ProductRateId = pr.id
		INNER JOIN Contract.CareGroup cg ON pr.Id = cg.ProductRateId AND cg.status = 1
		INNER JOIN Contract.Contract c ON cg.ContractId = c.Id
		WHERE ip.producttypeid <> 1
--END
--GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un dataset consolidado de tarifas de productos no medicamentos asociadas a grupos de atención y contratos vigentes, para alimentar un cubo de verificación de tarifas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMProductRateVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe asociación entre tarifa de producto (ProductRate) y grupo de atención (CareGroup) con status=1; El grupo de atención está vinculado a un contrato existente', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMProductRateVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan productos cuyo tipo no sea 1 (no medicamentos); Solo se incluyen grupos de atención con status=1 (activos); Cada fila pertenece a un contrato existente (INNER JOIN con Contract); La fecha de última actualización se calcula con GETDATE() convertido a zona horaria ''Pakistan Standard Time''; El identificador de compañía corresponde al nombre de la base de datos actual truncado a 9 caracteres; FECHA BUSQUEDA siempre es la parte fecha de InitialDate del detalle de tarifa', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMProductRateVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tarifa de producto; Producto de inventario; Grupo de atención; Contrato; Cobertura de producto; Producto contratado; Producto cotizado; Valor de venta; Vigencia de tarifa', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMProductRateVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMProductRateVerification: Devuelve registros únicamente cuando InventoryProduct.producttypeid <> 1 (excluye medicamentos)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMProductRateVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ip.producttypeid <> 1 → Incluye el producto en el resultado (productos distintos de medicamentos) else Excluye el producto del resultado; si ISNULL(prd.Id,0) > 0 → Marca CUBIERTO = ''Si'' else Marca CUBIERTO = ''No''; si ISNULL(prd.Contracted,0) = 1 → Marca CONTRATADO = ''Si'' else Marca CONTRATADO = ''No''; si ISNULL(prd.Quoted,0) = 1 → Marca COTIZADO = ''Si'' else Marca COTIZADO = ''No''; si cg.status = 1 → Solo se consideran grupos de atención activos en el join', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMProductRateVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ProductRateDetail; Inventory.ProductRate; Contract.CareGroup; Contract.Contract', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMProductRateVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMProductRateVerification';
GO
