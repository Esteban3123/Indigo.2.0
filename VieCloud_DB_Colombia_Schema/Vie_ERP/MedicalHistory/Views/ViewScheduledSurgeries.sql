

CREATE VIEW [MedicalHistory].[ViewScheduledSurgeries]
AS
SELECT A.CODAUTONU AS 'IDQX', A.CODCENATE , A.FECHORAIN , A.FECHORAFI , A.CODESTPQX ,A.IDAGENDA , B.CODCONCEC AS IDSALA, B.DESCRIPSAL AS 'Sala', CONVERT(VARCHAR(5), A.FECHORAIN, 108) AS 'Hora', RTRIM(A.IPCODPACI) AS IPCODPACI, RTRIM(A.IPCODPACI) + ' - ' + RTRIM(C.IPNOMCOMP) 
                  AS 'Paciente', RTRIM(C.IPTELEFON) AS TELEFONO, RTRIM(C.IPTELMOVI) AS CELULAR, RTRIM(E.CODSERIPS) + ' - ' + RTRIM(E.DESSERIPS) + '. ' + ISNULL(CD.Name, '') AS 'Procedimiento', RTRIM(F.CODPROSAL) 
                  + ' - ' + RTRIM(F.NOMMEDICO) AS 'Profesional', RTRIM(G.CODESPECI) + ' - ' + RTRIM(G.DESESPECI) AS 'Especialidad', A.FECHORAIN AS 'HoraInicial', A.FECHORAFI AS 'HoraFinal', A.CODESTPQX AS 'ESTADO', 
                  CASE A.CODESTPQX WHEN 0 THEN 'Cirugia Programada' WHEN 1 THEN 'Paciente admitido' WHEN 2 THEN 'Paciente en sala de espera' WHEN 3 THEN 'Paciente en sala quirurgíca' WHEN 4 THEN 'Paciente en recuperación' WHEN 5 THEN 'Paciente con alta'
                   WHEN 6 THEN 'Anulado' END AS 'DescripcionEstado', A.PRINCIPAL, CASE A.PRINCIPAL WHEN 1 THEN 'QX Principal - SI' ELSE 'QX - Principal NO' END AS 'DescripcionPrincipal', 
                  CASE A.ORIGENQX WHEN 1 THEN A.NUMINGRES WHEN 2 THEN CASE A.AUTOHCORDPROQ WHEN NULL THEN
                      (SELECT H.NUMINGRES
                       FROM      HCORDPRON H
                       WHERE   H.AUTO = A.AUTOHCORDPRON) ELSE
                      (SELECT H.NUMINGRES
                       FROM      HCORDPROQ H
                       WHERE   H.AUTO = A.AUTOHCORDPROQ) END END AS NUMINGRES, A.ORIGENQX, CASE A.ORIGENQX WHEN 1 THEN 'Ambulatoria' ELSE 'Hospitalaria' END AS 'ORIGENQXDescripcion', 'Servicio: ' + RTRIM(E.CODSERIPS) 
                  + ' - ' + RTRIM(E.DESSERIPS) + '. ' + ISNULL(CD.Name, '') + ' ' + CHAR(10) + 'Profesional: ' + RTRIM(F.CODPROSAL) + ' - ' + RTRIM(F.NOMMEDICO) + CHAR(10) + 'Especialidad:' + RTRIM(ISNULL(G.CODESPECI, '')) 
                  + ' - ' + RTRIM(ISNULL(G.DESESPECI, '')) + CHAR(10) + 'Principal: ' + CASE A.PRINCIPAL WHEN 1 THEN 'SI' ELSE 'NO' END + CHAR(10) 
                  + 'Origen Cirugía: ' + CASE A.ORIGENQX WHEN 1 THEN 'Ambulatoria' ELSE 'Hospitalaria' END AS 'Contenido', A.ESTADOFARM AS 'EstadoFarmacia', RTRIM(F.CODPROSAL) AS CODPROSAL, RTRIM(A.CODSERIPS) AS CODSERIPS, 
                  CASE A.TIPOANESTESIA WHEN 1 THEN 'Local' WHEN 2 THEN 'Regional' WHEN 3 THEN 'General' WHEN 4 THEN 'Combinada' WHEN 5 THEN 'No aplica' END AS TipoAnestesia, CASE A.AUTOHCORDPROQ WHEN NULL THEN
                      (SELECT CASE H.PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' ELSE 'Programada' END Prioridad
                       FROM      HCORDPRON H
                       WHERE   H.AUTO = A.AUTOHCORDPRON) ELSE
                      (SELECT CASE H.PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' ELSE 'Programada' END Prioridad
                       FROM      HCORDPROQ H
                       WHERE   H.AUTO = A.AUTOHCORDPROQ) END AS Prioridad, CASE A.PRINCIPAL WHEN 1 THEN 'SI' ELSE 'NO' END AS Expr1, CASE A.ORIGENQX WHEN 1 THEN 'Ambulatoria' ELSE 'Hospitalaria' END AS Origen
