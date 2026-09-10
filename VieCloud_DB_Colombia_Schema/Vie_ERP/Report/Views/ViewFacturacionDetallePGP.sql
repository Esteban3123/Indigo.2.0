

CREATE VIEW [Report].[ViewFacturacionDetallePGP]

AS

	SELECT 
		CASE 
			WHEN ING.CODCENATE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN '0' ELSE ING.CODCENATE END AS [CodCentroAtencion],
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
			WHEN (CUPS.ServiceType IS NULL) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =1) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =2) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =3) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =4 AND SERVICIOSIPSQ.Code IS NULL) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =5 AND SERVICIOSIPSQ.Code IS NULL) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =6) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =7) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =8) THEN DF.InvoicedQuantity
			WHEN (CUPS.ServiceType =9) THEN DF.InvoicedQuantity
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, SOD.ID ORDER BY F.STATUS, F.InvoiceNumber) = 1 AND CUPS.ServiceType = 4 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 1
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, SOD.ID ORDER BY F.STATUS, F.InvoiceNumber) > 1 AND CUPS.ServiceType = 4 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 0
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, SOD.ID ORDER BY F.STATUS, F.InvoiceNumber) > 1 AND CUPS.ServiceType = 5 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 0
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, SOD.ID ORDER BY F.STATUS, F.InvoiceNumber) = 1 AND CUPS.ServiceType = 5 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 1 END [CantidadCUP],
		CASE
			WHEN PR.CODE IS NULL THEN SERVICIOSIPS.CODE
			ELSE PR.CODE END AS [Codigo],
		CASE
			WHEN PR.NAME IS NULL THEN SERVICIOSIPS.NAME
			ELSE PR.NAME END AS [Descripcion],
		PR.CodeAlternative [CodigoAlternoProducto],
		PR.CodeCUM [CUMProducto],
		CASE SOD.PRESENTATION
			WHEN '1' THEN 'NO QUIRÚRGICO'
			WHEN '2' THEN 'QUIRÚRGICO'
			WHEN '3' THEN 'PAQUETE' END AS [PresentacionServicio],
		ISNULL(SERVICIOSIPSQ.Code, '00000') AS [Subcodigo],
		ISNULL(SERVICIOSIPSQ.Name, 'NO APLICA') AS [Subnombre],
		DF.InvoicedQuantity AS [CantidadOrdenServicio],
		IIF(SERVICIOSIPSQ.Code IS NULL, DF.TotalSalesPrice, DQ.TOTALSALESPRICE) AS [PrecioUnitarioVenta],
		IIF(SERVICIOSIPSQ.Code IS NULL, DF.GrandTotalSalesPrice, DQ.TOTALSALESPRICE) AS [PrecioVentaTotal],
		ISNULL(DQ.TOTALSALESPRICE, SOD.RateManualSalePrice) AS [TarifaServicioUnitario],
		ISNULL((DQ.INVOICEDQUANTITY)*(DQ.TOTALSALESPRICE),(SOD.InvoicedQuantity * SOD.RateManualSalePrice)) AS [TotalTarifaServicio],
		CASE SOD.ISPACKAGE
			WHEN 0 THEN 'NO'
			ELSE 'SI' END AS [Paquete],
		CASE SOD.PACKAGING
			WHEN 0 THEN 'NO'
			ELSE 'SI' END AS [ItemPaquete],
		CASE SOD.SettlementType
			WHEN 3 THEN 'SI (No se cobra nada)'
			ELSE 'NO (Se cobra)' END [ServiciosIncluidos],
		CUPS2.Code AS [CUPAgrupador],
		CUPS2.Description AS [DescripcionAgrupador],
		SOD2.InvoicedQuantity [CantidadAgrupador],
		SOD2.SubTotalSalesPrice [ValorUnitarioAgrupador],
		SOD2.GrandTotalSalesPrice [TotalAgrupador],
		SOD.AuthorizationNumber AS [NroAutorizacion],
		BO.Name AS [CentroAtencionServicio],
		LTRIM(FU.Code) + ' - ' + FU.Name AS [UnidadFuncionalServicio],
		OS.OrderDate AS [FechaOrden],
		sod.servicedate AS [FechaPrestacionServicio],
		CASE
			WHEN DQ.PERFORMSHEALTHPROFESSIONALCODE IS NULL THEN ISNULL(SOD.PERFORMSHEALTHPROFESSIONALCODE, '000')
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
		SOD.CostValue AS [CostoProducto],
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
		SOD.CUPSEntityContractDescriptionId [IDDescripcionRelacionada],
		SOD.ID [IDOrdenServicio],
		DF.ID [IDDetalleFactura],
		f.initialdate AS [FechaInicialRangoFactura],
		f.outputdate AS [FechaEgresoCorte]
	FROM Billing.Invoice AS F 
	INNER JOIN Billing.InvoiceDetail AS DF  ON DF.InvoiceId = F.Id
	INNER JOIN Billing.ServiceOrderDetail AS SOD  ON SOD.Id = DF.ServiceOrderDetailId
	INNER JOIN Payroll.FunctionalUnit AS FU ON FU.Id = SOD.PerformsFunctionalUnitId
	INNER JOIN Payroll.BranchOffice AS BO  ON FU.BranchOfficeId =BO.Id
	INNER JOIN [Security].[User] AS U  ON U.UserCode = F.InvoicedUser
	INNER JOIN [Security].Person AS PER  ON PER.Id = U.IdPerson
	INNER JOIN dbo.ADINGRESO AS ING  ON ING.NUMINGRES = F.AdmissionNumber
	INNER JOIN dbo.INPACIENT AS P  ON P.IPCODPACI = F.PatientCode
	LEFT JOIN dbo.adtipoidentifica AS tdoc ON p.iptipodoc = tdoc.codigo
	INNER JOIN Common.ThirdParty AS T  ON T.Id = F.ThirdPartyId
	INNER JOIN Contract.CareGroup AS GA  ON GA.Id = F.CareGroupId
	INNER JOIN Contract.HealthAdministrator AS EA  ON EA.Id = F.HealthAdministratorId
	INNER JOIN Billing.InvoiceCategories AS CAT  ON CAT.Id = F.InvoiceCategoryId
	LEFT JOIN Contract.Contract AS CC  ON GA.ContractId =CC.Id
	LEFT JOIN Contract.ContractDetail AS CD  ON CC.Id = CD.ContractId AND cd.validrecord = 1
	LEFT JOIN Contract.IPSService AS SERVICIOSIPS  ON SERVICIOSIPS.Id = SOD.IPSServiceId
	LEFT JOIN dbo.ADCENATEN AS CEN  ON CEN.CODCENATE = ING.CODCENATE
	LEFT JOIN Inventory.InventoryProduct AS PR  ON PR.Id = SOD.ProductId
	LEFT JOIN Inventory.ProductType AS PT ON PR.ProductTypeId = PT.Id
	LEFT JOIN Inventory.ProductGroup AS PG  ON PG.ID =PR.ProductGroupId
	LEFT JOIN Inventory.ProductSubGroup AS PSG  ON PSG.ID =PR.ProductSubGroupId
	LEFT JOIN Billing.InvoiceCategories AS C  ON C.Id = F.InvoiceCategoryId
	LEFT JOIN dbo.INUNIFUNC AS UF  ON UF.UFUCODIGO = ING.UFUCODIGO
	LEFT JOIN dbo.HCREGEGRE AS EH  ON EH.NUMINGRES = F.AdmissionNumber AND EH.IPCODPACI = F.PatientCode
	LEFT JOIN dbo.INPROFSAL AS MED  ON MED.CODPROSAL = SOD.PerformsHealthProfessionalCode
	LEFT JOIN dbo.INESPECIA AS ESPMED  ON ESPMED.CODESPECI = MED.CODESPEC1
	LEFT JOIN dbo.INDIAGNOS AS DIAG  ON DIAG.CODDIAGNO = F.OutputDiagnosis
	LEFT JOIN dbo.HCREGEGRE AS SALIDA  ON SALIDA.NUMINGRES= F.AdmissionNumber
	LEFT JOIN Billing.ServiceOrderDetailSurgical AS DQ  ON DQ.ServiceOrderDetailId = SOD.Id AND DQ.OnlyMedicalFees = '0'
	LEFT JOIN dbo.INPROFSAL AS MEDQX  ON MEDQX.CODPROSAL = DQ.PerformsHealthProfessionalCode
	LEFT JOIN dbo.INESPECIA AS ESPQX  ON ESPQX.CODESPECI = MEDQX.CODESPEC1
	LEFT JOIN Contract.IPSService AS SERVICIOSIPSQ  ON SERVICIOSIPSQ.Id = DQ.IPSServiceId
	LEFT JOIN Billing.ServiceOrder AS OS  ON OS.Id = SOD.ServiceOrderId

	LEFT JOIN [security].[user] AS srvu ON os.creationuser = srvu.usercode
	LEFT JOIN [security].person AS srvp ON  srvu.idperson = srvp.id

	LEFT JOIN Contract.CUPSEntity AS CUPS  ON CUPS.Id = SOD.CUPSEntityId
	LEFT JOIN Contract.CupsSubgroup AS CSG  ON CSG.Id =CUPS .CUPSSubGroupId
	LEFT JOIN Contract.CupsGroup AS CG  ON CG.Id =CSG.CupsGroupId
	LEFT JOIN Billing.BillingGroup AS GF  ON GF.Id = CUPS.BillingGroupId
	LEFT JOIN Billing.BillingGroup AS GF2  ON GF2.Id = PR.BillingGroupId
	LEFT JOIN dbo.INUBICACI AS BB  ON BB.AUUBICACI = P.AUUBICACI
	LEFT JOIN dbo.INMUNICIP AS EE  ON EE.DEPMUNCOD = BB.DEPMUNCOD
	LEFT JOIN Billing .ServiceOrderDetail AS SOD2  ON SOD2.Id=SOD.IncludeServiceOrderDetailId
	LEFT JOIN Contract.CUPSEntity AS CUPS2  ON CUPS2.Id = SOD2.CUPSEntityId
	LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD  ON CECD.ID=SOD.CUPSEntityContractDescriptionId
	LEFT JOIN Contract.ContractDescriptions AS CDD  ON CECD.ContractDescriptionId =CDD.Id
	LEFT JOIN Payroll.CostCenter AS COST ON COST.Id =SOD.CostCenterId
	LEFT JOIN Common.OperatingUnit AS OU ON OU.Id =OS.OperatingUnitId
	LEFT JOIN (
		SELECT TOP 1 EST.NUMEFOLIO, EST.IPCODPACI, ESTADIO FROM dbo.INDIAGNOH AS EST  
		INNER JOIN (
		SELECT MIN(NUMEFOLIO) NUMEFOLIO, IPCODPACI FROM dbo.INDIAGNOH WHERE ESTADIO IS NOT NULL AND ESTADIO NOT IN (93, 98, 99) GROUP BY IPCODPACI
		) B ON EST.NUMEFOLIO=B.NUMEFOLIO AND EST.IPCODPACI=B.IPCODPACI --Consulta de Primer Estadio 20201123 
		WHERE ESTADIO IS NOT NULL
	) EST ON ING.IPCODPACI=EST.IPCODPACI 
	WHERE F.DocumentType = '5' --AND CASE F.STATUS WHEN '1' THEN CAST(F.InvoiceDate AS DATE) WHEN '2' THEN CAST(F.AnnulmentDate AS DATE) END BETWEEN @FechaInicio AND @FechaFin
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el detalle completo de facturación por paciente y servicio prestado, orientada a consumo analítico (reportes PGP). Combina encabezados de facturas (tipo, estado, valores totales, entidad administradora, contrato, régimen) con el detalle de cada línea facturada (servicios CUPS, medicamentos, insumos, tarifas, cantidades, descuentos, cuota de recuperación), enriquecido con datos clínicos del ingreso, egreso, diagnóstico CIE-10, estadificación oncológica, profesional ejecutante y especialidad. Incluye dimensiones temporales (año, mes, día de facturación) y organizacionales (centro de atención, unidad funcional, centro de costo, sede) para facilitar el cruce y análisis de ingresos facturados por periodo, entidad pagadora y tipo de servicio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionDetallePGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionDetallePGP';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida el detalle de facturación bajo modalidad de Pago Global Prospectivo (PGP) — DocumentType=5 (Control Capitación) — uniendo factura, orden de servicio, paciente, contrato, CUPS, profesional y estadio clínico oncológico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionDetallePGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Solo considera facturas con Billing.Invoice.DocumentType = ''5'' (CONTROL CAPITACION).; Para traer detalle de contrato (ContractDetail) se requiere ValidRecord = 1.; Para incluir el quirúrgico/profesional QX (ServiceOrderDetailSurgical) se exige OnlyMedicalFees = ''0''.; Para el cálculo de estadio clínico se excluyen valores 93, 98 y 99 y se toma el primer folio (MIN(NUMEFOLIO)) por paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionDetallePGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las filas corresponden a facturas tipo PGP/Capitación (DocumentType=''5'').; El CodigoCUP siempre se resuelve por COALESCE en el orden CUPS.Code → SERVICIOSIPS.CODE → PR.CODE.; GrupoFacturacion se prioriza desde el CUPS (BillingGroupId) y solo si es nulo se toma del producto.; El nombre del médico prioriza el quirúrgico (DQ) sobre el profesional general del SOD; análogamente la especialidad.; El estadio clínico válido excluye los códigos 93, 98 y 99 al determinar el primer folio por paciente.; La cantidad facturada de servicios IPS quirúrgicos (ServiceType 4 o 5) se contabiliza una sola vez por combinación de factura/orden para evitar duplicidad.; Los meses de la factura se traducen al nombre en español según MONTH(InvoicedDate).; Subcodigo y Subnombre nunca son nulos: por defecto ''00000'' y ''NO APLICA''.; GrupoAtencion y CodGrupoAtencion devuelven ''NO APLICA'' cuando el contrato no define grupo de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionDetallePGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewFacturacionDetallePGP: Devuelve un row por cada InvoiceDetail x ServiceOrderDetail de facturas con DocumentType=''5'', enriquecido con datos clínicos, contractuales, profesionales y de costos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionDetallePGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.DocumentType = ''5'' → Filtra exclusivamente facturas tipo CONTROL CAPITACION (PGP).; si ING.CODCENATE IS NULL AND F.DOCUMENTTYPE LIKE ''6'' → Asigna CodCentroAtencion=''0'' y CentroAtencion=''DESCONOCIDO'' (regla heredada para FACTURA BASICA, aunque el WHERE limita a ''5'').; si F.STATUS = ''1'' → EstadoFactura=''FACTURADO'' y FechaBusqueda = InvoiceDate. else Si STATUS=''2'' EstadoFactura=''ANULADO'' y FechaBusqueda = AnnulmentDate.; si CUPS.ServiceType IN (4,5) AND SERVICIOSIPSQ.Code IS NOT NULL → La CantidadCUP se deduplica usando ROW_NUMBER() sobre (CUPS.Code, STATUS, InvoiceNumber, AdmissionNumber, PatientCode, OrderDate, SOD.ID): la primera fila trae 1 y las demás 0. else Para los demás ServiceType (1,2,3,6,7,8,9 o NULL) la CantidadCUP = DF.InvoicedQuantity.; si SERVICIOSIPSQ.Code IS NULL → Precios unitario y total se toman de InvoiceDetail (DF.TotalSalesPrice, DF.GrandTotalSalesPrice). else Se toman del registro quirúrgico DQ.TOTALSALESPRICE.; si ING.TIPOINGRE = 1 AND CUPS.ServiceType IN (1,2,3,8) AND SALIDA.FECALTPAC IS NULL → FechaAltaMedica = IFECHAING + 10 minutos (alta presunta para ambulatorios sin egreso registrado). else FechaAltaMedica = COALESCE(SALIDA.FECALTPAC, ING.IFECHAING).; si CDD.Name IS NOT NULL → DescripcionServicio toma la descripción contractual (ContractDescriptions). else Usa COALESCE(CUPS.Description, SERVICIOSIPS.NAME, PR.NAME).; si PT.Class = ''2'' → TipoServicio=''MEDICAMENTOS''.; si PT.Class = ''3'' → TipoServicio=''INSUMOS''. else TipoServicio=''SERVICIOS'' para cualquier otro Class.; si SOD.SettlementType = 3 → ServiciosIncluidos=''SI (No se cobra nada)''. else ServiciosIncluidos=''NO (Se cobra)''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionDetallePGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewFacturacionDetallePGP';
GO
