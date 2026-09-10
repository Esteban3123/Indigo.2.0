/****** Object:  StoredProcedure [Billing].[SP_GetInfoHospitalizacionRIPS]    Script Date: 23/01/2026 03:44:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================================================================================
-- Autor:				Giovanny Plazas
-- Fecha Creación:		2024-09-12
-- Descripción:			Genera datos RIPS de hospitalización. Consolida flujo normal + RIPSSupportRecord
--
-- Modificación:		Anthony Ocampo (2025-12-11)
--						Integración RIPSSupportRecord: estancias externas, fechas MIN/MAX, fuente más reciente
--
-- Parámetros:			@Parameters XML: /Data/Document/InvoiceNumber, /Data/Document/DocumentType
-- Resultado:			Dataset RIPS hospitalización (normativa colombiana)
-- =============================================================================================================
ALTER PROCEDURE [Billing].[SP_GetInfoHospitalizacionRIPS] 
	@Parameters AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	-- SECCIÓN 1: DECLARACIÓN DE TABLAS TEMPORALES
	
	-- Facturas a procesar (del XML)
	DECLARE @Invoices TABLE
	(
		InvoiceId			INT				NOT NULL,
		InvoiceNumber		VARCHAR(20)		NOT NULL,
		OutputDiagnosis		CHAR(4)			NULL,
		DocumentType		TINYINT			NOT NULL,
		AdmissionNumber		CHAR(10)		NOT NULL,
		[Status]			TINYINT			NOT NULL,
		IsCutAccount		BIT				NOT NULL,	-- Es corte de cuenta
		CutType				TINYINT			NOT NULL,	-- 1=Ingreso, 2=Ambos, 4=Egreso
		InitialDate			DATETIME		NOT NULL,
		OutputDate			DATETIME		NOT NULL
	);

	-- Datos consolidados de hospitalización (flujo normal + RIPSSupport)
	DECLARE @GeneralData TABLE 
	(
		InvoiceId						INT				NOT NULL,
		InvoiceNumber					VARCHAR(20)		NOT NULL,
		OutputDiagnosis					VARCHAR(4)		NULL,
		DocumentType					TINYINT			NOT NULL,
		PatientCode						VARCHAR(25)		NULL,
		CODCENATE						VARCHAR(10)		NOT NULL,	-- Centro de atención
		ICAUSAING						INT				NOT NULL,	-- Causa de ingreso
		CODDIAEGR						VARCHAR(4)		NULL,		-- Dx egreso
		IFECHAING						DATETIME		NOT NULL,	-- Fecha ingreso
		IdEntryRoutesHealthServices		INT				NULL,		-- Vía ingreso
		NUMINGRES						CHAR(10)		NOT NULL,
		INDICAPAC						VARCHAR(2)		NULL,		-- Razón egreso
		ESTPACEGR						INT				NULL,		-- Estado al egreso
		NUMEFOLIO						NCHAR(10)		NULL,
		IAUTORIZA						VARCHAR(15)		NULL,
		FECALTPAC						DATETIME		NOT NULL,	-- Fecha alta
		
		-- Campos RIPSSupport
		HasRIPSSupport					BIT				NOT NULL,	-- Tiene datos RIPSSupport
		HasNormalFlow					BIT				NOT NULL,	-- Tiene datos flujo normal
		RSR_MinInitialDate				DATETIME		NULL,		-- Fecha inicio RIPSSupport
		RSR_MaxFinalDate				DATETIME		NULL,		-- Fecha egreso RIPSSupport
		RSR_MainDiagnosis				CHAR(4)			NULL,		-- Dx principal
		RSR_RelatedDiagnosis			CHAR(4)			NULL,		-- Dx relacionado
		RSR_DischargeCondition			CHAR(2)			NULL,		-- Condición egreso
		RSR_DiagnosisCauseDeath			CHAR(4)			NULL,		-- Dx causa muerte
		
		-- Fechas consolidadas (MIN inicio, MAX egreso)
		FinalStartDate					DATETIME		NOT NULL,
		FinalEndDate					DATETIME		NOT NULL,
		
		-- Fuente ganadora: 'N'=Normal, 'R'=RIPSSupport (la más reciente)
		WinnerSource					CHAR(1)			NOT NULL
	);

	-- Diagnósticos (principales y relacionados)
	DECLARE @DIAGNOSTICOS TABLE
	(
		InvoiceId			INT				NULL,
		AdmissionNumber		CHAR(10)		NULL,
		IdPartition			INT				NULL,		-- Prioridad
		DiagnosticCode		CHAR(4)			NULL,		-- CIE-10
		CODDIAPRI			BIT				NULL,		-- 1=Principal, 0=Relacionado
		Diagnostic			VARCHAR(20)		NULL		-- Para pivoteo
	);

	-- Diagnósticos pivoteados (hasta 3 por factura)
	DECLARE @QueryDiagnostics TABLE
	(
		InvoiceId			INT				NULL,
		AdmissionNumber		CHAR(10)		NULL,
		DiagnosticCode1		CHAR(4)			NULL,
		DiagnosticCode2		CHAR(4)			NULL,
		DiagnosticCode3		CHAR(4)			NULL,
		CODDIAPRI			BIT				NULL		-- 1=Principal, 0=Relacionado
	);

	-- Datos RIPSSupportRecord (estancias externas de grupos clínicos)
	DECLARE @RIPSSupportData TABLE
	(
		AdmissionNumber				CHAR(10)		NOT NULL,
		MinInitialDate				DATETIME		NULL,		-- MIN de todas las estancias
		MaxFinalDate				DATETIME		NULL,		-- MAX de todas las estancias
		-- Del detalle más reciente
		LatestDetailId				INT				NULL,
		LatestMainDiagnosis			CHAR(4)			NULL,
		LatestRelatedDiagnosis		CHAR(4)			NULL,
		LatestDischargeCondition	CHAR(2)			NULL,
		LatestDiagnosisCauseDeath	CHAR(4)			NULL
	);

	BEGIN TRY

		-- SECCIÓN 2: CARGA DE FACTURAS DESDE XML (solo Status = 1)
		
		INSERT INTO @Invoices 
		(	
			InvoiceId,
			InvoiceNumber,
			OutputDiagnosis,
			DocumentType,
			AdmissionNumber,
			[Status],
			IsCutAccount,
			CutType,
			InitialDate,
			OutputDate
		)
		SELECT 
			i.Id				AS InvoiceId,
			i.InvoiceNumber		AS InvoiceNumber,
			i.OutputDiagnosis	AS OutputDiagnosis,
			i.DocumentType		AS DocumentType,
			i.AdmissionNumber	AS AdmissionNumber,
			i.[Status]			AS [Status],
			i.IsCutAccount		AS IsCutAccount,
			i.CutType			AS CutType,
			i.InitialDate		AS InitialDate,
			i.OutputDate		AS OutputDate
		FROM @Parameters.nodes('/Data/Document') t(x)
		INNER JOIN Billing.Invoice i WITH(NOLOCK) 
			ON t.x.value('InvoiceNumber[1]', 'VARCHAR(20)') = i.InvoiceNumber 
			AND t.x.value('DocumentType[1]', 'TINYINT') = i.DocumentType
		WHERE i.[Status] = 1  -- Solo facturas activas
		GROUP BY 
			i.Id,
			i.InvoiceNumber,
			i.OutputDiagnosis,
			i.DocumentType,
			i.AdmissionNumber,
			i.[Status],
			i.IsCutAccount,
			i.CutType,
			i.InitialDate,
			i.OutputDate;

		-- SECCIÓN 2.1: CARGA DE RIPSSupportRecord
		-- Filtros: RIPSHospitalStayRecord=1 AND (Status=1 OR ServiceOrder.Status=2)
		
		INSERT INTO @RIPSSupportData
		(
			AdmissionNumber,
			MinInitialDate,
			MaxFinalDate,
			LatestDetailId,
			LatestMainDiagnosis,
			LatestRelatedDiagnosis,
			LatestDischargeCondition,
			LatestDiagnosisCauseDeath
		)
		SELECT
			rsr.AdmissionNumber,
			MIN(rsrd.InitialDate) AS MinInitialDate,
			MAX(rsrd.FinalDate) AS MaxFinalDate,
			-- Datos del detalle más reciente
			MAX(CASE WHEN rsrd.FinalDate = latest.MaxFinalDate THEN rsrd.Id END) AS LatestDetailId,
			MAX(CASE WHEN rsrd.FinalDate = latest.MaxFinalDate THEN rsrd.MainDiagnosis END) AS LatestMainDiagnosis,
			MAX(CASE WHEN rsrd.FinalDate = latest.MaxFinalDate THEN rsrd.RelatedDiagnosis END) AS LatestRelatedDiagnosis,
			MAX(CASE WHEN rsrd.FinalDate = latest.MaxFinalDate THEN rsrd.DischargeCondition END) AS LatestDischargeCondition,
			MAX(CASE WHEN rsrd.FinalDate = latest.MaxFinalDate THEN rsrd.DiagnosisCauseDeath END) AS LatestDiagnosisCauseDeath
		FROM Billing.RIPSSupportRecord rsr WITH(NOLOCK)
		INNER JOIN Billing.RIPSSupportRecordDetail rsrd WITH(NOLOCK)
			ON rsr.Id = rsrd.RIPSSupportRecordId
		INNER JOIN (
			SELECT rsr2.AdmissionNumber, MAX(rsrd2.FinalDate) AS MaxFinalDate
			FROM Billing.RIPSSupportRecord rsr2 WITH(NOLOCK)
			INNER JOIN Billing.RIPSSupportRecordDetail rsrd2 WITH(NOLOCK)
				ON rsr2.Id = rsrd2.RIPSSupportRecordId
			WHERE rsr2.RIPSHospitalStayRecord = 1
				AND rsr2.AdmissionNumber IN (SELECT AdmissionNumber FROM @Invoices)
			GROUP BY rsr2.AdmissionNumber
		) latest ON rsr.AdmissionNumber = latest.AdmissionNumber
		WHERE rsr.AdmissionNumber IN (SELECT AdmissionNumber FROM @Invoices)
			AND rsr.RIPSHospitalStayRecord = 1
			AND (
				rsr.[Status] = 1 
				OR EXISTS (
					SELECT 1 FROM Billing.ServiceOrder so WITH(NOLOCK) 
					WHERE so.Id = rsr.ServiceOrderId AND so.[Status] = 2
				)
			)
		GROUP BY rsr.AdmissionNumber;

		-- SECCIÓN 3: CONSOLIDACIÓN DE DATOS DE HOSPITALIZACIÓN
		-- Cruza: ADINGRESO, CHREGEGRE, HCREGEGRE, HCHISPACA
		-- Fechas: CutType 1/2 = admisión, CutType 4 = factura
		-- Filtro: TIPOINGRE = 2 (hospitalizaciones)
		
		INSERT INTO @GeneralData
		(
			InvoiceId,
			InvoiceNumber,
			OutputDiagnosis,
			DocumentType,
			PatientCode,
			CODCENATE,
			ICAUSAING,
			CODDIAEGR,
			IFECHAING,
			IdEntryRoutesHealthServices,
			NUMINGRES,
			INDICAPAC,
			ESTPACEGR,
			NUMEFOLIO,
			IAUTORIZA,
			FECALTPAC,
			-- Campos RIPSSupport
			HasRIPSSupport, HasNormalFlow,
			RSR_MinInitialDate, RSR_MaxFinalDate,
			RSR_MainDiagnosis, RSR_RelatedDiagnosis, RSR_DischargeCondition, RSR_DiagnosisCauseDeath,
			FinalStartDate, FinalEndDate, WinnerSource
		)
		SELECT	
			i.InvoiceId,
			i.InvoiceNumber,
			i.OutputDiagnosis,
			i.DocumentType,
			ad.IPCODPACI,
			ad.CODCENATE,
			ad.ICAUSAING,
			ad.CODDIAEGR,
			-- Fecha ingreso: CutType 1/2 = admisión, otros = factura
			CASE WHEN i.CutType IN (1, 2) THEN ad.IFECHAING ELSE i.InitialDate END AS IFECHAING,
			ad.IdEntryRoutesHealthServices,
			ad.NUMINGRES,
			IIF(i.IsCutAccount = 1, 13, his.INDICAPAC) AS INDICAPAC,  -- Corte cuenta = 13
			Egreso.ESTPACEGR,
			HCE.NUMEFOLIO,
			ISNULL(RTRIM(ad.IAUTORIZA), '') AS IAUTORIZA,
			-- Fecha alta: CutType 1/4 = HCE o salida, otros = salida
			CASE WHEN i.CutType IN(1, 4) THEN COALESCE(HCE.FECALTPAC, i.OutputDate) ELSE i.OutputDate END AS FECALTPAC,
			
			-- Flags RIPSSupport
			CASE WHEN rsd.AdmissionNumber IS NOT NULL THEN 1 ELSE 0 END AS HasRIPSSupport,
			CASE WHEN his.INDICAPAC IS NOT NULL OR HCE.FECALTPAC IS NOT NULL THEN 1 ELSE 0 END AS HasNormalFlow,
			
			-- Datos RIPSSupport
			rsd.MinInitialDate AS RSR_MinInitialDate,
			rsd.MaxFinalDate AS RSR_MaxFinalDate,
			rsd.LatestMainDiagnosis AS RSR_MainDiagnosis,
			rsd.LatestRelatedDiagnosis AS RSR_RelatedDiagnosis,
			rsd.LatestDischargeCondition AS RSR_DischargeCondition,
			rsd.LatestDiagnosisCauseDeath AS RSR_DiagnosisCauseDeath,
			
			-- FinalStartDate: MIN de ambas fuentes
			CASE 
				WHEN rsd.MinInitialDate IS NULL THEN 
					CASE WHEN i.CutType IN (1, 2) THEN ad.IFECHAING ELSE i.InitialDate END
				WHEN HCE.FECALTPAC IS NULL AND his.INDICAPAC IS NULL THEN rsd.MinInitialDate
				ELSE CASE 
					WHEN rsd.MinInitialDate < CASE WHEN i.CutType IN (1, 2) THEN ad.IFECHAING ELSE i.InitialDate END 
					THEN rsd.MinInitialDate
					ELSE CASE WHEN i.CutType IN (1, 2) THEN ad.IFECHAING ELSE i.InitialDate END
				END
			END AS FinalStartDate,
			
			-- FinalEndDate: MAX de ambas fuentes
			CASE 
				WHEN rsd.MaxFinalDate IS NULL THEN 
					CASE WHEN i.CutType IN(1, 4) THEN COALESCE(Egreso.FECEGRESO, i.OutputDate) ELSE i.OutputDate END
				WHEN HCE.FECALTPAC IS NULL AND his.INDICAPAC IS NULL THEN rsd.MaxFinalDate
				ELSE CASE 
					WHEN rsd.MaxFinalDate > CASE WHEN i.CutType IN(1, 4) THEN COALESCE(Egreso.FECEGRESO, i.OutputDate) ELSE i.OutputDate END 
					THEN rsd.MaxFinalDate
					ELSE CASE WHEN i.CutType IN(1, 4) THEN COALESCE(Egreso.FECEGRESO, i.OutputDate) ELSE i.OutputDate END
				END
			END AS FinalEndDate,
			
			-- WinnerSource: 'N'=Normal, 'R'=RIPSSupport (fuente más reciente)
			CASE 
				WHEN (HCE.FECALTPAC IS NULL AND his.INDICAPAC IS NULL) AND rsd.MaxFinalDate IS NOT NULL THEN 'R'
				WHEN rsd.MaxFinalDate IS NULL THEN 'N'
				WHEN rsd.MaxFinalDate > CASE WHEN i.CutType IN(1, 4) THEN COALESCE(HCE.FECALTPAC, i.OutputDate) ELSE i.OutputDate END THEN 'R'
				ELSE 'N'
			END AS WinnerSource
			
		FROM @Invoices i
		INNER JOIN ADINGRESO ad WITH(NOLOCK) ON ad.NUMINGRES = i.AdmissionNumber
		LEFT JOIN dbo.CHREGEGRE Egreso WITH (NOLOCK) ON ad.NUMINGRES = Egreso.NUMINGRES
		LEFT JOIN HCREGEGRE HCE WITH(NOLOCK) ON HCE.NUMINGRES = ad.NUMINGRES
		OUTER APPLY (
			SELECT TOP 1 his.INDICAPAC
			FROM HCHISPACA his WITH(NOLOCK)
			JOIN INUNIFUNC inuni WITH(NOLOCK) ON his.UFUCODIGO = inuni.UFUCODIGO
			WHERE his.NUMINGRES = ad.NUMINGRES
				AND inuni.UFUTIPUNI <> 1
				AND NOT his.INDICAPAC IN (1, 2, 3, 4, 5, 6, 7, 8, 13)
				AND his.FECHISPAC <= CASE
					WHEN i.CutType IN(1, 4) THEN COALESCE(HCE.FECALTPAC, i.OutputDate)
					ELSE i.OutputDate
				END
			ORDER BY
				CASE WHEN HCE.NUMEFOLIO = his.NUMEFOLIO THEN 0 ELSE 1 END,
				his.FECHISPAC DESC
		) his
		LEFT JOIN @RIPSSupportData rsd ON rsd.AdmissionNumber = ad.NUMINGRES
		WHERE ad.TIPOINGRE = 2  -- Solo hospitalizaciones
			AND (HCE.FECALTPAC IS NOT NULL OR his.INDICAPAC IS NOT NULL OR rsd.AdmissionNumber IS NOT NULL)
			AND (
				rsd.AdmissionNumber IS NOT NULL
				OR HCE.FECALTPAC IS NULL
				OR CAST(IIF(i.CutType IN (1, 2), ad.IFECHAING, i.InitialDate) AS DATE) <= CAST(HCE.FECALTPAC AS DATE)
			);

		-- SECCIÓN 4: EXTRACCIÓN DE DIAGNÓSTICOS
		-- Prioridad: E(greso) > A(dmisión) > otros, luego por fecha DESC
		
		INSERT INTO @DIAGNOSTICOS
		(
			InvoiceId,
			AdmissionNumber,
			IdPartition,
			DiagnosticCode,
			CODDIAPRI,
			Diagnostic
		)
		SELECT	
			T.InvoiceId,
			T.AdmissionNumber,
			T.IdPartition,
			T.DiagnosticCode,
			T.CODDIAPRI,
			CONCAT('DiagnosticCode', T.IdPartition) AS Diagnostic
		FROM
		(
			SELECT 
				gd.InvoiceId,
				gd.NUMINGRES AS AdmissionNumber,
				ROW_NUMBER() OVER (
					PARTITION BY gd.InvoiceId, gd.NUMINGRES, idp.CODDIAPRI 
					ORDER BY
						CASE idp.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END ASC,
						idp.FECDIAGNO DESC
				) AS IdPartition,
				idp.CODDIAGNO AS DiagnosticCode,
				idp.CODDIAPRI
			FROM INDIAGNOP idp WITH(NOLOCK)
			INNER JOIN @GeneralData gd ON idp.NUMINGRES = gd.NUMINGRES
		) T;

		-- SECCIÓN 5: PIVOTEO DE DIAGNÓSTICOS (filas -> columnas)
		
		INSERT INTO @QueryDiagnostics
		(
			InvoiceId,
			AdmissionNumber,
			DiagnosticCode1,
			DiagnosticCode2,
			DiagnosticCode3,
			CODDIAPRI
		)
		SELECT	
			InvoiceId,
			AdmissionNumber,
			DiagnosticCode1,
			DiagnosticCode2,
			DiagnosticCode3,
			CODDIAPRI
		FROM 
		(
			SELECT 
				InvoiceId,
				AdmissionNumber,
				DiagnosticCode,
				Diagnostic,
				CODDIAPRI
			FROM @DIAGNOSTICOS
		) AS SourceTable
		PIVOT 
		(
			MAX(DiagnosticCode)
			FOR Diagnostic IN (DiagnosticCode1, DiagnosticCode2, DiagnosticCode3)
		) AS PivotTable;

		-- SECCIÓN 6: RESULTADO FINAL - FORMATO RIPS
		-- Consolida flujo normal + RIPSSupport. Fechas: MIN/MAX. Dx y egreso: fuente más reciente
		
		SELECT	
			LTRIM(RTRIM(ca.CODIPSSEC)) AS codPrestador,
			
			-- Vía ingreso: '01'/'04' -> '03'
			CASE WHEN erhs.RIPSCode IN ('01', '04') THEN '03' ELSE erhs.RIPSCode END AS viaIngresoServicioSalud,
			
			-- Fecha inicio: primera HC de hospitalización dentro del periodo facturado.
			CAST(FORMAT(fechas.FechaInicioHospitalizacionRips, 'yyyy-MM-dd HH:mm') AS VARCHAR(50)) AS fechaInicioAtencion,
			
			g.IAUTORIZA AS numAutorizacion,
			cfa.RIPSCode AS causaMotivoAtencion,
			
			-- Dx principal: RIPSSupport si gana, sino flujo normal
			CASE 
				WHEN g.WinnerSource = 'R' AND g.RSR_MainDiagnosis IS NOT NULL THEN g.RSR_MainDiagnosis
				ELSE COALESCE(qdPrin.DiagnosticCode2, qdPrin.DiagnosticCode1, g.CODDIAEGR, g.OutputDiagnosis, g.RSR_MainDiagnosis, 'Z000')
			END AS codDiagnosticoPrincipal,
			
			-- Dx principal egreso
			diagnosticoEgreso.codDiagnosticoPrincipalE,
			
			-- Dx relacionados (RIPSSupport solo tiene 1)
			diagnosticosRelacionadosEgreso.DiagnosticCode1 AS codDiagnosticoRelacionadoE1,
			diagnosticosRelacionadosEgreso.DiagnosticCode2 AS codDiagnosticoRelacionadoE2,
			diagnosticosRelacionadosEgreso.DiagnosticCode3 AS codDiagnosticoRelacionadoE3,
			
			NULL AS codComplicacion,
			
			-- Condición egreso: 01=Alta, 02=Remisión, 03=Extramural, 04=Contraremisión, 05=Hosp misma inst, 06=Muerte, 08=Corte
			CASE 
				WHEN g.ESTPACEGR = 3 THEN '06'
				WHEN g.WinnerSource = 'R' AND g.RSR_DischargeCondition IS NOT NULL THEN
					CASE  
						WHEN g.RSR_DischargeCondition = '12' AND refRegister.EXTRAMURAL = 1 THEN '03'
						WHEN g.RSR_DischargeCondition IN ('12', '15', '16') THEN '01'
						WHEN g.RSR_DischargeCondition = '11' THEN '02' 
						WHEN g.RSR_DischargeCondition = '10' AND erhs.RIPSCode = '13' THEN '05'
						WHEN g.RSR_DischargeCondition = '10' THEN '04'
						WHEN g.RSR_DischargeCondition = '9' THEN '06'
						WHEN g.RSR_DischargeCondition = '13' THEN '08'
						ELSE '01'
					END
				ELSE
					CASE  
						WHEN COALESCE(g.INDICAPAC, g.RSR_DischargeCondition) = '12' AND refRegister.EXTRAMURAL = 1 THEN '03'
						WHEN COALESCE(g.INDICAPAC, g.RSR_DischargeCondition) IN ('12', '15', '16') THEN '01'
						WHEN COALESCE(g.INDICAPAC, g.RSR_DischargeCondition) = '11' THEN '02' 
						WHEN COALESCE(g.INDICAPAC, g.RSR_DischargeCondition) = '10' AND erhs.RIPSCode = '13' THEN '05'
						WHEN COALESCE(g.INDICAPAC, g.RSR_DischargeCondition) = '10' THEN '04'
						WHEN COALESCE(g.INDICAPAC, g.RSR_DischargeCondition) = '9' THEN '06'
						WHEN COALESCE(g.INDICAPAC, g.RSR_DischargeCondition) = '13' THEN '08'
						ELSE '01'
					END
			END AS condicionDestinoUsuarioEgreso,
			
			-- Dx causa muerte
			CASE 
				WHEN g.WinnerSource = 'R' THEN g.RSR_DiagnosisCauseDeath
				WHEN g.ESTPACEGR = 3 THEN COALESCE(g.CODDIAEGR, g.RSR_DiagnosisCauseDeath)
				ELSE g.RSR_DiagnosisCauseDeath
			END AS codDiagnosticoCausaMuerte,
			
			-- Fecha egreso: no debe superar la fecha final de la factura.
			CAST(FORMAT(fechas.FechaEgresoHospitalizacionRips, 'yyyy-MM-dd HH:mm') AS VARCHAR(50)) AS fechaEgreso,
			
			NULL AS consecutivo,
			CAST('' AS VARCHAR(20)) AS codDiagnosticoPrincipalCIE11,
			CAST('' AS VARCHAR(250)) AS nomCodDiagnosticoPrincipalCIE11,
			CAST('' AS VARCHAR(20)) AS codDiagnosticoPrincipalECIE11,
			CAST('' AS VARCHAR(250)) AS nomCodDiagnosticoPrincipalECIE11,
			CAST('' AS VARCHAR(20)) AS codDiagnosticoRelacionadoE1CIE11,
			CAST('' AS VARCHAR(250)) AS nomCodDiagnosticoRelacionadoE1CIE11,
			CAST('' AS VARCHAR(20)) AS codDiagnosticoRelacionadoE2CIE11,
			CAST('' AS VARCHAR(250)) AS nomCodDiagnosticoRelacionadoE2CIE11,
			CAST('' AS VARCHAR(20)) AS codDiagnosticoRelacionadoE3CIE11,
			CAST('' AS VARCHAR(250)) AS nomCodDiagnosticoRelacionadoE3CIE11,
			CAST('' AS VARCHAR(20)) AS codComplicacionCIE11,
			CAST('' AS VARCHAR(250)) AS nomCodComplicacionCIE11,
			CAST('' AS VARCHAR(20)) AS codDiagnosticoCausaMuerteCIE11,
			CAST('' AS VARCHAR(250)) AS nomCodDiagnosticoCausaMuerteCIE11,
			rda.GetCodigoVIDAByDocumentNumber(g.PatientCode) AS codigoVIDA,
			g.InvoiceNumber AS InvoiceNumber,
			g.DocumentType AS DocumentType
			
		FROM @GeneralData g
		INNER JOIN @Invoices i ON i.InvoiceId = g.InvoiceId
		INNER JOIN dbo.ADCENATEN AS ca WITH (NOLOCK) ON g.CODCENATE = ca.CODCENATE
		INNER JOIN Causesofattention cfa WITH(NOLOCK) ON g.ICAUSAING = cfa.Code
		LEFT JOIN @QueryDiagnostics qdPrin ON qdPrin.InvoiceId = g.InvoiceId AND qdPrin.AdmissionNumber = g.NUMINGRES AND qdPrin.CODDIAPRI = 1
		-- Fecha inicio: Primera HC no urgencias
		OUTER APPLY (
			SELECT TOP 1 hc.FECHISPAC AS FechaInicioHospitalizacion
			FROM HCHISPACA hc WITH(NOLOCK)
			JOIN INUNIFUNC inuni WITH(NOLOCK) ON hc.UFUCODIGO = inuni.UFUCODIGO AND inuni.UFUTIPUNI <> 1
			WHERE hc.NUMINGRES = g.NUMINGRES
			ORDER BY hc.FECHISPAC ASC
		) hd
		CROSS APPLY (
			SELECT
				CASE
					WHEN COALESCE(hd.FechaInicioHospitalizacion, g.FinalStartDate) > i.InitialDate
						THEN COALESCE(hd.FechaInicioHospitalizacion, g.FinalStartDate)
					ELSE i.InitialDate
				END AS FechaInicioHospitalizacionRips,
				IIF(i.InitialDate > g.FinalEndDate, i.InitialDate, g.FinalEndDate) AS FechaEgresoBase
		) fechasBase
		CROSS APPLY (
			SELECT
				CASE
					WHEN fechasBase.FechaEgresoBase < fechasBase.FechaInicioHospitalizacionRips
						THEN fechasBase.FechaInicioHospitalizacionRips
					WHEN fechasBase.FechaEgresoBase > i.OutputDate
						THEN i.OutputDate
					ELSE fechasBase.FechaEgresoBase
				END AS FechaEgresoHospitalizacionRips,
				fechasBase.FechaInicioHospitalizacionRips
		) fechas
		LEFT JOIN EntryRoutesHealthServices erhs WITH (NOLOCK) ON g.IdEntryRoutesHealthServices = erhs.Id
		LEFT JOIN HCREFCONP refRegister WITH (NOLOCK) ON refRegister.NUMINGRES = g.NUMINGRES AND refRegister.ESTADO <> 4
		LEFT JOIN @QueryDiagnostics qdRela ON qdRela.InvoiceId = g.InvoiceId AND qdRela.AdmissionNumber = g.NUMINGRES AND qdRela.CODDIAPRI = 0
		CROSS APPLY (
			SELECT
				CASE
					WHEN g.WinnerSource = 'R' AND g.RSR_MainDiagnosis IS NOT NULL THEN g.RSR_MainDiagnosis
					ELSE COALESCE(qdPrin.DiagnosticCode1, g.CODDIAEGR, g.OutputDiagnosis, g.RSR_MainDiagnosis)
				END AS codDiagnosticoPrincipalE
		) diagnosticoEgreso
		OUTER APPLY (
			SELECT	MAX(CASE WHEN diagnosticos.IdPartition = 1 THEN diagnosticos.DiagnosticCode END) AS DiagnosticCode1,
					MAX(CASE WHEN diagnosticos.IdPartition = 2 THEN diagnosticos.DiagnosticCode END) AS DiagnosticCode2,
					MAX(CASE WHEN diagnosticos.IdPartition = 3 THEN diagnosticos.DiagnosticCode END) AS DiagnosticCode3
			FROM (
				SELECT	relacionado.DiagnosticCode,
						ROW_NUMBER() OVER (ORDER BY relacionado.SortOrder) AS IdPartition
				FROM (VALUES
						(1, CASE
								WHEN g.WinnerSource = 'R' AND g.RSR_RelatedDiagnosis IS NOT NULL THEN g.RSR_RelatedDiagnosis
								ELSE COALESCE(qdRela.DiagnosticCode1, g.RSR_RelatedDiagnosis)
							END),
						(2, CASE WHEN g.WinnerSource = 'R' THEN NULL ELSE qdRela.DiagnosticCode2 END),
						(3, CASE WHEN g.WinnerSource = 'R' THEN NULL ELSE qdRela.DiagnosticCode3 END)
					) relacionado(SortOrder, DiagnosticCode)
				WHERE relacionado.DiagnosticCode IS NOT NULL
					AND (
						diagnosticoEgreso.codDiagnosticoPrincipalE IS NULL
						OR relacionado.DiagnosticCode <> diagnosticoEgreso.codDiagnosticoPrincipalE
					)
			) diagnosticos
		) diagnosticosRelacionadosEgreso
		WHERE (g.HasRIPSSupport = 1 OR hd.FechaInicioHospitalizacion IS NOT NULL)
			AND COALESCE(hd.FechaInicioHospitalizacion, g.FinalStartDate) <= i.OutputDate
			AND fechas.FechaInicioHospitalizacionRips <= i.OutputDate;
	
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20));
	END CATCH
	
END
