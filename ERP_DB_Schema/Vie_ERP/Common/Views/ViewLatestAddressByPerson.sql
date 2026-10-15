CREATE VIEW [Common].[ViewLatestAddressByPerson]
AS
	SELECT a.IdPerson PersonId, MAX(a.Id) AddressId
	FROM Common.Address a WITH (NOLOCK)
	GROUP BY a.IdPerson