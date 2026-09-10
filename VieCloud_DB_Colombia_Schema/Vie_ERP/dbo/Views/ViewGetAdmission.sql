
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información completa de las admisiones o ingresos de pacientes, combinando datos del episodio de atención (número de ingreso, fecha, tipo de ingreso, cama, plan de beneficios, autorización, estado y tipo de liquidación) con los datos personales del paciente (cédula, nombre completo, fecha de nacimiento y sexo) y los datos de la entidad aseguradora o pagadora (EPS, ARS u otro pagador) asociada al ingreso. Integra las tablas de ingresos (ADINGRESO), pacientes (INPACIENT) y entidades externas (INENTIDAD) para ofrecer en una sola consulta todo lo necesario para identificar quién fue admitido, cuándo, bajo qué modalidad y con qué entidad responsable del pago. Es el punto de partida para reportería de urgencias, hospitalización, consulta externa, facturación y auditoría de ingresos. Incluye un campo descriptivo combinado con el número de ingreso y la identificación del paciente, útil para búsquedas y listados administrativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewGetAdmission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewGetAdmission';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone una vista consolidada de admisiones de pacientes uniendo datos demográficos del paciente y la entidad pagadora asociada, con un descriptor textual del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe tener un paciente existente en INPACIENT (IPCODPACI coincidente).; El ingreso debe tener una entidad existente en INENTIDAD (CODENTIDA coincidente).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen admisiones con paciente y entidad existentes (INNER JOIN excluye huérfanos).; El campo FullNameAdmission siempre se compone con el patrón ''No. INGRESO: <num> PACIENTE: <cod> - <nombre>''.; El código del centro de atención se entrega sin espacios en blanco a los extremos (TRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión / ingreso de paciente; Paciente; Entidad pagadora / aseguradora; Tipo de ingreso; Cama / estancia hospitalaria; Plan de beneficios; Autorización; Responsable del paciente; Grupo de atención; Administradora de salud; Tratamiento especial; Centro de atención; Liquidación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Devuelve una fila por cada admisión que tenga paciente y entidad válidos, incluyendo datos del ingreso, paciente y entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewGetAdmission';
GO
