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