FROM     dbo.AGEPROGQX AS A WITH (nolock) INNER JOIN
                  dbo.AGENSALAC AS B WITH (nolock) ON A.AGENSALAC = B.CODCONCEC INNER JOIN
                  dbo.INPACIENT AS C WITH (nolock) ON A.IPCODPACI = C.IPCODPACI INNER JOIN
                  dbo.INCUPSIPS AS E WITH (nolock) ON A.CODSERIPS = E.CODSERIPS INNER JOIN
                  dbo.INPROFSAL AS F WITH (nolock) ON A.CODPROSAL = F.CODPROSAL LEFT OUTER JOIN
                  dbo.INESPECIA AS G WITH (nolock) ON A.CODESPECI = G.CODESPECI LEFT OUTER JOIN
                  Contract.CUPSEntityContractDescriptions AS CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT OUTER JOIN
                  Contract.ContractDescriptions AS CD ON CD.Id = CDD.ContractDescriptionId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todas las cirugías y procedimientos quirúrgicos programados, integrando en una sola consulta la información del paciente (cédula, nombre, teléfono, celular), la sala quirúrgica asignada, el procedimiento CUPS con su descripción de contrato, el profesional de salud, la especialidad, los horarios de inicio y fin, el tipo de anestesia, la prioridad (emergencia, urgencia, normal) y el estado actual de la cirugía (programada, paciente admitido, en sala de espera, en sala quirúrgica, en recuperación, con alta o anulada). Combina la programación quirúrgica (AGEPROGQX) con el maestro de salas (AGENSALAC), el directorio de pacientes (INPACIENT), el catálogo de servicios CUPS (INCUPSIPS), el maestro de profesionales (INPROFSAL), las especialidades médicas (INESPECIA) y las descripciones de contrato (CUPSEntityContractDescriptions / ContractDescriptions), permitiendo distinguir si la cirugía es ambulatoria u hospitalaria y si el procedimiento es principal o secundario. Está diseñada para el seguimiento operativo del quirófano, la gestión de salas de cirugía, la consulta del historial médico y la reportería de agendamiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'VIEW', @level1name = N'ViewScheduledSurgeries';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'VIEW', @level1name = N'ViewScheduledSurgeries';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de cirugías programadas con datos enriquecidos de sala, paciente, procedimiento, profesional, especialidad, estado, prioridad, origen y tipo de anestesia para visualización en agenda quirúrgica.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'VIEW', @level1name=N'ViewScheduledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cirugía debe tener sala asignada existente en AGENSALAC; El paciente debe existir en INPACIENT; El servicio/procedimiento debe existir en INCUPSIPS; El profesional debe existir en INPROFSAL', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'VIEW', @level1name=N'ViewScheduledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado de la cirugía siempre se presenta tanto en código (CODESTPQX) como en descripción legible; El campo Paciente concatena identificación y nombre completo; El campo Procedimiento concatena código IPS, descripción IPS y descripción de contrato (si existe); El origen se clasifica binariamente como Ambulatoria (1) u Hospitalaria (cualquier otro valor); La especialidad y la descripción de contrato son opcionales (LEFT JOIN); Todas las consultas a tablas se hacen con NOLOCK, por lo que se permiten lecturas sucias', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'VIEW', @level1name=N'ViewScheduledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cirugía programada; Sala quirúrgica; Paciente; Procedimiento (CUPS/IPS); Profesional de la salud; Especialidad médica; Estado quirúrgico; Cirugía principal; Origen ambulatorio/hospitalario; Tipo de anestesia; Prioridad de atención (Emergencia/Urgencia/Normal/Programada); Orden quirúrgica; Número de ingreso; Estado de farmacia; Contrato/Descripción de contrato', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'VIEW', @level1name=N'ViewScheduledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalHistory.ViewScheduledSurgeries: Devuelve una fila por cada cirugía programada en AGEPROGQX cruzada con sala, paciente, procedimiento, profesional, especialidad y descripción de contrato', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'VIEW', @level1name=N'ViewScheduledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTPQX in (0..6) → Traduce el código a descripción: 0=Cirugía Programada, 1=Paciente admitido, 2=Sala de espera, 3=Sala quirúrgica, 4=Recuperación, 5=Alta, 6=Anulado; si PRINCIPAL = 1 → Marca la cirugía como ''QX Principal - SI'' else ''QX - Principal NO''; si ORIGENQX = 1 → Origen ''Ambulatoria'' y NUMINGRES tomado directamente de AGEPROGQX else Origen ''Hospitalaria'' y NUMINGRES obtenido de HCORDPROQ (o HCORDPRON si AUTOHCORDPROQ es NULL); si TIPOANESTESIA in (1..5) → Traduce a: 1=Local, 2=Regional, 3=General, 4=Combinada, 5=No aplica; si PRISERIPS de la orden quirúrgica/no quirúrgica → Traduce prioridad: 1=Emergencia, 2=Urgencia, 3=Normal, 4=Definir Conducta, otro=Programada', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'VIEW', @level1name=N'ViewScheduledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.AGENSALAC; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INESPECIA; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCORDPRON; dbo.HCORDPROQ', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'VIEW', @level1name=N'ViewScheduledSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'VIEW', @level1name=N'ViewScheduledSurgeries';
GO
