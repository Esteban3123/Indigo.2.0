

--CREATE PROCEDURE [Contract].[ReporteVerificacionCups] 
	-- Add the parameters for the stored procedure here

--AS
--BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

    -- Insert statements for procedure here

create view [Report].[UploadCubeVieRCMContractServiceVerification] AS

 SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		ce.Code AS 'CODIGO CUPS',--[CodCUPS],
		RTRIM(ce.Description) AS 'DESCRIPCION CUPS',--[DescripcionCUPS],
		CASE ce.status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END AS 'ESTADO CUPS',--[EstadoCUPS],
		CASE ce.financedresourceUPC WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END AS 'FINANCIADO RECURSOS UPS',--[FinanciadoRecursosUPC],
	    cg.Code AS 'CODIGO RUPO',--[CodGrupo],
		RTRIM(cg.Description) AS 'GRUPO',--[Grupo],
        csg.Code AS 'CODIGO SUBGRUPO',--[CodSubgrupo],
		RTRIM(csg.Description) AS 'SUBGRUPO',--[Subgrupo],
		cd.Code AS 'CODIGO DESCRIPCION RELACIONADA',--[CodDescripcionRelacionada],
		RTRIM(cd.Name) AS 'DESCRIPCION RELACIONADA',--[DescripcionRelacionada],
		ips.Name AS 'HOMOLOGO',--[Homologo],
		rm.Name AS 'MANUAL',--[Manual],
		IIF(ce.Status = 1, 'Activo', 'Inactivo') AS 'ESTADO',--[Estado]
	    CAST(GETDATE()  AS DATE) [FECHA BUSQUEDA],
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM Contract.CUPSEntity ce
	JOIN Contract.CupsSubgroup csg ON ce.CUPSSubGroupId = csg.Id
	JOIN Contract.CupsGroup cg ON csg.CupsGroupId = cg.Id
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId AND cecd.isdelete = 0
	LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN Contract.CupsHomologation ch ON ce.Id = ch.CupsEntityId
	LEFT JOIN Contract.IPSService ips ON ch.IPSServiceId = ips.Id
	LEFT JOIN Contract.RateManualDetail rmd ON ips.Id = rmd.IPSServiceId
	LEFT JOIN Contract.RateManualDetailSurgical rmds ON ips.Id = rmds.IPSServiceId
	LEFT JOIN Contract.RateManual rm ON rmd.RateManualId = rm.Id OR rmds.RateManualId = rm.Id
	--ORDER BY 1,6