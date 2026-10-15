

CREATE VIEW [Budget].[ViewAvailabilityBalance]
AS

SELECT a.Id
      ,Code
      ,BudgetaryValidityId
      ,DependencyId
      ,DocumentDate
      ,ExpirationDays
      ,ExpirationDate
      ,AvailabilityType
      ,Observations
      ,Status
	  ,(select sum(balance) from Budget.AvailabilityDetail as ad where a.id	= ad.AvailabilityId) as BalanceAvaliability
  FROM Budget.Availability as a