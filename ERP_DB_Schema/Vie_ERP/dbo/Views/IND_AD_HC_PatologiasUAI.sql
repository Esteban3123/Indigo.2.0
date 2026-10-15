CREATE VIEW [dbo].[IND_AD_HC_PatologiasUAI]
AS
     SELECT DISTINCT TOP (100) PERCENT C.NUMEFOLIO AS Folio, 
                                       I.NUMINGRES AS NumeroIngreso, 
                                       C.IPCODPACI AS Identificación, 
                                       P.IPNOMCOMP AS NombrePaciente, 
                                       RTRIM(LTRIM(dbo.Edad(CONVERT(VARCHAR, P.IPFECNACI, 105), CONVERT(VARCHAR, GETDATE(), 105)))) AS Edad, 
                                       YEAR(GETDATE()) - YEAR(P.IPFECNACI) - 1 AS AnosPaciente,
                                       CASE
                                           WHEN P.IPSEXOPAC = '1'
                                           THEN 'Masculino'
                                           WHEN P.IPSEXOPAC = '2'
                                           THEN 'Femenino'
                                       END AS Sexo, 
                                       P.IPDIRECCI AS Direccion, 
                                       P.IPTELEFON AS Telefono, 
                                       P.IPTELMOVI AS Movil, 
                                       CONVERT(VARCHAR, I.IFECHAING, 100) AS FechaIngreso, 
                                       CONVERT(VARCHAR, E.FECALTPAC, 100) AS FechaAltaMedica, 
                                       A.NOMENTIDA AS Entidad, 
                                       UF.UFUDESCRI AS Servicio, 
                                       D.CODDIAGNO AS CodDiag1, 
                                       V.NOMDIAGNO AS DiagnosticoPpal, 
                                       D.CODDIAGNO AS CodDiag2, 
                                       V2.NOMDIAGNO AS Diagnostico2, 
                                       D.CODDIAGNO AS CodDiag3, 
                                       dbo.Especialidades(dbo.MedicosEsp(C.CODPROSAL)) AS Especialidad, 
                                       I.IAUTORIZA AS NumeroAutorizacion, 
                                       I.IOBSERVAC AS Observaciones, 
                                       I.IFECHAING AS [Fecha Ingreso Filtro]
     FROM dbo.HCHISPACA AS C WITH(NOLOCK)
          INNER JOIN dbo.ADCENATEN AS CA WITH(NOLOCK) ON CA.CODCENATE = C.CODCENATE
          INNER JOIN dbo.INUNIFUNC AS UF WITH(NOLOCK) ON UF.UFUCODIGO = C.UFUCODIGO
          INNER JOIN dbo.ADINGRESO AS I WITH(NOLOCK) ON I.NUMINGRES = C.NUMINGRES
          INNER JOIN dbo.INPACIENT AS P WITH(NOLOCK) ON P.IPCODPACI = C.IPCODPACI
          INNER JOIN dbo.INENTIDAD AS A WITH(NOLOCK) ON A.CODENTIDA = I.CODENTIDA
          INNER JOIN dbo.INDIAGNOP AS D WITH(NOLOCK) ON D.NUMINGRES = C.NUMINGRES
                                                        AND D.IPCODPACI = C.IPCODPACI
                                                        AND D.CODDIAPRI = 'True'
          INNER JOIN dbo.INDIAGNOS AS V WITH(NOLOCK) ON V.CODDIAGNO = D.CODDIAGNO
          LEFT OUTER JOIN dbo.INDIAGNOP AS D2 WITH(NOLOCK) ON D2.NUMINGRES = C.NUMINGRES
                                                              AND D2.IPCODPACI = C.IPCODPACI
                                                              AND D2.CODDIAPRI = 'False'
          LEFT OUTER JOIN dbo.INDIAGNOS AS V2 WITH(NOLOCK) ON V2.CODDIAGNO = D2.CODDIAGNO
          LEFT OUTER JOIN dbo.HCREGEGRE AS E WITH(NOLOCK) ON E.IPCODPACI = C.IPCODPACI
                                                             AND E.NUMINGRES = C.NUMINGRES
     WHERE(C.CODCENATE IN('001', '00102'))
          AND (C.UFUCODIGO IN('N30', 'N40'))
AND (I.IFECHAING > '01/01/2019 00:00:00');