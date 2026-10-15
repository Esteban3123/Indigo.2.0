CREATE VIEW [Common].[ViewLatestEmailByPerson]
AS
	SELECT e.IdPerson PersonId, MAX(e.Id) EmailId
	FROM Common.Email e WITH (NOLOCK)
	GROUP BY e.IdPerson