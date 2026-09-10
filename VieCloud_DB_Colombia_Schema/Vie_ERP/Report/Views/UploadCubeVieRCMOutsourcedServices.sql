
--CREATE PROCEDURE [EHR].[SP_SERVICIOS_TERCERIZADOS]
--DECLARE	@DateStart DATETIME='2024-05-01';
--DECLARE	@DateEnd DATETIME ='2024-05-31';
--AS 
CREATE view [Report].[UploadCubeVieRCMOutsourcedServices] as

	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		aos.Code 'NRO AUTORIZACION',--[NroAutorizacion],
		aos.DocumentDate 'FECHA DOCUMENTO',--[FechaDocumento],
		RTRIM(PER.Fullname) AS 'USUARIO REGISTRA AUTORIZACION',--[UsuarioRegistroAutorizacion],
		CASE AOS.Status
			WHEN 1 THEN 'REGISTRADA' 
			WHEN 2 THEN 'CONFIRMADA'
			WHEN 3 THEN 'ANULADA' END AS 'ESTADO',--[Estado],
		case aos.Type when 1 then 'Hospitalaria' else 'Ambulatoria' END AS 'TIPO AUTORIZACION',--[TipoAutorizacion],
		aos.AdmissionNumber 'NRO INGRESO',--[NroIngreso],
		iif(fac.AdmissionNumber is null,'INGRESO NO FACTURADO','INGRESO FACTURADO')  AS 'FACTURADO',--[Facturado],
		FAC.InvoiceNumber AS 'NRO FACTURA',--[NroFactura],
		TP.Nit AS 'NIT',--[NIT], 
		TP.Name AS 'TERCERO',--[Tercero],
		EST.TECNOLOGIA AS 'TECNOLOGIA',--[Tecnologia],
		IIF(EST.CUPSEntityId IS NOT NULL, 'ESTA EN FACTURA','NO ESTA EN FACTURA') AS 'SERVICIO FACTURADO',--[ServicioFacturado],
		ce.Code AS 'CODIGO CUPS',--[CodigoCUPS],
		ce.Description AS 'CUPS',--[CUPS],
		IPS.Code AS 'CODIGO SERVICIO',--[CodigoServicio],
		IPS.Name 'SERVICIO',--[Servicio],
		cdt.code AS 'CODIGO DESCRIPCION',--[CodigoDescripcion],
		cdt.name AS 'DESCRIPCION',--[Descripcion],
		CASE assod.ServiceType 
			WHEN 1 THEN 'SOAT' 
			WHEN 2 THEN 'OTRO' 
			WHEN 3 THEN 'CUPS' END 'TIPO SERVICIO',--[TipoServicio],
		IPSQX.Code 'SUBCODIGO',--[Subcodigo],
		IPSQX .Name 'SUBNOMBRE',--[Subnombre],
		assod .InvoicedQuantity 'CANTIDAD',--[Cantidad],
		CASE WHEN QX.Id IS NULL THEN assod .SubTotalSalesPrice ELSE QX.TotalSalesPrice END AS 'PRECIO UNITARIO',--[PrecioUnitario],
		CASE WHEN QX.Id IS NULL THEN assod .GrandTotalSalesPrice ELSE QX.TotalSalesPrice END AS 'PRECIO TOTAL',--[PrecioTotal],
		CASE WHEN QX.Id IS NULL THEN '0' ELSE assod .SubTotalSalesPrice END AS 'PRECIO QX',--[PrecioQX],
		ASSOD.AuthorizationNumber AS 'AUTORIZACION',--[Autorizacion],
		thi.nit AS 'NIT ENTIDAD',--[NITEntidad],
		HEA.CODE + ' - ' + HEA.Name AS 'ENTIDAD',--[Entidad],
		CG.Code + ' - ' + CG.Name AS 'GRUPO ATENCION',--[GrupoAtencion],
		FU.Code + ' - ' + fu.Name AS 'UNIDAD FUNCIONAL',--[UnidadFuncional], 
		SAL.NOMMEDICO AS 'PROFESIONAL',--[Profesional],
		EST.[CODIGO GRUPO ATENCION] AS 'CODIGO GRUPO ATENCION FACTURA',--[CodGrupoAtencionFactura],
		EST.[GRUPO DE ATENCION] AS 'GRUPO ATENCION FACTURA',--[GrupoAtencionFactura]
		cast(aos.DocumentDate AS DATE) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
		--INTO INDIGODWH.[Authorization].STG_AUTORIZACION_SERVICIOS_TERCERIZADOS
	from [Authorization].AuthorizationOutsourcedServices  aos
	JOIN Security.[UserINT] AS USU ON USU.UserCode=AOS.CreationUser 
	JOIN Security.PersonINT AS PER ON PER.Id = USU.IdPerson 
	LEFT JOIN Common.ThirdParty AS TP ON aos.ThirdPartyId =TP.Id 
	LEFT JOIN [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail AS assod on assod.AuthorizationOutsourcedServicesId =aos.Id 
	LEFT JOIN [Authorization].AuthorizationOutsourcedServicesServiceOrderDetailSurgical AS QX ON QX.AuthorizationOutsourcedServicesServiceOrderDetailId =assod .Id
	left join Contract.CUPSEntity as ce on ce.Id =assod .CUPSEntityId 
	LEFT JOIN Contract.IPSService AS IPS ON IPS.Id =assod .IPSServiceId 
	left join Payroll.FunctionalUnit as fu on fu.Id =assod .PerformsFunctionalUnitId 
	LEFT JOIN dbo.INPROFSAL AS SAL ON SAL.CODPROSAL =assod .PerformsHealthProfessionalCode 
	LEFT JOIN Contract.IPSService AS IPSQX ON IPSQX.Id =QX.IPSServiceId 
	LEFT JOIN Contract.HealthAdministrator AS HEA ON HEA.Id =assod .HealthAdministratorId 
	LEFT JOIN Contract.CareGroup CG ON CG.Id =assod .CareGroupId 
	LEFT JOIN contract.cupsentitycontractdescriptions AS ccd WITH (NOLOCK) ON assod.cupsentitycontractdescriptionid = ccd.id
	LEFT JOIN contract.contractdescriptions AS cdt WITH (NOLOCK) ON ccd.contractdescriptionid = cdt.id

	LEFT JOIN common.thirdparty AS thi ON hea.thirdpartyid = thi.id
	LEFT JOIN (
			select AdmissionNumber,InvoiceNumber   		
			from Billing .Invoice where Status =1 group by AdmissionNumber,InvoiceNumber  
		) as fac on fac.AdmissionNumber =aos.AdmissionNumber 
	LEFT JOIN (
			SELECT F.AdmissionNumber,F.InvoiceNumber ,CUPS.Code ,CUPS.Description, SOD.CUPSEntityId, CAT.Name AS 'TECNOLOGIA',CG.Code 'CODIGO GRUPO ATENCION',CG.Name 'GRUPO DE ATENCION' 
			FROM Billing.Invoice AS F WITH (NOLOCK)
			INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
			INNER JOIN Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = DF.ServiceOrderDetailId
			INNER JOIN Billing.InvoiceCategories AS CAT ON CAT.Id =F.InvoiceCategoryId 
			INNER JOIN Contract.CareGroup AS CG ON CG.Id =F.CareGroupId 
			LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS DQ WITH (NOLOCK) ON DQ.ServiceOrderDetailId = SOD.Id AND DQ.OnlyMedicalFees = '0'
			LEFT OUTER JOIN Contract.CUPSEntity AS CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
				WHERE F.Status =1 GROUP BY F.AdmissionNumber,F.InvoiceNumber ,CUPS.Code ,CUPS.Description, SOD.CUPSEntityId,CAT.Name,
				CG.Code,CG.Name
		) AS EST ON EST.AdmissionNumber =FAC.AdmissionNumber AND EST.InvoiceNumber =FAC.InvoiceNumber AND EST.CUPSEntityId =assod.CUPSEntityId 
	--WHERE  cast(aos.DocumentDate AS DATE) between @DateStart and @DateEnd --AND AOS.AdmissionNumber IN ('115461')  
	--ORDER BY FAC.InvoiceNumber ASC
	---
	--SELECT * FROM indigo999 .Billing .Invoice WHERE AdmissionNumber IN ('113025')
	--SELECT * FROM indigo999 .Billing .InvoiceDetail

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista la información de autorizaciones de servicios tercerizados y la confronta con la facturación asociada para alimentar un cubo analítico de RCM.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las autorizaciones deben existir en Authorization.AuthorizationOutsourcedServices con su usuario creador presente en Security.UserINT y Security.PersonINT.; Para considerar una factura como vinculada al ingreso, su Status debe ser 1 (activa/vigente).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se cruzan facturas con Status = 1 (activas) tanto para detectar facturación del ingreso como para validar el servicio facturado.; El cruce de servicio facturado exige coincidencia simultánea de AdmissionNumber, InvoiceNumber y CUPSEntityId entre la autorización y la factura.; En el detalle quirúrgico facturado solo se consideran ítems con OnlyMedicalFees = ''0'' (excluye honorarios médicos puros).; La fecha de última actualización (ULT_ACTUA) se entrega convertida a la zona horaria ''Pakistan Standard Time''.; El identificador de compañía se reporta como los primeros 9 caracteres del nombre de la base de datos actual.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios tercerizados (outsourcing); Estado de autorización (Registrada/Confirmada/Anulada); Autorización hospitalaria vs ambulatoria; Ingreso/Admisión; Facturación (Invoice) y número de factura; Tercero / NIT; CUPS; Servicio IPS; Detalle quirúrgico (honorarios, tarifa QX); Tipo de servicio SOAT/CUPS/Otro; Entidad responsable (Health Administrator); Grupo de atención; Unidad funcional; Profesional de la salud; Tecnología (categoría de factura); RCM (Revenue Cycle Management)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMOutsourcedServices: Devuelve un conjunto distinto (SELECT DISTINCT) con autorizaciones tercerizadas enriquecidas con datos de tercero, CUPS, IPS, profesional, entidad, grupo de atención y facturación cuando aplica.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AOS.Status = 1 / 2 / 3 → Etiqueta el estado de la autorización como ''REGISTRADA'', ''CONFIRMADA'' o ''ANULADA'' respectivamente.; si aos.Type = 1 → Clasifica la autorización como ''Hospitalaria''. else Se clasifica como ''Ambulatoria''.; si fac.AdmissionNumber IS NULL → Marca el registro como ''INGRESO NO FACTURADO''. else Marca el registro como ''INGRESO FACTURADO''.; si EST.CUPSEntityId IS NOT NULL → Marca el servicio como ''ESTA EN FACTURA'' (el CUPS autorizado coincide con un CUPS facturado para el mismo ingreso/factura). else Marca el servicio como ''NO ESTA EN FACTURA''.; si assod.ServiceType = 1 / 2 / 3 → Clasifica el tipo de servicio como ''SOAT'', ''OTRO'' o ''CUPS'' respectivamente.; si QX.Id IS NULL (el detalle no tiene componente quirúrgico) → Toma como precio unitario y total los valores SubTotalSalesPrice y GrandTotalSalesPrice del detalle de autorización, y deja ''PRECIO QX'' en 0. else Toma como precio unitario y total el TotalSalesPrice del detalle quirúrgico (QX) y reporta el SubTotalSalesPrice del detalle como ''PRECIO QX''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationOutsourcedServices; Security.UserINT; Security.PersonINT; Common.ThirdParty; Authorization.AuthorizationOutsourcedServicesServiceOrderDetail; Authorization.AuthorizationOutsourcedServicesServiceOrderDetailSurgical; Contract.CUPSEntity; Contract.IPSService; Payroll.FunctionalUnit; dbo.INPROFSAL; Contract.HealthAdministrator; Contract.CareGroup; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.InvoiceCategories; Billing.ServiceOrderDetailSurgical', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMOutsourcedServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMOutsourcedServices';
GO
