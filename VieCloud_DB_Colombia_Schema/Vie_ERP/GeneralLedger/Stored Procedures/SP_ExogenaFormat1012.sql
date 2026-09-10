-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1012
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1012]
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
			SUM(v.Balance) Balance
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
				CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS Balance,
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
			AND v.[Format] = 1012
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la información requerida para el Formato 1012 de información exógena tributaria (DIAN), correspondiente a un año fiscal específico. Consolida los saldos contables por concepto y por tercero (proveedor, contratista o persona natural), obteniendo la identificación tributaria (NIT con dígito de verificación o cédula), nombres o razón social, y el país de domicilio a partir de los catálogos de terceros, personas, direcciones y geografía del sistema. Cuando el valor acumulado de un tercero no supera el mínimo exigido por la norma, agrupa esos registros bajo la figura de ''cuantías menores'' con identificación genérica (código 43 / NIT 222222222), cumpliendo así el requisito de reporte de la información exógena ante la autoridad tributaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1012';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1012';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de información exógena tributaria Formato 1012 (saldos en cuentas bancarias) agrupado por concepto y tercero, aplicando reglas de cuantías menores y mapeo de tipos de identificación a códigos DIAN.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1012';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe incluir Year y ExogenousFormatId.; Deben existir registros en ViewReportExogenousFormat con Format=1012 para el año y formato exógeno indicados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1012';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen registros del Format=1012 para el Year y ExogenousFormatId recibidos.; Sólo los valores con ConceptType=1 suman al Balance; los demás aportan 0.; Los terceros que no superan la cuantía mínima se consolidan bajo la identificación genérica 222222222 con razón social ''CUANTIAS MENORES'' y tipo 43.; El tipo de identificación interno siempre se traduce al catálogo DIAN (11,12,13,22,31,41,43).; Para personas jurídicas (IdentificationType=7) nunca se reportan nombres personales; para personas naturales nunca se reporta DigitVerification ni BusinessName.; El resultado se ordena por Concepto y luego por Número de identificación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1012';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena tributaria; Formato 1012 DIAN; Cuantías menores; Tercero; NIT y dígito de verificación; Tipo de identificación; Persona natural / persona jurídica; País de residencia', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1012';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por Concept y datos del tercero, con SUM(Balance) sólo de movimientos donde ConceptType=1.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1012';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (3,2,0,1,7,4) → Mapea a códigos DIAN 11, 12, 13, 22, 31, 41 respectivamente. else Cualquier otro tipo de identificación se mapea al código 43 (extranjero/sin identificación).; si p.IdentificationType = 7 (NIT/persona jurídica) → Se reporta BusinessName con tp.Name y se incluye DigitVerification; se omiten nombres y apellidos. else Se reportan FirstName/SecondName/FirstLastName/SecondLastName y se omite DigitVerification y BusinessName.; si v.MinimumValue >= SUM(v.Value) (no supera cuantía mínima) → IsMinimumValue=1: el tercero se anonimiza usando IdentificationType=43, IdentificationNumber=''222222222'', BusinessName=''CUANTIAS MENORES'' y se anulan nombres, dígito de verificación y país. else IsMinimumValue=0: se conservan los datos reales del tercero.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1012';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1012';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1012';
-- GO
