
CREATE VIEW [Authorization].[ViewAuthorizationAnnexes]
AS
SELECT
    a.[Id],
    a.[AuthorizationControlId],
    a.[HealthAdministratorId],
    a.[Consecutive],
    a.[Folio],
    a.[DiagnosticCode],
    a.[TypeRequestServices],
    CASE ac.SubjectType
        WHEN 1 THEN 'Autorización de Servicio'
        WHEN 2 THEN 'Autorización de Medicamento'
        WHEN 3 THEN 'Autorización de Insumo'
        WHEN 4 THEN 'Autorización de Urgencia'
        WHEN 5 THEN 'Autorización de Estancia'
        ELSE 'Otro'
    END AS TypeRequestServicesName,
    CONCAT(IIF(AC.SubjectCode IS NOT NULL, CONCAT(AC.SubjectCode,' - '), ''), AC.ServiceDescription) AS [SubjectCodeDescription],
    a.[PriorityAttention],
    a.[Justification],
    a.[AnnexBatchId],
    a.[CreationUser],
    a.[CreationDate],
    ac.[RequestedQuantity],
    ISNULL(ha.[Name], '') AS [PayerName], 
    AC.Status AS ControlStatus,
    CASE AC.Status 
        WHEN 1 THEN 'No Solicitado' 
        WHEN 2 THEN 'En Trámite' 
        WHEN 3 THEN 'Radicado' 
        WHEN 4 THEN 'Autorizado' 
        WHEN 5 THEN 'Entregado' 
        WHEN 6 THEN 'Confirmado'
        WHEN 7 THEN 'Cancelado' 
        WHEN 8 THEN 'Rechazado' 
        ELSE 'Desconocido' 
     END AS EstadoAutorizacion,
     (SELECT TOP 1 CASE e.[Status]
        WHEN 2 THEN 'En Trámite'
        WHEN 3 THEN 'Radicado'
        WHEN 4 THEN 'Autorizado'
        WHEN 8 THEN 'Rechazado'
        ELSE 'Otro'
    END AS [StatusName] FROM [Authorization].[AuthorizationEvents] e WHERE e.AuthorizationAnnexesId = a.Id ORDER BY e.RegistrationDate DESC) AS EstadoEvento
FROM [Authorization].[AuthorizationAnnexes]  a
INNER JOIN [Authorization].[AuthorizationControl] ac ON ac.[Id] = a.[AuthorizationControlId]
LEFT JOIN [Contract].[HealthAdministrator]       ha ON ha.[Id] = a.[HealthAdministratorId]
GO
