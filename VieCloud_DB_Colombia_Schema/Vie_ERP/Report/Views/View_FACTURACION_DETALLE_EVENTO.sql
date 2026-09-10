


--CREATE PROCEDURE [Billing].[SP_FACTURACION_DETALLE_EVENTO]
--	@FechaInicio Datetime ,
--	@FechaFin Datetime 
--AS

--declare @FechaInicio Datetime='2024-04-22'
--declare @FechaFin Datetime ='2024-04-23'

CREATE view [Report].[View_FACTURACION_DETALLE_EVENTO] as 

WITH
CTE_FACTURACIÓN AS 
(
SELECT
F.ID AS FID,
F.InvoicedUser,
F.AdmissionNumber,
F.PatientCode,
F.ThirdPartyId,
F.CareGroupId,
F.HealthAdministratorId,
F.InvoiceCategoryId,
F.OutputDiagnosis,
F.DOCUMENTTYPE,
F.[STATUS],
F.InvoiceNumber,
F.InvoiceDate,
F.InvoiceExpirationDate,
F.TotalInvoice,
F.ThirdPartySalesValue,
F.ThirdPartyDiscountValue,
F.TotalPatientSalesPrice,
F.PatientDiscount,
F.InvoicedDate,
F.AnnulmentDate,
F.initialdate,
F.outputdate,
DF.ServiceOrderDetailId,
DF.InvoicedQuantity,
DF.TotalSalesPrice,
DF.GrandTotalSalesPrice,
SOD.PerformsFunctionalUnitId,
SOD.IPSServiceId,
SOD.ProductId,
SOD.PerformsHealthProfessionalCode,
SOD.Id,
SOD.IncludeServiceOrderDetailId,
SOD.ServiceOrderId,
SOD.CUPSEntityId,
SOD.CUPSEntityContractDescriptionId,
SOD.CostCenterId,
SOD.PRESENTATION,
SOD.RateManualSalePrice,
SOD.ISPACKAGE,
SOD.PACKAGING,
SOD.SettlementType,
SOD.AuthorizationNumber,
sod.servicedate,
SOD.CostValue
FROM
Billing.Invoice F 
INNER JOIN Billing.InvoiceDetail AS DF ON DF.InvoiceId = F.Id
INNER JOIN Billing.ServiceOrderDetail AS SOD ON SOD.Id = DF.ServiceOrderDetailId
WHERE DocumentType <> '5' AND YEAR(InvoiceDate)='2024' 
)

	SELECT 
		CASE WHEN ING.CODCENATE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN '0' ELSE ING.CODCENATE END AS [CodCentroAtencion],
		CASE 
		WHEN CEN.NOMCENATE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'DESCONOCIDO' ELSE CEN.NOMCENATE END AS [CentroAtencion],
		CASE 
			F.STATUS WHEN '1' THEN 'FACTURADO' WHEN '2' THEN 'ANULADO' END AS [EstadoFactura],
		CASE F.DOCUMENTTYPE
			WHEN '1' THEN 'FACTURA EAPB CON CONTRATO'
			WHEN '2' THEN 'FACTURA EAPB SIN CONTRATO'
			WHEN '3' THEN 'FACTURA PARTICULAR'
			WHEN '4' THEN 'FACTURA CAPITA'
			WHEN '5' THEN 'CONTROL CAPITACION'
			WHEN '6' THEN 'FACTURA BASICA'
			WHEN '7' THEN 'FACTURA VENTA PRODUCTOS' END AS [TipoFactura],
		LTRIM(ING.UFUCODIGO) + ' - ' + UF.UFUDESCRI AS [UnidadFuncionalIngreso],
		CASE ING.TIPOINGRE 
			WHEN 1 THEN 'AMBULATORIO' WHEN 2 THEN 'HOSPITALARIO' ELSE 'DESCONOCIDO' END AS [TipoIngreso],
		CASE ING.IINGREPOR
			WHEN 1 THEN 'URGENCIA'
			WHEN 2 THEN 'CONSULTA EXTERNA'
			WHEN 3 THEN 'NACIDO HOSPITAL'
			WHEN 4 THEN 'REMITIDO'
			WHEN 5 THEN 'HOSPITALIZACION - URGENCIAS'
			ELSE 'DESCONOCIDO' END AS [CausaIngreso],
		ING.IFECHAING AS [FechaIngreso],
		T.Nit AS [NIT],
		EA.Code + ' - ' + EA.Name AS [EntidadAdministradora],
		CASE
			WHEN GA.NAME IS NULL THEN 'NO APLICA'
			ELSE GA.NAME END AS [GrupoAtencion],
		CD.ContractNumber AS [NroContrato],
		CASE EA.EntityType
			WHEN 1 THEN 'EPS CONTRIBUTIVO'
			WHEN 2 THEN 'EPS SUBSIDIADO'
			WHEN 3 THEN 'ET VINCULADO MUNICIPIO'
			WHEN 4 THEN 'ET VINCULADOS DAPARTAMENTO'
			WHEN 5 THEN 'ARL RIESGO LABORALES'
			WHEN 6 THEN 'MP MEDICINA PREPAGADA'
			WHEN 7 THEN 'IPS PRIVADA'
			WHEN 8 THEN 'IPS PUBLICA'
			WHEN 9 THEN 'REGIMEN ESPECIAL'
			WHEN 10 THEN 'ACCIDENTE DE TRANSITO'
			WHEN 11 THEN 'FOSYGA'
			WHEN 12 THEN 'OTROS' END AS [TipoRegimenFacturacion],
		RTRIM(tdoc.sigla) AS [TipoIdentificacion],
		F.PatientCode AS [NroIdentificacion],
		P.IPNOMCOMP AS [NombrePaciente],
		F.InvoiceNumber AS [NroFactura],
		F.AdmissionNumber AS [NroIngreso],
		CAST(F.InvoiceDate AS DATETIME) AS [FechaFacturaControl],
		F.InvoiceExpirationDate AS [FechaVencimientoControl],
		F.TotalInvoice AS [ValTotalFacturaControl],
		F.ThirdPartySalesValue AS [ValTotalEntidadControl],
		F.ThirdPartyDiscountValue AS [ValTotalDescuento],
		F.TotalPatientSalesPrice AS [ValTotalCuotaRecuperacion],
		F.PatientDiscount AS [ValDescuentoCuotaRecuperacion],
		C.Name AS [Categoria],
		ISNULL(GF.Name, GF2.Name) AS [GrupoFacturacion],
		CASE PT.Class
			WHEN '2' THEN 'MEDICAMENTOS'
			WHEN '3' THEN 'INSUMOS' 
			ELSE 'SERVICIOS' END AS [TipoServicio],
		ISNULL(CG.Code + '-' + CG.Name, PG.Code + '-' + PG.NAME) [Grupo],
		ISNULL(CSG.CODE + '-' + CSG.Name, PSG.Code + '-' + PSG.NAME) [Subgrupo],
		COALESCE(CUPS.Code, SERVICIOSIPS.CODE, PR.CODE) [CodigoCUP],
		CASE 
			WHEN CDD.Name IS NOT NULL THEN CDD.Name
			ELSE COALESCE(CUPS.Description, SERVICIOSIPS.NAME, PR.NAME) END [DescripcionServicio],
		CASE  
			WHEN (CUPS.ServiceType IS NULL) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =1) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =2) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =3) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =4 AND SERVICIOSIPSQ.Code IS NULL) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =5 AND SERVICIOSIPSQ.Code IS NULL) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =6) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =7) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =8) THEN F.InvoicedQuantity
			WHEN (CUPS.ServiceType =9) THEN F.InvoicedQuantity
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, F.ID ORDER BY F.STATUS, F.InvoiceNumber) = 1 AND CUPS.ServiceType = 4 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 1
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, F.ID ORDER BY F.STATUS, F.InvoiceNumber) > 1 AND CUPS.ServiceType = 4 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 0
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, F.ID ORDER BY F.STATUS, F.InvoiceNumber) > 1 AND CUPS.ServiceType = 5 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 0
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, F.ID ORDER BY F.STATUS, F.InvoiceNumber) = 1 AND CUPS.ServiceType = 5 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 1 END [CantidadCUP],
		CASE
			WHEN PR.CODE IS NULL THEN SERVICIOSIPS.CODE
			ELSE PR.CODE END AS [Codigo],
		CASE
			WHEN PR.NAME IS NULL THEN SERVICIOSIPS.NAME
			ELSE PR.NAME END AS [Descripcion],
		PR.CodeAlternative [CodigoAlternoProducto],
		PR.CodeCUM [CUMProducto],
		CASE F.PRESENTATION
			WHEN '1' THEN 'NO QUIRÚRGICO'
			WHEN '2' THEN 'QUIRÚRGICO'
			WHEN '3' THEN 'PAQUETE' END AS [PresentacionServicio],
		ISNULL(SERVICIOSIPSQ.Code, '00000') AS [Subcodigo],
		ISNULL(SERVICIOSIPSQ.Name, 'NO APLICA') AS [Subnombre],
		F.InvoicedQuantity AS [CantidadOrdenServicio],
		IIF(SERVICIOSIPSQ.Code IS NULL, F.TotalSalesPrice, DQ.TOTALSALESPRICE) AS [PrecioUnitarioVenta],
		IIF(SERVICIOSIPSQ.Code IS NULL, F.GrandTotalSalesPrice, DQ.TOTALSALESPRICE) AS [PrecioVentaTotal],
		ISNULL(DQ.TOTALSALESPRICE, F.RateManualSalePrice) AS [TarifaServicioUnitario],
		ISNULL((DQ.INVOICEDQUANTITY)*(DQ.TOTALSALESPRICE),(F.InvoicedQuantity * F.RateManualSalePrice)) AS [TotalTarifaServicio],
		CASE F.ISPACKAGE
			WHEN 0 THEN 'NO'
			ELSE 'SI' END AS [Paquete],
		CASE F.PACKAGING
			WHEN 0 THEN 'NO'
			ELSE 'SI' END AS [ItemPaquete],
		CASE F.SettlementType
			WHEN 3 THEN 'SI (No se cobra nada)'
			ELSE 'NO (Se cobra)' END [ServiciosIncluidos],
		CUPS2.Code AS [CUPAgrupador],
		CUPS2.Description AS [DescripcionAgrupador],
		SOD2.InvoicedQuantity [CantidadAgrupador],
		SOD2.SubTotalSalesPrice [ValorUnitarioAgrupador],
		SOD2.GrandTotalSalesPrice [TotalAgrupador],
		F.AuthorizationNumber AS [NroAutorizacion],
		BO.Name AS [CentroAtencionServicio],
		LTRIM(FU.Code) + ' - ' + FU.Name AS [UnidadFuncionalServicio],
		OS.OrderDate AS [FechaOrden],
		F.servicedate AS [FechaPrestacionServicio],
		CASE
			WHEN DQ.PERFORMSHEALTHPROFESSIONALCODE IS NULL THEN ISNULL(F.PERFORMSHEALTHPROFESSIONALCODE, '000')
			ELSE DQ.PERFORMSHEALTHPROFESSIONALCODE END AS [CodigoMedico],
		CASE
			WHEN RTRIM(MEDQX.NOMMEDICO) IS NULL THEN ISNULL(MED.NOMMEDICO, 'NO APLICA')
			ELSE RTRIM(MEDQX.NOMMEDICO) END AS [NombreMedico],
		F.OutputDiagnosis AS [CIE10],
		DIAG.NOMDIAGNO AS [DescripcionCIE10],
		CASE
			WHEN ESPMED.DESESPECI IS NULL THEN ESPQX.DESESPECI
			ELSE ESPMED.DESESPECI END AS [Especialidad],
		BB.UBINOMBRE AS [Ubicacion],
		EE.MUNNOMBRE AS [Municipio],
		F.InvoicedUser + ' - ' + PER.Fullname AS [Usuario],
		RTRIM(srvp.identification) + ' - ' + RTRIM(srvp.fullname) [UsuarioServicio],
		YEAR(F.InvoicedDate) AS [AñoFactura],
		F.AnnulmentDate [FechaAnulacion],
		CASE MONTH(F.INVOICEDDATE)
			WHEN 1 THEN 'ENERO'
			WHEN 2 THEN 'FEBRERO'
			WHEN 3 THEN 'MARZO'
			WHEN 4 THEN 'ABRIL'
			WHEN 5 THEN 'MAYO'
			WHEN 6 THEN 'JUNIO'
			WHEN 7 THEN 'JULIO'
			WHEN 8 THEN 'AGOSTO'
			WHEN 9 THEN 'SEPTIEMBRE'
			WHEN 10 THEN 'OCTUBRE'
			WHEN 11 THEN 'NOVIEMBRE'
			WHEN 12 THEN 'DICIEMBRE'
			ELSE 'DESCONOCIDO' END AS [NombreMesFactura],
		MONTH(F.InvoicedDate) AS [MesFactura],
		DAY(F.InvoicedDate) AS [DiaFactura],
		COST.Code [CodCentroCosto],
		COST.Name [CentroCosto],
		OU.UnitName [CiudadOrdenamiento],
		CASE F.STATUS
			WHEN '1' THEN CAST(F.InvoiceDate AS DATETIME)
			WHEN '2' THEN CAST(F.AnnulmentDate AS DATETIME) END AS [FechaBusqueda],
		IIF (ING.TIPOINGRE = 1
			AND CUPS.ServiceType IN (1, 2, 3, 8)
			AND SALIDA.FECALTPAC IS NULL, DATEADD(MINUTE, 10, ING.IFECHAING), isnull(SALIDA.FECALTPAC, ING.IFECHAING)) AS [FechaAltaMedica],
		CASE
			WHEN (GA.CODE) IS NULL THEN 'NO APLICA'
			ELSE (GA.CODE) END AS [CodGrupoAtencion],
		F.CostValue AS [CostoProducto],
		CASE ESTADIO
			WHEN 0 THEN 'estadio clínico (ec) 0 (tumor in situ)'
			WHEN 1 THEN 'ec I o 1'
			WHEN 2 THEN 'ec IA o 1A'
			WHEN 3 THEN 'ec IA1'
			WHEN 4 THEN 'ec IA2'
			WHEN 5 THEN 'ec IB o 1b'
			WHEN 6 THEN 'ec IB1'
			WHEN 7 THEN 'ec IB2'
			WHEN 8 THEN 'ec IC o 1c'
			WHEN 9 THEN 'ec IS o 1s'
			WHEN 10 THEN 'ec II o 2'
			WHEN 11 THEN 'ec IIA o 2a'
			WHEN 12 THEN 'ec IIA1'
			WHEN 13 THEN 'ec IIA2'
			WHEN 14 THEN 'ec IIB o 2b'
			WHEN 15 THEN 'ec IIC o 2c'
			WHEN 16 THEN 'ec III o 3'
			WHEN 17 THEN 'ec IIIA o 3a'
			WHEN 18 THEN 'ec IIIB o 3b'
			WHEN 19 THEN 'ec IIIC o 3c'
			WHEN 20 THEN 'ec IV o 4'
			WHEN 21 THEN 'ec IVA o 4a'
			WHEN 22 THEN 'ec IVB o 4b'
			WHEN 23 THEN 'ec IVC o 4c'
			WHEN 24 THEN 'ec 4S (para neuroblastoma)'
			WHEN 25 THEN 'ec  V o 5'
			WHEN 26 THEN 'ec Estadio IAB'
			WHEN 55 THEN 'Persona con aseguramiento (régimen subsidiado o contributivo y que no son PPNA) que recibió servicios de salud por parte del ente territorialdurante el periodo de reporte'
			WHEN 93 THEN 'Sin información de estadificación en historia clínica'
			WHEN 98 THEN 'No Aplica (Es cáncer de piel basocelular, es cáncer hematológico o es cáncer en SNC, excepto neuroblastoma)'
			WHEN 99 THEN 'Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos'
			ELSE '' END [EstadioClinico],
		F.CUPSEntityContractDescriptionId [IDDescripcionRelacionada],
		F.ID [IDOrdenServicio],
		F.FID [IDDetalleFactura],
		f.initialdate AS [FechaInicialRangoFactura],
		f.outputdate AS [FechaEgresoCorte],
		CAST(F.InvoiceDate AS DATE) AS [FECHA FACTURA]
	FROM CTE_FACTURACIÓN AS F 
	--INNER JOIN Billing.InvoiceDetail AS DF ON DF.InvoiceId = F.Id
	--INNER JOIN Billing.ServiceOrderDetail AS SOD  ON SOD.Id = FD.ServiceOrderDetailId
	INNER JOIN Payroll.FunctionalUnit AS FU ON FU.Id = F.PerformsFunctionalUnitId
	INNER JOIN Payroll.BranchOffice AS BO  ON FU.BranchOfficeId =BO.Id
	INNER JOIN [security].[User] AS U ON U.UserCode = F.InvoicedUser
	INNER JOIN [security].Person AS PER ON PER.Id = U.IdPerson
	INNER JOIN dbo.ADINGRESO AS ING  ON ING.NUMINGRES = F.AdmissionNumber
	INNER JOIN dbo.INPACIENT AS P  ON P.IPCODPACI = F.PatientCode
	LEFT JOIN dbo.adtipoidentifica AS tdoc ON p.iptipodoc = tdoc.codigo
	INNER JOIN Common.ThirdParty AS T  ON T.Id = F.ThirdPartyId
	INNER JOIN Contract.CareGroup AS GA  ON GA.Id = F.CareGroupId
	INNER JOIN Contract.HealthAdministrator AS EA  ON EA.Id = F.HealthAdministratorId
	INNER JOIN Billing.InvoiceCategories AS CAT  ON CAT.Id = F.InvoiceCategoryId
	LEFT JOIN Contract.Contract AS CC  ON GA.ContractId =CC.Id
	LEFT JOIN Contract.ContractDetail AS CD  ON CC.Id = CD.ContractId AND cd.validrecord = 1
	LEFT JOIN Contract.IPSService AS SERVICIOSIPS  ON SERVICIOSIPS.Id = F.IPSServiceId
	LEFT JOIN dbo.ADCENATEN AS CEN  ON CEN.CODCENATE = ING.CODCENATE
	LEFT JOIN Inventory.InventoryProduct AS PR  ON PR.Id = F.ProductId
	LEFT JOIN Inventory.ProductType AS PT ON PR.ProductTypeId = PT.Id
	LEFT JOIN Inventory.ProductGroup AS PG  ON PG.ID =PR.ProductGroupId
	LEFT JOIN Inventory.ProductSubGroup AS PSG  ON PSG.ID =PR.ProductSubGroupId
	LEFT JOIN Billing.InvoiceCategories AS C  ON C.Id = F.InvoiceCategoryId
	LEFT JOIN dbo.INUNIFUNC AS UF  ON UF.UFUCODIGO = ING.UFUCODIGO
	LEFT JOIN dbo.HCREGEGRE AS EH  ON EH.NUMINGRES = F.AdmissionNumber AND EH.IPCODPACI = F.PatientCode
	LEFT JOIN dbo.INPROFSAL AS MED  ON MED.CODPROSAL = F.PerformsHealthProfessionalCode
	LEFT JOIN dbo.INESPECIA AS ESPMED  ON ESPMED.CODESPECI = MED.CODESPEC1
	LEFT JOIN dbo.INDIAGNOS AS DIAG  ON DIAG.CODDIAGNO = F.OutputDiagnosis
	LEFT JOIN dbo.HCREGEGRE AS SALIDA  ON SALIDA.NUMINGRES = F.AdmissionNumber
	LEFT JOIN Billing.ServiceOrderDetailSurgical AS DQ  ON F.ServiceOrderDetailId = F.Id AND DQ.OnlyMedicalFees = '0'
	LEFT JOIN dbo.INPROFSAL AS MEDQX  ON MEDQX.CODPROSAL = DQ.PerformsHealthProfessionalCode
	LEFT JOIN dbo.INESPECIA AS ESPQX  ON ESPQX.CODESPECI = MEDQX.CODESPEC1
	LEFT JOIN Contract.IPSService AS SERVICIOSIPSQ  ON SERVICIOSIPSQ.Id = DQ.IPSServiceId
	LEFT JOIN Billing.ServiceOrder AS OS  ON OS.Id = F.ServiceOrderId
	LEFT JOIN [security].[user] AS srvu ON os.creationuser = srvu.usercode
	LEFT JOIN [security].person AS srvp ON  srvu.idperson = srvp.id
	LEFT JOIN Contract.CUPSEntity AS CUPS  ON CUPS.Id = F.CUPSEntityId
	LEFT JOIN Contract.CupsSubgroup AS CSG  ON CSG.Id =CUPS .CUPSSubGroupId
	LEFT JOIN Contract.CupsGroup AS CG  ON CG.Id =CSG.CupsGroupId
	LEFT JOIN Billing.BillingGroup AS GF  ON GF.Id = CUPS.BillingGroupId
	LEFT JOIN Billing.BillingGroup AS GF2  ON GF2.Id = PR.BillingGroupId
	LEFT JOIN dbo.INUBICACI AS BB  ON BB.AUUBICACI = P.AUUBICACI
	LEFT JOIN dbo.INMUNICIP AS EE  ON EE.DEPMUNCOD = BB.DEPMUNCOD
	LEFT JOIN Billing.ServiceOrderDetail AS SOD2  ON SOD2.Id=F.IncludeServiceOrderDetailId
	LEFT JOIN Contract.CUPSEntity AS CUPS2 ON CUPS2.Id = SOD2.CUPSEntityId
	LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD  ON CECD.ID=F.CUPSEntityContractDescriptionId
	LEFT JOIN Contract.ContractDescriptions AS CDD  ON CECD.ContractDescriptionId =CDD.Id
	LEFT JOIN Payroll.CostCenter AS COST ON COST.Id =F.CostCenterId
	LEFT JOIN Common.OperatingUnit AS OU ON OU.Id =OS.OperatingUnitId
	LEFT JOIN (
		SELECT TOP 1 EST.NUMEFOLIO, EST.IPCODPACI, ESTADIO FROM dbo.INDIAGNOH AS EST  
		INNER JOIN (
		SELECT MIN(NUMEFOLIO) NUMEFOLIO, IPCODPACI FROM dbo.INDIAGNOH WHERE ESTADIO IS NOT NULL AND ESTADIO NOT IN (93, 98, 99) GROUP BY IPCODPACI
		) B ON EST.NUMEFOLIO=B.NUMEFOLIO AND EST.IPCODPACI=B.IPCODPACI --Consulta de Primer Estadio 20201123 
		WHERE ESTADIO IS NOT NULL
	) EST ON ING.IPCODPACI=EST.IPCODPACI
	--WHERE CASE F.STATUS WHEN '1' THEN CAST(F.InvoiceDate AS DATE) WHEN '2' THEN CAST(F.AnnulmentDate AS DATE) END BETWEEN @FechaInicio AND @FechaFin

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el detalle de facturación del año en curso enriquecido con datos de paciente, ingreso, contrato, servicios CUPS, productos, profesionales, diagnósticos y estadio clínico para reporte analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_FACTURACION_DETALLE_EVENTO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen facturas en Billing.Invoice con detalle en Billing.InvoiceDetail y orden de servicio en Billing.ServiceOrderDetail.; El año de la fecha de factura debe ser 2024.; El tipo de documento de la factura no debe ser ''5'' (Control Capitación).; El ingreso (AdmissionNumber) y el paciente (PatientCode) deben existir en ADINGRESO e INPACIENT respectivamente.; El usuario facturador debe existir en security.User vinculado a una Person.; Tercero, grupo de atención, administrador de salud y categoría de factura deben existir (joins INNER).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_FACTURACION_DETALLE_EVENTO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan facturas del año 2024 distintas de Control Capitación (DocumentType=''5'').; Para servicios quirúrgicos con subcódigo IPS (ServiceType 4 o 5), la cantidad se contabiliza una sola vez por combinación factura/CUPS/orden, evitando duplicación.; El estadio clínico reportado corresponde al primer folio (MIN(NUMEFOLIO)) por paciente con ESTADIO informado y distinto de 93, 98 o 99.; Los códigos y descripciones de servicio priorizan CUPS, luego IPSService, y finalmente el producto de inventario.; El grupo y subgrupo se toman del CUPS si existe; si no, del producto.; Los descuentos quirúrgicos solo aplican cuando OnlyMedicalFees=''0''.; Las facturas anuladas usan AnnulmentDate como fecha de búsqueda; las facturadas usan InvoiceDate.; Si no hay médico quirúrgico (DQ), usa el profesional de la orden; si tampoco existe, asigna código ''000'' y nombre ''NO APLICA''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_FACTURACION_DETALLE_EVENTO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve únicamente facturas con YEAR(InvoiceDate)=2024 y DocumentType <> ''5''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_FACTURACION_DETALLE_EVENTO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.STATUS = ''1'' → Marca factura como ''FACTURADO'' y usa InvoiceDate como FechaBusqueda. else Si STATUS = ''2'' marca ''ANULADO'' y usa AnnulmentDate como FechaBusqueda.; si DOCUMENTTYPE = ''6'' (Factura Básica) y no hay centro de atención asociado al ingreso → Asigna CodCentroAtencion=''0'' y CentroAtencion=''DESCONOCIDO''. else Toma código y nombre desde ADCENATEN.; si CUPS.ServiceType IN (4,5) y existe SERVICIOSIPSQ.Code (servicio quirúrgico con subcódigo IPS) → Aplica deduplicación: la primera fila por (CUPS.Code, STATUS, InvoiceNumber, AdmissionNumber, PatientCode, OrderDate, ID) recibe Cantidad=1; las siguientes Cantidad=0. else Para los demás ServiceType, conserva F.InvoicedQuantity como cantidad.; si SERVICIOSIPSQ.Code IS NULL (no es subservicio quirúrgico) → Toma precios y tarifas de la línea de factura (TotalSalesPrice, GrandTotalSalesPrice, RateManualSalePrice). else Toma precios y tarifa de ServiceOrderDetailSurgical (DQ.TOTALSALESPRICE).; si Tipo de ingreso = 1 (Ambulatorio), CUPS.ServiceType IN (1,2,3,8) y SALIDA.FECALTPAC IS NULL → Calcula FechaAltaMedica = IFECHAING + 10 minutos. else Usa FECALTPAC si existe, en su defecto IFECHAING.; si PT.Class = ''2'' → Clasifica TipoServicio=''MEDICAMENTOS''. else Si Class=''3'' es ''INSUMOS''; cualquier otro valor es ''SERVICIOS''.; si F.SettlementType = 3 → Marca ServiciosIncluidos=''SI (No se cobra nada)''. else Marca ''NO (Se cobra)''.; si GA.NAME IS NULL → Asigna GrupoAtencion=''NO APLICA'' y CodGrupoAtencion=''NO APLICA''. else Toma nombre y código del grupo de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_FACTURACION_DETALLE_EVENTO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailSurgical; Billing.ServiceOrder; Billing.InvoiceCategories; Billing.BillingGroup; Payroll.FunctionalUnit; Payroll.BranchOffice; Payroll.CostCenter; security.User; security.Person; dbo.ADINGRESO; dbo.INPACIENT; dbo.adtipoidentifica; Common.ThirdParty; Common.OperatingUnit; Contract.CareGroup; Contract.HealthAdministrator; Contract.Contract; Contract.ContractDetail; Contract.IPSService; Contract.CUPSEntity; Contract.CupsSubgroup; Contract.CupsGroup; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Inventory.InventoryProduct; Inventory.ProductType; Inventory.ProductGroup (+10 adicionales)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_FACTURACION_DETALLE_EVENTO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'View_FACTURACION_DETALLE_EVENTO';
GO
