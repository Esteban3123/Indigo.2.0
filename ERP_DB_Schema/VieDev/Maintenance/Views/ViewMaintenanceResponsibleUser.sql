CREATE VIEW [Maintenance].[ViewMaintenanceResponsibleUser]
AS
SELECT	mr.Id,
		mr.Code,
		u.UserCode,
		mr.ResponsibleRole
FROM Maintenance.MaintenanceResponsible mr WITH (NOLOCK)
JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
JOIN Security.Person sp ON tp.Nit = sp.Identification
JOIN Security.[User] u ON sp.Id = u.IdPerson
WHERE mr.Status = 1