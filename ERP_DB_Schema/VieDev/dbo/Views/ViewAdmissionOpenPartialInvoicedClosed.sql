


CREATE VIEW [dbo].[ViewAdmissionOpenPartialInvoicedClosed]
AS
SELECT        i.NUMINGRES AS AdmissionCode, p.IPCODPACI AS PatientCode, p.IPNOMCOMP AS PatientName, p.IPFECNACI AS PatientDateBirth, p.IPSEXOPAC AS PatientGenus, i.IFECHAING AS AdmissionDate, 
                         i.TIPOINGRE AS AdmissionType, i.CODICAMHO AS BedStay, i.IINGREPOR AS PlaceEntry, i.ILIQUIDAC AS LiquidationType, i.CODPANATE AS BenefitPlan, e.CODENTIDA AS EntityCode, 
                         e.NOMENTIDA AS EntityName, i.IAUTORIZA AS AuthorizationNumber, i.IPRNOMBRE AS ResponsibleName, i.IPTELEFON AS ResponsiblePhone, i.GENCAREGROUP AS CareGroupId, 
                         i.GENCONENTITY AS HealthAdministratorId, i.UFUCODIGO AS FunctionalUnitCode, i.IESTADOIN AS Status, RTRIM(i.NUMINGRES) + ' - ' + RTRIM(p.IPCODPACI) + ' - ' + RTRIM(p.IPNOMCOMP) 
                         AS FullNameAdmission
FROM            dbo.ADINGRESO AS i with(nolock) INNER JOIN
                         dbo.INPACIENT AS p with(nolock) ON i.IPCODPACI = p.IPCODPACI INNER JOIN
                         dbo.INENTIDAD AS e with(nolock) ON i.CODENTIDA = e.CODENTIDA
WHERE        (i.IESTADOIN = ' ') OR (i.IESTADOIN = 'P') OR (i.IESTADOIN = 'F') OR (i.IESTADOIN = 'C')