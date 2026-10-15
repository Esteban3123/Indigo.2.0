CREATE VIEW [Billing].[ViewOpenRevenueReportSummary] AS
SELECT	
		ROW_NUMBER() OVER(ORDER BY v.CareGroupId ASC ) AS Id,
		v.CareGroupId,
		v.CareGroup,
		v.EntityId,
		v.Entity,
		v.CodCareCenter,
		v.CareCenter,
		v.CodCreationUser,
		v.CreationUser,
		v.ConceptStatusFolio,
		v.dateIncome,
		v.Status,
		COUNT(v.Id) AS QuantityFolios,
		RTRIM(v.Income) AS Income
		FROM Billing.ViewOpenRevenueReport v
		GROUP BY	CareGroup,
					Entity,
					CareCenter,
					CreationUser, 
					ConceptStatusFolio,
					v.CareGroupId,
					v.EntityId, 
					v.CodCareCenter,
					v.CodCreationUser,
					dateIncome,
					v.Status,
					v.Income