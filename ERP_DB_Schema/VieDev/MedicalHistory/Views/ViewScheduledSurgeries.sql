

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