--CREATE procedure [Billing].[FacturacionReporteCotizaciones]

--DECLARE	@ini_date AS DATE='2024-06-01';
--DECLARE	@end_date AS DATE='2024-06-30';
create view [Report].[UploadCubeVieRCMContractReportQuotation] AS

SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		Q.Code [DOCUMENTO COTIZACION], 
		DocumentDate [FECHA COTIZACION], 
		case Q. QuotationType 
			when 1 then 'INTRAHOSPITLARIO' 
			WHEN 2 THEN 'AMBULATORIO' END [TIPO COTIZACION], 
		CASE Q.Status 
			WHEN 1 THEN 'REGISTRADO' 
			WHEN 2 THEN 'CONFIRMADO' ELSE 'OTRO' END [ESTADO],
		Q.CreationUser + ' - ' + PER.Fullname [USUARIO], 
		Q.Description [DESCRIPCION],
		AdmissionNumber [INGRESO], 
		TPP.Nit [IDENTIFICACION],
		TPP.Name [PACIENTE], 
		HAP.Name [ENTIDAD],
		CGP.Code [CODIGO GRUPO ATENCION], 
		CGP.Name [GRUPO DE ATENCION],
		TPP.Nit [NIT],
		TPP.Name [TERCERO],
		'PRODUCTOS' [TIPO],
		'' [CUPS],
		'' [DESCRIPCION CUPS],
		'' AS [CODIGO DESCRIPCION],
		'' AS [DESCRIPCION P],
		PRO.Code [CODIGO SERVICIO/PRODUCTO],
		PRO.Name [SERVICIO/PRODUCTO],
		'' [CODIGO HIJO], 
		'' [SERVICIO HIJO],
		Quantity [CANTIDAD],
		SalePrice [VALOR TARIFA],
		QPDD.ServiceDate [FECHA SERVICIO],
		QPDD.AuthorizationNumber [NRO AUTORIZACION],
		FUP.Code + ' - ' + FUP.Name [UNIDAD FUNCIONAL SERVICIO],
		RTRIM(MEDP.CODPROSAL) + ' - ' + MEDP.NOMMEDICO [PROFESIONAL],
		QPDD.TotalSalesPrice [PRECIO UNITARIO SERVICIO], 
		QPDD.GrandTotalSalesPrice [PRECIO TOTAL SERVICIO],
		QPDD.GrandTotalSalesPrice [VALOR TOTAL ORDEN], 
		WH.Code + ' - ' + WH.Name [ALMACEN],
	    CAST(q.documentdate AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM Billing.Quotation Q WITH (NOLOCK)
	JOIN Billing.QuotationPharmaceuticalDispensingDetail QPDD WITH (NOLOCK) ON Q.Id =QPDD.QuotationId 
	LEFT JOIN Contract .HealthAdministrator AS HAP WITH (NOLOCK) ON HAP.Id =QPDD.HealthAdministratorId 
	LEFT JOIN Contract .CareGroup CGP WITH (NOLOCK) ON QPDD.CareGroupId =CGP.Id 
	LEFT JOIN Common.ThirdParty AS TPP WITH (NOLOCK) ON TPP.Id =Q.ThirdPartyId
	LEFT JOIN Inventory .InventoryProduct PRO (NOLOCK) ON PRO.Id =QPDD.ProductId
	LEFT JOIN Inventory .Warehouse WH (NOLOCK) ON WH.Id =QPDD .WarehouseId 
	LEFT JOIN Payroll .FunctionalUnit FUP WITH (NOLOCK) ON FUP.CostCenterId =QPDD.FunctionalUnitId 
	LEFT JOIN dbo.INPROFSAL AS MEDP WITH (NOLOCK) ON MEDP.CODPROSAL = QPDD.OrderedHealthProfessionalCode
	LEFT JOIN Security.[UserINT] AS USU WITH (NOLOCK) ON USU.UserCode =Q.CreationUser 
	LEFT JOIN Security.PersonINT AS PER WITH (NOLOCK) ON PER.Id =USU.IdPerson 
	WHERE CAST(q.documentdate AS DATE)>='2023-01-01'
	--CAST(q.documentdate AS DATE) BETWEEN @ini_date AND @end_date


UNION ALL

	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		Q.Code [NroCotizacion], 
		DocumentDate [Fecha],
		case Q. QuotationType 
			when 1 then 'INTRAHOSPITLARIO' 
			WHEN 2 THEN 'AMBULATORIO' END [Tipo], 
		CASE Q.Status 
			WHEN 1 THEN 'REGISTRADO'
			WHEN 2 THEN 'CONFIRMADO' ELSE 'OTRO' END [Estado],
		Q.CreationUser + ' - ' + PER.Fullname [UsuarioGenero], 
		Q.Description [Descripcion],
		AdmissionNumber [NroIngreso], 
		TP.Nit [NroIdentificacion],
		TP.Name [NombrePaciente],  
		HA.Name [Entidad ],
		CG.Code [CodGrupoAtencion], 
		CG.Name [GrupoAtencion],
		TP2.Nit [NIT],
		TP2.Name [Tercero],
		'SERVICIOS' [TipoServicio],
		CUPS.Code [CUPS],
		cdes.code AS [CodigoDescripcionRelacionada],
		cdes.name AS [DescripcionRelacionada],
		CUPS.Description [DescripcionCUPS], 
		SERVICIOSIPS.Code [CodServicio],
		SERVICIOSIPS.Name [ServicioProducto],
		QX.Code [CodHijo], 
		QX.Name [ServicioHijo],
		QSOD.InvoicedQuantity [Cantidad], 
		iif (QSODS.Id is null,QSOD.RateManualSalePrice,QSODS.TotalSalesPrice) [ValTarifa],
		QSOD.ServiceDate [FechaServicio], 
		QSOD.AuthorizationNumber [NroAutorizacion], 
		FU.Code + ' - ' + FU.Name [UFServicio], 
		RTRIM(MED.CODPROSAL) + ' - ' + MED.NOMMEDICO [Profesional],
		iif(QSODS.Id is null,qsod.SubTotalSalesPrice,QSODS.TotalSalesPrice) [ValUnitarioServicio],  
		iif(QSODS.Id is null,QSOD.GrandTotalSalesPrice,QSODS.TotalSalesPrice) [ValTotalServicio],
		QSOD.GrandTotalSalesPrice [ValTotalOrden],
		'' [Almacen],
		CAST(q.documentdate AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM Billing.Quotation Q WITH (NOLOCK)
	join Billing.QuotationServiceOrderDetail QSOD WITH (NOLOCK) ON QSOD .QuotationId =Q.Id 
	JOIN Common.OperatingUnit OU  WITH (NOLOCK) ON Q.OperatingUnitId =OU.Id 
	LEFT JOIN Security.[UserINT] AS USU WITH (NOLOCK) ON USU.UserCode =Q.CreationUser 
	LEFT JOIN Security.PersonINT AS PER WITH (NOLOCK) ON PER.Id =USU.IdPerson 
	LEFT JOIN Common.ThirdParty AS TP WITH (NOLOCK) ON TP.Id =Q.ThirdPartyId  
	LEFT JOIN Contract .HealthAdministrator AS HA WITH (NOLOCK) ON HA.Id =QSOD.HealthAdministratorId 
	LEFT JOIN Contract .CareGroup CG WITH (NOLOCK) ON QSOD.CareGroupId =CG.Id 
	LEFT JOIN Common.ThirdParty AS TP2 WITH (NOLOCK) ON TP2.Id =QSOD.ThirdPartyId 
	LEFT JOIN Contract.CUPSEntity AS CUPS WITH (NOLOCK) ON CUPS.Id = QSOD.CUPSEntityId
	LEFT JOIN Contract.IPSService AS SERVICIOSIPS WITH (NOLOCK) ON SERVICIOSIPS.Id = QSOD.IPSServiceId
	LEFT JOIN Payroll .FunctionalUnit FU WITH (NOLOCK) ON FU.CostCenterId =QSOD.PerformsFunctionalUnitId  
	LEFT JOIN dbo.INPROFSAL AS MED WITH (NOLOCK) ON MED.CODPROSAL = QSOD.PerformsHealthProfessionalCode
	LEFT JOIN Payroll .CostCenter AS CC WITH (NOLOCK) ON CC.Id =QSOD.CostCenterId  
	LEFT JOIN Billing .QuotationServiceOrderDetailSurgical AS QSODS WITH (NOLOCK) ON QSODS.QuotationServiceOrderDetailId =QSOD.Id 
	LEFT JOIN Contract.IPSService AS QX WITH (NOLOCK) ON QX.Id = QSODS.IPSServiceId AND QSODS.QuotationServiceOrderDetailId =QSOD.Id

	LEFT JOIN contract.cupsentitycontractdescriptions AS cup_des WITH (NOLOCK) ON qsod.cupsentitycontractdescriptionid = cup_des.id
	LEFT JOIN Contract.contractdescriptions AS cdes WITH (NOLOCK) ON cup_des.contractdescriptionid = cdes.Id
	WHERE CAST(q.documentdate AS DATE)>='2022-01-01'
	--CAST(q.documentdate AS DATE) BETWEEN @ini_date AND @end_date

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para cubo de reportería que unifica el detalle de cotizaciones de facturación, tanto de productos farmacéuticos como de servicios/procedimientos, con datos de paciente, entidad, profesional y valores tarifados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cotizaciones deben tener fecha de documento (DocumentDate) >= 2023-01-01 para el bloque de productos farmacéuticos y >= 2022-01-01 para el bloque de servicios.; Debe existir relación entre la cotización (Billing.Quotation) y al menos un detalle (QuotationPharmaceuticalDispensingDetail o QuotationServiceOrderDetail) para que el registro aparezca.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de compañía siempre se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.; El timestamp de última actualización siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El bloque de PRODUCTOS nunca expone CUPS, descripción CUPS ni servicio hijo (campos vacíos); el bloque de SERVICIOS nunca expone almacén (campo vacío).; En el bloque de productos, NIT/Tercero se replican desde el tercero de la cotización; en el bloque de servicios, el tercero corresponde al ThirdPartyId del detalle de servicio (puede ser distinto del paciente).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cotización; Paciente; Ingreso/Admisión; Entidad administradora de salud; Grupo de atención; Tercero; Autorización; CUPS; Servicio IPS; Profesional de la salud; Unidad funcional; Almacén; Dispensación farmacéutica; Orden de servicio; Procedimiento quirúrgico; Tarifa de venta', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMContractReportQuotation: Devuelve UNION ALL de dos conjuntos: (1) detalle de dispensación farmacéutica marcado como TIPO=''PRODUCTOS'' filtrado por documentdate>=''2023-01-01''; (2) detalle de orden de servicio marcado como TIPO=''SERVICIOS'' filtrado por documentdate>=''2022-01-01''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Q.QuotationType = 1 → Clasifica la cotización como ''INTRAHOSPITALARIO'' else Si QuotationType = 2 se clasifica como ''AMBULATORIO''; otro valor queda NULL; si Q.Status = 1 → Estado se reporta como ''REGISTRADO'' else Si Status = 2 se reporta ''CONFIRMADO''; cualquier otro valor se reporta como ''OTRO''; si QSODS.Id IS NULL (no existe detalle quirúrgico asociado) → El valor tarifa, unitario y total del servicio se toman de QSOD (RateManualSalePrice, SubTotalSalesPrice, GrandTotalSalesPrice) else Cuando existe detalle quirúrgico (QSODS), los valores se toman de QSODS.TotalSalesPrice', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Quotation; Billing.QuotationPharmaceuticalDispensingDetail; Contract.HealthAdministrator; Contract.CareGroup; Common.ThirdParty; Inventory.InventoryProduct; Inventory.Warehouse; Payroll.FunctionalUnit; dbo.INPROFSAL; Security.UserINT; Security.PersonINT; Billing.QuotationServiceOrderDetail; Common.OperatingUnit; Contract.CUPSEntity; Contract.IPSService; Payroll.CostCenter; Billing.QuotationServiceOrderDetailSurgical; Contract.cupsentitycontractdescriptions; Contract.contractdescriptions', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractReportQuotation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMContractReportQuotation';
GO
