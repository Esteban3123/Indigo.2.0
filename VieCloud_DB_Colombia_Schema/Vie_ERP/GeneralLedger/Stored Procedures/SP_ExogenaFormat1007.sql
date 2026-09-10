-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1007
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1007]
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
			IIF(v.IsMinimumValue = 0, v.IdentificationType, 43) IdentificationType,
			IIF(v.IsMinimumValue = 0, v.IdentificationNumber, '222222222') IdentificationNumber,
			IIF(v.IsMinimumValue = 0, v.DigitVerification, NULL) DigitVerification,
			IIF(v.IsMinimumValue = 0, v.FirstLastName, NULL) FirstLastName,
			IIF(v.IsMinimumValue = 0, v.SecondLastName, NULL) SecondLastName,
			IIF(v.IsMinimumValue = 0, v.FirstName, NULL) FirstName,
			IIF(v.IsMinimumValue = 0, v.SecondName, NULL) SecondName,
			IIF(v.IsMinimumValue = 0, v.BusinessName, 'CUANTIAS MENORES') BusinessName,
			IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL) CodeCountry,
			SUM(v.GrossIncome) GrossIncome,
			SUM(v.DeductedValue) DeductedValue
	FROM
	(
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
				ct.Code CodeCountry,
				CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS GrossIncome,
				CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS DeductedValue,
				IIF(v.MinimumValue < SUM(v.Value), 0, 1) IsMinimumValue
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
			AND v.[Format] = 1007
		GROUP BY v.Concept,
			v.MinimumValue,
			p.IdentificationType,
			tp.Nit,
			IIF(p.IdentificationType = 7, tp.DigitVerification, NULL),
			IIF(p.IdentificationType = 7, NULL, p.FirstLastName),
			IIF(p.IdentificationType = 7, NULL, p.SecondLastName),
			IIF(p.IdentificationType = 7, NULL, p.FirstName),
			IIF(p.IdentificationType = 7, NULL, p.SecondName),
			IIF(p.IdentificationType = 7, tp.Name, NULL),
			ct.Code
	) v
	GROUP BY v.Concept,
		IIF(v.IsMinimumValue = 0, v.IdentificationType, 43),
		IIF(v.IsMinimumValue = 0, v.IdentificationNumber, '222222222'),
		IIF(v.IsMinimumValue = 0, v.DigitVerification, NULL),
		IIF(v.IsMinimumValue = 0, v.FirstLastName, NULL),
		IIF(v.IsMinimumValue = 0, v.SecondLastName, NULL),
		IIF(v.IsMinimumValue = 0, v.FirstName, NULL),
		IIF(v.IsMinimumValue = 0, v.SecondName, NULL),
		IIF(v.IsMinimumValue = 0, v.BusinessName, 'CUANTIAS MENORES'),
		IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL)
	ORDER BY 1, 3
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la información requerida para el reporte de información exógena Formato 1007 ante la DIAN, correspondiente a pagos o abonos en cuenta a terceros. Recibe como parámetro el año fiscal y el identificador del formato exógeno, y consolida los ingresos brutos y valores deducidos por concepto y por tercero (proveedor, contratista o persona natural), consultando la vista ViewReportExogenousFormat junto con los datos de identificación, nombre o razón social y país de residencia de cada tercero. Aplica la regla de cuantías menores: cuando el valor acumulado de un tercero no supera el umbral mínimo definido, agrupa los registros bajo el NIT genérico ''222222222'' con la leyenda ''CUANTIAS MENORES'', tal como lo exige la normativa tributaria colombiana para este formato.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1007';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1007';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la información agregada por concepto y tercero para el reporte de información exógena Formato 1007 (ingresos brutos y retenciones) de un año fiscal dado, aplicando reglas de cuantías menores.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId.; Deben existir registros en GeneralLedger.ViewReportExogenousFormat con Format=1007 para el Year y ExogenousFormatId indicados.; Los terceros deben estar relacionados con personas, direcciones y país para obtener el código de país.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte siempre se restringe a registros con Format=1007.; Los terceros con suma de valores por debajo del MinimumValue siempre se anonimizan bajo identificación 43/''222222222'' y razón social ''CUANTIAS MENORES''.; Los importes GrossIncome y DeductedValue siempre se devuelven como DECIMAL(18,0) (sin decimales).; Los tipos de identificación internos siempre se traducen a la codificación DIAN antes de exponerse.; Para personas jurídicas (tipo 7) nunca se exponen nombres/apellidos personales; para personas naturales nunca se expone BusinessName ni DigitVerification.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena DIAN; Formato 1007 (ingresos recibidos); Tercero; Tipo y número de identificación; Dígito de verificación (NIT); Cuantías menores; Ingreso bruto; Valor deducido / retención; Código de país; Año gravable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por concepto y tercero con SUM(Value) para ConceptType=1 como GrossIncome y ConceptType=2 como DeductedValue, filtradas por Year, ExogenousFormatId y Format=1007.; [RETURN_RESULT] resultset: Cuando la suma de valores del tercero no supera MinimumValue (IsMinimumValue=1), se reemplaza la identificación por tipo 43, número ''222222222'' y razón social ''CUANTIAS MENORES'', anulando nombres, dígito de verificación y código de país.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (3,2,0,1,7,4) → Mapea el tipo de identificación interno al código DIAN equivalente (3→11, 2→12, 0→13, 1→22, 7→31, 4→41). else Asigna código DIAN 43 por defecto.; si p.IdentificationType = 7 (persona jurídica/NIT) → Toma el DigitVerification y el Name del tercero como BusinessName, anulando los nombres y apellidos personales. else Toma los nombres y apellidos de la persona y anula DigitVerification y BusinessName.; si v.MinimumValue >= SUM(v.Value) → Marca el registro como cuantía menor (IsMinimumValue=1) y lo consolida bajo el tercero genérico ''222222222'' - ''CUANTIAS MENORES''. else Conserva la identificación real del tercero (IsMinimumValue=0).; si v.ConceptType = 1 → El valor se acumula como GrossIncome (ingreso bruto). else Si ConceptType=2 se acumula como DeductedValue (valor deducido/retenido); en otro caso aporta 0.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1007';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1007';
-- GO
