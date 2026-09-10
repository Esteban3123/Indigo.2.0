-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1056
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1056]
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
			CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentValue,
			CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS IVA,
			CAST(SUM(IIF(v.ConceptType = 3, v.Value, 0)) AS DECIMAL(18, 0)) AS ReteDeductible,
			CAST(SUM(IIF(v.ConceptType = 4, v.Value, 0)) AS DECIMAL(18, 0)) AS ReteNoDeductible,
			CAST(SUM(IIF(v.ConceptType = 5, v.Value, 0)) AS DECIMAL(18, 0)) AS ReteIVAComun,
			CAST(SUM(IIF(v.ConceptType = 6, v.Value, 0)) AS DECIMAL(18, 0)) AS ReteIVAExt
	FROM GeneralLedger.ViewReportExogenousFormat v
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON v.ThirdPartyId = tp.Id
	LEFT JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
	LEFT JOIN [Common].[ViewLatestAddressByPerson] va ON p.Id = va.PersonId
	LEFT JOIN Common.Address a WITH (NOLOCK) ON va.AddressId = a.Id
	LEFT JOIN Common.City c WITH (NOLOCK) ON a.CityId = c.Id
	LEFT JOIN Common.Department d WITH (NOLOCK) ON c.DepartamentId = d.Id
	LEFT JOIN Common.Country ct WITH (NOLOCK) ON d.CountryId = ct.Id
	WHERE v.Year = @YearData
		AND v.ExogenousFormatId = @ExogenousFormatId
		AND v.[Format] = 1056
	GROUP BY v.Concept,
		p.IdentificationType,
		tp.Nit,
		IIF(p.IdentificationType = 7, tp.DigitVerification, NULL),
		IIF(p.IdentificationType = 7, NULL, p.FirstLastName),
		IIF(p.IdentificationType = 7, NULL, p.SecondLastName),
		IIF(p.IdentificationType = 7, NULL, p.FirstName),
		IIF(p.IdentificationType = 7, NULL, p.SecondName),
		IIF(p.IdentificationType = 7, tp.Name, NULL),
		a.Addresss, c.Code, d.Code, ct.Code
	ORDER BY v.Concept, tp.Nit
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte tributario de información exógena en el Formato 1056 de la DIAN para un año fiscal y formato exógeno específicos, recibidos como parámetros en XML. Consolida los valores de pagos, IVA, retenciones deducibles, no deducibles y retenciones de IVA (común y extraordinaria) agrupados por tercero y concepto contable. Para cada tercero reportado combina su tipo y número de identificación (NIT o documento personal), dígito de verificación, nombre o razón social, y dirección con ciudad, departamento y país, cruzando los catálogos geográficos del sistema. Traduce los tipos de identificación internos del ERP a los códigos oficiales exigidos por la DIAN (11=CC, 12=CE, 13=PA, 22=TI, 31=NIT, 41=CIE, etc.), distinguiendo entre personas naturales y jurídicas para estructurar correctamente cada registro del XML fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1056';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1056';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el detalle agregado por tercero y concepto para construir el reporte de información exógena tributaria Formato 1056 de un año fiscal específico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1056';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos Year y ExogenousFormatId bajo /Data; Deben existir registros en ViewReportExogenousFormat con Format = 1056 para el año y formato exógeno indicados', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1056';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros del formato exógeno 1056; Los valores monetarios se entregan como DECIMAL(18,0) (sin decimales); El tipo de identificación se traduce siempre a la codificación DIAN (11,12,13,22,31,41,43); Para personas jurídicas (IdentificationType=7) nunca se reportan nombres personales y siempre se reporta razón social; Para personas naturales nunca se reporta dígito de verificación ni razón social; Los resultados se ordenan por Concepto y NIT', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1056';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena tributaria; Formato 1056 DIAN; Tercero; NIT; Dígito de verificación; Tipo de identificación; IVA; Retención deducible; Retención no deducible; Retención IVA común; Retención IVA régimen extranjero; Razón social; Dirección fiscal; Ubicación geográfica (ciudad, departamento, país)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1056';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por concepto y tercero filtradas por Year=@YearData, ExogenousFormatId=@ExogenousFormatId y Format=1056, con sumatorias por tipo de concepto (pago, IVA, retenciones)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1056';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (3,2,0,1,7,4) → Mapea a códigos DIAN 11,12,13,22,31,41 respectivamente else Cualquier otro tipo se mapea a 43; si p.IdentificationType = 7 (NIT/persona jurídica) → Reporta DigitVerification y BusinessName (tp.Name); omite nombres y apellidos personales else Reporta nombres y apellidos personales (FirstName, SecondName, FirstLastName, SecondLastName) y omite BusinessName y DigitVerification; si v.ConceptType = 1 → El valor se suma como PaymentValue; si v.ConceptType = 2 → El valor se suma como IVA; si v.ConceptType = 3 → El valor se suma como ReteDeductible; si v.ConceptType = 4 → El valor se suma como ReteNoDeductible; si v.ConceptType = 5 → El valor se suma como ReteIVAComun; si v.ConceptType = 6 → El valor se suma como ReteIVAExt', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1056';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1056';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1056';
-- GO
