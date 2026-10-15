CREATE VIEW [Payments].[ViewAccountObligation]
AS
	SELECT DISTINCT
		CAST(od.EntityId AS VARCHAR(20)) Id, od.EntityId AccountPayableId, o.Id ObligationId, o.Code ObligationCode
	FROM Budget.Obligation o
	JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
	WHERE od.EntityName = 'AccountPayable'