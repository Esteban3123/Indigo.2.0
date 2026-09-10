
CREATE VIEW [dbo].[Programación Procedimientos Qx]
AS
SELECT        TOP (100) PERCENT A.CODAUTONU AS 'IDQX', B.CODCONCEC AS IDSALA, B.DESCRIPSAL AS 'Sala', CONVERT(VARCHAR(5), A.FECHORAIN, 108) AS 'Hora', RTRIM(A.IPCODPACI) AS IPCODPACI, 
                         RTRIM(A.IPCODPACI) + ' - ' + RTRIM(C.IPNOMCOMP) AS 'Paciente', RTRIM(C.IPTELEFON) AS TELEFONO, RTRIM(C.IPTELMOVI) AS CELULAR, RTRIM(E.CODSERIPS) + ' - ' + RTRIM(E.DESSERIPS) 
                         AS 'Procedimiento', CASE PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' ELSE 'Programada' END AS 'Prioridad', 
                         RTRIM(F.CODPROSAL) + ' - ' + RTRIM(F.NOMMEDICO) AS 'Profesional', RTRIM(G.CODESPECI) + ' - ' + RTRIM(G.DESESPECI) AS 'Especialidad', A.FECHORAIN AS 'HoraInicial', A.FECHORAFI AS 'HoraFinal', 
                         RTRIM(U.CODUSUARI) + ' - ' + RTRIM(U.NOMUSUARI) AS 'USUARIOASIGNO', A.CODESTPQX AS 'ESTADO', 
                         CASE A.CODESTPQX WHEN 0 THEN 'Cirugia Programada' WHEN 1 THEN 'Paciente admitido' WHEN 2 THEN 'Paciente en sala de espera' WHEN 3 THEN 'Paciente en sala quirurgíca' WHEN 4 THEN 'Paciente en recuperación'
                          WHEN 5 THEN 'Paciente con alta' WHEN 6 THEN 'Anulado' END AS 'DescripcionEstado', A.PRINCIPAL, CASE A.PRINCIPAL WHEN 1 THEN 'QX Principal - SI' ELSE 'QX - Principal NO' END AS 'DescripcionPrincipal',
                          J.UFUCODIGO, J.UFUTIPUNI, RTRIM(J.UFUDESCRI) AS 'Unidad Funcional', CASE A.ORIGENQX WHEN 1 THEN A.NUMINGRES ELSE I.NUMINGRES END AS NUMINGRES, A.ORIGENQX, 
                         CASE A.ORIGENQX WHEN 1 THEN 'Ambulatoria' ELSE 'Hospitalaria' END AS 'ORIGENQXDescripcion', 'Servicio: ' + RTRIM(E.CODSERIPS) + ' - ' + RTRIM(E.DESSERIPS) + CHAR(10) 
                         + 'Profesional: ' + RTRIM(F.CODPROSAL) + ' - ' + RTRIM(F.NOMMEDICO) + CHAR(10) + 'Especialidad:' + RTRIM(G.CODESPECI) + ' - ' + RTRIM(G.DESESPECI) + CHAR(10) 
                         + 'Principal: ' + CASE A.PRINCIPAL WHEN 1 THEN 'SI' ELSE 'NO' END + CHAR(10) + 'Origen Cirugía: ' + CASE A.ORIGENQX WHEN 1 THEN 'Ambulatoria' ELSE 'Hospitalaria' END AS 'Contenido', 
                         A.ESTADOFARM AS 'EstadoFarmacia', RTRIM(CE.CODCENATE) + ' - ' + RTRIM(CE.NOMCENATE) AS CentroAtencion
FROM            dbo.AGEPROGQX AS A WITH (nolock) INNER JOIN
                         dbo.AGENSALAC AS B WITH (nolock) ON A.AGENSALAC = B.CODCONCEC INNER JOIN
                         dbo.INPACIENT AS C WITH (nolock) ON A.IPCODPACI = C.IPCODPACI INNER JOIN
                         dbo.INCUPSIPS AS E WITH (nolock) ON A.CODSERIPS = E.CODSERIPS INNER JOIN
                         dbo.INPROFSAL AS F WITH (nolock) ON A.CODPROSAL = F.CODPROSAL INNER JOIN
                         dbo.INESPECIA AS G WITH (nolock) ON A.CODESPECI = G.CODESPECI INNER JOIN
                         dbo.SEGusuaru AS U WITH (nolock) ON A.CODUSUASI = U.CODUSUARI INNER JOIN
                         dbo.INUNIFUNC AS J WITH (nolock) ON B.UFUCODIGO = J.UFUCODIGO INNER JOIN
                         dbo.ADCENATEN AS CE WITH (nolock) ON CE.CODCENATE = A.CODCENATE LEFT OUTER JOIN
                         dbo.SEGusuaru AS UC ON UC.CODUSUARI = A.CODUSUCAN LEFT OUTER JOIN
                         dbo.HCORDPROQ AS H WITH (nolock) ON A.AUTOHCORDPROQ = H.AUTO LEFT OUTER JOIN
                         dbo.ADINGRESO AS I WITH (nolock) ON H.NUMINGRES = I.NUMINGRES
