



CREATE VIEW [Billing].[ViewListBillingConcept]
as

SELECT 
bc.*
FROM Billing.BillingConcept bc
LEFT JOIN Billing.BillingConcept bc2 ON bc.Id = bc2.AssociatedMainServiceId
WHERE bc2.AssociatedMainServiceId IS NULL AND bc.ConceptType = 1