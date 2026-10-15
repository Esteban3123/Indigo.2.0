CREATE VIEW [dbo].[Resolucion_256_Triage2]
AS
SELECT        CASE WHEN U.IPTIPODOC = '1' THEN 'CC' WHEN U.IPTIPODOC = '2' THEN 'CE' WHEN U.IPTIPODOC = '3' THEN 'TI' WHEN U.IPTIPODOC = '4' THEN 'RC' WHEN U.IPTIPODOC = '5' THEN 'PA' WHEN U.IPTIPODOC = '6' THEN ' AS'
                          WHEN U.IPTIPODOC = '7' THEN 'MS' END AS [Tipo de identificación del usuario ], C.IPCODPACI AS Número_de_identificación_del_usuario, CONVERT(VARCHAR(10), U.IPFECNACI, 103) AS Fecha_Nacimiento_Pac, 
                         CASE WHEN U.IPSEXOPAC = '1' THEN 'Masculino' WHEN U.IPSEXOPAC = '2' THEN 'Femenino' END AS Sexo_del_Usuario, U.IPPRIAPEL AS Primer_apellido, U.IPSEGAPEL AS Segundo_apellido_del_Usuario, 
                         U.IPPRINOMB AS Primer_Nombre_del_Usuario, U.IPSEGNOMB AS Segundo_Nombre, F.HealthEntityCode AS Código_EAPB, CONVERT(VARCHAR(10), A.TRIAFECHA, 103) AS Fecha_TRIAGE, RIGHT(A.TRIAFECHA, 7) 
                         AS Hora_TRIAGE, CONVERT(VARCHAR(10), k.FECHINIHI, 103) AS Fecha_CONSULTA_URGENCIAS, RIGHT(k.FECHINIHI, 7) AS Hora_CONSULTA_URGENCIAS, A.TRIAGECLA AS ClasificacionTriage
FROM            dbo.ADCONTURG AS C WITH (nolock) LEFT OUTER JOIN
                         dbo.INPACIENT AS U WITH (nolock) ON U.IPCODPACI = C.IPCODPACI LEFT OUTER JOIN
                         Contract.CareGroup AS E WITH (nolock) ON E.Id = U.GENCAREGROUP LEFT OUTER JOIN
                         dbo.ADTRIAGEU AS A WITH (nolock) ON A.TRIANUMER = C.CODCONCEC LEFT OUTER JOIN
                         dbo.INPROFSAL AS P WITH (nolock) ON P.CODPROSAL = A.CODPROSAL LEFT OUTER JOIN
                         dbo.INPROFSAL AS Pa WITH (nolock) ON Pa.CODPROSAL = C.PROAUSENT LEFT OUTER JOIN
                         dbo.INESPECIA AS ES WITH (nolock) ON ES.CODESPECI = P.CODESPEC1 LEFT OUTER JOIN
                         dbo.ADCATTRIU AS ds WITH (nolock) ON ds.TRIACATEG = A.TRIACATEG LEFT OUTER JOIN
                         dbo.HCURGING1 AS k WITH (nolock) ON k.IPCODPACI = A.IPCODPACI AND k.NUMINGRES = A.NUMINGRES AND k.UFUCODIGO = C.UFUCODIGO LEFT OUTER JOIN
                         dbo.INPROFSAL AS PAIU WITH (NOLOCK) ON PAIU.CODPROSAL = k.CODPROSAL LEFT OUTER JOIN
                         dbo.ADINGRESO AS I WITH (NOLOCK) ON I.NUMINGRES = A.NUMINGRES LEFT OUTER JOIN
                         dbo.INDIAGNOS AS DX WITH (NOLOCK) ON DX.CODDIAGNO = I.CODDIAING LEFT OUTER JOIN
                         dbo.INDIAGNOS AS DXA WITH (NOLOCK) ON DXA.CODDIAGNO = I.CODDIAEGR LEFT OUTER JOIN
                         Contract.HealthAdministrator AS F WITH (nolock) ON F.Id = I.GENCONENTITY
WHERE        (C.IPFECLLEGA >= '01/09/2020 00:00:00')