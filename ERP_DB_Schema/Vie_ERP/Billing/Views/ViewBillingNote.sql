CREATE VIEW [Billing].[ViewBillingNote]
AS
WITH Cte_ElectronicDocument AS (
	SELECT
		ROW_NUMBER() OVER (PARTITION BY ed.EntityName, ed.EntityId ORDER BY ed.Id DESC) AS RN,
		ed.EntityId,
		ed.Status,
		ed.CUFE,
		ed.ValidationDate
	FROM Billing.ElectronicDocument ed
	WHERE ed.EntityName = 'BillingNote'
)

-------

SELECT	bn.Id,
		IIF(bn.Nature = 1, 'NOTA DEBITO', 'NOTA CREDITO') NoteTypeName,
		bn.Code,
		bn.NoteDate,
		bn.Observations,
		------------------------------- CLIENTE -------------------------------
		CONCAT(tp.Nit, '-', tp.DigitVerification) CustomerPartyNit,
		tp.Name CustomerPartyName,
		a.Addresss CustomerAddress,
		c.Name CustomerCityName,
		-----------------------  DATOS NOTA ELECTRONICA -----------------------
		ISNULL(bn.CUDE, ed.CUFE) CUDE,
		ISNULL(bn.QR, CONCAT('https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=', ISNULL(bn.CUDE, ed.CUFE))) QR,
		CASE ed.Status
			WHEN 0 THEN 'Erronea'
			WHEN 1 THEN 'Registrada'
			WHEN 2 THEN 'Enviada'
			WHEN 3 THEN 'Valida'
			WHEN 4 THEN 'Invalida'
			WHEN 88 THEN 'Envio en Proceso'
			WHEN 99 THEN 'Validación en Proceso'
			ELSE 'Procesando...'
		END StatusDIAN,
		ed.ValidationDate,
		pn.NoteType,
		pn.EntityName PortfolioNoteEntityName
FROM Common.ThirdParty tp WITH (NOLOCK)
JOIN Billing.BillingNote bn WITH (NOLOCK) ON tp.Id = bn.CustomerPartyId
LEFT JOIN Portfolio.PortfolioNote pn WITH(NOLOCK) on pn.Id = bn.EntityId and bn.EntityName ='PortfolioNote'
-------------------------------------------------------------------------------
LEFT JOIN 
(
	SELECT IdPerson, MIN(Id) Id
	FROM Common.Address WITH (NOLOCK)
	GROUP BY IdPerson
) am ON tp.PersonId = am.IdPerson
LEFT JOIN Common.Address a WITH (NOLOCK) ON am.Id = a.Id
LEFT JOIN Common.City c WITH (NOLOCK) ON a.CityId = c.Id
-------------------------------------------------------------------------------
LEFT JOIN Cte_ElectronicDocument ed WITH (NOLOCK) ON bn.Id = ed.EntityId AND ed.RN = 1
GO