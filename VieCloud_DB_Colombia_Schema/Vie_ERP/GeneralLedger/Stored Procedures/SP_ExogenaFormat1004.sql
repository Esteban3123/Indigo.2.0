-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1004
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1004]
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
			CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS AmountPaid,
			CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS DiscountValue
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
		AND v.[Format] = 1004
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte tributario de información exógena en el Formato 1004 para la DIAN, correspondiente a un año fiscal específico. Consolida los valores pagados y descuentos aplicados a terceros (proveedores, contratistas, personas naturales o jurídicas) agrupados por concepto tributario, traduciendo los tipos de identificación internos del sistema a los códigos oficiales DIAN (cédula de ciudadanía, NIT, pasaporte, entre otros). Para cada tercero, integra datos de identificación desde ThirdParty y Person, la dirección más reciente vía ViewLatestAddressByPerson con sus catálogos de ciudad, departamento y país, y el correo electrónico vigente vía ViewLatestEmailByPerson, tomando los movimientos contables desde la vista ViewReportExogenousFormat filtrada por año, formato 1004 e identificador de exógena. El resultado se usa para construir el XML de reporte de información exógena que se presenta ante la autoridad tributaria colombiana.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1004';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1004';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de información exógena (Formato 1004) consolidando por concepto y tercero los valores pagados y descuentos para un año y formato exógeno específicos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos Year y ExogenousFormatId bajo /Data; Deben existir registros en ViewReportExogenousFormat con Format=1004 para el año y formato exógeno indicados', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros del Formato 1004; Los IdentificationType se traducen siempre a códigos DIAN (11,12,13,22,31,41,43); Cuando el tercero es persona jurídica (IdentificationType=7) nunca se exponen nombres/apellidos individuales; Cuando el tercero es persona natural nunca se expone DigitVerification ni BusinessName; Los montos AmountPaid y DiscountValue se entregan siempre como DECIMAL(18,0) sin decimales; Solo se considera la dirección y email más recientes por persona (vistas LatestAddress/LatestEmail)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena; Formato 1004 (DIAN); Tercero; Tipo de identificación; NIT; Dígito de verificación; Razón social; Persona natural/jurídica; Valor pagado; Valor de descuento; Ubicación geográfica (ciudad, departamento, país)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result set: Devuelve filas agrupadas por concepto y tercero con AmountPaid (suma de Value cuando ConceptType=1) y DiscountValue (suma de Value cuando ConceptType=2), filtradas por Year, ExogenousFormatId y Format=1004', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType = 3 → Mapea IdentificationType a 11; si p.IdentificationType = 2 → Mapea IdentificationType a 12; si p.IdentificationType = 0 → Mapea IdentificationType a 13; si p.IdentificationType = 1 → Mapea IdentificationType a 22; si p.IdentificationType = 7 → Mapea IdentificationType a 31 y trata al tercero como persona jurídica: expone DigitVerification y BusinessName, oculta nombres y apellidos else Expone nombres/apellidos de la persona natural y oculta DigitVerification y BusinessName; si p.IdentificationType = 4 → Mapea IdentificationType a 41; si Cualquier otro IdentificationType → Mapea IdentificationType a 43 (valor por defecto); si v.ConceptType = 1 → Suma el Value en AmountPaid else Aporta 0 a AmountPaid; si v.ConceptType = 2 → Suma el Value en DiscountValue else Aporta 0 a DiscountValue', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country; Common.ViewLatestEmailByPerson; Common.Email', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1004';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1004';
-- GO
