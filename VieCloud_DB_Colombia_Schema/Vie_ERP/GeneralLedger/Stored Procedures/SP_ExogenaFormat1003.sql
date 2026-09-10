-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-10
-- Description:	SP que genera la informacion para el XML Formato 1003
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1003]
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
			IIF(v.IsMinimumValue = 0, v.Addresss, NULL) Addresss,
			IIF(v.IsMinimumValue = 0, v.CityCode, NULL) CityCode,
			IIF(v.IsMinimumValue = 0, v.DepartmentCode, NULL) DepartmentCode,
			IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL) CodeCountry,
			SUM(v.BaseValue) BaseValue,
			SUM(v.RetentionValue) RetentionValue
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
				a.Addresss AS Addresss,
				c.Code CityCode,
				d.Code DepartmentCode,
				ct.Code CodeCountry,
				CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS BaseValue,
				CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS RetentionValue,
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
			AND v.[Format] = 1003
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
	GROUP BY v.Concept,
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de información exógena en el Formato 1003 de la DIAN para un año fiscal determinado, consolidando los valores base y retenciones agrupados por concepto tributario y tercero. Toma como entrada criterios en XML (año y código de formato) y construye la información de cada tercero (persona natural o jurídica) cruzando datos de identificación, nombre o razón social, dirección, ciudad, departamento y país. Los pagos o retenciones que no superan el valor mínimo reportable se agrupan como ''CUANTÍAS MENORES'' con identificación genérica (222222222), conforme a las reglas de la DIAN. Compone la información desde la vista de formato exógeno, el catálogo de terceros, personas naturales y la dirección más reciente del tercero.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1003';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1003';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la información agregada por concepto y tercero requerida para el reporte de información exógena Formato 1003 (retenciones en la fuente a título de renta) de un año fiscal, aplicando la regla de cuantías menores.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1003';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId.; Deben existir registros en GeneralLedger.ViewReportExogenousFormat para el Year, ExogenousFormatId indicados y Format = 1003.; Los terceros deben estar relacionados a Common.ThirdParty y opcionalmente a Common.Person para extraer nombres.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1003';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros del formato 1003 del año y formato exógeno solicitados.; Los terceros cuyo total no supera el valor mínimo (cuantía menor) se anonimizan bajo la identificación genérica ''222222222'' / ''CUANTIAS MENORES'' con tipo 43.; Los códigos de tipo de identificación se traducen al estándar DIAN (11,12,13,22,31,41,43).; Para personas jurídicas (IdentificationType=7) nunca se reportan nombres/apellidos; para personas naturales nunca se reporta razón social ni dígito de verificación.; BaseValue y RetentionValue se devuelven como DECIMAL(18,0) (sin decimales).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1003';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena DIAN; Formato 1003 (retenciones en la fuente a título de renta); Cuantías menores; Tercero / NIT; Dígito de verificación; Tipo de identificación; Base sujeta a retención; Valor de retención; Dirección, ciudad, departamento y país del tercero', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1003';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve agregados (SUM BaseValue, SUM RetentionValue) por concepto y tercero filtrando v.Year=@YearData, v.ExogenousFormatId=@ExogenousFormatId y v.Format=1003, ordenados por Concept e IdentificationNumber.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1003';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (3,2,0,1,7,4) → Mapea el tipo de identificación interno a los códigos DIAN (11, 12, 13, 22, 31, 41 respectivamente). else Asigna código 43 (otro tipo de identificación).; si p.IdentificationType = 7 (NIT/persona jurídica) → Reporta BusinessName = tp.Name y DigitVerification = tp.DigitVerification, dejando nombres y apellidos en NULL. else Reporta nombres y apellidos de la persona natural y deja BusinessName y DigitVerification en NULL.; si v.MinimumValue >= SUM(v.Value) por grupo (IsMinimumValue = 1) → Sustituye la identidad del tercero por la marca de cuantías menores: IdentificationType=43, IdentificationNumber=''222222222'', BusinessName=''CUANTIAS MENORES'' y resto de datos personales/dirección en NULL. else Conserva los datos reales del tercero (IsMinimumValue = 0).; si v.ConceptType = 1 → El valor se acumula como BaseValue. else Si v.ConceptType = 2 se acumula como RetentionValue; otros se ignoran (suman 0).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1003';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1003';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1003';
-- GO
