

CREATE VIEW [dbo].[ViewAdmissionOpenAndPartialServiceOrder]
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
				LEFT JOIN Billing.SettingsBilling sb on cg.OperativeUnitId = sb.IdOperatingUnit
WHERE        (i.IESTADOIN = ' ') OR (i.IESTADOIN = 'P') or (i.IESTADOIN = 'B' and sb.IncomeLockType =1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que lista todos los ingresos (admisiones) de pacientes que tienen órdenes de servicio abiertas o parcialmente liquidadas. Combina datos del ingreso (ADINGRESO) con información del paciente (nombre, fecha de nacimiento, sexo, cédula), la entidad pagadora o aseguradora (EPS/ARS), el grupo de atención del contrato y la administradora de salud asociada. Filtra únicamente los ingresos en estado abierto (sin liquidar), parcialmente liquidado o bloqueado con tipo de bloqueo de ingreso activo según la configuración de facturación. Sirve como fuente de consulta para módulos de facturación, autorización y gestión de camas, permitiendo identificar qué pacientes hospitalizados o en atención tienen órdenes médicas o servicios pendientes de cierre o liquidación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionOpenAndPartialServiceOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionOpenAndPartialServiceOrder';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos/admisiones que se encuentran abiertos, parciales o bloqueados por configuración de facturación, junto con datos del paciente, entidad responsable, grupo de atención y administradora de salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartialServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe tener paciente existente en el maestro de pacientes; El ingreso debe tener entidad responsable existente en el directorio de entidades', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartialServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna ingresos con estado distinto a abierto ('' ''), parcial (''P'') o bloqueado (''B'' con IncomeLockType=1); Si TRATAESPECIA es nulo, se reporta como 0; Pacientes y entidad son obligatorios (INNER JOIN); CareGroup, HealthAdministrator y SettingsBilling son opcionales (LEFT JOIN); Se construye una etiqueta descriptiva del ingreso con número y datos del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartialServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso; Paciente; Entidad responsable/Pagador; Autorización; Plan de beneficios; Grupo de atención (CareGroup); Administradora de salud; Unidad funcional; Cama/Estancia; Tipo de liquidación; Tratamiento especial; Bloqueo de ingreso por facturación; Estado de admisión (abierto/parcial/bloqueado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartialServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Solo retorna ingresos cuando IESTADOIN='' '' (abierto), IESTADOIN=''P'' (parcial), o IESTADOIN=''B'' siempre que la unidad operativa del CareGroup tenga IncomeLockType=1 en SettingsBilling', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartialServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IESTADOIN = '' '' o IESTADOIN = ''P'' → Se incluye el ingreso en el resultado sin condiciones adicionales else Se evalúa si está bloqueado y la configuración de facturación lo permite; si IESTADOIN = ''B'' y SettingsBilling.IncomeLockType = 1 para la unidad operativa del CareGroup → Se incluye el ingreso bloqueado en el resultado else Se excluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartialServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; Contract.CareGroup; Contract.HealthAdministrator; Billing.SettingsBilling', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartialServiceOrder';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionOpenAndPartialServiceOrder';
GO
