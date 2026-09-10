-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1006
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1006]
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
			CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS IVA,
			CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS IVARecovered,
			CAST(SUM(IIF(v.ConceptType = 3, v.Value, 0)) AS DECIMAL(18, 0)) AS ConsumptionTax
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
		AND v.[Format] = 1006
	GROUP BY v.Concept,
		p.IdentificationType,
		tp.Nit,
		IIF(p.IdentificationType = 7, tp.DigitVerification, NULL),
		IIF(p.IdentificationType = 7, NULL, p.FirstLastName),
		IIF(p.IdentificationType = 7, NULL, p.SecondLastName),
		IIF(p.IdentificationType = 7, NULL, p.FirstName),
		IIF(p.IdentificationType = 7, NULL, p.SecondName),
		IIF(p.IdentificationType = 7, tp.Name, NULL)
	ORDER BY v.Concept, tp.Nit
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la información requerida para el reporte de información exógena tributaria Formato 1006 ante la DIAN, correspondiente a un año fiscal específico. Consolida los valores de IVA generado, IVA recuperado e impuesto al consumo agrupados por concepto y tercero, cruzando los datos contables de la vista de formatos exógenos con la información de identificación del tercero (persona natural o jurídica), su tipo de documento (cédula, NIT, pasaporte, entre otros) y razón social o nombre completo. Recibe como parámetro un XML con el año de reporte y el identificador del formato exógeno, y devuelve un resultado ordenado por concepto e identificación del tercero, listo para construir el XML de declaración tributaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1006';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1006';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte agregado de información exógena Formato 1006 (impuestos descontables: IVA, IVA recuperado e impuesto al consumo) por concepto y tercero para un año y formato exógeno específicos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId con valores enteros; Debe existir información en ViewReportExogenousFormat para el año y formato exógeno indicados con Format = 1006', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros del año y formato exógeno solicitados y cuyo Format sea 1006; Para terceros con IdentificationType = 7 (jurídicos) se expone razón social y dígito de verificación; nunca nombres/apellidos; Para terceros con IdentificationType ≠ 7 se exponen nombres/apellidos; nunca razón social ni dígito de verificación; Los valores monetarios (IVA, IVA recuperado, Impuesto al consumo) se entregan agregados por concepto y tercero, redondeados a entero (DECIMAL(18,0)); Cualquier IdentificationType no mapeado se clasifica como 43', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena (Formato 1006); Tercero; Tipo de identificación tributaria; NIT; Dígito de verificación; Razón social; IVA; IVA recuperado; Impuesto al consumo', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.ViewReportExogenousFormat: Devuelve un result set con los datos del Formato 1006 agrupados por concepto y tercero, filtrando por Year=@YearData, ExogenousFormatId=@ExogenousFormatId y Format=1006, ordenado por Concept y Nit', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType = 3 → IdentificationType de salida = 11; si p.IdentificationType = 2 → IdentificationType de salida = 12; si p.IdentificationType = 0 → IdentificationType de salida = 13; si p.IdentificationType = 1 → IdentificationType de salida = 22; si p.IdentificationType = 7 → IdentificationType de salida = 31; se reporta DigitVerification y BusinessName (Name del tercero); se omiten nombres y apellidos; si p.IdentificationType = 4 → IdentificationType de salida = 41; si p.IdentificationType no coincide con 0,1,2,3,4,7 → IdentificationType de salida = 43 (valor por defecto); si v.ConceptType = 1 → El valor se acumula en columna IVA; si v.ConceptType = 2 → El valor se acumula en columna IVARecovered (IVA recuperado); si v.ConceptType = 3 → El valor se acumula en columna ConsumptionTax (impuesto al consumo)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country; Common.ViewLatestEmailByPerson; Common.Email', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1006';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1006';
-- GO
