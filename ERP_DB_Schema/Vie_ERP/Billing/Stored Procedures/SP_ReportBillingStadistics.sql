-- ================================================================================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 29/05/2023
-- Description:	Sp que se encarga de obtener los datos para el reporte estadístico de facturación
-- ================================================================================================

CREATE PROCEDURE [Billing].[SP_ReportBillingStadistics]

@XmlCriterias XML,
@XmlFilters XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY
	BEGIN TRY
	DECLARE @DateStart DATETIME
		   ,@DateEnd DATETIME
		   ,@ReportType INT
		   ,@GroupBy INT
		   ,@InvoiceType VARCHAR(30)
		   ,@includeCopay BIT
		   ,@BasicBilling BIT	
		   ,@Status INT
		   ,@CareCenter VARCHAR(MAX)
		   ,@ToCurrency INT
		   ,
			------FILTROS--------
			@ThirdParty VARCHAR(MAX)
		   ,@Entity VARCHAR(MAX)
		   ,@CareGroup VARCHAR(MAX)
		   ,@Users VARCHAR(MAX)
		   ,@FilterByInvoiceType BIT = 0
		   ,@FilterByThirdParty BIT = 0
		   ,@FilterByEntity BIT = 0
		   ,@FilterByCareGroup BIT = 0
		   ,@FilterByCareCenter BIT = 0
		   ,@FilterByUsers BIT = 0
		   ,@OfficialCurrency INT
		   ,@CurrencyReportId INT
		   ,@CurrencyReportAbreviation VARCHAR(5)

	--Se obtienen los criterios
	SELECT
		@DateStart = t.x.value('DateStart[1]', 'DATETIME')
	   ,@DateEnd = t.x.value('DateEnd[1]', 'DATETIME')
	   ,@ReportType = t.x.value('ReportType[1]', 'int')
	   ,@GroupBy = t.x.value('GroupBy[1]', 'int')
	   ,@InvoiceType = t.x.value('InvoiceType[1]', 'VARCHAR(30)')
	   ,@Status = t.x.value('Status[1]', 'int')
	   ,@CareCenter = t.x.value('CareCenter[1]', 'VARCHAR(MAX)')
	   ,@ToCurrency = t.x.value('CurrencyReport[1]', 'int')
	FROM @XmlCriterias.nodes('/Data') t (x)

	--Se obtienen los filtros
	SELECT
		@ThirdParty = t.x.value('ThirdParty[1]', 'VARCHAR(MAX)')
	   ,@Entity = t.x.value('Entity[1]', 'VARCHAR(MAX)')
	   ,@CareGroup = t.x.value('CareGroup[1]', 'VARCHAR(MAX)')
	   ,@Users = t.x.value('Users[1]', 'VARCHAR(MAX)')
	FROM @XmlFilters.nodes('/Data') t (x)

	-- =====================================================
	-- TABLAS TEMPORALES PARA FILTROS (con índices)
	-- =====================================================
	CREATE TABLE #Table_InvoiceType (Id TINYINT PRIMARY KEY)
	CREATE TABLE #Table_ThirdParty (Id INT PRIMARY KEY)
	CREATE TABLE #Table_Entity (Id INT PRIMARY KEY)
	CREATE TABLE #Table_CareGroup (Id INT PRIMARY KEY)
	CREATE TABLE #Table_Users (userCode VARCHAR(20) PRIMARY KEY)
	CREATE TABLE #Table_CareCenter (Code CHAR(10) PRIMARY KEY)

	-- Procesar filtro de tipo de factura
	IF ISNULL(@InvoiceType, '') <> ''
	BEGIN
		SET @FilterByInvoiceType = 1

		INSERT INTO #Table_InvoiceType
			SELECT CAST(Data AS INT) Data 		
			FROM dbo.Split(@InvoiceType, ',')

		IF EXISTS(SELECT 1 FROM #Table_InvoiceType WHERE Id = 8) 
		BEGIN
			SET @includeCopay = 1
			DELETE FROM #Table_InvoiceType WHERE Id = 8		 
		END
		ELSE BEGIN
			SET @includeCopay = 0
		END

		IF EXISTS(SELECT 1 FROM #Table_InvoiceType WHERE Id = 6) 
		BEGIN
			SET @BasicBilling = 1
			DELETE FROM #Table_InvoiceType WHERE Id = 6		 
		END
		ELSE BEGIN
			SET @BasicBilling = 0
		END
	END

	IF ISNULL(@ThirdParty, '') <> ''
	BEGIN
		SET @FilterByThirdParty = 1
		INSERT INTO #Table_ThirdParty
			SELECT CAST(Data AS INT) Data 		
			FROM dbo.Split(@ThirdParty, ',')
	END

	IF ISNULL(@Entity, '') <> ''
	BEGIN
		SET @FilterByEntity = 1
		INSERT INTO #Table_Entity
			SELECT CAST(Data AS INT) Data 		
			FROM dbo.Split(@Entity, ',')
	END

	IF ISNULL(@CareGroup, '') <> ''
	BEGIN
		SET @FilterByCareGroup = 1
		INSERT INTO #Table_CareGroup
			SELECT CAST(Data AS INT) Data 		
			FROM dbo.Split(@CareGroup, ',')
	END

	IF ISNULL(@Users, '') <> ''
	BEGIN
		SET @FilterByUsers = 1
		INSERT INTO #Table_Users
			SELECT CAST(Data AS VARCHAR(20)) Data 	
			FROM dbo.Split(@Users, ',')
	END

	IF ISNULL(@CareCenter, '') <> ''
	BEGIN
		SET @FilterByCareCenter = 1
		INSERT INTO #Table_CareCenter
			SELECT CAST(Data AS CHAR(10)) Data 		
			FROM dbo.Split(@CareCenter, ',')
	END

	-- Se obtiene la moneda oficial
	SELECT @OfficialCurrency = OfficialCurrencyId
	FROM GeneralLedger.CompanySettings

	-- Obtener ID y abreviación de la moneda del reporte
	IF EXISTS(SELECT 1 FROM Common.Currency c WHERE c.Id = @ToCurrency)
	BEGIN
		SELECT 
			@CurrencyReportId = Id, 
			@CurrencyReportAbreviation = Abbreviation
		FROM Common.Currency 
		WHERE Id = @ToCurrency
	END

	-- =====================================================
	-- TABLAS TEMPORALES
	-- =====================================================
	
	-- Tabla temporal para usuarios
	SELECT
		u.UserCode,
		per.Fullname
	INTO #Users
	FROM Security.[User] AS u
	INNER JOIN Security.Person AS per ON per.Id = u.IdPerson

	CREATE NONCLUSTERED INDEX IX_Users_UserCode ON #Users(UserCode)

	-- Tabla temporal para admisiones
	SELECT
		ing.NUMINGRES,
		ing.CODCENATE,
		CEN.NOMCENATE,
		ing.IFECHAING,
		ing.ICAUSAING,
		ing.TIPOINGRE,
		ing.CODDIAEGR,
		UF.UFUCODIGO,
		UF.UFUDESCRI,
		P.IPFECNACI,
		P.IPTIPODOC,
		P.IPPRINOMB,
		P.IPSEGNOMB,
		P.IPPRIAPEL,
		P.IPSEGAPEL,
		P.IPSEXOPAC,
		P.IPNOMCOMP,
		ing.IPCODPACI
	INTO #Admissions
	FROM dbo.ADINGRESO AS ing
	INNER JOIN dbo.ADCENATEN AS CEN ON CEN.CODCENATE = ing.CODCENATE
	INNER JOIN dbo.INUNIFUNC AS UF ON UF.UFUCODIGO = ing.UFUCODIGO
	INNER JOIN dbo.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = ing.IPCODPACI

	CREATE NONCLUSTERED INDEX IX_Admissions_NUMINGRES ON #Admissions(NUMINGRES)
	CREATE NONCLUSTERED INDEX IX_Admissions_CODCENATE ON #Admissions(CODCENATE)

	-- Tabla temporal para administradoras de salud
	SELECT
		ea.Id,
		ea.Code,
		ea.Name,
		tHA.Nit,
		tHA.[Name] AS NameThird,
		tHA.Id AS IdThirdParty
	INTO #HealthAdministrators
	FROM Contract.HealthAdministrator AS ea
	INNER JOIN Common.ThirdParty tHA WITH (NOLOCK) ON tHA.Id = ea.ThirdPartyId

	CREATE NONCLUSTERED INDEX IX_Health_Id ON #HealthAdministrators(Id)

	-- Tabla temporal para facturas básicas copago
	SELECT 
		i.Id,
		i.DocumentType,
		i.InvoiceNumber,
		i.RevenueControlDetailId,
		ad.AdmissionNumber,
		i.HealthAdministratorId,
		i.ThirdPartyId,
		i.PatientCode,
		i.CareGroupId,
		i.InvoiceDate,
		i.TotalInvoice,
		i.ThirdPartySalesValue,
		i.TotalPatientSalesPrice,
		i.Observation,
		i.IsCutAccount,
		i.Status,
		i.InvoicedUser,
		i.ReversalReasonId,
		i.AnnulmentUser,
		i.AnnulmentDate,	
		i.TotalValue,
		i.CurrencyId	
	INTO #BasicBillingCopay
	FROM Billing.InvoiceCopay ic
	JOIN Billing.BasicBilling bb ON bb.Id = ic.BasicBillingId
	JOIN Billing.Invoice i ON i.Id = bb.InvoiceId
	JOIN (SELECT id.Id, id.AdmissionNumber FROM Billing.Invoice id) ad ON ad.id = ic.invoiceId
	WHERE @includeCopay = 1

	CREATE NONCLUSTERED INDEX IX_BasicBillingCopay_Id ON #BasicBillingCopay(Id)

	-- Tabla temporal para facturas básicas
	SELECT 
		i.Id,
		i.DocumentType,
		i.InvoiceNumber,
		i.RevenueControlDetailId,
		i.AdmissionNumber,
		i.HealthAdministratorId,
		i.ThirdPartyId,
		i.PatientCode,
		i.CareGroupId,
		i.InvoiceDate,
		i.TotalInvoice,
		i.ThirdPartySalesValue,
		i.TotalPatientSalesPrice,
		i.Observation,
		i.IsCutAccount,
		i.Status,
		i.InvoicedUser,
		i.ReversalReasonId,
		i.AnnulmentUser,
		i.AnnulmentDate,	
		i.TotalValue,
		i.CurrencyId
	INTO #InvoiceBasicBilling
	FROM Billing.Invoice i
	INNER JOIN Billing.BasicBilling bb ON bb.InvoiceId = i.Id
	WHERE i.DocumentType = IIF(@BasicBilling = 1, 6, 0) AND bb.ThirdPartyEntityCopayId IS NULL

	CREATE NONCLUSTERED INDEX IX_InvoiceBasicBilling_Id ON #InvoiceBasicBilling(Id)

	-----------------------------RESULTADOS-----------------------
	--Me retorna todos los facturas diferentes de factura basica y copago

	SELECT
		F.Id
	   ,ing.CODCENATE CareCenterCode
	   ,ing.NOMCENATE AS CareCenterName
	   ,ing.CODCENATE + ' - ' + ing.NOMCENATE CareCenterDescription
	   ,hc.Nit ThirdPartyNit
	   ,hc.Name ThirdPartyName
	   ,hc.Nit + ' - ' + hc.Name ThirdPartyDescription
	   ,hc.Code HealthAdministratorCode
	   ,hc.Name HealthAdministratorName
	   ,hc.Code + ' - ' + hc.Name HealthAdministratorDescription
	   ,ga.Code CareGroupCode
	   ,ga.Name CareGroupName
	   ,ga.Code + ' - ' + ga.Name CareGroupDescription
	   ,F.Status StatusInvoice
	   ,IIF(F.Status = 1, 'Facturado', 'Anulado') StatusDescription
	   ,F.DocumentType
	   ,CASE F.DocumentType
			WHEN '1' THEN 'Factura EAPB con Contrato'
			WHEN '2' THEN 'Factura EAPB Sin Contrato'
			WHEN '3' THEN 'Factura Particular'
			WHEN '4' THEN 'Factura Capitada '
			WHEN '5' THEN 'Control de Capitacion'
			WHEN '6' THEN 'Factura Basica'
			WHEN '7' THEN 'Factura de Venta de Productos'
		END AS DocumentTypeDescription
	   ,F.InvoiceNumber
	   ,F.AdmissionNumber
	   ,ing.IFECHAING AdmissionDate
	   ,CASE ing.ICAUSAING
			WHEN '1' THEN 'Heridos en Combate'
			WHEN '2' THEN 'Enfermedad Profesional'
			WHEN '3' THEN 'Enfermedad General Adulto'
			WHEN '4' THEN 'Enfermedad General Pediatria'
			WHEN '5' THEN 'Odontología'
			WHEN '6' THEN 'Accidente Transito'
			WHEN '7' THEN 'Catastrofe/Fisalud'
			WHEN '8' THEN 'Quemados'
			WHEN '9' THEN 'Maternidad'
			WHEN '10' THEN 'Accidente Laboral'
			WHEN '11' THEN 'Cirugia Programada'
		END CauseIncomeDescription
	   ,CASE ing.TIPOINGRE
			WHEN '1' THEN 'Ambulatorio'
			WHEN '2' THEN 'Hospitalario'
		END AS AdmissionTypeDescription
	   ,F.PatientCode
	   ,CASE ing.IPSEXOPAC
			WHEN '1' THEN 'Hombre'
			ELSE 'Mujer'
		END SexDescription
		,ISNULL(IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.TotalInvoice, c.Id, @CurrencyReportId, @DateEnd)
			, F.TotalInvoice), 0) AS TotalInvoice
	   ,F.InvoiceDate
	   ,ISNULL(IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.TotalInvoice, c.Id, @CurrencyReportId, @DateEnd)
			, F.TotalInvoice), 0) AS TotalValue
	   ,RTRIM(LTRIM(ing.UFUCODIGO)) FunctionalUnitCode
	   ,ing.UFUDESCRI FunctionalUnitName
	   ,salida.FECALTPAC HighMedicalDate
	   ,ing.CODDIAEGR DiagnosticCode
	   ,CASE F.IsCutAccount
			WHEN 'True' THEN 'Si'
			ELSE 'No'
		END IsCutAccountDescription
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
			Common.CurrencyConverterWithDate(F.TotalPatientSalesPrice, c.Id, @CurrencyReportId, @DateEnd)
			, F.TotalPatientSalesPrice) AS ValueCopay 
	   ,us.UserCode
	   ,us.Fullname UserName
	   ,us.UserCode + ' - ' + us.Fullname UserDescription
	   ,hc.IdThirdParty ThirdPartyId
	   ,hc.Id HealthAdministratorId
	   ,ga.Id CareGroupId
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.ThirdPartySalesValue, c.Id, @CurrencyReportId, @DateEnd)
			, F.ThirdPartySalesValue) AS EntityValue 
	   ,ing.IPNOMCOMP PatientName
	   ,ing.IPCODPACI + ' - ' + ing.IPNOMCOMP PatientDescription
	   ,ing.IPFECNACI BirthDate
	   ,ti.NOMBRE IdentificationTypeDescription
	   ,ing.IPPRINOMB FirstName
	   ,ing.IPSEGNOMB SecondName
	   ,ing.IPPRIAPEL FirstLastName
	   ,ing.IPSEGAPEL SecondLastName
	   ,(CAST(DATEDIFF(dd, ing.IPFECNACI, GETDATE()) / 365.25 AS INT)) PatientAge
	   ,F.AnnulmentUser + ISNULL(' - ' + usAn.Fullname, '') AnnulmentUser
	   ,F.AnnulmentDate
	   ,brr.Code + ' - ' + brr.Name ReversalReasonDescription
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				@CurrencyReportId 
			, c.Id) AS CurrencyId
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
			 @CurrencyReportAbreviation 
		, c.Abbreviation) AS Abbreviation
		,c.Name AS CurrencyName
		,ISNULL(ic.Name, '') AS InvoiceCategories
	FROM Billing.Invoice AS F WITH (NOLOCK)
	LEFT JOIN Billing.InvoiceCategories ic ON ic.Id = F.InvoiceCategoryId
	INNER JOIN Common.Currency c ON c.Id = COALESCE(F.CurrencyId, @ToCurrency)
	LEFT JOIN #Users us ON us.UserCode = F.InvoicedUser 
	INNER JOIN Common.ThirdParty AS t WITH (NOLOCK) ON t.Id = F.ThirdPartyId
	LEFT JOIN #Admissions ing ON ing.NUMINGRES = F.AdmissionNumber
	LEFT JOIN Contract.CareGroup AS ga WITH (NOLOCK) ON ga.Id = F.CareGroupId
	LEFT JOIN #HealthAdministrators hc ON hc.Id = F.HealthAdministratorId
	LEFT JOIN Billing.BillingReversalReason brr WITH (NOLOCK) ON brr.Id = F.ReversalReasonId
	LEFT JOIN dbo.HCREGEGRE AS salida WITH (NOLOCK) ON salida.NUMINGRES = F.AdmissionNumber AND salida.IPCODPACI = F.PatientCode
	LEFT JOIN #Users usAn ON usAn.UserCode = F.AnnulmentUser
	LEFT JOIN ADTIPOIDENTIFICA ti WITH (NOLOCK) ON ti.CODIGO = ing.IPTIPODOC
	LEFT JOIN #Table_InvoiceType tip ON F.DocumentType = tip.Id	
	LEFT JOIN #Table_ThirdParty ttp ON F.ThirdPartyId = ttp.Id
	LEFT JOIN #Table_Entity tet ON hc.Id = tet.Id
	LEFT JOIN #Table_CareGroup tcg ON ga.Id = tcg.Id
	LEFT JOIN #Table_Users tus ON us.UserCode = tus.userCode
	LEFT JOIN #Table_CareCenter tcc ON ing.CODCENATE = tcc.Code
	WHERE F.InvoiceDate >= @DateStart
		AND F.InvoiceDate <= @DateEnd  
		AND (@FilterByInvoiceType = 0 OR tip.Id IS NOT NULL)
		AND (F.Status IN (@Status) OR @Status = '')
		AND (@FilterByCareCenter = 0 OR tcc.Code IS NOT NULL)
		AND (@FilterByThirdParty = 0 OR ttp.Id IS NOT NULL)
		AND (@FilterByEntity = 0 OR tet.Id IS NOT NULL)
		AND (@FilterByCareGroup = 0 OR tcg.Id IS NOT NULL)
		AND (@FilterByUsers = 0 OR tus.userCode IS NOT NULL)
		AND (
			  (tip.Id = 4 AND (F.CurrencyId IS NULL OR F.CurrencyId = @ToCurrency))
			  OR (tip.Id != 4 AND F.CurrencyId = @ToCurrency)
			)

	UNION ALL
	---Me retorna facturas básicas copago y cuotas moderadoras
	SELECT
		F.Id
	   ,ing.CODCENATE CareCenterCode
	   ,ing.NOMCENATE AS CareCenterName
	   ,ing.CODCENATE + ' - ' + ing.NOMCENATE CareCenterDescription
	   ,hc.Nit ThirdPartyNit
	   ,hc.Name ThirdPartyName
	   ,hc.Nit + ' - ' + hc.Name ThirdPartyDescription
	   ,hc.Code HealthAdministratorCode
	   ,hc.Name HealthAdministratorName
	   ,hc.Code + ' - ' + hc.Name HealthAdministratorDescription
	   ,ga.Code CareGroupCode
	   ,ga.Name CareGroupName
	   ,ga.Code + ' - ' + ga.Name CareGroupDescription
	   ,F.Status StatusInvoice
	   ,IIF(F.Status = 1, 'Facturado', 'Anulado') StatusDescription
	   ,F.DocumentType
	   ,CASE F.DocumentType			
			WHEN '6' THEN 'Factura Básica Copago y Cuotas moderadoras'			
		END AS DocumentTypeDescription
	   ,F.InvoiceNumber
	   ,F.AdmissionNumber
	   ,ing.IFECHAING AdmissionDate
	   ,CASE ing.ICAUSAING
			WHEN '1' THEN 'Heridos en Combate'
			WHEN '2' THEN 'Enfermedad Profesional'
			WHEN '3' THEN 'Enfermedad General Adulto'
			WHEN '4' THEN 'Enfermedad General Pediatria'
			WHEN '5' THEN 'Odontología'
			WHEN '6' THEN 'Accidente Transito'
			WHEN '7' THEN 'Catastrofe/Fisalud'
			WHEN '8' THEN 'Quemados'
			WHEN '9' THEN 'Maternidad'
			WHEN '10' THEN 'Accidente Laboral'
			WHEN '11' THEN 'Cirugia Programada'
		END CauseIncomeDescription
	   ,CASE ing.TIPOINGRE
			WHEN '1' THEN 'Ambulatorio'
			WHEN '2' THEN 'Hospitalario'
		END AS AdmissionTypeDescription
	   ,F.PatientCode
	   ,CASE ing.IPSEXOPAC
			WHEN '1' THEN 'Hombre'
			ELSE 'Mujer'
		END SexDescription
		,ISNULL(IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.TotalInvoice, F.CurrencyId, @CurrencyReportId, @DateEnd)
			, F.TotalInvoice), 0) AS TotalInvoice
	   ,F.InvoiceDate
	   ,ISNULL(IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.TotalInvoice, F.CurrencyId, @CurrencyReportId, @DateEnd)
			, F.TotalInvoice), 0) AS TotalValue
	   ,RTRIM(LTRIM(ing.UFUCODIGO)) FunctionalUnitCode
	   ,ing.UFUDESCRI FunctionalUnitName
	   ,salida.FECALTPAC HighMedicalDate
	   ,ing.CODDIAEGR DiagnosticCode
	   ,CASE F.IsCutAccount
			WHEN 'True' THEN 'Si'
			ELSE 'No'
		END IsCutAccountDescription
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
			Common.CurrencyConverterWithDate(F.TotalPatientSalesPrice, F.CurrencyId, @CurrencyReportId, @DateEnd)
			, F.TotalPatientSalesPrice) AS ValueCopay 
	   ,us.UserCode
	   ,us.Fullname UserName
	   ,us.UserCode + ' - ' + us.Fullname UserDescription
	   ,hc.IdThirdParty ThirdPartyId
	   ,hc.Id HealthAdministratorId
	   ,ga.Id CareGroupId
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.ThirdPartySalesValue, F.CurrencyId, @CurrencyReportId, @DateEnd)
			, F.ThirdPartySalesValue) AS EntityValue 
	   ,ing.IPNOMCOMP PatientName
	   ,ing.IPCODPACI + ' - ' + ing.IPNOMCOMP PatientDescription
	   ,ing.IPFECNACI BirthDate
	   ,ti.NOMBRE IdentificationTypeDescription
	   ,ing.IPPRINOMB FirstName
	   ,ing.IPSEGNOMB SecondName
	   ,ing.IPPRIAPEL FirstLastName
	   ,ing.IPSEGAPEL SecondLastName
	   ,(CAST(DATEDIFF(dd, ing.IPFECNACI, GETDATE()) / 365.25 AS INT)) PatientAge
	   ,F.AnnulmentUser + ISNULL(' - ' + usAn.Fullname, '') AnnulmentUser
	   ,F.AnnulmentDate
	   ,brr.Code + ' - ' + brr.Name ReversalReasonDescription
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				@CurrencyReportId 
			, F.CurrencyId) AS CurrencyId
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
			 @CurrencyReportAbreviation 
		, c.Abbreviation) AS Abbreviation
		,c.Name AS CurrencyName
		,'' AS InvoiceCategories
	FROM #BasicBillingCopay AS F WITH (NOLOCK)
	INNER JOIN Common.Currency c ON c.Id = COALESCE(F.CurrencyId, @ToCurrency)
	LEFT JOIN #Users us ON us.UserCode = F.InvoicedUser 
	INNER JOIN Common.ThirdParty AS t WITH (NOLOCK) ON t.Id = F.ThirdPartyId
	LEFT JOIN #Admissions ing ON ing.NUMINGRES = F.AdmissionNumber
	LEFT JOIN Contract.CareGroup AS ga WITH (NOLOCK) ON ga.Id = F.CareGroupId
	LEFT JOIN #HealthAdministrators hc ON hc.Id = F.HealthAdministratorId
	LEFT JOIN Billing.BillingReversalReason brr WITH (NOLOCK) ON brr.Id = F.ReversalReasonId
	LEFT JOIN dbo.HCREGEGRE AS salida WITH (NOLOCK) ON salida.NUMINGRES = F.AdmissionNumber AND salida.IPCODPACI = F.PatientCode
	LEFT JOIN #Users usAn ON usAn.UserCode = F.AnnulmentUser
	LEFT JOIN ADTIPOIDENTIFICA ti WITH (NOLOCK) ON ti.CODIGO = ing.IPTIPODOC
	LEFT JOIN #Table_InvoiceType tip ON F.DocumentType = tip.Id	
	LEFT JOIN #Table_ThirdParty ttp ON F.ThirdPartyId = ttp.Id
	LEFT JOIN #Table_Entity tet ON hc.Id = tet.Id
	LEFT JOIN #Table_CareGroup tcg ON ga.Id = tcg.Id
	LEFT JOIN #Table_Users tus ON us.UserCode = tus.userCode
	LEFT JOIN #Table_CareCenter tcc ON ing.CODCENATE = tcc.Code
	WHERE F.InvoiceDate >= @DateStart
		AND F.InvoiceDate <= @DateEnd
		AND (F.Status IN (@Status) OR @Status = '')
		AND (@FilterByCareCenter IS NULL OR @FilterByCareCenter = 0)
		AND (@FilterByThirdParty = 0 OR ttp.Id IS NOT NULL)
		AND (@FilterByEntity IS NOT NULL OR @FilterByEntity = 0)
		AND (@FilterByCareGroup IS NOT NULL OR @FilterByCareGroup = 0)
		AND (@FilterByUsers = 0 OR tus.userCode IS NOT NULL)
		AND F.CurrencyId = @ToCurrency 

	UNION ALL 
	--Me retorna facturas basicas 
	SELECT
		F.Id
	   ,NULL CareCenterCode
	   ,NULL CareCenterName
	   ,NULL CareCenterDescription
	   ,NULL ThirdPartyNit
	   ,NULL ThirdPartyName
	   ,NULL ThirdPartyDescription
	   ,NULL HealthAdministratorCode
	   ,NULL HealthAdministratorName
	   ,NULL HealthAdministratorDescription
	   ,NULL CareGroupCode
	   ,NULL CareGroupName
	   ,NULL CareGroupDescription
	   ,F.Status StatusInvoice
	   ,IIF(F.Status = 1, 'Facturado', 'Anulado') StatusDescription
	   ,F.DocumentType
	   ,CASE F.DocumentType			
			WHEN '6' THEN 'Factura Basica'
		END AS DocumentTypeDescription
	   ,F.InvoiceNumber
	   ,F.AdmissionNumber
	   ,NULL AdmissionDate
	   ,NULL CauseIncomeDescription
	   ,NULL AdmissionTypeDescription
	   ,F.PatientCode
	   ,CASE P.IPSEXOPAC
			WHEN '1' THEN 'Hombre'
			ELSE 'Mujer'
		END SexDescription
		,ISNULL(IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.TotalInvoice, F.CurrencyId, @CurrencyReportId, @DateEnd)
			, F.TotalInvoice), 0) AS TotalInvoice
	   ,F.InvoiceDate
	   ,ISNULL(IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.TotalInvoice, F.CurrencyId, @CurrencyReportId, @DateEnd)
			, F.TotalInvoice), 0) AS TotalValue
	   ,NULL FunctionalUnitCode
	   ,NULL FunctionalUnitName
	   ,NULL HighMedicalDate
	   ,NULL DiagnosticCode
	   ,CASE F.IsCutAccount
			WHEN 'True' THEN 'Si'
			ELSE 'No'
		END IsCutAccountDescription
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
			Common.CurrencyConverterWithDate(F.TotalPatientSalesPrice, F.CurrencyId, @CurrencyReportId, @DateEnd)
			, F.TotalPatientSalesPrice) AS ValueCopay 
	   ,us.UserCode
	   ,us.Fullname UserName
	   ,us.UserCode + ' - ' + us.Fullname UserDescription
	   ,NULL ThirdPartyId
	   ,NULL HealthAdministratorId
	   ,NULL CareGroupId
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				Common.CurrencyConverterWithDate(F.ThirdPartySalesValue, F.CurrencyId, @CurrencyReportId, @DateEnd)
			, F.ThirdPartySalesValue) AS EntityValue 
	   ,P.IPNOMCOMP PatientName
	   ,P.IPCODPACI + ' - ' + P.IPNOMCOMP PatientDescription
	   ,P.IPFECNACI BirthDate
	   ,ti.NOMBRE IdentificationTypeDescription
	   ,P.IPPRINOMB FirstName
	   ,P.IPSEGNOMB SecondName
	   ,P.IPPRIAPEL FirstLastName
	   ,P.IPSEGAPEL SecondLastName
	   ,(CAST(DATEDIFF(dd, P.IPFECNACI, GETDATE()) / 365.25 AS INT)) PatientAge
	   ,F.AnnulmentUser + ISNULL(' - ' + usAn.Fullname, '') AnnulmentUser
	   ,F.AnnulmentDate
	   ,brr.Code + ' - ' + brr.Name ReversalReasonDescription
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
				@CurrencyReportId 
			, F.CurrencyId) AS CurrencyId
		,IIF(@CurrencyReportId IS NOT NULL AND @CurrencyReportAbreviation IS NOT NULL,
			 @CurrencyReportAbreviation 
		, c.Abbreviation) AS Abbreviation
		,c.Name AS CurrencyName
		,'' AS InvoiceCategories
	FROM #InvoiceBasicBilling AS F WITH (NOLOCK)
	JOIN Common.Currency c ON c.Id = COALESCE(F.CurrencyId, @ToCurrency)
	LEFT JOIN #Users us ON us.UserCode = F.InvoicedUser
	INNER JOIN Common.ThirdParty AS t WITH (NOLOCK) ON t.Id = F.ThirdPartyId
	LEFT JOIN dbo.INPACIENT AS P ON t.Nit = P.IPCODPACI
	LEFT JOIN Billing.BillingReversalReason brr WITH (NOLOCK) ON brr.Id = F.ReversalReasonId
	LEFT JOIN #Users usAn ON usAn.UserCode = F.AnnulmentUser
	LEFT JOIN ADTIPOIDENTIFICA ti WITH (NOLOCK) ON ti.CODIGO = P.IPTIPODOC
	LEFT JOIN #Table_ThirdParty ttp ON F.ThirdPartyId = ttp.Id
	LEFT JOIN #Table_Users tus ON us.UserCode = tus.userCode
	WHERE F.InvoiceDate >= @DateStart
		AND F.InvoiceDate <= @DateEnd
		AND (F.Status IN (@Status) OR @Status = '')
		AND (@FilterByCareCenter IS NULL OR @FilterByCareCenter = 0)
		AND (@FilterByThirdParty = 0 OR ttp.Id IS NOT NULL)
		AND (@FilterByEntity IS NOT NULL OR @FilterByEntity = 0)
		AND (@FilterByCareGroup IS NOT NULL OR @FilterByCareGroup = 0)
		AND (@FilterByUsers = 0 OR tus.userCode IS NOT NULL)
		AND F.CurrencyId = @ToCurrency 

	-- =====================================================
	-- LIMPIEZA DE TABLAS TEMPORALES
	-- =====================================================
	DROP TABLE IF EXISTS #Table_InvoiceType
	DROP TABLE IF EXISTS #Table_ThirdParty
	DROP TABLE IF EXISTS #Table_Entity
	DROP TABLE IF EXISTS #Table_CareGroup
	DROP TABLE IF EXISTS #Table_Users
	DROP TABLE IF EXISTS #Table_CareCenter
	DROP TABLE IF EXISTS #Users
	DROP TABLE IF EXISTS #Admissions
	DROP TABLE IF EXISTS #HealthAdministrators
	DROP TABLE IF EXISTS #BasicBillingCopay
	DROP TABLE IF EXISTS #InvoiceBasicBilling

END TRY
BEGIN CATCH
	-- Limpieza en caso de error
	DROP TABLE IF EXISTS #Table_InvoiceType
	DROP TABLE IF EXISTS #Table_ThirdParty
	DROP TABLE IF EXISTS #Table_Entity
	DROP TABLE IF EXISTS #Table_CareGroup
	DROP TABLE IF EXISTS #Table_Users
	DROP TABLE IF EXISTS #Table_CareCenter
	DROP TABLE IF EXISTS #Users
	DROP TABLE IF EXISTS #Admissions
	DROP TABLE IF EXISTS #HealthAdministrators
	DROP TABLE IF EXISTS #BasicBillingCopay
	DROP TABLE IF EXISTS #InvoiceBasicBilling

	SELECT CONCAT('Se presentó un error al crear el reporte: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
END CATCH
END
