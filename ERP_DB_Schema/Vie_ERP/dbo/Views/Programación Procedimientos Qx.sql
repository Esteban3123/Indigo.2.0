
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