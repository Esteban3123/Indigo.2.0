

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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la verificación de servicios CUPS con su grupo, subgrupo, descripción contractual, homologación a servicios IPS y manual tarifario asociado, para alimentar un cubo de reporte RCM.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractServiceVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las entidades CUPS deben tener subgrupo (CUPSSubGroupId) y grupo (CupsGroupId) válidos por ser JOIN obligatorios; El servidor debe soportar la zona horaria ''Pakistan Standard Time'' para el cálculo de ULT_ACTUAL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractServiceVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de compañía se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres; La fecha de búsqueda corresponde al día actual del servidor; el timestamp ULT_ACTUAL se reporta convertido a zona horaria ''Pakistan Standard Time''; Cada fila representa una combinación única de CUPS-grupo-subgrupo-descripción-homólogo-manual (uso de DISTINCT); Los CUPS sin homologación, sin descripción de contrato o sin manual tarifario igualmente aparecen (LEFT JOIN); Las descripciones de contrato con isdelete=1 nunca se incluyen', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractServiceVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Grupo y subgrupo de CUPS; Descripción de contrato; Homologación de servicios; Servicio IPS; Manual tarifario; Recursos UPC (financiación); Servicios quirúrgicos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractServiceVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMContractServiceVerification: Devuelve filas únicas (SELECT DISTINCT) por CUPS con su grupo, subgrupo, descripción de contrato, homólogo IPS y manual tarifario, incluyendo fecha de búsqueda y timestamp en zona horaria Pakistan Standard Time', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractServiceVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ce.Status = 1 → Marca el CUPS como ''Activo'' (en columnas ESTADO CUPS y ESTADO) else Marca el CUPS como ''Inactivo''; si ce.financedresourceUPC = 1 → Etiqueta ''FINANCIADO RECURSOS UPS'' como ''Si'' else Etiqueta como ''No'' cuando es 0; si cecd.isdelete = 0 en el LEFT JOIN con CUPSEntityContractDescriptions → Solo se vinculan descripciones de contrato no eliminadas lógicamente else Se omite la descripción relacionada (queda NULL); si rmd.RateManualId = rm.Id OR rmds.RateManualId = rm.Id → Asocia el manual tarifario tanto desde el detalle estándar como desde el detalle quirúrgico', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractServiceVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CUPSEntity; Contract.CupsSubgroup; Contract.CupsGroup; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Contract.CupsHomologation; Contract.IPSService; Contract.RateManualDetail; Contract.RateManualDetailSurgical; Contract.RateManual', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractServiceVerification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractServiceVerification';
GO
