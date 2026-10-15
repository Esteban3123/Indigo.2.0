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