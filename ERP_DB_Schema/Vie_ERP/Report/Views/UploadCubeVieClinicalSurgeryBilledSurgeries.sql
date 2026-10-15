

--CREATE PROCEDURE [Billing].[SP_CIRUGIAS_FACTURADAS]
--DECLARE	@FechaInicio Datetime='2024-06-01';
--DECLARE	@FechaFin Datetime ='2024-06-30';
--AS
CREATE view [Report].[UploadCubeVieClinicalSurgeryBilledSurgeries] AS

	WITH CTE_PACIENTES_CIRUGIAS AS
	(
		SELECT F.* FROM Billing.Invoice AS F
		WHERE F.Status = '1' AND CAST(F.InvoiceDate  AS DATE)>='2023-01-01'
		--CAST(F.InvoiceDate  AS DATE) BETWEEN @FechaInicio AND @FechaFin AND 
	)

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE p.iptipodoc
			WHEN 1 THEN 'CC'
			WHEN 2 THEN 'CE'
			WHEN 3 THEN 'TI'
			WHEN 4 THEN 'RC'
			WHEN 5 THEN 'PA'
			WHEN 6 THEN 'AS'
			WHEN 7 THEN 'MS'
			WHEN 8 THEN 'NU'
			WHEN 9 THEN 'CN'
			WHEN 10 THEN 'CD'
			WHEN 11 THEN 'SC' 
			WHEN 12 THEN 'PE' 
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			WHEN 15 THEN 'SI' END AS 'TIPO DOCUMENTO',--[TipoDocumento],
		F.PatientCode AS 'NRO DOCUMENTO PACIENTE',--[NroDocumentoPaciente], 
		RTRIM(P.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NomPaciente],
		RTRIM(T.Nit) AS 'NIT',--[NIT], 
		EA.Code + ' - ' + EA.Name AS 'ENTIDAD ADMINISTRADORA',--[EntidadAdministradora],  
		CASE  
			WHEN (GA.CODE + ' - ' + GA.NAME) IS NULL THEN 'NO APLICA' 
			ELSE (GA.CODE + ' - ' + GA.NAME) END AS 'GRUPO ATENCION',--[GrpAtencion], 
		CAST(F.AdmissionNumber AS VARCHAR) AS 'NRO INGRESO',--[NroIngreso],

		CASE IPS.PRESENTATION  WHEN '1' THEN 'NO QUIRURGICO'  WHEN '2' THEN 'QUIRURGICO' WHEN '3' THEN 'PAQUETE'END AS 'PRESENTACION CUPS',--[PresentacionCUPS],
		CUPS.Code 'CODIGO CUPS',--[CodigoCUPS],
		CUPS.Description 'DESCRIPCION CUPS',--[DesCUPS],
		IPS.Code AS 'CODIGO SERVICIOS IPS',--[CodSerIPS],
		IPS.Name AS 'DESCRIPCION SERVICIO IPS',--[DesSerIPS],
		CASE SOD.IsFirstEvent 
			WHEN 1 THEN 'SI' 
			ELSE 'NO' END AS 'PRIMER EVENTO',--[PrimerEvento],
		CAST(SOD.ServiceDate AS DATE) AS 'FECHA CIRUGIA',--[FecCirugia],
		CAST(SOD.GrandTotalSalesPrice AS MONEY) AS 'TOTAL CUPS CIRUGIA',--[TotalCUPSCirugia],
		IPSQX.Code AS 'CODIGO SERVICIO',--[CodigoServicio], 
		IPSQX.Name AS 'DESCRIPCION SERVICIO',--[DesServicio],
		ISNULL(DQ.InvoicedQuantity,SOD.InvoicedQuantity) AS 'CANTIDAD',--[Cantidad],
		CAST(ISNULL(DQ.RateManualSalePrice,SOD.GrandTotalSalesPrice) AS MONEY) AS 'PRECIO MANUAL TARIFARIO',--[PrecioManualTarifario],
		CAST(ISNULL(DQ.TotalSalesPrice,SOD.GrandTotalSalesPrice) AS MONEY) AS 'TOTAL COBRADO',--[TotalCobrado],
		ISNULL(DQ.PerformsHealthProfessionalCode,SOD.PerformsHealthProfessionalCode ) AS 'NRO DOCUMENTO MEDICO',--[NroDocumentoMedico],
		ISNULL(MEDQX.NOMMEDICO,MED.NOMMEDICO) AS 'NOMBRE MEDICO',--[NomMedico],
		SOD.AuthorizationNumber 'NRO AUTORIZACION',--[NroAutorizacion],
		F.InvoiceNumber AS 'NRO CONTROL FACTURA',--[NroControlFactura], 
		CAST(F.InvoiceDate AS DATE) AS 'FECHA CONTROL FACTURA',--[FecControlFactura], 
		CAST(F.InvoiceExpirationDate AS DATE) AS 'FECHA CONTROL VENCIMIENTO',--[FecControlVencimiento], 
		F.TotalInvoice AS 'VALOR CONTROL FACTURA',--[ValControlFactura],
		F.ThirdPartySalesValue AS 'VALOR CONTROL ENTIDAD',--[ValControlEntidad], 
		F.ThirdPartyDiscountValue AS 'VALOR TOTAL DESCUENTO',--[ValTotalDescuento], 
		F.TotalPatientSalesPrice AS 'VALOR TOTAL CUOTA RECUPERACION',--[ValTotalCuotaRecuperacion], 
		F.PatientDiscount AS 'VALOR DESCUENTO CUOTA RECUPERACION',-- [ValDescuentoCuotaRecuperacion],
		C.Name AS 'CATEGORIA',--[Categoria],
		CAST(F.InvoiceDate AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM CTE_PACIENTES_CIRUGIAS AS F WITH (NOLOCK)
	INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
	INNER JOIN Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = DF.ServiceOrderDetailId
	INNER JOIN Billing.InvoiceCategories AS CAT WITH (NOLOCK) ON CAT.Id = F.InvoiceCategoryId
	INNER JOIN Billing.ServiceOrder AS OS WITH (NOLOCK) ON OS.Id = SOD.ServiceOrderId
	INNER JOIN Contract.CUPSEntity AS CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId AND CUPS.ServiceType IN ('4','5')
	INNER JOIN dbo.ADINGRESO AS ING WITH (NOLOCK) ON CAST(ING.NUMINGRES AS VARCHAR) = CAST(F.AdmissionNumber AS VARCHAR)
	INNER JOIN dbo.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = F.PatientCode
	INNER JOIN Common.ThirdParty AS T WITH (NOLOCK) ON T.Id = F.ThirdPartyId
	INNER JOIN Contract.CareGroup AS GA WITH (NOLOCK) ON GA.Id = F.CareGroupId
	INNER JOIN Contract.HealthAdministrator AS EA WITH (NOLOCK) ON EA.Id = F.HealthAdministratorId
	LEFT JOIN Contract.IPSService AS IPS WITH (NOLOCK) ON IPS.Id = SOD.IPSServiceId
	LEFT JOIN Billing.ServiceOrderDetailSurgical AS DQ WITH (NOLOCK) ON DQ.ServiceOrderDetailId = SOD.Id AND DQ.OnlyMedicalFees = '0'
	LEFT JOIN dbo.INPROFSAL AS MEDQX WITH (NOLOCK) ON MEDQX.CODPROSAL = DQ.PerformsHealthProfessionalCode
	LEFT JOIN dbo.INESPECIA AS ESPQX WITH (NOLOCK) ON ESPQX.CODESPECI = MEDQX.CODESPEC1
	LEFT JOIN Contract.IPSService AS IPSQX WITH (NOLOCK) ON IPSQX.Id = DQ.IPSServiceId
	LEFT JOIN Contract.CupsSubgroup AS CSG WITH (NOLOCK) ON CSG.Id =CUPS .CUPSSubGroupId
	LEFT JOIN Billing.InvoiceCategories AS C WITH (NOLOCK) ON C.Id = F.InvoiceCategoryId
	LEFT JOIN dbo.INPROFSAL AS MED WITH (NOLOCK) ON MED.CODPROSAL = SOD.PerformsHealthProfessionalCode 
	WHERE IPS.Presentation IN ('2','3')