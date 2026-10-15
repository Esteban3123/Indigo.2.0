CREATE VIEW [Payments].[ViewThirdPartyRetentionAccumulated]
AS
SELECT	CONCAT(ap.ThirdPartyId, '-', YEAR(ap.DocumentDate)) Id
		, ap.ThirdPartyId
		, YEAR(ap.DocumentDate) [Year]
		, SUM(ap.ExemptIncome) ExemptIncome
		, SUM(ap.MaxDeductionsAndRentExents) MaxDeductionsAndRentExents
FROM [Payments].[ViewThirdPartyRetention] ap
GROUP BY ap.ThirdPartyId, YEAR(ap.DocumentDate)