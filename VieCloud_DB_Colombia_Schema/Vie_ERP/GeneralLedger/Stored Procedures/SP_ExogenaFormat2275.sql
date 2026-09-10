-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 2275
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat2275]
   @xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearData INT,
			@ExogenousFormatId INT

	SELECT	@YearData = t.x.value('Year[1]','int'),
			@ExogenousFormatId = t.x.value('ExogenousFormatId[1]','int')
	FROM @xmlCriterias.nodes('/Data') t(x)

    SELECT	v.Concept,
			CASE p.IdentificationType
				WHEN 3 THEN 11
				WHEN 2 THEN 12
				WHEN 0 THEN 13
				WHEN 1 THEN 22
				WHEN 7 THEN 31
				WHEN 4 THEN 41
				ELSE 43
			END AS IdentificationType,
			tp.Nit AS IdentificationNumber,
			IIF(p.IdentificationType = 7, tp.DigitVerification, NULL) DigitVerification,
			IIF(p.IdentificationType = 7, NULL, p.FirstLastName) FirstLastName,
			IIF(p.IdentificationType = 7, NULL, p.SecondLastName) SecondLastName,
			IIF(p.IdentificationType = 7, NULL, p.FirstName) FirstName,
			IIF(p.IdentificationType = 7, NULL, p.SecondName) SecondName,
			IIF(p.IdentificationType = 7, tp.Name, NULL) AS BusinessName,
			a.Addresss AS Addresss,
			c.Code CityCode,
			d.Code DepartmentCode,
			ct.Code CodeCountry,
			e.Email,
			CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS IncomeValue,
			CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS ExemptIncomeValue
	FROM GeneralLedger.ViewReportExogenousFormat v
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON v.ThirdPartyId = tp.Id
	LEFT JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
	LEFT JOIN [Common].[ViewLatestAddressByPerson] va ON p.Id = va.PersonId
	LEFT JOIN Common.Address a WITH (NOLOCK) ON va.AddressId = a.Id
	LEFT JOIN Common.City c WITH (NOLOCK) ON a.CityId = c.Id
	LEFT JOIN Common.Department d WITH (NOLOCK) ON c.DepartamentId = d.Id
	LEFT JOIN Common.Country ct WITH (NOLOCK) ON d.CountryId = ct.Id
	LEFT JOIN [Common].[ViewLatestEmailByPerson] ve ON p.Id = ve.PersonId
	LEFT JOIN Common.Email e ON ve.EmailId = e.Id
	WHERE v.Year = @YearData
		AND v.ExogenousFormatId = @ExogenousFormatId
		AND v.[Format] = 2275
	GROUP BY v.Concept,
		p.IdentificationType,
		tp.Nit,
		IIF(p.IdentificationType = 7, tp.DigitVerification, NULL),
		IIF(p.IdentificationType = 7, NULL, p.FirstLastName),
		IIF(p.IdentificationType = 7, NULL, p.SecondLastName),
		IIF(p.IdentificationType = 7, NULL, p.FirstName),
		IIF(p.IdentificationType = 7, NULL, p.SecondName),
		IIF(p.IdentificationType = 7, tp.Name, NULL),
		a.Addresss, c.Code, d.Code, ct.Code,
		e.Email
	ORDER BY v.Concept, tp.Nit
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de información exógena tributaria en el Formato 2275 de la DIAN para un año fiscal y formato específico. Consolida los valores de ingresos gravados y exentos agrupados por concepto y tercero, combinando datos contables de la vista de formatos exógenos con la información de identificación del tercero (NIT o documento), nombre o razón social, dirección, ciudad, departamento, país y correo electrónico. Convierte los tipos de identificación internos del sistema a los códigos oficiales exigidos por la DIAN (cédula de ciudadanía, NIT, pasaporte, etc.) y distingue entre personas naturales y jurídicas para estructurar el XML requerido en la declaración de información exógena.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat2275';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat2275';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de información exógena tributaria Formato 2275 (ingresos no constitutivos de renta) consolidando datos de terceros, identificación, ubicación y valores por concepto para un año fiscal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2275';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId; Deben existir registros en ViewReportExogenousFormat con Format=2275 para el año y ExogenousFormatId indicados', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2275';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros con Format = 2275; Cuando el tercero es persona jurídica (IdentificationType=7) se reporta razón social y dígito de verificación, nunca nombres personales; Cuando el tercero no es jurídico (IdentificationType<>7) no se reporta DigitVerification ni BusinessName; Los valores monetarios se truncan/redondean a DECIMAL(18,0) (sin decimales); Se toma la dirección y email más recientes por persona (vía ViewLatestAddressByPerson y ViewLatestEmailByPerson); Los tipos de identificación se mapean a códigos DIAN (11,12,13,22,31,41,43); Resultados ordenados por concepto y NIT', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2275';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena tributaria; Formato 2275 DIAN; Tercero; Tipo de identificación (CC, NIT, etc.); Dígito de verificación; Razón social; Ingresos no constitutivos de renta; Ingresos exentos; Concepto contable; Dirección y email del tercero', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2275';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por concepto y tercero filtradas por Year=@YearData, ExogenousFormatId=@ExogenousFormatId y Format=2275, sumando valores como IncomeValue (ConceptType=1) y ExemptIncomeValue (ConceptType=2)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2275';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType = 3 → IdentificationType reportado = 11; si p.IdentificationType = 2 → IdentificationType reportado = 12; si p.IdentificationType = 0 → IdentificationType reportado = 13; si p.IdentificationType = 1 → IdentificationType reportado = 22; si p.IdentificationType = 7 → IdentificationType reportado = 31; se reporta DigitVerification y BusinessName (tp.Name); se omiten nombres y apellidos; si p.IdentificationType = 4 → IdentificationType reportado = 41; si Otro IdentificationType → IdentificationType reportado = 43 (default); si v.ConceptType = 1 → El valor se acumula en IncomeValue else Se acumula 0 en IncomeValue; si v.ConceptType = 2 → El valor se acumula en ExemptIncomeValue else Se acumula 0 en ExemptIncomeValue', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2275';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country; Common.ViewLatestEmailByPerson; Common.Email', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2275';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2275';
-- GO
