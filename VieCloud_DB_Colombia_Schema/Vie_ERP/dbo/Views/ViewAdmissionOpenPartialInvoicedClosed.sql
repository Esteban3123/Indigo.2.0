

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ingresos o admisiones de pacientes que se encuentran en estado Abierto (sin estado), Parcialmente facturado (P), Facturado (F) o Cerrado (C). Combina datos del episodio de ingreso (ADINGRESO), la información demográfica del paciente (INPACIENT) y la entidad aseguradora o pagadora asociada (INENTIDAD). Sirve para reportería y gestión de admisiones activas o recientes, permitiendo identificar por número de ingreso, cédula del paciente, nombre, fecha de nacimiento, sexo, fecha de admisión, tipo de ingreso, cama, plan de beneficios, entidad (EPS/aseguradora), número de autorización, responsable, unidad funcional y estado de la admisión. Incluye un campo calculado con la descripción completa del ingreso (número de ingreso, cédula y nombre del paciente) útil para búsquedas y selección en interfaces de usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionOpenPartialInvoicedClosed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionOpenPartialInvoicedClosed';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos/admisiones de pacientes que se encuentran en estado abierto, parcialmente facturado, facturado o cerrado, junto con datos del paciente y la entidad responsable de pago.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenPartialInvoicedClosed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso debe tener un paciente válido en el maestro de pacientes (INNER JOIN sobre IPCODPACI).; Cada ingreso debe tener una entidad responsable válida en el directorio de entidades (INNER JOIN sobre CODENTIDA).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenPartialInvoicedClosed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen admisiones en estados operativos del ciclo de facturación: abierto, parcialmente facturado, facturado o cerrado.; Se construye un identificador legible de la admisión concatenando código de ingreso, código de paciente y nombre completo del paciente.; Se usan lecturas con NOLOCK, por lo que los resultados pueden incluir datos no confirmados (lecturas sucias).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenPartialInvoicedClosed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso de paciente; Paciente; Entidad responsable de pago; Plan de beneficios; Autorización; Responsable del paciente; Unidad funcional; Cama/Estancia; Tipo de liquidación; Estado de facturación de la admisión (abierto/parcial/facturado/cerrado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenPartialInvoicedClosed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Devuelve únicamente ingresos cuyo estado (IESTADOIN) sea '' '' (abierto), ''P'' (parcialmente facturado), ''F'' (facturado) o ''C'' (cerrado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenPartialInvoicedClosed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.IESTADOIN IN ('' '',''P'',''F'',''C'') → Se incluye el ingreso en el resultado else Se excluye (estados como anulado u otros no se exponen)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenPartialInvoicedClosed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenPartialInvoicedClosed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenPartialInvoicedClosed';
GO
