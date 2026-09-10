

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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información de cirugías facturadas (procedimientos quirúrgicos y paquetes) con datos de paciente, médico, entidad administradora y valores de la factura para alimentar un cubo de análisis.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeryBilledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe tener Status = ''1'' (activa/válida).; La fecha de factura (InvoiceDate) debe ser >= 2023-01-01.; El servicio CUPS asociado debe tener ServiceType en (''4'',''5'').; El IPSService debe tener Presentation en (''2'',''3'') (quirúrgico o paquete).; Debe existir correspondencia entre el número de ingreso de la factura y dbo.ADINGRESO, y el paciente debe existir en dbo.INPACIENT.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeryBilledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan procedimientos cuya presentación CUPS sea quirúrgica (''2'') o paquete (''3''); excluye no quirúrgicos.; Solo se incluyen servicios CUPS de tipo ''4'' o ''5''.; Solo facturas activas (Status=''1'') desde 2023-01-01.; Excluye honorarios médicos puros del detalle quirúrgico (OnlyMedicalFees=''0'').; Cada fila trae fecha de actualización en zona horaria ''Pakistan Standard Time''.; El identificador de compañía corresponde al nombre de la base de datos actual (DB_NAME).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeryBilledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de documento; Factura; Entidad administradora (EPS/aseguradora); Grupo de atención; Procedimiento CUPS; Servicio IPS; Cirugía; Paquete quirúrgico; Primer evento; Autorización; Cuota de recuperación; Descuento; Médico tratante; Especialidad médica; Ingreso/admisión hospitalaria; Honorarios médicos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeryBilledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalSurgeryBilledSurgeries: Cuando Invoice.Status=''1'' y InvoiceDate>=''2023-01-01'' y CUPS.ServiceType IN (''4'',''5'') y IPSService.Presentation IN (''2'',''3''), retorna fila con datos del paciente, cirugía y factura.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeryBilledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INPACIENT.iptipodoc entre 1 y 15 → Mapea el código numérico a la abreviatura del tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI).; si GA.Code + GA.Name IS NULL → Asigna ''NO APLICA'' como grupo de atención. else Retorna ''Code - Name'' del grupo de atención.; si IPSService.Presentation = ''1'' → ''NO QUIRURGICO'' else Si ''2'' → ''QUIRURGICO''; si ''3'' → ''PAQUETE''.; si ServiceOrderDetail.IsFirstEvent = 1 → Marca ''PRIMER EVENTO'' = ''SI''. else Marca ''NO''.; si Existe ServiceOrderDetailSurgical (DQ) con OnlyMedicalFees=''0'' → Usa cantidad, precio manual, total cobrado, médico y servicio IPS del detalle quirúrgico (DQ). else Usa los valores del ServiceOrderDetail (SOD).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeryBilledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.InvoiceCategories; Billing.ServiceOrder; Billing.ServiceOrderDetailSurgical; Contract.CUPSEntity; Contract.IPSService; Contract.CareGroup; Contract.HealthAdministrator; Contract.CupsSubgroup; Common.ThirdParty; dbo.ADINGRESO; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeryBilledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSurgeryBilledSurgeries';
GO
