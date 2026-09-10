-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1008
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1008]
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
				a.Addresss AS Addresss,
				c.Code CityCode,
				d.Code DepartmentCode,
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
			AND v.[Format] = 1008
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la información tributaria para el Formato 1008 de información exógena ante la DIAN, correspondiente a un año fiscal específico. Consolida los movimientos contables por concepto desde la vista de reportes exógenos, cruzando con los datos del tercero (NIT, dígito de verificación, razón social) y de la persona natural (tipo y número de identificación, nombres, apellidos), así como la dirección más reciente (ciudad, departamento, país). Los terceros que no superan el valor mínimo reportable se agrupan bajo la categoría ''CUANTIAS MENORES'' con identificación genérica 222222222, cumpliendo así los requisitos de reporte fiscal para pagos o retenciones a terceros.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1008';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1008';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la información consolidada por concepto y tercero para el reporte de información exógena DIAN Formato 1008 (saldos de cuentas por cobrar) de un año fiscal específico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId; Debe existir información cargada en GeneralLedger.ViewReportExogenousFormat para el año y formato (1008) indicados; Los terceros deben estar relacionados con personas y, opcionalmente, con direcciones, ciudades, departamentos y países en Common', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa registros con Format = 1008; Filtra estrictamente por el año y el ExogenousFormatId recibidos; Los terceros cuyo acumulado no supera el valor mínimo se agrupan bajo la pseudo-identificación ''222222222'' tipo 43 con razón social ''CUANTIAS MENORES'' (regla DIAN para cuantías menores); Las personas jurídicas (IdentificationType=7) nunca exponen nombres/apellidos; las personas naturales nunca exponen razón social ni dígito de verificación; El Balance siempre se redondea a entero (DECIMAL(18,0)) y solo acumula valores con ConceptType=1; Usa la dirección más reciente por persona (ViewLatestAddressByPerson); Lecturas con NOLOCK sobre tablas maestras (lecturas sucias permitidas)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena DIAN; Formato 1008 (cuentas por cobrar); Tercero; Tipo de identificación (RC, TI, CC, CE, NIT, Pasaporte); Dígito de verificación NIT; Razón social; Cuantías menores; Concepto contable; Saldo (Balance); Ubicación geográfica (ciudad, departamento, país)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto de resultados con concepto, identificación, nombres/razón social, ubicación y saldo agrupado por tercero, ordenado por concepto y número de identificación', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (3,2,0,1,7,4) → Mapea el tipo de identificación interno al código DIAN (11=Registro civil, 12=TI, 13=CC, 22=CE, 31=NIT, 41=Pasaporte) else Asigna 43 (documento extranjero) como tipo de identificación por defecto; si p.IdentificationType = 7 (NIT/persona jurídica) → Reporta DigitVerification y BusinessName (razón social) y deja en NULL los nombres y apellidos else Reporta nombres y apellidos de la persona natural y deja NULL DigitVerification y BusinessName; si v.MinimumValue >= SUM(v.Value) (el acumulado no supera la cuantía mínima) → Marca IsMinimumValue=1 y consolida bajo identificación 222222222, tipo 43 y razón social ''CUANTIAS MENORES'', anulando datos personales y de dirección else Conserva los datos reales del tercero (IsMinimumValue=0); si v.ConceptType = 1 → Suma v.Value al Balance reportado else Aporta 0 al Balance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1008';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1008';
-- GO
