-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1009
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1009]
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
			AND v.[Format] = 1009
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la información tributaria requerida para el Formato 1009 de información exógena ante la DIAN, correspondiente a un año fiscal específico. Consolida los valores contables por concepto y tercero (proveedor, contratista o persona natural) cruzando datos de identificación, nombre o razón social, y dirección completa (ciudad, departamento, país). Aplica la regla de cuantías menores: cuando el saldo acumulado de un tercero no supera el mínimo permitido, agrupa esos valores bajo el identificador genérico ''222222222 - CUANTIAS MENORES'', cumpliendo así las instrucciones de la DIAN para no reportar terceros de bajo valor individualmente. Recibe los criterios de filtro (año y formato exógeno) como XML y produce el listado listo para construir el archivo XML de envío a la autoridad tributaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1009';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1009';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la información consolidada por concepto y tercero para el formato 1009 de información exógena (DIAN), aplicando reglas de cuantías menores y mapeo de tipos de identificación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId; Deben existir registros en GeneralLedger.ViewReportExogenousFormat con Format=1009 para el año y ExogenousFormatId indicados', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los terceros cuyo acumulado no supera el valor mínimo se reportan agrupados como ''CUANTIAS MENORES'' con NIT ''222222222'' y tipo 43; Solo se incluyen registros con Format=1009 del año y ExogenousFormatId solicitados; El Balance se calcula únicamente sobre conceptos con ConceptType=1; Los códigos de tipo de identificación se traducen al estándar DIAN (11,12,13,22,31,41,43); Para personas jurídicas (IdentificationType=7) nunca se exponen nombres/apellidos; para naturales nunca se expone razón social ni dígito de verificación', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena; Formato 1009 DIAN; Tercero; Tipo de identificación; NIT; Dígito de verificación; Razón social; Cuantías menores; Cuantía mínima', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result-set: Devuelve filas agrupadas por Concepto y tercero con los datos de identificación, ubicación y suma de saldos para el formato 1009', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (3,2,0,1,7,4) → Mapea el tipo de identificación interno a los códigos DIAN (11,12,13,22,31,41 respectivamente) else Asigna código 43 como tipo de identificación por defecto; si p.IdentificationType = 7 (jurídica/NIT) → Usa DigitVerification y BusinessName (tp.Name); anula nombres y apellidos de persona natural else Usa nombres y apellidos de la persona natural; anula DigitVerification y BusinessName; si MinimumValue >= SUM(Value) (no supera la cuantía mínima) → Marca IsMinimumValue=1 y reemplaza identificación por 43/''222222222'', BusinessName=''CUANTIAS MENORES'' y anula nombres, dirección, ciudad, departamento y país else Conserva los datos reales del tercero (IsMinimumValue=0); si v.ConceptType = 1 → Suma el Value al Balance else Aporta 0 al Balance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1009';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1009';
-- GO
