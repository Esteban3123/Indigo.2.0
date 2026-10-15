
CREATE VIEW [dbo].[IND_AS_HC_PendientesAtenderGineco]
AS
SELECT        CASE a.codtippac WHEN '1' THEN 'Materna' END AS Tipo, A.IPFECLLEGA AS FechaHoraLlegada, A.IPCODPACI AS Identificacion, A.IPNOMCOMP AS Paciente, 
                         RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), CONVERT(varchar, GETDATE(), 105)))) AS Edad, E.NOMENTIDA AS Entidad, DATEDIFF(MINUTE,
                          A.IPFECLLEGA, GETDATE()) AS MinutosEspera
FROM            dbo.ADCONTURG AS A WITH (nolock) LEFT OUTER JOIN
                         dbo.ADTRIAGEU AS t WITH (nolock) ON t.CODCONCEC = A.CODCONCEC INNER JOIN
                         dbo.INPACIENT AS P WITH (nolock) ON P.IPCODPACI = A.IPCODPACI INNER JOIN
                         dbo.INENTIDAD AS E WITH (nolock) ON E.CODENTIDA = A.CODENTIDA
WHERE        (A.CODCENATE = '001') AND (A.UFUCODIGO = 'N12') AND (A.IPFECLLEGA >= '19/09/2016 00:00:00') AND (A.CONESTADO = '1')