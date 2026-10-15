
CREATE VIEW [dbo].[IND_AD_AD_IngresosAbiertos_UAI]
AS
SELECT        K.IFECHAING AS FechaLlegada, 
                         CASE K.CODTIPPAC WHEN '1' THEN 'Maternas' WHEN '2' THEN 'Menores de 5 Años' WHEN '3' THEN 'Adultos Mayores' WHEN '4' THEN 'Discapacitados' WHEN
                          '5' THEN 'Poblacion General' END AS Tipo, RTRIM(K.IPCODPACI) AS Identificacion, RTRIM(F.IPNOMCOMP) AS Paciente, CAST(0 AS BIT) AS Confirmado, 
                         CAST(0 AS BIT) AS Ausente, K.NUMINGRES AS Ingreso, F.IPFECNACI AS [Fecha Nacimiento], RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, F.IPFECNACI, 105), 
                         CONVERT(varchar, GETDATE(), 105)))) AS Edad, L.RIESGOAGRE AS RiesgoAgresión, K.VIVESOLO,
                             (SELECT        COUNT(*) AS Expr1
                               FROM            dbo.ADACOMPAN
                               WHERE        (NUMINGRES = K.NUMINGRES)) AS ACOMPANANTES, CONVERT(BIT, 0) AS Riesgo, RTRIM(F.CODENTIDA) + '-' + RTRIM(M.NOMENTIDA) 
                         AS EntidadPaciente
FROM            dbo.ADINGRESO AS K WITH (nolock) INNER JOIN
                         dbo.INPACIENT AS F WITH (nolock) ON K.IPCODPACI = F.IPCODPACI LEFT OUTER JOIN
                         dbo.ADACTIVID AS L WITH (nolock) ON F.CODACTIVI = L.codactivi LEFT OUTER JOIN
                         dbo.INENTIDAD AS M WITH (nolock) ON M.CODENTIDA = F.CODENTIDA
WHERE        (K.IFECHAING > '01/06/2017') AND (K.IESTADOIN = ' ') AND (K.NUMINGRES NOT IN
                             (SELECT DISTINCT A.NUMINGRES
                               FROM            dbo.ADINGRESO AS A WITH (nolock) INNER JOIN
                                                         dbo.AMBORDIMA AS B WITH (nolock) ON B.IPCODPACI = A.IPCODPACI AND CONVERT(varchar(10), CONVERT(date, A.IFECHAING, 106), 103) 
                                                         = CONVERT(varchar(10), CONVERT(date, B.FECORDMED, 106), 103))) AND (K.REQTRIAGE = 0 OR
                         K.REQTRIAGE IS NULL) AND (K.CODCENATE = '001') AND (K.UFUCODIGO = 'N30') AND (K.NUMINGRES NOT IN
                             (SELECT        NUMDOCUME
                               FROM            dbo.INDOCUMEN WITH (nolock)
                               WHERE        (CODDOCUME = '3'))) AND
                             ((SELECT        COUNT(*) AS Expr1
                                 FROM            dbo.HCHISPACA WITH (nolock)
                                 WHERE        (NUMINGRES = K.NUMINGRES)) = 0)