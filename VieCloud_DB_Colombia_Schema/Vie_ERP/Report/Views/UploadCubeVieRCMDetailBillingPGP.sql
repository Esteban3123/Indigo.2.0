


--CREATE PROCEDURE [Billing].[SP_FACTURACION_DETALLE_PGP]
--declare 	@FechaInicio Datetime ='2024-05-01';
--declare 	@FechaFin Datetime ='2024-05-05';
--AS

CREATE view [Report].[UploadCubeVieRCMDetailBillingPGP] AS

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,CIU.name 'CIUDAD',
		CASE 
			WHEN ING.CODCENATE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN '0' ELSE ING.CODCENATE END AS 'CODIGO CENTRO ATENCION',--[CodCentroAtencion],
		CASE 
		WHEN CEN.NOMCENATE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'DESCONOCIDO' ELSE CEN.NOMCENATE END AS 'CENTRO ATENCION',--[CentroAtencion],
		CASE 
			F.STATUS WHEN '1' THEN 'FACTURADO' WHEN '2' THEN 'ANULADO' END AS 'ESTADO FACTURA',--[EstadoFactura],
		CASE F.DOCUMENTTYPE
			WHEN '1' THEN 'FACTURA EAPB CON CONTRATO'
			WHEN '2' THEN 'FACTURA EAPB SIN CONTRATO'
			WHEN '3' THEN 'FACTURA PARTICULAR'
			WHEN '4' THEN 'FACTURA CAPITA'
			WHEN '5' THEN 'CONTROL CAPITACION'
			WHEN '6' THEN 'FACTURA BASICA'
			WHEN '7' THEN 'FACTURA VENTA PRODUCTOS' END AS 'TIPO FACTURA',--[TipoFactura],
		LTRIM(ING.UFUCODIGO) + ' - ' + UF.UFUDESCRI AS 'UNIDAD FUNCIONAL INGRESO',--[UnidadFuncionalIngreso],
		CASE ING.TIPOINGRE 
			WHEN 1 THEN 'AMBULATORIO' WHEN 2 THEN 'HOSPITALARIO' ELSE 'DESCONOCIDO' END AS 'TIPO INGRESO',--[TipoIngreso],
		CASE ING.IINGREPOR
			WHEN 1 THEN 'URGENCIA'
			WHEN 2 THEN 'CONSULTA EXTERNA'
			WHEN 3 THEN 'NACIDO HOSPITAL'
			WHEN 4 THEN 'REMITIDO'
			WHEN 5 THEN 'HOSPITALIZACION - URGENCIAS'
			ELSE 'DESCONOCIDO' END AS 'CAUSA INGRESO',--[CausaIngreso],
		ING.IFECHAING AS 'FECHA INGRESO',--[FechaIngreso],
		T.Nit AS 'NIT',-- [NIT],
		EA.Code + ' - ' + EA.Name AS 'ENTIDAD ADMINISTRADORA',--[EntidadAdministradora],
		CASE
			WHEN GA.NAME IS NULL THEN 'NO APLICA'
			ELSE GA.NAME END AS 'GRUPO ANTENCION',--[GrupoAtencion],
		CD.ContractNumber AS 'NRO CONTRATO',--[NroContrato],
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
			WHEN 12 THEN 'OTROS' END AS 'TIPO REGIMEN FACTURACION',--[TipoRegimenFacturacion],
		RTRIM(tdoc.sigla) AS 'TIPO IDENTIFICACION',--[TipoIdentificacion],
		F.PatientCode AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		P.IPNOMCOMP AS 'NOMBRE PACIENTE',--[NombrePaciente],
		F.InvoiceNumber AS 'NRO FACTURA',--[NroFactura],
		F.AdmissionNumber AS 'NRO INGRESO',--[NroIngreso],
		CAST(F.InvoiceDate AS DATETIME) AS 'FECHA FACTURA/CONTROL',-- [FechaFacturaControl],
		F.InvoiceExpirationDate AS 'FECHA VENCIMIENTO/CONTROL',--[FechaVencimientoControl],
		F.TotalInvoice AS 'VALOR TOTAL FACTURA/CONTROL',-- [ValTotalFacturaControl],
		F.ThirdPartySalesValue AS 'VALOR TOTAL ENTIDAD/CONTROL',--[ValTotalEntidadControl],
		F.ThirdPartyDiscountValue AS 'VALOR TOTAL DESCUENTO',--[ValTotalDescuento],
		F.TotalPatientSalesPrice AS 'VALOR TOTAL CUOTA RECUPERACION',--[ValTotalCuotaRecuperacion],
		F.PatientDiscount AS 'VALOR DESCUENTO CUOTA RECUPERACION',--[ValDescuentoCuotaRecuperacion],
		C.Name AS 'CATEGORIA',--[Categoria],
		ISNULL(GF.Name, GF2.Name) AS 'GRUPO FACTURACION',--[GrupoFacturacion],
		CASE PT.Class
			WHEN '2' THEN 'MEDICAMENTOS'
			WHEN '3' THEN 'INSUMOS' 
			ELSE 'SERVICIOS' END AS 'TIPO SERVICIO',--[TipoServicio],
		ISNULL(CG.Code + '-' + CG.Name, PG.Code + '-' + PG.NAME) 'GRUPO',--[Grupo],
		ISNULL(CSG.CODE + '-' + CSG.Name, PSG.Code + '-' + PSG.NAME) 'SUBGRUPO',--[Subgrupo],
		COALESCE(CUPS.Code, SERVICIOSIPS.CODE, PR.CODE) 'CODIGO CUPS',--[CodigoCUP],
		CASE 
			WHEN CDD.Name IS NOT NULL THEN CDD.Name
			ELSE COALESCE(CUPS.Description, SERVICIOSIPS.NAME, PR.NAME) END 'DESCRIPCION SERVICIO',--[DescripcionServicio],
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
			WHEN ROW_NUMBER() OVER(PARTITION BY CUPS.Code, F.STATUS, F.InvoiceNumber, F.AdmissionNumber, F.PatientCode, OS.OrderDate, SOD.ID ORDER BY F.STATUS, F.InvoiceNumber) = 1 AND CUPS.ServiceType = 5 AND SERVICIOSIPSQ.Code IS NOT NULL THEN 1 END 'CANTIDAD CUPS',--[CantidadCUP],
		CASE
			WHEN PR.CODE IS NULL THEN SERVICIOSIPS.CODE
			ELSE PR.CODE END AS 'CODIGO',--[Codigo],
		CASE
			WHEN PR.NAME IS NULL THEN SERVICIOSIPS.NAME
			ELSE PR.NAME END AS 'DESCRIPCION',--[Descripcion],
		PR.CodeAlternative 'CODIGO ALTERNO PRODUCTO',--[CodigoAlternoProducto],
		PR.CodeCUM 'CUM/PRODUCTO',--[CUMProducto],
		CASE SOD.PRESENTATION
			WHEN '1' THEN 'NO QUIRÚRGICO'
			WHEN '2' THEN 'QUIRÚRGICO'
			WHEN '3' THEN 'PAQUETE' END AS 'PRESENTACION SERVICIO',--[PresentacionServicio],
		ISNULL(SERVICIOSIPSQ.Code, '00000') AS 'SUBCODIGO',--[Subcodigo],
		ISNULL(SERVICIOSIPSQ.Name, 'NO APLICA') AS 'SUBNOMBRE',--[Subnombre],
		DF.InvoicedQuantity AS 'CANTIDAD ORDEN SERVICIO',--[CantidadOrdenServicio],
		IIF(SERVICIOSIPSQ.Code IS NULL, DF.TotalSalesPrice, DQ.TOTALSALESPRICE) AS 'PRECIO UNITARIO VENTA',--[PrecioUnitarioVenta],
		IIF(SERVICIOSIPSQ.Code IS NULL, DF.GrandTotalSalesPrice, DQ.TOTALSALESPRICE) AS 'PRECIO VENTA TOTAL',--[PrecioVentaTotal],
		ISNULL(DQ.TOTALSALESPRICE, SOD.RateManualSalePrice) AS 'TARIFA SERVICIO UNITARIO',--[TarifaServicioUnitario],
		ISNULL((DQ.INVOICEDQUANTITY)*(DQ.TOTALSALESPRICE),(SOD.InvoicedQuantity * SOD.RateManualSalePrice)) AS 'TOTAL TARIFA SERVICIO',--[TotalTarifaServicio],
		CASE SOD.ISPACKAGE
			WHEN 0 THEN 'NO'
			ELSE 'SI' END AS 'PAQUETE',--[Paquete],
		CASE SOD.PACKAGING
			WHEN 0 THEN 'NO'
			ELSE 'SI' END AS 'ITEM PAQUETE',-- [ItemPaquete],
		CASE SOD.SettlementType
			WHEN 3 THEN 'SI (No se cobra nada)'
			ELSE 'NO (Se cobra)' END 'SERVICIO INCLUIDO',--[ServiciosIncluidos],
		CUPS2.Code AS 'CODIGO AGRUPADOR',--[CUPAgrupador],
		CUPS2.Description AS 'DESCRICION AGRUPADOR',--[DescripcionAgrupador],
		SOD2.InvoicedQuantity 'CANTIDAD AGRUPADOR',--[CantidadAgrupador],
		SOD2.SubTotalSalesPrice 'VALOR UNITARIO AGRUPADOR',--[ValorUnitarioAgrupador],
		SOD2.GrandTotalSalesPrice 'TOTAL AGRUPADOR',--[TotalAgrupador],
		SOD.AuthorizationNumber AS 'NRO AUTORIZACIONES',--[NroAutorizacion],
		BO.Name AS 'CENTRO ATENCION SERVICIO',--[CentroAtencionServicio],
		LTRIM(FU.Code) + ' - ' + FU.Name AS 'UNIDAD FUNCIONAL SERVICIO',-- [UnidadFuncionalServicio],
		OS.OrderDate AS 'FECHA ORDEN',--[FechaOrden],
		sod.servicedate AS 'FECHA SERVICIO',--[FechaPrestacionServicio],
		CASE
			WHEN DQ.PERFORMSHEALTHPROFESSIONALCODE IS NULL THEN ISNULL(SOD.PERFORMSHEALTHPROFESSIONALCODE, '000')
			ELSE DQ.PERFORMSHEALTHPROFESSIONALCODE END AS 'CODIGO MEDICO',--[CodigoMedico],
		CASE
			WHEN RTRIM(MEDQX.NOMMEDICO) IS NULL THEN ISNULL(MED.NOMMEDICO, 'NO APLICA')
			ELSE RTRIM(MEDQX.NOMMEDICO) END AS 'NOMBRE MEDICO',--[NombreMedico],
		F.OutputDiagnosis AS 'CIE10',--[CIE10],
		DIAG.NOMDIAGNO AS 'DIAGNOSTICO',--[DescripcionCIE10],
		CASE
			WHEN ESPMED.DESESPECI IS NULL THEN ESPQX.DESESPECI
			ELSE ESPMED.DESESPECI END AS 'ESPECIALIDAD',--[Especialidad],
		BB.UBINOMBRE AS 'UBICACION',--[Ubicacion],
		EE.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
		F.InvoicedUser + ' - ' + SUF.NOMUSUARI AS 'USUARIO',--[Usuario],
		RTRIM(SUO.CODUSUARI) + ' - ' + RTRIM(SUO.NOMUSUARI) 'USUARIO SERVICIO',--[UsuarioServicio],
		YEAR(F.InvoicedDate) AS 'AÑO FACTURA',--[AñoFactura],
		F.AnnulmentDate 'FECHA ANULACION',--[FechaAnulacion],
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
			ELSE 'DESCONOCIDO' END AS 'NOMBRE MES FACTURA',--[NombreMesFactura],
		MONTH(F.InvoicedDate) AS 'MES FACTURA',--[MesFactura],
		DAY(F.InvoicedDate) AS 'DIA FACTURA',--[DiaFactura],
		COST.Code 'CODIGO CENTRO COSTO',--[CodCentroCosto],
		COST.Name 'CENTRO COSTO',--[CentroCosto],
		OU.UnitName 'CIUDAD ORDENAMIENTO',--[CiudadOrdenamiento],
		IIF (ING.TIPOINGRE = 1
			AND CUPS.ServiceType IN (1, 2, 3, 8)
			AND SALIDA.FECALTPAC IS NULL, DATEADD(MINUTE, 10, ING.IFECHAING), isnull(SALIDA.FECALTPAC, ING.IFECHAING)) AS 'FECHA ALTA MEDICA',--[FechaAltaMedica],
		CASE
			WHEN (GA.CODE) IS NULL THEN 'NO APLICA'
			ELSE (GA.CODE) END AS 'COIGO GRUPO ATENCION',--[CodGrupoAtencion],
		SOD.CostValue AS 'COSTO PRODUCTO',--[CostoProducto],
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
			ELSE '' END 'ESTADIO CLINICO',-- [EstadioClinico],
		SOD.CUPSEntityContractDescriptionId [IDDescripcionRelacionada],
		SOD.ID [IDOrdenServicio],
		DF.ID [IDDetalleFactura],
		f.initialdate AS 'FECHA INICIAL RANGO FACTURA',--[FechaInicialRangoFactura],
		f.outputdate AS 'FECHA EGRESO CORTE',--[FechaEgresoCorte]
		F.Observation 'OBSERVACION',
			CASE F.STATUS
			WHEN '1' THEN CAST(F.InvoiceDate AS DATETIME)
			WHEN '2' THEN CAST(F.AnnulmentDate AS DATETIME) END AS 'FECHA BUSQUEDA',
			CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM Billing.Invoice AS F WITH (NOLOCK)
	INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
	INNER JOIN Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = DF.ServiceOrderDetailId
	INNER JOIN Payroll.FunctionalUnit AS FU ON FU.Id = SOD.PerformsFunctionalUnitId
	INNER JOIN Payroll.BranchOffice AS BO WITH (NOLOCK) ON FU.BranchOfficeId =BO.Id
	INNER JOIN common.OperatingUnit  OUc WITH (NOLOCK) ON OUc.ID=F.OperatingUnitid
	INNER JOIN COMMON.CITY AS CIU ON CIU.ID= OUc.idCity
	LEFT JOIN DBO.SEGusuaru SUF ON F.InvoicedUser=SUF.CODUSUARI
		
	INNER JOIN dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES = F.AdmissionNumber
	INNER JOIN dbo.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = F.PatientCode
	LEFT JOIN dbo.adtipoidentifica AS tdoc ON p.iptipodoc = tdoc.codigo
	INNER JOIN Common.ThirdParty AS T WITH (NOLOCK) ON T.Id = F.ThirdPartyId
	INNER JOIN Contract.CareGroup AS GA WITH (NOLOCK) ON GA.Id = F.CareGroupId
	INNER JOIN Contract.HealthAdministrator AS EA WITH (NOLOCK) ON EA.Id = F.HealthAdministratorId
	INNER JOIN Billing.InvoiceCategories AS CAT WITH (NOLOCK) ON CAT.Id = F.InvoiceCategoryId
	LEFT JOIN Contract.Contract AS CC WITH (NOLOCK) ON GA.ContractId =CC.Id
	LEFT JOIN Contract.ContractDetail AS CD WITH (NOLOCK) ON CC.Id = CD.ContractId AND cd.validrecord = 1
	LEFT JOIN Contract.IPSService AS SERVICIOSIPS WITH (NOLOCK) ON SERVICIOSIPS.Id = SOD.IPSServiceId
	LEFT JOIN dbo.ADCENATEN AS CEN WITH (NOLOCK) ON CEN.CODCENATE = ING.CODCENATE
	LEFT JOIN Inventory.InventoryProduct AS PR WITH (NOLOCK) ON PR.Id = SOD.ProductId
	LEFT JOIN Inventory.ProductType AS PT ON PR.ProductTypeId = PT.Id
	LEFT JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.ID =PR.ProductGroupId
	LEFT JOIN Inventory.ProductSubGroup AS PSG WITH (NOLOCK) ON PSG.ID =PR.ProductSubGroupId
	LEFT JOIN Billing.InvoiceCategories AS C WITH (NOLOCK) ON C.Id = F.InvoiceCategoryId
	LEFT JOIN dbo.INUNIFUNC AS UF WITH (NOLOCK) ON UF.UFUCODIGO = ING.UFUCODIGO
	LEFT JOIN dbo.HCREGEGRE AS EH WITH (NOLOCK) ON EH.NUMINGRES = F.AdmissionNumber AND EH.IPCODPACI = F.PatientCode
	LEFT JOIN dbo.INPROFSAL AS MED WITH (NOLOCK) ON MED.CODPROSAL = SOD.PerformsHealthProfessionalCode
	LEFT JOIN dbo.INESPECIA AS ESPMED WITH (NOLOCK) ON ESPMED.CODESPECI = MED.CODESPEC1
	LEFT JOIN dbo.INDIAGNOS AS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = F.OutputDiagnosis
	LEFT JOIN dbo.HCREGEGRE AS SALIDA WITH (NOLOCK) ON SALIDA.NUMINGRES = F.AdmissionNumber
	LEFT JOIN Billing.ServiceOrderDetailSurgical AS DQ WITH (NOLOCK) ON DQ.ServiceOrderDetailId = SOD.Id AND DQ.OnlyMedicalFees = '0'
	LEFT JOIN dbo.INPROFSAL AS MEDQX WITH (NOLOCK) ON MEDQX.CODPROSAL = DQ.PerformsHealthProfessionalCode
	LEFT JOIN dbo.INESPECIA AS ESPQX WITH (NOLOCK) ON ESPQX.CODESPECI = MEDQX.CODESPEC1
	LEFT JOIN Contract.IPSService AS SERVICIOSIPSQ WITH (NOLOCK) ON SERVICIOSIPSQ.Id = DQ.IPSServiceId
	LEFT JOIN Billing.ServiceOrder AS OS WITH (NOLOCK) ON OS.Id = SOD.ServiceOrderId

	LEFT JOIN DBO.SEGusuaru SUO ON os.creationuser=SUO.CODUSUARI
	--LEFT JOIN indigosec.security.[user] AS srvu ON os.creationuser = srvu.usercode
	

	LEFT JOIN Contract.CUPSEntity AS CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
	LEFT JOIN Contract.CupsSubgroup AS CSG WITH (NOLOCK) ON CSG.Id =CUPS .CUPSSubGroupId
	LEFT JOIN Contract.CupsGroup AS CG WITH (NOLOCK) ON CG.Id =CSG.CupsGroupId
	LEFT JOIN Billing.BillingGroup AS GF WITH (NOLOCK) ON GF.Id = CUPS.BillingGroupId
	LEFT JOIN Billing.BillingGroup AS GF2 WITH (NOLOCK) ON GF2.Id = PR.BillingGroupId
	LEFT JOIN dbo.INUBICACI AS BB WITH (NOLOCK) ON BB.AUUBICACI = P.AUUBICACI
	LEFT JOIN dbo.INMUNICIP AS EE WITH (NOLOCK) ON EE.DEPMUNCOD = BB.DEPMUNCOD
	LEFT JOIN Billing .ServiceOrderDetail AS SOD2 WITH (NOLOCK) ON SOD2.Id=SOD.IncludeServiceOrderDetailId
	LEFT JOIN Contract.CUPSEntity AS CUPS2 WITH (NOLOCK) ON CUPS2.Id = SOD2.CUPSEntityId
	LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD WITH (NOLOCK) ON CECD.ID=SOD.CUPSEntityContractDescriptionId
	LEFT JOIN Contract.ContractDescriptions AS CDD WITH (NOLOCK) ON CECD.ContractDescriptionId =CDD.Id
	LEFT JOIN Payroll.CostCenter AS COST ON COST.Id =SOD.CostCenterId
	LEFT JOIN Common.OperatingUnit AS OU ON OU.Id =OS.OperatingUnitId
	LEFT JOIN (
		SELECT TOP 1 EST.NUMEFOLIO, EST.IPCODPACI, ESTADIO FROM dbo.INDIAGNOH AS EST WITH (NOLOCK) 
		INNER JOIN (
		SELECT MIN(NUMEFOLIO) NUMEFOLIO, IPCODPACI FROM dbo.INDIAGNOH WHERE ESTADIO IS NOT NULL AND ESTADIO NOT IN (93, 98, 99) GROUP BY IPCODPACI
		) B ON EST.NUMEFOLIO=B.NUMEFOLIO AND EST.IPCODPACI=B.IPCODPACI --Consulta de Primer Estadio 20201123 
		WHERE ESTADIO IS NOT NULL
	) EST ON ING.IPCODPACI=EST.IPCODPACI
	WHERE F.DocumentType = '5' AND CASE F.STATUS WHEN '1' THEN YEAR(F.InvoiceDate) WHEN '2' THEN YEAR(F.AnnulmentDate) END >=2023

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el detalle de facturación de control de capitación (PGP) con datos del paciente, ingreso, servicio, profesional, contrato y diagnóstico para alimentar un cubo de reporte RCM.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMDetailBillingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice con su detalle en Billing.InvoiceDetail y orden de servicio asociada en Billing.ServiceOrderDetail.; El tipo de documento de la factura debe ser ''5'' (CONTROL CAPITACION).; El año de la factura (InvoiceDate si STATUS=1, o AnnulmentDate si STATUS=2) debe ser >= 2023.; Debe existir el ingreso (ADINGRESO), paciente (INPACIENT), tercero (ThirdParty), grupo de atención (CareGroup), entidad administradora (HealthAdministrator) y categoría de factura.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMDetailBillingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen facturas de tipo CONTROL CAPITACION (DocumentType=''5'').; Solo se exponen registros desde el año 2023 en adelante según el estado de la factura.; El detalle quirúrgico (ServiceOrderDetailSurgical) se considera únicamente cuando OnlyMedicalFees=''0''.; El estadio clínico se determina con el folio mínimo del paciente que tenga ESTADIO no nulo y distinto de 93, 98 y 99.; El detalle de contrato sólo se vincula cuando ContractDetail.validrecord = 1.; La fecha de última actualización (ULT_ACTUAL) se devuelve en zona horaria ''Pakistan Standard Time''.; Para servicios con CUPS.ServiceType 4 o 5 con subcódigo IPS, sólo la primera fila por (CUPS.Code, STATUS, InvoiceNumber, AdmissionNumber, PatientCode, OrderDate, SOD.ID) computa cantidad 1; las restantes 0, evitando doble conteo.; Cuando el grupo de atención no existe se etiqueta ''NO APLICA''.; Cuando el centro de atención del ingreso es nulo y el documento es tipo 6, se reporta como ''DESCONOCIDO''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMDetailBillingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMDetailBillingPGP: Devuelve filas filtradas por DocumentType=''5'' y año de InvoiceDate/AnnulmentDate >= 2023, con datos enriquecidos del paciente, ingreso, factura, servicio, profesional y contrato.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMDetailBillingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.DocumentType = ''5'' → Solo se incluyen facturas tipo ''CONTROL CAPITACION''. else Se excluyen del resultado.; si F.STATUS = ''1'' → Estado ''FACTURADO''; FECHA BUSQUEDA = InvoiceDate y se evalúa YEAR(InvoiceDate) >= 2023. else Si STATUS=''2'' estado ''ANULADO''; FECHA BUSQUEDA = AnnulmentDate y se evalúa YEAR(AnnulmentDate) >= 2023.; si ING.CODCENATE IS NULL AND F.DOCUMENTTYPE LIKE ''6'' → Se asigna código ''0'' y nombre ''DESCONOCIDO'' al centro de atención. else Se toma el código y nombre real del centro de atención.; si CUPS.ServiceType IN (4,5) AND SERVICIOSIPSQ.Code IS NOT NULL → La cantidad CUPS se controla por ROW_NUMBER particionado: la primera fila recibe 1 y las siguientes 0 para evitar duplicar cantidades en servicios quirúrgicos con subcódigo IPS. else Para los demás ServiceType se reporta DF.InvoicedQuantity tal cual.; si SERVICIOSIPSQ.Code IS NULL → PRECIO UNITARIO/TOTAL VENTA toma DF.TotalSalesPrice / DF.GrandTotalSalesPrice. else Toma DQ.TOTALSALESPRICE del detalle quirúrgico.; si ING.TIPOINGRE = 1 AND CUPS.ServiceType IN (1,2,3,8) AND SALIDA.FECALTPAC IS NULL → FECHA ALTA MEDICA se calcula como IFECHAING + 10 minutos (alta médica inferida para ambulatorios sin egreso). else Se usa SALIDA.FECALTPAC o, en su defecto, ING.IFECHAING.; si SOD.SettlementType = 3 → Servicio marcado como ''SI (No se cobra nada)'' (servicio incluido). else ''NO (Se cobra)''.; si CDD.Name IS NOT NULL → La descripción del servicio se toma de la descripción contractual (ContractDescriptions). else Se usa COALESCE(CUPS.Description, SERVICIOSIPS.Name, PR.Name).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMDetailBillingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMDetailBillingPGP';
GO
