
create VIEW [dbo].[ViewAdmissionServiceOrder]
AS
SELECT        i.NUMINGRES AS AdmissionCode, p.IPCODPACI AS PatientCode, p.IPNOMCOMP AS PatientName, p.IPFECNACI AS PatientDateBirth, p.IPSEXOPAC AS PatientGenus, i.IFECHAING AS AdmissionDate, 
                         i.TIPOINGRE AS AdmissionType, i.CODICAMHO AS BedStay, i.IINGREPOR AS PlaceEntry, i.ILIQUIDAC AS LiquidationType, i.CODPANATE AS BenefitPlan, e.CODENTIDA AS EntityCode, 
                         e.NOMENTIDA AS EntityName, i.IAUTORIZA AS AuthorizationNumber, i.IPRNOMBRE AS ResponsibleName, i.IPTELEFON AS ResponsiblePhone, i.GENCAREGROUP AS CareGroupId, 
                         i.GENCONENTITY AS HealthAdministratorId, i.UFUCODIGO AS FunctionalUnitCode, i.IESTADOIN AS Status, RTRIM(i.NUMINGRES) + ' - ' + RTRIM(p.IPCODPACI) + ' - ' + RTRIM(p.IPNOMCOMP) 
                         AS FullNameAdmission
FROM            dbo.ADINGRESO AS i with(nolock) INNER JOIN
                         dbo.INPACIENT AS p with(nolock) ON i.IPCODPACI = p.IPCODPACI INNER JOIN
                         dbo.INENTIDAD AS e with(nolock) ON i.CODENTIDA = e.CODENTIDA
WHERE        (i.IESTADOIN = ' ') OR (i.IESTADOIN = 'P') OR (i.IESTADOIN = 'F')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ingresos o admisiones activas de pacientes (en estado abierto, pendiente o finalizado) junto con los datos personales del paciente y la entidad pagadora (EPS, aseguradora o empresa contratante). Combina la tabla de admisiones (ADINGRESO), el directorio de pacientes (INPACIENT) y el catálogo de entidades (INENTIDAD) para ofrecer en una sola consulta información de: número de ingreso, cédula y nombre del paciente, fecha de nacimiento, sexo, fecha de admisión, tipo de ingreso, cama, plan de beneficios, número de autorización, responsable, unidad funcional y estado. Sirve como fuente principal para órdenes de servicio, agendamiento clínico y reportes de atención, permitiendo identificar rápidamente un episodio de atención mediante el campo resumen FullNameAdmission que concatena número de ingreso, cédula e identificación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionServiceOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionServiceOrder';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de admisiones activas/parciales/finalizadas con datos básicos del paciente y de la entidad pagadora asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso debe tener un paciente válido en INPACIENT y una entidad válida en INENTIDAD para aparecer en la vista.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone admisiones cuyo estado sea vacío ('' ''), ''P'' o ''F''; excluye cualquier otro estado (p.ej. anuladas o cerradas distintas).; Toda admisión expuesta tiene paciente y entidad asociados existentes (INNER JOIN con INPACIENT e INENTIDAD).; Compone un identificador legible concatenando número de ingreso, código y nombre del paciente separados por '' - ''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso; Paciente; Entidad pagadora; Autorización; Plan de beneficios; Unidad funcional; Cama/estancia; Grupo de atención; Administrador de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Devuelve solo ingresos con IESTADOIN en ('' '', ''P'', ''F''), enriquecidos con datos del paciente y de la entidad responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionServiceOrder';
GO
