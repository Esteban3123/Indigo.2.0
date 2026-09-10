

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los ingresos o admisiones de pacientes que están actualmente abiertos (en curso) o en estado parcial, es decir, aquellos que aún no han sido cerrados ni completamente liquidados. Combina datos del episodio de admisión (número de ingreso, tipo, fecha, cama, plan de beneficios, número de autorización, estado de liquidación) con información del paciente (cédula, nombre completo, fecha de nacimiento, sexo), la entidad pagadora (EPS, aseguradora o empresa contratante), el grupo de atención del contrato y la administradora de salud asociada. Se usa principalmente en módulos de admisiones, facturación y seguimiento de camas para identificar qué pacientes tienen ingresos pendientes de cerrar o liquidar, facilitando la gestión operativa y la conciliación de cuentas abiertas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionOpenAndPartial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionOpenAndPartial';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos/admisiones de pacientes que están abiertos o en estado parcial, enriquecidos con datos del paciente, entidad responsable, grupo de cuidado y administradora de salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso debe tener paciente existente en el maestro de pacientes (INNER JOIN); Cada ingreso debe tener entidad responsable existente en el directorio de entidades (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ingresos en estado abierto ('' '') o parcial (''P''); se excluyen los demás estados; El indicador de tratamiento especial nunca se entrega nulo: se reemplaza por 0; El grupo de cuidado y la administradora de salud son opcionales (LEFT JOIN); su ausencia no excluye el ingreso; Se construye una etiqueta concatenada con número de ingreso, código y nombre del paciente; Se realizan lecturas con NOLOCK (lecturas sucias permitidas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso de paciente; Paciente; Entidad responsable de pago; Plan de beneficios; Autorización; Cama/Estancia; Tipo de liquidación; Unidad funcional; Grupo de cuidado (CareGroup); Administradora de salud (HealthAdministrator); Tratamiento especial; Responsable del paciente; Estado de ingreso (abierto/parcial)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna ingresos cuyo estado (IESTADOIN) sea '' '' (abierto) o ''P'' (parcial)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.IESTADOIN = '' '' → Incluye el ingreso como abierto; si i.IESTADOIN = ''P'' → Incluye el ingreso como parcial; si i.TRATAESPECIA IS NULL → Se entrega como 0 (ISNULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; Contract.CareGroup; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartial';
GO
