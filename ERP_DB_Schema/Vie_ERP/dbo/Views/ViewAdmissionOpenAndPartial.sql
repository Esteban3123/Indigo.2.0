


CREATE VIEW [dbo].[ViewAdmissionOpenAndPartial]
AS
SELECT        i.NUMINGRES AS AdmissionCode, p.IPCODPACI AS PatientCode, p.IPNOMCOMP AS PatientName, p.IPFECNACI AS PatientDateBirth, p.IPSEXOPAC AS PatientGenus, i.IFECHAING AS AdmissionDate, 
                         i.TIPOINGRE AS AdmissionType, i.CODICAMHO AS BedStay, i.IINGREPOR AS PlaceEntry, i.ILIQUIDAC AS LiquidationType, i.CODPANATE AS BenefitPlan, e.CODENTIDA AS EntityCode, 
                         e.NOMENTIDA AS EntityName, i.IAUTORIZA AS AuthorizationNumber, i.IPRNOMBRE AS ResponsibleName, i.IPTELEFON AS ResponsiblePhone, i.GENCAREGROUP AS CareGroupId, 
                         i.GENCONENTITY AS HealthAdministratorId, i.UFUCODIGO AS FunctionalUnitCode, i.IESTADOIN AS Status, CONCAT('No. INGRESO: ', RTRIM(i.NUMINGRES), ' PACIENTE: ', RTRIM(p.IPCODPACI) + ' - ' + RTRIM(p.IPNOMCOMP)) 
                         AS FullNameAdmission , ISNULL (i.TRATAESPECIA, 0) as TRATAESPECIA, CONCAT(cg.Code, ' - ', cg.Name) AS CareGroupCodeName, CONCAT(ha.Code, ' - ', ha.Name) AS HealthAdministratorCodeName
FROM            dbo.ADINGRESO AS i with(nolock) 
				INNER JOIN dbo.INPACIENT AS p with(nolock) ON i.IPCODPACI = p.IPCODPACI 
				INNER JOIN dbo.INENTIDAD AS e with(nolock) ON i.CODENTIDA = e.CODENTIDA
				LEFT JOIN Contract.CareGroup as cg with(nolock) on i.GENCAREGROUP = cg.Id
				LEFT JOIN Contract.HealthAdministrator as ha with(nolock) on i.GENCONENTITY = ha.Id
WHERE        (i.IESTADOIN = ' ') OR (i.IESTADOIN = 'P')