WHERE        (A.FECHORAIN BETWEEN '01/07/2019 00:00' AND '05/07/2019 23:59') AND (A.CODCENATE = '001') AND (A.CODESTPQX IN (0, 1, 2, 4, 5, 6, 7))
ORDER BY 'HoraInicial'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la programación de cirugías y procedimientos quirúrgicos agendados para un centro de atención y rango de fechas específico, integrando en una sola consulta los datos del paciente (cédula, nombre, teléfono, celular), la sala quirúrgica asignada, el procedimiento CUPS/IPS ordenado, el profesional de la salud y su especialidad, la unidad funcional, el estado actual de la cirugía (programada, paciente admitido, en sala, en recuperación, con alta, anulada), la prioridad (emergencia, urgencia, normal, programada), el origen de la cirugía (ambulatoria u hospitalaria) y el usuario que realizó la asignación. Compone información de los maestros de salas (AGENSALAC), pacientes (INPACIENT), servicios CUPS (INCUPSIPS), profesionales (INPROFSAL), especialidades (INESPECIA), unidades funcionales (INUNIFUNC), centros de atención (ADCENATEN), usuarios del sistema (SEGusuaru) y órdenes de procedimientos quirúrgicos de historia clínica (HCORDPROQ), permitiendo a los gestores de salas de cirugía y al personal administrativo visualizar el tablero diario de cirugías con toda la información operativa necesaria para la gestión de quirófanos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Programación Procedimientos Qx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Programación Procedimientos Qx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista la programación de procedimientos quirúrgicos con datos de sala, paciente, profesional, especialidad, estado y origen para visualización en agenda quirúrgica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Programación Procedimientos Qx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de relaciones íntegras entre AGEPROGQX y los maestros de salas, paciente, CUPS, profesional, especialidad, usuario, unidad funcional y centro de atención; El centro de atención filtrado debe ser ''001''; Las cirugías deben tener estado en (0,1,2,4,5,6,7); Las cirugías deben tener fecha/hora inicial entre 01/07/2019 y 05/07/2019', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Programación Procedimientos Qx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda cirugía listada pertenece al centro de atención ''001''; Se excluye el estado 3 (Paciente en sala quirúrgica) del listado pese a estar definido en el mapeo; El NUMINGRES expuesto depende del origen: ambulatorio usa el de la cirugía; hospitalario usa el del ingreso ligado a la orden quirúrgica de historia clínica; La unidad funcional se deriva de la sala asignada, no directamente de la cirugía; Las consultas a tablas operativas se hacen con NOLOCK (lecturas sucias permitidas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Programación Procedimientos Qx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Programación quirúrgica; Sala de cirugía; Paciente; Procedimiento (CUPS); Prioridad quirúrgica (Emergencia/Urgencia/Normal/Programada); Estado del procedimiento quirúrgico; Profesional de salud; Especialidad médica; Unidad funcional; Cirugía principal; Origen de cirugía (Ambulatoria/Hospitalaria); Centro de atención; Ingreso hospitalario; Estado de farmacia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Programación Procedimientos Qx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGEPROGQX: Se retornan únicamente cirugías programadas en el centro ''001'', con FECHORAIN entre 01/07/2019 00:00 y 05/07/2019 23:59 y estado CODESTPQX en (0,1,2,4,5,6,7), ordenadas por hora inicial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Programación Procedimientos Qx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRISERIPS = ''1'' → Prioridad = ''Emergencia'' else ''2''→Urgencia, ''3''→Normal, ''4''→Definir Conducta, otro→Programada; si CODESTPQX entre 0..6 → Mapea a descripción: 0=Cirugía Programada, 1=Paciente admitido, 2=En sala de espera, 3=En sala quirúrgica, 4=En recuperación, 5=Con alta, 6=Anulado; si PRINCIPAL = 1 → Marca como ''QX Principal - SI'' else ''QX - Principal NO''; si ORIGENQX = 1 → Origen ''Ambulatoria'' y NUMINGRES tomado de AGEPROGQX else Origen ''Hospitalaria'' y NUMINGRES tomado de ADINGRESO vía HCORDPROQ', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Programación Procedimientos Qx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.AGENSALAC; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INESPECIA; dbo.SEGusuaru; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.HCORDPROQ; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Programación Procedimientos Qx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Programación Procedimientos Qx';
GO
