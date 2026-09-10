
CREATE VIEW [Authorization].[ViewAuthorizationEvents]
AS
SELECT
    e.[Id],
    e.[AuthorizationControlId],
    e.[AuthorizationAnnexesId],
    e.[ReportType],
    CASE e.[ReportType]
        WHEN 1 THEN 'Teléfono'
        WHEN 2 THEN 'Envío Físico'
        WHEN 3 THEN 'Web'
        WHEN 4 THEN 'Email'
        WHEN 5 THEN 'Tramita Paciente'
        ELSE 'Otro'
    END AS [ReportTypeName],
    e.[Status],
    e.[FileNumber],
    e.[AuthorizationCode],
    e.[AuthorizedQuantity],
    e.[ContactPerson],
    e.[Charge],
    e.[PhoneNumber],
    e.[Extension],
    e.[InitialTime],
    e.[EndTime],
    e.[RadicateNumber],
    e.[SendType],
    e.[ReceivedDate],
    e.[RecipientPerson],
    e.[RecipientCharge],
    e.[WebUrl],
    e.[RegistrationDate],
    e.[RadicateNumberWebPage],
    e.[Email],
    e.[SendDate],
    e.[Instructions],
    e.[Comments],
    e.[PatientNotified],
    e.[PatientInfo],
    e.[AuthorizedBy],
    e.[AuthorizationDate],
    e.[AuthorizationExpiredDate],
    e.[CreationUser],
    e.[CreationDate],
    ISNULL(ha.[Name], '') AS [PayerName],
    CASE e.[Status]
        WHEN 2 THEN 'En Trámite'
        WHEN 3 THEN 'Radicado'
        WHEN 4 THEN 'Autorizado'
        WHEN 8 THEN 'Rechazado'
        ELSE 'Otro'
    END AS [StatusName],
    CONCAT(IIF(AC.SubjectCode IS NOT NULL, CONCAT(AC.SubjectCode,' - '), ''), AC.ServiceDescription) AS [SubjectCodeDescription]
FROM [Authorization].[AuthorizationEvents] e
INNER JOIN [Authorization].[AuthorizationControl] ac ON ac.[Id] = e.[AuthorizationControlId]
LEFT JOIN [Contract].[HealthAdministrator] ha ON ha.[Id] = e.[HealthAdministratorId]
GO
