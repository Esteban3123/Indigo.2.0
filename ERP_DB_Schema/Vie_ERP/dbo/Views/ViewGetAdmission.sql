
CREATE VIEW [dbo].[ViewGetAdmission]
AS

select a.NUMINGRES AdmissionCode,
p.IPCODPACI PatientCode,
p.IPNOMCOMP PatientName,
p.IPFECNACI PatientDateBirth,
p.IPSEXOPAC PatientGenus,
a.IFECHAING AdmissionDate,
a.TIPOINGRE AdmissionType,
a.CODICAMHO BedStay,
a.IINGREPOR PlaceEntry,
a.ILIQUIDAC LiquidationType,
a.CODPANATE BenefitPlan,
e.CODENTIDA EntityCode,
e.NOMENTIDA EntityName,
a.IAUTORIZA AuthorizationNumber,
a.IPRNOMBRE ResponsibleName,
a.IPTELEFON ResponsiblePhone,
a.GENCAREGROUP CareGroupId,
a.GENCONENTITY HealthAdministratorId,
a.IESTADOIN Status,
a.TRATAESPECIA TRATAESPECIA,
'No. INGRESO: ' + RTRIM(a.NUMINGRES) + ' PACIENTE: ' + RTRIM(p.IPCODPACI) + ' - ' + RTRIM(p.IPNOMCOMP) FullNameAdmission,
trim(a.CODCENATE) CenterAttentionCode
from dbo.ADINGRESO a
inner join dbo.INPACIENT p on p.IPCODPACI = a.IPCODPACI
inner join dbo.INENTIDAD e on e.CODENTIDA = a.CODENTIDA