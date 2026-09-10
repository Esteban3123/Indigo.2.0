-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1004
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat2276]
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
			IIF(v.IsMinimumValue = 0, v.FirstLastName, 'CUANTIAS MENORES') FirstLastName,
			IIF(v.IsMinimumValue = 0, v.SecondLastName, NULL) SecondLastName,
			IIF(v.IsMinimumValue = 0, v.FirstName, NULL) FirstName,
			IIF(v.IsMinimumValue = 0, v.SecondName, NULL) SecondName,
			IIF(v.IsMinimumValue = 0, v.Addresss, NULL) Addresss,
			IIF(v.IsMinimumValue = 0, v.CityCode, NULL) CityCode,
			IIF(v.IsMinimumValue = 0, v.DepartmentCode, NULL) DepartmentCode,
			IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL) CodeCountry,
			SUM(v.PaymentsForWages) PaymentsForWages,
			SUM(v.PaymentsForChurchEmoluments) PaymentsForChurchEmoluments,
			SUM(v.PaymentsForFees) PaymentsForFees,
			SUM(v.PaymentsForServices) PaymentsForServices,
			SUM(v.PaymentsForCommisions) PaymentsForCommisions,
			SUM(v.PaymentsForSocialBenefits) PaymentsForSocialBenefits,
			SUM(v.PaymentsForPerDiem) PaymentsForPerDiem,
			SUM(v.PaymentsForRepresentationExpenses) PaymentsForRepresentationExpenses,
			SUM(v.PaymentsForCooperativeAssociateWork) PaymentsForCooperativeAssociateWork,
			SUM(v.OtherPayments) OtherPayments,
			SUM(v.PaymentsWithBonds) PaymentsWithBonds,
			SUM(v.UnemploymentValue) UnemploymentValue,
			SUM(v.RetirementValue) RetirementValue,
			SUM(v.HealthValue) HealthValue,
			SUM(v.PensionValue) PensionValue,
			SUM(v.VoluntaryPensionValue) VoluntaryPensionValue,
			SUM(v.AFCValue) AFCValue,
			SUM(v.RetentionValue) RetentionValue
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
				IIF(p.IdentificationType = 7, NULL, p.FirstLastName) FirstLastName,
				IIF(p.IdentificationType = 7, NULL, p.SecondLastName) SecondLastName,
				IIF(p.IdentificationType = 7, NULL, p.FirstName) FirstName,
				IIF(p.IdentificationType = 7, NULL, p.SecondName) SecondName,
				a.Addresss AS Addresss,
				c.Code CityCode,
				d.Code DepartmentCode,
				ct.Code CodeCountry,
				CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForWages,
				CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForChurchEmoluments,
				CAST(SUM(IIF(v.ConceptType = 3, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForFees,
				CAST(SUM(IIF(v.ConceptType = 4, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForServices,
				CAST(SUM(IIF(v.ConceptType = 5, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForCommisions,
				CAST(SUM(IIF(v.ConceptType = 6, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForSocialBenefits,
				CAST(SUM(IIF(v.ConceptType = 7, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForPerDiem,
				CAST(SUM(IIF(v.ConceptType = 8, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForRepresentationExpenses,
				CAST(SUM(IIF(v.ConceptType = 9, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsForCooperativeAssociateWork,
				CAST(SUM(IIF(v.ConceptType = 10, v.Value, 0)) AS DECIMAL(18, 0)) AS OtherPayments,
				CAST(SUM(IIF(v.ConceptType = 11, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentsWithBonds,
				CAST(SUM(IIF(v.ConceptType = 12, v.Value, 0)) AS DECIMAL(18, 0)) AS UnemploymentValue,
				CAST(SUM(IIF(v.ConceptType = 13, v.Value, 0)) AS DECIMAL(18, 0)) AS RetirementValue,
				CAST(SUM(IIF(v.ConceptType = 14, v.Value, 0)) AS DECIMAL(18, 0)) AS HealthValue,
				CAST(SUM(IIF(v.ConceptType = 15, v.Value, 0)) AS DECIMAL(18, 0)) AS PensionValue,
				CAST(SUM(IIF(v.ConceptType = 16, v.Value, 0)) AS DECIMAL(18, 0)) AS VoluntaryPensionValue,
				CAST(SUM(IIF(v.ConceptType = 17, v.Value, 0)) AS DECIMAL(18, 0)) AS AFCValue,
				CAST(SUM(IIF(v.ConceptType = 18, v.Value, 0)) AS DECIMAL(18, 0)) AS RetentionValue,
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
			AND v.[Format] = 2276
		GROUP BY v.Concept,
			v.MinimumValue,
			p.IdentificationType,
			tp.Nit,
			IIF(p.IdentificationType = 7, NULL, p.FirstLastName),
			IIF(p.IdentificationType = 7, NULL, p.SecondLastName),
			IIF(p.IdentificationType = 7, NULL, p.FirstName),
			IIF(p.IdentificationType = 7, NULL, p.SecondName),
			a.Addresss, c.Code, d.Code, ct.Code
	) v
	GROUP BY 
		IIF(v.IsMinimumValue = 0, v.IdentificationType, 43),
		IIF(v.IsMinimumValue = 0, v.IdentificationNumber, '222222222'),
		IIF(v.IsMinimumValue = 0, v.FirstLastName, 'CUANTIAS MENORES'),
		IIF(v.IsMinimumValue = 0, v.SecondLastName, NULL),
		IIF(v.IsMinimumValue = 0, v.FirstName, NULL),
		IIF(v.IsMinimumValue = 0, v.SecondName, NULL),
		IIF(v.IsMinimumValue = 0, v.Addresss, NULL),
		IIF(v.IsMinimumValue = 0, v.CityCode, NULL),
		IIF(v.IsMinimumValue = 0, v.DepartmentCode, NULL),
		IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL)
	ORDER BY 1, 3
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la información requerida para el reporte de información exógena tributaria en el Formato 2276 (equivalente al antiguo Formato 1004), utilizado para declarar ante la DIAN los pagos laborales y de servicios realizados a terceros durante un año fiscal específico. Consolida, por cada beneficiario del pago, los valores desagregados por concepto (salarios, honorarios, servicios, comisiones, prestaciones sociales, cesantías, pensión, salud, retención en la fuente, entre otros), cruzando datos de identificación del tercero desde las tablas de Terceros y Personas, y su dirección más reciente a través de Ciudad, Departamento y País. Los pagos que no superan el umbral de cuantías mínimas se agrupan bajo el código de identificación ''222222222'' con la leyenda ''CUANTIAS MENORES'', en cumplimiento de la normativa tributaria colombiana. Recibe como parámetro un XML con el año fiscal y el identificador del formato exógeno a procesar.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat2276';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat2276';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos consolidado por tercero para el reporte de información exógena DIAN Formato 2276 (pagos laborales y retenciones), agrupando bajo ''CUANTIAS MENORES'' los terceros que no superan el valor mínimo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2276';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodo /Data con Year y ExogenousFormatId; Deben existir datos en ViewReportExogenousFormat para el año, ExogenousFormatId y Format=2276; Los terceros deben tener relación con Person, y opcionalmente con dirección, ciudad, departamento y país vigentes', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2276';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros del año y formato exógeno indicados y con Format=2276; Los terceros con pagos por debajo del mínimo se agrupan obligatoriamente bajo el seudónimo ''CUANTIAS MENORES'' con NIT ''222222222'' y tipo 43; Para personas con tipo de identificación 7 nunca se exponen nombres ni apellidos; Los valores monetarios se entregan como DECIMAL(18,0) (sin decimales); Cada concepto contable se mapea a una columna fija (ConceptType 1..18) del formato', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2276';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena tributaria; Formato 2276 DIAN; Tercero; Tipo de identificación; Pagos por salarios; Emolumentos eclesiásticos; Honorarios; Servicios; Comisiones; Prestaciones sociales; Viáticos; Gastos de representación; Trabajo asociado cooperativo; Cesantías; Pensiones; Aportes a salud; Pensión voluntaria; AFC; Retención en la fuente; Cuantías menores', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2276';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por tercero con sumas por ConceptType (1..18) mapeadas a columnas de pagos y retenciones del Formato 2276, filtrando v.Year=@YearData, v.ExogenousFormatId=@ExogenousFormatId y v.[Format]=2276; [RETURN_RESULT] resultset: Cuando v.MinimumValue >= SUM(v.Value) (IsMinimumValue=1) los datos identificatorios se reemplazan por IdentificationType=43, IdentificationNumber=''222222222'', FirstLastName=''CUANTIAS MENORES'' y demás campos en NULL; [RETURN_RESULT] resultset: Cuando p.IdentificationType=7 los campos FirstLastName, SecondLastName, FirstName y SecondName se devuelven en NULL', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2276';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IdentificationType de la persona = 3 → Se mapea a tipo 11 para reporte exógeno; si IdentificationType = 2 → Se mapea a tipo 12; si IdentificationType = 0 → Se mapea a tipo 13; si IdentificationType = 1 → Se mapea a tipo 22; si IdentificationType = 7 → Se mapea a tipo 31 y se omiten nombres y apellidos (NULL); si IdentificationType = 4 → Se mapea a tipo 41; si IdentificationType distinto a los anteriores → Se mapea a tipo 43 (otros/extranjero); si MinimumValue >= SUM(Value) por concepto (IsMinimumValue=1) → El registro se consolida bajo identificación 43, NIT ''222222222'', nombre ''CUANTIAS MENORES'' y se anulan datos personales/dirección else Se conservan los datos reales del tercero/persona', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2276';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2276';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat2276';
-- GO
