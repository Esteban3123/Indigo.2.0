-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1010
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1010]
   @xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearData INT,
			@ExogenousFormatId INT

	SELECT	@YearData = t.x.value('Year[1]','int'),
			@ExogenousFormatId = t.x.value('ExogenousFormatId[1]','int')
	FROM @xmlCriterias.nodes('/Data') t(x)

	SELECT	IIF(v.IsMinimumValue = 0, v.IdentificationType, 43) IdentificationType,
			IIF(v.IsMinimumValue = 0, v.IdentificationNumber, '222222222') IdentificationNumber,
			IIF(v.IsMinimumValue = 0, v.DigitVerification, NULL) DigitVerification,
			IIF(v.IsMinimumValue = 0, v.FirstLastName, NULL) FirstLastName,
			IIF(v.IsMinimumValue = 0, v.SecondLastName, NULL) SecondLastName,
			IIF(v.IsMinimumValue = 0, v.FirstName, NULL) FirstName,
			IIF(v.IsMinimumValue = 0, v.SecondName, NULL) SecondName,
			IIF(v.IsMinimumValue = 0, v.BusinessName, 'CUANTIAS MENORES') BusinessName,
			IIF(v.IsMinimumValue = 0, v.Addresss, NULL) Addresss,
			IIF(v.IsMinimumValue = 0, v.CityCode, NULL) CityCode,
			IIF(v.IsMinimumValue = 0, v.DepartmentCode, NULL) DepartmentCode,
			IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL) CodeCountry,
			SUM(v.PatrimonialValue) PatrimonialValue,
			SUM(v.ParticipationPercentage) ParticipationPercentage,
			SUM(v.ParticipationPercentageDecimal) ParticipationPercentageDecimal
	FROM
	(
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
				a.Addresss AS Addresss,
				c.Code CityCode,
				d.Code DepartmentCode,
				ct.Code CodeCountry,
				CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS PatrimonialValue,
				CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS ParticipationPercentage,
				CAST(SUM(IIF(v.ConceptType = 3, v.Value, 0)) AS DECIMAL(18, 0)) AS ParticipationPercentageDecimal,
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
			AND v.[Format] = 1010
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
			a.Addresss, c.Code, d.Code, ct.Code
	) v
	GROUP BY 
		IIF(v.IsMinimumValue = 0, v.IdentificationType, 43),
		IIF(v.IsMinimumValue = 0, v.IdentificationNumber, '222222222'),
		IIF(v.IsMinimumValue = 0, v.DigitVerification, NULL),
		IIF(v.IsMinimumValue = 0, v.FirstLastName, NULL),
		IIF(v.IsMinimumValue = 0, v.SecondLastName, NULL),
		IIF(v.IsMinimumValue = 0, v.FirstName, NULL),
		IIF(v.IsMinimumValue = 0, v.SecondName, NULL),
		IIF(v.IsMinimumValue = 0, v.BusinessName, 'CUANTIAS MENORES'),
		IIF(v.IsMinimumValue = 0, v.Addresss, NULL),
		IIF(v.IsMinimumValue = 0, v.CityCode, NULL),
		IIF(v.IsMinimumValue = 0, v.DepartmentCode, NULL),
		IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL)
	ORDER BY 1, 3
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la información tributaria del Formato 1010 de la información exógena requerida por la DIAN para un año fiscal determinado. Consolida datos de terceros (proveedores, contratistas, socios o entidades relacionadas) junto con su identificación, dirección, ciudad, departamento y país, calculando valores patrimoniales, porcentajes de participación y sus decimales por cada concepto reportable. Los registros que no superan el monto mínimo establecido se agrupan bajo la categoría ''CUANTIAS MENORES'' con identificación genérica 222222222, según las reglas de la norma. Compone la respuesta cruzando la vista de movimientos exógenos con las tablas maestras de terceros, personas y ubicaciones geográficas para producir el detalle completo exigido en el XML del formato 1010.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1010';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1010';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos para el reporte de información exógena Formato 1010 (DIAN), consolidando terceros con valores patrimoniales y agrupando los que no superan la cuantía mínima bajo el rubro ''CUANTIAS MENORES''.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId; Deben existir registros en GeneralLedger.ViewReportExogenousFormat con Format=1010 para el año y formato exógeno indicados; Los terceros deben estar vinculados a Person, Address, City, Department y Country para obtener datos demográficos completos', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los terceros con valores que no superan la cuantía mínima se consolidan bajo identificación ''222222222'', tipo 43 y razón social ''CUANTIAS MENORES''; El DigitVerification solo se reporta para terceros tipo NIT (IdentificationType=7); Los nombres/apellidos personales y la razón social son mutuamente excluyentes según el tipo de identificación; Los valores monetarios se truncan a DECIMAL(18,0) (sin decimales); Solo se incluyen registros del Format 1010 para el año y formato exógeno indicados', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena DIAN; Formato 1010; Tercero; NIT; Dígito de verificación; Cuantías menores; Valor patrimonial; Porcentaje de participación; Tipo de identificación tributaria', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result-set: Devuelve filas agrupadas por identificación y ubicación con sumas de PatrimonialValue, ParticipationPercentage y ParticipationPercentageDecimal filtradas por Year, ExogenousFormatId y Format=1010', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (3,2,0,1,7,4) → Mapea el tipo de identificación interno al código DIAN (11, 12, 13, 22, 31, 41 respectivamente) else Asigna 43 como tipo de identificación por defecto; si p.IdentificationType = 7 (NIT/empresa) → Reporta DigitVerification y BusinessName (tp.Name); omite nombres y apellidos personales else Reporta nombres y apellidos personales; omite DigitVerification y BusinessName; si v.MinimumValue >= SUM(v.Value) (no supera cuantía mínima → IsMinimumValue=1) → Sustituye identificación por ''222222222'', tipo por 43, BusinessName por ''CUANTIAS MENORES'' y anula datos personales y de ubicación else Conserva los datos reales del tercero; si v.ConceptType = 1 / 2 / 3 → Acumula el valor en PatrimonialValue, ParticipationPercentage o ParticipationPercentageDecimal según corresponda', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1010';
-- GO
