-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1647
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1647]
   @xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearData INT,
			@ExogenousFormatId INT

	SELECT	@YearData = t.x.value('Year[1]','int'),
			@ExogenousFormatId = t.x.value('ExogenousFormatId[1]','int')
	FROM @xmlCriterias.nodes('/Data') t(x)

	SELECT	efd.Concept,
			-------------------------------------------------------------------------------------
			CASE pr.IdentificationType
				WHEN 3 THEN 11
				WHEN 2 THEN 12
				WHEN 0 THEN 13
				WHEN 1 THEN 22
				WHEN 7 THEN 31
				WHEN 4 THEN 41
				else 43
			END AS IdentificationTypeRecive,
			tpr.Nit AS IdentificationNumberRecive,
			IIF(pr.IdentificationType = 7, tpr.DigitVerification, NULL) DigitVerificationRecive,
			IIF(pr.IdentificationType = 7, NULL, pr.FirstLastName) AS FirstLastNameRecive, 
			IIF(pr.IdentificationType = 7, NULL, pr.SecondLastName) AS SecondLastNameRecive, 
			IIF(pr.IdentificationType = 7, NULL, pr.FirstName) AS FirstNameRecive, 
			IIF(pr.IdentificationType = 7, NULL, pr.SecondName) AS SecondNameRecive, 
			IIF(pr.IdentificationType = 7, tpr.Name, NULL) AS BusinessNameRecive, 
			adr.Addresss AS AddressRecive, 
			cr.Code AS CodeCityRecive, 
			dr.Code AS CodeDepartmentRecive, 			
			ctr.Code AS CodeCountryRecive,
			-------------------------------------------------------------------------------------
			CACP.CrossingValue + CACC.CrossingValue AS ValueOperation, 
			CACP.CrossingValue AS IngressValue, 
			CACC.CrossingValue AS RetentionValue, 
			-------------------------------------------------------------------------------------
			CASE ps.IdentificationType
				WHEN 3 THEN 11
				WHEN 2 THEN 12
				WHEN 0 THEN 13
				WHEN 1 THEN 22
				WHEN 7 THEN 31
				WHEN 4 THEN 41
				else 43
			END AS IdentificationTypeSend, 
			tps.Nit AS IdentificationNumberSend,
			IIF(ps.IdentificationType = 7, tps.DigitVerification, NULL) DigitVerificationSend,
			IIF(ps.IdentificationType = 7, NULL, ps.FirstLastName) AS FirstLastNameSend, 
			IIF(ps.IdentificationType = 7, NULL, ps.SecondLastName) AS SecondLastNameSend, 
			IIF(ps.IdentificationType = 7, NULL, ps.FirstName) AS FirstNameSend, 
			IIF(ps.IdentificationType = 7, NULL, ps.SecondName) AS SecondNameSend, 
			IIF(ps.IdentificationType = 7, tps.Name, NULL) AS BusinessNameSend, 
			ads.Addresss AS AddressSend, 
			cs.Code AS CodeCitySend, 
			ds.Code AS CodeDepartmentSend, 			
			cts.Code AS CodeCountrySend
	FROM Treasury.CrossingAccount ca
	JOIN Treasury.CrossingAccountDetailCxC cacc ON ca.Id = cacc.CrossingAccountId
	JOIN Treasury.CrossingAccountDetailCxP cacp ON ca.Id = cacp.CrossingAccountId
	JOIN
	(
		SELECT	efdr.Concept,
				efdr.MainAccountId ReciveMainAccountId, 
				efds.MainAccountId SendMainAccountId
		FROM GeneralLedger.ExogenousFormatDetail efdr, GeneralLedger.ExogenousFormatDetail efds
		WHERE efdr.ExogenousFormatId = efds.ExogenousFormatId 
			AND efdr.Concept = efds.Concept
			AND efdr.ExogenousFormatId = @ExogenousFormatId
			AND efdr.Nature = 1 AND efds.Nature = 2
		GROUP BY efdr.Concept, efdr.MainAccountId, efds.MainAccountId
	) efd ON cacc.MainAccountId = efd.ReciveMainAccountId AND cacp.MainAccountId = efd.SendMainAccountId
	-------------------------------------------------------------------------------------
	JOIN Portfolio.AccountReceivable ar ON cacc.AccountReceivableId = ar.Id
	JOIN Common.ThirdParty tpr ON ar.ThirdPartyId = tpr.Id 
	JOIN Common.Person pr ON tpr.PersonId = pr.Id 
	LEFT JOIN [Common].[ViewLatestAddressByPerson] vapr ON pr.Id = vapr.PersonId
	LEFT JOIN Common.Address adr WITH (NOLOCK) ON vapr.AddressId = adr.Id
	LEFT JOIN Common.City cr WITH (NOLOCK) ON adr.CityId = cr.Id
	LEFT JOIN Common.Department dr WITH (NOLOCK) ON cr.DepartamentId = dr.Id
	LEFT JOIN Common.Country ctr WITH (NOLOCK) ON dr.CountryId = ctr.Id
	-------------------------------------------------------------------------------------
	JOIN [Payments].[AccountPayable] ap ON cacp.AccountPayableId = ap.Id	
	JOIN Common.ThirdParty tps ON ap.IdThirdParty = tps.Id 
	JOIN Common.Person ps ON tps.PersonId = ps.Id 
	LEFT JOIN [Common].[ViewLatestAddressByPerson] vaps ON ps.Id = vaps.PersonId
	LEFT JOIN Common.Address ads WITH (NOLOCK) ON vaps.AddressId = ads.Id
	LEFT JOIN Common.City cs WITH (NOLOCK) ON ads.CityId = cs.Id
	LEFT JOIN Common.Department ds WITH (NOLOCK) ON cs.DepartamentId = ds.Id
	LEFT JOIN Common.Country cts WITH (NOLOCK) ON ds.CountryId = cts.Id
	WHERE YEAR(ca.DocumentDate) = @YearData
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la información tributaria requerida para el Formato Exógeno 1647 de la DIAN, correspondiente a operaciones de cruce entre cuentas por cobrar y cuentas por pagar de un año fiscal determinado. Cruza los documentos de compensación de tesorería (Treasury.CrossingAccount) con sus detalles de CxC y CxP, filtrando las cuentas contables según la configuración del formato exógeno indicado (ExogenousFormatDetail). Para cada operación entrega los datos de identificación completos (tipo y número de documento, nombre o razón social, dirección, ciudad, departamento y país) tanto del tercero que recibe (receptor de la cuenta por cobrar) como del tercero que envía (pagador de la cuenta por pagar), junto con los valores de la operación, el ingreso y la retención aplicada. Se utiliza en el proceso de preparación y reporte de información exógena fiscal ante la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1647';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1647';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el insumo de datos para el reporte exógeno DIAN Formato 1647, cruzando información de cuentas por cobrar y por pagar compensadas en tesorería con los terceros emisor y receptor del año fiscal indicado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1647';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId.; Debe existir configuración en ExogenousFormatDetail para el formato indicado con conceptos que tengan registros tanto de naturaleza 1 (recibe) como 2 (envía).; Los cruces de tesorería deben tener detalle tanto en CxC como en CxP asociados a cuentas principales configuradas en el formato exógeno.; Las cuentas por cobrar y por pagar deben referenciar terceros con persona asociada.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1647';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cruces cuyo año del DocumentDate coincide con el año parámetro.; Cada concepto reportado proviene de pares de configuración (Nature=1 y Nature=2) bajo el mismo ExogenousFormatId y mismo Concept.; El ValueOperation reportado es la suma del CrossingValue de CxC y CxP del mismo cruce.; Cuando el tercero es persona jurídica (IdentificationType=7) los campos de nombres se anulan y solo se reporta razón social y dígito de verificación.; Los códigos de tipo de identificación reportados se ajustan a la tabla DIAN (11,12,13,22,31,41,43).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1647';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena DIAN; Formato 1647; Cruce de cuentas / compensación; Cuentas por cobrar; Cuentas por pagar; Terceros; Tipo de identificación tributaria; NIT y dígito de verificación; Retención; Ingresos', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1647';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un conjunto de filas con datos de identificación, dirección y valores de operación para cada cruce de cuentas cuyo DocumentDate esté en el año indicado y cuyas cuentas principales coincidan con las configuradas en el formato exógeno.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1647';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IdentificationType del tercero (receptor o emisor) → Mapea a códigos DIAN: 3→11, 2→12, 0→13, 1→22, 7→31, 4→41 else Cualquier otro valor se mapea a 43; si IdentificationType = 7 (NIT/persona jurídica) → Reporta DigitVerification y BusinessName (razón social) del ThirdParty; omite nombres y apellidos de la persona else Reporta nombres y apellidos de la persona natural; omite razón social y dígito de verificación; si ExogenousFormatDetail.Nature → Nature=1 identifica la cuenta principal del lado receptor (CxC) y Nature=2 la del lado emisor (CxP), uniendo ambos por mismo Concept y ExogenousFormatId', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1647';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxC; Treasury.CrossingAccountDetailCxP; GeneralLedger.ExogenousFormatDetail; Portfolio.AccountReceivable; Payments.AccountPayable; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1647';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1647';
-- GO
