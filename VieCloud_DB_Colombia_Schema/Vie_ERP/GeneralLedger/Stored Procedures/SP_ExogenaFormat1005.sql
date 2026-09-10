-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1005
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1005]
   @xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearData INT,
			@ExogenousFormatId INT

	SELECT	@YearData = t.x.value('Year[1]','int'),
			@ExogenousFormatId = t.x.value('ExogenousFormatId[1]','int')
	FROM @xmlCriterias.nodes('/Data') t(x)

    SELECT	CASE p.IdentificationType
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
			CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS DiscountableTax,
			CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS IvaValue
	FROM GeneralLedger.ViewReportExogenousFormat v
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON v.ThirdPartyId = tp.Id
	LEFT JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
	WHERE v.Year = @YearData
		AND v.ExogenousFormatId = @ExogenousFormatId
		AND v.[Format] = 1005
	GROUP BY v.Concept,
		p.IdentificationType,
		tp.Nit,
		IIF(p.IdentificationType = 7, tp.DigitVerification, NULL),
		IIF(p.IdentificationType = 7, NULL, p.FirstLastName),
		IIF(p.IdentificationType = 7, NULL, p.SecondLastName),
		IIF(p.IdentificationType = 7, NULL, p.FirstName),
		IIF(p.IdentificationType = 7, NULL, p.SecondName),
		IIF(p.IdentificationType = 7, tp.Name, NULL)
	ORDER BY tp.Nit
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la información requerida para el Formato 1005 de la información exógena tributaria (DIAN), correspondiente a IVA descontable e IVA generado por tercero. Recibe como parámetro un XML con el año fiscal y el identificador del formato exógeno, consulta los movimientos contables consolidados desde la vista ViewReportExogenousFormat y los cruza con los datos de identificación de terceros (personas naturales y jurídicas) desde Common.ThirdParty y Common.Person. Convierte los tipos de identificación internos al código DIAN correspondiente (NIT, cédula, pasaporte, etc.) y devuelve un resultado agrupado por tercero con los totales de IVA descontable e IVA generado, listo para construir el XML de reporte exógeno ante la autoridad tributaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1005';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1005';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos del Formato 1005 de información exógena (IVA descontable) agregando valores por tercero y concepto para un año y formato exógeno determinados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1005';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodo /Data con Year y ExogenousFormatId; Deben existir registros en ViewReportExogenousFormat con Format=1005 para el año y formato indicados; Los terceros referenciados deben existir en Common.ThirdParty y opcionalmente en Common.Person', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1005';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros cuyo Format = 1005, del año y formato exógeno indicados en el XML; Los tipos de identificación se traducen al catálogo DIAN (11,12,13,22,31,41,43); Para personas jurídicas (IdentificationType=7) se reporta razón social y dígito de verificación, nunca nombres; Para personas naturales se reportan nombres/apellidos y nunca razón social ni dígito de verificación; Los valores monetarios se entregan como DECIMAL(18,0) (sin decimales); La agregación se hace por concepto y por tercero, ordenando por NIT', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1005';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena; Formato 1005 DIAN; IVA descontable; Tercero; Tipo de identificación; Dígito de verificación; Razón social; NIT', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1005';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.ViewReportExogenousFormat: Cuando Year=@YearData, ExogenousFormatId=@ExogenousFormatId y Format=1005, retorna por tercero el tipo y número de identificación (mapeado a códigos DIAN), nombres o razón social, y la suma de Value separada en DiscountableTax (ConceptType=1) e IvaValue (ConceptType=2)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1005';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType = 7 (persona jurídica) → Se reporta BusinessName y DigitVerification; se omiten nombres y apellidos else Se reportan nombres y apellidos de persona natural; BusinessName y DigitVerification quedan en NULL; si Mapeo de p.IdentificationType a código DIAN → 3→11, 2→12, 0→13, 1→22, 7→31, 4→41 else Cualquier otro valor se mapea a 43; si v.ConceptType = 1 → Su Value se suma como DiscountableTax (impuesto descontable) else No suma a DiscountableTax; si v.ConceptType = 2 → Su Value se suma como IvaValue else No suma a IvaValue', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1005';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1005';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1005';
-- GO
