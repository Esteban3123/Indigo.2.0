-- ================================================================================================
-- Author:		
-- Create date: 
-- Description:	Sp que cuenta la cantidad de registros que retornaría el reporte estadístico
--              de facturación, para validar si supera el límite de 250,000 registros
-- ================================================================================================
CREATE PROCEDURE [Billing].[SP_ReportBillingStadistics_Count]

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
		   -- Variables para COUNT progresivo
		   ,@Count1 INT = 0
		   ,@Count2 INT = 0
		   ,@Count3 INT = 0
		   ,@TotalCount INT = 0
		   ,@MaxRecords INT = 250000
		   ,@IsValid BIT = 1

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
	-- TABLAS TEMPORALES PARA FILTROS COMPLEJOS
	-- =====================================================
	
	-- Tabla temporal para usuarios
	SELECT
		u.UserCode,
		per.Fullname
	INTO #Users
	FROM Security.[User] AS u
	INNER JOIN Security.Person AS per ON per.Id = u.IdPerson

	CREATE NONCLUSTERED INDEX IX_Users_UserCode ON #Users(UserCode)

	-- Tabla temporal para admisiones (solo campos necesarios para filtros)
	SELECT
		ing.NUMINGRES,
		ing.CODCENATE
	INTO #Admissions
	FROM dbo.ADINGRESO AS ing
	INNER JOIN dbo.ADCENATEN AS CEN ON CEN.CODCENATE = ing.CODCENATE

	CREATE NONCLUSTERED INDEX IX_Admissions_NUMINGRES ON #Admissions(NUMINGRES)
	CREATE NONCLUSTERED INDEX IX_Admissions_CODCENATE ON #Admissions(CODCENATE)

	-- Tabla temporal para administradoras de salud
	SELECT
		ea.Id
	INTO #HealthAdministrators
	FROM Contract.HealthAdministrator AS ea

	CREATE NONCLUSTERED INDEX IX_Health_Id ON #HealthAdministrators(Id)

	-- Tabla temporal para facturas básicas copago
	SELECT 
		i.Id,
		i.DocumentType,
		ad.AdmissionNumber,
		i.HealthAdministratorId,
		i.ThirdPartyId,
		i.CareGroupId,
		i.InvoiceDate,
		i.Status,
		i.InvoicedUser,
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
		i.ThirdPartyId,
		i.InvoiceDate,
		i.Status,
		i.InvoicedUser,
		i.CurrencyId
	INTO #InvoiceBasicBilling
	FROM Billing.Invoice i
	INNER JOIN Billing.BasicBilling bb ON bb.InvoiceId = i.Id
	WHERE i.DocumentType = IIF(@BasicBilling = 1, 6, 0) AND bb.ThirdPartyEntityCopayId IS NULL

	CREATE NONCLUSTERED INDEX IX_InvoiceBasicBilling_Id ON #InvoiceBasicBilling(Id)

	-- =====================================================
	-- COUNT 1: Facturas Normales
	-- =====================================================
	SELECT @Count1 = COUNT(1)
	FROM Billing.Invoice AS F WITH (NOLOCK)
	LEFT JOIN #Admissions ing ON ing.NUMINGRES = F.AdmissionNumber
	LEFT JOIN #HealthAdministrators hc ON hc.Id = F.HealthAdministratorId
	LEFT JOIN #Users us ON us.UserCode = F.InvoicedUser
	LEFT JOIN Contract.CareGroup AS ga WITH (NOLOCK) ON ga.Id = F.CareGroupId
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

	-- =====================================================
	-- COUNT 2: Facturas Básicas Copago (si aplica)
	-- =====================================================
	IF @includeCopay = 1
	BEGIN
		SELECT @Count2 = COUNT(1)
		FROM #BasicBillingCopay AS F
		LEFT JOIN #Admissions ing ON ing.NUMINGRES = F.AdmissionNumber
		LEFT JOIN #HealthAdministrators hc ON hc.Id = F.HealthAdministratorId
		LEFT JOIN #Users us ON us.UserCode = F.InvoicedUser
		LEFT JOIN Contract.CareGroup AS ga WITH (NOLOCK) ON ga.Id = F.CareGroupId
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
	END

	-- =====================================================
	-- COUNT 3: Facturas Básicas (si aplica)
	-- =====================================================
	IF @BasicBilling = 1
	BEGIN
		SELECT @Count3 = COUNT(1)
		FROM #InvoiceBasicBilling AS F
		LEFT JOIN #Users us ON us.UserCode = F.InvoicedUser
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
	END

	-- =====================================================
	-- CALCULAR TOTAL Y RETORNAR RESULTADO
	-- =====================================================
	SET @TotalCount = @Count1 + @Count2 + @Count3
	IF  @TotalCount > @MaxRecords BEGIN
		SET @IsValid = 0
	END

	SELECT 
		@TotalCount AS TotalRecords,
		@MaxRecords AS MaxAllowedRecords,
		@IsValid AS IsValid,
		CASE WHEN @TotalCount > @MaxRecords 
			THEN CONCAT('La consulta retornaría ', FORMAT(@TotalCount, 'N0'), ' registros, superando el límite de ', FORMAT(@MaxRecords, 'N0'), '. Por favor ajuste los filtros.')
			ELSE CONCAT('La consulta retornará ', FORMAT(@TotalCount, 'N0'), ' registros.')
		END AS Message

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

	SELECT 
		0 AS TotalRecords,
		@MaxRecords AS MaxAllowedRecords,
		0 AS IsValid,
		CONCAT('Se presentó un error al contar los registros: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS Message
END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que cuenta el número total de registros que devolvería el reporte estadístico de facturación antes de ejecutarlo, con el fin de validar si supera el límite permitido de 250.000 registros. Recibe criterios de búsqueda (rango de fechas, tipo de reporte, tipo de factura, estado, moneda y centro de atención) y filtros adicionales (tercero pagador, entidad, grupo de atención, usuarios y sede) en formato XML. Compone tablas temporales con admisiones de pacientes, administradoras de salud (EPS/pagadores), usuarios del sistema y copagos de facturas para aplicar todos los filtros seleccionados y retornar un conteo acumulado que le indica a la interfaz si el reporte es viable o debe restringirse. Toca las entidades de facturación, ingresos/admisiones, contratos con administradoras y configuración de moneda oficial de la compañía.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBillingStadistics_Count';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBillingStadistics_Count';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula previamente cuántos registros retornaría el reporte estadístico de facturación (facturas normales + básicas con copago + básicas) y valida si supera el tope de 250.000 antes de ejecutar el reporte completo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XmlCriterias debe contener el nodo /Data con DateStart, DateEnd, ReportType, GroupBy, InvoiceType, Status, CareCenter y CurrencyReport.; @XmlFilters debe contener el nodo /Data con ThirdParty, Entity, CareGroup y Users (pueden ir vacíos).; Las cadenas de filtros multivaluados deben venir separadas por coma para ser procesadas por dbo.Split.; Debe existir un registro en GeneralLedger.CompanySettings con OfficialCurrencyId.; El ToCurrency recibido debe existir en Common.Currency para resolver Id y Abreviación; de lo contrario quedan en NULL.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El límite máximo permitido para el reporte estadístico de facturación es 250.000 registros (constante interna).; El código 8 dentro de InvoiceType siempre se interpreta como marcador para incluir copagos, no como un DocumentType real a filtrar.; El código 6 dentro de InvoiceType siempre se interpreta como marcador para incluir facturación básica, no como un DocumentType a filtrar directamente.; Todas las tablas temporales se eliminan tanto en flujo exitoso como en error (garantía de limpieza).; El conteo siempre filtra por rango [DateStart, DateEnd] sobre InvoiceDate.; El conteo siempre exige coincidencia con la moneda de reporte (@ToCurrency), salvo el caso especial de tipo 4 en Count1 donde se admite CurrencyId NULL.; El procedimiento nunca modifica datos persistentes; sólo lee y devuelve un result set agregado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Facturación básica; Copago; Tipo de documento (DocumentType); Administradora de salud (EPS); Tercero pagador; Grupo de atención (CareGroup); Centro de atención; Admisión/Ingreso del paciente; Moneda oficial / moneda de reporte; Usuario facturador', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve siempre una fila con TotalRecords, MaxAllowedRecords (250000), IsValid y Message; IsValid=0 cuando @TotalCount > 250000.; [RETURN_RESULT] RESULT_SET: Si ocurre error, el CATCH devuelve TotalRecords=0, IsValid=0 y un Message con el detalle del error y la línea.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El filtro @InvoiceType contiene el código 8 → Activa @includeCopay=1 y elimina el código 8 del set de tipos de factura, habilitando el conteo de facturas básicas con copago (Count2) else @includeCopay=0 y no se ejecuta Count2; si El filtro @InvoiceType contiene el código 6 → Activa @BasicBilling=1 y elimina el código 6 del set, habilitando el conteo de facturas básicas (Count3) else @BasicBilling=0 y no se ejecuta Count3; si @includeCopay = 1 → Carga #BasicBillingCopay uniendo InvoiceCopay + BasicBilling + Invoice y ejecuta Count2 sobre dicha temporal; si @BasicBilling = 1 → Carga #InvoiceBasicBilling con facturas cuyo DocumentType=6 y BasicBilling.ThirdPartyEntityCopayId IS NULL, y ejecuta Count3; si @TotalCount (Count1+Count2+Count3) > 250000 → Devuelve IsValid=0 y un mensaje indicando que la consulta supera el límite y se deben ajustar filtros else Devuelve IsValid=1 con mensaje indicando la cantidad de registros que retornará; si Para Count1, tip.Id = 4 (tipo de factura 4) → Permite que F.CurrencyId sea NULL o igual a la moneda destino else Para otros tipos exige F.CurrencyId = @ToCurrency estricto; si Ocurre una excepción dentro del TRY → El CATCH limpia todas las temporales y devuelve TotalRecords=0, IsValid=0 y mensaje con ERROR_MESSAGE() y ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.Currency; Security.User; Security.Person; dbo.ADINGRESO; dbo.ADCENATEN; Contract.HealthAdministrator; Billing.InvoiceCopay; Billing.BasicBilling; Billing.Invoice; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStadistics_Count';
-- GO
